using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.TeacherLessonsDto
{
    public class TermPaperGradeDto
    {

        [Required]
        public string StudentUserId { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Required, Range(0, 10)]
        public int Grade { get; set; }
    }
}
