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

		public TeacherLessonController(ILessonService lessonService, IStudentUserService studenUserService, ILessonTimeService lessonTimeService)
		{
			_lessonService = lessonService;
			_studenUserService = studenUserService;
			_lessonTimeService = lessonTimeService;
		}
		public async Task<IActionResult> Index(int id)
		{
			Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x => x.Id == id);
			List<StudentUser> students = await _studenUserService.GetAll(x=>x.GroupId == lesson.GroupId);
			List<LessonTime> lessonTimes = await _lessonTimeService.GetAllLessonTimes(x => x.LessonId == lesson.Id,x=>x.Date,false);
			TeacherLessonDto lessonDto = new TeacherLessonDto() 
			{
				Lesson = lesson,
				Students = students,
				LessonTimes = lessonTimes
			
			};
			return View(lessonDto);
		}
	}
}
