using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Biz.ReportObjects;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using ALS.ALSI.Web.view.request;
using Microsoft.Reporting.WebForms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class Seagate_GCMS : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Seagate_GCMS));

        #region "Property"
        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }
        public List<tb_m_gcms_cas> tbCas
        {
            get { return (List<tb_m_gcms_cas>)Session[GetType().Name + "tbCas"]; }
            set { Session[GetType().Name + "tbCas"] = value; }
        }

        public List<template_seagate_gcms_coverpage> coverpages
        {
            get { return (List<template_seagate_gcms_coverpage>)Session[GetType().Name + "coverpages"]; }
            set { Session[GetType().Name + "coverpages"] = value; }
        }

        public job_sample jobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
        }
        //public List<template_seagate_gcms_coverpage_img> refImg
        //{
        //    get { return (List<template_seagate_gcms_coverpage_img>)Session[GetType().Name + "refImg"]; }
        //    set { Session[GetType().Name + "refImg"] = value; }
        //}
        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
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

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "tbCas");
            Session.Remove(GetType().Name + "coverpages");
            Session.Remove(GetType().Name + "libs");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
            Session.Remove(GetType().Name + "DirectInject");
            Session.Remove(GetType().Name + "SampleSize");
            Session.Remove(GetType().Name + "Unit");
            Session.Remove(GetType().Name + "PercentRecovery");
            Session.Remove(GetType().Name + "HumID");

        }

        private void initialPage()
        {
            this.CommandName = CommandNameEnum.Add;

            tb_m_component comp = new tb_m_component();
            comp.specification_id = this.jobSample.specification_id;
            comp.template_id = this.jobSample.template_id;

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));


            ddlComponent.Items.Clear();
            ddlComponent.DataSource = comp.SelectAll();
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            tb_unit unit = new tb_unit();

            ddlUnitCompound.Items.Clear();
            ddlUnitCompound.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitCompound.DataBind();
            ddlUnitCompound.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorBaseSub.Items.Clear();
            ddlUnitMotorBaseSub.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorBaseSub.DataBind();
            ddlUnitMotorBaseSub.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorBase.Items.Clear();
            ddlUnitMotorBase.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorBase.DataBind();
            ddlUnitMotorBase.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorHubSub.Items.Clear();
            ddlUnitMotorHubSub.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorHubSub.DataBind();
            ddlUnitMotorHubSub.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorHub.Items.Clear();
            ddlUnitMotorHub.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorHub.DataBind();
            ddlUnitMotorHub.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnitMotorOilContamination.Items.Clear();
            ddlUnitMotorOilContamination.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnitMotorOilContamination.DataBind();
            ddlUnitMotorOilContamination.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));










            /*INFO*/

            this.jobSample = new job_sample().SelectByID(this.SampleID);
            this.coverpages = template_seagate_gcms_coverpage.FindAllBySampleID(this.SampleID);

            #region "CAS"
            this.tbCas = tb_m_gcms_cas.FindAllBySampleID(this.SampleID);
            if (this.tbCas != null && this.tbCas.Count > 0 && this.coverpages != null && this.coverpages.Count > 0)
            {
                this.coverpages = this.coverpages.FindAll(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal));

                gvRHCBase.DataSource = this.tbCas.Where(x => x.cas_group == Convert.ToInt32(GcmsSeagateEnum.RHC_BASE)).ToList();
                gvRHCBase.DataBind();
                gvRHCHub.DataSource = this.tbCas.Where(x => x.cas_group == Convert.ToInt32(GcmsSeagateEnum.RHC_HUB)).ToList();
                gvRHCHub.DataBind();
            }

            #endregion

            #region "SAMPLE"
            if (this.jobSample != null)
            {
                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);
                StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
                lbJobStatus.Text = Constants.GetEnumDescription(status);
                ddlStatus.Items.Clear();

                pRemark.Visible = false;
                pDisapprove.Visible = false;
                pSpecification.Visible = false;
                pStatus.Visible = false;
                pUploadfile.Visible = false;
                pDownload.Visible = false;
                btnSubmit.Visible = false;
                gvMotorOil.Columns[3].Visible = false;

                switch (userRole)
                {
                    case RoleEnum.LOGIN:
                        if (status == StatusEnum.LOGIN_SELECT_SPEC)
                        {
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = true;
                            pStatus.Visible = false;
                            pUploadfile.Visible = false;
                            pDownload.Visible = false;
                            btnSubmit.Visible = true;
                        }
                        break;
                    case RoleEnum.CHEMIST:
                        if (status == StatusEnum.CHEMIST_TESTING)
                        {
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = false;
                            pUploadfile.Visible = false;
                            pDownload.Visible = false;
                            btnSubmit.Visible = true;
                        }
                        break;
                    case RoleEnum.SR_CHEMIST:
                        if (status == StatusEnum.SR_CHEMIST_CHECKING)
                        {
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_APPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_APPROVE) + ""));
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_DISAPPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_DISAPPROVE) + ""));
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = true;
                            pUploadfile.Visible = false;
                            pDownload.Visible = false;
                            btnSubmit.Visible = true;
                        }
                        break;
                    case RoleEnum.ADMIN:
                        if (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD)
                        {
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = false;
                            pUploadfile.Visible = true;
                            pDownload.Visible = true;
                            btnSubmit.Visible = true;
                        }
                        break;
                    case RoleEnum.LABMANAGER:
                        if (status == StatusEnum.LABMANAGER_CHECKING)
                        {
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_APPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_APPROVE) + ""));
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_DISAPPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_DISAPPROVE) + ""));
                            pRemark.Visible = false;
                            pDisapprove.Visible = false;
                            pSpecification.Visible = false;
                            pStatus.Visible = true;
                            pUploadfile.Visible = false;
                            pDownload.Visible = true;
                            btnSubmit.Visible = true;
                        }
                        break;
                }

                txtDateAnalyzed.Text = (this.jobSample.date_chemist_alalyze != null) ? this.jobSample.date_chemist_alalyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = userRole == RoleEnum.CHEMIST;

                #region "VISIBLE RESULT DATA"
                if (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                {
                    //#region ":: STAMP ANALYZED DATE ::"
                    //if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                    //{
                    //    if (this.jobSample.date_chemist_alalyze == null)
                    //    {
                    //        this.jobSample.date_chemist_alalyze = DateTime.Now;
                    //        this.jobSample.Update();
                    //    }
                    //}
                    //#endregion


                    txtProcedure.Enabled = true;
                    txtSampleSize.Enabled = true;
                    txtExtractionMedium.Enabled = true;
                    txtExtractionVolumn.Enabled = true;
                    btnCoverPage.Visible = true;
                    btnRH.Visible = true;
                    btnExtractable.Visible = true;
                    btnMotorOil.Visible = true;
                    gvMotorOil.Columns[3].Visible = true;
                }
                else
                {
                    txtProcedure.Enabled = false;
                    txtSampleSize.Enabled = false;
                    txtExtractionMedium.Enabled = false;
                    txtExtractionVolumn.Enabled = false;
                    btnCoverPage.Visible = false;
                    btnRH.Visible = false;
                    btnExtractable.Visible = false;
                    btnMotorOil.Visible = false;

                    gvMotorOil.Columns[3].Visible = false;

                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
                    {
                        btnCoverPage.Visible = true;
                        btnRH.Visible = true;
                        btnExtractable.Visible = true;
                        btnMotorOil.Visible = true;
                    }
                }
                #endregion
                #region "COVERPAGE"
                if (this.coverpages != null && this.coverpages.Count > 0)
                {

                    this.CommandName = CommandNameEnum.Edit;
                    //Result Description
                    template_seagate_gcms_coverpage cov = this.coverpages[0];
                    tb_m_component component = new tb_m_component().SelectByID(this.coverpages[0].component_id.Value);// this.coverpages[0].tb_m_component;
                    if (component != null)
                    {
                        ddlComponent.SelectedValue = component.ID.ToString();
                        ddlUnitMotorOilContamination.SelectedValue = this.coverpages[0].UnitMotorOilContamination == null ? "0" : this.coverpages[0].UnitMotorOilContamination.ToString();
                        ddlUnitMotorHub.SelectedValue = this.coverpages[0].UnitMotorHub == null ? "0" : this.coverpages[0].UnitMotorHub.ToString();
                        ddlUnitMotorHubSub.SelectedValue = this.coverpages[0].UnitMotorHubSub == null ? "0" : this.coverpages[0].UnitMotorHubSub.ToString();
                        ddlUnitMotorBase.SelectedValue = this.coverpages[0].UnitMotorBase == null ? "0" : this.coverpages[0].UnitMotorBase.ToString();
                        ddlUnitMotorBaseSub.SelectedValue = this.coverpages[0].UnitMotorBaseSub == null ? "0" : this.coverpages[0].UnitMotorBaseSub.ToString();
                        ddlUnitCompound.SelectedValue = this.coverpages[0].UnitCompound == null ? "0" : this.coverpages[0].UnitCompound.ToString();




                        cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;
                        if (cbCheckBox.Checked)
                        {
                            lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
                        }
                        else
                        {
                            lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", component.B, component.A);

                        }
                    }
                    txtProcedure.Text = cov.procedure_no;
                    txtSampleSize.Text = cov.sample_size;
                    txtExtractionMedium.Text = cov.extraction_medium;
                    txtExtractionVolumn.Text = cov.extraction_volumn;
                    //

                    txtB13.Text = cov.B13;
                    txtB14.Text = cov.B14;
                    lbB15.Text = cov.B15;
                    txtB18.Text = cov.B18;
                    txtB19.Text = cov.B19;
                    lbB20.Text = cov.B20;
                    txtB23.Text = cov.B23;
                    txtB24.Text = cov.B24;
                    txtB25.Text = cov.B25;
                    txtB26.Text = cov.B26;
                    txtB27.Text = cov.B27;
                    txtB28.Text = cov.B28;
                    txtC23.Text = cov.C23;
                    txtC24.Text = cov.C24;
                    txtC25.Text = cov.C25;
                    txtC26.Text = cov.C26;
                    txtC27.Text = cov.C27;
                    txtC28.Text = cov.C28;
                    txtB30.Text = cov.B30;
                    txtB31.Text = cov.B31;
                    lbB32.Text = cov.B32;

                    txtC30.Text = cov.C30;
                    txtC31.Text = cov.C31;
                    lbC32.Text = cov.C32;
                    txtB34.Text = cov.B34;
                    txtB35.Text = cov.B35;
                    lbB36.Text = cov.B36;
                    txtC34.Text = cov.C34;
                    txtC35.Text = cov.C35;
                    lbC36.Text = cov.C36;
                    txtB39.Text = cov.B39;
                    txtB40.Text = cov.B40;
                    lbB43.Text = cov.B43;
                    lbB44.Text = cov.B44;
                    lbC43.Text = cov.C43;
                    lbC44.Text = cov.C44;
                    lbD43.Text = cov.D43;
                    lbD44.Text = cov.D44;

                    txtB47.Text = cov.B47;
                    txtB48.Text = cov.B48;
                    txtB49.Text = cov.B49;
                    txtB51.Text = cov.B51;
                    txtB52.Text = cov.B52;
                    txtB53.Text = cov.B53;
                    txtB54.Text = cov.B54;
                    txtB55.Text = cov.B55;
                    txtB56.Text = cov.B56;
                    txtB57.Text = cov.B57;
                    txtB58.Text = cov.B58;
                    txtB59.Text = cov.B59;
                    txtB60.Text = cov.B60;
                    txtB61.Text = cov.B61;
                    txtB62.Text = cov.B62;

                    //txtB13_MO.Text = cov.B13_MO;
                    //txtB13_MO.Text = cov.B13_MO;
                    //txtB14_MO.Text = cov.B14_MO;
                    //txtB17_MO.Text = cov.B17_MO;
                    //txtC17_MO.Text = cov.C17_MO;
                    //txtD17_MO.Text = cov.D17_MO;
                    //txtB18_MO.Text = cov.B18_MO;
                    //txtC18_MO.Text = cov.C18_MO;
                    //txtD18_MO.Text = cov.D18_MO;

                    //txtB20_MO.Text = cov.B20_MO;
                    //txtB21_MO.Text = cov.B21_MO;
                    //txtB22_MO.Text = cov.B22_MO;
                    //txtB23_MO.Text = cov.B23_MO;

                    //txtB25_MO.Text = cov.B25_MO;
                    //txtB26_MO.Text = cov.B26_MO;




                    //txtC20_MO.Text = cov.C20_MO;
                    //txtC21_MO.Text = cov.C21_MO;
                    //txtC22_MO.Text = cov.C22_MO;
                    //txtC23_MO.Text = cov.C23_MO;
                    //txtC26_MO.Text = cov.C25_MO;
                    //txtC26_MO.Text = cov.C26_MO;

                    //txtD20_MO.Text = cov.D20_MO;
                    //txtD21_MO.Text = cov.D21_MO;
                    //txtD22_MO.Text = cov.D22_MO;
                    //txtD23_MO.Text = cov.D23_MO;
                    //txtD26_MO.Text = cov.D25_MO;
                    //txtD26_MO.Text = cov.D26_MO;

                    //lbB30_MO.Text = cov.B30_MO;
                    //lbC30_MO.Text = cov.C30_MO;
                    //lbD30_MO.Text = cov.D30_MO;
                    //lbE30_MO.Text = cov.E30_MO;

                    //lbB31_MO.Text = cov.B31_MO;
                    //lbC31_MO.Text = cov.C31_MO;
                    //lbD31_MO.Text = cov.D31_MO;
                    //lbE31_MO.Text = cov.E31_MO;

                    //txtB28_MO.Text = cov.B28_MO;
                    //txtC28_MO.Text = cov.C28_MO;
                    //txtD28_MO.Text = cov.D28_MO;


                    GenerrateCoverPage();
                    #region "Unit"
                    //gvMotorOil.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                    //gvMotorOil.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                    //gvMotorBaseSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                    //gvMotorBaseSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                    //gvMotorBase.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorBase.SelectedItem.Text);
                    //gvMotorBase.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBase.SelectedItem.Text);
                    //gvMotorHubSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                    //gvMotorHubSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                    //gvMotorHub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorHub.SelectedItem.Text);
                    //gvMotorHub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHub.SelectedItem.Text);
                    //gvCompound.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitCompound.SelectedItem.Text);
                    //gvCompound.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);
                    //gvCompoundSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitCompound.SelectedItem.Text);
                    //gvCompoundSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);
                    #endregion
                }
                #endregion
            }

            #endregion


            //initial button.
            btnCoverPage.CssClass = "btn blue";
            btnRH.CssClass = "btn green";
            btnExtractable.CssClass = "btn green";
            btnMotorOil.CssClass = "btn green";

            pCoverpage.Visible = true;
            pRH.Visible = false;
            pExtractable.Visible = false;
            pMotorOil.Visible = false;
            pLoadFile.Visible = false;

            switch (lbJobStatus.Text)
            {
                case "CONVERT_PDF":
                    litDownloadIcon.Text = "<i class=\"fa fa-file-pdf-o\"></i>";
                    break;
                default:
                    litDownloadIcon.Text = "<i class=\"fa fa-file-word-o\"></i>";
                    break;
            }

        }

        #endregion

        List<String> errors = new List<string>();


        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;
            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        #region "Button"

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Boolean isValid = true;

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    this.jobSample.step1owner = userLogin.id;
                    template_seagate_gcms_coverpage.DeleteBySampleID(this.SampleID);
                    tb_m_gcms_cas.DeleteBySampleID(this.SampleID);
                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.jobSample.step2owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    #region "Cover Page#"
                    foreach (template_seagate_gcms_coverpage cov in this.coverpages)
                    {
                        cov.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        cov.procedure_no = txtProcedure.Text;
                        cov.sample_size = txtSampleSize.Text;
                        cov.extraction_medium = txtExtractionMedium.Text;
                        cov.extraction_volumn = txtExtractionVolumn.Text;

                        cov.UnitMotorOilContamination = Convert.ToInt32(ddlUnitMotorOilContamination.SelectedValue);
                        cov.UnitMotorHub = Convert.ToInt32(ddlUnitMotorHub.SelectedValue);
                        cov.UnitMotorHubSub = Convert.ToInt32(ddlUnitMotorHubSub.SelectedValue);
                        cov.UnitMotorBase = Convert.ToInt32(ddlUnitMotorBase.SelectedValue);
                        cov.UnitMotorBaseSub = Convert.ToInt32(ddlUnitMotorBaseSub.SelectedValue);
                        cov.UnitCompound = Convert.ToInt32(ddlUnitCompound.SelectedValue);


                    }
                    template_seagate_gcms_coverpage.DeleteBySampleID(this.SampleID);
                    template_seagate_gcms_coverpage.InsertList(this.coverpages);

                    #endregion
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    //CalculateCas(false);
                    if (this.tbCas.Count > 0)
                    {
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                        this.jobSample.step3owner = userLogin.id;
                        this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                        #region ":: STAMP COMPLETE DATE"
                        this.jobSample.date_chemist_complete = DateTime.Now;
                        this.jobSample.date_chemist_alalyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                        #endregion
                        #region "CAS#"
                        tb_m_gcms_cas.DeleteBySampleID(this.SampleID);
                        tb_m_gcms_cas.InsertList(this.tbCas);
                        #endregion
                        #region "Cover Page#"
                        foreach (template_seagate_gcms_coverpage cov in this.coverpages)
                        {
                            cov.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                            cov.procedure_no = txtProcedure.Text;
                            cov.sample_size = txtSampleSize.Text;
                            cov.extraction_medium = txtExtractionMedium.Text;
                            cov.extraction_volumn = txtExtractionVolumn.Text;
                            //
                            cov.B13 = txtB13.Text;
                            cov.B14 = txtB14.Text;
                            cov.B15 = lbB15.Text;
                            cov.B18 = txtB18.Text;
                            cov.B19 = txtB19.Text;
                            cov.B20 = lbB20.Text;
                            cov.B23 = txtB23.Text;
                            cov.B24 = txtB24.Text;
                            cov.B25 = txtB25.Text;
                            cov.B26 = txtB26.Text;
                            cov.B27 = txtB27.Text;
                            cov.B28 = txtB28.Text;
                            cov.C23 = txtC23.Text;
                            cov.C24 = txtC24.Text;
                            cov.C25 = txtC25.Text;
                            cov.C26 = txtC26.Text;
                            cov.C27 = txtC27.Text;
                            cov.C28 = txtC28.Text;
                            cov.B30 = txtB30.Text;
                            cov.B31 = txtB31.Text;
                            cov.B32 = lbB32.Text;

                            cov.C30 = txtC30.Text;
                            cov.C31 = txtC31.Text;
                            cov.C32 = lbC32.Text;

                            cov.B34 = txtB34.Text;
                            cov.B35 = txtB35.Text;
                            cov.B36 = lbB36.Text;

                            cov.C34 = txtC34.Text;
                            cov.C35 = txtC35.Text;
                            cov.C36 = lbC36.Text;

                            cov.B39 = txtB39.Text;
                            cov.B40 = txtB40.Text;
                            cov.B43 = lbB43.Text;
                            cov.B44 = lbB44.Text;

                            cov.C43 = lbC43.Text;
                            cov.C44 = lbC44.Text;
                            cov.D43 = lbD43.Text;
                            cov.D44 = lbD44.Text;

                            cov.B47 = txtB47.Text;
                            cov.B48 = txtB48.Text;
                            cov.B49 = txtB49.Text;
                            cov.B51 = txtB51.Text;
                            cov.B52 = txtB52.Text;
                            cov.B53 = txtB53.Text;
                            cov.B54 = txtB54.Text;
                            cov.B55 = txtB55.Text;
                            cov.B56 = txtB56.Text;
                            cov.B57 = txtB57.Text;
                            cov.B58 = txtB58.Text;
                            cov.B59 = txtB59.Text;
                            cov.B60 = txtB60.Text;
                            cov.B61 = txtB61.Text;
                            cov.B62 = txtB62.Text;

                            //cov.B13_MO = txtB13_MO.Text;
                            //cov.B13_MO = txtB13_MO.Text;
                            //cov.B14_MO = txtB14_MO.Text;
                            //cov.B17_MO = txtB17_MO.Text;
                            //cov.C17_MO = txtC17_MO.Text;
                            //cov.D17_MO = txtD17_MO.Text;
                            //cov.B18_MO = txtB18_MO.Text;
                            //cov.C18_MO = txtC18_MO.Text;
                            //cov.D18_MO = txtD18_MO.Text;

                            //cov.B20_MO = txtB20_MO.Text;
                            //cov.B21_MO = txtB21_MO.Text;
                            //cov.B22_MO = txtB22_MO.Text;
                            //cov.B23_MO = txtB23_MO.Text;
                            //cov.B25_MO = txtB26_MO.Text;
                            //cov.B26_MO = txtB26_MO.Text;

                            //cov.C20_MO = txtC20_MO.Text;
                            //cov.C21_MO = txtC21_MO.Text;
                            //cov.C22_MO = txtC22_MO.Text;
                            //cov.C23_MO = txtC23_MO.Text;
                            //cov.C25_MO = txtC26_MO.Text;
                            //cov.C26_MO = txtC26_MO.Text;

                            //cov.D20_MO = txtD20_MO.Text;
                            //cov.D21_MO = txtD21_MO.Text;
                            //cov.D22_MO = txtD22_MO.Text;
                            //cov.D23_MO = txtD23_MO.Text;
                            //cov.D25_MO = txtD25_MO.Text;
                            //cov.D26_MO = txtD26_MO.Text;

                            //cov.B30_MO = lbB30_MO.Text;
                            //cov.C30_MO = lbC30_MO.Text;
                            //cov.D30_MO = lbD30_MO.Text;
                            //cov.E30_MO = lbE30_MO.Text;

                            //cov.B31_MO = lbB31_MO.Text;
                            //cov.C31_MO = lbC31_MO.Text;
                            //cov.D31_MO = lbD31_MO.Text;
                            //cov.E31_MO = lbE31_MO.Text;

                            //cov.B28_MO = txtB28_MO.Text;
                            //cov.C28_MO = txtC28_MO.Text;
                            //cov.D28_MO = txtD28_MO.Text;


                            cov.UnitMotorOilContamination = Convert.ToInt32(ddlUnitMotorOilContamination.SelectedValue);
                            cov.UnitMotorHub = Convert.ToInt32(ddlUnitMotorHub.SelectedValue);
                            cov.UnitMotorHubSub = Convert.ToInt32(ddlUnitMotorHubSub.SelectedValue);
                            cov.UnitMotorBase = Convert.ToInt32(ddlUnitMotorBase.SelectedValue);
                            cov.UnitMotorBaseSub = Convert.ToInt32(ddlUnitMotorBaseSub.SelectedValue);
                            cov.UnitCompound = Convert.ToInt32(ddlUnitCompound.SelectedValue);

                            //cov.selected_base = Convert.ToInt32(ddlBase.SelectedValue);

                        }
                        template_seagate_gcms_coverpage.UpdateList(this.coverpages);

                        #endregion
                    }
                    else
                    {
                        errors.Add("ไม่พบข้อมูล WorkSheet");
                    }
                    break;
                case StatusEnum.SR_CHEMIST_CHECKING:
                    StatusEnum srChemistApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (srChemistApproveStatus)
                    {
                        case StatusEnum.SR_CHEMIST_APPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                            //#region ":: STAMP COMPLETE DATE"
                            //this.jobSample.sr_approve_date = DateTime.Now;
                            //#endregion
                            break;
                        case StatusEnum.SR_CHEMIST_DISAPPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                            #region "LOG"
                            job_sample_logs jobSampleLog = new job_sample_logs
                            {
                                ID = 0,
                                job_sample_id = this.jobSample.ID,
                                log_title = String.Format("Sr.Chemist DisApprove"),
                                job_remark = txtRemark.Text,
                                is_active = "0",
                                date = DateTime.Now
                            };
                            jobSampleLog.Insert();
                            #endregion
                            break;
                    }
                    this.jobSample.step4owner = userLogin.id;

                    break;
                case StatusEnum.LABMANAGER_CHECKING:
                    StatusEnum labApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (labApproveStatus)
                    {
                        case StatusEnum.LABMANAGER_APPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF);

                            this.jobSample.date_labman_complete = DateTime.Now;
                            break;
                        case StatusEnum.LABMANAGER_DISAPPROVE:
                            this.jobSample.job_status = Convert.ToInt32(ddlAssignTo.SelectedValue);
                            #region "LOG"
                            job_sample_logs jobSampleLog = new job_sample_logs
                            {
                                ID = 0,
                                job_sample_id = this.jobSample.ID,
                                log_title = String.Format("Lab Manager DisApprove"),
                                job_remark = txtRemark.Text,
                                is_active = "0",
                                date = DateTime.Now
                            };
                            jobSampleLog.Insert();
                            #endregion
                            break;
                    }
                    this.jobSample.step5owner = userLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (FileUpload1.HasFile)// && (Path.GetExtension(FileUpload1.FileName).Equals(".doc") || Path.GetExtension(FileUpload1.FileName).Equals(".docx")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                        String source_file_url = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        FileUpload1.SaveAs(source_file);
                        this.jobSample.path_word = source_file_url;
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING);
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
                    this.jobSample.step6owner = userLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:

                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                    this.jobSample.step7owner = userLogin.id;
                    break;

            }
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();
            }
            else
            {
                litErrorMessage.Text = String.Empty;
                //########
                this.jobSample.update_date = DateTime.Now;
                this.jobSample.update_by = userLogin.id;
                this.jobSample.Update();

                //Commit
                GeneralManager.Commit();

                removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(this.PreviousPath);
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            String sheetName = string.Empty;

            List<tb_m_gcms_cas> _cas = new List<tb_m_gcms_cas>();
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            for (int i = 0; i < btnUpload.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = btnUpload.PostedFiles[i];
                try
                {
                    if (_postedFile.ContentLength > 0)
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));
                        //String source_file_url = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));

                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        _postedFile.SaveAs(source_file);
                        #region "XLS"
                        if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")))
                        {
                            using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                            {
                                HSSFWorkbook wb = new HSSFWorkbook(fs);
                                #region "RHC"
                                ISheet isheet = wb.GetSheet("RHC");
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhcbase"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;
                                    for (int j = 15; j < isheet.LastRowNum; j++)
                                    {
                                        if (isheet.GetRow(j) != null)
                                        {
                                            tb_m_gcms_cas tmp = new tb_m_gcms_cas();
                                            tmp.ID = j;
                                            tmp.sample_id = this.SampleID;
                                            tmp.pk = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(0));
                                            tmp.rt = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1));
                                            tmp.library_id = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(2));
                                            tmp.classification = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(3));
                                            tmp.cas = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(4));
                                            tmp.qual = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(5));
                                            tmp.area = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                            if (isheet.GetRow(j).GetCell(6) != null)
                                            {
                                                if (isheet.GetRow(j).GetCell(6).CellType != CellType.Blank)
                                                {
                                                    tmp.amount = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                                }
                                            }
                                            tmp.row_type = (

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.classification) ||

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.library_id) && !String.IsNullOrEmpty(tmp.area)) ? Convert.ToInt32(RowTypeEnum.TotalRow) : Convert.ToInt32(RowTypeEnum.Normal);
                                            tmp.cas_group = Convert.ToInt32(GcmsSeagateEnum.RHC_BASE);
                                            if (!String.IsNullOrEmpty(tmp.area))
                                            {
                                                _cas.Add(tmp);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region "RHC-BASE"
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhcbase"]);
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhcbase"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;
                                    for (int j = 15; j < isheet.LastRowNum; j++)
                                    {
                                        if (isheet.GetRow(j) != null)
                                        {
                                            tb_m_gcms_cas tmp = new tb_m_gcms_cas();
                                            tmp.ID = j;
                                            tmp.sample_id = this.SampleID;
                                            tmp.pk = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(0));
                                            tmp.rt = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1));
                                            tmp.library_id = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(2));
                                            tmp.classification = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(3));
                                            tmp.cas = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(4));
                                            tmp.qual = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(5));
                                            tmp.area = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                            if (isheet.GetRow(j).GetCell(6) != null)
                                            {
                                                if (isheet.GetRow(j).GetCell(6).CellType != CellType.Blank)
                                                {
                                                    tmp.amount = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                                }
                                            }
                                            tmp.row_type = (

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.classification) ||

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.library_id) && !String.IsNullOrEmpty(tmp.area)) ? Convert.ToInt32(RowTypeEnum.TotalRow) : Convert.ToInt32(RowTypeEnum.Normal);
                                            tmp.cas_group = Convert.ToInt32(GcmsSeagateEnum.RHC_BASE);
                                            if (!String.IsNullOrEmpty(tmp.area))
                                            {
                                                _cas.Add(tmp);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region "RHC-HUB"
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhchub"]);
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.rhchub"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    for (int j = 15; j < isheet.LastRowNum; j++)
                                    {
                                        if (isheet.GetRow(j) != null)
                                        {
                                            tb_m_gcms_cas tmp = new tb_m_gcms_cas();
                                            tmp.ID = j;
                                            tmp.sample_id = this.SampleID;
                                            tmp.pk = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(0));
                                            tmp.rt = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1));
                                            tmp.library_id = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(2));
                                            tmp.classification = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(3));
                                            tmp.cas = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(4));
                                            tmp.qual = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(5));
                                            tmp.area = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                            if (isheet.GetRow(j).GetCell(6) != null)
                                            {
                                                if (isheet.GetRow(j).GetCell(6).CellType != CellType.Blank)
                                                {
                                                    tmp.amount = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                                }
                                            }
                                            tmp.row_type = (

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.classification) ||

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.library_id) && !String.IsNullOrEmpty(tmp.area)) ? Convert.ToInt32(RowTypeEnum.TotalRow) : Convert.ToInt32(RowTypeEnum.Normal);
                                            tmp.cas_group = Convert.ToInt32(GcmsSeagateEnum.RHC_HUB);
                                            if (!String.IsNullOrEmpty(tmp.area))
                                            {
                                                _cas.Add(tmp);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region "Workingpg - Extractable"
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.workingpg_extractable"]);
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.workingpg_extractable"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    txtB13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.B));
                                    txtB14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.B));
                                    lbB15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.B));
                                    txtB18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.B));
                                    txtB19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.B));
                                    lbB20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.B));
                                    txtB23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.B));
                                    txtB24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.B));
                                    txtB25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.B));

                                    txtB26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.B));

                                    txtB27.Text = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.B))), Convert.ToInt32(txtDecimal01.Text)).ToString();//Recovery of internal standard (Hub)	2
                                    txtB28.Text = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.B))), Convert.ToInt32(txtDecimal02.Text)).ToString();//Recovery of internal standard (Base)	2

                                    txtC23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.C));
                                    txtC24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.C));
                                    txtC25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.C));
                                    txtC26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.C));
                                    txtC27.Text = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.C))), Convert.ToInt32(txtDecimal01.Text)).ToString();
                                    txtC28.Text = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.C))), Convert.ToInt32(txtDecimal02.Text)).ToString();
                                    txtB30.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.B));
                                    txtB31.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.B));
                                    lbB32.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.B));

                                    txtC30.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.C));
                                    txtC31.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.C));
                                    lbC32.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.C));
                                    txtB34.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.B));
                                    txtB35.Text = CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.B));
                                    lbB36.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.B));
                                    txtC34.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.C));
                                    txtC35.Text = CustomUtils.GetCellValue(isheet.GetRow(35 - 1).GetCell(ExcelColumn.C));
                                    lbC36.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.C));
                                    txtB39.Text = CustomUtils.GetCellValue(isheet.GetRow(39 - 1).GetCell(ExcelColumn.B));
                                    txtB40.Text = CustomUtils.GetCellValue(isheet.GetRow(40 - 1).GetCell(ExcelColumn.B));

                                    lbB43.Text = (isheet.GetRow(43 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(43 - 1).GetCell(ExcelColumn.B)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal03.Text)).ToString(); //Compunds <= DOP			2
                                    lbB44.Text = (isheet.GetRow(44 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(44 - 1).GetCell(ExcelColumn.B)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal03.Text)).ToString(); //Compunds >= DOP			2

                                    lbC43.Text = (isheet.GetRow(43 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(43 - 1).GetCell(ExcelColumn.C)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal04.Text)).ToString();//Compunds <= DOP			2
                                    lbC44.Text = (isheet.GetRow(44 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(44 - 1).GetCell(ExcelColumn.C)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal04.Text)).ToString();//Compunds >= DOP			2

                                    lbD43.Text = (isheet.GetRow(43 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(43 - 1).GetCell(ExcelColumn.D)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal05.Text)).ToString();//Repeated Hydrocarcon		2
                                    lbD44.Text = (isheet.GetRow(44 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(44 - 1).GetCell(ExcelColumn.D)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal05.Text)).ToString();//Repeated Hydrocarcon		2

                                    txtB47.Text = (isheet.GetRow(47 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(47 - 1).GetCell(ExcelColumn.B)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal06.Text)).ToString();//Minimum RHC Detection Limit is	3
                                    txtB48.Text = (isheet.GetRow(48 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(48 - 1).GetCell(ExcelColumn.B)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal06.Text)).ToString();//Minimum RHC Detection Limit is	3
                                    txtB49.Text = (isheet.GetRow(49 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(49 - 1).GetCell(ExcelColumn.B)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal06.Text)).ToString();//Minimum RHC Detection Limit is	3

                                    txtB51.Text = (isheet.GetRow(51 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(51 - 1).GetCell(ExcelColumn.B)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal07.Text)).ToString();//R-Hub				2
                                    txtB52.Text = (isheet.GetRow(52 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(52 - 1).GetCell(ExcelColumn.B));

                                    txtB53.Text = (isheet.GetRow(53 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(53 - 1).GetCell(ExcelColumn.B));
                                    txtB54.Text = (isheet.GetRow(54 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(54 - 1).GetCell(ExcelColumn.B));
                                    txtB55.Text = (isheet.GetRow(55 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(55 - 1).GetCell(ExcelColumn.B));
                                    txtB56.Text = (isheet.GetRow(56 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(56 - 1).GetCell(ExcelColumn.B));
                                    txtB57.Text = (isheet.GetRow(57 - 1) == null) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(57 - 1).GetCell(ExcelColumn.B))), Convert.ToInt32(txtDecimal08.Text)).ToString();//R-Base	
                                    txtB58.Text = (isheet.GetRow(58 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(58 - 1).GetCell(ExcelColumn.B));
                                    txtB59.Text = (isheet.GetRow(59 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(59 - 1).GetCell(ExcelColumn.B));
                                    txtB60.Text = (isheet.GetRow(60 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(60 - 1).GetCell(ExcelColumn.B));
                                    txtB61.Text = (isheet.GetRow(61 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(61 - 1).GetCell(ExcelColumn.B));
                                    txtB62.Text = (isheet.GetRow(62 - 1) == null) ? "" : CustomUtils.GetCellValue(isheet.GetRow(62 - 1).GetCell(ExcelColumn.B));


                                    //--------
                                }
                                #endregion
                                #region "Workingpg - Motor Oil "
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.workingpg_motor_oil"]);
                                if (isheet == null)
                                {
                                    //errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.gcms.excel.sheetname.workingpg_motor_oil"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    txtB13_MO.Text = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.B))), Convert.ToInt32(txtDecimal09.Text)).ToString();//Surface area of Base			2
                                    txtB13_MO.Text = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.B))), Convert.ToInt32(txtDecimal10.Text)).ToString();//Surface area of Hub			2

                                    txtB14_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.B));

                                    txtB17_MO.Text = String.IsNullOrEmpty(CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.B))) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.B)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal11.Text)).ToString();
                                    txtC17_MO.Text = String.IsNullOrEmpty(CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.C))) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.C)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal11.Text)).ToString();
                                    txtD17_MO.Text = String.IsNullOrEmpty(CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.D))) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.D)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal11.Text)).ToString();

                                    txtB18_MO.Text = String.IsNullOrEmpty(CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.B))) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.B)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal12.Text)).ToString();
                                    txtC18_MO.Text = String.IsNullOrEmpty(CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.C))) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.C)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal12.Text)).ToString();
                                    txtD18_MO.Text = String.IsNullOrEmpty(CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.D))) ? "" : Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.D)).Replace(",", "").Replace(" ", "")), Convert.ToInt32(txtDecimal12.Text)).ToString();

                                    txtB20_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.B));
                                    txtB21_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.B));
                                    txtB22_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.B));


                                    txtB23_MO.Text = ddlBaseType.SelectedValue.Equals("1") ? CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.B)) : CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.B));//




                                    #region "Motor Hub"
                                    txtB26_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.B));
                                    txtC26_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.C));
                                    txtD26_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.D));
                                    #endregion
                                    #region "Motor Base / Base 3.5"
                                    txtB28_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.B));//
                                    txtC28_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.C));
                                    txtD28_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.D));
                                    #endregion



                                    txtC20_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.C));
                                    txtC21_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.C));
                                    txtC22_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.C));
                                    txtC23_MO.Text = ddlBaseType.SelectedValue.Equals("1") ? CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.C)) : CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.C));

                                    txtD20_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.D));
                                    txtD21_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.D));
                                    txtD22_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.D));
                                    txtD23_MO.Text = ddlBaseType.SelectedValue.Equals("1") ? CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.D)) : CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.D));

                                    lbB30_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.B));
                                    lbC30_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.C));
                                    lbD30_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.D));
                                    //lbE30_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.E));

                                    lbB31_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.B));
                                    lbC31_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.C));
                                    lbD31_MO.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.D));
                                    //lbE31_MO.Text = ddlBaseType.SelectedValue.Equals("1") ? CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.E)) : CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.E));
                                }
                                #endregion
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", sheetName, CustomUtils.ErrorIndex));
                    Console.WriteLine();
                }
            }
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();
            }
            else
            {
                litErrorMessage.Text = String.Empty;
                this.tbCas = _cas;
                gvRHCBase.DataSource = this.tbCas.Where(x => x.cas_group == Convert.ToInt32(GcmsSeagateEnum.RHC_BASE) && !x.library_id.Equals("0")).ToList();
                gvRHCBase.DataBind();
                gvRHCHub.DataSource = this.tbCas.Where(x => x.cas_group == Convert.ToInt32(GcmsSeagateEnum.RHC_HUB) && !x.library_id.Equals("0")).ToList();
                gvRHCHub.DataBind();
            }
        }


        protected void btnCoverPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case "btnCoverPage":
                    btnCoverPage.CssClass = "btn blue";
                    btnRH.CssClass = "btn green";
                    btnExtractable.CssClass = "btn green";
                    btnMotorOil.CssClass = "btn green";

                    pCoverpage.Visible = true;
                    pRH.Visible = false;
                    pExtractable.Visible = false;
                    pMotorOil.Visible = false;
                    pLoadFile.Visible = false;

                    GenerrateCoverPage();
                    break;
                case "btnRH":
                    btnCoverPage.CssClass = "btn green";
                    btnRH.CssClass = "btn blue";
                    btnExtractable.CssClass = "btn green";
                    btnMotorOil.CssClass = "btn green";

                    pCoverpage.Visible = false;
                    pRH.Visible = true;
                    pExtractable.Visible = false;
                    pMotorOil.Visible = false;
                    pLoadFile.Visible = false;
                    pLoadFile.Visible = true;

                    break;
                case "btnExtractable":
                    btnCoverPage.CssClass = "btn green";
                    btnRH.CssClass = "btn green";
                    btnExtractable.CssClass = "btn blue";
                    btnMotorOil.CssClass = "btn green";

                    pCoverpage.Visible = false;
                    pRH.Visible = false;
                    pExtractable.Visible = true;
                    pMotorOil.Visible = false;
                    pLoadFile.Visible = true;

                    break;
                case "btnMotorOil":
                    btnCoverPage.CssClass = "btn green";
                    btnRH.CssClass = "btn green";
                    btnExtractable.CssClass = "btn green";
                    btnMotorOil.CssClass = "btn blue";

                    pCoverpage.Visible = false;
                    pRH.Visible = false;
                    pExtractable.Visible = false;
                    pMotorOil.Visible = true;
                    pLoadFile.Visible = true;

                    break;

            }
        }

        #endregion

        #region "COVERPAGES GRID."

        protected void gvMotorOil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorOil.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorOil.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        protected void gvMotorOil_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorOil.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL)).ToList();
                    gvMotorOil.DataBind();
                }
            }
        }

        protected void gvMotorHub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorHub.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorHub.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        protected void gvMotorHub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorHub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE)).ToList();
                    gvMotorHub.DataBind();
                }
            }
        }

        protected void gvMotorHubSub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorHubSub.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorHubSub.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        protected void gvMotorHubSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorHubSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB)).ToList();
                    gvMotorHubSub.DataBind();
                }
            }
        }

        protected void gvMotorBase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorBase.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorBase.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        protected void gvMotorBase_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorBase.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE)).ToList();
                    gvMotorBase.DataBind();
                }
            }
        }

        protected void gvMotorBaseSub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvMotorBaseSub.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMotorBaseSub.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        protected void gvMotorBaseSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvMotorBaseSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB)).ToList();
                    gvMotorBaseSub.DataBind();
                }
            }
        }


        protected void gvCompound_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvCompound.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCompound.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        protected void gvCompound_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvCompound.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND)).ToList();
                    gvCompound.DataBind();
                }
            }
        }


        protected void gvCompoundSub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvCompoundSub.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCompoundSub.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        protected void gvCompoundSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB));
                if (gcms != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);
                            break;
                        case RowTypeEnum.Normal:
                            gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvCompoundSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB)).ToList();
                    gvCompoundSub.DataBind();
                }
            }
        }
        #endregion

        #region "DHS GRID."
        protected void gvRHCBase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int _id = Convert.ToInt32(gvRHCBase.DataKeys[e.Row.RowIndex].Values[0].ToString());

                tb_m_gcms_cas _cas = this.tbCas.Find(x => x.ID == _id);
                if (_cas != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)_cas.row_type);
                    switch (cmd)
                    {
                        case RowTypeEnum.Normal:
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                        case RowTypeEnum.TotalRow:
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            e.Row.Font.Bold = true;
                            break;
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }
        protected void gvRHCHub_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int _id = Convert.ToInt32(gvRHCHub.DataKeys[e.Row.RowIndex].Values[0].ToString());

                tb_m_gcms_cas _cas = this.tbCas.Find(x => x.ID == _id);
                if (_cas != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)_cas.row_type);
                    switch (cmd)
                    {
                        case RowTypeEnum.Normal:
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                        case RowTypeEnum.TotalRow:
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            e.Row.Font.Bold = true;
                            break;
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }

        #endregion



        #region "Custom method"

        private void GenerrateCoverPage()
        {


            //Note: This report was performed test by ALS Singapore.
            if (!string.IsNullOrEmpty(txtB47.Text))
            {
                lbRemark1.Text = String.Format("Remark: Minimum Detection Limit is {0} ng/part.", Math.Round(Convert.ToDecimal(txtB47.Text), 3));
            }
            if (!string.IsNullOrEmpty(txtB48.Text))
            {
                lbRemark2.Text = String.Format("Minimum RHC Detection Limit of Base is {0} ng/cm2", Math.Round(Convert.ToDecimal(txtB48.Text), 3));
            }
            if (!string.IsNullOrEmpty(txtB49.Text))
            {
                lbRemark3.Text = String.Format("Minimum RHC Detection Limit of Hub is {0} g/cm2", Math.Round(Convert.ToDecimal(txtB49.Text), 3));
            }

            #region "Binding"

            List<template_seagate_gcms_coverpage> motorOils = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL) && !x.A.Equals("-")).ToList();
            if (motorOils.Count > 0)
            {

                double c0 = Convert.ToDouble(String.IsNullOrEmpty(lbB30_MO.Text) ? "0" : lbB30_MO.Text) + Convert.ToDouble(String.IsNullOrEmpty(lbC30_MO.Text) ? "0" : lbC30_MO.Text);
                double c1 = Convert.ToDouble(String.IsNullOrEmpty(lbB31_MO.Text) ? "0" : lbB31_MO.Text) + Convert.ToDouble(String.IsNullOrEmpty(lbC31_MO.Text) ? "0" : lbC31_MO.Text);


                motorOils[0].C = c0 == 0 ? "Not Detected" : Math.Round(c0, 2) + "";
                motorOils[1].C = c1 == 0 ? "Not Detected" : Math.Round(c1, 2) + "";

                gvMotorOil.DataSource = motorOils;
                gvMotorOil.DataBind();
                gvMotorOil.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                gvMotorOil.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                gvMotorOil.Visible = true;
            }
            else
            {
                gvMotorOil.Visible = false;
            }

            List<template_seagate_gcms_coverpage> motorHubs = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB) && !x.A.Equals("-")).ToList();
            if (motorHubs.Count > 0)
            {

                if (!String.IsNullOrEmpty(lbD43.Text) && !String.IsNullOrEmpty(lbB31_MO.Text))
                {
                    motorHubs[0].C = (Convert.ToDecimal(lbD43.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(lbB31_MO.Text), 2) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)
                }

                gvMotorHub.DataSource = motorHubs;
                gvMotorHub.DataBind();


                gvMotorHub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorHub.SelectedItem.Text);
                gvMotorHub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHub.SelectedItem.Text);
                gvMotorHub.Visible = true;
            }
            else
            {
                gvMotorHub.Visible = false;
            }



            List<template_seagate_gcms_coverpage> motorHubSubs = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB) && !x.A.Equals("-")).ToList();
            if (motorHubSubs.Count > 0)
            {
                motorHubSubs[1].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(lbB43.Text) ? "0" : lbB43.Text), 2) + "";//Compounds with RT ≤ DOP
                motorHubSubs[2].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(lbC43.Text) ? "0" : lbC43.Text), 2) + "";//Compounds with RT > DOP
                motorHubSubs[0].C = Math.Round(Convert.ToDecimal((Convert.ToDecimal(motorHubSubs[1].C) + Convert.ToDecimal(motorHubSubs[2].C)) + ""), 2) + "";//Total Organic Compound (TOC)
                gvMotorHubSub.DataSource = motorHubSubs;
                gvMotorHubSub.DataBind();

                gvMotorHubSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                gvMotorHubSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                gvMotorHubSub.Visible = true;
            }
            else
            {
                gvMotorHubSub.Visible = false;
            }



            List<template_seagate_gcms_coverpage> motorBases = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE) && !x.A.Equals("-")).ToList();
            if (motorBases.Count > 0)
            {
                if (!String.IsNullOrEmpty(lbD44.Text) && !String.IsNullOrEmpty(lbB31_MO.Text))
                {
                    motorBases[0].C = (Convert.ToDecimal(lbD44.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(lbB31_MO.Text), 2) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)
                }
                gvMotorBase.DataSource = motorBases;

                gvMotorBase.DataBind();
                gvMotorBase.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorBase.SelectedItem.Text);
                gvMotorBase.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBase.SelectedItem.Text);
                gvMotorBase.Visible = true;
            }
            else
            {
                gvMotorBase.Visible = false;
            }


            List<template_seagate_gcms_coverpage> motorBaseSubs = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB) && !x.A.Equals("-")).ToList();
            if (motorBaseSubs.Count > 0)
            {

                motorBaseSubs[1].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(lbB44.Text) ? "0" : lbB44.Text), 2) + "";//Compounds with RT ≤ DOP
                motorBaseSubs[2].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(lbC44.Text) ? "0" : lbC44.Text), 2) + "";//Compounds with RT > DOP
                motorBaseSubs[0].C = Math.Round(Convert.ToDecimal((Convert.ToDecimal(motorBaseSubs[1].C) + Convert.ToDecimal(motorBaseSubs[2].C)) + ""), 2) + "";//Total Organic Compound (TOC)

                gvMotorBaseSub.DataSource = motorBaseSubs;
                gvMotorBaseSub.DataBind();

                gvMotorBaseSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                gvMotorBaseSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                gvMotorBaseSub.Visible = true;
            }
            else
            {
                gvMotorBaseSub.Visible = false;
            }



            List<template_seagate_gcms_coverpage> compounds = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND) && !x.A.Equals("-")).ToList();
            if (compounds.Count > 0)
            {

                if (!String.IsNullOrEmpty(lbD43.Text))
                {
                    compounds[0].C = (Convert.ToDecimal(lbD43.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(lbD43.Text), 2) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)
                }
                //compounds[2].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(lbB43.Text) ? "0" : lbC43.Text), 2) + "";//Compounds with RT > DOP
                //compounds[3].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(lbC43.Text) ? "0" : lbC43.Text), 2) + "";//Compounds with RT > DOP

                //compounds[1].C = (Convert.ToDecimal(compounds[2].C) + Convert.ToDecimal(compounds[3].C)) + "";



                gvCompound.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitCompound.SelectedItem.Text);
                gvCompound.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);

                gvCompound.DataSource = compounds;
                gvCompound.DataBind();

                gvCompound.Visible = true;
            }
            else
            {
                gvCompound.Visible = false;
            }
            List<template_seagate_gcms_coverpage> compoundSubs = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB) && !x.A.Equals("-")).ToList();
            if (compoundSubs.Count > 0)
            {

                //if (!String.IsNullOrEmpty(lbD43.Text))
                //{
                //    compoundSubs[0].C = (Convert.ToDecimal(lbD43.Text) == 0) ? "Not Detected" : Math.Round(Convert.ToDecimal(lbD43.Text), 2) + "";//Repeated Hydrocarbon (C20-C40 Alkanes)
                //}
                compoundSubs[1].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(lbB43.Text) ? "0" : lbC43.Text), 2) + "";//Compounds with RT > DOP
                compoundSubs[2].C = Math.Round(Convert.ToDecimal(String.IsNullOrEmpty(lbC43.Text) ? "0" : lbC43.Text), 2) + "";//Compounds with RT > DOP

                compoundSubs[0].C = (Convert.ToDecimal(compoundSubs[1].C) + Convert.ToDecimal(compoundSubs[2].C)) + "";


                gvCompoundSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitCompound.SelectedItem.Text);
                gvCompoundSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);

                gvCompoundSub.DataSource = compoundSubs;
                gvCompoundSub.DataBind();

                gvCompoundSub.Visible = true;
            }
            else
            {
                gvCompoundSub.Visible = false;
            }
            #endregion
        }

        #endregion

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {

            tb_m_component component = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (component != null)
            {
                if (!String.IsNullOrEmpty(component.E))
                {
                    tb_m_detail_spec_ref detailSpecRef = new tb_m_detail_spec_ref();
                    detailSpecRef.spec_ref = Convert.ToInt32(component.E);
                    detailSpecRef.template_id = this.jobSample.template_id;


                    List<tb_m_detail_spec_ref> detailSpecRefs = (List<tb_m_detail_spec_ref>)detailSpecRef.SelectAll();

                    List<template_seagate_gcms_coverpage> newCoverPage = new List<template_seagate_gcms_coverpage>();
                    int index = 0;
                    foreach (tb_m_detail_spec_ref spec in detailSpecRefs)
                    {
                        template_seagate_gcms_coverpage work = new template_seagate_gcms_coverpage();
                        work.ID = spec.ID;// (this.CommandName == CommandNameEnum.Add) ? index : this.coverpages[index].ID;
                        work.sample_id = this.SampleID;
                        work.component_id = component.ID;
                        work.A = spec.B;
                        work.B = spec.C;
                        work.C = string.Empty;
                        work.RowState = this.CommandName;
                        work.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                        newCoverPage.Add(work);
                        index++;
                    }

                    //if (newCoverPage.Count > 0)
                    //{

                    //    if (newCoverPage.Count >= 10)
                    //    {
                    /*
                    # Motor Oil Contamination
                    1.Motor Oil - Base 0.6
                    2.Motor Oil - Hub  10
                    # Motor Hub
                    3.Repeated Hydrocarbon(C20 - C40 Alkanes)
                    4.Total Organic Compound (TOC)
                    5.Compounds with RT ≤ DOP
                    6.Compounds with RT > DOP
                    # Motor Base
                    7.Repeated Hydrocarbon(C20 - C40 Alkanes)
                    8.Total Organic Compound(TOC)
                    9.Compounds with RT ≤ DOP
                    10.Compounds with RT > DOP
                    */



                    newCoverPage[0].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                    newCoverPage[1].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL);
                    newCoverPage[2].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB);
                    newCoverPage[3].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                    newCoverPage[4].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                    newCoverPage[5].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB);
                    newCoverPage[6].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE);
                    newCoverPage[7].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                    newCoverPage[8].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                    newCoverPage[9].data_type = Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB);
                    newCoverPage[10].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                    newCoverPage[11].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    newCoverPage[12].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    newCoverPage[13].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    newCoverPage[14].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    newCoverPage[15].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    newCoverPage[16].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    newCoverPage[17].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    newCoverPage[18].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);
                    newCoverPage[19].data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB);



                    //}
                    //else
                    //{
                    //    foreach (template_seagate_gcms_coverpage item in newCoverPage)
                    //    {
                    //        item.data_type = Convert.ToInt32(SeagateGcmsEnum.COMPOUND);
                    //    }
                    //}
                    //}

                    this.coverpages = newCoverPage;

                    #region "Binding"
                    gvMotorOil.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL) && !x.A.Equals("-"));
                    gvMotorOil.DataBind();
                    if (gvMotorOil.Rows.Count > 0)
                    {

                        gvMotorOil.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                        gvMotorOil.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
                        gvMotorOil.Visible = true;
                    }
                    else
                    {
                        gvMotorOil.Visible = false;
                    }
                    gvMotorHub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB) && !x.A.Equals("-"));
                    gvMotorHub.DataBind();
                    if (gvMotorHub.Rows.Count > 0)
                    {
                        gvMotorHub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorHub.SelectedItem.Text);
                        gvMotorHub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHub.SelectedItem.Text);
                        gvMotorHub.Visible = true;
                    }
                    else
                    {
                        gvMotorHub.Visible = false;
                    }
                    gvMotorHubSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB) && !x.A.Equals("-"));
                    gvMotorHubSub.DataBind();
                    if (gvMotorHubSub.Rows.Count > 0)
                    {
                        gvMotorHubSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                        gvMotorHubSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
                        gvMotorHubSub.Visible = true;
                    }
                    else
                    {
                        gvMotorHubSub.Visible = false;
                    }
                    gvMotorBase.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE) && !x.A.Equals("-"));
                    gvMotorBase.DataBind();
                    if (gvMotorBase.Rows.Count > 0)
                    {
                        gvMotorBase.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorBase.SelectedItem.Text);
                        gvMotorBase.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBase.SelectedItem.Text);
                        gvMotorBase.Visible = true;
                    }
                    else
                    {
                        gvMotorBase.Visible = false;
                    }
                    gvMotorBaseSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB) && !x.A.Equals("-"));
                    gvMotorBaseSub.DataBind();
                    if (gvMotorBaseSub.Rows.Count > 0)
                    {
                        gvMotorBaseSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                        gvMotorBaseSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
                        gvMotorBaseSub.Visible = true;
                    }
                    else
                    {
                        gvMotorBaseSub.Visible = false;
                    }
                    gvCompound.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND) && !x.A.Equals("-"));
                    gvCompound.DataBind();
                    if (gvCompound.Rows.Count > 0)
                    {
                        gvCompound.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitCompound.SelectedItem.Text);
                        gvCompound.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);
                        gvCompound.Visible = true;
                    }
                    else
                    {
                        gvCompound.Visible = false;
                    }
                    gvCompoundSub.DataSource = this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND_SUB) && !x.A.Equals("-"));
                    gvCompoundSub.DataBind();
                    if (gvCompoundSub.Rows.Count > 0)
                    {
                        gvCompoundSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", ddlUnitCompound.SelectedItem.Text);
                        gvCompoundSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);
                        gvCompoundSub.Visible = true;
                    }
                    else
                    {
                        gvCompoundSub.Visible = false;
                    }


                    #endregion




                    lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", component.B, component.A);
                    txtProcedure.Text = component.F;
                    txtSampleSize.Text = component.G;
                    //lbExtractionMedium.Text;
                    txtExtractionVolumn.Text = component.H;


                    //gvMotorOil.DataSource = this.coverpages;
                    //gvMotorOil.DataBind();
                    //}
                }
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);
                ReportHeader reportHeader = new ReportHeader();
                reportHeader = reportHeader.getReportHeder(this.jobSample);


                ReportParameterCollection reportParameters = new ReportParameterCollection();

                reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo + " "));
                reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
                reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
                reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));
                reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));

                reportParameters.Add(new ReportParameter("rpt_unit1", ddlUnitMotorOilContamination.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit2", ddlUnitMotorHub.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit3", ddlUnitMotorHubSub.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit4", ddlUnitMotorBase.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit5", ddlUnitMotorBaseSub.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit6", ddlUnitCompound.SelectedItem.Text));



                reportParameters.Add(new ReportParameter("Test", "GCMS Extractable"));
                reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
                reportParameters.Add(new ReportParameter("Remark1", lbRemark1.Text));
                reportParameters.Add(new ReportParameter("Remark2", lbRemark2.Text));
                reportParameters.Add(new ReportParameter("Remark3", lbRemark3.Text));


                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/gcms_seagate.rdlc");
                viewer.LocalReport.SetParameters(reportParameters);


                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL)).ToList().ToDataTable()));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB)).ToList().ToDataTable()));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB)).ToList().ToDataTable()));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE)).ToList().ToDataTable()));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB)).ToList().ToDataTable()));
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet7", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND)).ToList().ToDataTable()));


                string download = String.Empty;

                StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
                switch (status)
                {
                    case StatusEnum.ADMIN_CONVERT_WORD:
                        if (!String.IsNullOrEmpty(this.jobSample.path_word))
                        {
                            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
                        }
                        else
                        {
                            byte[] bytes = viewer.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                            if (!Directory.Exists(Server.MapPath("~/Report/")))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Report/"));
                            }
                            using (FileStream fs = File.Create(Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension))
                            {
                                fs.Write(bytes, 0, bytes.Length);
                            }


                            #region "Insert Footer & Header from template"
                            Document doc1 = new Document();
                            doc1.LoadFromFile(Server.MapPath("~/template/") + "Blank Letter Head - EL.doc");
                            Spire.Doc.HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
                            Spire.Doc.HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
                            Document doc2 = new Document(Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension);
                            foreach (Section section in doc2.Sections)
                            {
                                foreach (DocumentObject obj in header.ChildObjects)
                                {
                                    section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                                }
                                foreach (DocumentObject obj in footer.ChildObjects)
                                {
                                    section.HeadersFooters.Footer.ChildObjects.Add(obj.Clone());
                                }
                            }



                            doc2.SaveToFile(Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension);
                            #endregion
                            Response.ContentType = mimeType;
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
                            Response.WriteFile(Server.MapPath("~/Report/" + this.jobSample.job_number + "." + extension));
                            Response.Flush();

                            #region "Delete After Download"
                            String deleteFile1 = Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension;
                            String deleteFile2 = Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension;

                            if (File.Exists(deleteFile1))
                            {
                                File.Delete(deleteFile1);
                            }
                            if (File.Exists(deleteFile2))
                            {
                                File.Delete(deleteFile2);
                            }
                            #endregion
                        }
                        break;
                    case StatusEnum.LABMANAGER_CHECKING:
                    case StatusEnum.LABMANAGER_APPROVE:
                    case StatusEnum.LABMANAGER_DISAPPROVE:
                        if (!String.IsNullOrEmpty(this.jobSample.path_word))
                        {
                            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
                        }
                        break;
                    case StatusEnum.ADMIN_CONVERT_PDF:
                        if (!String.IsNullOrEmpty(this.jobSample.path_word))
                        {
                            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
                        }

                        //if (!String.IsNullOrEmpty(this.jobSample.path_word))
                        //{
                        //    Word2Pdf objWorPdf = new Word2Pdf();
                        //    objWorPdf.InputLocation = String.Format("{0}{1}", Configurations.PATH_DRIVE, this.jobSample.path_word);
                        //    objWorPdf.OutputLocation = String.Format("{0}{1}", Configurations.PATH_DRIVE, this.jobSample.path_word).Replace("doc", "pdf");
                        //    try
                        //    {
                        //        objWorPdf.Word2PdfCOnversion();
                        //        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word).Replace("doc", "pdf"));

                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Console.WriteLine();
                        //        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));

                        //    }
                        //}
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }

        }

        protected void lbDownloadPdf_Click(object sender, EventArgs e)
        {


            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);


            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo + " "));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));
            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));

            reportParameters.Add(new ReportParameter("rpt_unit1", ddlUnitMotorOilContamination.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit2", ddlUnitMotorHub.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit3", ddlUnitMotorHubSub.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit4", ddlUnitMotorBase.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit5", ddlUnitMotorBaseSub.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit6", ddlUnitCompound.SelectedItem.Text));



            reportParameters.Add(new ReportParameter("Test", "GCMS Extractable"));
            reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
            reportParameters.Add(new ReportParameter("Remark1", lbRemark1.Text));
            reportParameters.Add(new ReportParameter("Remark2", lbRemark2.Text));
            reportParameters.Add(new ReportParameter("Remark3", lbRemark3.Text));


            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/gcms_seagate_pdf.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);


            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_OIL)).ToList().ToDataTable()));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB)).ToList().ToDataTable()));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_HUB_SUB)).ToList().ToDataTable()));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE)).ToList().ToDataTable()));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.MOTOR_BASE_SUB)).ToList().ToDataTable()));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet7", this.coverpages.Where(x => x.data_type == Convert.ToInt32(SeagateGcmsEnum.COMPOUND)).ToList().ToDataTable()));


            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
            }
            else
            {
                tb_m_component component = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
                if (component != null)
                {
                    lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", component.B, component.A);
                }
            }

        }



        protected void ddlUnitMotorOilContamination_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorOil.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
            gvMotorOil.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorOilContamination.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitMotorBaseSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorBaseSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
            gvMotorBaseSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBaseSub.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitMotorBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorBase.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorBase.SelectedItem.Text);
            gvMotorBase.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorBase.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitMotorHubSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorHubSub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorHubSub.SelectedItem.Text);
            gvMotorHubSub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHubSub.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitMotorHub_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvMotorHub.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitMotorHub.SelectedItem.Text);
            gvMotorHub.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitMotorHub.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }
        protected void ddlUnitCompound_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvCompound.Columns[1].HeaderText = String.Format("Maximum Allowable Amount ({0})", ddlUnitCompound.SelectedItem.Text);
            gvCompound.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnitCompound.SelectedItem.Text);
            ModolPopupExtender.Show();
            GenerrateCoverPage();
        }

    }
}



