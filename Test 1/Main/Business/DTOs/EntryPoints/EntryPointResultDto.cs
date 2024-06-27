using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.EntryPoints
{
    public record EntryPointResultDto
    {
        public string StudentUserId { get; set; }
        public Lesson Lesson { get; set; }
        public int GradeAverage { get; set; }
        public int ColloquiumPoint { get; set;}
        public int TermPoint { get; set; }
        public int AttendancePoint { get; set; }
        public int TotalPoint { get; set; }
        public bool IsFailed { get; set; }
    }
}
