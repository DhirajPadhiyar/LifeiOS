using LifeiOS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeiOS.Models;
using Microsoft.EntityFrameworkCore;
using LifeiOS.ViewModels;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CalendarController> _logger;

        public CalendarController(ApplicationDbContext context, ILogger<CalendarController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Calendar
        // GET: Calendar
        public async Task<IActionResult> Index(
     string? searchString,
     string? filter,
     string? sortOrder,
     int page = 1,
     string viewMode = "list",
     int? month = null,
     int? year = null)
        {
            ViewBag.ViewMode = viewMode;
            int pageSize = 9;

            var events = _context.CalendarEvents.AsQueryable();

            // Search

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                events = events.Where(e =>
                    e.Title.Contains(searchString) ||
                    (e.Description != null &&
                     e.Description.Contains(searchString)));
            }

            // Filter

            var today = DateTime.Today;

            switch (filter)
            {
                case "today":

                    events = events.Where(e =>
                        e.StartDate.Date == today);

                    break;

                case "week":

                    var weekEnd = today.AddDays(7);

                    events = events.Where(e =>
                        e.StartDate.Date >= today &&
                        e.StartDate.Date <= weekEnd);

                    break;

                case "month":

                    events = events.Where(e =>
                        e.StartDate.Month == today.Month &&
                        e.StartDate.Year == today.Year);

                    break;

                case "upcoming":

                    events = events.Where(e =>
                        e.StartDate >= DateTime.Now);

                    break;
            }

            // Sorting

            events = sortOrder switch
            {
                "oldest" => events.OrderBy(e => e.StartDate),

                "title" => events.OrderBy(e => e.Title),

                "title_desc" => events.OrderByDescending(e => e.Title),

                _ => events.OrderByDescending(e => e.CreatedAt)
            };

            int totalItems = await events.CountAsync();

            var items = await events
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.SearchString = searchString;
            ViewBag.Filter = filter;
            ViewBag.SortOrder = sortOrder;

            //var today = DateTime.Today;

            ViewBag.Month = month ?? today.Month;
            ViewBag.Year = year ?? today.Year;
            ViewBag.ViewMode = viewMode;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages =
                (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(items);
        }
        // GET: Calendar/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Calendar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CalendarEvent calendarEvent)
        {
            if (!ModelState.IsValid)
                return View(calendarEvent);

            if (calendarEvent.EndDate < calendarEvent.StartDate)
            {
                ModelState.AddModelError("", "End Date cannot be earlier than Start Date.");
                return View(calendarEvent);
            }

            calendarEvent.CreatedAt = DateTime.Now;

            _context.CalendarEvents.Add(calendarEvent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating calendar event.");
                throw;
            }

            TempData["ToastType"] = "success";
            TempData["ToastMessage"] = "Event created successfully.";
            _logger.LogInformation("Event Created");

            return RedirectToAction(nameof(Index));
        }
        // GET: Calendar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var calendarEvent = await _context.CalendarEvents
                .FirstOrDefaultAsync(e => e.Id == id);

            if (calendarEvent == null)
                return NotFound();

            return View(calendarEvent);
        }
        // GET: Calendar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var calendarEvent = await _context.CalendarEvents.FindAsync(id);

            if (calendarEvent == null)
                return NotFound();

            return View(calendarEvent);
        }
        // POST: Calendar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CalendarEvent calendarEvent)
        {
            if (id != calendarEvent.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(calendarEvent);

            if (calendarEvent.EndDate < calendarEvent.StartDate)
            {
                ModelState.AddModelError("", "End Date cannot be earlier than Start Date.");
                return View(calendarEvent);
            }

            try
            {
                _context.Update(calendarEvent);
                await _context.SaveChangesAsync();

                TempData["ToastType"] = "success";
                TempData["ToastMessage"] = "Event updated successfully.";
                _logger.LogInformation("Event Updated");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CalendarEvents.Any(e => e.Id == calendarEvent.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Calendar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var calendarEvent = await _context.CalendarEvents
                .FirstOrDefaultAsync(e => e.Id == id);

            if (calendarEvent == null)
                return NotFound();

            return View(calendarEvent);
        }
        // POST: Calendar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);

            if (calendarEvent != null)
            {
                _context.CalendarEvents.Remove(calendarEvent);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while deleting calendar event.");
                    throw;
                }

                TempData["ToastType"] = "delete";
                TempData["ToastMessage"] = "Event deleted successfully.";
                _logger.LogWarning("Event Deleted");
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Calendar/Month
        public async Task<IActionResult> Month(int? month, int? year)
        {
            var current = DateTime.Today;

            int selectedMonth = month ?? current.Month;
            int selectedYear = year ?? current.Year;

            var firstDay = new DateTime(selectedYear, selectedMonth, 1);

            var model = new CalendarViewModel
            {
                Month = selectedMonth,
                Year = selectedYear,
                CurrentMonth = firstDay,
                DaysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth),
                FirstDayOfWeek = (int)firstDay.DayOfWeek,

                Events = await _context.CalendarEvents
                    .Where(e =>
                        e.StartDate.Month == selectedMonth &&
                        e.StartDate.Year == selectedYear)
                    .ToListAsync()
            };

            return View(model);
        }
    }
}