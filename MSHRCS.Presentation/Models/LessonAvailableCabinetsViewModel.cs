using System.Collections.Generic;

namespace MSHRCS.Presentation.Models
{
	public class LessonAvailableCabinetsViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public List<CabinetViewModel> AvailableCabinets { get; set; }
	}
}