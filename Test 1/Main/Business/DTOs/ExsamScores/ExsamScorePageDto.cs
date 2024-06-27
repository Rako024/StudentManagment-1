using Business.DTOs.EntryPoints;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.ExsamScores
{
    public record ExsamScorePageDto
    {
        public List<StudentUser> StudentUsers {  get; set; }
        public Lesson Lesson { get; set; }
        public Group Group { get; set; }
        public List<ExamScore> ExamScores { get; set; }
        public List<EntryPointResultDto> EntryPoints { get; set; }
    }
}
