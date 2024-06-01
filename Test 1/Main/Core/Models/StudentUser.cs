using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class StudentUser:AppUser
    {
        public int GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
