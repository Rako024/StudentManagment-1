using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ILessonService
    {
        void CreateLesson(Lesson lesson);
        void DeleteLesson(int id);
        void UpdateLesson(int id,Lesson lesson);
        Lesson GetLesson(Func<Lesson,bool>? func = null);
        List<Lesson> GetAllLessons(Func<Lesson, bool>? func = null);
        List<Lesson> GetAllLessinsWithGroupAndTeacherUser(Func<Lesson, bool>? func = null);
        Lesson GetLessonsWithGroupAndTeacherUser(Func<Lesson, bool>? func = null);
    }
}
