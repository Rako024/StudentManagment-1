using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class GradeAndAttendace:BaseEntity
    {
        [Required]
        public string StudentUserId { get; set; } = null!;
        public StudentUser? StudentUser { get; set; }

        [Required]
        public int LessonTimeId { get; set; }
        public LessonTime? LessonTime { get; set; }

        
        [Range(0, 10)]
        public int? Score { get; set; }

        
        public bool? IsPresent { get; set; }
    }
}
