using Business.DTOs.Notifications;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles ="Admin,Conrdinator")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly ITeacherUserService _teacherUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICordinatorUserService _coordinatorUserService;

        public NotificationController(INotificationService notificationService, ITeacherUserService teacherUserService, UserManager<AppUser> userManager, ICordinatorUserService coordinatorUserService)
        {
            _notificationService = notificationService;
            _teacherUserService = teacherUserService;
            _userManager = userManager;
            _coordinatorUserService = coordinatorUserService;
        }

        public async Task<IActionResult> Index()
        {
            List<Notification> notificationList = await _notificationService.GetAllNotificationsAsync
                (
                    x=> x.IsDeleted==false,
                    x=> x.CreatedDate,
                    false
                );
            List<NotificatioPageDto> dtos = new List<NotificatioPageDto> ();
            foreach(var item in notificationList)
            {
                TeacherUser teacher = _teacherUserService.GetTeacher(x => x.Id == item.ReceiverId);
                AppUser cordinator = await _userManager.FindByIdAsync(item.SenderId);
                NotificatioPageDto dto = new NotificatioPageDto()
                {
                    Notification = item,
                    Teacher = teacher,
                    Cordinator = cordinator,
                };
                dtos.Add(dto);
            }
            
            return View(dtos);
        }

        public async Task<IActionResult> Create()
        {
            
            var teachers = await _teacherUserService.GetAllTeachers(x => !x.IsDeleted);
            ViewBag.Teachers = teachers;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string content, string receiverId)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            if (receiverId !="All")
            {
                TeacherUser teacher = _teacherUserService.GetTeacher(x => x.Id == receiverId);
                if (teacher == null)
                {
                    return View("Error");
                }
            }
           
            NotificationDto notification = new NotificationDto
            {
                SenderId = user.Id,
                ReceiverId = receiverId == "All" ? null : receiverId,
                Content = content
            };
            try
            {
            await _notificationService.SendNotificationAsync(notification);

            }catch (Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _notificationService.DeleteNotificationAsync(id);
            }catch(Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
