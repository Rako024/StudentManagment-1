using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.Homeworks
{
	public record HomeworkDto
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(500)]
		public string Description { get; set; }

		[Required]
		public int LessonId { get; set; }
	}

}
