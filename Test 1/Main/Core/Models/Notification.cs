using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Notification:BaseEntity
    {
        [Required]
        public string SenderId { get; set; } 
        public CordinatorUser? Sender { get; set; } 
        public string? ReceiverId { get; set; } 
        public TeacherUser? Receiver { get; set; } 
        public string Content { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
    }
}
