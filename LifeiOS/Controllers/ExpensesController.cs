using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        public IActionResult Create()
        {
            return Content("Expense Create Page");
        }
    }
}
