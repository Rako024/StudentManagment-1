using Business.DTOs.Teacher.Homeworks;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Teacher.Controllers
{
    [Area("Teacher"), Authorize(Roles = "Teacher")]
    public class TeacherHomeworkController : Controller
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IStudentUserService _studentUserService;
        private readonly IHomeworkSubmissionService _homeworkSubmissionService;
        private readonly UserManager<AppUser> _userManager;
        ILessonService _lessonService;

        public TeacherHomeworkController(IHomeworkService homeworkService, UserManager<AppUser> userManager, ILessonService lessonService, IStudentUserService studentUserService, IHomeworkSubmissionService homeworkSubmissionService)
        {
            _homeworkService = homeworkService;
            _userManager = userManager;
            _lessonService = lessonService;
            _studentUserService = studentUserService;
            _homeworkSubmissionService = homeworkSubmissionService;
        }

        public async Task<IActionResult> Index(int lessonId)
        {
            Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x=>x.Id == lessonId);
            if(lesson == null)
            {
                return View("Error");
            }
            var homeworks = await _homeworkService.GetAllHomeworks(h => h.LessonId == lessonId);
            HomeworkIndexPage dto = new HomeworkIndexPage() {Homeworks = homeworks, LessonId = lessonId , Lesson = lesson};
            return View(dto);
        }

        public async Task<IActionResult> Details(int id)
        {
            var homework = await _homeworkService.GetHomework(x => x.Id == id);
            if (homework == null)
            {
                return View("Error");
            }

            var submissions = await _homeworkSubmissionService.GetAllHomeworkSubmissions(x => x.HomeworkId == id);
            var students = new List<StudentUser>();

            foreach (var submission in submissions)
            {
                var student = _studentUserService.Get(x => x.Id == submission.StudentUserId);
                if (student != null)
                {
                    students.Add(student);
                }
            }

            var model = new DetailsViewModel
            {
                Homework = homework,
                Submissions = submissions,
                Students = students
            };

            return View(model);
        }

        public IActionResult Create(int lessonId)
        {
            var model = new HomeworkDto { LessonId = lessonId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeworkDto homeworkDto)
        {
            if (!ModelState.IsValid)
            {
                return View(homeworkDto);
            }

            await _homeworkService.CreateHomework(homeworkDto);
            return RedirectToAction("Index", new { lessonId = homeworkDto.LessonId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var homework = await _homeworkService.GetHomework(x => x.Id == id);
            if (homework == null)
            {
                return View("Error");
            }

            var homeworkDto = new HomeworkDto
            {
                Id = homework.Id,
                Name = homework.Name,
                Description = homework.Description,
                LessonId = homework.LessonId
            };

            return View(homeworkDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HomeworkDto homeworkDto,int id)  
        {
            if (!ModelState.IsValid)
            {
                return View(homeworkDto);
            }

            await _homeworkService.UpdateHomework(id, homeworkDto);
            return RedirectToAction("Index", new { lessonId = homeworkDto.LessonId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var homework = await _homeworkService.GetHomework(x => x.Id == id);
            if (homework == null)
            {
                return View("Error");
            }

            await _homeworkService.DeleteHomework(id);
            return RedirectToAction("Index", new { lessonId = homework.LessonId });
        }
    }

}
