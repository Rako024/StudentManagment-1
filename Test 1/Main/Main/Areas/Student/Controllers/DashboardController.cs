using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Student.Controllers
{
	[Area("Student")]
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
