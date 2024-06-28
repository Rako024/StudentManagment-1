using Business.DTOs.Cordinator;
using Business.Exceptions;
using Business.Exceptions.Cordinator;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Main.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
	public class CordinatorUserService : ICordinatorUserService
	{
		readonly UserManager<AppUser> _userManager;
		readonly ICordinatorUserRepository _userRepository;
		readonly SignInManager<AppUser> _signInManager;

		public CordinatorUserService(UserManager<AppUser> userManager, ICordinatorUserRepository userRepository, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_userRepository = userRepository;
			_signInManager = signInManager;
		}

		public async Task CreateCordinator(CordinatorCreateDto dto)
		{
			CordinatorUser user = new CordinatorUser()
			{
				Name = dto.Name,
				Surname = dto.Surname,
				Email = dto.Email,
				UserName = dto.UserName,
				Born = dto.Born,
			};
			var result = await _userManager.CreateAsync(user, dto.Password);
			if (!result.Succeeded)
			{
				var errors = string.Join(", ", result.Errors.Select(e => e.Description));
				throw new CordinatorUserNotCreatedException($"Cordinator not created! Errors: {errors}");
			}
			await _userManager.AddToRoleAsync(user, RolesEnum.Cordinator.ToString());
		}

		public async Task DeleteCordinator(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				throw new CordinatorUserNotFoundException("Cordinator not found!");
			}
			await _userManager.DeleteAsync(user);
		}

		public async Task<List<CordinatorUser>> GetAllCordinators(
			Expression<Func<CordinatorUser, bool>>? func = null,
			Expression<Func<CordinatorUser, object>>? orderBy = null,
			bool isOrderByDesting = false,
			params Expression<Func<CordinatorUser, object>>[] includes
		)
		{
			var queryable = await _userRepository.GetAll(func, orderBy, isOrderByDesting, includes);
			return queryable.ToList();
		}

		public CordinatorUser GetCordinator(Func<CordinatorUser, bool>? func = null)
		{
			return _userRepository.Get(func);
		}

		public async Task UpdatePasswordAsync(string userId, string currentPassword, string newPassword)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				throw new CordinatorUserNotFoundException("Cordinator not found!");
			}

			var resultCheck = await _signInManager.CheckPasswordSignInAsync(user, currentPassword, false);

			if (!resultCheck.Succeeded)
			{
				throw new PasswordOrUserNameNotValidException("Current password is not valid!");
			}

			var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
			if (!result.Succeeded)
			{
				var errors = string.Join(", ", result.Errors.Select(e => e.Description));
				throw new Exception($"Password change failed! Errors: {errors}");
			}
		}

		public async Task UpdateCordinator(string id, CordinatorUpdateDto dto)
		{
			var result = await _userManager.FindByIdAsync(id);
			if (result == null)
			{
				throw new CordinatorUserNotFoundException("Cordinator not found!");
			}
			var cordinatorUser = result as CordinatorUser;
			if (cordinatorUser == null)
			{
				throw new InvalidCastException("Error!");
			}
			cordinatorUser.Name = dto.Name;
			cordinatorUser.Email = dto.Email;
			cordinatorUser.UserName = dto.UserName;
			cordinatorUser.Surname = dto.Surname;
			cordinatorUser.Born = dto.Born;
			var Succeeded = await _userManager.UpdateAsync(cordinatorUser);
			if (!Succeeded.Succeeded)
			{
				throw new Exception("Update Time Error!");
			}
		}
	}
}
