using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions.Teacher
{
    public class NotFoundColloquiumException : Exception
    {
        public NotFoundColloquiumException(string? message = "Colloquium not found") : base(message)
        {
        }
    }
}
