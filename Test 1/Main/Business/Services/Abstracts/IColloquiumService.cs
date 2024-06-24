using Business.DTOs.Teacher.TeacherLessonsDto;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IColloquiumService
    {
        Task AddColloquium(ColloquiumDto dto);
        Task UpdateColloquium(ColloquiumDto dto);
        Task<List<Colloquium>> GetAllColloquiumAsync(Expression<Func<Colloquium, bool>>? func = null,
            Expression<Func<Colloquium, object>>? orderBy = null,
            bool isOrderByDesting = false,
           params Expression<Func<Colloquium, object>>[] includes);
        Task<Colloquium> GetColloquiumAsync(Func<Colloquium, bool>? func = null);
        Task<bool> CheckColloquiumAsync(string studentId, int lessonId);
        Task AddOrUpdateColloquiumAsync(ColloquiumDto dto);
    }
}
