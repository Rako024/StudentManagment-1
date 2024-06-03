using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryAbstracts
{
    public interface ILessonRepository:IGenericRepositroy<Lesson>
    {
        List<Lesson> GetAllLessonsWithGroupAndTeacherUser(Func<Lesson,bool>? func = null);
        Lesson GetLessonsWithGroupAndTeacherUser(Func<Lesson, bool>? func = null);
    }
}
