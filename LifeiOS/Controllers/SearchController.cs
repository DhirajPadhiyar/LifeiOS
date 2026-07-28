using LifeiOS.Data;
using LifeiOS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View();
            }

            searchTerm = searchTerm.Trim();

            var taskQuery = _context.TaskItems
    .Where(x =>
        x.Title.Contains(searchTerm) ||
        (x.Description != null && x.Description.Contains(searchTerm)));

            ViewBag.TaskTotal = await taskQuery.CountAsync();

            ViewBag.Tasks = await taskQuery
                .Take(5)
                .ToListAsync();

            var noteQuery = _context.Notes
    .Where(x =>
        x.Title.Contains(searchTerm) ||
        (x.Content != null && x.Content.Contains(searchTerm)) ||
        (x.Category != null && x.Category.Contains(searchTerm)));

            ViewBag.NoteTotal = await noteQuery.CountAsync();

            ViewBag.Notes = await noteQuery
                .Take(5)
                .ToListAsync();

            var goalQuery = _context.Goals
    .Where(x =>
        x.GoalTitle.Contains(searchTerm) ||
        (x.Description != null && x.Description.Contains(searchTerm)));

            ViewBag.GoalTotal = await goalQuery.CountAsync();

            ViewBag.Goals = await goalQuery
                .Take(5)
                .ToListAsync();

            var habitQuery = _context.Habits
     .Where(x => x.Name.Contains(searchTerm));

            ViewBag.HabitTotal = await habitQuery.CountAsync();

            ViewBag.Habits = await habitQuery
                .Take(5)
                .ToListAsync();

            var expenseQuery = _context.Expenses
    .Where(x => x.Title.Contains(searchTerm));

            ViewBag.ExpenseTotal = await expenseQuery.CountAsync();

            ViewBag.Expenses = await expenseQuery
                .Take(5)
                .ToListAsync();

            var eventQuery = _context.CalendarEvents
    .Where(x => x.Title.Contains(searchTerm));

            ViewBag.EventTotal = await eventQuery.CountAsync();

            ViewBag.Events = await eventQuery
                .Take(5)
                .ToListAsync();

            ViewBag.TotalResults =
     ViewBag.TaskTotal +
     ViewBag.NoteTotal +
     ViewBag.GoalTotal +
     ViewBag.HabitTotal +
     ViewBag.ExpenseTotal +
     ViewBag.EventTotal;

            return View();
        }
    }
}