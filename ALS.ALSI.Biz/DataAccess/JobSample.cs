using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class job_sample
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(job_sample));

        private static IRepository<job_sample> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<job_sample>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<job_sample> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public job_sample SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            job_sample existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            this.update_date = DateTime.Now;
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }



        #region "Custom"

        public static void InsertList(List<job_sample> _lists)
        {
            foreach (job_sample tmp in _lists)
            {
                _repository.Add(tmp);
            }
        }

        public static List<job_sample> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.ID == _sampleID).ToList();
        }
        
        public static List<job_sample> FindAllByJobID(int _job_id)
        {
            return _repository.GetAll().Where(x => x.job_id == _job_id).ToList();
        }

        #endregion
    }
}
