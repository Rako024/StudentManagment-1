using Business.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Teacher.Controllers
{
    public class DashboardController : Controller
    {
        

		[Area("Teacher")]
        [Authorize(Roles ="Teacher")]
        public IActionResult Index(string id)
        {

            return View();
        }
    }
}
