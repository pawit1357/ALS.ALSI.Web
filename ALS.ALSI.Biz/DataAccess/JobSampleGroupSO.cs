﻿using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class job_sample_group_so
    {


        private static IRepository<job_sample_group_so> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<job_sample_group_so>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<job_sample_group_so> SelectAll(string status = "", string soCode = "")
        {
            var result = _repository.GetAll();

            if (!string.IsNullOrEmpty(status))
            {
                result = result.Where(x => x.status.Equals(status));
            }
            if (!string.IsNullOrEmpty(soCode))
            {
                result = result.Where(x => x.so.Equals(soCode));
            }
            return result.ToList();

        }
        public IEnumerable<job_sample_group_so> SelectAllInv(string status = "")
        {
            if (!string.IsNullOrEmpty(status))
            {
                return _repository.GetAll().Where(x => x.status.Equals("C") && x.inv_status!=null && x.inv_status.Equals(status)).ToList();
            }
            else
            {
                return _repository.GetAll().Where(x => x.status.Equals("C")).ToList();
            }
        }
        public IEnumerable<job_sample_group_so> SelectInvAll()
        {
            return _repository.GetAll().ToList();
        }

        public job_sample_group_so SelectByID(int _id)
        {
            return _repository.Find(x => x.id == _id).OrderByDescending(x=>x.id).FirstOrDefault();
        }
        public static Boolean FindBySO(String _so)
        {
            return _repository.Find(x => x.so.Equals(_so)).Any();
        }

        public static job_sample_group_so getBySo(String _so)
        {
            return _repository.Find(x => x.so.Equals(_so)).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            try
            {
                job_sample_group_so existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
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


    }
}
