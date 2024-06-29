using Business.DTOs.Teacher.LearningMaterials;
using Business.Exceptions;
using Business.Exceptions.File;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Main.Areas.Teacher.Controllers
{
    [Area("Teacher"),Authorize(Roles ="Teacher")]
    public class LearningMaterialController : Controller
    {
        ILearningMaterialService _learningMaterialService;
        ILessonService _lessonService;
        public LearningMaterialController(ILearningMaterialService learningMaterialService, ILessonService lessonService)
        {
            _learningMaterialService = learningMaterialService;
            _lessonService = lessonService;
        }

        public async Task<IActionResult> Index(int lessonId)
        {
            Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x=>x.Id == lessonId);
            if(lesson == null)
            {
                return View("Error");
            }
            List<LearningMaterial> learningMaterials = await _learningMaterialService.GetAllLearningMaterials
                (
                x=>x.LessonId == lessonId && x.IsDeleted == false,
                null,
                false,
                x=>x.Lesson,
                x=>x.Lesson.Group
                );
            MaterialPageDto dto = new MaterialPageDto
            {
                LearningMaterials = learningMaterials,
                Lesson = lesson,
                LessonId = lessonId
            };
            return View(dto);
        }

        public async Task<IActionResult> Create(int lessonId)
        {
            Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x=>x.Id==lessonId);
            if(lesson == null)
            {
                return View("Error");
            }
            CreateLearningMaterialDto dto = new CreateLearningMaterialDto
            {
                LessonId = lessonId
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLearningMaterialDto dto)
        {

            Lesson lesson = _lessonService.GetLessonsWithGroupAndTeacherUser(x => x.Id == dto.LessonId);
            if (lesson == null)
            {
                return View("Error");
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _learningMaterialService.CreateLearningMaterial(dto);
            }catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName,ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index), new { lessonId = dto.LessonId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var material = await _learningMaterialService.GetLearningMaterial(x => x.Id == id);
            if(material == null)
            {
                return View("Error");
            }
            return View(material);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var existMaterial = await _learningMaterialService.GetLearningMaterial(x => x.Id == id);
            if(existMaterial == null)
            {
                return View("Error");
            }
            CreateLearningMaterialDto dto = new CreateLearningMaterialDto()
            {
                Id = id,
                LessonId = existMaterial.LessonId,
                Name = existMaterial.Name,
                Link = existMaterial.Link,
                FileUrl = existMaterial.FileUrl,
            };
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateLearningMaterialDto dto)
        {
            var existMaterial = await _learningMaterialService.GetLearningMaterial(x => x.Id == dto.Id);
            if (existMaterial == null)
            {
                return View("Error");
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _learningMaterialService.UpdateLearningMaterial((int)dto.Id, dto);
            }catch(GlobalException ex)
            {
                ModelState.AddModelError(ex.ProperyName, ex.Message);
                return View(dto);
            }catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View(dto);
            }
            catch(Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction("Index", new {lessonId = dto.LessonId});
        }
        public async Task<IActionResult> Delete(int id)
        {
            var material = await _learningMaterialService.GetLearningMaterial(x => x.Id == id);
            if (material == null)
            {
                return View("Error");
            }
            try
            {
                await _learningMaterialService.DeleteLearningMaterial(id);
            }catch(GlobalException ex)
            {
                return View("Error");
            }
            return RedirectToAction("Index", new { lessonId = material.LessonId});
        }
    }
}
