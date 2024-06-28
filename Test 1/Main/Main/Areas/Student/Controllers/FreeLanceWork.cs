using Business.DTOs.Student.Files;
using Business.Exceptions;
using Business.Exceptions.File;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class FreeLanceWork : Controller
    {
        IStudentUserService _studentUserService;
        ILessonService _lessonService;
        ITermPaperService _termPaperService;
        ISemesterService _semesterService;

        public FreeLanceWork(IStudentUserService studentUserService, ILessonService lessonService, ITermPaperService termPaperService, ISemesterService semesterService)
        {
            _studentUserService = studentUserService;
            _lessonService = lessonService;
            _termPaperService = termPaperService;
            _semesterService = semesterService;
        }

        public async Task<IActionResult> Index(string id)
        {
            StudentUser user = _studentUserService.Get(x=>x.Id == id);
            if(user == null)
            {
                return View("Error");
            }
            var activeSemester = _semesterService.GetSemester(x => x.IsActive);
            List<Lesson> lessons = await _lessonService.GetAllLessons
                (
                x => x.GroupId == user.GroupId &&
                (int)x.Semester == activeSemester.SemesterNumber &&
                x.IsDeleted == false &&
                x.IsPast == false,
                null,
                false,
                x => x.Group,
                x => x.TeacherUser
                );
            TermPaperIndexDto term = new TermPaperIndexDto() { Lessons = lessons, UserId = user.Id };
            return View(term);
        }
        public async Task<IActionResult> Details(string userId, int lessonId)
        {
            StudentUser user = _studentUserService.Get(x => x.Id == userId);
            if (user == null)
            {
                return View("Error");
            }
            Lesson lesson = _lessonService.GetLesson(x => x.Id == lessonId);
            if (lesson == null)
            {
                return View("Error");
            }
            List<TermPaper> papers = await _termPaperService.GetAllTermPapers
                (x => x.StudentUserId == user.Id && x.LessonId == lesson.Id,
                null,
                false,
                x=>x.Lesson,
                x=>x.Lesson.TeacherUser,
                x=>x.StudentUser
                );
            TermPaperDetailsDto termPaperDetails = new TermPaperDetailsDto()
            {
                TermPapers = papers,
                UserId = user.Id,
                LessonId = lesson.Id    
            };
            return View(termPaperDetails);
        }

        public async Task<IActionResult> Create(string userId, int lessonId)
        {
            StudentUser studentUser = _studentUserService.Get(x=>x.Id == userId);
            if (studentUser == null)
            {
                return View("Error");
            }
            ViewBag.StudentUserId = studentUser.Id;
            
            Lesson lesson = _lessonService.GetLesson(x=>x.Id== lessonId);
            if(lesson == null)
            {
                return View("Error");
            }
            ViewBag.LessonId = lesson.Id;
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TermFileDto termFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.StudentUserId = termFile.StudentUserId;
                ViewBag.LessonId = termFile.LessonId;
                return View(termFile);
            }
            try
            {
                await _termPaperService.CreateTermPaper(termFile);
            }
            catch (GlobalException)
            {
                
                return View("Error");   
            }
            catch(ArgumentNotFoundException ex)
            {
                ViewBag.StudentUserId = termFile.StudentUserId;
                ViewBag.LessonId = termFile.LessonId;
                ModelState.AddModelError(ex.PropertyName,ex.Message);
                return View(termFile);
            }catch (ContentTypeException ex)
            {
                ViewBag.StudentUserId = termFile.StudentUserId;
                ViewBag.LessonId = termFile.LessonId;
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View(termFile);
            }catch(Exception) 
            {
                return View("Error");
            }
            return RedirectToAction("Details", new {userId = termFile.StudentUserId, lessonId = termFile.LessonId});
        }

        public async Task<IActionResult> Update(int id)
        {
            TermPaper term = await _termPaperService.GetTermPaper(x => x.Id == id);
            Lesson lesson = _lessonService.GetLesson(x=>x.Id == term.LessonId);
            if (lesson == null)
            {
                return View("Error");
            }
            if(term == null)
            {
                return View("Error");
            }
            TermFileUpdateDto termFileDto = new TermFileUpdateDto() 
            {
                Id = term.Id,
                Name = term.Name,
                
                Description = term.Description,
            };
            ViewBag.LessonId = lesson.Id;
            ViewBag.StudentUserId= term.StudentUserId;
            return View(termFileDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TermFileUpdateDto termFileDto)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.LessonId = termFileDto.LessonId;
                ViewBag.StudentUserId = termFileDto.StudentUserId;
                return View();
            }
            try
            {
                await _termPaperService.UpdateTermPaper(termFileDto.Id, termFileDto);
            }catch (GlobalException )
            {
                return View("Error");
            }catch(ContentTypeException ex)
            {
                ViewBag.LessonId = termFileDto.LessonId;
                ViewBag.StudentUserId = termFileDto.StudentUserId;
                ModelState.AddModelError(ex.PropertyName,ex.Message);
                return View();
            }catch(Exception) {
                return View("Error");
            }
            return RedirectToAction("Details", new { userId = termFileDto.StudentUserId, lessonId = termFileDto.LessonId });
        }
    }
}
