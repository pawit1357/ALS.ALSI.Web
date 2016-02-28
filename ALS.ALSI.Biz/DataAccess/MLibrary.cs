using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class tb_m_dhs_library 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(tb_m_dhs_library));

        private static IRepository<tb_m_dhs_library> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<tb_m_dhs_library>>(); }
        }

        #region "Property"
        public String SelectedText { get { return "["+this.classification + "]-" + this.libraryID; } }
        #endregion


        public IEnumerable<tb_m_dhs_library> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public tb_m_dhs_library SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            tb_m_dhs_library existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public void InsertList(List<tb_m_dhs_library> _lists)
        {
            foreach (tb_m_dhs_library tmp in _lists)
            {
                _repository.Add(tmp);
            }
        }

        public void DeleteBySpecificationID(int _id)
        {
            List<tb_m_dhs_library> lists = _repository.Find(x => x.specification_id > _id).ToList();
            foreach (tb_m_dhs_library tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }
        public void EmptyDB()
        {
            List<tb_m_dhs_library> lists = _repository.Find(x => x.id > 0).ToList();
            foreach (tb_m_dhs_library tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public List<tb_m_dhs_library> SelectBySpecificationID(int _id)
        {
            return _repository.Find(x => x.specification_id == _id).ToList();
        }

        public IEnumerable SearchData()
        {
            return _repository.GetAll().ToList();
        }

        #endregion


    }
}
