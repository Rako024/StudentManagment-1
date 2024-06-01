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
    public class StudentUserRepository : GenericRepository<StudentUser>, IStudentUserRepositroy
    {
        AppDbContext _dbContext;
        public StudentUserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<StudentUser> GetAllStudentWithGroup(Func<StudentUser, bool>? func = null)
        {
            return func == null ? _dbContext.Students.Include(x => x.Group).ToList() :
                                  _dbContext.Students.Include(x=>x.Group).Where(func).ToList();
        }
    }
}
