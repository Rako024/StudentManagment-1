using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Group:BaseEntity
    {
        public string Name { get; set; }
        public string FacultyName { get; set; }
        public string Specialty { get; set; }
        public List<StudentUser>? Students { get; set; }
    }
}
