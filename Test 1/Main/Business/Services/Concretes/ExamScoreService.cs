using Business.DTOs.ExamScores;
using Business.Exceptions.ExamScore;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class ExamScoreService : IExamScoreService
    {
        IExamScoreRepository _examScoreRepository;

        public ExamScoreService(IExamScoreRepository examScoreRepository)
        {
            _examScoreRepository = examScoreRepository;
        }

        public async Task CreateExsamScore(ExamScoreDto dto)
        {
            var examScore = new ExamScore
            {
                StudentUserId = dto.StudentUserId,
                LessonId = dto.LessonId,
                Score = dto.Score
            };

            _examScoreRepository.Add(examScore);
            _examScoreRepository.Commit();
        }

        public async Task DeleteExamScore(int id)
        {
            var examScore = await _examScoreRepository.GetAsync(e => e.Id == id);
            if (examScore != null)
            {
                _examScoreRepository.Remove(examScore);
                _examScoreRepository.Commit();
            }
            throw new ExamScoreNotFoudException("Exam Score is Not Found!");
        }

        public async Task<List<ExamScore>> GetAllExamScores(Expression<Func<ExamScore, bool>>? func = null, Expression<Func<ExamScore, object>>? orderBy = null, bool isOrderByDesting = false, params Expression<Func<ExamScore, object>>[] includes)
        {
            IQueryable<ExamScore> examScores = await _examScoreRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            return examScores.ToList();
        }

        public async Task<ExamScore> GetExamScore(Expression<Func<ExamScore, bool>>? func = null)
        {
            return await _examScoreRepository.GetAsync(func);
        }

        public async Task SoftDeleteExamScore(int id)
        {
            var examScore = await _examScoreRepository.GetAsync(e => e.Id == id);
            if (examScore != null)
            {
                examScore.IsDeleted = true;
                 _examScoreRepository.Commit();
            }
            throw new ExamScoreNotFoudException("Exam Score is Not Found!");
        }

        public async Task UpdateExamScore(int id, ExamScoreDto dto)
        {
            var examScore = await _examScoreRepository.GetAsync(e => e.Id == id);
            if (examScore != null)
            {
                examScore.StudentUserId = dto.StudentUserId;
                examScore.LessonId = dto.LessonId;
                examScore.Score = dto.Score;
                _examScoreRepository.Commit();
            }
            throw new ExamScoreNotFoudException("Exam Score is Not Found!");
        }
        public async Task CreateOrUpdateExamScore(ExamScoreDto dto)
        {
            var existingExamScore = await _examScoreRepository.GetAsync(e => e.StudentUserId == dto.StudentUserId && e.LessonId == dto.LessonId);

            if (existingExamScore != null)
            {
                existingExamScore.Score = dto.Score;
                _examScoreRepository.Commit();
            }
            else
            {
                var examScore = new ExamScore
                {
                    StudentUserId = dto.StudentUserId,
                    LessonId = dto.LessonId,
                    Score = dto.Score
                };

                _examScoreRepository.Add(examScore);
                _examScoreRepository.Commit();
            }
        }
    }
}
