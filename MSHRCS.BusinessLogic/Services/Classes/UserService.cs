using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Interfaces;

namespace MSHRCS.BusinessLogic.Services.Classes
{
	public class UserService : IUserService
	{
		private readonly IRepository<User> _userRepository;

		public UserService(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public User CheckUserExists(int userId, string password)
		{
			return _userRepository.FirstOrDefault(user => user.Id == userId && user.Password.Equals(password));
		}
	}
}
