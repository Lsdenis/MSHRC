using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Interfaces;

namespace MSHRCS.BusinessLogic.Services.Classes
{
	public class AcademicDisciplineService: IAcademicDisciplineService
	{
		private readonly IRepository<AcademicDiscipline> _academicDisciplineRepository;

		public AcademicDisciplineService(IRepository<AcademicDiscipline> academicDisciplineRepository)
		{
			_academicDisciplineRepository = academicDisciplineRepository;
		}

		public IEnumerable<AcademicDiscipline> GetAll()
		{
			return _academicDisciplineRepository.GetAll();
		}

		public void AddOrUpdate(AcademicDiscipline academicdiscipline)
		{
			_academicDisciplineRepository.Add(academicdiscipline);
		}

		public void Delete(AcademicDiscipline academicdiscipline)
		{
			_academicDisciplineRepository.Delete(academicdiscipline);
		}
	}
}
