using Business.DTOs.Student.Homeworks;
using Business.DTOs.Teacher.Homeworks;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Student.Controllers
{
    [Area("Student"), Authorize(Roles = "Student")]
    public class StudentHomeworkController : Controller
    {
        private readonly IHomeworkSubmissionService _homeworkSubmissionService;
        private readonly ILessonService _lessonService;
        private readonly IGroupService _groupService;
        private readonly IHomeworkService _homeworkService;
        private readonly ISemesterService _semesterService;
        private readonly IStudentUserService _studentUserService;
        private readonly UserManager<AppUser> _userManager;

        public StudentHomeworkController(IHomeworkSubmissionService homeworkSubmissionService, UserManager<AppUser> userManager, ILessonService lessonService, IHomeworkService homeworkService, IGroupService groupService, ISemesterService semesterService, IStudentUserService studentUserService)
        {
            _homeworkSubmissionService = homeworkSubmissionService;
            _userManager = userManager;
            _lessonService = lessonService;
            _homeworkService = homeworkService;
            _groupService = groupService;
            _semesterService = semesterService;
            _studentUserService = studentUserService;
        }

        public async Task<IActionResult> Index(string id)
        {
            StudentUser user = _studentUserService.Get(x => x.Id == id);
            if(user == null)
            {
                return View("Error");   
            }
            Group group = _groupService.GetGroup(x=>x.Id == user.GroupId);
            if(group == null)
            {
                return View("Error");
            }
            Semester activeSemester = _semesterService.GetSemester(x=>x.IsActive);
            List<Lesson> lessons = await _lessonService.GetAllLessons
                (
                x => x.GroupId == group.Id &&
                x.IsDeleted == false &&
                x.IsPast == false &&
                (int)x.Semester == activeSemester.SemesterNumber,
                null,
                false,
                x => x.TeacherUser
                );

            return View(lessons);
        }

        public async Task<IActionResult> Details(int lessonId)
        {
            var userId = _userManager.GetUserId(User);
            List<Homework> homeworks = await _homeworkService.GetAllHomeworks(x => x.LessonId == lessonId);
            var submissions = await _homeworkSubmissionService.GetAllHomeworkSubmissions(x => x.StudentUserId == userId);

            var model = new HomeworkDetailsViewModel
            {
                Homeworks = homeworks,
                Submissions = submissions
            };

            ViewBag.UserId = userId;
            return View(model);
        }

        public async Task<IActionResult> Submit(int homeworkId)
        {
            var homework = await _homeworkService.GetHomework(x => x.Id == homeworkId);
            if (homework == null)
            {
                return View("Error");
            }

            var homeworkSubmissionDto = new HomeworkSubmissionDto
            {
                HomeworkId = homeworkId,
                StudentUserId =  _userManager.GetUserId(User)
            };

            return View(homeworkSubmissionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(HomeworkSubmissionDto submissionDto)
        {
            if (!ModelState.IsValid)
            {
                return View(submissionDto);
            }
            Homework homework = await _homeworkService.GetHomework(x=>x.Id == submissionDto.HomeworkId);
            Lesson lesson = _lessonService.GetLesson(x => x.Id == homework.LessonId);

            await _homeworkSubmissionService.CreateHomeworkSubmission(submissionDto);
            return RedirectToAction("Details", new { lessonId = lesson.Id});
        }

        public async Task<IActionResult> Edit(int id)
        {
            var submission = await _homeworkSubmissionService.GetHomeworkSubmission(x => x.Id == id);
            if (submission == null)
            {
                return View("Error");
            }

            var submissionDto = new HomeworkSubmissionDto
            {
                HomeworkId = submission.HomeworkId,
                StudentUserId = submission.StudentUserId,
                Link = submission.Link,
                Description = submission.Description
            };

            return View(submissionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HomeworkSubmissionDto submissionDto)
        {
            if (!ModelState.IsValid)
            {
                return View(submissionDto);
            }
            Homework homework = await _homeworkService.GetHomework(x => x.Id == submissionDto.HomeworkId);
            Lesson lesson = _lessonService.GetLesson(x => x.Id == homework.LessonId);

            await _homeworkSubmissionService.UpdateHomeworkSubmission(submissionDto.Id, submissionDto);
            return RedirectToAction("Details", new { lessonId = lesson.Id });
        }

        public async Task<IActionResult> SubmissionDetails(int submissionId)
        {
            HomeworkSubmission submission = await _homeworkSubmissionService.GetHomeworkSubmission(x=>x.Id == submissionId);
            if(submission == null)
            {
                return View("Error");
            }
            Homework homework = await _homeworkService.GetHomework(x => x.Id == submission.HomeworkId);
            Lesson lesson = _lessonService.GetLesson(x => x.Id == homework.LessonId);
            ViewBag.LessonId = lesson.Id;
            HomeworkSubmissionDto dto = new HomeworkSubmissionDto()
            {
                Id = submissionId,
                StudentUserId = submission.StudentUserId,
                HomeworkId = submission.HomeworkId,
                FileUrl = submission.FileUrl,
                Link = submission.Link,
                Description = submission.Description,
            };
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _homeworkSubmissionService.DeleteHomeworkSubmission(id);
            return RedirectToAction("Details", "Lesson", new { id });
        }
    }

}
