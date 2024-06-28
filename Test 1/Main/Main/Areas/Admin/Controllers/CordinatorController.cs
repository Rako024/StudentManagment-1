using Business.DTOs.Cordinator;
using Business.Exceptions.Cordinator;
using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CordinatorController : Controller
	{
		ICordinatorUserService _userService;

		public CordinatorController(ICordinatorUserService userService)
		{
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			List<CordinatorUser> users = await _userService.GetAllCordinators();
			return View(users);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CordinatorCreateDto cordinator)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				await _userService.CreateCordinator(cordinator);
			}
			catch (CordinatorUserNotCreatedException)
			{
				return View("Error");
			}catch(Exception ) { return View("Error"); }
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(string id)
		{
			try
			{
				await _userService.DeleteCordinator(id);
			}
			catch (CordinatorUserNotFoundException)
			{
				return View("Error");
			}
			catch (Exception)
			{
				return View("Error");
			}
			return RedirectToAction(nameof(Index));
		}

		public ActionResult Update(string id)
		{
			var result = _userService.GetCordinator(x => x.Id == id);
			if (result == null)
			{
				return View("Error");
			}
			CordinatorUpdateDto cordinator = new CordinatorUpdateDto
			{
				Id = id,
				Name = result.Name,
				Surname = result.Surname,
				Email = result.Email,
				UserName = result.UserName,
				Born = result.Born
			};
			return View(cordinator);
		}

		[HttpPost]
		public async Task<IActionResult> Update(CordinatorUpdateDto cordinatorUpdateDto)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				await _userService.UpdateCordinator(cordinatorUpdateDto.Id, cordinatorUpdateDto);
			}
			catch (CordinatorUserNotFoundException)
			{
				return View("Error");
			}
			catch (InvalidCastException)
			{
				return View("Error");
			}
			catch (Exception)
			{
				return View("Error");
			}

			return RedirectToAction(nameof(Index));
		}

		public IActionResult UpdatePassword(string id)
		{
			var user = _userService.GetCordinator(x => x.Id == id);
			if (user == null)
			{
				return View("Error");
			}
			UpdatePasswordDto passwordDto = new UpdatePasswordDto { Id = id };
			return View(passwordDto);
		}

		[HttpPost]
		public async Task<IActionResult> UpdatePassword(UpdatePasswordDto user)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			try
			{
				await _userService.UpdatePasswordAsync(user.Id, user.CurrentPassword, user.NewPassword);
			}
			catch (CordinatorUserNotFoundException)
			{
				return View("Error");
			}
			catch (PasswordOrUserNameNotValidException ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
			catch (Exception)
			{
				return View("Error");
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
