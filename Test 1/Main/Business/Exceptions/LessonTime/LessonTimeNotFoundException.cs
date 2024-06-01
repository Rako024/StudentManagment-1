using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions.LessonTime
{
    public class LessonTimeNotFoundException : Exception
    {
        public LessonTimeNotFoundException(string? message = "LessonTime Not Found!") : base(message)
        {
        }
    }
}
