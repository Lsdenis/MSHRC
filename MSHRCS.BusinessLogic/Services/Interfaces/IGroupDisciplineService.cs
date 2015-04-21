using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;

namespace MSHRCS.BusinessLogic.Services.Interfaces
{
	public interface IGroupDisciplineService
	{
		IEnumerable<GroupDiscipline> GetAllGroupDisciplines();
		void SaveOrUpdate(GroupDiscipline groupDiscipline);
		void DeleteGroupDiscipline(int id);
	}
}
