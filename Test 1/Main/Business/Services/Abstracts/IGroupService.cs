using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IGroupService
    {
        void CreateGroup(Group group);
        void UpdateGroup(int id,Group group);
        void DeleteGroup(int id);
        void SoftDeleteGroup(int id);

        Group GetGroup(Func<Group,bool>? func = null);
        Task<List<Group>> GetAllGroup
            (
            Expression<Func<Group, bool>>? func = null,
            Expression<Func<Group, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<Group, object>>[] includes
            );
    }
}
