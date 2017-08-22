using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.IO;
using System.Web.UI;

namespace ALS.ALSI.Web.view.request
{
    public partial class JobWorkFlow : System.Web.UI.Page
    {
        #region "Property"
        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }
        protected String Step1Status
        {
            get { return (String)Session[GetType().Name + "Step1Status"]; }
            set { Session[GetType().Name + "Step1Status"] = value; }
        }

        protected String Step2Status
        {
            get { return (String)Session[GetType().Name + "Step2Status"]; }
            set { Session[GetType().Name + "Step2Status"] = value; }
        }
        protected String Step3Status
        {
            get { return (String)Session[GetType().Name + "Step3Status"]; }
            set { Session[GetType().Name + "Step3Status"] = value; }
        }
        protected String Step4Status
        {
            get { return (String)Session[GetType().Name + "Step4Status"]; }
            set { Session[GetType().Name + "Step4Status"] = value; }
        }
        protected String Step5Status
        {
            get { return (String)Session[GetType().Name + "Step5Status"]; }
            set { Session[GetType().Name + "Step5Status"] = value; }
        }
        protected String Step6Status
        {
            get { return (String)Session[GetType().Name + "Step6Status"]; }
            set { Session[GetType().Name + "Step6Status"] = value; }
        }

        public job_sample jobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
        }

        public int SampleID
        {
            get { return (int)Session[GetType().Name + "SampleID"]; }
            set { Session[GetType().Name + "SampleID"] = value; }
        }

        private string LastLoadedTemplate
        {
            get
            {
                return ViewState["Template"] as string;
            }
            set
            {
                ViewState["Template"] = value;
            }
        }

        private void LoadTemplate()
        {
            string controlPath = LastLoadedTemplate;

            if (!string.IsNullOrEmpty(controlPath))
            {
                plhCoverPage.Controls.Clear();
                UserControl uc = (UserControl)LoadControl(controlPath);
                plhCoverPage.Controls.Add(uc);
                //Job Sample Info
                job_info job = new job_info().SelectByID(this.jobSample.job_id);
                plhSampleInfo.Controls.Clear();
                UserControl uc1 = (UserControl)LoadControl(Constants.LINK_SAMPLE_INFO);
                plhSampleInfo.Controls.Add(uc1);
                ((ALS.ALSI.Web.UserControls.SampleInfo)(uc1)).initialInfo(job, jobSample);
            }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name + "Step1Status");
            Session.Remove(GetType().Name + "Step2Status");
            Session.Remove(GetType().Name + "Step3Status");
            Session.Remove(GetType().Name + "Step4Status");
            Session.Remove(GetType().Name + "Step5Status");
            Session.Remove(GetType().Name + "Step6Status");
        }

        private void initialPage()
        {

            this.jobSample = new job_sample().SelectByID(this.SampleID);
            if (jobSample != null)
            {

                StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), jobSample.job_status.ToString(), true);

                switch (status)
                {
                    case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    case StatusEnum.LOGIN_SELECT_SPEC:
                        Step1Status = " active";
                        Step2Status = "";
                        Step3Status = "";
                        Step4Status = "";
                        Step5Status = "";
                        Step6Status = "";
                        lbWorkFlowStep.Text = "1";
                        break;
                    case StatusEnum.CHEMIST_TESTING:
                        Step1Status = " done";
                        Step2Status = " active";
                        Step3Status = ""; ;
                        Step4Status = "";
                        Step5Status = "";
                        Step6Status = "";
                        lbWorkFlowStep.Text = "2";
                        break;
                    case StatusEnum.SR_CHEMIST_CHECKING:
                    case StatusEnum.SR_CHEMIST_APPROVE:
                    case StatusEnum.SR_CHEMIST_DISAPPROVE:
                        Step1Status = " done";
                        Step2Status = " done";
                        Step3Status = " active";
                        Step4Status = "";
                        Step5Status = "";
                        Step6Status = "";
                        lbWorkFlowStep.Text = "3";
                        break;
                    case StatusEnum.ADMIN_CONVERT_WORD:
                        Step1Status = " done";
                        Step2Status = " done";
                        Step3Status = " done";
                        Step4Status = " active";
                        Step5Status = "";
                        Step6Status = "";
                        lbWorkFlowStep.Text = "4";
                        break;
                    case StatusEnum.LABMANAGER_CHECKING:
                    case StatusEnum.LABMANAGER_APPROVE:
                    case StatusEnum.LABMANAGER_DISAPPROVE:
                        Step1Status = " done";
                        Step2Status = " done";
                        Step3Status = " done";
                        Step4Status = " done";
                        Step5Status = " active";
                        Step6Status = "";
                        lbWorkFlowStep.Text = "5";
                        break;
                    case StatusEnum.ADMIN_CONVERT_PDF:
                        Step1Status = " done";
                        Step2Status = " done";
                        Step3Status = " done";
                        Step4Status = " done";
                        Step5Status = " done";
                        Step6Status = " active";
                        lbWorkFlowStep.Text = "6";
                        break;
                }
                job_sample_logs logs = new job_sample_logs();

                //job_sample_logs sampleLog = logs.SelectDate(this.jobSample.ID, Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC));
                //lbStep1UseDate.Text = (sampleLog != null) ? String.Format("({0})", DateTime.Now.Subtract(Convert.ToDateTime(sampleLog.date)).Days.ToString()) : String.Empty;
                //sampleLog = logs.SelectDate(this.jobSample.ID, Convert.ToInt16(StatusEnum.CHEMIST_TESTING));
                //lbStep2UseDate.Text = (sampleLog != null) ? String.Format("({0})", DateTime.Now.Subtract(Convert.ToDateTime(sampleLog.date)).Days.ToString()) : String.Empty;
                //sampleLog = logs.SelectDate(this.jobSample.ID, Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING));
                //lbStep3UseDate.Text = (sampleLog != null) ? String.Format("({0})", DateTime.Now.Subtract(Convert.ToDateTime(sampleLog.date)).Days.ToString()) : String.Empty;
                //sampleLog = logs.SelectDate(this.jobSample.ID, Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD));
                //lbStep4UseDate.Text = (sampleLog != null) ? String.Format("({0})", DateTime.Now.Subtract(Convert.ToDateTime(sampleLog.date)).Days.ToString()) : String.Empty;
                //sampleLog = logs.SelectDate(this.jobSample.ID, Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING));
                //lbStep5UseDate.Text = (sampleLog != null) ? String.Format("({0})", DateTime.Now.Subtract(Convert.ToDateTime(sampleLog.date)).Days.ToString()) : String.Empty;
                //sampleLog = logs.SelectDate(this.jobSample.ID, Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF));
                //lbStep6UseDate.Text = (sampleLog != null) ? String.Format("({0})", DateTime.Now.Subtract(Convert.ToDateTime(sampleLog.date)).Days.ToString()) : String.Empty;

                lbStep1UseDate.Text = (lbStep1UseDate.Text.Equals("(0)")) ? String.Empty : lbStep1UseDate.Text;
                lbStep2UseDate.Text = (lbStep2UseDate.Text.Equals("(0)")) ? String.Empty : lbStep2UseDate.Text;
                lbStep3UseDate.Text = (lbStep3UseDate.Text.Equals("(0)")) ? String.Empty : lbStep3UseDate.Text;
                lbStep4UseDate.Text = (lbStep4UseDate.Text.Equals("(0)")) ? String.Empty : lbStep4UseDate.Text;
                lbStep5UseDate.Text = (lbStep5UseDate.Text.Equals("(0)")) ? String.Empty : lbStep5UseDate.Text;
                lbStep6UseDate.Text = (lbStep6UseDate.Text.Equals("(0)")) ? String.Empty : lbStep6UseDate.Text;

            }

        }

        private void reloadTemplate()
        {
            m_template template = new m_template();
            template = template.SelectByID(jobSample.template_id);

            if (template != null)
            {
                LastLoadedTemplate = String.Format("{0}{1}.ascx", Constants.BASE_TEMPLATE_PATH, Path.GetFileNameWithoutExtension(template.path_url));
                lbDebugInfo.Text = String.Format("debug:{0},TemplateID:{1},SampleID:{2},User:{3})", LastLoadedTemplate,template.ID,this.jobSample.ID, userLogin.username);
            }

            LoadTemplate();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;
            if (!Page.IsPostBack)
            {
                initialPage();
            }
            reloadTemplate();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            // Ensure that the control is nested in a server form. 
            if (Page != null)
            {
                Page.VerifyRenderingInServerForm(this);
            }
            base.Render(writer);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

    }
}