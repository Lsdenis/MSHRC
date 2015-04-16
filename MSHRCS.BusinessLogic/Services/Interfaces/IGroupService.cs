using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;

namespace MSHRCS.BusinessLogic.Services.Interfaces
{
	public interface IGroupService
	{
		IEnumerable<Group> GetAllGroups();
	}
}
