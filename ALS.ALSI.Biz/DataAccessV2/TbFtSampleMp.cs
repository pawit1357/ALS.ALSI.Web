using ALS.ALIS.Repository.Interface;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class tb_ft_sample_mp
    {
        private static IRepository<tb_ft_sample_mp> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<tb_ft_sample_mp>>(); }
        }

        #region "Property"
        #endregion


        public IEnumerable<tb_ft_sample_mp> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public tb_ft_sample_mp SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id);
        }
        public tb_ft_sample_mp selectBySampleID(int sampleID)
        {
            return _repository.First(x => x.sample_id == sampleID);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            tb_ft_sample_mp existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

    }
}
