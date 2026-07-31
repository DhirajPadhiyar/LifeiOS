    using LifeiOS.Data;
using LifeiOS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GoalsController> _logger;

        public GoalsController(ApplicationDbContext context, ILogger<GoalsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(
    string? searchString,
    string? status,
    int page = 1)
        {
            const int pageSize = 5;

            var query = _context.Goals.AsQueryable();

            // Search

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(g =>
                    g.GoalTitle.Contains(searchString) ||
                    (g.Description != null && g.Description.Contains(searchString)));
            }

            // Status Filter

            if (!string.IsNullOrWhiteSpace(status))
            {
                switch (status)
                {
                    case "completed":
                        query = query.Where(g => g.IsCompleted);
                        break;

                    case "progress":
                        query = query.Where(g =>
                            !g.IsCompleted &&
                            g.TargetDate >= DateTime.Today);
                        break;

                    case "overdue":
                        query = query.Where(g =>
                            !g.IsCompleted &&
                            g.TargetDate < DateTime.Today);
                        break;
                }
            }

            // Sorting

            query = query.OrderByDescending(g => g.CreatedAt);

            var totalGoals = await query.CountAsync();

            var goals = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.SearchString = searchString;
            ViewBag.Status = status;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalGoals / (double)pageSize);

            ViewBag.TotalGoals = await _context.Goals.CountAsync();

            ViewBag.CompletedGoals = await _context.Goals
                .CountAsync(g => g.IsCompleted);

            ViewBag.InProgressGoals = await _context.Goals
                .CountAsync(g =>
                    !g.IsCompleted &&
                    g.TargetDate >= DateTime.Today);

            ViewBag.OverdueGoals = await _context.Goals
                .CountAsync(g =>
                    !g.IsCompleted &&
                    g.TargetDate < DateTime.Today);

            return View(goals);
        }
        // GET: Goals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Goals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Goal goal)
        {
            if (ModelState.IsValid)
            {
                // Calculate Progress
                if (goal.TargetValue.HasValue && goal.TargetValue > 0)
                {
                    goal.Progress = (int)((goal.CurrentValue / goal.TargetValue.Value) * 100);

                    if (goal.Progress > 100)
                        goal.Progress = 100;
                }
                else
                {
                    goal.Progress = 0;
                }

                // Completed?
                goal.IsCompleted = goal.Progress >= 100;

                goal.CreatedAt = DateTime.Now;

                _context.Add(goal);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error while creating Goal.");

                    throw;
                }

                TempData["ToastMessage"] = "Goal created successfully.";
                TempData["ToastType"] = "success";
                _logger.LogInformation("Goal Created");

                return RedirectToAction(nameof(Index));
            }

            return View(goal);
        }
        // GET: Goals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals
                .FirstOrDefaultAsync(g => g.Id == id);

            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }
        // GET: Goals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals.FindAsync(id);

            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        // POST: Goals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Goal goal)
        {
            if (id != goal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Recalculate Progress
                    if (goal.TargetValue.HasValue && goal.TargetValue > 0)
                    {
                        goal.Progress = (int)((goal.CurrentValue / goal.TargetValue.Value) * 100);

                        if (goal.Progress > 100)
                            goal.Progress = 100;
                    }
                    else
                    {
                        goal.Progress = 0;
                    }

                    // Update Status
                    goal.IsCompleted = goal.Progress >= 100;

                    _context.Update(goal);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Goals.Any(e => e.Id == goal.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                TempData["ToastMessage"] = "Goal updated successfully.";
                TempData["ToastType"] = "info";
                _logger.LogInformation("Goal Updated");

                return RedirectToAction(nameof(Index));
            }

            return View(goal);
        }
        // GET: Goals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals
                .FirstOrDefaultAsync(g => g.Id == id);

            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        // POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goal = await _context.Goals.FindAsync(id);

            if (goal != null)
            {
                _context.Goals.Remove(goal);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while deleting goal.");
                    throw;
                }

                TempData["ToastMessage"] = "Goal deleted successfully.";
                TempData["ToastType"] = "delete";
                _logger.LogWarning("Goal Deleted");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}