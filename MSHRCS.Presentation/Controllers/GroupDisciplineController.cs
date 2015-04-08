using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MSHRCS.BusinessLogic.DataModel;

namespace MSHRCS.Presentation.Controllers
{
	public class GroupDisciplineController : Controller
	{
		private MSHRCSchedulerContext db = new MSHRCSchedulerContext();

		public ViewResult Index()
		{
			var groupdisciplines = db.GroupDisciplines.Include(g => g.AcademicDiscipline).Include(g => g.Group);
			return View(groupdisciplines.ToList());
		}

		public ViewResult Details(int id)
		{
			GroupDiscipline groupdiscipline = db.GroupDisciplines.Find(id);
			return View(groupdiscipline);
		}

		public ActionResult Create()
		{
			ViewBag.DisciplineId = new SelectList(db.AcademicDisciplines, "Id", "Code");
			ViewBag.GroupId = new SelectList(db.Groups, "Id", "Code");
			return View();
		}

		[HttpPost]
		public ActionResult Create(GroupDiscipline groupdiscipline)
		{
			if (ModelState.IsValid)
			{
				db.GroupDisciplines.Add(groupdiscipline);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.DisciplineId = new SelectList(db.AcademicDisciplines, "Id", "Code", groupdiscipline.DisciplineId);
			ViewBag.GroupId = new SelectList(db.Groups, "Id", "Code", groupdiscipline.GroupId);
			return View(groupdiscipline);
		}

		public ActionResult Edit(int id)
		{
			GroupDiscipline groupdiscipline = db.GroupDisciplines.Find(id);
			ViewBag.DisciplineId = new SelectList(db.AcademicDisciplines, "Id", "Code", groupdiscipline.DisciplineId);
			ViewBag.GroupId = new SelectList(db.Groups, "Id", "Code", groupdiscipline.GroupId);
			return View(groupdiscipline);
		}

		[HttpPost]
		public ActionResult Edit(GroupDiscipline groupdiscipline)
		{
			if (ModelState.IsValid)
			{
				db.Entry(groupdiscipline).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.DisciplineId = new SelectList(db.AcademicDisciplines, "Id", "Code", groupdiscipline.DisciplineId);
			ViewBag.GroupId = new SelectList(db.Groups, "Id", "Code", groupdiscipline.GroupId);
			return View(groupdiscipline);
		}

		public ActionResult Delete(int id)
		{
			GroupDiscipline groupdiscipline = db.GroupDisciplines.Find(id);
			return View(groupdiscipline);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			GroupDiscipline groupdiscipline = db.GroupDisciplines.Find(id);
			db.GroupDisciplines.Remove(groupdiscipline);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}