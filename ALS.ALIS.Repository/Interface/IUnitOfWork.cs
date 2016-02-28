using System;

namespace ALS.ALIS.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
