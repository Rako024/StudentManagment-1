using Business.DTOs.EntryPoints;
using Business.DTOs.ExamScores;
using Business.DTOs.ExsamScores;
using Business.Exceptions.Lesson;
using Business.Helper;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Cordinator")]
    public class ExamScoreController : Controller
    {
        IStudentUserService _studentUserService;
        ILessonService _lessonService;
        IGradeAndAttendaceService _gradeAndAttendaceService;
        ITermPaperGradeService _termPaperGradeService;
        IColloquiumService _colloquiumService;
        IGroupService _groupService;
        IExamScoreService _examScoreService;

        public ExamScoreController(IStudentUserService studentUserService, ILessonService lessonService, IGradeAndAttendaceService gradeAndAttendaceService, ITermPaperGradeService termPaperGradeService, IColloquiumService colloquiumService, IGroupService groupService, IExamScoreService examScoreService)
        {
            _studentUserService = studentUserService;
            _lessonService = lessonService;
            _gradeAndAttendaceService = gradeAndAttendaceService;
            _termPaperGradeService = termPaperGradeService;
            _colloquiumService = colloquiumService;
            _groupService = groupService;
            _examScoreService = examScoreService;
        }

        public async Task<IActionResult> Index(string? searchTerm)
        {
            List<Core.Models.Group> groups;
            if (string.IsNullOrEmpty(searchTerm))
            {
                groups = await _groupService.GetAllGroup
                    (
                    x=>x.IsDeleted == false,
                    x=> x.Name
                    );
            }
            else
            {
                string upperSearchTerm = searchTerm.ToUpper();
                groups = await _groupService.GetAllGroup(
                    g => !g.IsDeleted && g.Name.ToUpper().Contains(upperSearchTerm),
                    g => g.Name);
            }
            ViewBag.SearchTerm = searchTerm;
            return View(groups);
        }

        public async Task<IActionResult> Lessons(int id, string? searchTerm = null,bool isPast = false)
        {
            Group  group =  _groupService.GetGroup(x=>x.Id == id);
            if (group == null)
                return View("Error");
            List<Lesson> lessons;
            if (searchTerm.IsNullOrEmpty())
            {
                lessons = await _lessonService.GetAllLessons
                    (
                    x => x.IsDeleted == false &&
                    x.GroupId == id &&
                    x.IsPast == isPast,
                    x => x.Name,
                    false,
                    x=>x.TeacherUser,
                    x=>x.Group
                    );
            }
            else
            {
                string upperSearchTerm = searchTerm.ToUpper();
                lessons = await _lessonService.GetAllLessons
                    (
                    x => x.IsDeleted == false &&
                    x.GroupId == id &&
                    x.IsPast == isPast &&
                    x.Name.ToUpper().Contains(upperSearchTerm),
                    x => x.Name,
                    false,
                    x => x.TeacherUser,
                    x => x.Group
                    );
            }
            ViewBag.Group= group;
            ViewBag.IsPast = isPast;
            ViewBag.SearchTerm = searchTerm;
            return View(lessons);
        }

        public async Task<IActionResult> AddExamScore(int id, int groupId)
        {
            Group group = _groupService.GetGroup(x => x.Id == groupId);
            if (group == null)
            {
                return View("Error");
            }
            Lesson lesson = _lessonService.GetLesson(x => x.Id == id);
            if (lesson == null)
            {
                return View("Error");
            }
            List<StudentUser> students = await _studentUserService.GetAll
                (
                    x => x.IsDeleted == false &&
                    x.GroupId == groupId
                );
            List<EntryPointResultDto> results = new List<EntryPointResultDto>();
            List<ExamScore> examScores = new List<ExamScore>();
            foreach (var student in students)
            {
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
                results.Add(entryResult);

                ExamScore exam = await _examScoreService.GetExamScore(x=>x.StudentUserId == student.Id && x.LessonId == lesson.Id);
                if (exam != null)
                {
                    examScores.Add(exam);
                }
            }
            
            ExsamScorePageDto enrtryPointPageDto = new ExsamScorePageDto()
            { 
                StudentUsers = students,
                Lesson = lesson,
                Group = group,
                ExamScores = examScores,
                EntryPoints = results
            };
            return View(enrtryPointPageDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateExsamScore(ExamScoreDto examScoreDto)
        {
            var user = _studentUserService.Get(x => x.Id == examScoreDto.StudentUserId);
            var lesson = _lessonService.GetLesson(x=>x.Id ==  examScoreDto.LessonId);
            if(user == null || lesson == null) 
            {
                return View("Error");
            }
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AddExamScore), new { id = lesson.Id, groupId = lesson.GroupId });
                
            }
            await _examScoreService.CreateOrUpdateExamScore(examScoreDto);
            return RedirectToAction(nameof(AddExamScore), new {id=lesson.Id,groupId=lesson.GroupId});
        }


        public async Task<IActionResult> ChangeIsPast(int lessonId, bool isPast, int groupId)
        {
            Group group = _groupService.GetGroup(x => x.Id == groupId);
            Lesson lesson = _lessonService.GetLesson(x => x.Id == lessonId);
            if(lesson == null || group == null)
            {
                return View("Error");
            }
            try
            {
                await _lessonService.ChangeIsPast(lessonId, isPast);
            }catch(LessonNotFoundException)
            {
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }


            return RedirectToAction(nameof(AddExamScore), new {id=lessonId,groupId = groupId});
        }
    }
}
