using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Semester : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int SemesterNumber { get; set; }
    }
}
