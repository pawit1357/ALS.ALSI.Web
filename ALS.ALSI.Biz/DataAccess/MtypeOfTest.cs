using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class m_type_of_test 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_type_of_test));

        private static IRepository<m_type_of_test> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_type_of_test>>(); }
        }

        #region "Property"
        public String customName {get;set;}
        #endregion


        public IEnumerable<m_type_of_test> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public m_type_of_test SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            m_type_of_test existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public List<m_type_of_test> SelectParent()
        {
            return _repository.Find(x => x.parent == -1).ToList();
        }

        public List<m_type_of_test> SelectBySpecificationId(int _id)
        {
            return _repository.Find(x => x.specification_id == _id).ToList();
        }

        public List<m_type_of_test> SelectParent(int _specification_id)
        {
            return _repository.Find(x => x.specification_id == _specification_id && x.parent == -1).ToList();
        }

        public List<m_type_of_test> SelectChild(int _id)
        {
            return _repository.Find(x => x.parent == _id).ToList();
        }

        public IEnumerable<m_type_of_test> SelectAllByID()
        {
            return null;
        }
        public IEnumerable SelectDistinct()
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {
                var result = from c in ctx.m_type_of_test
                             //where c.parent == -1
                             group c by c.data_group into g
                             select new { Id = g.Key, Name = g.Key };

                return result.ToList();

            }

        }
        public IEnumerable SearchData()
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {
                var result = from j in ctx.m_type_of_test
                             join s in ctx.m_specification on j.specification_id equals s.ID
                             select new
                             {
                                 ID = j.ID,
                                 specification_id = s.ID,
                                 specification = s.name,
                                 prefix = j.prefix,
                                 name = j.name
                             };

                if (this.ID > 0)
                {
                    result = result.Where(x => x.ID == this.ID);
                }
                if (this.specification_id > 0)
                {
                    result = result.Where(x => x.specification_id == this.specification_id);
                }
                if (!String.IsNullOrEmpty(this.name))
                {
                    result = result.Where(x => x.name == this.name);
                }
                return result.ToList();
            }
        }

        #endregion
    }
}
