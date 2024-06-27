using Business.DTOs.Student.Files;
using Business.DTOs.Teacher.TeacherLessonsDto;
using Business.Exceptions;
using Business.Exceptions.Lesson;
using Business.Exceptions.TermPaperGrade;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Main.Areas.Teacher.Controllers
{
	[Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class TeacherLessonController : Controller
	{
		ILessonService _lessonService;
		IStudentUserService _studenUserService;
		ILessonTimeService _lessonTimeService;
		IGradeAndAttendaceService _gradeAndAttendaceService;
		IColloquiumService _colloquiumService;
		ITermPaperGradeService _termPaperGradeService;
		ITermPaperService _termPaperService;

		public TeacherLessonController(ILessonService lessonService, IStudentUserService studenUserService, ILessonTimeService lessonTimeService, IGradeAndAttendaceService gradeAndAttendaceService, IColloquiumService colloquiumService, ITermPaperGradeService termPaperGradeService, ITermPaperService termPaperService)
		{
			_lessonService = lessonService;
			_studenUserService = studenUserService;
			_lessonTimeService = lessonTimeService;
			_gradeAndAttendaceService = gradeAndAttendaceService;
			_colloquiumService = colloquiumService;
			_termPaperGradeService = termPaperGradeService;
			_termPaperService = termPaperService;
		}
		public async Task<IActionResult> Index(int id)
		{
			Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x => x.Id == id);
			List<StudentUser> students = await _studenUserService.GetAll(x=>x.GroupId == lesson.GroupId && x.IsDeleted == false);
			List<LessonTime> lessonTimes = await _lessonTimeService.GetAllLessonTimes(x => x.LessonId == lesson.Id && x.IsDeleted == false,x=>x.Date,false);
			List<GradeAndAttendace> gradeAndAttendances = await _gradeAndAttendaceService.GetAllGradeAndAttendaceAsync(x => x.LessonTime.LessonId == id && x.IsDeleted == false);
            if (lesson == null)
            {
                return View("Error");
            }
            TeacherLessonDto lessonDto = new TeacherLessonDto()
			{
			Lesson = lesson,
			Students = students,
			LessonTimes = lessonTimes,
			GradeAndAttendances = gradeAndAttendances
			};
			return View(lessonDto);
		}

		public async Task<IActionResult> Colloquium(int id)
		{
            Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x => x.Id == id);
            List<StudentUser> students = await _studenUserService.GetAll(x => x.GroupId == lesson.GroupId && x.IsDeleted == false,null,false);
			List<Colloquium> colloquia = await _colloquiumService.GetAllColloquiumAsync(x=>x.LessonId == id,null,false);
            if (lesson == null)
            {
                return View("Error");
            }
            ColloquiumPageDto pageDto = new ColloquiumPageDto()
			{
				Lesson = lesson,
				Students = students,
				Colloquia = colloquia
			};
            return View(pageDto);
		}
		public async Task<IActionResult> TermPaper(int id)
		{
            Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x => x.Id == id);
            List<StudentUser> students = await _studenUserService.GetAll(x => x.GroupId == lesson.GroupId && x.IsDeleted == false, null, false);
			List<TermPaperGrade> termPapers = await _termPaperGradeService.GetAllTermPapers(x=>x.LessonId == lesson.Id,null,false,x=>x.Lesson,x=>x.StudentUser);
			if(lesson == null)
			{
				return View("Error");
			}
			TermPaperPageDto pageDto = new TermPaperPageDto()
			{
				Lesson = lesson,
				Students = students,
				TermPaperGrades = termPapers
			};
			return View(pageDto);
        }

		public async Task<IActionResult> TermPaperDetails(string studentUserId, int lessonId)
		{
			StudentUser user = _studenUserService.Get(x => x.Id == studentUserId);
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
				x => x.Lesson,
				x => x.Lesson.TeacherUser,
				x => x.StudentUser
				);
			TermPaperDetailsDto termPaperDetails = new TermPaperDetailsDto()
			{
				TermPapers = papers,
				UserId = user.Id,
				LessonId = lesson.Id,
				StudentUser = user,
				Lesson = lesson
			};
			return View(termPaperDetails);
		}


		[HttpPost]
		public async Task<IActionResult> AddOrUpdateTermPape(TermPaperGradeDto dto)
		{
			if (!ModelState.IsValid)
			{
                return RedirectToAction(nameof(TermPaper), new { id = dto.LessonId });

            }
            if (dto == null)
			{
				return View("Error");
			}
			if(dto.StudentUserId == null)
			{
				return View("Error");
			}
			if(dto.LessonId == 0)
			{
				return View("Error");
			}
			try
			{

			await _termPaperGradeService.CreateOrUpdateTermPaperGrade(dto);
			}catch(LessonNotFoundException)
			{
				return View("Error");
			}
			catch (StudentUserNotFoundException)
			{
				return View("Error");
			}
			catch (TermPaperGradeNotFoundException)
			{
				return View("Error");
			}catch(Exception ) 
			{
				return View("Error");
			}
			return RedirectToAction(nameof(TermPaper), new {id = dto.LessonId});
		}

		[HttpPost]
		public async Task<IActionResult> AddOrUpdateColloquium(ColloquiumDto colloquiumDto)
		{
			await _colloquiumService.AddOrUpdateColloquiumAsync (colloquiumDto);
			return RedirectToAction("Colloquium", new {id = colloquiumDto.LessonId});
		}

		[HttpPost]
		public async Task<IActionResult> AddOrUpdateGradeAndAttendace(GradeAndAttendaceDto dto)
		{
			var user = _studenUserService.Get(x => x.Id == dto.StudentId);
            if (user == null)
            {
                return View("Error");
            }
            List<LessonTime> lessonTimes = await _lessonTimeService.GetAllLessonTimes(x => x.Id == dto.LessonTimeId, null, false, x => x.Lesson);

			if (lessonTimes.IsNullOrEmpty())
			{
                return View("Error");
            }
            LessonTime lessonTime = lessonTimes.FirstOrDefault();
            if (lessonTime == null)
            {
                return View("Error");
            }
            await _gradeAndAttendaceService.AddOrUpdateGradeAndAttendaceAsync(dto);
			
            return RedirectToAction("Index",new { id = lessonTime.LessonId });
		}

    }
}
