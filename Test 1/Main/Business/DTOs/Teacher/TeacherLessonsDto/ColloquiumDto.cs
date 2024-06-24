using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.TeacherLessonsDto
{
    public class ColloquiumDto
    {
        [Required]
        public string StudentUserId { get; set; }
        [Required]  
        public int LessonId { get; set; }
        public int? FirstGrade { get; set; }
        public int? SecondGrade { get; set; }
        public int? ThirdGrade { get; set; }
    }
}
