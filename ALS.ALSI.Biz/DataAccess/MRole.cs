using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class m_role 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_role));

        private static IRepository<m_role> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_role>>(); }
        }

        #region "Property"
        #endregion


        public IEnumerable<m_role> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public m_role SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            m_role existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
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
                var result = from r in ctx.m_role
                             select new
                             {
r.ID,
r.name
                             };

                if (!String.IsNullOrEmpty(this.name))
                {
                    result = result.Where(x => x.name == this.name);
                }
                return result.ToList();
            }
        }

        //GET MENU_ROLE MAX ID
        public int GetMax()
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {
                m_role mRole = (from c in ctx.m_role select c).OrderByDescending(x=>x.ID).FirstOrDefault();
                if (mRole != null)
                {
                    return (int)mRole.ID + 1;
                }
                return -1;
            }
        }
        #endregion
    }
}
