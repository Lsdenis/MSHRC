using System.Collections.Generic;
using System.ComponentModel;

namespace MSHRCS.Presentation.Models
{
	public class GroupDisciplineViewModel
	{
		public int Id { get; set; }

		[DisplayName("Группа")]
		public string Group { get; set; }

		[DisplayName("Предмет")]
		public string AcademicDiscipline { get; set; }

		public List<GroupDisciplineTeacherViewModel> Teachers { get; set; }
	}
}