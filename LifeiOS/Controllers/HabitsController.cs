using LifeiOS.Data;
using LifeiOS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class HabitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HabitsController> _logger;

        public HabitsController(ApplicationDbContext context, ILogger<HabitsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        private static DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }

        private static bool IsCompletedThisWeek(DateTime? lastCompletedDate)
        {
            if (!lastCompletedDate.HasValue)
                return false;

            var weekStart = GetStartOfWeek(DateTime.Today);

            return lastCompletedDate.Value.Date >= weekStart;
        }

        // GET: Habits/Create
        // GET: Habits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Habit habit)
        {
            if (!ModelState.IsValid)
            {
                return View(habit);
            }

            // Set default values for new habit
            habit.CreatedAt = DateTime.Now;
            habit.CurrentStreak = 0;
            habit.LastCompletedDate = null;

            _context.Habits.Add(habit);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error while creating habit.");

                throw;
            }

            TempData["ToastMessage"] = "Habit created successfully.";
            TempData["ToastType"] = "success";
            _logger.LogInformation(
    "Habit Created. Id={Id}, Name={Name}",
    habit.Id,
    habit.Name);

            return RedirectToAction(nameof(Index));
        }
        // GET: Habits
        public async Task<IActionResult> Index(
    string? searchString,
    string? status,
    int page = 1)
        {
            const int pageSize = 6;

            var today = DateTime.Today;
            var weekStart = GetStartOfWeek(today);

            var query = _context.Habits
                .AsNoTracking()
                .AsQueryable();

            // Search

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(h =>
                    h.Name.Contains(searchString) ||
                    (h.Description != null &&
                     h.Description.Contains(searchString)));
            }

            // Filter

            if (!string.IsNullOrWhiteSpace(status))
            {
                switch (status)
                {
                    case "completed":

                        query = query.Where(h =>

                            (h.Frequency == HabitFrequency.Daily &&
                             h.LastCompletedDate.HasValue &&
                             h.LastCompletedDate.Value.Date == today)

                            ||

                            (h.Frequency == HabitFrequency.Weekly &&
                             h.LastCompletedDate.HasValue &&
                             h.LastCompletedDate.Value.Date >= weekStart));

                        break;

                    case "pending":

                        query = query.Where(h =>

                            (h.Frequency == HabitFrequency.Daily &&
                             (!h.LastCompletedDate.HasValue ||
                              h.LastCompletedDate.Value.Date != today))

                            ||

                            (h.Frequency == HabitFrequency.Weekly &&
                             (!h.LastCompletedDate.HasValue ||
                              h.LastCompletedDate.Value.Date < weekStart)));

                        break;

                    case "daily":

                        query = query.Where(h =>
                            h.Frequency == HabitFrequency.Daily);

                        break;

                    case "weekly":

                        query = query.Where(h =>
                            h.Frequency == HabitFrequency.Weekly);

                        break;
                }
            }

            // Pending habits first, Completed habits last

            query = query
      .OrderBy(h =>

          h.Frequency == HabitFrequency.Daily

              ? h.LastCompletedDate.HasValue &&
                h.LastCompletedDate.Value.Date == today

              : h.LastCompletedDate.HasValue &&
                h.LastCompletedDate.Value.Date >= weekStart)

      .ThenByDescending(h => h.CurrentStreak)

      .ThenByDescending(h => h.CreatedAt);

            var totalHabits = await query.CountAsync();

            var habits = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Search / Filter

            ViewBag.SearchString = searchString;
            ViewBag.Status = status;

            // Pagination

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalHabits / (double)pageSize);

            // Dashboard Cards

            ViewBag.TotalHabits = await _context.Habits.CountAsync();

            ViewBag.CompletedToday = await _context.Habits.CountAsync(h =>
                h.LastCompletedDate.HasValue &&
                h.LastCompletedDate.Value.Date == today);

            ViewBag.PendingToday = await _context.Habits.CountAsync(h =>
                !h.LastCompletedDate.HasValue ||
                h.LastCompletedDate.Value.Date != today);

            ViewBag.BestStreak = await _context.Habits.AnyAsync()
                ? await _context.Habits.MaxAsync(h => h.CurrentStreak)
                : 0;

            return View(habits);
        }
        // POST: Habits/CompleteToday/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteToday(int id)
        {
            var habit = await _context.Habits.FindAsync(id);

            if (habit == null)
            {
                return NotFound();
            }

            // Already completed today
            // Already completed today
            var weekStart = GetStartOfWeek(DateTime.Today);

            if (habit.Frequency == HabitFrequency.Daily)
            {
                if (habit.LastCompletedDate.HasValue &&
                    habit.LastCompletedDate.Value.Date == DateTime.Today)
                {
                    TempData["ToastMessage"] = "Habit already completed today.";
                    TempData["ToastType"] = "info";
                    _logger.LogInformation(
    "Habit Completed. Id={Id}",
    habit.Id);

                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                if (habit.LastCompletedDate.HasValue &&
                    habit.LastCompletedDate.Value.Date >= weekStart)
                {
                    TempData["ToastMessage"] = "Habit already completed this week.";
                    TempData["ToastType"] = "info";

                    return RedirectToAction(nameof(Index));
                }
            }

            // Continue streak if yesterday was completed
            if (habit.LastCompletedDate.HasValue &&
                habit.LastCompletedDate.Value.Date == DateTime.Today.AddDays(-1))
            {
                habit.CurrentStreak++;
            }
            else
            {
                habit.CurrentStreak = 1;
            }

            habit.LastCompletedDate = DateTime.Now;

            _context.Update(habit);

            await _context.SaveChangesAsync();

            TempData["ToastMessage"] = "Habit completed successfully.";
            TempData["ToastType"] = "success";

            return RedirectToAction(nameof(Index));
        }
        // GET: Habits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habit = await _context.Habits.FindAsync(id);

            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }
        // POST: Habits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Habit habit)
        {
            if (id != habit.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(habit);
            }

            try
            {
                _context.Update(habit);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error while updating habit.");

                    throw;
                }

                TempData["ToastMessage"] = "Habit updated successfully.";
                TempData["ToastType"] = "info";
                _logger.LogInformation(
    "Habit Updated. Id={Id}",
    habit.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Habits.Any(h => h.Id == habit.Id))
                {
                    return NotFound();
                }

                throw;
            }
        }
        // GET: Habits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habit = await _context.Habits
                .FirstOrDefaultAsync(h => h.Id == id);

            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }
        // GET: Habits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habit = await _context.Habits
                .FirstOrDefaultAsync(h => h.Id == id);

            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }
        // POST: Habits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habit = await _context.Habits.FindAsync(id);

            if (habit != null)
            {
                _context.Habits.Remove(habit);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error while deleting habit.");

                    throw;
                }

                TempData["ToastMessage"] = "Habit deleted successfully.";
                TempData["ToastType"] = "delete";
                _logger.LogWarning(
    "Habit Deleted. Id={Id}",
    habit.Id);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}