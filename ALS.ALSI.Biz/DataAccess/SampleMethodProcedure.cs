using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class sample_method_procedure
    {

        private static IRepository<sample_method_procedure> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<sample_method_procedure>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<sample_method_procedure> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public sample_method_procedure SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
            GeneralManager.Commit();
        }

        public void Update()
        {
            sample_method_procedure existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }
        public static void InsertList(List<sample_method_procedure> _lists)
        {
            foreach (sample_method_procedure tmp in _lists)
            {
                tmp.Insert();

            }
        }

    }
}
