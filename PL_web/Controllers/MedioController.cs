using Microsoft.AspNetCore.Mvc;

namespace PL_web.Controllers
{
    public class MedioController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }
    }
}
