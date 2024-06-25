using Business.DTOs.Account;
using Core.Models;
using Main.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers;

public class AccountController : Controller
{
    UserManager<AppUser> _userManager;
    RoleManager<IdentityRole> _roleManager;
    SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

   public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return View(loginDto);
        }
        AppUser user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
        }

        if(user == null)
        {
            ModelState.AddModelError("", "Username or Password is not valid");
            return View();
        }
        if(!user.LockoutEnabled)
        {
            ModelState.AddModelError("", "You Are Blocked!");
            return View();
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Username or Password is not valid");
            return View();
        }
        var signInResult = await _signInManager.PasswordSignInAsync(user,loginDto.Password,loginDto.RememberMe,false);
        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError("", "Username or Password is not valid");
            return View();
        }
        IList<string> roleList = await _userManager.GetRolesAsync(user);
        string role = roleList.FirstOrDefault()?.ToString();
        if (role == RolesEnum.Teacher.ToString())
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Teacher", id = user.Id });
        }else if (role == RolesEnum.Student.ToString()) 
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Student", id = user.Id });
        }
        else if (role == RolesEnum.Cordinator.ToString() || role == RolesEnum.SuperAdmin.ToString() || role == RolesEnum.Admin.ToString())
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin", id = user.Id });
        }
        else
        {
            return View();
        }
        
    }

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    //public async Task<IActionResult> CreateRoles()
    //{
    //    IdentityRole role1 = new IdentityRole(RolesEnum.Admin.ToString());
    //    IdentityRole role2 = new IdentityRole(RolesEnum.Cordinator.ToString());
    //    IdentityRole role3 = new IdentityRole(RolesEnum.Teacher.ToString());
    //    IdentityRole role4 = new IdentityRole(RolesEnum.Student.ToString());

    //    await _roleManager.CreateAsync(role1);
    //    await _roleManager.CreateAsync(role2);
    //    await _roleManager.CreateAsync(role3);
    //    await _roleManager.CreateAsync(role4);
    //    return Ok("salam exi");
    //}
    //public async Task<IActionResult> CreateAdmin()
    //{
    //    AppUser user = new AppUser() 
    //    {
    //        UserName="Admin",
    //        Email = "admin@gmail.com",
    //        Name = "admin",
    //        Surname = "admin",
    //    };
    //    var result = await _userManager.CreateAsync(user,"Salam123@");
    //    if(result.Succeeded)
    //    {
    //        var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
    //        if(!roleResult.Succeeded)
    //        {
    //            return BadRequest(roleResult.Errors);
    //        }
    //        return Ok("Yarandi");
    //    }
    //    else
    //    {
    //        return BadRequest(result.Errors);
    //    }
    //}
}
