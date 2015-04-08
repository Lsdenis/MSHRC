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
		}

		private static void InitAcademicDisciplineMappers()
		{
			Mapper.CreateMap<AcademicDiscipline, AcademicDisciplineViewModel>();
			Mapper.CreateMap<AcademicDisciplineViewModel, AcademicDiscipline>();
		}
	}
}