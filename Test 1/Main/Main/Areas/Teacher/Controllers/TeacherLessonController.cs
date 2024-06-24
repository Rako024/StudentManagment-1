using Business.DTOs.Teacher.TeacherLessonsDto;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Teacher.Controllers
{
	[Area("Teacher")]
	public class TeacherLessonController : Controller
	{
		ILessonService _lessonService;
		IStudentUserService _studenUserService;
		ILessonTimeService _lessonTimeService;
		IGradeAndAttendaceService _gradeAndAttendaceService;
		IColloquiumService _colloquiumService;

        public TeacherLessonController(ILessonService lessonService, IStudentUserService studenUserService, ILessonTimeService lessonTimeService, IGradeAndAttendaceService gradeAndAttendaceService, IColloquiumService colloquiumService)
        {
            _lessonService = lessonService;
            _studenUserService = studenUserService;
            _lessonTimeService = lessonTimeService;
            _gradeAndAttendaceService = gradeAndAttendaceService;
            _colloquiumService = colloquiumService;
        }
        public async Task<IActionResult> Index(int id)
		{
			Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x => x.Id == id);
			List<StudentUser> students = await _studenUserService.GetAll(x=>x.GroupId == lesson.GroupId);
			List<LessonTime> lessonTimes = await _lessonTimeService.GetAllLessonTimes(x => x.LessonId == lesson.Id,x=>x.Date,false);
			List<GradeAndAttendace> gradeAndAttendances = await _gradeAndAttendaceService.GetAllGradeAndAttendaceAsync(x => x.LessonTime.LessonId == id);

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
            List<StudentUser> students = await _studenUserService.GetAll(x => x.GroupId == lesson.GroupId,null,false);
			List<Colloquium> colloquia = await _colloquiumService.GetAllColloquiumAsync(x=>x.LessonId == id,null,false);
			ColloquiumPageDto pageDto = new ColloquiumPageDto()
			{
				Lesson = lesson,
				Students = students,
				Colloquia = colloquia
			};
            return View(pageDto);
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
			await _gradeAndAttendaceService.AddOrUpdateGradeAndAttendaceAsync(dto);
			List<LessonTime> lessonTimes = await _lessonTimeService.GetAllLessonTimes(x => x.Id == dto.LessonTimeId, null, false, x => x.Lesson);
			LessonTime lessonTime = lessonTimes.FirstOrDefault();
			return RedirectToAction("Index",new { id = lessonTime.LessonId });
		}

    }
}
