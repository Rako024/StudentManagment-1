using Business.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.LayoutServices
{
    public class AdminLayoutService
    {
        ITeacherUserService _teacherUserService;
        IStudentUserService _studentUserService;
        IGroupService _groupService;
        ILessonService _lessonService;

        public AdminLayoutService(ITeacherUserService teacherUserService, IStudentUserService studentUserService, IGroupService groupService, ILessonService lessonService)
        {
            _teacherUserService = teacherUserService;
            _studentUserService = studentUserService;
            _groupService = groupService;
            _lessonService = lessonService;
        }

        public async Task<int> GetTeacherCountAsync() 
        {
            var list = await _teacherUserService.GetAllTeachers(x => x.IsDeleted == false);
            int count =  list.Count();
            return count;
        }

        public async Task<int> GetStudentCountAsync()
        {
            var list = await _studentUserService.GetAll(x => x.IsDeleted == false);
            int count = list.Count();
            return count;
        }
        public async Task<int> GetGroupCountAsync()
        {
            var list = await _groupService.GetAllGroup(x=>x.IsDeleted == false);
            int count = list.Count();
            return count;
        }
        public async Task<int> GetLessonCountAsync()
        {
            var list = await _lessonService.GetAllLessons(x=>x.IsDeleted == false);
            int count = list.Count();
            return count;
        }
    }
}
