using Business.DTOs.Admin;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ITeacherUserService
    {
        Task CreateTeacher(TeacherCreateDto dto);
        Task DeleteTeacher(string id);
        Task UpdateTeacher(string id, TeacherUpdateDto dto);
        Task<List<TeacherUser>> GetAllTeachers
            (
            Expression<Func<TeacherUser, bool>>? func = null,
            Expression<Func<TeacherUser, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<TeacherUser, object>>[] includes
            );
        TeacherUser GetTeacher(Func<TeacherUser,bool>? func = null);
        Task UpdatePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
