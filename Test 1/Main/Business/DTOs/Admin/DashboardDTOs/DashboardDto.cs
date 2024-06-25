using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Admin.DashboardDTOs
{
    public record DashboardDto
    {
        public int TeacherCount { get; set; }
        public int StudentCount { get; set; }
        public int GroupCount { get; set; }
        public int LessonCount { get; set; }
    }
}
