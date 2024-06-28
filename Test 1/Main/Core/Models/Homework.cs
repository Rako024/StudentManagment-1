using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
	public class Homework : BaseEntity
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(500)]
		public string Description { get; set; }

		public int LessonId { get; set; }
		public Lesson? Lesson { get; set; }

		public List<HomeworkSubmission>? Submissions { get; set; }
	}
}
