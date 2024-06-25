using Business.DTOs.Admin.DashboardDTOs;
using Business.LayoutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Cordinator")]
    public class DashboardController : Controller
    {
        private AdminLayoutService adminLayoutService;

        public DashboardController(AdminLayoutService adminLayoutService)
        {
            this.adminLayoutService = adminLayoutService;
        }

        public async Task<IActionResult> Index()
        {
            int teacherCount = await adminLayoutService.GetTeacherCountAsync();
            int studentCount = await adminLayoutService.GetStudentCountAsync();
            int groupCount = await adminLayoutService.GetGroupCountAsync();
            int lessonCount = await adminLayoutService.GetLessonCountAsync();
            DashboardDto dashboardDto = new DashboardDto() 
            {
                TeacherCount = teacherCount,
                StudentCount = studentCount,
                GroupCount = groupCount,
                LessonCount = lessonCount
            };
            return View(dashboardDto);
        }
    }
}
