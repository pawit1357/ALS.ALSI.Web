using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.request
{
    public partial class PrintSticker : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PrintSticker));
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

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
        public int SampleID
        {
            get { return (int)Session[GetType().Name + "SampleID"]; }
            set { Session[GetType().Name + "SampleID"] = value; }
        }

        //public job_info objInfo
        //{
        //    get { return (job_info)Session[GetType().Name + "objInfo"]; }
        //    set { Session[GetType().Name + "objInfo"] = value; }
        //}


        //public int PhysicalYear
        //{
        //    get { return (int)Session[GetType().Name + "PhysicalYear"]; }
        //    set { Session[GetType().Name + "PhysicalYear"] = value; }
        //}

        public job_info objInfo
        {
            get
            {
                job_info tmp = new job_info();
                tmp.ID = JobID;
                tmp.bNotShowDelete = true;
                return tmp;
            }
        }


        private void initialPage()
        {
            fillinScreen();
            //btnCancel.Enabled = true;
            bindingData();
        }

        private void fillinScreen()
        {
            type_of_test = "";
            job_sample jobSample = new job_sample().SelectByID(this.SampleID);
            job_info jobInfo = new job_info().SelectByID(this.JobID);
            if (jobInfo != null)
            {

                m_customer cus = new m_customer().SelectByID(jobInfo.customer_id);
                m_customer_contract_person cus_con_per = new m_customer_contract_person().SelectByID(jobInfo.contract_person_id);
                lbClient.Text = cus.company_name;// jobInfo.m_customer.company_name;
                lbContract.Text = cus_con_per.name;// jobInfo.m_customer_contract_person.name;


                List<job_sample> samples = job_sample.FindAllByJobID(jobInfo.ID);
                if (samples != null && samples.Count > 0)
                {
                    lbSample.Text = samples[0].description.Length > 30 ? samples[0].description.Substring(0, 30) + "  ..." : samples[0].description;
                    int spec_id = 0;
                    int index = 0;
                    foreach (job_sample s in samples)
                    {
                        if (index == 0)
                        {
                            lbJobNo.Text = s.job_number.Split('-')[0] +"-"+s.job_number.Split('-')[1] + "-"+s.job_number.Split('-')[2]+ ",";// String.Format("{0}{1}", jobSample.job_running.prefix, Convert.ToInt32(jobInfo.job_number).ToString("00000"));
                        }
                        else
                        {
                            lbJobNo.Text += s.job_number.Split('-')[2] + ",";
                        }
                        m_specification spec = new m_specification().SelectByID(s.specification_id);

                        lbSpec.Text = spec.name.Length > 15 ? spec.name.Substring(0, 15) + "..." : spec.name;
                        if (spec_id != s.specification_id)
                        {
                            spec_id = s.specification_id;
                        }
                        m_type_of_test tot = new m_type_of_test().SelectByID(s.type_of_test_id);
                        type_of_test += tot.name + ","; //String.Format("<i class=\"icon-check\"></i> {0} ", tot.name);
                        index++;
                    }

                    lbTot.Text = type_of_test.Substring(0,type_of_test.Length-1);
                    if (jobInfo.sample_diposition != null)
                    {
                        if (jobInfo.sample_diposition.Equals("Y"))
                        {
                            sample_diposition = "Discard";// "<i class=\"icon-check\"></i>Discard<i class=\"icon-check-empty\"></i>Return";
                        }
                        else
                        {
                            sample_diposition = "Return";// "<i class=\"icon-check-empty\"></i>Discard <i class=\"icon-check\"></i>Return";
                        }
                        lbSd.Text = sample_diposition;
                    }
                    lbSa.Text = samples[0].surface_area;

                }

                lbJobNo.Text = lbJobNo.Text.Substring(0, lbJobNo.Text.Length - 1);
                ///
                ReportParameterCollection reportParameters = new ReportParameterCollection();

                reportParameters.Add(new ReportParameter("pJobNo", lbJobNo.Text));
                reportParameters.Add(new ReportParameter("pClient", lbClient.Text));
                reportParameters.Add(new ReportParameter("pContract", lbContract.Text));
                reportParameters.Add(new ReportParameter("pSample", lbSample.Text));
                reportParameters.Add(new ReportParameter("pSpec", lbSpec.Text));
                reportParameters.Add(new ReportParameter("pTest", lbTot.Text));
                reportParameters.Add(new ReportParameter("pSampleD", lbSd.Text));




                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/rptStricker.rdlc");
                viewer.LocalReport.SetParameters(reportParameters);
                
            }
            else
            {
                //btnCancel_Click(null, null);
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
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;
            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;
            //if (this.objInfo == null)
            //{
            //    this.objInfo = prvPage.obj;
            //}

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        //void btnCancel_Click(object sender, EventArgs e)
        //{
        //    removeSession();
        //    Response.Redirect(PreviousPath);
        //}

        protected void btnPrint_Click(object sender, EventArgs e)
        {


        }



        protected void gvJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            int key = Convert.ToInt32(gvJob.SelectedDataKey.Value);

            job_sample jobSample = new job_sample().SelectByID(key);
            job_info jobInfo = new job_info().SelectByID(this.JobID);
            if (jobInfo != null)
            {

                m_customer cus = new m_customer().SelectByID(jobInfo.customer_id);
                m_customer_contract_person cus_con_per = new m_customer_contract_person().SelectByID(jobInfo.contract_person_id);
                lbJobNo.Text = jobSample.job_number;// String.Format("{0}{1}", jobSample.job_running.prefix, Convert.ToInt32(jobInfo.job_number).ToString("00000"));
                lbClient.Text = cus.company_name;// jobInfo.m_customer.company_name;
                lbContract.Text = cus_con_per.name;// jobInfo.m_customer_contract_person.name;


                List<job_sample> samples = job_sample.FindAllBySampleID(key);//.FindAllByJobID(jobInfo.ID);
                if (samples != null && samples.Count > 0)
                {
                    lbSample.Text = samples[0].description.Length > 30 ? samples[0].description.Substring(0, 30) + "  ..." : samples[0].description;
                    int spec_id = 0;
                    foreach (job_sample s in samples)
                    {


                        m_specification spec = new m_specification().SelectByID(s.specification_id);

                        lbSpec.Text = spec.name.Length >15 ? spec.name.Substring(0,15)+"...":spec.name;

                        if (spec_id != s.specification_id)
                        {
                            spec_id = s.specification_id;
                        }
                        m_type_of_test tot = new m_type_of_test().SelectByID(s.type_of_test_id);
                        type_of_test = tot.name + ","; //String.Format("<i class=\"icon-check\"></i> {0} ", tot.name);
                    }
                    lbTot.Text = type_of_test.Substring(0, type_of_test.Length - 1);
                    if (jobInfo.sample_diposition != null)
                    {
                        if (jobInfo.sample_diposition.Equals("Y"))
                        {
                            sample_diposition = "Discard";// "<i class=\"icon-check\"></i>Discard<i class=\"icon-check-empty\"></i>Return";
                        }
                        else
                        {
                            sample_diposition = "Return";// "<i class=\"icon-check-empty\"></i>Discard <i class=\"icon-check\"></i>Return";
                        }
                        lbSd.Text = sample_diposition;
                    }
                    lbSa.Text = samples[0].surface_area;
                }

            }
            else
            {
                //btnCancel_Click(null, null);
            }

            Console.WriteLine();
        }


        protected void gvJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }


        protected void lbPrint_Click(object sender, EventArgs e)
        {
            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("pJobNo", lbJobNo.Text));
            reportParameters.Add(new ReportParameter("pClient", lbClient.Text));
            reportParameters.Add(new ReportParameter("pContract", lbContract.Text));
            reportParameters.Add(new ReportParameter("pSample", lbSample.Text));
            reportParameters.Add(new ReportParameter("pSpec", lbSpec.Text));
            reportParameters.Add(new ReportParameter("pTest", lbTot.Text));
            reportParameters.Add(new ReportParameter("pSampleD", lbSd.Text));




            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/Report1.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);



            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + lbJobNo.Text + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

    }
}