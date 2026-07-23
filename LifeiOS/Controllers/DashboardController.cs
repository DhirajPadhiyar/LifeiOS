using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
