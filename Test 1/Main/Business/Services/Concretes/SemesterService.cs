using Business.Exceptions.Semester;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class SemesterService : ISemesterService
    {
        ISemesterRepository _semesterRepository;

        public SemesterService(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        public Task CreateSemester(Semester semester)
        {
            _semesterRepository.Add(semester);
            _semesterRepository.Commit();
            return Task.CompletedTask;
        }

        public async Task<List<Semester>> GetAllSemester(Expression<Func<Semester, bool>>? func = null, Expression<Func<Semester, object>>? orderBy = null, bool isOrderByDesting = false, params Expression<Func<Semester, object>>[] includes)
        {
            IQueryable<Semester> semesters = await _semesterRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            return semesters.ToList();
        }

        public Semester GetSemester(Func<Semester, bool>? func = null)
        {
            return _semesterRepository.Get(func);
        }

        public Task SetActiveSemester(string name)
        {
            Semester activeSemester = GetSemester(x=>x.IsActive);
            if(activeSemester != null)
            {
                activeSemester.IsActive = false;
            }
            Semester sem = GetSemester(x=>x.Name == name);
            if(sem == null)
            {
                throw new SemesterNotFoundException("Name", name + " - Semester not found");
            }
            sem.IsActive = true;
            _semesterRepository.Commit();
            return Task.CompletedTask;
        }

        public Task SetActiveSemester(int id)
        {
            Semester activeSemester = GetSemester(x => x.IsActive);
            if (activeSemester != null)
            {
                activeSemester.IsActive = false;
            }
            Semester sem = GetSemester(x => x.Id == id);
            if (sem == null)
            {
                throw new SemesterNotFoundException("Name", id + " - Semester not found");
            }
            sem.IsActive = true;
            _semesterRepository.Commit();
            return Task.CompletedTask;
        }
    }
}
