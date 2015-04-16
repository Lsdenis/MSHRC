using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.BusinessLogic.UnitOfWork;
using MSHRCS.Presentation.Models;
using Newtonsoft.Json;

namespace MSHRCS.Presentation.Controllers
{
	public class GroupDisciplineController : Controller
	{
		private readonly IGroupService _groupService;
		private readonly IAcademicDisciplineService _academicDisciplineService;
		private readonly ILessonService _lessonService;
		private readonly ITeacherService _teacherService;
		private readonly IGroupDisciplineService _groupDisciplineService;
		private readonly IUnitOfWork _unitOfWork;

		public GroupDisciplineController(IGroupService groupService, IAcademicDisciplineService academicDisciplineService,
			ILessonService lessonService, ITeacherService teacherService, IGroupDisciplineService groupDisciplineService,
			IUnitOfWork unitOfWork)
		{
			_groupService = groupService;
			_academicDisciplineService = academicDisciplineService;
			_lessonService = lessonService;
			_teacherService = teacherService;
			_groupDisciplineService = groupDisciplineService;
			_unitOfWork = unitOfWork;
		}

		public ViewResult Index()
		{
			var groupdisciplines = _groupDisciplineService.GetAllGroupDisciplines().ToList();
			var groupDisciplineViewModels = Mapper.Map<List<GroupDiscipline>, IEnumerable<GroupDisciplineViewModel>>(groupdisciplines);
			return View(groupDisciplineViewModels);
		}

		public ViewResult Details(int id)
		{
			var groupdiscipline = _groupDisciplineService.GetAllGroupDisciplines().First(gd => gd.Id == id);
			var groupDisciplineViewModel = Mapper.Map<GroupDiscipline, GroupDisciplineViewModel>(groupdiscipline);
			return View(groupDisciplineViewModel);
		}

		public ActionResult Create()
		{
			ViewBag.DisciplineId = new SelectList(_academicDisciplineService.GetAll(), "Id", "Code");
			ViewBag.GroupId = new SelectList(_groupService.GetAllGroups(), "Id", "Code");
			ViewBag.LessonTypes =
				_lessonService.GetLessonTypes()
					.Select(type => new SelectListItem { Selected = false, Text = type.Name, Value = type.Id.ToString() }).ToList();
			return View();
		}

		public ActionResult Edit(int id)
		{
			var groupdiscipline = _groupDisciplineService.GetAllGroupDisciplines().First(gd => gd.Id == id);
			var groupDisciplineViewModel = Mapper.Map<GroupDiscipline, GroupDisciplineViewModel>(groupdiscipline);
			ViewBag.GroupId = new SelectList(_groupService.GetAllGroups(), "Id", "Code", groupdiscipline.GroupId);
			return View(groupDisciplineViewModel);
		}

		[HttpPost]
		public ActionResult Edit(GroupDisciplineViewModel groupdiscipline)
		{
//			if (ModelState.IsValid)
//			{
//				db.Entry(groupdiscipline).State = EntityState.Modified;
//				db.SaveChanges();
//				return RedirectToAction("Index");
//			}
			ViewBag.DisciplineId = new SelectList(_academicDisciplineService.GetAll(), "Id", "Code");
			ViewBag.GroupId = new SelectList(_groupService.GetAllGroups(), "Id", "Code");
			return View(groupdiscipline);
		}

		public ActionResult Delete(int id)
		{
			var groupdiscipline = _groupDisciplineService.GetAllGroupDisciplines().First(gd => gd.Id == id);
			var groupDisciplineViewModel = Mapper.Map<GroupDiscipline, GroupDisciplineViewModel>(groupdiscipline);
			return View(groupDisciplineViewModel);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
//			GroupDiscipline groupdiscipline = db.GroupDisciplines.Find(id);
//			db.GroupDisciplines.Remove(groupdiscipline);
//			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public JsonResult GetGroups()
		{
			var groups = _groupService.GetAllGroups().ToList();
			var groupViewModels = Mapper.Map<IEnumerable<Group>, IEnumerable<GroupViewModel>>(groups);

			return Json(new { groupViewModels }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetDisciplines()
		{
			var disciplines = _academicDisciplineService.GetAll().ToList();
			var disciplineViewModels =
				Mapper.Map<IEnumerable<AcademicDiscipline>, IEnumerable<AcademicDisciplineViewModel>>(disciplines);

			return Json(new { disciplineViewModels }, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetTeachers()
		{
			var teachers = _teacherService.GetAllTeachers();
			var teacherViewModels = Mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherViewModel>>(teachers);

			return Json(new { teacherViewModels }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult SaveGroupDiscipline(int groupId, int disciplineId, string gdTeachers)
		{
			var groupDiscipline = new GroupDiscipline();
			groupDiscipline.GroupId = groupId;
			groupDiscipline.DisciplineId = disciplineId;

			var teachers = JsonConvert.DeserializeObject<List<GDTeacher>>(gdTeachers);
			teachers.ForEach(t => t.GroupDiscipline = groupDiscipline);

			_groupDisciplineService.SaveOrUpdate(groupDiscipline);

			foreach (var teacher in teachers)
			{
				_teacherService.SaveOrUpdateGDTeacher(teacher);
			}

			_unitOfWork.Commit();

			return Json(new { nextPage = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
		}
	}
}