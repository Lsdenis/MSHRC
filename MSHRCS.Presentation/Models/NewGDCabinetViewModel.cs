using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MSHRCS.Presentation.Models
{
	public class NewGDCabinetViewModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо выбрать группу!")]
		[DisplayName("Группа")]
		public int GroupId { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо выбрать предмет!")]
		[DisplayName("Предмет")]
		public int DisciplineId { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо выбрать кабинет!")]
		[DisplayName("Кабинет")]
		public int CabinetId { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо выбрать время!")]
		[DisplayName("Время")]
		public int LessonId { get; set; }

		[DisplayName("Замена")]
		public int? SubstituteDisciplineId { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо выбрать дату!")]
		[DisplayName("Дата")]
		public DateTime Date { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо выбрать тип урока!")]
		[DisplayName("Тип урока")]
		public int LessonTypeId { get; set; }
	}
}