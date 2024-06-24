using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Colloquium:BaseEntity
    {
        [Required]
        public string StudentUserId { get; set; } = null!;
        public StudentUser? StudentUser { get; set; }
        [Required]
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }

        public int? FirstGrade { get; set; }
        public int? SecondGrade { get; set;}
        public int? ThirdGrade { get; set; }
    }
}
