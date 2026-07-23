using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeiOS.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        public IActionResult Create()
        {
            return Content("Note Create Page");
        }
    }
}
