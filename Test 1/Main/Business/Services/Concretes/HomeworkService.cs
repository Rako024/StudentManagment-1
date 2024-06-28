using Business.DTOs.Teacher.Homeworks;
using Business.Exceptions;
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
	public class HomeworkService : IHomeworkService
	{
		private readonly IHomeworkRepository _homeworkRepository;

		public HomeworkService(IHomeworkRepository homeworkRepository)
		{
			_homeworkRepository = homeworkRepository;
		}

		public async Task CreateHomework(HomeworkDto homeworkDto)
		{
			var homework = new Homework
			{
				Name = homeworkDto.Name,
				Description = homeworkDto.Description,
				LessonId = homeworkDto.LessonId
			};

			_homeworkRepository.Add(homework);
			_homeworkRepository.Commit();
		}

        public async Task UpdateHomework(int id, HomeworkDto homeworkDto)
        {
            Homework existingHomework = _homeworkRepository.Get(x => x.Id == id);
            if (existingHomework == null)
            {
                throw new GlobalException("Homework", "Homework not found");
            }

            existingHomework.Name = homeworkDto.Name;
            existingHomework.Description = homeworkDto.Description;
            existingHomework.LessonId = homeworkDto.LessonId;

            
            _homeworkRepository.Commit();
        }

        public async Task DeleteHomework(int id)
		{
			var homework = await _homeworkRepository.GetAsync(x => x.Id == id);
			if (homework == null)
			{
				throw new GlobalException("Homework", "Homework not found");
			}

			_homeworkRepository.Remove(homework);
			 _homeworkRepository.Commit();
		}

		public async Task<Homework> GetHomework(Expression<Func<Homework, bool>>? func = null)
		{
			return await _homeworkRepository.GetAsync(func);
		}

		public async Task<List<Homework>> GetAllHomeworks(Expression<Func<Homework, bool>>? func = null, Expression<Func<Homework, object>>? orderBy = null, bool isOrderByDescending = false, params Expression<Func<Homework, object>>[] includes)
		{
			IQueryable<Homework> queryableHomework = await _homeworkRepository.GetAll(func, orderBy, isOrderByDescending, includes);
			return queryableHomework.ToList();
		}
	}
}
