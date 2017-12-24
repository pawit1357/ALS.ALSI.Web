using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class m_microscopic_analysis
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_microscopic_analysis));

        private static IRepository<m_microscopic_analysis> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_microscopic_analysis>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<m_microscopic_analysis> SelectAll()
        {
            IEnumerable<m_microscopic_analysis> result = _repository.GetAll();

            //if (this.specification_id > 0)
            //{
            //    result = result.Where(x => x.specification_id == this.specification_id);
            //}
            if (this.template_id > 0)
            {
                result = result.Where(x => x.template_id == this.template_id);
            }
            return result.ToList();
        }

        public m_microscopic_analysis SelectByID(int _id)
        {
            return _repository.Find(x => x.id == _id).FirstOrDefault();
        }
        public List<m_microscopic_analysis> SelectByTemplateID(int _id)
        {
            return _repository.Find(x => x.template_id == _id).ToList();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            m_microscopic_analysis existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public void InsertList(List<m_microscopic_analysis> _lists)
        {
            foreach (m_microscopic_analysis tmp in _lists)
            {
                switch (tmp.RowState)
                {
                    case CommandNameEnum.Add:
                        _repository.Add(tmp);
                        break;
                    case CommandNameEnum.Edit:
                        m_microscopic_analysis existing = _repository.Find(x => x.id == tmp.id).FirstOrDefault();
                        _repository.Edit(existing, tmp);
                        break;
                }

            }
        }

        public void DeleteByTemplateID(int _template_id)
        {
            List<m_microscopic_analysis> lists = _repository.Find(x => x.template_id == _template_id).ToList();
            foreach (m_microscopic_analysis tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }
        
        #endregion
    }
}
