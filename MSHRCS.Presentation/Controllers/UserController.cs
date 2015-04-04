using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
			ViewBag.Users = _userService.GetAll();
			return View();
		}

		[HttpPost]
		public JsonResult LogIn(LogInUserViewModel logInUserViewModel)
		{
			if (!ModelState.IsValid)
			{
				return Json(new { success = false, message = Constants.ErrorMessage });
			}

			var password = AuthorizationHelper.GetHashString(logInUserViewModel.Password);
			var user = _userService.CheckUserExists(logInUserViewModel.UserId, password);

			if (user == null)
			{
				return Json(new { success = false, message = "Неправильный пароль!" });
			}

			FormsAuthentication.SetAuthCookie(user.Name, logInUserViewModel.RememberMe);
			GlobalStoreHelper.SetSession(user);

			return Json(new { success = true, nextPage = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			Session[Constants.SessionKeyUser] = null;

			return RedirectToAction("Index", "Home");
		}
	}
}
