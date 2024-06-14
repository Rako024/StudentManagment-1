using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ILessonService
    {
        void CreateLesson(Lesson lesson);
        void DeleteLesson(int id);
        void SoftDeleteLesson(int id);
        void UpdateLesson(int id,Lesson lesson);
        Lesson GetLesson(Func<Lesson,bool>? func = null);
        Task<List<Lesson>> GetAllLessons
            (
            Expression<Func<Lesson, bool>>? func = null,
            Expression<Func<Lesson, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<Lesson, object>>[] includes
            );
        List<Lesson> GetAllLessinsWithGroupAndTeacherUser(Func<Lesson, bool>? func = null);
        Lesson GetLessonsWithGroupAndTeacherUser(Func<Lesson, bool>? func = null);
    }
}
