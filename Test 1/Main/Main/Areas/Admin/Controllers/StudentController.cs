﻿using Business.DTOs.Admin;
using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Main.DTOs.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers;

[Area("Admin")]
public class StudentController : Controller
{
    UserManager<AppUser> _userManager;
    IStudentUserService _studentUserService;
    IGroupService _groupService;

    public StudentController(UserManager<AppUser> userManager, IStudentUserService studentUserService, IGroupService groupService)
    {
        _userManager = userManager;
        _studentUserService = studentUserService;
        _groupService = groupService;
    }

    public IActionResult Index()
    {
        List<StudentUser> students = _studentUserService.GetAllStudentWithGroup();
        return View(students);
    }

    public IActionResult Create()
    {
        ViewBag.Groups = _groupService.GetAllGroup();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudentCreateDto student)
    {
        
        if(!ModelState.IsValid)
        {
            ViewBag.Groups = _groupService.GetAllGroup();
            return View();
        }
        try
        {
            await _studentUserService.Add(student);
        }catch(StudentUserNotCreatedException ex)
        {
            ModelState.AddModelError("",ex.Message);
            return View();
        }
        catch (Exception)
        {
            return View("Error");
        }

        
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(string id)
    {
        try
        {
          await _studentUserService.Delete(id);

        }catch(StudentUserNotFoundException ex)
        {
            return View("Error");
        }catch(Exception)
        {
            return View("Error");
        }
        return RedirectToAction(nameof(Index));
    }


    public IActionResult Update(string id)
    {
        ViewBag.Groups = _groupService.GetAllGroup();
        var result =  _studentUserService.Get(x=>x.Id == id);
        if(result == null)
        {
            return View("Error");
        }
        
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(StudentUser student)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Groups = _groupService.GetAllGroup();
            return View();
        }

        try
        {
            await _studentUserService.Update(student.Id, student);
        }catch (StudentUserNotFoundException)
        {
            return View("Error");
        }
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> UpdatePassword(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
       if (user == null)
        {
            return View("Error");
        }
       StudentUpdatePasswordDto student = new StudentUpdatePasswordDto();
        student.Id = id;
        return View(student);
    }
    [HttpPost]
    public async Task<IActionResult> UpdatePassword(StudentUpdatePasswordDto user)
    {
        if(!ModelState.IsValid)
        {
            return View();
        }
        try
        {

            await _studentUserService.UpdatePasswordAsync(user.Id, user.CurrentPassword, user.NewPassword);
        }catch(StudentUserNotFoundException ex)
        {
            return View("Error");
        }
        catch (PasswordOrUserNameNotValidException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }catch(Exception ex)
        {
            return View("Error");
        }
        return RedirectToAction(nameof(Index));
    }
}