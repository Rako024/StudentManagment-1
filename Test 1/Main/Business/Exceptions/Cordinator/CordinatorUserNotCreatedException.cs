using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions.Cordinator
{
	public class CordinatorUserNotCreatedException : Exception
	{
		public CordinatorUserNotCreatedException(string? message) : base(message)
		{
		}
	}
}
