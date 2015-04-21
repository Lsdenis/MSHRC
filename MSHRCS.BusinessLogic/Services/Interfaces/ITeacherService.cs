using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;

namespace MSHRCS.BusinessLogic.Services.Interfaces
{
	public interface ITeacherService
	{
		IEnumerable<Teacher> GetAllTeachers();
		void SaveOrUpdateGDTeacher(GDTeacher teacher);
		void SaveOrUpdateGDTeacher(List<GDTeacher> teachers, List<int> existedTeachers);
	}
}
