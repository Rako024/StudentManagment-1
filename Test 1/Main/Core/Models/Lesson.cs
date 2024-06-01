using Core.CoreEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Lesson : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }

        public string? TeacherUserId { get; set; }
        public TeacherUser? TeacherUser { get; set; }
        [Range(1, 60)]
        public int LessonCount { get; set; }    
        public Semesters Semester  { get; set; }
        public Years Year { get; set; }
        public List<LessonTime>? LessonTimes { get; set; }

    }
}
