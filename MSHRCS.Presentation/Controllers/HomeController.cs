using System.Web.Mvc;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.Presentation.Helpers;

namespace MSHRCS.Presentation.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{
		}
		
		public ActionResult Index()
		{
			return View();
		}

	}
}
