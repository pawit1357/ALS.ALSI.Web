using ALS.ALIS.Repository.Interface;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class ws_hash_value
    {

        private static IRepository<ws_hash_value> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<ws_hash_value>>(); }
        }

        public IEnumerable<ws_hash_value> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public static void DeleteBySampleID(int _sampleID)
        {
            List<ws_hash_value> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (ws_hash_value tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<ws_hash_value> _lists)
        {
            foreach (ws_hash_value tmp in _lists)
            {
                tmp.Insert();
            }
        }

        public ws_hash_value SelectByID(int _id)
        {
            
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            _repository.Edit(this, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public List<ws_hash_value> SelectBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).ToList();
        }
        public IEnumerable SearchData()
        {
            return _repository.GetAll().ToList();
        }

        #endregion
    }
}
