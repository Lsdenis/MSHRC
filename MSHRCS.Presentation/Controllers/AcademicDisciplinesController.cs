using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Enums;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.BusinessLogic.UnitOfWork;
using MSHRCS.Presentation.Attributes;
using MSHRCS.Presentation.Models;

namespace MSHRCS.Presentation.Controllers
{
	[CustomAuthorize(RequiredUsers = new[] { UsersEnum.Administrator })]
	public class AcademicDisciplinesController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAcademicDisciplineService _academicDisciplineService;

		public AcademicDisciplinesController(IUnitOfWork unitOfWork, IAcademicDisciplineService academicDisciplineService)
		{
			_unitOfWork = unitOfWork;
			_academicDisciplineService = academicDisciplineService;
		}

		public ViewResult Index()
		{
			var aDVM =
				Mapper.Map<IEnumerable<AcademicDiscipline>, IEnumerable<AcademicDisciplineViewModel>>(
					_academicDisciplineService.GetAll());
			return View(aDVM);
		}

		public ViewResult Details(int id)
		{
			var academicdiscipline = _academicDisciplineService.GetAll().First(ad => ad.Id == id);
			var aDVM = Mapper.Map<AcademicDiscipline, AcademicDisciplineViewModel>(academicdiscipline);
			return View(aDVM);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(AcademicDisciplineViewModel academicDisciplineViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(academicDisciplineViewModel);
			}

			var academicDiscipline = Mapper.Map<AcademicDisciplineViewModel, AcademicDiscipline>(academicDisciplineViewModel);
			_academicDisciplineService.AddOrUpdate(academicDiscipline);
			_unitOfWork.Commit();
			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			var academicdiscipline = _academicDisciplineService.GetAll().First(ad => ad.Id == id);
			var aDVM = Mapper.Map<AcademicDiscipline, AcademicDisciplineViewModel>(academicdiscipline);
			return View(aDVM);
		}

		[HttpPost]
		public ActionResult Edit(AcademicDisciplineViewModel academicDisciplineViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(academicDisciplineViewModel);
			}

			var academicDiscipline = Mapper.Map<AcademicDisciplineViewModel, AcademicDiscipline>(academicDisciplineViewModel);

			_unitOfWork.Context.Entry(academicDiscipline).State = EntityState.Modified;
			_unitOfWork.Commit();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			var academicdiscipline = _academicDisciplineService.GetAll().First(ad => ad.Id == id);
			var aDVM = Mapper.Map<AcademicDiscipline, AcademicDisciplineViewModel>(academicdiscipline);
			return View(aDVM);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			var academicdiscipline = _academicDisciplineService.GetAll().First(ad => ad.Id == id);
			_academicDisciplineService.Delete(academicdiscipline);
			_unitOfWork.Commit();
			return RedirectToAction("Index");
		}
	}
}