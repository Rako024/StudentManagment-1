using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TermPaper:BaseEntity
    {
        [Required,MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string StudentUserId { get; set; }
        public StudentUser? StudentUser { get; set; }
        [Required]
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public string? FileUrl { get; set; }
        [NotMapped]
        public IFormFile? TermPaperFile { get; set; }
    }
}
