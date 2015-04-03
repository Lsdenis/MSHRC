using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MSHRCS.Presentation.Models
{
	public class LogInUserViewModel
	{
		[Required]
		public int UserId { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Пароль не должен быть пустым!")]
		[DisplayName("Пароль")]
		public string Password { get; set; }

		[Required]
		[DisplayName("Запомнить меня?")]
		public bool RememberMe { get; set; }
	}
}