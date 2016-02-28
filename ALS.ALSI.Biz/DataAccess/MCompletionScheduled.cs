using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class m_completion_scheduled 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_completion_scheduled));

        private static IRepository<m_completion_scheduled> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_completion_scheduled>>(); }
        }

        #region "Property"

        #endregion


        public IEnumerable<m_completion_scheduled> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public m_completion_scheduled SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
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
            return _repository.GetAll().ToList();
        }

        #endregion

    }
}
