using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class m_evaluation_of_particles
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_evaluation_of_particles));

        private static IRepository<m_evaluation_of_particles> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_evaluation_of_particles>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<m_evaluation_of_particles> SelectAll()
        {
            IEnumerable<m_evaluation_of_particles> result = _repository.GetAll();

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

        public m_evaluation_of_particles SelectByID(int _id)
        {
            return _repository.Find(x => x.id == _id).FirstOrDefault();
        }
        public List<m_evaluation_of_particles> SelectByTemplateID(int _id)
        {
            return _repository.Find(x => x.template_id == _id).ToList();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            m_evaluation_of_particles existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public void InsertList(List<m_evaluation_of_particles> _lists)
        {
            foreach (m_evaluation_of_particles tmp in _lists)
            {
                switch (tmp.RowState)
                {
                    case CommandNameEnum.Add:
                        _repository.Add(tmp);
                        break;
                    case CommandNameEnum.Edit:
                        m_evaluation_of_particles existing = _repository.Find(x => x.id == tmp.id).FirstOrDefault();
                        _repository.Edit(existing, tmp);
                        break;
                }

            }
        }

        public void DeleteByTemplateID(int _template_id)
        {
            List<m_evaluation_of_particles> lists = _repository.Find(x => x.template_id == _template_id).ToList();
            foreach (m_evaluation_of_particles tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }
        
        #endregion
    }
}
