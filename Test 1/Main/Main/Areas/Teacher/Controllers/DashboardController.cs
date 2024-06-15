using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Teacher.Controllers
{
    public class DashboardController : Controller
    {
        

		[Area("Teacher")]
        public IActionResult Index(string id)
        {

            return View();
        }
    }
}
