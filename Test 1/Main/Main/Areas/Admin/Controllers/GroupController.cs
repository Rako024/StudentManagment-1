using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : Controller
    {
        IGroupService _groupService;
        IStudentUserService _studentUserService;

        public GroupController(IGroupService groupService, IStudentUserService studentUserService)
        {
            _groupService = groupService;
            _studentUserService = studentUserService;
        }

        public IActionResult Index()
        {
            List<Group> groups = _groupService.GetAllGroup();
            return View(groups);
        }

        public IActionResult Delete(int id) 
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _groupService.DeleteGroup(id);
            }catch(GroupNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View("Error");
            }catch (Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Group group) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _groupService.CreateGroup(group);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            Group group = _groupService.GetGroup(x=>x.Id == id);
            if(group == null)
            {
                return View("Error");
            }
            return View(group);
        }

        [HttpPost]
        public IActionResult Update(Group group)
        {
            if (!ModelState.IsValid)
            {
                return View() ; 
            }
            try
            {
                _groupService.UpdateGroup(group.Id, group);
            }catch(GroupNotFoundException ex)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            Group group = _groupService.GetGroup(x=>x.Id==id);
            if(group == null)
            {
                return View("Error");
            }
            List<StudentUser> students = _studentUserService.GetAll(x=>x.GroupId == id);
            return View(students);  
        }
    }
}
