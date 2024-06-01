using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
