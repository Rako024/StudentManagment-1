using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Group> GetAllGroup(Func<Group, bool>? func = null)
        {
            return _groupRepository.GetAll();
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
