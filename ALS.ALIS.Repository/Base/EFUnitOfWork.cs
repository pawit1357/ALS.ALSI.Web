using ALS.ALIS.Repository.Interface;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace ALS.ALIS.Repository.Base
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        public DbContext Context { get; private set; }

        public EFUnitOfWork(DbContext context)
        {
            Context = context;

            Context.Configuration.LazyLoadingEnabled = false;
            Context.Configuration.ProxyCreationEnabled = false;

        }

        public void Commit()
        {
            try{
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine();
                    //logger.Error(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                         //eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine();
                        //logger.Error(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            //ve.PropertyName, ve.ErrorMessage));
                    }
                }
                throw;
            }

        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
