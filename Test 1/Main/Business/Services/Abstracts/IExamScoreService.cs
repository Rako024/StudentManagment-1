using Business.DTOs.ExamScores;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IExamScoreService
    {
        Task CreateExsamScore(ExamScoreDto dto);
        Task UpdateExamScore(int id, ExamScoreDto dto);
        Task DeleteExamScore(int id);
        Task SoftDeleteExamScore(int id);
        Task<ExamScore> GetExamScore(Expression<Func<ExamScore, bool>>? func = null);
        Task CreateOrUpdateExamScore(ExamScoreDto dto);
        Task<List<ExamScore>> GetAllExamScores
             (
            Expression<Func<ExamScore, bool>>? func = null,
            Expression<Func<ExamScore, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<ExamScore, object>>[] includes
            );
    }
}
