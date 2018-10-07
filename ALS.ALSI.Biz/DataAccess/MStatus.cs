using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class m_status
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_status));

        private static IRepository<m_status> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_status>>(); }
        }

        #region "Property"

        #endregion


        public IEnumerable<m_status> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public IEnumerable<m_status> SelectByMainStatus()
        {
     
            return _repository.GetAll().Where(x=>x.status_group_id == 1  && x.status.Equals("A")).ToList();
        }
        public IEnumerable<m_status> SelectByMainStatusNoDelete()
        {

            return _repository.GetAll().Where(x => x.status_group_id == 1 && x.status.Equals("A") && x.ID != 18).ToList();
        }
        public m_status SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            m_status existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public IEnumerable SearchData()
        {

            return _repository.GetAll().ToList();
            //using (ALSIEntities ctx = new ALSIEntities())
            //{
            //    var result = from j in ctx.m_status where j.status == "A" select j;

            //    if (this.ID > 0)
            //    {
            //        result = result.Where(x => x.ID == this.ID);
            //    }
            //    if (!String.IsNullOrEmpty(this.name))
            //    {



            //        result = result.Where(x => x.name == this.name);
            //    }
            //    return result.ToList();
            //}
        }



        #endregion
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_status));
        //public IEnumerable<m_status> SelectAll()
        //{
        //    using (ALSIEntities ctx = new ALSIEntities())
        //    {
        //        return (from c in ctx.m_status select c).ToList();
        //    }
        //}

        //public m_status SelectByID(int _id)
        //{
        //    using (ALSIEntities ctx = new ALSIEntities())
        //    {
        //        return (from c in ctx.m_status where c.ID == _id select c).FirstOrDefault();
        //    }
        //}

        //public void Insert()
        //{
        //    using (ALSIEntities context = new ALSIEntities())
        //    {
        //        try
        //        {
        //            context.m_status.Add(this);
        //            context.SaveChanges();
        //        }
        //        catch (DbEntityValidationException e)
        //        {
        //            foreach (var eve in e.EntityValidationErrors)
        //            {

        //                logger.Error(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                     eve.Entry.Entity.GetType().Name, eve.Entry.State));
        //                foreach (var ve in eve.ValidationErrors)
        //                {
        //                    logger.Error(String.Format("- Property: \"{0}\", Error: \"{1}\"",
        //                        ve.PropertyName, ve.ErrorMessage));
        //                }
        //            }
        //            throw;
        //        }
        //    }
        //}

        //public void Update()
        //{
        //    using (ALSIEntities context = new ALSIEntities())
        //    {
        //        var result = from j in context.m_status where j.ID == this.ID select j;
        //        m_status tmp = result.FirstOrDefault();
        //        if (tmp != null)
        //        {
        //            setEditData(tmp);
        //        }
        //        context.SaveChanges();
        //    }
        //}

        //public void Delete()
        //{
        //    using (ALSIEntities context = new ALSIEntities())
        //    {
        //        var tmp = from p in context.m_status where p.ID == this.ID select p;
        //        context.m_status.Remove(tmp.FirstOrDefault());
        //        context.SaveChanges();
        //    }
        //}

        //#region "Custom"
        //private void setEditData(m_status _value)
        //{
        //    _value.ID = this.ID;
        //    _value.name = this.name;
        //}
        //public IEnumerable SearchData()
        //{
        //    using (ALSIEntities ctx = new ALSIEntities())
        //    {
        //        var result = from j in ctx.m_status select j;

        //        if (this.ID > 0)
        //        {
        //            result = result.Where(x => x.ID == this.ID);
        //        }
        //        if (!String.IsNullOrEmpty(this.name))
        //        {
        //            result = result.Where(x => x.name == this.name);
        //        }
        //        return result.ToList();
        //    }
        //}
        //#endregion
        //#region "IDisposable Support"
        //void IDisposable.Dispose()
        //{
        //    GC.SuppressFinalize(this);
        //}
        //#endregion
    }
}
