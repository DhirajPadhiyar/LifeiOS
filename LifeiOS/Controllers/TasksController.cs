using LifeiOS.Data;
using LifeiOS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(
    string? searchTerm,
    string? filter,
    string? sortOrder,
    int page = 1)
        {
            const int pageSize = 10;
            var query = _context.TaskItems.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim();

                query = query.Where(t =>
                    t.Title.Contains(searchTerm) ||
                    (t.Description != null && t.Description.Contains(searchTerm)));
            }

            // Filters
            switch (filter)
            {
                case "pending":
                    query = query.Where(t => !t.IsCompleted);
                    break;

                case "completed":
                    query = query.Where(t => t.IsCompleted);
                    break;

                case "high":
                    query = query.Where(t => t.Priority == TaskPriority.High);
                    break;

                case "today":
                    query = query.Where(t =>
                        t.DueDate.HasValue &&
                        t.DueDate.Value.Date == DateTime.Today);
                    break;
            }
            query = sortOrder switch
            {
                // Title
                "title_desc" => query.OrderByDescending(t => t.Title),
                "title" => query.OrderBy(t => t.Title),

                // Priority
                "priority_desc" => query.OrderByDescending(t => t.Priority),
                "priority" => query.OrderBy(t => t.Priority),

                // Due Date
                "due_desc" => query.OrderByDescending(t => t.DueDate),
                "due" => query.OrderBy(t => t.DueDate),

                // Created Date
                "created" => query.OrderBy(t => t.CreatedAt),

                // Default View (Pending first, Completed last)
                _ => query
                        .OrderBy(t => t.IsCompleted)
                        .ThenByDescending(t => t.CreatedAt)
            };

            var totalTasks = await query.CountAsync();

            var tasks = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

            ViewBag.SearchTerm = searchTerm;
            ViewBag.Filter = filter;

            ViewBag.CurrentPage = page;

            ViewBag.TotalPages = (int)Math.Ceiling(totalTasks / (double)pageSize);

            ViewBag.SearchTerm = searchTerm;

            ViewBag.Filter = filter;
            ViewBag.TotalTasks = await _context.TaskItems.CountAsync();

            ViewBag.PendingTasks = await _context.TaskItems
                .CountAsync(t => !t.IsCompleted);

            ViewBag.CompletedTasks = await _context.TaskItems
                .CountAsync(t => t.IsCompleted);

            ViewBag.HighPriorityTasks = await _context.TaskItems
                .CountAsync(t => t.Priority == TaskPriority.High);

            ViewBag.DueTodayTasks = await _context.TaskItems
                .CountAsync(t =>
                    t.DueDate.HasValue &&
                    t.DueDate.Value.Date == DateTime.Today);

            ViewBag.SortOrder = sortOrder;
            return View(tasks);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return View(task);
            }

            task.CreatedAt = DateTime.Now;

            _context.TaskItems.Add(task);

            await _context.SaveChangesAsync();

            TempData["ToastType"] = "success";
            TempData["ToastMessage"] = "Task created successfully.";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.TaskItems
                                     .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.TaskItems.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskItem task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(task);
            }

            var existingTask = await _context.TaskItems.FindAsync(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Priority = task.Priority;
            existingTask.DueDate = task.DueDate;
            existingTask.IsCompleted = task.IsCompleted;
            existingTask.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["ToastType"] = "success";
            TempData["ToastMessage"] = "Task updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.TaskItems
                                     .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);

            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }

            TempData["ToastType"] = "success";
            TempData["ToastMessage"] = "Task deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);

            if (task == null)
                return NotFound();

            task.IsCompleted = !task.IsCompleted;
            task.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["ToastType"] = "info";
            TempData["ToastMessage"] = task.IsCompleted
                ? "Task marked as completed."
                : "Task marked as pending.";

            return RedirectToAction(nameof(Index));
        }

    }
}
