using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class GroupService : IGroupService
    {
        IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void CreateGroup(Group group)
        {
            _groupRepository.Add(group);
            _groupRepository.Commit();
        }

        public void DeleteGroup(int id)
        {
            Group group = _groupRepository.Get(x=>x.Id == id);
            if(group == null)
            {
                throw new GroupNotFoundException("", "Group Not Found!");
            }
            _groupRepository.Remove(group);
            _groupRepository.Commit();
        }
        public void SoftDeleteGroup(int id)
        {
            Group group = _groupRepository.Get(x => x.Id == id);
            if (group == null)
            {
                throw new GroupNotFoundException("", "Group Not Found!");
            }
            group.IsDeleted = true;
            _groupRepository.Commit();
        }

        public async Task<List<Group>> GetAllGroup
            (
            Expression<Func<Group, bool>>? func = null,
            Expression<Func<Group, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<Group, object>>[] includes
            )
        {
            var queryable = await _groupRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            return await queryable.ToListAsync();
        }

        public Group GetGroup(Func<Group, bool>? func = null)
        {
            return _groupRepository.Get(func);
        }

        public void UpdateGroup(int id, Group group)
        {
            Group oldGroup = _groupRepository.Get(x => x.Id == id);
            if (oldGroup == null)
            {
                throw new GroupNotFoundException("", "Group Not Found!");
            }
            oldGroup.Name = group.Name;
            oldGroup.FacultyName = group.FacultyName;
            oldGroup.Specialty = group.Specialty;
            _groupRepository.Commit();
        }
    }
}
