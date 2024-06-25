using Business.DTOs.Admin;
using Business.Exceptions;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Cordinator")]
public class TeacherController : Controller
{
    ITeacherUserService _userService;

    public TeacherController(ITeacherUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        List<TeacherUser> users = await _userService.GetAllTeachers();
        return View(users);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(TeacherCreateDto teacher)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        try
        {
            await _userService.CreateTeacher(teacher);
        }catch (TeacherUserNotCreatedException ex) 
        {
            return View("Error");
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _userService.DeleteTeacher(id);
        }
        catch (TeacherUserNotFoundException)
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
        var result = _userService.GetTeacher(x=>x.Id == id);
        if(result == null)
        {
            return View("Error");
        }
        TeacherUpdateDto teacher = new TeacherUpdateDto();
        teacher.Id = id;
        teacher.Name = result.Name;
        teacher.Surname = result.Surname;
        teacher.Email = result.Email;
        teacher.UserName = result.UserName;
        teacher.Kafedra = result.Kafedra;
        teacher.Born = result.Born;
        return View(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Update(TeacherUpdateDto teacherUpdateDto)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }

        try
        {
            await _userService.UpdateTeacher(teacherUpdateDto.Id, teacherUpdateDto);
        }catch(TeacherUserNotFoundException)
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
        var user = _userService.GetTeacher(x=>x.Id == id);
        if (user == null)
        {
            return View("Error");
        }
        TeacherUpdatePasswordDto student = new TeacherUpdatePasswordDto();
        student.Id = id;
        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePassword(TeacherUpdatePasswordDto user)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        try
        {

            await _userService.UpdatePasswordAsync(user.Id, user.CurrentPassword, user.NewPassword);
        }
        catch (TeacherUserNotFoundException ex)
        {
            return View("Error");
        }
        catch (PasswordOrUserNameNotValidException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
        catch (Exception ex)
        {
            return View("Error");
        }
        return RedirectToAction(nameof(Index));
    }
}
