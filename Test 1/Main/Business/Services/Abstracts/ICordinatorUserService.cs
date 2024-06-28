using Business.DTOs.Cordinator;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
	public interface ICordinatorUserService
	{
		Task CreateCordinator(CordinatorCreateDto dto);
		Task DeleteCordinator(string id);
		Task UpdateCordinator(string id, CordinatorUpdateDto dto);
		Task<List<CordinatorUser>> GetAllCordinators(
			Expression<Func<CordinatorUser, bool>>? func = null,
			Expression<Func<CordinatorUser, object>>? orderBy = null,
			bool isOrderByDesting = false,
			params Expression<Func<CordinatorUser, object>>[] includes
		);
		CordinatorUser GetCordinator(Func<CordinatorUser, bool>? func = null);
		Task UpdatePasswordAsync(string userId, string currentPassword, string newPassword);
	}
}
