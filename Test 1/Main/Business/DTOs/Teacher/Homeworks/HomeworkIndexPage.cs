using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.Homeworks
{
    public record HomeworkIndexPage
    {
        public List<Homework> Homeworks { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
