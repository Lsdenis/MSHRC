using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.Presentation.Models;
using Newtonsoft.Json;

namespace MSHRCS.Presentation.Controllers
{
	public class GDCabinetController : Controller
	{
		private readonly IGDCabinetService _cabinetService;
		private readonly IGroupDisciplineService _groupDisciplineService;
		private MSHRCSchedulerContext db = new MSHRCSchedulerContext();

		public GDCabinetController(IGDCabinetService cabinetService, IGroupDisciplineService groupDisciplineService)
		{
			_cabinetService = cabinetService;
			_groupDisciplineService = groupDisciplineService;
		}

		public ViewResult Index()
		{
			var cabinets = _cabinetService.GetAllGDCabinets().ToList();

			var cabinetViewModels = Mapper.Map<IEnumerable<GDCabinet>, List<GDCabinetViewModel>>(cabinets);
			return View(cabinetViewModels);
		}

		public ViewResult Details(int id)
		{
			GDCabinet gdcabinet = db.GDCabinets.Find(id);
			return View(gdcabinet);
		}

		public ActionResult Create()
		{
			ViewBag.Lessons = new SelectList(db.Lessons, "Id", "Name");
			ViewBag.LessonTypes = new SelectList(db.LessonTypes, "Id", "Name");
			return View();
		}

		[HttpPost]
		public ActionResult Create(NewGDCabinetViewModel gdcabinet)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			ViewBag.Lessons = new SelectList(db.Lessons, "Id", "Name");
			ViewBag.LessonTypes = new SelectList(db.LessonTypes, "Id", "Name");
			return View(gdcabinet);
		}
		public ActionResult Edit(int id)
		{
			GDCabinet gdcabinet = db.GDCabinets.Find(id);
			ViewBag.CabinetId = new SelectList(db.Cabinets, "Id", "Id", gdcabinet.CabinetId);
			ViewBag.GDId = new SelectList(db.GroupDisciplines, "Id", "Id", gdcabinet.GDId);
			ViewBag.LessonId = new SelectList(db.Lessons, "Id", "Name", gdcabinet.LessonId);
			ViewBag.LessonTypeId = new SelectList(db.LessonTypes, "Id", "Name", gdcabinet.LessonTypeId);
			ViewBag.SubstituteDGId = new SelectList(db.GroupDisciplines, "Id", "Id", gdcabinet.SubstituteDGId);
			return View(gdcabinet);
		}

		[HttpPost]
		public ActionResult Edit(GDCabinet gdcabinet)
		{
			if (ModelState.IsValid)
			{
				db.Entry(gdcabinet).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.CabinetId = new SelectList(db.Cabinets, "Id", "Id", gdcabinet.CabinetId);
			ViewBag.GDId = new SelectList(db.GroupDisciplines, "Id", "Id", gdcabinet.GDId);
			ViewBag.LessonId = new SelectList(db.Lessons, "Id", "Name", gdcabinet.LessonId);
			ViewBag.LessonTypeId = new SelectList(db.LessonTypes, "Id", "Name", gdcabinet.LessonTypeId);
			ViewBag.SubstituteDGId = new SelectList(db.GroupDisciplines, "Id", "Id", gdcabinet.SubstituteDGId);
			return View(gdcabinet);
		}

		public ActionResult Delete(int id)
		{
			GDCabinet gdcabinet = db.GDCabinets.Find(id);
			return View(gdcabinet);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			GDCabinet gdcabinet = db.GDCabinets.Find(id);
			db.GDCabinets.Remove(gdcabinet);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}

		public JsonResult GetGroupDisciplineViewModels()
		{
			var groupDisciplines = _groupDisciplineService.GetAllGroupDisciplines();
			var groupDisciplineViewModels = Mapper.Map<IEnumerable<GroupDiscipline>, IEnumerable<GroupDisciplineViewModel>>(groupDisciplines);

			return Json(new { groupDisciplineViewModels }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetCabinetLessonViewModels()
		{
			var cabinetLessons = _cabinetService.GetCabinetLessons(new DateTime(2015, 4, 6));
			var lessonAvailableCabinetsViewModels = Mapper.Map<Dictionary<Lesson, List<Cabinet>>, List<LessonAvailableCabinetsViewModel>>(cabinetLessons);

			return Json(new { lessonAvailableCabinetsViewModels }, JsonRequestBehavior.AllowGet);
		}
	}
}