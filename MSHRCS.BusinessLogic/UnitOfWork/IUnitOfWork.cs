using System;
using MSHRCS.BusinessLogic.DataModel;

namespace MSHRCS.BusinessLogic.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		MSHRCSchedulerContext Context { get; }
		void Commit();
	}
}
