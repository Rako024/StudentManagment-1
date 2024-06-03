using Business.DTOs.Admin.LessonDTOs;
using Business.Exceptions;
using Business.Exceptions.Lesson;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LessonController : Controller
    {
        ILessonService _lessonService;
        IGroupService _groupService;
        ITeacherUserService _teacherUserService;
        ILessonTimeService _lessonTimeService;

        public LessonController(ILessonService lessonService, IGroupService groupService, ITeacherUserService teacherUserService, ILessonTimeService lessonTimeService)
        {
            _lessonService = lessonService;
            _groupService = groupService;
            _teacherUserService = teacherUserService;
            _lessonTimeService = lessonTimeService;
        }

        public IActionResult Index()
        {
            List<Lesson> lessons = _lessonService.GetAllLessinsWithGroupAndTeacherUser();
            return View(lessons);
        }

        public IActionResult Create(int? groupId)
        {
            if (groupId == null)
            {
                ViewBag.Groups = _groupService.GetAllGroup();
                ViewBag.Teachers = _teacherUserService.GetAllTeachers();
                return View();
            }
            else
            {
                ViewBag.Groups = _groupService.GetAllGroup();
                ViewBag.Teachers = _teacherUserService.GetAllTeachers();
                Lesson lesson = new Lesson()
                {
                    GroupId = groupId,
                };
                return View(lesson);
            }
        }

        [HttpPost]
        public IActionResult Create(Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Groups = _groupService.GetAllGroup();
                ViewBag.Teachers = _teacherUserService.GetAllTeachers();
                return View();
            }
            try
            {
                _lessonService.CreateLesson(lesson);
            }catch (TeacherUserNotFoundException)
            {
                return View("Error");
            }
            catch (GroupNotFoundException)
            {
                return View("Error");

            }catch(Exception)
            {
                return View("Error");
            }
            return RedirectToAction("DetailsLessons", "Group", new { id = lesson.GroupId });
        }

        public IActionResult Delete(int id)
        {
            var lesson = _lessonService.GetLesson(x=>x.Id == id);
            if(lesson == null)
            {
                return View("Error");
            }
            try
            {
                _lessonService.DeleteLesson(id);
            }
            catch (Exception)
            {
                return View("Error");

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            ViewBag.Groups = _groupService.GetAllGroup();
            ViewBag.Teachers = _teacherUserService.GetAllTeachers();
            var lesson = _lessonService.GetLesson(x => x.Id == id);
            if (lesson == null)
            {
                return View("Error");
            }
            return View(lesson);
        }

        [HttpPost]
        public IActionResult Update(Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Groups = _groupService.GetAllGroup();
                ViewBag.Teachers = _teacherUserService.GetAllTeachers();
                return View();
            }
            try
            {
                _lessonService.UpdateLesson(lesson.Id, lesson);
            }
            catch (LessonNotFoundException)
            {
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction("DetailsLessons", "Group", new { id = lesson.GroupId });
        }


        public IActionResult Details(int id)
        {
            Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x=>x.Id == id);
            if(lesson == null)
            {
                return View("Error");
            }
            List<LessonTime> lessonTimes = _lessonTimeService.GetLessonsWithLessonWithTeacherAndGroup(x=>x.LessonId == id)
                .OrderBy(x=>x.Date).ToList();
            LessonDetails lessonDetails = new LessonDetails()
            {
                LessonTimes = lessonTimes,
                Lesson = lesson
            };
            return View(lessonDetails);
        }
        
    }
}
