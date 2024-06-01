using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class StudentUserNotFoundException : Exception
    {
        public StudentUserNotFoundException(string? message) : base(message)
        {
        }
    }
}
