using Business.DTOs.Admin;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ITeacherUserService
    {
        Task CreateTeacher(TeacherCreateDto dto);
        Task DeleteTeacher(string id);
        Task UpdateTeacher(string id, TeacherUpdateDto dto);
        List<TeacherUser> GetAllTeachers(Func<TeacherUser, bool>? func = null);
        TeacherUser GetTeacher(Func<TeacherUser,bool>? func = null);
        Task UpdatePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
