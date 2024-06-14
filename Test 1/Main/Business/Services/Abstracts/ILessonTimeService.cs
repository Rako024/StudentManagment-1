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
        void CreateLessonTime(LessonTime lessonTime);
        void DeleteLessonTime(int id);
        void SoftDeleteLessonTime(int id);
        void UpdateLessonTime(int id, LessonTime lessonTime);
        LessonTime GetLessonTime(Func<LessonTime, bool>? func = null);
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
