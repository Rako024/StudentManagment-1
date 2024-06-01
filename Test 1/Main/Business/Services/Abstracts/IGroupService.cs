using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IGroupService
    {
        void CreateGroup(Group group);
        void UpdateGroup(int id,Group group);
        void DeleteGroup(int id);

        Group GetGroup(Func<Group,bool>? func = null);
        List<Group> GetAllGroup(Func<Group,bool>? func = null);
    }
}
