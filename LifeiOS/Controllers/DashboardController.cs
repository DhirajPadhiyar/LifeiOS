using LifeiOS.Data;
using LifeiOS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var nextMonth = firstDayOfMonth.AddMonths(1);

            var model = new DashboardViewModel
            {
                // Summary Cards
                TotalTasks = await _context.TaskItems.CountAsync(),
                TotalNotes = await _context.Notes.CountAsync(),
                TotalHabits = await _context.Habits.CountAsync(),
                TotalGoals = await _context.Goals.CountAsync(),
                TotalEvents = await _context.CalendarEvents.CountAsync(),

                // Task Summary
                CompletedTasks = await _context.TaskItems.CountAsync(x => x.IsCompleted),
                PendingTasks = await _context.TaskItems.CountAsync(x =>
    !x.IsCompleted &&
    (!x.DueDate.HasValue || x.DueDate.Value.Date >= today)),
                OverdueTasks = await _context.TaskItems.CountAsync(x =>
    !x.IsCompleted &&
    x.DueDate.HasValue &&
    x.DueDate.Value.Date < today),

                // Recent Tasks
                RecentTasks = await _context.TaskItems
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(4)
                    .ToListAsync(),

                // Latest Notes
                LatestNotes = await _context.Notes
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(4)
                    .ToListAsync(),

                // Goal Progress
                Goals = await _context.Goals
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(4)
                    .ToListAsync(),

                // Today's Events
                TodayEvents = await _context.CalendarEvents
                    .Where(x => x.StartDate.Date == today)
                    .OrderBy(x => x.StartDate)
                    .Take(4)
                    .ToListAsync(),

                // This Month Expense
                TotalExpensesThisMonth = await _context.Expenses
                    .Where(x => x.ExpenseDate >= firstDayOfMonth &&
                                x.ExpenseDate < nextMonth)
                    .SumAsync(x => (decimal?)x.Amount) ?? 0
            };

            for (int i = 5; i >= 0; i--)
            {
                var month = today.AddMonths(-i);

                var startDate = new DateTime(month.Year, month.Month, 1);

                var endDate = startDate.AddMonths(1);

                model.ExpenseMonths.Add(startDate.ToString("MMM"));

                var total = await _context.Expenses
                    .Where(x => x.ExpenseDate >= startDate &&
                                x.ExpenseDate < endDate)
                    .SumAsync(x => (decimal?)x.Amount) ?? 0;

                model.MonthlyExpenses.Add(total);
            }

            return View(model);
        }
    }
}