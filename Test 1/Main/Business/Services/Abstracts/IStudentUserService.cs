using Business.DTOs.Admin;
using Core.Models;
using Main.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IStudentUserService
    {
        Task Add(StudentCreateDto student);
        Task Delete(string id);
        Task Update(string id, StudentUser student);
        StudentUser Get(Func<StudentUser, bool>? func=null);
        Task<List<StudentUser>> GetAll(Expression<Func<StudentUser, bool>>? func = null,
             Expression<Func<StudentUser, object>>? orderBy = null,
             bool isOrderByDesting = false,
            params Expression<Func<StudentUser, object>>[] includes);
        List<StudentUser> GetAllStudentWithGroup(Func<StudentUser, bool>? func = null);
        Task UpdatePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
