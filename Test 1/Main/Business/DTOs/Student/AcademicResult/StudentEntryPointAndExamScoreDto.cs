using Business.DTOs.EntryPoints;
using Core.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Student.AcademicResult
{
    public record StudentEntryPointAndExamScoreDto
    {
        public string StudentUserId { get; set; }
        public Lesson Lesson { get; set; }
        public EntryPointResultDto? EntryPoint { get; set; }
        public ExamScore? ExamScore { get; set; }
    }
}
