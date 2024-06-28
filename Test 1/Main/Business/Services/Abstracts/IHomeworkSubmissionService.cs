using Business.DTOs.Teacher.Homeworks;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IHomeworkSubmissionService
    {
        Task CreateHomeworkSubmission(HomeworkSubmissionDto submissionDto);
        Task UpdateHomeworkSubmission(int id, HomeworkSubmissionDto submissionDto);
        Task DeleteHomeworkSubmission(int id);
        Task<HomeworkSubmission> GetHomeworkSubmission(Expression<Func<HomeworkSubmission, bool>>? func = null);
        Task<List<HomeworkSubmission>> GetAllHomeworkSubmissions(Expression<Func<HomeworkSubmission, bool>>? func = null, Expression<Func<HomeworkSubmission, object>>? orderBy = null, bool isOrderByDescending = false, params Expression<Func<HomeworkSubmission, object>>[] includes);
    }
}
