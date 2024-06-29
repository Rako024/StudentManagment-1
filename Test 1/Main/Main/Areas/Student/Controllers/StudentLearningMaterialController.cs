using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Student.Controllers
{
    [Area("Student"),Authorize(Roles ="Student")]
    public class StudentLearningMaterialController : Controller
    {
        
        private readonly ILessonService _lessonService;
        private readonly IGroupService _groupService;
        private readonly ILearningMaterialService _learningMaterialService;
        private readonly ISemesterService _semesterService;
        private readonly IStudentUserService _studentUserService;


        public StudentLearningMaterialController(ILessonService lessonService, IGroupService groupService, ISemesterService semesterService, IStudentUserService studentUserService, ILearningMaterialService learningMaterialService)
        {

            _lessonService = lessonService;

            _groupService = groupService;
            _semesterService = semesterService;
            _studentUserService = studentUserService;
            _learningMaterialService = learningMaterialService;
        }
        public async Task<IActionResult> Index(string id)
        {
            StudentUser user = _studentUserService.Get(x => x.Id == id);
            if (user == null)
            {
                return View("Error");
            }
            Group group = _groupService.GetGroup(x => x.Id == user.GroupId);
            if (group == null)
            {
                return View("Error");
            }
            Semester activeSemester = _semesterService.GetSemester(x => x.IsActive);
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
            Lesson lesson = _lessonService.GetLesson(x => x.Id == lessonId);
            if (lesson == null)
            {
                return View("Error");
            }
            List<LearningMaterial> learningMaterials = await _learningMaterialService.GetAllLearningMaterials
                (
                x=>x.LessonId == lessonId && !x.IsDeleted
                );
            return View(learningMaterials);
        }
    }
}
