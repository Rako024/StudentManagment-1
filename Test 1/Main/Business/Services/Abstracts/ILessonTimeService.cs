using Core.Helper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ILessonTimeService
    {
        Task CreateLessonTime(LessonTime lessonTime);
        void DeleteLessonTime(int id);
        void SoftDeleteLessonTime(int id);
        Task UpdateLessonTime(int id, LessonTime lessonTime);
        Task<bool> CheckDate(DateTime date, int lessonId);
        Task<bool> CheckDate(LessonTime lessonTime, int lessonId);
        LessonTime GetLessonTime(Func<LessonTime, bool>? func = null);
        Task<LessonTime> GetLessonTimeAsync(Expression<Func<LessonTime, bool>> func);
        Task<List<LessonTime>> GetAllLessonTimes(Expression<Func<LessonTime, bool>>? func = null,
             Expression<Func<LessonTime, object>>? orderBy = null,
             bool isOrderByDesting = false,
            params Expression<Func<LessonTime, object>>[] includes);
        List<LessonTime> GetLessonsWithLessonWithTeacherAndGroup(Func<LessonTime, bool>? func = null);
        Task<PageResult<LessonTime>> GetPagedLessonTimes(
        int pageNumber,
        int pageSize,
        Expression<Func<LessonTime, bool>>? filter = null,
        Expression<Func<LessonTime, object>>? orderBy = null,
        bool isOrderByDescending = false,
        params Expression<Func<LessonTime, object>>[] includes);
    }
}
