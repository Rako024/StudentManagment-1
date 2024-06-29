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
    public class LearningMaterial:BaseEntity
    {
        [Required]
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }
        [Required]
        public string Name { get; set; }
        public string? FileUrl { get; set; }
        [NotMapped]
        public IFormFile? MaterialFile { get; set; }
        public string? Link { get; set; }
    }
}
