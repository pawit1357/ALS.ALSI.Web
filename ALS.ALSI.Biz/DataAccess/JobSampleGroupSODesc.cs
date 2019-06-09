using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class job_sample_group_so_ignore_code
    {


        private static IRepository<job_sample_group_so_ignore_code> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<job_sample_group_so_ignore_code>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion




        public IEnumerable SearchData()
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {
                var result = from j in ctx.job_sample_group_so_ignore_code
                             select new
                             {
                                 ID = j.id,
                                 name = j.name,
                                 code = j.code
                             };

                if (this.id > 0)
                {
                    result = result.Where(x => x.ID == this.id);
                }

                if (!String.IsNullOrEmpty(this.code))
                {
                    result = result.Where(x => x.code == this.code);
                }

                if (!String.IsNullOrEmpty(this.name))
                {
                    result = result.Where(x => x.name == this.name);
                }

                return result.ToList();
            }
            
        }



        public List<job_sample_group_so_ignore_code> SelectInvAll()
        {
            return _repository.GetAll().Where(x=>x.isActive.Equals("A")).ToList();
        }

        public job_sample_group_so_ignore_code SelectByID(int _id)
        {
            return _repository.Find(x => x.id == _id).OrderByDescending(x=>x.id).FirstOrDefault();
        }



        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            try
            {
                job_sample_group_so_ignore_code existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
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
