using Business.DTOs.EntryPoints;
using Business.DTOs.Student.StudentInformations;
using Business.Helper;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Main.Areas.Student.Controllers
{
    [Area("Student"),Authorize(Roles ="Student")]
    public class StudentInformationController : Controller
    {
        ILessonService _lessonService;
        IStudentUserService _studentUserService;
        ITermPaperGradeService _termPaperGradeService;
        IExamScoreService _examScoreService;
        IGradeAndAttendaceService _gradeAndAttendaceService;
        IColloquiumService _colloquiumService;
        ISemesterService _semesterService;
        public StudentInformationController(ILessonService lessonService, IStudentUserService studentUserService, ITermPaperGradeService termPaperGradeService, IExamScoreService examScoreService, IGradeAndAttendaceService gradeAndAttendaceService, IColloquiumService colloquiumService, ISemesterService semesterService)
        {
            _lessonService = lessonService;
            _studentUserService = studentUserService;
            _termPaperGradeService = termPaperGradeService;
            _examScoreService = examScoreService;
            _gradeAndAttendaceService = gradeAndAttendaceService;
            _colloquiumService = colloquiumService;
            _semesterService = semesterService;
        }

        public async Task<IActionResult> Index(string id)
        {
            StudentUser user = _studentUserService.Get(x=>x.Id == id);
            if(user == null)
            {
                return View("Error");
            }
            Semester activeSemester = _semesterService.GetSemester(x=>x.IsActive);
            List<Lesson> lessons = await _lessonService.GetAllLessons
                (
                x => x.IsDeleted == false &&
                x.GroupId == user.GroupId &&
                x.IsPast == false &&
                (int)x.Semester == activeSemester.SemesterNumber,
                null,
                false,
                x => x.Group,
                x => x.TeacherUser
                );
            StudentInfoLessonsDto dto = new StudentInfoLessonsDto() {Lessons = lessons, StudentUserId = user.Id };
            return View(dto);
        }

        public async Task<IActionResult> Details(string userId, int lessonId)
        {
            Lesson lesson = _lessonService.GetLesson(x => x.Id == lessonId);
            if(lesson == null)
            {
                return View("Error");
            }
            StudentUser student = _studentUserService.Get(x=>x.Id == userId);
            if(student == null)
            {
                return View("Error");
            }
            EntryPointResultDto results;
            
            
            
                List<int> grades = new List<int>();
                int qbCount = 0;
                int? col1 = null;
                int? col2 = null;
                int? col3 = null;
                int? termGrade = null;
                List<GradeAndAttendace> gradeAndAttendaces = await _gradeAndAttendaceService.GetAllGradeAndAttendaceAsync
                    (x => x.StudentUserId == student.Id &&
                    x.LessonTime.LessonId == lesson.Id &&
                    x.IsPresent != null,
                    null,
                    false,
                    x => x.LessonTime
                    );
                if (!gradeAndAttendaces.IsNullOrEmpty())
                {



                    foreach (var grade in gradeAndAttendaces)
                    {
                        if (grade.Score != null)
                        {
                            grades.Add((int)grade.Score);
                        }
                        if (grade.IsPresent != null && !(bool)grade.IsPresent)
                        {
                            qbCount++;
                        }
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
                EntryPointResultDto entryResult = EntryPointCalculator.Calculator(etry);
                results = entryResult;

            ExamScore exam = await _examScoreService.GetExamScore(x => x.StudentUserId == student.Id && x.LessonId == lesson.Id);

            if (exam == null)
            {
                exam = new ExamScore
                {
                    Score = 0,
                    StudentUserId = student.Id,
                    LessonId = lesson.Id
                };
            }
            
            StudentInfoLessonPointInfoDto dto = new StudentInfoLessonPointInfoDto()
            {
                StudentUser = student,
                Lesson = lesson,
                Grades = grades,
                QbCount = qbCount,
                ColloquiumFirst = col1,
                ColloquiumSecound = col2,
                ColloquiumThird = col3,
                TermPaperGrade  = termGrade,
                ExsamScore = exam,
                EntryPoint = results
            };

            return View(dto);
        }
    }
}
