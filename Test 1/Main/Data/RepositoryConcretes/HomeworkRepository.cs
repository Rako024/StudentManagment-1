using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcretes
{
	public class HomeworkRepository : GenericRepository<Homework>, IHomeworkRepository
	{
        
		public HomeworkRepository(AppDbContext dbContext) : base(dbContext)
		{
		}
        

        
    }
}
