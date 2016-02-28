using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class tb_m_detail_spec_ref
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(tb_m_detail_spec_ref));

        private static IRepository<tb_m_detail_spec_ref> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<tb_m_detail_spec_ref>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<tb_m_detail_spec_ref> SelectAll()
        {

            IEnumerable<tb_m_detail_spec_ref> result = _repository.GetAll();

            if (this.specification_id > 0)
            {
                result = result.Where(x => x.specification_id == this.specification_id);
            }
            if (this.template_id > 0)
            {
                result = result.Where(x => x.template_id == this.template_id);
            }
            if (this.spec_ref > 0)
            {
                result = result.Where(x => x.spec_ref == this.spec_ref);
            }

            return result.ToList();
        }

        public tb_m_detail_spec_ref SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            tb_m_detail_spec_ref existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public void InsertList(List<tb_m_detail_spec_ref> _lists)
        {
            foreach (tb_m_detail_spec_ref tmp in _lists)
            {
                switch (tmp.RowState)
                {
                    case CommandNameEnum.Add:
                        _repository.Add(tmp);
                        break;
                    case CommandNameEnum.Edit:
                        tb_m_detail_spec_ref existing = _repository.Find(x => x.ID == tmp.ID).FirstOrDefault();
                        if (existing != null)
                        {
                            _repository.Edit(existing, tmp);
                        }
                        else
                        {
                            _repository.Add(tmp);
                        }
                        break;
                }

            }
        }

        public void DeleteByTemplateID(int _template_id)
        {
            List<tb_m_detail_spec_ref> lists = _repository.Find(x => x.template_id == _template_id).ToList();
            foreach (tb_m_detail_spec_ref tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }


        #endregion
    }
}
