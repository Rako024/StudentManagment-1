using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Admin
{
    public class GroupNameNotUniqueException : Exception
    {
        public GroupNameNotUniqueException(string message) : base(message)
        {
        }
    }
}
