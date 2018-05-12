using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;

namespace ALS.ALSI.Web.UserControls
{
    public partial class SampleInfo : System.Web.UI.UserControl
    {
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

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
                //String[] tmp = _sample.job_number.Split('-');

                int phisicalYear = Convert.ToInt16(DateTime.Now.Year.ToString().Substring(2));
                if (DateTime.Now.Month < 4)
                {
                    phisicalYear = Convert.ToInt16(DateTime.Now.Year.ToString().Substring(2)) - 1;
                }

                String AmRetest = String.Empty;
                switch (_sample.amend_or_retest)
                {
                    case "AM":
                        AmRetest = (_sample.amend_count > 0) ? "AM" + ((_sample.amend_count == 1) ? "" : _sample.amend_count + "") + "/" : String.Empty;
                        break;
                    case "R":
                        AmRetest = (_sample.retest_count > 0) ? "R" + ((_sample.retest_count == 1) ? "" : _sample.retest_count + "") + "/" : String.Empty;
                        break;
                }

                String[] tmp = _sample.job_number.Split('-');
                lbRefNo.Text = String.Format("{0}ATT/{1}/{2}/{3}-{4}", AmRetest, tmp[0], phisicalYear, tmp[1], tmp[2]);// _sample.job_number.ToString();


                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);

                lbDateTestCompleted.Text = (userRole == RoleEnum.CHEMIST)? "-": Convert.ToDateTime(_sample.date_chemist_complete).ToString("MM/dd/yyyy");

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