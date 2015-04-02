using NoTime.BusinessLogic.DataModel;

namespace NoTime.BusinessLogic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
		private readonly EntityContainer _context;
		private readonly NT_BlogEntities _blogContext;

		public UnitOfWork(EntityContainer context, NT_BlogEntities blogContext)
        {
            _context = context;
			_blogContext = blogContext;
        }

		public EntityContainer Context
        {
            get { return _context; }
        }

		public NT_BlogEntities BlogContext
		{
			get { return _blogContext; }
		}

        public void Commit()
        {
            _context.SaveChanges();
	        _blogContext.SaveChanges();
        }
       
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }

	        if (_blogContext != null)
	        {
		        _blogContext.Dispose();
	        }
        }
    }
}
