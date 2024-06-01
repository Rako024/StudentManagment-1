using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ILessonTimeService
    {
        void CreateLessonTime(LessonTime lessonTime);
        void DeleteLessonTime(int id);
        void UpdateLessonTime(int id, LessonTime lessonTime);
        LessonTime GetLessonTime(Func<LessonTime, bool>? func = null);
        List<LessonTime> GetAllLessonTimes(Func<LessonTime, bool>? func = null);
        List<LessonTime> GetLessonsWithLessonWithTeacherAndGroup(Func<LessonTime, bool>? func = null);
    }
}
