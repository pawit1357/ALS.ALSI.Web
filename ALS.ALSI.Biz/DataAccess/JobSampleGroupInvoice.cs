using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class job_sample_group_invoice
    {


        private static IRepository<job_sample_group_invoice> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<job_sample_group_invoice>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<job_sample_group_invoice> SelectAll(string inv_status ="")
        {
            if (!string.IsNullOrEmpty(inv_status))
            {
                return _repository.GetAll().Where(x => x.inv_status.Equals(inv_status)).ToList();
            }
            else
            {
                return _repository.GetAll().Where(x => x.inv_status != null).ToList();
            }
        }
        public IEnumerable<job_sample_group_invoice> SelectInvAll()
        {
            return _repository.GetAll().ToList();
        }

        public job_sample_group_invoice SelectByID(int _id)
        {
            return _repository.Find(x => x.id == _id).OrderByDescending(x=>x.id).FirstOrDefault();
        }
        public static Boolean FindBySO(String _so)
        {
            return _repository.Find(x => x.so.Equals(_so)).Any();
        }

        public static job_sample_group_invoice getBySo(String _so)
        {
            return _repository.Find(x => x.so!=null && x.so.Equals(_so)).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            try
            {
                job_sample_group_invoice existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
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
