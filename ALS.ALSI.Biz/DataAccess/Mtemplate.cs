using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class m_template 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_template));

        private static IRepository<m_template> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_template>>(); }
        }

        #region "Property"

        #endregion


        public IEnumerable<m_template> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public m_template SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            m_template existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public IEnumerable<m_template> SelectAllByActive ()
        {

            //return _repository.Find(x => x.status == "A").OrderBy(x => x.name).DistinctBy(x => x.path_url).ToList();
            return _repository.Find(x => x.status == "A").OrderBy(x => x.name).ToList();

        }
        public IEnumerable<m_template> SelectAllByActiveForConvertPage(int _spec)
        {

            return _repository.Find(x => x.status == "A" && x.specification_id.Value == _spec).OrderBy(x => x.name).ToList();
        }
        public IEnumerable SearchData()
        {

           //return _repository.GetAll().ToList();
            using (ALSIEntities ctx = new ALSIEntities())
            {
                var result = from j in ctx.m_template select j;// j.status == "A" select j;

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

        public int InsertGetLastID()
        {
            _repository.Add(this);
            _repository.SaveChanges();
            return this.ID;
        }


 
        #endregion

    }
}
