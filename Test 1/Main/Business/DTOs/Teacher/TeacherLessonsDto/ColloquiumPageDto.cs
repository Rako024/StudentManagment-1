using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.TeacherLessonsDto
{
    public class ColloquiumPageDto
    {
        public List<StudentUser> Students {  get; set; }
        public Lesson Lesson { get; set; }
        public List<Colloquium> Colloquia { get; set; }
    }
}
