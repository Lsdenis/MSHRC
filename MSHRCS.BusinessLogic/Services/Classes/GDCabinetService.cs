using System;
using System.Collections.Generic;
using System.Linq;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.DTO;
using MSHRCS.BusinessLogic.Extensions;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Interfaces;

namespace MSHRCS.BusinessLogic.Services.Classes
{
	public class GDCabinetService : IGDCabinetService
	{
		private readonly IRepository<GDCabinet> _gDCabinetRepository;

		public GDCabinetService(IRepository<GDCabinet> gDCabinetRepository)
		{
			_gDCabinetRepository = gDCabinetRepository;
		}

		public IEnumerable<GeneralTableRowValue> GetGeneralTableRowValues(DateTime date)
		{
			var mainTableRowValues = new List<GeneralTableRowValue>();

			var dgc = _gDCabinetRepository.GetAll().Where(cabinet => cabinet.Date.Date.Equals(date.Date));

			foreach (var cabinet in dgc)
			{
				var generalTableRowValue = new GeneralTableRowValue();
				generalTableRowValue.AcademicDiscipline = cabinet.GroupDiscipline.AcademicDiscipline.Code;
				generalTableRowValue.Cabinet = cabinet.Cabinet.Code.ToString();
				generalTableRowValue.Group = cabinet.GroupDiscipline.Group.Code;
				generalTableRowValue.Hours = cabinet.GroupDiscipline.GDTeachers.First(teacher => teacher.LessonTypeId == cabinet.LessonTypeId).ActualHoursNumber;

				var teachers =
					cabinet.GroupDiscipline.GDTeachers.Where(teacher => teacher.LessonTypeId == cabinet.LessonTypeId).ToList();

				var teacherNames = teachers.First().Teacher.LastName;

				for (var i = 1; i < teachers.Count; i++)
				{
					teacherNames += "/" + teachers[i].Teacher.LastName;
				}

				generalTableRowValue.Teacher = teacherNames;
				generalTableRowValue.Time = cabinet.Lesson.Name;
				generalTableRowValue.TimeId = cabinet.LessonId;

				mainTableRowValues.Add(generalTableRowValue);
			}

			return mainTableRowValues;
		}
	}
}
