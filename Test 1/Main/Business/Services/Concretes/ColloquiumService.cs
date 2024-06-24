using Business.DTOs.Teacher.TeacherLessonsDto;
using Business.Exceptions.Teacher;
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
    public class ColloquiumService : IColloquiumService
    {
        IColloquiumRepository _colloquiumRepository;

        public ColloquiumService(IColloquiumRepository colloquiumRepository)
        {
            _colloquiumRepository = colloquiumRepository;
        }

        public async Task AddColloquium(ColloquiumDto dto)
        {
            Colloquium colloquium = new Colloquium() 
            {
                StudentUserId = dto.StudentUserId,
                LessonId = dto.LessonId,
                FirstGrade = dto.FirstGrade,
                SecondGrade = dto.SecondGrade,
                ThirdGrade = dto.ThirdGrade,
            };
            _colloquiumRepository.Add(colloquium);
            _colloquiumRepository.Commit();
        }

        public async Task AddOrUpdateColloquiumAsync(ColloquiumDto dto)
        {
            bool check = await CheckColloquiumAsync(dto.StudentUserId, dto.LessonId);
            if (check)
            {
                await UpdateColloquium(dto);
            }
            else
            {
                await AddColloquium(dto);
            }
        }

        public Task<bool> CheckColloquiumAsync(string studentId, int lessonId)
        {
            Colloquium colloquium = _colloquiumRepository.Get(x => x.StudentUserId == studentId && x.LessonId == lessonId);
            if (colloquium != null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<List<Colloquium>> GetAllColloquiumAsync(Expression<Func<Colloquium, bool>>? func = null, Expression<Func<Colloquium, object>>? orderBy = null, bool isOrderByDesting = false, params Expression<Func<Colloquium, object>>[] includes)
        {
            IQueryable<Colloquium> colloquia = await _colloquiumRepository.GetAll(func, orderBy, isOrderByDesting, includes); 
            return colloquia.ToList();
        }

        public async Task<Colloquium> GetColloquiumAsync(Func<Colloquium, bool>? func = null)
        {
            return _colloquiumRepository.Get(func);
        }

        public async Task UpdateColloquium(ColloquiumDto dto)
        {
            Colloquium colloquium = _colloquiumRepository.Get(x=>x.StudentUserId == dto.StudentUserId && x.LessonId == dto.LessonId);
            if (colloquium == null)
            {
                throw new NotFoundColloquiumException();
            }

            if (dto.FirstGrade != null)
            {
                colloquium.FirstGrade = dto.FirstGrade;
            }

            if (dto.SecondGrade != null)
            {
                colloquium.SecondGrade = dto.SecondGrade;
            }

            if (dto.ThirdGrade != null)
            {
                colloquium.ThirdGrade = dto.ThirdGrade;
            }

            _colloquiumRepository.Commit();
        }
    }
}
