using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHRCS.BusinessLogic.DataModel
{
	public partial class Teacher
	{
		public string FullName()
		{
			return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName);
		}
	}
}
