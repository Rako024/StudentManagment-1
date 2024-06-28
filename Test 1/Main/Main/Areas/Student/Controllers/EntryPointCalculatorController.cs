using Business.DTOs.EntryPoints;
using Business.Helper;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Student.Controllers
{
    [Area("Student"), Authorize(Roles = "Student")]
    public class EntryPointCalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new EntryPointCalculatorDto());
        }

        [HttpPost]
        public IActionResult CalculateEntryPoint(EntryPointCalculatorDto dto)
        {
            dto.Lesson = new Lesson
            {
                LessonCount = 10 
            };
            EntryPointResultDto result = Calculator(dto);
            ViewBag.TotalPoint = result.TotalPoint;
            return View("Index", dto);
        }
        public static EntryPointResultDto Calculator(EntryPointCalculatorDto dto)
        {
            int average = 0;
            if (dto.Grades != null && dto.Grades.Count > 0)
            {
                int sum = 0;
                foreach (var grade in dto.Grades)
                {
                    sum += grade;
                }
                average = sum / dto.Grades.Count;
            }

            int col1 = dto.ColloquiumFirst ?? 0;
            int col2 = dto.ColloquiumSecound ?? 0;
            int col3 = dto.ColloquiumThird ?? 0;

            int colAvarage = ((col1 + col2 + col3) / 3) * 2;
            int termGrade = dto.TermPaperGrade ?? 0;

            int qbLimitCount = dto.Lesson.LessonCount / 4;
            int qbCount = dto.QbCount;
            bool isFailed = qbLimitCount < qbCount;

            int attendancePoint = 10;
            if (qbCount > 0)
            {
                if (qbCount >= 4)
                {
                    attendancePoint -= 2;
                }
                else if (qbCount >= 2)
                {
                    attendancePoint -= 1;
                }
            }

            EntryPointResultDto result = new EntryPointResultDto()
            {
                StudentUserId = dto.StudentUserId,
                Lesson = dto.Lesson,
                GradeAverage = average,
                ColloquiumPoint = colAvarage,
                TermPoint = termGrade,
                AttendancePoint = attendancePoint,
                TotalPoint = average + colAvarage + termGrade + attendancePoint,
                IsFailed = isFailed,
            };
            return result;
        }

    }   
}
