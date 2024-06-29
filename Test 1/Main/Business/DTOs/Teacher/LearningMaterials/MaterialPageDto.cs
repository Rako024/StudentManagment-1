using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.LearningMaterials
{
    public record MaterialPageDto
    {
        public List<LearningMaterial> LearningMaterials { get; set; }
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }
    }
}
