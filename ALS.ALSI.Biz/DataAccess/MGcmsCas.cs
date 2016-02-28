using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class tb_m_gcms_cas
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(tb_m_gcms_cas));

        #region "Property"
        public String SelectedText { get { return classification + (String.IsNullOrEmpty(library_id) ? String.Empty : " ( " + library_id + " )"); } }
        public String ref_ { get; set; }
        public CommandNameEnum RowState { get; set; }
        public int pkInt { get { return Convert.ToInt32(String.IsNullOrEmpty(this.pk) ? "0" : this.pk); } }
        public int areaInt { get { return Convert.ToInt32(String.IsNullOrEmpty(this.area) ? "0" : this.area); } }
        #endregion

        private static IRepository<tb_m_gcms_cas> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<tb_m_gcms_cas>>(); }
        }

        #region "Custom"

        public IEnumerable<tb_m_gcms_cas> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public tb_m_gcms_cas SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            tb_m_gcms_cas existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            if (existing != null)
            {
                _repository.Edit(existing, this);
            }
            else
            {
                //_repository.Add(this);
            }
        }

        public void Delete()
        {
            tb_m_gcms_cas existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            if (existing != null)
            {
                _repository.Delete(this);
            }
        }

        public static void DeleteBySampleID(int _sampleID)
        {
            List<tb_m_gcms_cas> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (tb_m_gcms_cas tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<tb_m_gcms_cas> _lists)
        {
            foreach (tb_m_gcms_cas tmp in _lists)
            {
                //switch (tmp.RowState)
                //{
                //    case CommandNameEnum.Add:
                        tmp.Insert();
                //        break;
                //    case CommandNameEnum.Edit:
                //        tmp.Update();
                //        break;
                //    case CommandNameEnum.Delete:
                //        tmp.Delete();
                //        break;
                //}
            }
        }

        public static void UpdateList(List<tb_m_gcms_cas> _lists)
        {
            foreach (tb_m_gcms_cas tmp in _lists)
            {

                tmp.Update();
            }
        }
        public static List<tb_m_gcms_cas> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion

    }
}
