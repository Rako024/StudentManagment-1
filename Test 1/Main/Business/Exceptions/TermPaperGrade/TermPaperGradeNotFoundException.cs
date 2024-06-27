using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions.TermPaperGrade
{
    public class TermPaperGradeNotFoundException : Exception
    {
        public TermPaperGradeNotFoundException(string? message) : base(message)
        {
        }
    }
}
