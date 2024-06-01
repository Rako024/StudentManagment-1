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

    public IActionResult Register()
    {
        return View();
    }

    public async Task<IActionResult> CreateRoles()
    {
        IdentityRole role1 = new IdentityRole(RolesEnum.Admin.ToString());
        IdentityRole role2 = new IdentityRole(RolesEnum.Cordinator.ToString());
        IdentityRole role3 = new IdentityRole(RolesEnum.Teacher.ToString());
        IdentityRole role4 = new IdentityRole(RolesEnum.Student.ToString());

        await _roleManager.CreateAsync(role1);
        await _roleManager.CreateAsync(role2);
        await _roleManager.CreateAsync(role3);
        await _roleManager.CreateAsync(role4);
        return Ok("salam exi");
    }
}
