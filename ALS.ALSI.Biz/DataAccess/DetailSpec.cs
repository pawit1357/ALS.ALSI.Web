using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{

    public partial class tb_m_detail_spec
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(tb_m_detail_spec));

        private static IRepository<tb_m_detail_spec> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<tb_m_detail_spec>>(); }
        }

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion


        public IEnumerable<tb_m_detail_spec> SelectAll()
        {
            IEnumerable<tb_m_detail_spec> result = _repository.GetAll();

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

        public tb_m_detail_spec SelectByID(int _id)
        {
            return _repository.Find(x => x.ID == _id && x.status.Equals('Y')).FirstOrDefault();
        }
        public List<tb_m_detail_spec> SelectByTemplateID(int _id)
        {
            return _repository.Find(x => x.template_id == _id).ToList();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            tb_m_detail_spec existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public void InsertList(List<tb_m_detail_spec> _lists)
        {
            foreach (tb_m_detail_spec tmp in _lists)
            {
                switch (tmp.RowState)
                {
                    case CommandNameEnum.Add:
                        _repository.Add(tmp);
                        break;
                    case CommandNameEnum.Edit:
                        tb_m_detail_spec existing = _repository.Find(x => x.ID == tmp.ID).FirstOrDefault();
                        _repository.Edit(existing, tmp);
                        break;
                }

            }
        }

        public void DeleteByTemplateID(int _template_id)
        {
            List<tb_m_detail_spec> lists = _repository.Find(x => x.template_id == _template_id).ToList();
            foreach (tb_m_detail_spec tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public string getValueByPrefix(string _prefix)
        {
            string result = "-";
            switch (_prefix)
            {
                case "A": result = this.A; break;
                case "B": result = this.B; break;
                case "C": result = this.C; break;
                case "D": result = this.D; break;
                case "E": result = this.E; break;
                case "F": result = this.F; break;
                case "G": result = this.G; break;
                case "H": result = this.H; break;
                case "I": result = this.I; break;
                case "J": result = this.J; break;
                case "K": result = this.K; break;
                case "L": result = this.L; break;
                case "M": result = this.M; break;
                case "N": result = this.N; break;
                case "O": result = this.O; break;
                case "P": result = this.P; break;
                case "Q": result = this.Q; break;
                case "R": result = this.R; break;
                case "S": result = this.S; break;
                case "T": result = this.T; break;
                case "U": result = this.U; break;
                case "V": result = this.V; break;
                case "W": result = this.W; break;
                case "X": result = this.X; break;
                case "Y": result = this.Y; break;
                case "Z": result = this.Z; break;
                case "AA": result = this.AA; break;
                case "AB": result = this.AB; break;
                case "AC": result = this.AC; break;
                case "AD": result = this.AD; break;
                case "AE": result = this.AE; break;
                case "AF": result = this.AF; break;
                case "AG": result = this.AG; break;
                case "AH": result = this.AH; break;
                case "AI": result = this.AI; break;
                case "AJ": result = this.AJ; break;
                case "AK": result = this.AK; break;
                case "AL": result = this.AL; break;
                case "AM": result = this.AM; break;
                case "AN": result = this.AN; break;
                case "AO": result = this.AO; break;
                case "AP": result = this.AP; break;
                case "AQ": result = this.AQ; break;
                case "AR": result = this.AR; break;
                case "AS": result = this.AS; break;
                case "AT": result = this.AT; break;
                case "AU": result = this.AU; break;
                case "AV": result = this.AV; break;
                case "AW": result = this.AW; break;
                case "AX": result = this.AX; break;
                case "AY": result = this.AY; break;
                case "AZ": result = this.AZ; break;
                case "BA": result = this.BA; break;
                case "BB": result = this.BB; break;
                case "BC": result = this.BC; break;
                case "BD": result = this.BD; break;
                case "BE": result = this.BE; break;
                case "BF": result = this.BF; break;
                case "BG": result = this.BG; break;
                case "BH": result = this.BH; break;
                case "BI": result = this.BI; break;
                case "BJ": result = this.BJ; break;
                case "BK": result = this.BK; break;
                case "BL": result = this.BL; break;
                case "BM": result = this.BM; break;
                case "BN": result = this.BN; break;
                case "BO": result = this.BO; break;
                case "BP": result = this.BP; break;
                case "BQ": result = this.BQ; break;
                case "BR": result = this.BR; break;
                case "BS": result = this.BS; break;
                case "BT": result = this.BT; break;
                case "BU": result = this.BU; break;
                case "BV": result = this.BV; break;
                case "BW": result = this.BW; break;
                case "BX": result = this.BX; break;
                case "BY": result = this.BY; break;
                case "BZ": result = this.BZ; break;
            }
            return result;
        }
        #endregion
    }
}
