using MSHRCS.BusinessLogic.DataModel;

namespace MSHRCS.BusinessLogic.Services.Interfaces
{
	public interface IUserService
	{
		User CheckUserExists(int userId, string password);
	}
}
