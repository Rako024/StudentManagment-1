using Business.DTOs.EntryPoints;
using Business.DTOs.ExsamScores;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Student.StudentInformations
{
    public record StudentInfoLessonPointInfoDto
    {
        public StudentUser StudentUser { get; set; }
        public Lesson Lesson { get; set; }
        public List<int> Grades { get; set; }
        public int QbCount { get; set; }
        public int? ColloquiumFirst { get; set; }
        public int? ColloquiumSecound { get; set; }
        public int? ColloquiumThird { get; set; }
        public int? TermPaperGrade { get; set; }
        public ExamScore ExsamScore { get; set; }
        public EntryPointResultDto EntryPoint { get; set; }
    }
}
