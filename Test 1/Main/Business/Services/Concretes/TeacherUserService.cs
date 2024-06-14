using Business.DTOs.Admin;
using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Main.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class TeacherUserService : ITeacherUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITeacherUserRepository _userRepository;
        readonly SignInManager<AppUser> _signInManager;
        public TeacherUserService(UserManager<AppUser> userManager, ITeacherUserRepository userRepository, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task CreateTeacher(TeacherCreateDto dto)
        {
            TeacherUser user = new TeacherUser()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                UserName = dto.UserName,
                Kafedra = dto.Kafedra,
                Born = dto.Born,
            };
            var result = await _userManager.CreateAsync(user,dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new TeacherUserNotCreatedException($"Teacher not created! Errors: {errors}");
            }
            await _userManager.AddToRoleAsync(user, RolesEnum.Teacher.ToString());
        }

        public async Task DeleteTeacher(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new TeacherUserNotFoundException("Teacher not Found!");
            }
            await _userManager.DeleteAsync(user);
        }

        public async Task<List<TeacherUser>> GetAllTeachers
            (
            Expression<Func<TeacherUser, bool>>? func = null,
            Expression<Func<TeacherUser, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<TeacherUser, object>>[] includes
            )
        {
            var queryable = await _userRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            return await queryable.ToListAsync();
        }

        public TeacherUser GetTeacher(Func<TeacherUser, bool>? func = null)
        {
            return _userRepository.Get(func);
        }

        public async Task UpdatePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new TeacherUserNotFoundException("Teacher not Found!");
            }

            var resultCheck = await _signInManager.CheckPasswordSignInAsync(user, currentPassword, false);

            if (!resultCheck.Succeeded)
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

        public async Task UpdateTeacher(string id, TeacherUpdateDto dto)
        {
            var result = await _userManager.FindByIdAsync(id);
            if(result == null)
            {
                throw new TeacherUserNotFoundException("Teacher not foud!");
            }
            var teacherUser = result as TeacherUser;
            if (teacherUser == null)
            {
                throw new InvalidCastException("Error!");
            }
            teacherUser.Name = dto.Name;
            teacherUser.Email = dto.Email;
            teacherUser.UserName = dto.UserName;
            teacherUser.Surname = dto.Surname;
            teacherUser.Born = dto.Born;
            teacherUser.Kafedra = dto.Kafedra;
            var Succeeded = await _userManager.UpdateAsync(teacherUser);
            if (!Succeeded.Succeeded)
            {
                throw new Exception("Update Time Error!");
            }

        }
    }
}
