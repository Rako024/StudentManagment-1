using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.TeacherLessonsDto
{
    public record GradeAndAttendaceDto
    {
        [Required]
        public string StudentId { get; set; }
        [Required]
        public int LessonTimeId { get; set; }
        
        public string? GradeAndAttendance { get; set; }
    }
}
