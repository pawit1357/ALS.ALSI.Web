using System;
using System.Collections.Generic;


namespace ALS.ALSI.Biz.DataAccess
{
    public class CSo
    {
        public String SO { get; set; }
        public String PO { get; set; }
        public DateTime Date { get; set; }
        public List<Double> _Qty { get; set; }
        public List<Double> _UnitPrice { get; set; }
        public List<String> _ReportNo { get; set; }

        public String Qty { get { return String.Join(",", _Qty); } }
        public String UnitPrice { get { return String.Join("\r\n", _UnitPrice); } }
        public String ReportNo { get { return String.Join("\r\n", _ReportNo); } }

    }
}
