using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections;
using System.Linq;

namespace ALS.ALSI.Biz.ReportObjects
{
    public class ReportHeader
    {
        public String cusRefNo { get; set; }
        public String alsRefNo { get; set; }
        public DateTime cur_date { get; set; }
        public String addr1 { get; set; }
        public String addr2 { get; set; }
        public DateTime dateOfDampleRecieve { get; set; }
        public DateTime dateOfAnalyze { get; set; }
        public DateTime dateOfTestComplete { get; set; }
        public String description { get; set; }
        public String model { get; set; }
        public String surface_areas { get; set; }
        public String remark { get; set; }

        #region "Custom"
        public IEnumerable getReportHeader(int sample_id)
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {
                var result = from i in ctx.job_info
                             join s in ctx.job_sample on i.ID equals s.job_id
                             join c in ctx.m_customer on i.customer_id equals c.ID
                             where s.ID == sample_id
                             orderby s.job_number ascending
                             select new
                             {
                                 cusRefNo = s.sample_po,
                                 alsRefNo = s.job_number,
                                 cur_date = i.date_of_receive,
                                 addr1 = c.company_name,
                                 addr2 = c.address,
                                 dateOfDampleRecieve = i.date_of_receive,
                                 dateOfAnalyze = i.date_of_request,
                                 dateOfTestComplete = s.due_date,
                                 description = s.description,
                                 model = s.model,
                                 surface_areas = s.surface_area,
                                 remark = s.remarks
                             };


                return result.ToList();
            }
        }

        public static ReportHeader getReportHeder(job_sample _sample)
        {
            ReportHeader rpt = new ReportHeader();

            job_info _job = new job_info();
            _job = _job.SelectByID(_sample.job_id);
            if (_job != null)
            {
                m_customer _cus = new m_customer();
                _cus = _cus.SelectByID(_job.customer_id);

                m_customer_address addr = new m_customer_address();
                addr = addr.SelectByID(_job.customer_address_id.Value);

                if (_cus != null)
                {
                    rpt.addr1 = _cus.company_name;
                    rpt.addr2 = addr.address;
                }
                rpt.cusRefNo = _job.customer_ref_no;// (_sample.sample_po== null) ? String.Empty : _sample.sample_po.ToString();
                rpt.cur_date = DateTime.Now;

                rpt.dateOfDampleRecieve = Convert.ToDateTime(_job.date_of_receive);
                rpt.dateOfAnalyze = Convert.ToDateTime(_sample.date_chemist_analyze);
                rpt.dateOfTestComplete = Convert.ToDateTime(_sample.date_chemist_complete);

                //ATT / ELP / 16 / XXXX(เลขจ็อบ) - XX(Test)

                int phisicalYear = Convert.ToInt16(DateTime.Now.Year.ToString().Substring(2));
                if (DateTime.Now.Month < Constants.PHYSICAL_YEAR)
                {
                    phisicalYear = Convert.ToInt16(DateTime.Now.Year.ToString().Substring(2)) - 1;
                }

                String AmRetest = String.Empty;
                switch (_sample.amend_or_retest)
                {
                    case "AM":
                        AmRetest = (_sample.amend_count > 0) ? "AM" + ((_sample.amend_count==1)? "":_sample.amend_count+"") + "/" : String.Empty;
                        break;
                    case "R":
                        AmRetest = (_sample.retest_count > 0) ? "R" + ((_sample.retest_count==1)? "": _sample.retest_count+"") + "/" : String.Empty;
                        break;
                }

                String[] tmp = _sample.job_number.Split('-');
                rpt.alsRefNo = String.Format("{0}ATT/{1}/{2}/{3}-{4}", AmRetest, tmp[0], phisicalYear, tmp[1], tmp[2]);// _sample.job_number.ToString();
                rpt.description = (String.IsNullOrEmpty(_sample.description) ? String.Empty : "Description:" + _sample.description + "\n") +
                                  (String.IsNullOrEmpty(_sample.model) ? String.Empty : "Model:" + _sample.model + "\n") +
                                  (String.IsNullOrEmpty(_sample.surface_area) ? String.Empty : "Surface Area:" + _sample.surface_area + "\n") +
                                  (!String.IsNullOrEmpty(_sample.remarks) ? "Remark: " + _sample.remarks + "\n" : "");

                rpt.model = _sample.model;
                rpt.surface_areas = _sample.surface_area;
                rpt.remark = _sample.remarks;
            }
            return rpt;
        }
        #endregion
    }
}
