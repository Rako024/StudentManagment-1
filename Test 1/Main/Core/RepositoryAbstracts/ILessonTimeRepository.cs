using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryAbstracts
{
    public interface ILessonTimeRepository:IGenericRepositroy<LessonTime>
    {
        List<LessonTime> GetLessonsWithLessonWithTeacherAndGroup(Func<LessonTime,bool>? func = null);
    }
}
