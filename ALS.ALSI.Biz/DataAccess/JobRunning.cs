using ALS.ALIS.Repository.Interface;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class job_running 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(job_running));

        private static IRepository<job_running> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<job_running>>(); }
        }

        #region "Property"

        #endregion


        public IEnumerable<job_running> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public job_running SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            job_running existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            if (existing != null)
            {
                _repository.Edit(existing, this);
            }
            else
            {
                _repository.Add(this);
            }
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public static int GetRunning(int id)
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {
                job_running running = (from c in ctx.job_running where c.ID == id select c).FirstOrDefault();
                if (running != null)
                {
                    return (int)running.running_number + 1;
                }
                return -1;
            }
        }

        public static void IncrementRunning(int id)
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {
                job_running running = (from c in ctx.job_running where c.ID == id select c).FirstOrDefault();
                if (running != null)
                {
                    running.running_number = running.running_number + 1;
                    ctx.SaveChanges();
                }
            }
        }

        public IEnumerable SearchData()
        {
            return _repository.GetAll().ToList();

        }

        #endregion

    }
}
