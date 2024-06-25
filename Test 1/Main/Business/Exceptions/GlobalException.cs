using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class GlobalException:Exception
    {
        public int ErrorCode { get; set; }

        public GlobalException() : base() { }

        public GlobalException(string message) : base(message) { }

        public GlobalException(string message, Exception innerException) : base(message, innerException) { }

        public GlobalException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public GlobalException(string message, int errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
