using Microsoft.AspNetCore.Mvc;

namespace Pizzerie.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SectionEdit()
        {
            return View();
        }

        public IActionResult GaleryEdit()
        {
            return View();
        }

        public IActionResult MenuEdit()
        {
            return View();
        }

        public IActionResult Administrators()
        {
            return View();
        }

        public IActionResult Options()
        {
            return View();
        }
    }
}
