using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class GoalsController : Controller
    {
        public IActionResult Create()
        {
            return Content("Goal Create Page");
        }
    }
}
