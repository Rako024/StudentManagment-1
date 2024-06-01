using Business.Exceptions.Lesson;
using Business.Exceptions.LessonTime;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LessonTimeController : Controller
    {
        ILessonTimeService _lessonTimeService;
        ILessonService _lessonService;

        public LessonTimeController(ILessonTimeService lessonTimeService, ILessonService lessonService)
        {
            _lessonTimeService = lessonTimeService;
            _lessonService = lessonService;
        }

        public IActionResult Index()
        {
            List<LessonTime> lessonTimes = _lessonTimeService.GetLessonsWithLessonWithTeacherAndGroup();
            return View(lessonTimes);
        }

        public IActionResult Create()
        {
            ViewBag.Lessons = _lessonService.GetAllLessons();
            return View();
        }

        [HttpPost]
        public IActionResult Create(LessonTime lessonTime) 
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Lessons = _lessonService.GetAllLessons();
                return View();
            }
            try
            {
                _lessonTimeService.CreateLessonTime(lessonTime);
            }
            catch (LessonNotFoundException ex)
            {
                ViewBag.Lessons = _lessonService.GetAllLessons();
                ModelState.AddModelError("LessonId", ex.Message);
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index)); 
        }


        public IActionResult Delete(int id)
        {
            LessonTime lessonTime = _lessonTimeService.GetLessonTime(x=>x.Id == id);
            if(lessonTime == null)
            {
                return View("Error");
            }
            try
            {
                _lessonTimeService.DeleteLessonTime(id);
            }catch (LessonTimeNotFoundException ex) 
            {
                return View("Error");

            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id) 
        {
            ViewBag.Lessons = _lessonService.GetAllLessons();
            LessonTime lessonTime = _lessonTimeService.GetLessonTime(x => x.Id == id);
            if (lessonTime == null)
            {
                return View("Error");
            }
            return View(lessonTime);
        }

        [HttpPost]
        public IActionResult Update(LessonTime lessonTime)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Lessons = _lessonService.GetAllLessons();
                return View();
            }

            try
            {
                _lessonTimeService.UpdateLessonTime(lessonTime.Id, lessonTime);
            }catch(LessonTimeNotFoundException ex)
            {
                return View("Error");
            }catch (LessonNotFoundException ex)
            {
                ViewBag.Lessons = _lessonService.GetAllLessons();
                ModelState.AddModelError("LessonId", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
