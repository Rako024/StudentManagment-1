using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Student.Files
{
    public record TermPaperIndexDto
    {
        public List<Lesson> Lessons {  get; set; }
        public string UserId { get; set; }
    }
}
