using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Cordinator
{
	public record CordinatorCreateDto
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string UserName { get; set; }
		
		[Required]
		public DateOnly Born { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required, DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
