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
    public partial class ChangeJobDueDate : System.Web.UI.Page
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
                if (this.jobSample.due_date_lab != null)
                {
                    if (this.jobSample.due_date_lab.Value.Year == 1 && this.jobSample.due_date_lab.Value.Month == 1 && this.jobSample.due_date_lab.Value.Day == 1)
                    {
                        txtDuedate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        cbIsTba.Checked = true;
                    }
                    else
                    {
                        txtDuedate.Text = Convert.ToDateTime(this.jobSample.due_date_lab).ToString("dd/MM/yyyy");
                        cbIsTba.Checked = false;
                    }
                }
                txtRemark.Text = this.jobSample.remarks;
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
            holiday_calendar hc = new holiday_calendar();
            this.jobSample.due_date = hc.GetWorkingDay(CustomUtils.converFromDDMMYYYY(txtDuedate.Text),0);
            if (cbIsTba.Checked)
            {
                this.jobSample.due_date_lab = new DateTime(1, 1, 1);
                this.jobSample.due_date_customer = new DateTime(1, 1, 1);
            }
            else
            {

                //1|Normal
                //2|Urgent
                //3|Express
                //4|Extend 1
                //5|Extend 2

                switch (this.jobSample.status_completion_scheduled.Value)
                {
                    case 1:
                    case 2:
                    case 4:
                    case 5:
                        this.jobSample.due_date_lab = hc.GetWorkingDay(CustomUtils.converFromDDMMYYYY(txtDuedate.Text), 1);
                        this.jobSample.due_date_customer = hc.GetWorkingDay(CustomUtils.converFromDDMMYYYY(txtDuedate.Text), 2);
                        break;
                    case 3:
                        this.jobSample.due_date_lab = hc.GetWorkingDay(CustomUtils.converFromDDMMYYYY(txtDuedate.Text), 1);
                        this.jobSample.due_date_customer = hc.GetWorkingDay(CustomUtils.converFromDDMMYYYY(txtDuedate.Text), 1);
                        break;
                }

            }

            this.jobSample.Update();
            job_sample_logs tmp = new job_sample_logs
            {
                ID = 0,
                job_sample_id = this.jobSample.ID,
                log_title = String.Format("Change Due Date"),
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