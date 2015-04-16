using System.Collections.Generic;
using System.Data;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.BusinessLogic.UnitOfWork;

namespace MSHRCS.BusinessLogic.Services.Classes
{
	public class GroupDisciplineService : IGroupDisciplineService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<GroupDiscipline> _groupDisciplineRepository;

		public GroupDisciplineService(IUnitOfWork unitOfWork, IRepository<GroupDiscipline> groupDisciplineRepository)
		{
			_unitOfWork = unitOfWork;
			_groupDisciplineRepository = groupDisciplineRepository;
		}

		public IEnumerable<GroupDiscipline> GetAllGroupDisciplines()
		{
			return _groupDisciplineRepository.GetAll();
		}

		public void SaveOrUpdate(GroupDiscipline groupDiscipline)
		{
			if (groupDiscipline.Id == 0)
			{
				_groupDisciplineRepository.Add(groupDiscipline);
			}
			else
			{
				var discipline = _groupDisciplineRepository.Get(gd => gd.Id == groupDiscipline.Id);
				_unitOfWork.Context.Entry(discipline).CurrentValues.SetValues(groupDiscipline);
				_unitOfWork.Context.Entry(discipline).State = EntityState.Modified;
			}
		}
	}
}
