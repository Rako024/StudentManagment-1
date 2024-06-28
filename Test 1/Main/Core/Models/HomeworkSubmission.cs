using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class HomeworkSubmission : BaseEntity
	{
		public int HomeworkId { get; set; }
		public Homework? Homework { get; set; }

		public string StudentUserId { get; set; }
		public StudentUser? StudentUser { get; set; }

		public string? Link { get; set; }

		public string? FileUrl { get; set; }

		[MaxLength(500)]
		public string Description { get; set; }

		[NotMapped]
		public IFormFile? HomeworkFile { get; set; }
	}
}
