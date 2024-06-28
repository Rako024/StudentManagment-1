using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcretes
{
    public class HomeworkSubmissionRepository : GenericRepository<HomeworkSubmission>, IHomeworkSubmissionRepository
    {
        public HomeworkSubmissionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
