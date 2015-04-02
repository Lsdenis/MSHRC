using System;
using NoTime.BusinessLogic.DataModel;

namespace NoTime.BusinessLogic.UnitOfWork
{    
    public interface IUnitOfWork : IDisposable
    {
		EntityContainer Context { get; }
		NT_BlogEntities BlogContext { get; }
        void Commit();        
    }
}
