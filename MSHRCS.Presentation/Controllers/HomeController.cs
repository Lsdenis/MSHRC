using System.Web.Mvc;
using MSHRCS.BusinessLogic.Services.Interfaces;

namespace MSHRCS.Presentation.Controllers
{
	public class HomeController : Controller
	{

		public HomeController()
		{
		}

		//
		// GET: /Home/

		public ActionResult Index()
		{
			return View();
		}

	}
}
