using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.Homeworks
{
    public record DetailsViewModel
    {
        public Homework Homework { get; set; }
        public List<HomeworkSubmission> Submissions { get; set; }
        public List<StudentUser> Students { get; set; }
    }

}
