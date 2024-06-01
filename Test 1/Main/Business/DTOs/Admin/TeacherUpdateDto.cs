using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Admin
{
    public class TeacherUpdateDto
    {

        public string Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        [MaxLength(25)]
        [Required]
        public string Surname { get; set; }
        [MaxLength(30)]
        [Required]
        public string UserName { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        public string Kafedra { get; set; }
        [Required, DataType(DataType.Date)]
        public DateOnly Born { get; set; }
    }
}
