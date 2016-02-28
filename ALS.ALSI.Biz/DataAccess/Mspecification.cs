using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class m_specification 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_specification));

        private static IRepository<m_specification> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_specification>>(); }
        }

        #region "Property"

        #endregion


        public IEnumerable<m_specification> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public m_specification SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            m_specification existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public IEnumerable SearchData()
        {
            //return _repository.GetAll().ToList();
            using (ALSIEntities ctx = new ALSIEntities())
            {
                var result = from j in ctx.m_specification select j;

                //if (this.ID > 0)
                //{
                //    result = result.Where(x => x.ID == this.ID);
                //}
                if (!String.IsNullOrEmpty(this.name))
                {
                    result = result.Where(x => x.name.Contains(this.name));
                }
                return result.ToList();
            }
        }

        #endregion
    }
}
