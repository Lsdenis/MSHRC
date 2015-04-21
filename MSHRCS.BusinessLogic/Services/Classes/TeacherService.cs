using System.Collections.Generic;
using System.Data;
using System.Linq;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.BusinessLogic.UnitOfWork;

namespace MSHRCS.BusinessLogic.Services.Classes
{
	public class TeacherService: ITeacherService
	{
		private readonly IRepository<Teacher> _teacherRepository;
		private readonly IRepository<GDTeacher> _gdTeacherRepository;
		private readonly IUnitOfWork _unitOfWork;

		public TeacherService(IRepository<Teacher> teacherRepository, IRepository<GDTeacher> gdTeacherRepository, IUnitOfWork unitOfWork)
		{
			_teacherRepository = teacherRepository;
			_gdTeacherRepository = gdTeacherRepository;
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Teacher> GetAllTeachers()
		{
			return _teacherRepository.GetAll();
		}

		public void SaveOrUpdateGDTeacher(GDTeacher teacher)
		{
			if (teacher.Id == 0)
			{
				_gdTeacherRepository.Add(teacher);
			}
			else
			{
				var gdTeacher = _gdTeacherRepository.Get(t => t.Id == teacher.Id);
				_unitOfWork.Context.Entry(gdTeacher).CurrentValues.SetValues(teacher);
				_unitOfWork.Context.Entry(gdTeacher).State = EntityState.Modified;
			}
		}

		public void SaveOrUpdateGDTeacher(List<GDTeacher> teachers, List<int> existedTeachers)
		{
			var gdId = teachers.First().GroupDisciplineId;
			var notExisted =
				_gdTeacherRepository.GetAll(
					teacher => teacher.GroupDisciplineId == gdId && existedTeachers.All(id => id != teacher.Id));

			foreach (var gdTeacher in notExisted)
			{
				_gdTeacherRepository.Delete(gdTeacher);
			}

			foreach (var teacher in teachers.Where(teacher => teacher.Id == 0).ToList())
			{
				_gdTeacherRepository.Add(teacher);
			}
		}
	}
}
