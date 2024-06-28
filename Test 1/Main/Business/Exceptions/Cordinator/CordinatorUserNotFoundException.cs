using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions.Cordinator
{
	public class CordinatorUserNotFoundException : Exception
	{
		public CordinatorUserNotFoundException(string? message) : base(message)
		{
		}
	}
}
