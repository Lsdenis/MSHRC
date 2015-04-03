using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSHRCS.BusinessLogic;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Enums;
using MSHRCS.Presentation.Helpers;

namespace MSHRCS.Presentation.Attributes
{
	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		public UsersEnum[] RequiredUsers { get; set; }

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}

			if (httpContext.User == null)
			{
				return false;
			}

			if (httpContext.Session == null || !(httpContext.Session[Constants.SessionKeyUser] is User))
			{
				GlobalStoreHelper.SetSession(int.Parse(httpContext.User.Identity.Name));
			}

			var user = httpContext.Session[Constants.SessionKeyUser] as User;

			return user != null && RequiredUsers.Contains((UsersEnum)user.Id);
		}
	}
}