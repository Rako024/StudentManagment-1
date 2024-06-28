using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Teacher.Homeworks
{
	public record HomeworkSubmissionDto
	{
		public int Id { get; set; }

		[Required]
		public int HomeworkId { get; set; }

		[Required]
		public string StudentUserId { get; set; }

		public string? Link { get; set; }

		public string? FileUrl { get; set; }
		public IFormFile? File { get; set; }

		[MaxLength(500)]
		public string Description { get; set; }
	}

}
