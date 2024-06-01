using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class StudentUserNotCreatedException : Exception
    {
        public StudentUserNotCreatedException(string? message) : base(message)
        {
        }
    }
}
