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
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        AppDbContext _dbContext;
        public LessonRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Lesson> GetAllLessonsWithGroupAndTeacherUser(Func<Lesson, bool>? func = null)
        {
            return func == null ? _dbContext.Lessons.Include(x => x.Group).Include(x => x.TeacherUser).ToList() :
                _dbContext.Lessons.Include(x=>x.Group).Include(x=>x.TeacherUser).Where(func).ToList();
        }

        public Lesson GetLessonsWithGroupAndTeacherUser(Func<Lesson, bool>? func = null)
        {
            return func == null ? _dbContext.Lessons.Include(x => x.Group).Include(x => x.TeacherUser).FirstOrDefault() :
                _dbContext.Lessons.Include(x => x.Group).Include(x => x.TeacherUser).Where(func).FirstOrDefault();
        }
    }
}
