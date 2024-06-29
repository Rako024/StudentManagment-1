using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Notifications
{
    public record NotificationDto
    {
        public int? Id { get; set; }
        [Required]
        public string SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
