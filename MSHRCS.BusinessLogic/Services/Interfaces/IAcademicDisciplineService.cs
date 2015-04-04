using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;

namespace MSHRCS.BusinessLogic.Services.Interfaces
{
	public interface IAcademicDisciplineService
	{
		IEnumerable<AcademicDiscipline> GetAll();
		void AddOrUpdate(AcademicDiscipline academicdiscipline);
		void Delete(AcademicDiscipline academicdiscipline);
	}
}
