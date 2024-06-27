using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.TeacherLessonsDto
{
    public record TermPaperPageDto
    {
        public Lesson Lesson { get; set; }
        public List<StudentUser> Students { get; set; }
        public List<TermPaperGrade> TermPaperGrades { get; set; }
    }
}
