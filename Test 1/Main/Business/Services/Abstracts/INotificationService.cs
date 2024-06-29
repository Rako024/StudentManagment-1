using Business.DTOs.Notifications;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface INotificationService
    {
        Task SendNotificationAsync(NotificationDto notification);
        Task<List<Notification>> GetNotificationsAsync(string userId);
        Task<List<Notification>> GetAllNotificationsAsync
            (
            Expression<Func<Notification, bool>>? func = null,
            Expression<Func<Notification, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<Notification, object>>[] includes
            );
        Task DeleteNotificationAsync(int notificationId);
        Task<Notification> GetNotification(Expression<Func<Notification, bool>>? func = null);
        Task UpdateNotificationAsync(int id, NotificationDto notification);
    }
}
