using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class tb_m_specification
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(tb_m_specification));

        private static IRepository<tb_m_specification> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<tb_m_specification>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<tb_m_specification> SelectAll()
        {

            IEnumerable<tb_m_specification> result = _repository.GetAll().ToList();

            if (this.specification_id > 0)
            {
                result = result.Where(x => x.specification_id == this.specification_id);
            }
            if (this.template_id > 0)
            {
                result = result.Where(x => x.template_id == this.template_id);
            }

            return result.ToList();
        }

        public tb_m_specification SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id).FirstOrDefault();
        }

        public string getProcedureByTemplateId(int _templateId)
        {
            List<tb_m_specification> listOfSpec = _repository.Find(x => x.template_id == _templateId).ToList();
            return (listOfSpec.Count > 0) ? listOfSpec[1].C : string.Empty;
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            tb_m_specification existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public List<tb_m_specification> SelectBySpecificationID(int _specification_id, int _template_id)
        {
            return _repository.Find(x => x.specification_id == _specification_id && x.template_id == _template_id && x.status !=null && x.status.Equals("A")).ToList();
        }

        public void InsertList(List<tb_m_specification> _lists)
        {
            foreach (tb_m_specification tmp in _lists)
            {
                switch (tmp.RowState)
                {
                    case CommandNameEnum.Add:
                        _repository.Add(tmp);
                        break;
                    case CommandNameEnum.Edit:
                        tb_m_specification existing = _repository.Find(x => x.ID == tmp.ID).FirstOrDefault();
                        _repository.Edit(existing, tmp);
                        break;
                }

            }
        }

        public void DeleteByTemplateID(int _template_id)
        {
            //using (ALSIEntities ctx = new ALSIEntities())
            //{
            //    var result = from j in ctx.tb_m_specification select j;
            //    ctx.re
            //}
            List<tb_m_specification> lists = _repository.Find(x => x.template_id == _template_id && x.status!=null && x.status.Equals("A")).ToList();
            foreach (tb_m_specification tmp in lists)
            {
                tb_m_specification existing = _repository.Find(x => x.ID == tmp.ID).FirstOrDefault();
                existing.status = "I";
                _repository.Edit(existing, tmp);
                //tmp.Delete();
                //_repository.Delete(tmp);
            }
        }
        public IEnumerable SearchData()
        {
            return _repository.GetAll().ToList();
            //using (ALSIEntities ctx = new ALSIEntities())
            //{
            //    var result = from j in ctx.tb_m_specification select j;

            //    //if (this.ID > 0)
            //    //{
            //    //    result = result.Where(x => x.ID == this.ID);
            //    //}
            //    //if (!String.IsNullOrEmpty(this.name))
            //    //{
            //    //    result = result.Where(x => x.name == this.name);
            //    //}
            //    return result.ToList();
            //}
        }




        public static List<tb_m_specification> getDistinctValue(List<tb_m_specification> lists)
        {
            List<tb_m_specification> returns = new List<tb_m_specification>();
            int index = 1;
            Hashtable ht = new Hashtable();
            foreach (var item in lists)
            {
                tb_m_specification tmp = new tb_m_specification();
                tmp.ID = index;
                tmp.B = item.B;
                if (!ht.Contains(tmp.B))
                {
                    returns.Add(tmp);
                    ht.Add(tmp.B, tmp.B);
                }
                index++;
            }
            return returns;
        }

        public static List<String> findColumnCount(tb_m_specification specCol)
        {
            List<String> colNames = new List<String>();

            //if (!String.IsNullOrEmpty(specCol.A))
            //{
            //    colNames.Add(specCol.A);
            //}
            //if (!String.IsNullOrEmpty(specCol.B))
            //{
            //    colNames.Add(specCol.B);
            //}
            if (!String.IsNullOrEmpty(specCol.C))
            {
                colNames.Add(specCol.C);
            }
            if (!String.IsNullOrEmpty(specCol.D))
            {
                colNames.Add(specCol.D);
            }
            if (!String.IsNullOrEmpty(specCol.E))
            {
                colNames.Add(specCol.E);
            }
            if (!String.IsNullOrEmpty(specCol.F))
            {
                colNames.Add(specCol.F);
            }
            if (!String.IsNullOrEmpty(specCol.G))
            {
                colNames.Add(specCol.G);
            }
            if (!String.IsNullOrEmpty(specCol.H))
            {
                colNames.Add(specCol.H);
            }
            if (!String.IsNullOrEmpty(specCol.I))
            {
                colNames.Add(specCol.I);
            }
            if (!String.IsNullOrEmpty(specCol.J))
            {
                colNames.Add(specCol.J);
            }
            if (!String.IsNullOrEmpty(specCol.K))
            {
                colNames.Add(specCol.K);
            }
            if (!String.IsNullOrEmpty(specCol.L))
            {
                colNames.Add(specCol.L);
            }
            if (!String.IsNullOrEmpty(specCol.M))
            {
                colNames.Add(specCol.M);
            }
            if (!String.IsNullOrEmpty(specCol.N))
            {
                colNames.Add(specCol.N);
            }
            if (!String.IsNullOrEmpty(specCol.O))
            {
                colNames.Add(specCol.O);
            }
            if (!String.IsNullOrEmpty(specCol.P))
            {
                colNames.Add(specCol.P);
            }
            if (!String.IsNullOrEmpty(specCol.Q))
            {
                colNames.Add(specCol.Q);
            }
            if (!String.IsNullOrEmpty(specCol.R))
            {
                colNames.Add(specCol.R);
            }
            if (!String.IsNullOrEmpty(specCol.S))
            {
                colNames.Add(specCol.S);
            }
            if (!String.IsNullOrEmpty(specCol.T))
            {
                colNames.Add(specCol.T);
            }
            if (!String.IsNullOrEmpty(specCol.U))
            {
                colNames.Add(specCol.U);
            }
            if (!String.IsNullOrEmpty(specCol.V))
            {
                colNames.Add(specCol.V);
            }
            if (!String.IsNullOrEmpty(specCol.W))
            {
                colNames.Add(specCol.W);
            }
            if (!String.IsNullOrEmpty(specCol.X))
            {
                colNames.Add(specCol.X);
            }
            if (!String.IsNullOrEmpty(specCol.Y))
            {
                colNames.Add(specCol.Y);
            }
            if (!String.IsNullOrEmpty(specCol.Z))
            {
                colNames.Add(specCol.Z);
            }

            return colNames;
        }


        #endregion
    }
}
