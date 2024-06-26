using Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Student.Files
{
    public class TermFileDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string StudentUserId { get; set; }
        
        [Required]
        public int LessonId { get; set; }
        
        [MaxLength(200)]
        public string? Description { get; set; }
        [Required]
        public IFormFile TermPaperFile { get; set; }
    }
}
