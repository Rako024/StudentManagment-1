using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions.ExamScore
{
    public class ExamScoreNotFoudException : Exception
    {
        public ExamScoreNotFoudException(string? message) : base(message)
        {
        }
    }
}
