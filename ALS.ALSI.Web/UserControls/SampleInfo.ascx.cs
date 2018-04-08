using ALS.ALSI.Biz.DataAccess;
using System;

namespace ALS.ALSI.Web.UserControls
{
    public partial class SampleInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void initialInfo(job_info _job, job_sample _sample)
        {
            /*INFO*/
            if (_job != null && _sample != null)
            {
                m_customer cus = new m_customer();
                cus = cus.SelectByID(_job.customer_id);
                lbPoNo.Text = (_sample.sample_po == null) ? String.Empty : _sample.sample_po.ToString();
                lbDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                lbCompany.Text = String.Format("{0}<br />{1}", cus.company_name, cus.address);
                lbDateSampleReceived.Text = Convert.ToDateTime(_job.date_of_receive).ToString("MM/dd/yyyy");
                String[] tmp = _sample.job_number.Split('-');



                lbRefNo.Text = String.Format("{4}ATT/{0}/{1}/{2}-{3}", tmp[0], _job.date_of_receive.Value.ToString("yy"), tmp[1], tmp[2], (_sample.amend_count > 0 ? (_sample.amend_count == 1 ? "AM/" : "AM/" + _sample.amend_count) : ""));

                ////String.Format("AM/ATT/ELP/17/XXXX-XX")
                //lbDownloadName.Text = _sample.job_number.ToString();
                lbDateTestCompleted.Text = Convert.ToDateTime(_sample.due_date).ToString("MM/dd/yyyy");

                lbSampleDescription.Text = String.Format("Description:{0}<br />Model:{1}<br />Surface Area:{2}<br />Remark:{3}<br />", _sample.description, _sample.model, _sample.surface_area, _sample.remarks);

                m_specification mSpec = new m_specification();
                mSpec = mSpec.SelectByID(_sample.specification_id);
                m_type_of_test typeOfTest = new m_type_of_test();
                typeOfTest = typeOfTest.SelectByID(_sample.type_of_test_id);
                txtSpec.Text = String.Format("Specification [{0}]-->Type Of Test [{1}]", (mSpec==null) ? String.Empty : mSpec.name, typeOfTest.name);
            }
        }
    }
}