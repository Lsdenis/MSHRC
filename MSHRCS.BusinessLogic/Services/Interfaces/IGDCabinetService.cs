using System;
using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.DTO;

namespace MSHRCS.BusinessLogic.Services.Interfaces
{
	public interface IGDCabinetService
	{
		IEnumerable<GeneralTableRowValue> GetGeneralTableRowValues(DateTime date);
		IEnumerable<GDCabinet> GetAllGDCabinets();
		Dictionary<Lesson, List<Cabinet>> GetCabinetLessons(DateTime date);
	}
}
