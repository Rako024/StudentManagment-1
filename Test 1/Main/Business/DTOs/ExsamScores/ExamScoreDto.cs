using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.ExamScores
{
    public record ExamScoreDto
    {
        [Required]
        public string StudentUserId { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Range(0,50)]
        public int? Score { get; set; }
    }
}
