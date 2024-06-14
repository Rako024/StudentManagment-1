using Business.Exceptions;
using Business.Exceptions.Lesson;
using Business.Services.Abstracts;
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
    public class LessonService : ILessonService
    {
        ILessonRepository _lessonRepository;
        IGroupService _groupService;
        ITeacherUserService _userService;

        public LessonService(ILessonRepository lessonRepository, IGroupService groupService, ITeacherUserService userService)
        {
            _lessonRepository = lessonRepository;
            _groupService = groupService;
            _userService = userService;
        }

        public void CreateLesson(Lesson lesson)
        {
            var Teacher = _userService.GetTeacher(x=>x.Id == lesson.TeacherUserId);
            if(Teacher == null)
            {
                throw new TeacherUserNotFoundException("Teacher Not Found!");
            }
            var Group = _groupService.GetGroup(x=>x.Id == lesson.GroupId);
            if(Group == null)
            {
                throw new GroupNotFoundException("","Group Not Found!");
            }
            _lessonRepository.Add(lesson);
            _lessonRepository.Commit();
        }

        public void DeleteLesson(int id)
        {
            var lesson = _lessonRepository.Get(x=>x.Id == id);
            if(lesson == null)
            {
                throw new LessonNotFoundException();
            }
            _lessonRepository.Remove(lesson);
            _lessonRepository.Commit();
        }

        public List<Lesson> GetAllLessinsWithGroupAndTeacherUser(Func<Lesson, bool>? func = null)
        {
            return _lessonRepository.GetAllLessonsWithGroupAndTeacherUser(func);
        }

        public async Task<List<Lesson>> GetAllLessons
            (
            Expression<Func<Lesson, bool>>? func = null,
            Expression<Func<Lesson, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<Lesson, object>>[] includes
            )
        {
            var queryable = await _lessonRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            return await queryable.ToListAsync();
        }

        public Lesson GetLesson(Func<Lesson, bool>? func = null)
        {
            return _lessonRepository.Get(func);
        }

        public Lesson GetLessonsWithGroupAndTeacherUser(Func<Lesson, bool>? func = null)
        {
            return _lessonRepository.GetLessonsWithGroupAndTeacherUser(func);
        }

        public void SoftDeleteLesson(int id)
        {
            var lesson = _lessonRepository.Get(x => x.Id == id);
            if (lesson == null)
            {
                throw new LessonNotFoundException();
            }
            lesson.IsDeleted = true;
            _lessonRepository.Commit();
        }

        public void UpdateLesson(int id, Lesson lesson)
        {
            var oldLesson = _lessonRepository.Get(x => x.Id == id);
            if (oldLesson == null)
            {
                throw new LessonNotFoundException();
            }
            oldLesson.Name = lesson.Name;
            oldLesson.GroupId = lesson.GroupId;
            oldLesson.TeacherUserId = lesson.TeacherUserId;
            oldLesson.LessonCount = lesson.LessonCount;
            oldLesson.Semester = lesson.Semester;
            oldLesson.Year = lesson.Year;
            oldLesson.Credit = lesson.Credit;
            _lessonRepository.Commit();
        }
    }
}
