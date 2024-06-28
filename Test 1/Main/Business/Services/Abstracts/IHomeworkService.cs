using Business.DTOs.Teacher.Homeworks;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
	public interface IHomeworkService
	{
		Task CreateHomework(HomeworkDto homeworkDto);
		Task UpdateHomework(int id, HomeworkDto homeworkDto);
		Task DeleteHomework(int id);
		Task<Homework> GetHomework(Expression<Func<Homework, bool>>? func = null);
		Task<List<Homework>> GetAllHomeworks(Expression<Func<Homework, bool>>? func = null, Expression<Func<Homework, object>>? orderBy = null, bool isOrderByDescending = false, params Expression<Func<Homework, object>>[] includes);
	}
}
