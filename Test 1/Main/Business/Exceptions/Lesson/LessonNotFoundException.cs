using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions.Lesson
{
    public class LessonNotFoundException : Exception
    {
        public LessonNotFoundException(string? message="Lesson Not Found!") : base(message)
        {
        }
    }
}
