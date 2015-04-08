using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MSHRCS.BusinessLogic;
using MSHRCS.BusinessLogic.DTO;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.Presentation.Models;
using PagedList;

namespace MSHRCS.Presentation.Controllers
{
	public class TablesController : Controller
	{
		private readonly IGDCabinetService _gdCabinetService;

		public TablesController(IGDCabinetService gdCabinetService)
		{
			_gdCabinetService = gdCabinetService;
		}

		[HttpGet]
		public ActionResult GeneralTable()
		{
			var rowValues = _gdCabinetService.GetGeneralTableRowValues(DateTime.Now);
			var generalTableViewModel = GetGeneralTableViewModel(rowValues, 1);

			return View(generalTableViewModel);
		}

		[HttpGet]
		public ActionResult AdditionalTable()
		{
			return View();
		}

		[HttpGet]
		public PartialViewResult GetGeneralTableData(
			string group,
			int timeId,
			string teacher,
			string cabinet,
			string discipline,
			int? page)
		{
			var pageNumber = page ?? 1;
			var rowValues = _gdCabinetService.GetGeneralTableRowValues(DateTime.Now).Where(value => value.Group.StartsWith(group)).ToList();

			if (timeId != 0)
			{
				rowValues = rowValues.Where(value => value.TimeId == timeId).ToList();
			}

			if (!string.IsNullOrWhiteSpace(teacher))
			{
				rowValues = rowValues.Where(value => value.Teacher.Contains(teacher)).ToList();
			}

			if (!string.IsNullOrWhiteSpace(cabinet))
			{
				rowValues = rowValues.Where(value => value.Cabinet.StartsWith(cabinet)).ToList();
			}

			if (!string.IsNullOrWhiteSpace(discipline))
			{
				rowValues = rowValues.Where(value => value.AcademicDiscipline.StartsWith(discipline)).ToList();
			}

			var generalTableViewModel = GetGeneralTableViewModel(rowValues, pageNumber);
			generalTableViewModel.Group = group;
			generalTableViewModel.TimeId = timeId;
			generalTableViewModel.Teacher = teacher;
			generalTableViewModel.Cabinet = cabinet;
			generalTableViewModel.Discipline = discipline;

			return PartialView(generalTableViewModel);
		}

		private static GeneralTableViewModel GetGeneralTableViewModel(IEnumerable<GeneralTableRowValue> rowValues, int pageNumber)
		{
			var generalTableViewModel = new GeneralTableViewModel();
			generalTableViewModel.RowValues = rowValues.ToPagedList(pageNumber, Constants.PageSize);
			return generalTableViewModel;
		}
	}
}
