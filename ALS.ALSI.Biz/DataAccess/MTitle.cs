using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class m_title 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_title));

        private static IRepository<m_title> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_title>>(); }
        }

        #region "Property"

        #endregion


        public IEnumerable<m_title> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public m_title SelectByID(int _id)
        {
            
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            _repository.Edit(this, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public IEnumerable SearchData()
        {
            //using (ALSIEntities ctx = new ALSIEntities())
            //{
            //    var result = from j in ctx.m_title select j;

            //    if (this.id > 0)
            //    {
            //        result = result.Where(x => x.id == this.id);
            //    }
            //    if (!String.IsNullOrEmpty(this.name))
            //    {
            //        result = result.Where(x => x.name == this.name);
            //    }
            //    return result.ToList();
            //}
            return _repository.GetAll().ToList();
        }

        #endregion
    }
}
