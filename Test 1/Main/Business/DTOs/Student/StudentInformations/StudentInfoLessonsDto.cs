using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Student.StudentInformations
{
    public  record StudentInfoLessonsDto
    {
        public List<Lesson> Lessons { get; set; }
        public string StudentUserId { get; set; }
    }
}
