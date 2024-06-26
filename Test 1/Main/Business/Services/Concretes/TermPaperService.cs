using Business.DTOs.Student.Files;
using Business.Exceptions;
using Business.Exceptions.File;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class TermPaperService : ITermPaperService
    {
        IWebHostEnvironment _webHostEnvironment;
        ITermPaperRepository _termPaperRepository;
        public TermPaperService(IWebHostEnvironment webHostEnvironment, ITermPaperRepository termPaperRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _termPaperRepository = termPaperRepository;
        }

        public async Task CreateTermPaper(TermFileDto fileDto)
        {
            if (fileDto == null)
            {
                throw new GlobalException("", "Not found");
            }
            if (fileDto.TermPaperFile == null)
            {
                throw new ArgumentNotFoundException("TermPaperFile", "File is not found!");
            }
            if (!fileDto.TermPaperFile.ContentType.Contains("image/") &&
                !fileDto.TermPaperFile.ContentType.Contains("application/pdf") &&
                !fileDto.TermPaperFile.ContentType.Contains("text/") &&
                !fileDto.TermPaperFile.ContentType.Contains("application/msword") &&
                !fileDto.TermPaperFile.ContentType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document") &&
                !fileDto.TermPaperFile.ContentType.Contains("application/vnd.ms-excel") &&
                !fileDto.TermPaperFile.ContentType.Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
            {
                throw new ContentTypeException("TermPaperFile", "Termpaper File is not correct file type!");
            }

            var fileExtension = Path.GetExtension(fileDto.TermPaperFile.FileName);
            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

            
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "termpapers");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileDto.TermPaperFile.CopyToAsync(fileStream);
            }

            
            var termPaper = new TermPaper
            {
                Name = fileDto.Name,
                StudentUserId = fileDto.StudentUserId,
                LessonId = fileDto.LessonId,
                Description = fileDto.Description,
                FileUrl = uniqueFileName
            };

            
            _termPaperRepository.Add(termPaper);
            _termPaperRepository.Commit();
        }


        public Task DeleteTermPaper(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TermPaper>> GetAllTermPapers(Expression<Func<TermPaper, bool>>? func = null, Expression<Func<TermPaper, object>>? orderBy = null, bool isOrderByDesting = false, params Expression<Func<TermPaper, object>>[] includes)
        {
            IQueryable<TermPaper> papers = await _termPaperRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            return papers.ToList();    
        }

        public async Task<TermPaper> GetTermPaper(Func<TermPaper, bool>? func = null)
        {
            return  _termPaperRepository.Get(func);
        }

        public Task SoftDeleteTermPaper(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTermPaper(int id, TermFileUpdateDto updatedFileDto)
        {
            var existingTermPaper = _termPaperRepository.Get(x=>x.Id == id);
            if (existingTermPaper == null)
            {
                throw new GlobalException("TermPaper", "Term paper not found");
            }

            if (updatedFileDto.TermPaperFile != null)
            {
                if (!updatedFileDto.TermPaperFile.ContentType.Contains("image/") &&
                    !updatedFileDto.TermPaperFile.ContentType.Contains("application/pdf") &&
                    !updatedFileDto.TermPaperFile.ContentType.Contains("text/") &&
                    !updatedFileDto.TermPaperFile.ContentType.Contains("application/msword") &&
                    !updatedFileDto.TermPaperFile.ContentType.Contains("application/vnd.openxmlformats-officedocument.wordprocessingml.document") &&
                    !updatedFileDto.TermPaperFile.ContentType.Contains("application/vnd.ms-excel") &&
                    !updatedFileDto.TermPaperFile.ContentType.Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                {
                    throw new ContentTypeException("TermPaperFile", "Term paper file is not correct file type!");
                }

                var fileExtension = Path.GetExtension(updatedFileDto.TermPaperFile.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "termpapers");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await updatedFileDto.TermPaperFile.CopyToAsync(fileStream);
                }

                // kohne fillari silme
                var oldFilePath = Path.Combine(uploadPath, existingTermPaper.FileUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                existingTermPaper.FileUrl = uniqueFileName;
            }

            existingTermPaper.Name = updatedFileDto.Name;
            existingTermPaper.StudentUserId = updatedFileDto.StudentUserId;
            existingTermPaper.LessonId = updatedFileDto.LessonId;
            existingTermPaper.Description = updatedFileDto.Description;

            
            _termPaperRepository.Commit();
        }

    }
}
