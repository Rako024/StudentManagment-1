using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions.Semester
{
    public class SemesterNotFoundException : Exception
    {
        public string PropertyName { get; set; }
        public SemesterNotFoundException(string propName,string? message) : base(message)
        {
            PropertyName = propName;
        }
    }
}
