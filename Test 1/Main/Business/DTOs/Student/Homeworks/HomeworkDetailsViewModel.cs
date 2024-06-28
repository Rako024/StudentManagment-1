using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Student.Homeworks
{
    public record HomeworkDetailsViewModel
    {
        public List<Homework> Homeworks { get; set; }
        public List<HomeworkSubmission> Submissions { get; set; }
    }
}
