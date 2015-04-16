using AutoMapper;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.Presentation.Models;

namespace MSHRCS.Presentation.Helpers
{
	public class AutomapperHelper
	{
		public static void InitAutomapper()
		{
			InitAcademicDisciplineMappers();
			InitGroupDisciplineMappers();
			InitTeacherMappers();
		}

		private static void InitTeacherMappers()
		{
			Mapper.CreateMap<Teacher, TeacherViewModel>();
		}

		private static void InitGroupDisciplineMappers()
		{
			Mapper.CreateMap<Group, GroupViewModel>();
		}

		private static void InitAcademicDisciplineMappers()
		{
			Mapper.CreateMap<AcademicDiscipline, AcademicDisciplineViewModel>();
			Mapper.CreateMap<AcademicDisciplineViewModel, AcademicDiscipline>();
		}
	}
}