using System;
using System.Collections.Generic;
using System.Linq;
using ALS.ALIS.Repository.Interface;
using System.Data.Entity;
using System.Data;

namespace ALS.ALIS.Repository.Base
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private DbContext _context;
        private DbSet<T> _objectSet;

        protected DbContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = GetCurrentUnitOfWork<EFUnitOfWork>().Context;

                }
                _context.Configuration.LazyLoadingEnabled = false;
                _context.Configuration.ProxyCreationEnabled = false;
                return _context;
            }
        }
   
        protected DbSet<T> ObjectSet
        {
            get
            {
                if (_objectSet == null)
                {
                    _objectSet = this.Context.Set<T>();
                }

                return _objectSet;
            }
        }

        public TUnitOfWork GetCurrentUnitOfWork<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
        {
            return (TUnitOfWork)UnitOfWork.Current;
        }

        public IQueryable<T> GetQuery()
        {
            return ObjectSet;
        }

        public IEnumerable<T> GetAll()
        {
            return GetQuery().ToList();
        }

        public IEnumerable<T> Find(Func<T, bool> where)
        {
            return this.ObjectSet.Where<T>(where);
        }

        public T Single(Func<T, bool> where)
        {
            return this.ObjectSet.SingleOrDefault<T>(where);
        }

        public T First(Func<T, bool> where)
        {
            return this.ObjectSet.First<T>(where);
        }

        public virtual void Delete(T entity)
        {
            
            this.Context.Entry(entity).State = EntityState.Deleted;
            //this.ObjectSet.Remove(entity);
        }

        public virtual void Add(T entity)
        {
            this.ObjectSet.Add(entity);
        }

        public virtual void Edit(T oldEntity, T entity)
        {

            this.Context.Entry(oldEntity).CurrentValues.SetValues(entity);

   
        }

        public void Attach(T entity)
        {
            this.ObjectSet.Attach(entity);

        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
