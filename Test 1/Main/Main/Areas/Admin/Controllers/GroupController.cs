using Business.Exceptions;
using Business.Services.Abstracts;
using Core.CoreEnums;
using Core.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Cordinator")]
    public class GroupController : Controller
    {
        IGroupService _groupService;
        IStudentUserService _studentUserService;
        ILessonService _lessonService;
        ILessonTimeService _lessonTimeService;
        ISemesterService _semesterService;


        public GroupController
            (
            IGroupService groupService,
            IStudentUserService studentUserService,
            ILessonService lessonService,
            ILessonTimeService lessonTimeService
,
            ISemesterService semesterService)
        {
            _groupService = groupService;
            _studentUserService = studentUserService;
            _lessonService = lessonService;
            _lessonTimeService = lessonTimeService;
            _semesterService = semesterService;
        }

        public async Task<IActionResult> Index(string? searchTerm)
        {
            List<Group> groups;
            if (string.IsNullOrEmpty(searchTerm))
            {
                groups = await _groupService.GetAllGroup(g => !g.IsDeleted, g => g.Name);
            }
            else
            {
                string upperSearchTerm = searchTerm.ToUpper();
                groups = await _groupService.GetAllGroup(
                    g => !g.IsDeleted && g.Name.ToUpper().Contains(upperSearchTerm),
                    g => g.Name);
            }
            ViewBag.SearchTerm = searchTerm;
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
                _groupService.SoftDeleteGroup(id);
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

        public async Task<IActionResult> Details(int id)
        {
            Group group = _groupService.GetGroup(x=>x.Id==id);
            if(group == null)
            {
                return View("Error");
            }
            ViewBag.Group = group;
            List<StudentUser> students = await _studentUserService.GetAll(x=>x.GroupId == id && x.IsDeleted == false,x=>x.Name);
            return View(students);  
        }
        public async Task<IActionResult> DetailsLessons(int id)
        {
            Group group = _groupService.GetGroup(x => x.Id == id && x.IsDeleted == false);
            if (group == null)
            {
                return View("Error");
            }
            ViewBag.Group = group;
            Semester activeSemester = _semesterService.GetSemester(x=>x.IsActive);
            
            List<Lesson> lessons = await _lessonService.GetAllLessons(x=>x.GroupId == id && x.IsDeleted == false && (int)x.Semester == activeSemester.SemesterNumber, x => x.Name,false,x=>x.Group, x => x.TeacherUser);
            return View(lessons);
        }

        public async Task<IActionResult> DetailsTeachers(int id)
        {
            Group group = _groupService.GetGroup(x => x.Id == id);
            if (group == null)
            {
                return View("Error");
            }
            ViewBag.Group = group;
            List<Lesson> lessons = await _lessonService.GetAllLessons(x => x.GroupId == id && x.IsDeleted == false, x => x.Name, false,x=>x.Group, x=>x.TeacherUser);
            List<TeacherUser> teachers = new List<TeacherUser>();
            foreach (var teacher in lessons)
            {
                teachers.Add(teacher.TeacherUser);
            }
            
            return View(teachers);
        }


        public async Task<IActionResult> DetailsLessonTimes(int id)
        {
            Group group = _groupService.GetGroup(x => x.Id == id);
            if (group == null)
            {
                return View("Error");
            }
            ViewBag.Group = group;
            List<LessonTime> lessonTimes = await _lessonTimeService.GetAllLessonTimes
                (
                x => x.Lesson.GroupId == id && x.IsDeleted == false && x.Lesson.IsDeleted == false,
                x => x.Date, false,
                x=>x.Lesson,
                x=>x.Lesson.Group,
                x=>x.Lesson.TeacherUser
                );
                
            return View(lessonTimes);
        }
    }
}
