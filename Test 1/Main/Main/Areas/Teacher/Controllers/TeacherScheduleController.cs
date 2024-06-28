using Business.DTOs.Student;
using Business.Helper;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Teacher.Controllers
{
    [Area("Teacher"), Authorize(Roles = "Teacher")]
    public class TeacherScheduleController : Controller
    {
        private readonly ILessonTimeService _lessonTimeService;
        private readonly UserManager<AppUser> _userManager;

        public TeacherScheduleController(ILessonTimeService lessonTimeService, UserManager<AppUser> userManager)
        {
            _lessonTimeService = lessonTimeService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(DateTime? weekStartDate)
        {
            var teacher = await _userManager.GetUserAsync(User);
            if (teacher == null)
            {
                return View("Error");
            }

            var currentWeekStartDate = weekStartDate ?? DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var lessons = await _lessonTimeService.GetLessonsForWeekTeacherAsync(teacher.Id, currentWeekStartDate);

            var model = new ScheduleViewModel
            {
                WeekStartDate = currentWeekStartDate,
                WeeklySchedule = new Dictionary<DateTime, List<LessonTime>>()
            };

            
            for (int i = 0; i < 7; i++)
            {
                var day = currentWeekStartDate.AddDays(i);
                model.WeeklySchedule[day] = lessons.Where(lesson => lesson.Date.Date == day.Date).OrderBy(lesson => lesson.Date).ToList();
            }

            return View(model);
        }

        public IActionResult PreviousWeek(DateTime currentWeekStartDate)
        {
            return RedirectToAction("Index", new { weekStartDate = currentWeekStartDate.AddDays(-7) });
        }

        public IActionResult NextWeek(DateTime currentWeekStartDate)
        {
            return RedirectToAction("Index", new { weekStartDate = currentWeekStartDate.AddDays(7) });
        }
    }

}
