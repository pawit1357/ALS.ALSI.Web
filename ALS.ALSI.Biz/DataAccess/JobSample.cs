﻿using ALS.ALIS.Repository.Interface;
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

        //public String jobPreFix
        //{
        //    get
        //    {
        //        return this.job_number.Split('-')[0];
        //    }
        //}
        #region "Property"
        public CommandNameEnum RowState { get; set; }
        public Boolean isChecked { get; set; }
        #endregion


        public IEnumerable<job_sample> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public job_sample SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }

        public static job_sample SelectByJobNumber(String _job_number)
        {
            return _repository.Find(x => x.job_number.ToUpper().Equals(_job_number.ToUpper())).OrderByDescending(x=>x.ID).FirstOrDefault();
        }
        public static List<job_sample> selectByRemark(String _remarks)
        {
            return _repository.GetAll().Where(x =>x.remarks!=null && x.remarks.Equals(_remarks)).ToList();
        }

        public List<job_sample> findByIdAndStatus(String jobNumber, StatusEnum status )
        {
            return _repository.Find(x => x.job_number == jobNumber && x.job_status == Convert.ToInt16(status)).ToList();
        }


        public int findAmendOrRetestCount(String jobNumber, String amendOrRetest)
        {
            int result = 0;
            List<job_sample> listOfSample = _repository.Find(x => x.job_number == jobNumber && x.amend_or_retest == amendOrRetest).ToList();

            if(listOfSample!=null && listOfSample.Count > 0)
            {
                result = listOfSample.Count;
            }
            return result;
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            try
            {
                job_sample existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
                _repository.Edit(existing, this);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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

        public static List<job_sample> FindAllByIds(List<int> ids)
        {
            return _repository.GetAll().Where(x => ids.Contains(x.ID)).ToList();
        }

        public static List<job_sample> FindAllByJobNumbers(List<string> jns)
        {
            var result = _repository.GetAll().Where(x => jns.Contains(x.job_number)  && String.IsNullOrEmpty(x.amend_or_retest));

            var q = result.GroupBy(x => x.job_number).Select(g => g.OrderByDescending(i => i.ID).First());


            return q.ToList();
        }

        public static List<job_sample> FindAllByJobID(int _job_id)
        {
            return _repository.GetAll().Where(x => x.job_id == _job_id).ToList();
        }


        public static List<job_sample> FindAllBySo(string so)
        {
            return _repository.GetAll().Where(x => x.sample_so != null && x.sample_so.Equals(so)).ToList();
        }

        public static List<job_sample> FindAllBySos(IEnumerable<string> sos)
        {
            return _repository.GetAll().Where(x => x.sample_so != null && sos.Contains(x.sample_so)).ToList();
        }
        #endregion
    }
}
