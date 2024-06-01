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

        public LessonController(ILessonService lessonService, IGroupService groupService, ITeacherUserService teacherUserService)
        {
            _lessonService = lessonService;
            _groupService = groupService;
            _teacherUserService = teacherUserService;
        }

        public IActionResult Index()
        {
            List<Lesson> lessons = _lessonService.GetAllLessinsWithGroupAndTeacherUser();
            return View(lessons);
        }

        public IActionResult Create()
        {
            ViewBag.Groups = _groupService.GetAllGroup();
            ViewBag.Teachers = _teacherUserService.GetAllTeachers();
            return View();
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
            return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }
    }
}
