using System.Web;
using MSHRCS.BusinessLogic;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Enums;
using MSHRCS.BusinessLogic.Services.Interfaces;

namespace MSHRCS.Presentation.Helpers
{
	public static class GlobalStoreHelper
	{
		private static IUserService _userService;

		public static void Initialize(IUserService userService)
		{
			_userService = userService;
		}

		public static void SetSession(int userId)
		{
			var user = _userService.Get(userId);
			HttpContext.Current.Session[Constants.SessionKeyUser] = user;
		}

		public static void SetSession(User user)
		{
			HttpContext.Current.Session[Constants.SessionKeyUser] = user;
		}
	}
}