using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Student.Files
{
    public record TermPaperDetailsDto
    {
        public List<TermPaper> TermPapers { get; set; }
        public string UserId { get; set; }
        public StudentUser? StudentUser { get; set; }
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }
    }
}
