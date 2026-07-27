using LifeiOS.Data;
using LifeiOS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenses
        // GET: Expenses
        public async Task<IActionResult> Index(
     string searchString,
     string category,
     string sortOrder,
     int page = 1)
        {
            const int pageSize = 10;

            IQueryable<Expense> query = _context.Expenses;

            // Search
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(e =>
                    e.Title.Contains(searchString) ||
                    e.Category.Contains(searchString));
            }

            // Category Filter
            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(e => e.Category == category);
            }
            ViewBag.Categories = new List<string>
{
    "Food",
    "Transport",
    "Shopping",
    "Bills",
    "Health",
    "Education",
    "Entertainment",
    "Travel",
    "Investment",
    "Subscription",
    "Personal",
    "Business",
    "Other"
};

            ViewBag.CurrentCategory = category;

            // Dashboard Cards
            ViewBag.TotalExpenses = await _context.Expenses.SumAsync(e => e.Amount);

            ViewBag.ThisMonthExpenses = await _context.Expenses
                .Where(e =>
                    e.ExpenseDate.Month == DateTime.Today.Month &&
                    e.ExpenseDate.Year == DateTime.Today.Year)
                .SumAsync(e => e.Amount);

            ViewBag.TodayExpenses = await _context.Expenses
                .Where(e => e.ExpenseDate.Date == DateTime.Today)
                .SumAsync(e => e.Amount);

            ViewBag.TotalTransactions = await _context.Expenses.CountAsync();

            // Preserve Filters
            ViewBag.SearchString = searchString;
            ViewBag.Category = category;
            ViewBag.CurrentSort = sortOrder;

            ViewBag.TitleSort = sortOrder == "title" ? "title_desc" : "title";

            ViewBag.AmountSort = sortOrder == "amount" ? "amount_desc" : "amount";

            ViewBag.DateSort = sortOrder == "date" ? "date_desc" : "date";

            // Sorting
            query = query
                .OrderByDescending(e => e.ExpenseDate)
                .ThenByDescending(e => e.CreatedAt);
            query = sortOrder switch
            {
                "title" => query.OrderBy(e => e.Title),

                "title_desc" => query.OrderByDescending(e => e.Title),

                "amount" => query.OrderBy(e => e.Amount),

                "amount_desc" => query.OrderByDescending(e => e.Amount),

                "date" => query.OrderBy(e => e.ExpenseDate),

                "date_desc" => query.OrderByDescending(e => e.ExpenseDate),

                _ => query.OrderByDescending(e => e.CreatedAt)
            };

            // Pagination
            int totalItems = await query.CountAsync();

            var expenses = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(expenses);
        }
        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return View(expense);
            }

            _context.Add(expense);

            await _context.SaveChangesAsync();

            TempData["ToastMessage"] = "Expense added successfully.";

            TempData["ToastType"] = "success";

            return RedirectToAction(nameof(Index));
        }
        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(expense);
            }

            _context.Update(expense);

            await _context.SaveChangesAsync();

            TempData["ToastMessage"] = "Expense updated successfully.";

            TempData["ToastType"] = "info";

            return RedirectToAction(nameof(Index));
        }
        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }
        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense != null)
            {
                _context.Expenses.Remove(expense);

                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = "Expense deleted successfully.";

                TempData["ToastType"] = "success";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}