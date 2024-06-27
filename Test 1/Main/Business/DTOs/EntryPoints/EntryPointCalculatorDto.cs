using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.EntryPoints
{
    public record EntryPointCalculatorDto
    {
        public string StudentUserId { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public List<int> Grades { get; set; }
        public int QbCount { get; set; }
        public int? ColloquiumFirst { get; set; }
        public int? ColloquiumSecound { get; set; }
        public int? ColloquiumThird { get; set; }
        public int? TermPaperGrade { get; set; }
    }
}
