using Business.Exceptions.Semester;
using Business.Services.Abstracts;
using Core.CoreEnums;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class SemesterController : Controller
    {
        ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        public async Task<IActionResult> Index()
        {
            List<Semester> semesters = await _semesterService.GetAllSemester(x=>x.IsDeleted ==false);
            ViewBag.AciveSemester = semesters.FirstOrDefault(x=>x.IsActive);
            return View(semesters);
        }

        //public async Task<IActionResult> CreateSemester()
        //{
        //    Semester sem1 = new Semester() 
        //    {
        //        Name= "Autumn",
        //        IsDeleted=false,
        //        IsActive=false,
        //    };
        //    Semester sem2 = new Semester()
        //    {
        //        Name = "Spring",
        //        IsDeleted = false,
        //        IsActive = true,
        //    };
        //    Semester sem3 = new Semester()
        //    {
        //        Name = "Summer",
        //        IsDeleted = false,
        //        IsActive = false,
        //    };

        //    await _semesterService.CreateSemester(sem1);
        //    await _semesterService.CreateSemester(sem2);
        //    await _semesterService.CreateSemester(sem3);
        //    return Ok("Success");
        //}
        [HttpPost]
        public async Task<IActionResult> SetActiveSemester(int id)
        {
            try {
                await _semesterService.SetActiveSemester(id);
            }catch (SemesterNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return RedirectToAction("Index"); ;
            }catch (Exception ex)
            {
                return View("Error");
            }
            
            return RedirectToAction("Index");
        }
    }
}
