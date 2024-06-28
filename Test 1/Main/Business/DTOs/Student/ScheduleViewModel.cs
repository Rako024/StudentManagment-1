using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Student
{
    public class ScheduleViewModel
    {
        public DateTime WeekStartDate { get; set; }
        public List<LessonTime> Lessons { get; set; }
        public Dictionary<DateTime, List<LessonTime>> WeeklySchedule { get; set; }

        public ScheduleViewModel()
        {
            WeeklySchedule = new Dictionary<DateTime, List<LessonTime>>();
        }
    }


}
