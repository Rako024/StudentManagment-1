using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcretes
{
    public class LessonTimeRepository : GenericRepository<LessonTime>, ILessonTimeRepository
    {
        AppDbContext _dbContext;
        public LessonTimeRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<LessonTime> GetLessonsWithLessonWithTeacherAndGroup(Func<LessonTime, bool>? func = null)
        {
            return func == null ? _dbContext.LessonTimes.Include(x => x.Lesson).ThenInclude(x => x.Group)
                .Include(x => x.Lesson).ThenInclude(x => x.TeacherUser).ToList()
                :
                _dbContext.LessonTimes.Include(x => x.Lesson).ThenInclude(x => x.Group)
                .Include(x => x.Lesson).ThenInclude(x => x.TeacherUser).Where(func).ToList();
        }
    }
}
