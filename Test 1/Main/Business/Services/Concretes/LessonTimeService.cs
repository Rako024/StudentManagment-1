using Business.Exceptions.Lesson;
using Business.Exceptions.LessonTime;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<LessonTime> GetAllLessonTimes(Func<LessonTime, bool>? func = null)
        {
            return _lessonTimeRepository.GetAll(func);
        }

        public List<LessonTime> GetLessonsWithLessonWithTeacherAndGroup(Func<LessonTime, bool>? func = null)
        {
            return _lessonTimeRepository.GetLessonsWithLessonWithTeacherAndGroup(func);
        }

        public LessonTime GetLessonTime(Func<LessonTime, bool>? func = null)
        {
            return _lessonTimeRepository.Get(func);
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
    }
}
