using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Admin.LessonDTOs
{
    public struct LessonDetails
    {
        public List<LessonTime> LessonTimes { get; set; }
        public Lesson Lesson { get; set; }
    }
}
