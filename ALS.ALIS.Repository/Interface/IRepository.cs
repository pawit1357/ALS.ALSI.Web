using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALIS.Repository.Interface
{
    public interface IRepository<T> 
    {
        IQueryable<T> GetQuery();

        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> where);
        T Single(Func<T, bool> where);
        T First(Func<T, bool> where);

        void Delete(T entity);
        void Add(T entity);
        void Edit(T oldEntity, T entity);
        void Attach(T entity);
        void SaveChanges();



    }
}
