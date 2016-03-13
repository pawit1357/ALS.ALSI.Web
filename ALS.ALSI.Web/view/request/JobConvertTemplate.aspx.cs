using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.request
{
    public partial class JobConvertTemplate : System.Web.UI.Page
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(JobConvertTemplate));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "JobConvertTemplate"]; }
            set { Session[GetType().Name + "JobConvertTemplate"] = value; }
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
            DataTable dt = Extenders.ObjectToDataTable( objInfo.SearchData());
            //searchResult = objInfo.SearchData();
            gvJob.DataSource = dt;
            gvJob.DataBind();

            m_template template = new m_template();

            ddlTemplate.Items.Clear();
            ddlTemplate.DataSource = template.SelectAllByActiveForConvertPage(Convert.ToInt16(dt.Rows[0]["spec_id"].ToString()));
            ddlTemplate.DataBind();
            ddlTemplate.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            switch (CommandName)
            {
                case CommandNameEnum.ConvertTemplate:
                    fillinScreen();
                    ddlTemplate.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    break;
            }

        }

        private void fillinScreen()
        {
            this.jobSample = new job_sample().SelectByID(this.SampleID);
            if (this.jobSample != null)
            {
                ddlTemplate.SelectedValue = this.jobSample.template_id.ToString();
            }
            else
            {
                btnCancel_Click(null, null);
            }

        }

        //private void bindingData()
        //{
        //    searchResult = objInfo.SearchData();
        //    gvJob.DataSource = searchResult;
        //    gvJob.DataBind();
        //}

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
            switch (CommandName)
            {
                case CommandNameEnum.ConvertTemplate:
                    this.jobSample.template_id = String.IsNullOrEmpty(ddlTemplate.SelectedValue) ? 0 : int.Parse(ddlTemplate.SelectedValue);
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC);
                    this.jobSample.Update();
                    break;
            }
            //Commit
            GeneralManager.Commit();
            removeSession();
            MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlTemplate.SelectedIndex = 0;
            removeSession();
            Response.Redirect(PreviousPath);
        }
    }

}