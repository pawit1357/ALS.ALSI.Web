using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace ALS.ALSI.Web.view.request
{
    public partial class AdminPrint : System.Web.UI.Page
    {
                                //Console.WriteLine("หน้าจอพิมพ์ word,pdf แบบเลือก ไม่เอา footer,header ได้");
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AdminPrint));

        #region "Property"
        protected String type_of_test
        {
            get { return (String)Session[GetType().Name + "type_of_test"]; }
            set { Session[GetType().Name + "type_of_test"] = value; }
        }

        protected String sample_diposition
        {
            get { return (String)Session[GetType().Name + "sample_diposition"]; }
            set { Session[GetType().Name + "sample_diposition"] = value; }
        }
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "PrintSticker"]; }
            set { Session[GetType().Name + "PrintSticker"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        public int JobID
        {
            get { return (int)Session[GetType().Name + "JobID"]; }
            set { Session[GetType().Name + "JobID"] = value; }
        }

        public job_info objInfo
        {
            get
            {
                job_info tmp = new job_info();
                tmp.ID = JobID;
                return tmp;
            }
        }


        private void initialPage()
        {
            fillinScreen();
            btnCancel.Enabled = true;
            bindingData();
        }

        private void fillinScreen()
        {
            job_info jobInfo = new job_info().SelectByID(this.JobID);
            if (jobInfo != null)
            {



                lbJobNo.Text = String.Format("{0}{1}", jobInfo.job_running.prefix, Convert.ToInt32(jobInfo.job_number).ToString("00000"));
                lbClient.Text = jobInfo.m_customer.company_name;
                lbContract.Text = jobInfo.m_customer_contract_person.name;


            //    List<job_sample> samples = job_sample.FindAllByJobID(jobInfo.ID);
            //    if (samples != null && samples.Count > 0)
            //    {
            //        int spec_id = 0;
            //        foreach (job_sample s in samples)
            //        {
            //            lbSample.Text += s.description + "<br/>";


            //            lbSpec.Text = s.m_specification.name;
            //            if (spec_id != s.specification_id)
            //            {
            //                spec_id = s.specification_id;
            //            }
            //            type_of_test += String.Format("<i class=\"icon-check\"></i> {0} ", s.m_type_of_test.name);
            //        }

            //        if (jobInfo.sample_diposition != null)
            //        {
            //            if (jobInfo.sample_diposition.Equals("Y"))
            //            {
            //                sample_diposition = "<i class=\"icon-check\"></i>Discard<i class=\"icon-check-empty\"></i>Return";
            //            }
            //            else
            //            {
            //                sample_diposition = "<i class=\"icon-check-empty\"></i>Discard <i class=\"icon-check\"></i>Return";
            //            }
            //        }
            //    }

            }
            else
            {
                btnCancel_Click(null, null);
            }
        }

        private void bindingData()
        {
            searchResult = objInfo.SearchData();
            gvJob.DataSource = searchResult;
            gvJob.DataBind();
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);

            Session.Remove(GetType().Name + "PrintSticker");
            Session.Remove(GetType().Name + "JobID");
            Session.Remove(GetType().Name + "type_of_test");
            Session.Remove(GetType().Name + "sample_diposition");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.JobID = (prvPage == null) ? this.JobID : prvPage.JobID;
            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }



        protected void gvJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            int key = Convert.ToInt32(gvJob.SelectedDataKey.Value);

            job_info jobInfo = new job_info().SelectByID(this.JobID);
            if (jobInfo != null)
            {



                lbJobNo.Text = String.Format("{0}{1}", jobInfo.job_running.prefix, Convert.ToInt32(jobInfo.job_number).ToString("00000"));
                lbClient.Text = jobInfo.m_customer.company_name;
                lbContract.Text = jobInfo.m_customer_contract_person.name;


                List<job_sample> samples = job_sample.FindAllByJobID(jobInfo.ID);
                if (samples != null && samples.Count > 0)
                {
                    int spec_id = 0;
                    foreach (job_sample s in samples)
                    {
                        //if (s.ID == 18)
                        //{
                        //    lbSample.Text += s.description + "<br/>";


                        //    lbSpec.Text = s.m_specification.name;
                        //    if (spec_id != s.specification_id)
                        //    {
                        //        spec_id = s.specification_id;
                        //    }
                        //    type_of_test += String.Format("<i class=\"icon-check\"></i> {0} ", s.m_type_of_test.name);
                        //}
                    }

                    if (jobInfo.sample_diposition != null)
                    {
                        if (jobInfo.sample_diposition.Equals("Y"))
                        {
                            sample_diposition = "<i class=\"icon-check\"></i>Discard<i class=\"icon-check-empty\"></i>Return";
                        }
                        else
                        {
                            sample_diposition = "<i class=\"icon-check-empty\"></i>Discard <i class=\"icon-check\"></i>Return";
                        }
                    }
                }

            }
            else
            {
                btnCancel_Click(null, null);
            }

            Console.WriteLine();
        }
    }
}