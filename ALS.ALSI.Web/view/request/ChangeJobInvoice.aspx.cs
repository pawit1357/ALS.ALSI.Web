using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.request
{
    public partial class ChangeJobInvoice : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ChangeJobInvoice));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "ChangeJobInvoice"]; }
            set { Session[GetType().Name + "ChangeJobInvoice"] = value; }
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
        public job_sample jobSample
        {
            get { return (job_sample)Session[GetType().Name + "job_sample"]; }
            set { Session[GetType().Name + "job_sample"] = value; }
        }


        private void initialPage()
        {
            fillinScreen();
            txtInvoice.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            bindingData();
        }

        private void fillinScreen()
        {
            jobSample = new job_sample().SelectByID(this.SampleID);
            if (jobSample != null)
            {
                //txtPo.Text = jobSample.sample_po;
                txtInvoice.Text = jobSample.sample_invoice;
                txtInvoiceAmt.Text = jobSample.sample_invoice_amount.ToString();

                if (jobSample.sample_invoice_complete_date != null)
                {
                    txtPaymentDate.Text = jobSample.sample_invoice_complete_date.Value.ToString("dd/MM/yyyy");

                }

                if (jobSample.sample_invoice_date != null)
                {
                    txtInvoiceDate.Text = jobSample.sample_invoice_date.Value.ToString("dd/MM/yyyy");

                }
                else
                {
                    txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            else
            {
                btnCancel_Click(null, null);
            }

        }

        private void bindingData()
        {
            job_info jobInfo = new job_info();

            jobInfo.sample_id = this.SampleID;
            searchResult = jobInfo.SearchData();
            gvJob.DataSource = searchResult;
            gvJob.DataBind();
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);

            Session.Remove(GetType().Name + "ChangeJobInvoice");
            Session.Remove(GetType().Name + "JobID");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.JobID = (prvPage == null) ? this.JobID : prvPage.JobID;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;

            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }
        protected void gvJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            jobSample.sample_invoice = txtInvoice.Text;
            if (!String.IsNullOrEmpty(txtInvoiceDate.Text))
            {
                jobSample.sample_invoice_date = CustomUtils.converFromDDMMYYYY(txtInvoiceDate.Text);
            }
            jobSample.sample_invoice_amount = (CustomUtils.isNumber(txtInvoiceAmt.Text)) ? Convert.ToDouble(txtInvoiceAmt.Text) : 0;
            jobSample.sample_invoice_status = Convert.ToInt16(PaymentStatus.PAYMENT_INPROCESS);
            if (!String.IsNullOrEmpty(txtPaymentDate.Text))
            {
                jobSample.sample_invoice_status = Convert.ToInt16(PaymentStatus.PAYMENT_COMPLETE);
                jobSample.sample_invoice_complete_date = CustomUtils.converFromDDMMYYYY(txtInvoiceDate.Text);
            }
            jobSample.Update();
            //Commit
            GeneralManager.Commit();
            removeSession();
            MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }
    }
}