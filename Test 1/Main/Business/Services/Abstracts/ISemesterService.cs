using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ISemesterService
    {
        Semester GetSemester(Func<Semester,bool>? func = null);
        Task CreateSemester(Semester semester);

        Task<List<Semester>> GetAllSemester(Expression<Func<Semester, bool>>? func = null,
            Expression<Func<Semester, object>>? orderBy = null,
            bool isOrderByDesting = false,
           params Expression<Func<Semester, object>>[] includes);
        Task SetActiveSemester(string name);
        Task SetActiveSemester(int id);
    }
}
