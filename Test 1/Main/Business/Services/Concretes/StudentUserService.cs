using Business.DTOs.Admin;
using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Data.RepositoryConcretes;
using Main.DTOs.Admin;
using Main.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class StudentUserService : IStudentUserService
    {
        UserManager<AppUser> _userManager;
        IStudentUserRepositroy _studentUserRepositroy;
        SignInManager<AppUser> _signInManager;


        public StudentUserService(IStudentUserRepositroy studentUserRepositroy, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _studentUserRepositroy = studentUserRepositroy;
            _signInManager = signInManager;
        }

        public async Task Add(StudentCreateDto student)
        {
            StudentUser user = new StudentUser()
            {
                Email = student.Email,
                UserName = student.UserName,
                Name = student.Name,
                Surname = student.Surname,
                GroupId = student.GroupId,
                Born = student.Born
            };

            var result = await _userManager.CreateAsync(user, student.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new StudentUserNotCreatedException($"Student not created! Errors: {errors}");
            }

            await _userManager.AddToRoleAsync(user, RolesEnum.Student.ToString());
        }

        public async Task Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null) 
            {
                throw new StudentUserNotFoundException("Student not Found!");
            }
            await _userManager.DeleteAsync(user);
        }

        public StudentUser Get(Func<StudentUser, bool>? func = null)
        {
            return _studentUserRepositroy.Get(func);
        }

        public List<StudentUser> GetAll(Func<StudentUser, bool>? func = null)
        {
            return _studentUserRepositroy.GetAll(func);
        }

        public List<StudentUser> GetAllStudentWithGroup(Func<StudentUser,bool>? func = null)
        {
            return _studentUserRepositroy.GetAllStudentWithGroup(func);
        }

        public async Task Update(string id, StudentUser student)
        {
            var user = Get(x=>x.Id == id);
            if (user == null)
            {
                throw new StudentUserNotFoundException("Student not Found!");
            }

            user.Name = student.Name;
            user.Email = student.Email;
            user.UserName = student.UserName;
            user.GroupId = student.GroupId;
            user.Born = student.Born;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user");
            }
            
        }


        public async Task UpdatePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new StudentUserNotFoundException("Student not Found!");
            }

            var resultCheck = await _signInManager.CheckPasswordSignInAsync(user, currentPassword, false);

            if(!resultCheck.Succeeded)
            {
                throw new PasswordOrUserNameNotValidException("Current Password is not valid!");
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Password change failed! Errors: {errors}");
            }
        }

    }
}
