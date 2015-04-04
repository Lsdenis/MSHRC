using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MSHRCS.Presentation.Models
{
	public class AcademicDisciplineViewModel
	{
		public int Id { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо ввести код!")]
		[DisplayName("Код")]
		public string Code { get; set; }

		[DisplayName("Название")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо ввести название!")]
		public string Description { get; set; }
	}
}