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

        public ReportHeader getReportHeder(job_sample _sample)
        {
            ReportHeader rpt = new ReportHeader();

            job_info _job = new job_info();
            _job = _job.SelectByID(_sample.job_id);
            if (_job != null)
            {
                m_customer _cus = new m_customer();
                _cus = _cus.SelectByID(_job.customer_id);

                m_customer_address addr = new m_customer_address();
                addr = addr.SelectByCompanyID(_cus.ID);

                if (_cus != null)
                {
                    rpt.addr1 = _cus.company_name;
                    rpt.addr2 = addr.address;
                }
                rpt.cusRefNo = _job.customer_ref_no;// (_sample.sample_po== null) ? String.Empty : _sample.sample_po.ToString();
                rpt.cur_date = DateTime.Now;

                rpt.dateOfDampleRecieve = Convert.ToDateTime(_job.date_of_receive);
                rpt.dateOfAnalyze = Convert.ToDateTime(_sample.date_chemist_alalyze);
                rpt.dateOfTestComplete = Convert.ToDateTime(_sample.date_chemist_complete);

                //ATT / ELP / 16 / XXXX(เลขจ็อบ) - XX(Test)
                String[] tmp = _sample.job_number.Split('-');
                rpt.alsRefNo = String.Format("ATT/{0}/{1}/{2}-{3}", tmp[0], DateTime.Now.ToString("yy"), tmp[1], tmp[2]);// _sample.job_number.ToString();
                rpt.description = "Description:" + _sample.description + "\n" +
                                  "Model:" + _sample.model + "\n" +
                                  "Surface Area:" + _sample.surface_area + "\n"+
                                  (!String.IsNullOrEmpty(_sample.remarks)? "Remark: "+_sample.remarks+"\n":"");

                rpt.model = _sample.model;
                rpt.surface_areas = _sample.surface_area;
                rpt.remark = _sample.remarks;
            }
            return rpt;
        }

        #endregion



        //select 
        //i.customer_po_ref cusRefNo,
        //s.job_number alsRefNo,
        //i.date_of_receive cur_date,
        //m.company_name addr1,
        //m.address addr2,
        //i.date_of_receive dateOfDampleRecieve,
        //i.date_of_request dateOfAnalyze,
        //s.due_date dateOfTestComplete,
        //s.description,
        //s.model,
        //s.surface_area,
        //s.remarks
        //from job_info i 
        //left join job_sample s on i.ID = s.job_id
        //left join m_customer m on i.customer_id = m.ID
        //where s.ID =12

    }
}
