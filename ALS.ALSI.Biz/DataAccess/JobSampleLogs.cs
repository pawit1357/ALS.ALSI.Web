using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class job_sample_logs 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(job_sample_logs));

        private static IRepository<job_sample_logs> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<job_sample_logs>>(); }
        }

        #region "Property"
        public String resposible { get; set; }
        public int role_id { get; set; }
        #endregion


        public IEnumerable<job_sample_logs> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public job_sample_logs SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            job_sample_logs existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public job_sample_logs SelectDate(int _sample_Id, int _status)
        {
            return _repository.Find(x => x.job_sample_id == _sample_Id && x.job_status == _status).OrderBy(x=>x.date).FirstOrDefault();
        }

        public List<job_sample_logs> SelectNotification()
        {
            return _repository.Find(x => x.get_alerts == "0").OrderByDescending(x => x.ID).ToList();
        }
        public List<job_sample_logs> SearchData()
        {
            return _repository.GetAll().ToList();

        }

        #endregion

    }
}
