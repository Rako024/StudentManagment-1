using System.ComponentModel.DataAnnotations;

namespace Main.DTOs.Admin
{
    public class StudentCreateDto
    {
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
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConPassword { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateOnly Born { get; set; }
    }
}
