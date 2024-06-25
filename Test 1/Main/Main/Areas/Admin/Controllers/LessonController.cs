using Business.DTOs.Admin.LessonDTOs;
using Business.Exceptions;
using Business.Exceptions.Lesson;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Cordinator")]
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

        public async Task<IActionResult> Index()
        {
            List<Lesson> lessons = await _lessonService.GetAllLessons(x => x.IsDeleted == false, x => x.Name, false, x => x.Group, x => x.TeacherUser);
            return View(lessons);
        }

        public async Task<IActionResult> Create(int? groupId)
        {
            if (groupId == null)
            {
                ViewBag.Groups = await _groupService.GetAllGroup(x=>x.IsDeleted == false);
                ViewBag.Teachers = await _teacherUserService.GetAllTeachers(x => x.IsDeleted == false);
                return View();
            }
            else
            {
                ViewBag.Groups = await _groupService.GetAllGroup(x => x.IsDeleted == false);
                ViewBag.Teachers = await _teacherUserService.GetAllTeachers(x => x.IsDeleted == false);
                Lesson lesson = new Lesson()
                {
                    GroupId = groupId,
                };
                return View(lesson);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Groups = await _groupService.GetAllGroup(x => x.IsDeleted == false);
                ViewBag.Teachers = await _teacherUserService.GetAllTeachers(x => x.IsDeleted == false);
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
                _lessonService.SoftDeleteLesson(id);
            }
            catch (Exception)
            {
                return View("Error");

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Groups = await _groupService.GetAllGroup(x => x.IsDeleted == false);
            ViewBag.Teachers = await _teacherUserService.GetAllTeachers(x => x.IsDeleted == false);
            var lesson = _lessonService.GetLesson(x => x.Id == id);
            if (lesson == null)
            {
                return View("Error");
            }
            return View(lesson);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Groups = await _groupService.GetAllGroup(x => x.IsDeleted == false);
                ViewBag.Teachers = await _teacherUserService.GetAllTeachers(x => x.IsDeleted == false);
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


        public async Task<IActionResult> Details(int id)
        {
            Lesson lesson =  _lessonService.GetLessonsWithGroupAndTeacherUser(x=>x.Id == id);
            if(lesson == null)
            {
                return View("Error");
            }
            List<LessonTime> lessonTimes = await _lessonTimeService.GetAllLessonTimes
                (
                x => x.LessonId == id && x.IsDeleted == false,
                x => x.Date,
                false,
                x => x.Lesson,
                x => x.Lesson.Group,
                x => x.Lesson.TeacherUser
            );
                
            LessonDetails lessonDetails = new LessonDetails()
            {
                LessonTimes = lessonTimes,
                Lesson = lesson
            };
            return View(lessonDetails);
        }
        
    }
}
