using Business.DTOs.Teacher.TeacherLessonsDto;
using Business.Exceptions.Lesson;
using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Exceptions.TermPaperGrade;

namespace Business.Services.Concretes
{
    public class TermPaperGradeService:ITermPaperGradeService
    {
        private readonly ITermPaperGradeRepository _termPaperGradeRepository;
        private readonly ILessonService _lessonService;
        private readonly IStudentUserService _studentUserService;

        public TermPaperGradeService(ITermPaperGradeRepository termPaperGradeRepository, ILessonService lessonService, IStudentUserService studentUserService)
        {
            _termPaperGradeRepository = termPaperGradeRepository;
            _lessonService = lessonService;
            _studentUserService = studentUserService;
        }

        public async Task CreateTermPaperGrade(TermPaperGradeDto fileDto)
        {
            var lesson = _lessonService.GetLesson(x => x.Id == fileDto.LessonId);
            if (lesson == null)
            {
                throw new LessonNotFoundException("Lesson not found!");
            }

            var student = _studentUserService.Get(x => x.Id == fileDto.StudentUserId);
            if (student == null)
            {
                throw new StudentUserNotFoundException("Student not found!");
            }

            var termPaperGrade = new TermPaperGrade
            {
                StudentUserId = fileDto.StudentUserId,
                LessonId = fileDto.LessonId,
                Grade = fileDto.Grade
            };

            _termPaperGradeRepository.Add(termPaperGrade);
            _termPaperGradeRepository.Commit();
        }

        public async Task DeleteTermPaperGrade(int id)
        {
            var termPaperGrade = await _termPaperGradeRepository.GetAsync(x => x.Id == id);
            if (termPaperGrade != null)
            {
                _termPaperGradeRepository.Remove(termPaperGrade);
                _termPaperGradeRepository.Commit();
            }
        }

        public async Task SoftDeleteTermPaperGrade(int id)
        {
            var termPaperGrade = await _termPaperGradeRepository.GetAsync(x => x.Id == id);
            if (termPaperGrade != null)
            {
                termPaperGrade.IsDeleted = true;
                _termPaperGradeRepository.Commit();
            }
        }

        public async Task<TermPaperGrade> GetTermPaperGrade(Func<TermPaperGrade, bool>? func = null)
        {
            return _termPaperGradeRepository.Get(func);
        }

        public async Task<List<TermPaperGrade>> GetAllTermPapers(
            Expression<Func<TermPaperGrade, bool>>? func = null,
            Expression<Func<TermPaperGrade, object>>? orderBy = null,
            bool isOrderByDescending = false,
            params Expression<Func<TermPaperGrade, object>>[] includes)
        {
            IQueryable<TermPaperGrade> data = await _termPaperGradeRepository.GetAll(func, orderBy, isOrderByDescending, includes);
            return data.ToList();
        }

        public async Task UpdateTermPaperGrade(int id, TermPaperGradeDto newTermPaper)
        {
            var termPaperGrade = await _termPaperGradeRepository.GetAsync(x => x.Id == id);
            if (termPaperGrade != null)
            {
                var lesson = _lessonService.GetLesson(x => x.Id == newTermPaper.LessonId);
                if (lesson == null)
                {
                    throw new LessonNotFoundException("Lesson not found!");
                }

                var student = _studentUserService.Get(x => x.Id == newTermPaper.StudentUserId);
                if (student == null)
                {
                    throw new StudentUserNotFoundException("Student not found!");
                }

                termPaperGrade.StudentUserId = newTermPaper.StudentUserId;
                termPaperGrade.LessonId = newTermPaper.LessonId;
                termPaperGrade.Grade = newTermPaper.Grade;
                _termPaperGradeRepository.Commit();
            }
            throw new TermPaperGradeNotFoundException("Term Paper Grade Not Found!");
        }
        public async Task CreateOrUpdateTermPaperGrade(TermPaperGradeDto termPaperGradeDto)
        {
            // Check if the lesson exists
            var lesson = _lessonService.GetLesson(x => x.Id == termPaperGradeDto.LessonId);
            if (lesson == null)
            {
                throw new LessonNotFoundException("Lesson not found!");
            }

            // Check if the student exists
            var student = _studentUserService.Get(x => x.Id == termPaperGradeDto.StudentUserId);
            if (student == null)
            {
                throw new StudentUserNotFoundException("Student not found!");
            }

            // Check if the term paper grade already exists
            var existingTermPaperGrade = await _termPaperGradeRepository.GetAsync(
                x => x.StudentUserId == termPaperGradeDto.StudentUserId && x.LessonId == termPaperGradeDto.LessonId);

            if (existingTermPaperGrade != null)
            {
                // Update the existing term paper grade
                existingTermPaperGrade.Grade = termPaperGradeDto.Grade;
                _termPaperGradeRepository.Commit();
            }
            else
            {
                // Create a new term paper grade
                var newTermPaperGrade = new TermPaperGrade
                {
                    StudentUserId = termPaperGradeDto.StudentUserId,
                    LessonId = termPaperGradeDto.LessonId,
                    Grade = termPaperGradeDto.Grade
                };

                _termPaperGradeRepository.Add(newTermPaperGrade);
                _termPaperGradeRepository.Commit();
            }
        }

       
    }
}
