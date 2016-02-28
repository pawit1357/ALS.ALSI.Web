using System;
using ALS.ALIS.Repository.Interface;
using System.Data.Entity;

namespace ALS.ALIS.Repository.Base
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static Func<DbContext> _objectContextDelegate;
        private static readonly Object _lockObject = new object();

        public static void SetObjectContext(Func<DbContext> objectContextDelegate)
        {
            _objectContextDelegate = objectContextDelegate;
        }

        public IUnitOfWork Create()
        {
            DbContext context;

            lock (_lockObject)
            {
                context = _objectContextDelegate();
                
            }

            return new EFUnitOfWork(context);
        }

    }
}
