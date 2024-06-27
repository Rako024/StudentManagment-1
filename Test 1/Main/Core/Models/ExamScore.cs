using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ExamScore:BaseEntity
    {
        [Required]
        public string StudentUserId { get; set; }
        public StudentUser? StudentUser { get; set; }
        [Required]
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        [Range(0,50)]
        public int? Score { get; set; }
    }
}
