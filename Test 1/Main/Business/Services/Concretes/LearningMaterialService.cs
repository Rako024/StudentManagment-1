using Business.DTOs.Teacher.LearningMaterials;
using Business.Exceptions;
using Business.Exceptions.File;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Data.RepositoryConcretes;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class LearningMaterialService : ILearningMaterialService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILearningMaterialRepository _learningMaterialRepository;

        public LearningMaterialService(IWebHostEnvironment webHostEnvironment, ILearningMaterialRepository learningMaterialRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _learningMaterialRepository = learningMaterialRepository;
        }

        public async Task CreateLearningMaterial(CreateLearningMaterialDto dto)
        {
            LearningMaterial learningMaterial = new LearningMaterial
            {
                Name = dto.Name,
                LessonId = dto.LessonId,

            };

            if (dto.Link != null)
            {
                learningMaterial.Link = dto.Link;
            }

            if (dto.File != null)
            {

                if (!dto.File.ContentType.Contains("image/") &&
                !dto.File.ContentType.Contains("application/pdf") &&
                !dto.File.ContentType.Contains("text/") &&
                !dto.File.ContentType.Contains("application/msword") &&
                !dto.File.ContentType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document") &&
                !dto.File.ContentType.Contains("application/vnd.ms-excel") &&
                !dto.File.ContentType.Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") &&
                !dto.File.ContentType.Contains("application/vnd.ms-powerpoint") &&
                !dto.File.ContentType.Contains("application/vnd.openxmlformats-officedocument.presentationml.presentation"))
                {
                    throw new ContentTypeException("File", " File is not correct file type!");
                }
                var fileExtension = Path.GetExtension(dto.File.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "learningmaterials");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(fileStream);
                }

                learningMaterial.FileUrl = uniqueFileName;
            }

            _learningMaterialRepository.Add(learningMaterial);
            _learningMaterialRepository.Commit();
        }

        public async Task DeleteLearningMaterial(int id)
        {
            var submission = await _learningMaterialRepository.GetAsync(x => x.Id == id);
            if (submission == null)
            {
                throw new GlobalException("LearningMaterial", "Learning Material not found");
            }
            if (!string.IsNullOrEmpty(submission.FileUrl))
            {
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "learningmaterials");
                var filePath = Path.Combine(uploadPath, submission.FileUrl);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            _learningMaterialRepository.Remove(submission);
            _learningMaterialRepository.Commit();
        }

        public async Task<List<LearningMaterial>> GetAllLearningMaterials(Expression<Func<LearningMaterial, bool>>? func = null, Expression<Func<LearningMaterial, object>>? orderBy = null, bool isOrderByDescending = false, params Expression<Func<LearningMaterial, object>>[] includes)
        {
            IQueryable<LearningMaterial> learningMaterials = await _learningMaterialRepository.GetAll(func,orderBy,isOrderByDescending, includes);
            return learningMaterials.ToList();
        }

        public async Task<LearningMaterial> GetLearningMaterial(Expression<Func<LearningMaterial, bool>>? func = null)
        {
            return await _learningMaterialRepository.GetAsync(func);
        }

        public async Task UpdateLearningMaterial(int id, CreateLearningMaterialDto dto)
        {
            var existingSubmission = await _learningMaterialRepository.GetAsync(x => x.Id == id);
            if (existingSubmission == null)
            {
                throw new GlobalException("LearningMaterial", "Learning Material not found");
            }

            existingSubmission.Name = dto.Name;

            if (dto.Link != null)
            {
                existingSubmission.Link = dto.Link;
            }
            if (dto.File != null)
            {

                if (!dto.File.ContentType.Contains("image/") &&
                !dto.File.ContentType.Contains("application/pdf") &&
                !dto.File.ContentType.Contains("text/") &&
                !dto.File.ContentType.Contains("application/msword") &&
                !dto.File.ContentType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document") &&
                !dto.File.ContentType.Contains("application/vnd.ms-excel") &&
                !dto.File.ContentType.Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") &&
                !dto.File.ContentType.Contains("application/vnd.ms-powerpoint") &&
                !dto.File.ContentType.Contains("application/vnd.openxmlformats-officedocument.presentationml.presentation"))
                {
                    throw new ContentTypeException("File", " File is not correct file type!");
                }

                var fileExtension = Path.GetExtension(dto.File.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "learningmaterials");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(fileStream);
                }

                if (existingSubmission.FileUrl != null)
                {
                    var oldFilePath = Path.Combine(uploadPath, existingSubmission.FileUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                existingSubmission.FileUrl = uniqueFileName;
            }

            _learningMaterialRepository.Commit();
        }
    }
}
