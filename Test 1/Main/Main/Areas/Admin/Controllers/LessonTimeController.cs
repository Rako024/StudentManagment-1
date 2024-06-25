using Business.Exceptions.Lesson;
using Business.Exceptions.LessonTime;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Cordinator")]
    public class LessonTimeController : Controller
    {
        ILessonTimeService _lessonTimeService;
        ILessonService _lessonService;

        public LessonTimeController(ILessonTimeService lessonTimeService, ILessonService lessonService)
        {
            _lessonTimeService = lessonTimeService;
            _lessonService = lessonService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
    {
        var pagedResult = await _lessonTimeService.GetPagedLessonTimes(
            pageNumber, 
            pageSize, 
            x => x.IsDeleted == false,
            x => x.Date,
            false,
            x => x.Lesson,
            x => x.Lesson.Group,
            x => x.Lesson.TeacherUser
        );

        return View(pagedResult);
    }

        public async Task<IActionResult> Create(int? lessonId)
        {
            if (lessonId == null)
            {
                ViewBag.Lessons = await _lessonService.GetAllLessons(x => x.IsDeleted == false, x=>true,false,x => x.Group, x=>x.TeacherUser);
                
            }
            else
            {
                Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x=>x.Id == lessonId);
                if(lesson == null)
                {
                    return View("Error");
                }
                ViewBag.Lesson = lesson;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(LessonTime lessonTime) 
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Lessons = _lessonService.GetAllLessons(x => x.IsDeleted == false, x => true, false, x => x.Group, x => x.TeacherUser);
                return View();
            }
            try
            {
                _lessonTimeService.CreateLessonTime(lessonTime);
            }
            catch (LessonNotFoundException ex)
            {
                ViewBag.Lessons = _lessonService.GetAllLessons(x => x.IsDeleted == false, x => true, false, x => x.Group, x => x.TeacherUser);
                ModelState.AddModelError("LessonId", ex.Message);
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction("Details", "Lesson", new { id = lessonTime.LessonId });
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


        public async Task<IActionResult> Update(int id) 
        {
            ViewBag.Lessons = await _lessonService.GetAllLessons(x => x.IsDeleted == false, x => true, false, x => x.Group, x => x.TeacherUser);
            LessonTime lessonTime = await _lessonTimeService.GetLessonTimeAsync(x => x.Id == id);
            if (lessonTime == null)
            {
                return View("Error");
            }
            return View(lessonTime);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LessonTime lessonTime)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Lessons = await _lessonService.GetAllLessons(x => x.IsDeleted == false, x => true, false, x => x.Group, x => x.TeacherUser);
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
                ViewBag.Lessons = await _lessonService.GetAllLessons(x => x.IsDeleted == false, x => true, false, x => x.Group, x => x.TeacherUser);
                ModelState.AddModelError("LessonId", ex.Message);
                return View();
            }
            return RedirectToAction("Details", "Lesson", new { id = lessonTime.LessonId });
        }
    }
}
