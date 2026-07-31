using LifeiOS.Data;
using LifeiOS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            // Habit Statistics
            var totalHabits = await _context.Habits.CountAsync();

            var completedToday = await _context.Habits.CountAsync(h =>
                h.LastCompletedDate.HasValue &&
                h.LastCompletedDate.Value.Date == DateTime.Today);

            var model = new ReportViewModel
            {
                // Tasks
                CompletedTasks = await _context.TaskItems.CountAsync(x => x.IsCompleted),

                PendingTasks = await _context.TaskItems.CountAsync(x => !x.IsCompleted),

                // Notes
                TotalNotes = await _context.Notes.CountAsync(),

                // Goals
                TotalGoals = await _context.Goals.CountAsync(),

                GoalProgress = await _context.Goals.AnyAsync()
                    ? await _context.Goals.AverageAsync(x => x.Progress)
                    : 0,

                // Habits
                TotalHabits = totalHabits,

                HabitCompletion = totalHabits > 0
                    ? (double)completedToday * 100 / totalHabits
                    : 0,

                // Expenses
                MonthlyExpenses = await _context.Expenses
                    .Where(x => x.ExpenseDate.Month == currentMonth &&
                                x.ExpenseDate.Year == currentYear)
                    .SumAsync(x => (decimal?)x.Amount) ?? 0,

                // Calendar
                UpcomingEvents = await _context.CalendarEvents
                    .CountAsync(x => x.StartDate >= DateTime.Today)
            };

            // ==============================
            // Productivity Score
            // ==============================

            double taskScore = 0;

            if (model.CompletedTasks + model.PendingTasks > 0)
            {
                taskScore =
                    (double)model.CompletedTasks /
                    (model.CompletedTasks + model.PendingTasks) * 100;
            }

            double goalScore = model.GoalProgress;

            double habitScore = model.HabitCompletion;

            model.ProductivityScore = (int)Math.Round(
                (taskScore + goalScore + habitScore) / 3
            );

            model.ProductivityLevel = model.ProductivityScore switch
            {
                >= 90 => "Excellent",
                >= 75 => "Very Good",
                >= 60 => "Good",
                >= 40 => "Average",
                _ => "Needs Improvement"
            };

            return View(model);
        }
    }
}