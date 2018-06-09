using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections;
using System.Web.UI;

namespace ALS.ALSI.Web.view.request
{
    public partial class ChangeSrChemistCompleteDate : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ChangeJobDueDate));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "ChangeJobDueDate"]; }
            set { Session[GetType().Name + "ChangeJobDueDate"] = value; }
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

        public job_info objInfo
        {
            get
            {
                job_info tmp = new job_info();
                tmp.sample_id = SampleID;
                return tmp;
            }
        }


        private void initialPage()
        {

            fillinScreen();
            txtDuedate.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            bindingData();
        }

        private void fillinScreen()
        {
            this.jobSample = new job_sample().SelectByID(this.SampleID);
            if (this.jobSample != null)
            {
                txtDuedate.Text = Convert.ToDateTime(this.jobSample.date_srchemist_complate).ToString("dd/MM/yyyy");
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
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;
            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            this.jobSample.date_srchemist_complate = CustomUtils.converFromDDMMYYYY(txtDuedate.Text);
            this.jobSample.Update();
            job_sample_logs tmp = new job_sample_logs
            {
                ID = 0,
                job_sample_id = this.jobSample.ID,
                log_title = String.Format("Change Sr Complete date by {0}",userLogin.username),
                job_remark = txtRemark.Text,
                is_active = "0",
                date = DateTime.Now
            };
            tmp.Insert();

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