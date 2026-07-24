using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LifeiOS.Data;
using Microsoft.EntityFrameworkCore;
using LifeiOS.Models;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(
     string? searchString,
     string? category,
     string? sortOrder,
     int page = 1)
        {
            const int pageSize = 9;

            var query = _context.Notes.AsQueryable();

            // Search

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(n =>
                    n.Title.Contains(searchString) ||
                    n.Content.Contains(searchString) ||
                    (n.Category != null && n.Category.Contains(searchString)));
            }

            // Category Filter

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(n => n.Category == category);
            }

            // Sorting

            query = sortOrder switch
            {
                "title" => query.OrderBy(n => n.Title),

                "title_desc" => query.OrderByDescending(n => n.Title),

                "category" => query.OrderBy(n => n.Category),

                "category_desc" => query.OrderByDescending(n => n.Category),

                "oldest" => query.OrderBy(n => n.CreatedAt),

                _ => query.OrderByDescending(n => n.CreatedAt)
            };

            var totalNotes = await query.CountAsync();

            var notes = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.SearchString = searchString;
            ViewBag.Category = category;
            ViewBag.SortOrder = sortOrder;

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalNotes / (double)pageSize);

            return View(notes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Note note)
        {
            if (!ModelState.IsValid)
            {
                return View(note);
            }

            note.CreatedAt = DateTime.Now;

            _context.Add(note);

            await _context.SaveChangesAsync();

            TempData["ToastMessage"] = "Note created successfully.";
            TempData["ToastType"] = "success";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var note = await _context.Notes.FindAsync(id);

            if (note == null)
                return NotFound();

            return View(note);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(note);
            }

            try
            {
                note.UpdatedAt = DateTime.Now;

                _context.Update(note);

                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = "Note updated successfully.";
                TempData["ToastType"] = "success";

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Notes.Any(n => n.Id == note.Id))
                {
                    return NotFound();
                }

                throw;
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note != null)
            {
                _context.Notes.Remove(note);

                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = "Note deleted successfully.";
                TempData["ToastType"] = "success";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
