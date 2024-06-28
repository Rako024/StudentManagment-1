using Business.DTOs.Teacher.Homeworks;
using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class HomeworkSubmissionService : IHomeworkSubmissionService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHomeworkSubmissionRepository _homeworkSubmissionRepository;

        public HomeworkSubmissionService(IWebHostEnvironment webHostEnvironment, IHomeworkSubmissionRepository homeworkSubmissionRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _homeworkSubmissionRepository = homeworkSubmissionRepository;
        }

        public async Task CreateHomeworkSubmission(HomeworkSubmissionDto submissionDto)
        {
            var homeworkSubmission = new HomeworkSubmission
            {
                HomeworkId = submissionDto.HomeworkId,
                StudentUserId = submissionDto.StudentUserId,
                Description = submissionDto.Description
            };

            if (submissionDto.Link != null)
            {
                homeworkSubmission.Link = submissionDto.Link;
            }

             if (submissionDto.File != null)
            {
                var fileExtension = Path.GetExtension(submissionDto.File.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "homework");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await submissionDto.File.CopyToAsync(fileStream);
                }

                homeworkSubmission.FileUrl = uniqueFileName;
            }

            _homeworkSubmissionRepository.Add(homeworkSubmission);
            _homeworkSubmissionRepository.Commit();
        }

        public async Task UpdateHomeworkSubmission(int id, HomeworkSubmissionDto submissionDto)
        {
            var existingSubmission = await _homeworkSubmissionRepository.GetAsync(x => x.Id == id);
            if (existingSubmission == null)
            {
                throw new GlobalException("HomeworkSubmission", "Homework submission not found");
            }

            existingSubmission.Description = submissionDto.Description;

            if (submissionDto.Link != null)
            {
                existingSubmission.Link = submissionDto.Link;
            }
            if (submissionDto.File != null)
            {
                var fileExtension = Path.GetExtension(submissionDto.File.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "homework");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await submissionDto.File.CopyToAsync(fileStream);
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

            _homeworkSubmissionRepository.Commit();
        }

        public async Task DeleteHomeworkSubmission(int id)
        {
            var submission = await _homeworkSubmissionRepository.GetAsync(x => x.Id == id);
            if (submission == null)
            {
                throw new GlobalException("HomeworkSubmission", "Homework submission not found");
            }
            if (!string.IsNullOrEmpty(submission.FileUrl))
            {
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "homework");
                var filePath = Path.Combine(uploadPath, submission.FileUrl);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            _homeworkSubmissionRepository.Remove(submission);
            _homeworkSubmissionRepository.Commit();
        }

        public async Task<HomeworkSubmission> GetHomeworkSubmission(Expression<Func<HomeworkSubmission, bool>>? func = null)
        {
            return await _homeworkSubmissionRepository.GetAsync(func);
        }

        public async Task<List<HomeworkSubmission>> GetAllHomeworkSubmissions(Expression<Func<HomeworkSubmission, bool>>? func = null, Expression<Func<HomeworkSubmission, object>>? orderBy = null, bool isOrderByDescending = false, params Expression<Func<HomeworkSubmission, object>>[] includes)
        {
            IQueryable<HomeworkSubmission> quaryableSubmissions = await _homeworkSubmissionRepository.GetAll(func, orderBy, isOrderByDescending, includes);
            return quaryableSubmissions.ToList();
        }
    }
}
