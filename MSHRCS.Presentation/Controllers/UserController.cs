using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSHRCS.BusinessLogic;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.Presentation.Helpers;
using MSHRCS.Presentation.Models;

namespace MSHRCS.Presentation.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public ActionResult LogIn()
		{
			return View();
		}

		[HttpPost]
		public JsonResult LogIn(LogInUserViewModel logInUserViewModel)
		{
			if (ModelState.IsValid)
			{
				var password = AuthorizationHelper.GetHashString(logInUserViewModel.Password);
				var user = _userService.CheckUserExists(logInUserViewModel.UserId, password);

				if (user == null)
				{
					return Json(new { success = false });
				}
				else
				{
					return Json(new { success = true });
				}
			}
			else
			{
				return Json(new { success = false, message = Constants.ErrorMessage });
			}
		}
	}
}
