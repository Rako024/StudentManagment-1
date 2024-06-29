using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.LearningMaterials
{
    public record CreateLearningMaterialDto
    {
        public int? Id { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        public string? FileUrl { get; set; }
        public string? Link { get; set; }
        public IFormFile? File { get; set; }
    }
}
