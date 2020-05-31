using ALS.ALIS.Repository.Interface;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class tb_ft_sample_result
    {
        private static IRepository<tb_ft_sample_result> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<tb_ft_sample_result>>(); }
        }

        #region "Property"
        #endregion


        public IEnumerable<tb_ft_sample_result> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public tb_ft_sample_result SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            tb_ft_sample_result existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

    }
}
