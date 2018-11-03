using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Web;
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
            DataTable dt = Extenders.ObtainDataTableFromIEnumerable(objInfo.SearchData());
            //searchResult = objInfo.SearchData();
            gvJob.DataSource = dt;
            gvJob.DataBind();

            m_template template = new m_template();


            var data = template.SelectAllByActive();//.SelectAllByActiveForConvertPage(Convert.ToInt16(dt.Rows[0]["spec_id"].ToString()));
            ddlTemplate.Items.Clear();
            ddlTemplate.DataSource = data;
            ddlTemplate.DataBind();
            ddlTemplate.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));
            ddlTemplate.SelectedValue = "";


            switch (CommandName)
            {
                case CommandNameEnum.ConvertTemplate:
                    fillinScreen();
                    ddlTemplate.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    pUploadfile.Visible = false;
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
                    if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).Equals(".xls") || Path.GetExtension(FileUpload1.FileName).Equals(".xlt") || Path.GetExtension(FileUpload1.FileName).Equals(".doc") || Path.GetExtension(FileUpload1.FileName).Equals(".docx")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        FileUpload1.SaveAs(source_file);
                        this.jobSample.ad_hoc_tempalte_path = source_file_url;
                    }


                    this.jobSample.template_id = String.IsNullOrEmpty(ddlTemplate.SelectedValue) ? 0 : int.Parse(ddlTemplate.SelectedValue);
                    switch (int.Parse(ddlTemplate.SelectedValue))
                    {
                        case 901://PA TAMPLATE(BLANK)
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                            break;
                        default:
                            if (pUploadfile.Visible)
                            {
                                this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                            }
                            else
                            {
                                this.jobSample.job_status = Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC);
                            }
                            break;
                    }


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
            //ddlTemplate.SelectedIndex = 0;
            removeSession();
            Response.Redirect(PreviousPath);
        }
        protected void gvJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }

        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FixCode 407,408 Blank template
            if (ddlTemplate.SelectedValue.Equals("407") || ddlTemplate.SelectedValue.Equals("408"))
            {
                pUploadfile.Visible = true;
            }
            else
            {
                pUploadfile.Visible = false;
            }
        }
    }

}