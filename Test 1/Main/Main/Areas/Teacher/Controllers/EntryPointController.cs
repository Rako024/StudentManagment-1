using Business.DTOs.EntryPoints;
using Business.Helper;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Main.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class EntryPointController : Controller
    {
        IStudentUserService _studentUserService;
        ILessonService _lessonService;
        IGradeAndAttendaceService _gradeAndAttendanceService;
        IColloquiumService _colloquiumService;
        ITermPaperGradeService _termPaperGradeService;

        public EntryPointController(IStudentUserService studentUserService, ILessonService lessonService, IGradeAndAttendaceService gradeAndAttendanceService, IColloquiumService colloquiumService, ITermPaperGradeService termPaperGradeService)
        {
            _studentUserService = studentUserService;
            _lessonService = lessonService;
            _gradeAndAttendanceService = gradeAndAttendanceService;
            _colloquiumService = colloquiumService;
            _termPaperGradeService = termPaperGradeService;
        }

        public async Task<IActionResult> Index(int lessonId)
        {
            Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x => x.Id == lessonId);
            if(lesson == null)
            {
                return View("Error");
            }
            List<StudentUser> students = await _studentUserService.GetAll
                (
                x => x.IsDeleted == false &&
                x.GroupId == lesson.GroupId
                );
            List<EntryPointResultDto> results = new List<EntryPointResultDto> ();
            foreach (var student in students)
            {
                List<int> grades = new List<int> ();
                int qbCount = 0;
                int? col1 = null;
                int? col2 = null;
                int ?col3 = null;
                int? termGrade = null;
                List<GradeAndAttendace> gradeAndAttendaces = await _gradeAndAttendanceService.GetAllGradeAndAttendaceAsync
                    (x=>x.StudentUserId == student.Id &&
                    x.LessonTime.LessonId == lesson.Id &&
                    x.IsPresent !=null,
                    null,
                    false,
                    x=>x.LessonTime
                    );
                if(gradeAndAttendaces.IsNullOrEmpty())
                {
                    return View("Error");
                }
                foreach(var grade in gradeAndAttendaces)
                {
                    if(grade.Score != null)
                    {
                        grades.Add((int)grade.Score);
                    }
                    if (grade.IsPresent !=null &&!(bool) grade.IsPresent)
                    {
                        qbCount++;
                    }
                }
               Colloquium col = await _colloquiumService.GetColloquiumAsync
                    (
                    x => x.StudentUserId == student.Id &&
                    x.LessonId == lesson.Id
                    );

                if (col != null)
                {
                    col1 = col.FirstGrade;
                    col2 = col.SecondGrade;
                    col3 = col.ThirdGrade;
                }
                TermPaperGrade term = await _termPaperGradeService.GetTermPaperGrade
                    (
                        x => x.StudentUserId == student.Id &&
                        x.LessonId == lesson.Id
                    );
                if (term != null)
                {
                    termGrade = term.Grade;
                }

                EntryPointCalculatorDto etry = new EntryPointCalculatorDto()
                {
                    StudentUserId = student.Id,
                    LessonId = lesson.Id,
                    Lesson = lesson,
                    Grades = grades,
                    QbCount = qbCount,
                    ColloquiumFirst = col1,
                    ColloquiumSecound = col2,
                    ColloquiumThird = col3,
                    TermPaperGrade = termGrade
                };
                EntryPointResultDto entryResult =  EntryPointCalculator.Calculator(etry);
                results.Add(entryResult);
            }

            EnrtryPointPageDto enrtryPointPageDto = new EnrtryPointPageDto()
            {
                StudentUsers = students,
                Lesson = lesson,
                EntryPoints = results
            };
            return View(enrtryPointPageDto);
        }
    }
}
