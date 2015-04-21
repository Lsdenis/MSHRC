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
		private readonly IRepository<GDCabinet> _gdCabinetRepository;
		private readonly IRepository<Cabinet> _cabinetRepository;
		private readonly IRepository<Lesson> _lessonRepository;

		public GDCabinetService(IRepository<GDCabinet> gdCabinetRepository, IRepository<Cabinet> cabinetRepository,
			IRepository<Lesson> lessonRepository)
		{
			_gdCabinetRepository = gdCabinetRepository;
			_cabinetRepository = cabinetRepository;
			_lessonRepository = lessonRepository;
		}

		public IEnumerable<GeneralTableRowValue> GetGeneralTableRowValues(DateTime date)
		{
			var mainTableRowValues = new List<GeneralTableRowValue>();

			var dgc = _gdCabinetRepository.GetAll().Where(cabinet => cabinet.Date.Date.Equals(date.Date));

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

		public IEnumerable<GDCabinet> GetAllGDCabinets()
		{
			return _gdCabinetRepository.GetAll();
		}

		public Dictionary<Lesson, List<Cabinet>> GetCabinetLessons(DateTime date)
		{
			var lessons = _lessonRepository.GetAll().ToList();
			var cabinets = _cabinetRepository.GetAll().ToList();
			var dictionary = new Dictionary<Lesson, List<Cabinet>>();

			foreach (var lesson in lessons)
			{
				var list = new List<Cabinet>();

				foreach (var cabinet in cabinets)
				{
					if (cabinet.GDCabinets.Any(gdCabinet => gdCabinet.Date.Date == date.Date && gdCabinet.LessonId == lesson.Id))
					{
						continue;
					}

					list.Add(cabinet);
				}

				dictionary.Add(lesson, list);
			}

			return dictionary;
		}
	}
}
