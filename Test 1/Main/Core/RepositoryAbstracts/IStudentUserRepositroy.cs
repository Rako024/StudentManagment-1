using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryAbstracts
{
    public interface IStudentUserRepositroy:IGenericRepositroy<StudentUser>
    {
        List<StudentUser> GetAllStudentWithGroup(Func<StudentUser, bool>? func = null);
    }
}
