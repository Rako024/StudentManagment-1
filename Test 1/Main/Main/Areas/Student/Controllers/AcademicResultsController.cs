using Business.DTOs.EntryPoints;
using Business.DTOs.Student.AcademicResult;
using Business.Helper;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Main.Areas.Student.Controllers
{
    [Area("Student"),Authorize(Roles ="Student")]
    public class AcademicResultsController : Controller
    {
        ILessonService _lessonService;
        IStudentUserService _studentUserService;
        ITermPaperGradeService _termPaperGradeService;
        IExamScoreService _examScoreService;
        IGradeAndAttendaceService _gradeAndAttendaceService;
        IColloquiumService _colloquiumService;
        ISemesterService _semesterService;

        public AcademicResultsController(ILessonService lessonService, IStudentUserService studentUserService, ITermPaperGradeService termPaperGradeService, IExamScoreService examScoreService, IGradeAndAttendaceService gradeAndAttendaceService, IColloquiumService colloquiumService, ISemesterService semesterService)
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
            StudentUser student = _studentUserService.Get(x => x.Id == id);
            if (student == null)
            {
                return View("Error");
            }
            List<Lesson> lessons = await _lessonService.GetAllLessons(x => x.IsDeleted == false && x.GroupId == student.GroupId);
            AcademicResultsEntryPointAndExamScoreDto result = new AcademicResultsEntryPointAndExamScoreDto();
            result.StudentUser = student;
            foreach (Lesson lesson in lessons)
            {
                StudentEntryPointAndExamScoreDto studentEntryPointAndExamScoreDto = new StudentEntryPointAndExamScoreDto()
                {
                    StudentUserId = student.Id,
                    Lesson = lesson,
                    EntryPoint = null,
                    ExamScore = null
                };

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

                studentEntryPointAndExamScoreDto.EntryPoint = entryResult;
                studentEntryPointAndExamScoreDto.ExamScore = exam;
                if (lesson.Semester == Core.CoreEnums.Semesters.Autumn)
                {
                    if (lesson.Year == Core.CoreEnums.Years.First)
                    {
                        result.Year1Semester1.Add(studentEntryPointAndExamScoreDto);
                    }
                    else if (lesson.Year == Core.CoreEnums.Years.Second)
                    {
                        result.Year2Semester1.Add(studentEntryPointAndExamScoreDto);
                    }
                    else if (lesson.Year == Core.CoreEnums.Years.Third)
                    {
                        result.Year3Semester1.Add(studentEntryPointAndExamScoreDto);
                    }
                    else if (lesson.Year == Core.CoreEnums.Years.Fourth)
                    {
                        result.Year4Semester1.Add(studentEntryPointAndExamScoreDto);
                    }
                }
                else if (lesson.Semester == Core.CoreEnums.Semesters.Spring)
                {
                    if (lesson.Year == Core.CoreEnums.Years.First)
                    {
                        result.Year1Semester2.Add(studentEntryPointAndExamScoreDto);
                    }
                    else if (lesson.Year == Core.CoreEnums.Years.Second)
                    {
                        result.Year2Semester2.Add(studentEntryPointAndExamScoreDto);
                    }
                    else if (lesson.Year == Core.CoreEnums.Years.Third)
                    {
                        result.Year3Semester2.Add(studentEntryPointAndExamScoreDto);
                    }
                    else if (lesson.Year == Core.CoreEnums.Years.Fourth)
                    {
                        result.Year4Semester2.Add(studentEntryPointAndExamScoreDto);
                    }
                }
                else if (lesson.Semester == Core.CoreEnums.Semesters.Summer)
                {
                    if (lesson.Year == Core.CoreEnums.Years.First)
                    {
                        result.Year1Semester3.Add(studentEntryPointAndExamScoreDto);
                    }
                    else if (lesson.Year == Core.CoreEnums.Years.Second)
                    {
                        result.Year2Semester3.Add(studentEntryPointAndExamScoreDto);
                    }
                    else if (lesson.Year == Core.CoreEnums.Years.Third)
                    {
                        result.Year3Semester3.Add(studentEntryPointAndExamScoreDto);
                    }
                    else if (lesson.Year == Core.CoreEnums.Years.Fourth)
                    {
                        result.Year4Semester3.Add(studentEntryPointAndExamScoreDto);
                    }
                }
            }
            result.SemesterGPA11 = EntryPointCalculator.SemesterGPA(result.Year1Semester1);
            result.SemesterGPA12 = EntryPointCalculator.SemesterGPA(result.Year1Semester2);
            result.SemesterGPA13 = EntryPointCalculator.SemesterGPA(result.Year1Semester3);

            result.SemesterGPA21 = EntryPointCalculator.SemesterGPA(result.Year2Semester1);
            result.SemesterGPA22 = EntryPointCalculator.SemesterGPA(result.Year2Semester2);
            result.SemesterGPA23 = EntryPointCalculator.SemesterGPA(result.Year2Semester3);

            result.SemesterGPA31 = EntryPointCalculator.SemesterGPA(result.Year3Semester1);
            result.SemesterGPA32 = EntryPointCalculator.SemesterGPA(result.Year3Semester2);
            result.SemesterGPA33 = EntryPointCalculator.SemesterGPA(result.Year3Semester3);

            result.SemesterGPA41 = EntryPointCalculator.SemesterGPA(result.Year4Semester1);
            result.SemesterGPA42 = EntryPointCalculator.SemesterGPA(result.Year4Semester2);
            result.SemesterGPA43 = EntryPointCalculator.SemesterGPA(result.Year4Semester3);
            return View(result);
        }

    }
}
