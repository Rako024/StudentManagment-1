using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : Controller
    {
        IGroupService _groupService;
        IStudentUserService _studentUserService;
        ILessonService _lessonService;
        ILessonTimeService _lessonTimeService;


        public GroupController(IGroupService groupService, IStudentUserService studentUserService, ILessonService lessonService, ILessonTimeService lessonTimeService)
        {
            _groupService = groupService;
            _studentUserService = studentUserService;
            _lessonService = lessonService;
            _lessonTimeService = lessonTimeService;
        }

        public IActionResult Index()
        {
            List<Group> groups = _groupService.GetAllGroup();
            return View(groups);
        }

        public IActionResult Delete(int id) 
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _groupService.DeleteGroup(id);
            }catch(GroupNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View("Error");
            }catch (Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Group group) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _groupService.CreateGroup(group);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Group group = _groupService.GetGroup(x=>x.Id == id);
            if(group == null)
            {
                return View("Error");
            }
            return View(group);
        }

        [HttpPost]
        public IActionResult Update(Group group)
        {
            if (!ModelState.IsValid)
            {
                return View() ; 
            }
            try
            {
                _groupService.UpdateGroup(group.Id, group);
            }catch(GroupNotFoundException ex)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Group group = _groupService.GetGroup(x=>x.Id==id);
            if(group == null)
            {
                return View("Error");
            }
            ViewBag.Group = group;
            List<StudentUser> students = _studentUserService.GetAll(x=>x.GroupId == id);
            return View(students);  
        }
        public IActionResult DetailsLessons(int id)
        {
            Group group = _groupService.GetGroup(x => x.Id == id);
            if (group == null)
            {
                return View("Error");
            }
            ViewBag.Group = group;
            List<Lesson> lessons = _lessonService.GetAllLessinsWithGroupAndTeacherUser(x=>x.GroupId == id);
            return View(lessons);
        }

        public IActionResult DetailsTeachers(int id)
        {
            Group group = _groupService.GetGroup(x => x.Id == id);
            if (group == null)
            {
                return View("Error");
            }
            ViewBag.Group = group;
            List<Lesson> lessons = _lessonService.GetAllLessinsWithGroupAndTeacherUser(x => x.GroupId == id);
            List<TeacherUser> teachers = new List<TeacherUser>();
            foreach (var teacher in lessons)
            {
                teachers.Add(teacher.TeacherUser);
            }
            
            return View(teachers);
        }


        public IActionResult DetailsLessonTimes(int id)
        {
            Group group = _groupService.GetGroup(x => x.Id == id);
            if (group == null)
            {
                return View("Error");
            }
            ViewBag.Group = group;
            List<LessonTime> lessonTimes = _lessonTimeService.GetLessonsWithLessonWithTeacherAndGroup(x => x.Lesson.GroupId == id)
                .OrderBy(x=>x.Date).ToList();
            return View(lessonTimes);
        }
    }
}
