using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class tb_m_specification 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(tb_m_specification));

        private static IRepository<tb_m_specification> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<tb_m_specification>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<tb_m_specification> SelectAll()
        {

            IEnumerable<tb_m_specification> result = _repository.GetAll().ToList();

            if (this.specification_id > 0)
            {
                result = result.Where(x => x.specification_id == this.specification_id);
            }
            if (this.template_id > 0)
            {
                result = result.Where(x => x.template_id == this.template_id);
            }

            return result.ToList();
        }

        public tb_m_specification SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }

        public string getProcedureByTemplateId(int _templateId)
        {
            List<tb_m_specification> listOfSpec = _repository.Find(x => x.template_id == _templateId).ToList();
            return (listOfSpec.Count > 0) ? listOfSpec[1].C : string.Empty;
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            tb_m_specification existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public List<tb_m_specification> SelectBySpecificationID(int _specification_id,int _template_id)
        {
            return _repository.Find(x => x.specification_id == _specification_id && x.template_id == _template_id).ToList();
        }

        public void InsertList(List<tb_m_specification> _lists)
        {
            foreach (tb_m_specification tmp in _lists)
            {
                switch (tmp.RowState)
                {
                    case CommandNameEnum.Add:
                        _repository.Add(tmp);
                        break;
                    case CommandNameEnum.Edit:
                        tb_m_specification existing = _repository.Find(x => x.ID == tmp.ID).FirstOrDefault();
                        _repository.Edit(existing, tmp);
                        break;
                }

            }
        }

        public void DeleteByTemplateID(int _template_id)
        {
            //using (ALSIEntities ctx = new ALSIEntities())
            //{
            //    var result = from j in ctx.tb_m_specification select j;
            //    ctx.re
            //}
            List<tb_m_specification> lists = _repository.Find(x => x.template_id == _template_id).ToList();
            foreach (tb_m_specification tmp in lists)
            {
                tmp.Delete();
                //_repository.Delete(tmp);
            }
        }
        public IEnumerable SearchData()
        {
            return _repository.GetAll().ToList();
            //using (ALSIEntities ctx = new ALSIEntities())
            //{
            //    var result = from j in ctx.tb_m_specification select j;

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
        }

        #endregion
    }
}
