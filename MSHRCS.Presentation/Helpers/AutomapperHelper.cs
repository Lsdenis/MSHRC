using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.Presentation.Controllers;
using MSHRCS.Presentation.Models;

namespace MSHRCS.Presentation.Helpers
{
	public class AutomapperHelper
	{
		public static void InitAutomapper()
		{
			InitTeacherMappers();
			InitAcademicDisciplineMappers();
			InitGroupDisciplineMappers();
			InitGDCabinetMappers();
			InitCustomMappers();
		}

		private static void InitCustomMappers()
		{
			Mapper.CreateMap<Dictionary<Lesson, List<Cabinet>>, List<LessonAvailableCabinetsViewModel>>()
			.ConvertUsing(dictionary =>
			{
				var list = new List<LessonAvailableCabinetsViewModel>();

				foreach (var pair in dictionary)
				{
					var lessonAvailableCabinetsViewModel = new LessonAvailableCabinetsViewModel();
					lessonAvailableCabinetsViewModel.Id = pair.Key.Id;
					lessonAvailableCabinetsViewModel.Name = pair.Key.Name;

					var cabinetViewModels = pair.Value.Select(cabinet => new CabinetViewModel {Code = cabinet.Code, Id = cabinet.Id}).ToList();
					lessonAvailableCabinetsViewModel.AvailableCabinets = cabinetViewModels;

					list.Add(lessonAvailableCabinetsViewModel);
				}

				return list;
			});
		}

		private static void InitGDCabinetMappers()
		{
			Mapper.CreateMap<GDCabinet, GDCabinetViewModel>()
				.ForMember(model => model.Discipline,
					source => source.MapFrom(cabinet => cabinet.GroupDiscipline.AcademicDiscipline.Description))
				.ForMember(model => model.Cabinet, source => source.MapFrom(cabinet => cabinet.Cabinet.Code))
				.ForMember(model => model.Date, source => source.MapFrom(cabinet => cabinet.Date.ToShortDateString()))
				.ForMember(model => model.Lesson, source => source.MapFrom(cabinet => cabinet.Lesson.Name))
				.ForMember(model => model.LessonType, source => source.MapFrom(cabinet => cabinet.LessonType.Name));

		}

		private static void InitTeacherMappers()
		{
			Mapper.CreateMap<Teacher, TeacherViewModel>();
			Mapper.CreateMap<GDTeacher, GroupDisciplineTeacherViewModel>()
				.ForMember(model => model.InitialHoursNumber,
					source => source.MapFrom(teacher => teacher.InitialHoursNumber.ToString()))
				.ForMember(model => model.ActualHoursNumber,
					source => source.MapFrom(teacher => teacher.ActualHoursNumber.ToString()))
				.ForMember(model => model.LessonType, source => source.MapFrom(teacher => teacher.LessonType.Name));
		}

		private static void InitGroupDisciplineMappers()
		{
			Mapper.CreateMap<Group, GroupViewModel>();
			Mapper.CreateMap<GroupDiscipline, GroupDisciplineViewModel>()
				.ForMember(model => model.AcademicDiscipline,
					source => source.MapFrom(discipline => discipline.AcademicDiscipline.Description))
				.ForMember(model => model.Group, source => source.MapFrom(discipline => discipline.Group.Code))
				.AfterMap((discipline, model) =>
				{
					model.Teachers =
						discipline.GDTeachers.Select(teacher => new GroupDisciplineTeacherViewModel
						{
							ActualHoursNumber = teacher.ActualHoursNumber.ToString(),
							InitialHoursNumber = teacher.InitialHoursNumber.ToString(),
							FullName = teacher.Teacher.FullName(),
							LessonType = teacher.LessonType.Name,
							GroupDisciplineTeacherId = teacher.Id
						}).ToList();
				});
		}

		private static void InitAcademicDisciplineMappers()
		{
			Mapper.CreateMap<AcademicDiscipline, AcademicDisciplineViewModel>();
			Mapper.CreateMap<AcademicDisciplineViewModel, AcademicDiscipline>();
		}
	}
}