using ALS.ALIS.Repository.Interface;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class tb_ft_cfg_parameter
    {
        public static string PARAM_GROUP_METHOD_PROCEDURE_HEADER = "METHOD_PROCEDURE_HEADER";
        public static string PARAM_GROUP_METHOD_PROCEDURE_ITEM = "METHOD_PROCEDURE_ITEM";
        public static string PARAM_GROUP_RESULT_TEXT = "RESULT_TEXT";
        public static string PARAM_GROUP_RESULT_TEXT_ITEM = "RESULT_TEXT_ITEM";
        public static string PARAM_GROUP_RESULT_TABLE_HEADER = "RESULT_TABLE_HEADER";


        private static IRepository<tb_ft_cfg_parameter> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<tb_ft_cfg_parameter>>(); }
        }

        #region "Property"
        #endregion

        public List<tb_ft_cfg_parameter> selectByParamGroup(string param_group)
        {
            return _repository.GetAll().Where(x=>x.param_group.ToLower().Equals(param_group)).ToList();
        }

        public IEnumerable<tb_ft_cfg_parameter> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public tb_ft_cfg_parameter SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            tb_ft_cfg_parameter existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

    }
}
