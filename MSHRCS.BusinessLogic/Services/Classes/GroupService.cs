using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.BusinessLogic.UnitOfWork;

namespace MSHRCS.BusinessLogic.Services.Classes
{
	public class GroupService: IGroupService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<Group> _groupRepository;

		public GroupService(IUnitOfWork unitOfWork, IRepository<Group> groupRepository)
		{
			_unitOfWork = unitOfWork;
			_groupRepository = groupRepository;
		}

		public IEnumerable<Group> GetAllGroups()
		{
			return _groupRepository.GetAll();
		}
	}
}
