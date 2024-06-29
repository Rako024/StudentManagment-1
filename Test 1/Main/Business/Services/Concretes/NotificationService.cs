using Business.DTOs.Notifications;
using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetAsync(n => n.Id == notificationId);
            if (notification != null)
            {
                notification.IsDeleted = true;
                _notificationRepository.Commit();
                return;
            }
            throw new GlobalException("Notification not Found!");
        }

        public async Task<List<Notification>> GetAllNotificationsAsync(Expression<Func<Notification, bool>>? func = null, Expression<Func<Notification, object>>? orderBy = null, bool isOrderByDesting = false, params Expression<Func<Notification, object>>[] includes)
        {
            IQueryable<Notification> query = await _notificationRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            return query.ToList();  
        }

        public Task<Notification> GetNotification(Expression<Func<Notification, bool>>? func = null)
        {
            return _notificationRepository.GetAsync(func);
        }

        public async Task<List<Notification>> GetNotificationsAsync(string userId)
        {
            IQueryable<Notification> query = await _notificationRepository.GetAll(
            n => (n.ReceiverId == userId || n.ReceiverId == null) && n.IsDeleted == false,
            n=>n.CreatedDate,true);
            return query.ToList();
        }

        

        public Task SendNotificationAsync(NotificationDto notification)
        {
            if(notification == null)
            {
                throw new GlobalException("Notification DTO not Found!");
            }
            Notification model = new Notification()
            {
                ReceiverId = notification.ReceiverId,
                SenderId = notification.SenderId,
                Content = notification.Content,

            };
            _notificationRepository.Add(model);
            _notificationRepository.Commit();
            return Task.CompletedTask;
        }

        public async Task UpdateNotificationAsync(int id, NotificationDto notificationDto)
        {
            var notification = await _notificationRepository.GetAsync(n => n.Id == id);
            if (notification == null)
            {
                throw new ArgumentException("Notification not found.");
            }

            
            notification.ReceiverId = notificationDto.ReceiverId;
            notification.Content = notificationDto.Content;

           
            _notificationRepository.Commit();

        }
    }
}
