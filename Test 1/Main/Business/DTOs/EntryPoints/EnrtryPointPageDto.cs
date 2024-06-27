using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.EntryPoints
{
    public record EnrtryPointPageDto
    {
        public List<StudentUser> StudentUsers { get; set; }
        public Lesson Lesson { get; set; }
        public List<EntryPointResultDto> EntryPoints { get; set; }
    }
}
