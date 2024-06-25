using Business.Exceptions.Lesson;
using Business.Exceptions.LessonTime;
using Business.Services.Abstracts;
using Core.Helper;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class LessonTimeService : ILessonTimeService
    {
        ILessonTimeRepository _lessonTimeRepository;
        ILessonService _lessonService;
        public LessonTimeService(ILessonTimeRepository lessonTimeRepository, ILessonService lessonService)
        {
            _lessonTimeRepository = lessonTimeRepository;
            _lessonService = lessonService;
        }

        public void CreateLessonTime(LessonTime lessonTime)
        {
            Lesson lesson = _lessonService.GetLesson(x => x.Id == lessonTime.LessonId);
            if (lesson == null)
            {
                throw new LessonNotFoundException();
            }
            lessonTime.EndDate = lessonTime.Date.AddMinutes(95);
            _lessonTimeRepository.Add(lessonTime);
            _lessonTimeRepository.Commit();
        }

        public void DeleteLessonTime(int id)
        {
            LessonTime lessonTime = _lessonTimeRepository.Get(x => x.Id == id);
            if (lessonTime == null)
            {
                throw new LessonTimeNotFoundException();
            }
            _lessonTimeRepository.Remove(lessonTime);
            _lessonTimeRepository.Commit();
        }
        public void SoftDeleteLessonTime(int id)
        {
            LessonTime lessonTime = _lessonTimeRepository.Get(x => x.Id == id);
            if (lessonTime == null)
            {
                throw new LessonTimeNotFoundException();
            }
            lessonTime.IsDeleted = true;
            _lessonTimeRepository.Commit();
        }
        public async Task<List<LessonTime>> GetAllLessonTimes(Expression<Func<LessonTime, bool>>? func = null,
             Expression<Func<LessonTime, object>>? orderBy = null,
             bool isOrderByDesting = false,
             params Expression<Func<LessonTime, object>>[] includes)
        {
            IQueryable<LessonTime> queryable = await _lessonTimeRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            return await queryable.ToListAsync();
        }

        public List<LessonTime> GetLessonsWithLessonWithTeacherAndGroup(Func<LessonTime, bool>? func = null)
        {
            return _lessonTimeRepository.GetLessonsWithLessonWithTeacherAndGroup(func);
        }

        public LessonTime GetLessonTime(Func<LessonTime, bool>? func = null)
        {
            return _lessonTimeRepository.Get(func);
        }
        public async Task<LessonTime> GetLessonTimeAsync(Expression<Func<LessonTime, bool>> func)
        {
            return await _lessonTimeRepository.GetAsync(func);
        }
        public void UpdateLessonTime(int id, LessonTime lessonTime)
        {
            LessonTime oldLessonTime = _lessonTimeRepository.Get(x => x.Id == id);
            if (oldLessonTime == null)
            {
                throw new LessonTimeNotFoundException();
            }
            Lesson lesson = _lessonService.GetLesson(x => x.Id == lessonTime.LessonId);
            if (lesson == null)
            {
                throw new LessonNotFoundException();
            }
            oldLessonTime.LessonId = lessonTime.LessonId;
            oldLessonTime.Date = lessonTime.Date;
            _lessonTimeRepository.Commit();
        }




       public async Task<PageResult<LessonTime>> GetPagedLessonTimes(
       int pageNumber,
       int pageSize,
       Expression<Func<LessonTime, bool>>? filter = null,
       Expression<Func<LessonTime, object>>? orderBy = null,
       bool isOrderByDescending = false,
       params Expression<Func<LessonTime, object>>[] includes)
        {
            IQueryable<LessonTime> query = await _lessonTimeRepository.GetAll(filter, orderBy, isOrderByDescending, includes);

            int totalCount = query.Count();

            List<LessonTime> items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<LessonTime>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
