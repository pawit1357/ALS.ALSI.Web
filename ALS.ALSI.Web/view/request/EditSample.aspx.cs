using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace ALS.ALSI.Web.view.request
{
    public partial class EditSample : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(JobReTest));

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
            get { return (int)ViewState[GetType().Name + "JobID"]; }
            set { ViewState[GetType().Name + "JobID"] = value; }
        }
        public int SampleID
        {
            get { return (int)ViewState[GetType().Name + "SampleID"]; }
            set { ViewState[GetType().Name + "SampleID"] = value; }
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
                tmp.ID = JobID;
                return tmp;
            }
        }


        private void initialPage()
        {
            IEnumerable<m_status> listOfStatus = new m_status().SelectByMainStatus();
            List<String> status = new List<String>();
            status.Add("ADMIN_CONVERT_WORD");
            status.Add("LOGIN_SELECT_SPEC");
            status.Add("CHEMIST_TESTING");
            


            ddlStatus.Items.Clear();
            ddlStatus.DataSource = listOfStatus.Where(x=> status.Contains(x.name));
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            fillinScreen();
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            bindingData();
        }

        private void fillinScreen()
        {

            jobSample = new job_sample().SelectByID(this.SampleID);
            if (jobSample != null)
            {

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

            lbTitleName.Text = String.Format("{0} Report", (this.CommandName == CommandNameEnum.Amend) ? CommandNameEnum.Amend.ToString() : CommandNameEnum.Retest.ToString());

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //job_sample oldSample = new job_sample().SelectByID(this.SampleID);
            //List<job_sample> jobSamples = new job_sample().findByIdAndStatus(oldSample.job_number, (this.CommandName == CommandNameEnum.Retest) ? StatusEnum.JOB_RETEST : StatusEnum.JOB_AMEND);

            //job_sample newSample = new job_sample();
            //newSample.template_id = oldSample.template_id;
            //newSample.job_id = oldSample.job_id;
            //newSample.type_of_test_id = oldSample.type_of_test_id;
            //newSample.specification_id = oldSample.specification_id;
            //newSample.job_number = oldSample.job_number;
            //newSample.description = oldSample.description;
            //newSample.model = oldSample.model;
            //newSample.surface_area = oldSample.surface_area;
            //newSample.remarks = oldSample.remarks;
            //newSample.no_of_report = oldSample.no_of_report;
            //newSample.uncertainty = oldSample.uncertainty;
            //newSample.job_role = userLogin.role_id;
            //newSample.status_completion_scheduled = oldSample.status_completion_scheduled;
            //newSample.due_date = oldSample.due_date;
            //newSample.due_date_customer = oldSample.due_date_customer;
            //newSample.due_date_lab = oldSample.due_date_lab;
            //newSample.path_word = oldSample.path_word;
            //newSample.path_pdf = oldSample.path_pdf;
            //newSample.step1owner = oldSample.step1owner;
            //newSample.step2owner = oldSample.step2owner;
            //newSample.step3owner = oldSample.step3owner;
            //newSample.step4owner = oldSample.step4owner;
            //newSample.step5owner = oldSample.step5owner;
            //newSample.step6owner = oldSample.step6owner;
            //newSample.step7owner = oldSample.step7owner;
            //newSample.due_date = oldSample.due_date;
            //newSample.is_no_spec = oldSample.is_no_spec;
            //newSample.is_hold = oldSample.is_hold;
            //newSample.update_date = DateTime.Now;
            //newSample.amend_count = 0;
            //newSample.retest_count = 0;
            //newSample.amend_or_retest = (this.CommandName == CommandNameEnum.Amend) ? "A" : "R";
            //newSample.sample_prefix = oldSample.sample_prefix;
            //switch (this.CommandName)
            //{
            //    case CommandNameEnum.Amend:
            //        newSample.amend_count = oldSample.amend_count + 1;
            //        break;
            //    case CommandNameEnum.Retest:
            //        newSample.retest_count = oldSample.retest_count + 1;
            //        break;
            //}

            //newSample.job_status = Convert.ToInt16(ddlStatus.SelectedValue);

            //newSample.path_pdf = String.Empty;
            //newSample.path_word = String.Empty;
            //newSample.Insert();
            //oldSample.Update();


            //Commit
            //GeneralManager.Commit();
            //m_template template = new m_template().SelectByID(oldSample.template_id);
            //if (template != null)
            //{
            //    switch (Path.GetFileNameWithoutExtension(template.path_url))
            //    {
            //        case "Seagate_Copperwire":
            //            template_seagate_copperwire_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            template_seagate_copperwire_img.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "Seagate_Corrosion":
            //            template_seagate_corrosion_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            template_seagate_corrosion_img.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "Seagate_DHS":
            //        case "Seagate_DHS_V2":
            //            template_seagate_dhs_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            tb_m_dhs_cas.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "Seagate_FTIR":
            //        case "Seagate_FTIR_Adhesive":
            //        case "Seagate_FTIR_Damper":
            //        case "Seagate_FTIR_Packing":
            //            template_seagate_ftir_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "Seagate_GCMS":
            //        case "Seagate_GCMS_2":
            //        case "Seagate_GCMS_3":
            //            template_seagate_gcms_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            template_seagate_gcms_coverpage_img.CloneData(oldSample.ID, newSample.ID);
            //            tb_m_gcms_cas.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "Seagate_HPA":
            //        case "Seagate_HPA_1":
            //        case "Seagate_HPA_Boyd":
            //        case "Seagate_HPA_Siam":
            //            template_seagate_hpa_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "Seagate_IC":
            //            template_seagate_ic_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "Seagate_LPC":
            //            template_seagate_lpc_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "WD_Corrosion":
            //            template_wd_corrosion_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            template_wd_corrosion_img.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "WD_DHS":
            //            template_wd_dhs_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            tb_m_dhs_cas.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "WD_FTIR":
            //        case "WD_FTIR_IDM":
            //            template_wd_ftir_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "WD_GCMS":
            //        case "WD_GCMS_CVR":
            //            template_wd_gcms_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            tb_m_gcms_cas.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "WD_HPA_FOR_1":
            //        case "WD_HPA_FOR_1_V2":
            //            template_wd_hpa_for1_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "WD_HPA_FOR_3":
            //            template_wd_hpa_for3_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "WD_IC":
            //            template_wd_ic_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            //template_wd_ir_coverpage template_wd_ir_coverpage = new template_wd_ir_coverpage();
            //            break;
            //        case "WD_LPC":
            //            template_wd_lpc_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //        case "WD_MESA":
            //        case "WD_MESA_IDM":
            //        case "WD_MESA_InkRibon":
            //            template_wd_mesa_coverpage.CloneData(oldSample.ID, newSample.ID);
            //            template_wd_mesa_img.CloneData(oldSample.ID, newSample.ID);
            //            break;
            //    }
            //}
            //Commit
            //GeneralManager.Commit();
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