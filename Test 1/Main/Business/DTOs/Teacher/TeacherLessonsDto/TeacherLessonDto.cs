using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.TeacherLessonsDto
{
	public record TeacherLessonDto
	{
        public Lesson Lesson { get; set; }
		public List<StudentUser> Students { get; set; }
        public List<LessonTime> LessonTimes { get; set; }
        public List<GradeAndAttendace> GradeAndAttendances { get; set; }
    }
}
