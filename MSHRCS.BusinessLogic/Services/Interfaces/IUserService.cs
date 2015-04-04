using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;

namespace MSHRCS.BusinessLogic.Services.Interfaces
{
	public interface IUserService
	{
		User CheckUserExists(int userId, string password);
		User Get(int userEnum);
		IEnumerable<User> GetAll();
		User Get(string userName);
	}
}
