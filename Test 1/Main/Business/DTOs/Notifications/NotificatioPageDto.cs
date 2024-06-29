using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Notifications
{
    public record NotificatioPageDto
    {
        public Notification Notification { get; set; }
        public TeacherUser? Teacher { get; set; }
        public AppUser Cordinator { get; set; }
    }
}
