using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class m_customer_contract_person 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_customer_contract_person));

        private static IRepository<m_customer_contract_person> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_customer_contract_person>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<m_customer_contract_person> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public m_customer_contract_person SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }
        public List<m_customer_contract_person> SelectAllByCusID(int _company_id)
        {
            return _repository.GetAll().Where(x => x.company_id == _company_id).ToList();
        }
        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            m_customer_contract_person existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            m_customer_contract_person existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            if (existing!=null)
            {
                _repository.Delete(existing);
            }
        }

        #region "Custom"

        public List<m_customer_contract_person> FindAllByCompanyID(int _company_id)
        {
            return _repository.GetAll().Where(x => x.company_id == _company_id).ToList();
        }
        public m_customer_contract_person FindByID(int _id)
        {
            return _repository.GetAll().Where(x => x.ID == _id).FirstOrDefault();
        }
        public IEnumerable SearchData()
        {
            //using (ALSIEntities ctx = new ALSIEntities())
            //{
            //    var result = from j in ctx.m_customer_contract_person select j;

            //    //if (this.ID > 0)
            //    //{
            //    //    result = result.Where(x => x.ID == this.ID);
            //    //}
            //    //if (!String.IsNullOrEmpty(this.name))
            //    //{
            //    //    result = result.Where(x => x.name == this.name);
            //    //}
            //    return result.ToList();
            //}
            return _repository.GetAll().ToList();
        }

        #endregion

    }
}
