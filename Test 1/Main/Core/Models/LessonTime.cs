using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class LessonTime:BaseEntity
    {
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
