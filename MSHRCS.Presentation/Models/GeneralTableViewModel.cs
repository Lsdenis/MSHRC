using System;
using MSHRCS.BusinessLogic.DTO;
using PagedList;

namespace MSHRCS.Presentation.Models
{
	public class GeneralTableViewModel
	{
		public IPagedList<GeneralTableRowValue> RowValues { get; set; }
		public string Group { get; set; }
		public int TimeId { get; set; }
		public string Teacher { get; set; }
		public string Cabinet { get; set; }
		public string Discipline { get; set; }

		public GeneralTableViewModel()
		{
			Group = Teacher = Cabinet = Discipline = String.Empty;
			TimeId = 0;
		}
	}
}