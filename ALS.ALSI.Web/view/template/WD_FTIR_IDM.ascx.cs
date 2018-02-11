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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Spire.Doc;

namespace ALS.ALSI.Web.view.template
{
    public partial class WD_FTIR_IDM : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_FTIR));

        #region "Property"

        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }

        public job_sample jobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public List<template_wd_ftir_coverpage> Ftir
        {
            get { return (List<template_wd_ftir_coverpage>)Session[GetType().Name + "Ftir"]; }
            set { Session[GetType().Name + "Ftir"] = value; }
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
            Session.Remove(GetType().Name + "Ftir");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
        }

        List<String> errors = new List<string>();


        private void initialPage()
        {
            tb_m_detail_spec detailSpec = new tb_m_detail_spec();
            detailSpec.specification_id = this.jobSample.specification_id;
            detailSpec.template_id = this.jobSample.template_id;

            tb_m_component comp = new tb_m_component();
            comp.specification_id = this.jobSample.specification_id;
            comp.template_id = this.jobSample.template_id;

            ddlDetailSpec.Items.Clear();
            ddlDetailSpec.DataSource = detailSpec.SelectAll();
            ddlDetailSpec.DataBind();
            ddlDetailSpec.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlComponent.Items.Clear();
            ddlComponent.DataSource = comp.SelectAll();
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            tb_unit unit = new tb_unit();
            ddlUnit.Items.Clear();
            ddlUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("FTIR")).ToList();
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            #region "SAMPLE"
            this.jobSample = new job_sample().SelectByID(this.SampleID);
            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            if (this.jobSample != null)
            {
                lbJobStatus.Text = Constants.GetEnumDescription(status);

                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);

                pRemark.Visible = false;
                pDisapprove.Visible = false;
                pSpecification.Visible = false;
                pStatus.Visible = false;
                pUploadfile.Visible = false;
                pDownload.Visible = false;
                btnSubmit.Visible = false;
                //btnCalculate.Visible = false;
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
                            //btnCalculate.Visible = false;
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
                            //btnCalculate.Visible = true;
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
                            //btnCalculate.Visible = true;
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
                            //btnCalculate.Visible = false;
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
                            //btnCalculate.Visible = false;
                        }
                        break;
                }

                txtDateAnalyzed.Text = (this.jobSample.date_chemist_alalyze != null) ? this.jobSample.date_chemist_alalyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = userRole == RoleEnum.CHEMIST;

                #region "METHOD/PROCEDURE:"
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

                    btnNVRFTIR.Visible = true;
                    btnCoverPage.Visible = true;
                    gvMethodProcedure.Columns[5].Visible = true;
                    gvResult.Columns[5].Visible = true;
                    pLoadFile.Visible = true;
                }
                else
                {
                    btnNVRFTIR.Visible = false;
                    gvMethodProcedure.Columns[5].Visible = false;
                    gvResult.Columns[5].Visible = false;
                    pLoadFile.Visible = false;
                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
                    {
                        btnCoverPage.Visible = true;
                        btnNVRFTIR.Visible = true;
                    }
                }
                #endregion
            }
            #endregion
            #region "WorkSheet"

            this.Ftir = template_wd_ftir_coverpage.FindAllBySampleID(this.SampleID);
            if (this.Ftir != null && this.Ftir.Count > 0)
            {
                ddlComponent.SelectedValue = this.Ftir[0].component_id.Value + "";
                ddlDetailSpec.SelectedValue = this.Ftir[0].detail_spec_id.Value + "";
                ddlUnit.SelectedValue = (this.Ftir[0].ftir_unit == null) ? "0" : this.Ftir[0].ftir_unit.Value + "";

                gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == 1).ToList();
                gvMethodProcedure.DataBind();
                gvResult.DataSource = this.Ftir.Where(x => x.data_type == 2).ToList();
                gvResult.DataBind();

                CalculateCas();

                #region "Unit"
                gvResult.Columns[2].HeaderText = String.Format("Specification Limits ({0})", ddlUnit.SelectedItem.Text);
                gvResult.Columns[3].HeaderText = String.Format("Results ({0})", ddlUnit.SelectedItem.Text);
                #endregion

                cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;
                if (cbCheckBox.Checked)
                {
                    lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "WD");
                }
                else
                {
                    tb_m_detail_spec tmp = new tb_m_detail_spec().SelectByID(this.Ftir[0].detail_spec_id.Value);
                    if (tmp != null)
                    {
                        lbSpecDesc.Text = String.Format("The Specification is based on Western Digital's Doc {0} {1}", tmp.B, tmp.A);
                    }
                }

                txtNVR_FTIR_B14.Text = this.Ftir[0].td_b14;//Volume of solvent used:
                txtNVR_FTIR_B15.Text = this.Ftir[0].td_b15;//Surface area (S):
                txtNVR_FTIR_B16.Text = this.Ftir[0].td_b16;//No. of parts extracted (N):

                #region "NVR"
                txtNVR_B20.Text = this.Ftir[0].nvr_b20;//Blank (B)-Wt.of Empty Pan (µg)
                txtNVR_B21.Text = this.Ftir[0].nvr_b21;//Sample (A)-Wt.of Empty Pan (µg)
                txtNVR_C20.Text = this.Ftir[0].nvr_c20;//Blank (B)-Wt.of Pan + Residue (µg)
                txtNVR_C21.Text = this.Ftir[0].nvr_c21;//Sample (A)-Wt.of Pan + Residue (µg)
                lbC26.Text = this.Ftir[0].nvr_c26;//Calculations:-Wt. of Residue (µg)
                #endregion
                #region "Silicone"
                txtFTIR_B30.Text = this.Ftir[0].ftr_b30;//Peak Ht at 800cm-
                txtFTIR_B31.Text = this.Ftir[0].ftr_b31;//Slope of Calibration Plot
                txtFTIR_B32.Text = this.Ftir[0].ftr_b32;//y-intercept
                txtFTIR_B33.Text = this.Ftir[0].ftr_b33;//Amount of Amide Detected (µg)
                txtFTIR_B35.Text = this.Ftir[0].ftr_b36;//Method Detection Limit, MDL
                lbFTIR_C40.Text = this.Ftir[0].ftr_c40;//Calculations
                txtC41.Text = this.Ftir[0].ftr_c41;//Reported as 
                #endregion
                #region "Amide"
                txtFTIR_B42.Text = this.Ftir[0].ftr_b43; //Peak Ht at cm-1
                txtFTIR_B43.Text = this.Ftir[0].ftr_b44;//Slope of Calibration Plot
                txtFTIR_B44.Text = this.Ftir[0].ftr_b45;//y-intercept
                txtFTIR_B45.Text = this.Ftir[0].ftr_b46; //Amount of Amide Detected (µg)
                txtFTIR_B45.Text = this.Ftir[0].ftr_b48; //Method Detection Limit, MDL
                lbFTIR_C49.Text = this.Ftir[0].ftr_c52;//Calculations
                txtC53.Text = this.Ftir[0].ftr_c53;//Reportas
                #endregion
                #region "Unit"
                lbAmide.Text = this.Ftir[0].amide_unit;
                lbSilicone.Text = this.Ftir[0].silicone_unit;
                #endregion

                CalculateCas();

                this.CommandName = CommandNameEnum.Edit;
                //ShowItem(this.Ftir.item_visible);
            }
            else
            {
                this.Ftir = new List<template_wd_ftir_coverpage>();
                this.CommandName = CommandNameEnum.Add;
            }

            #endregion

            //initial component
            btnNVRFTIR.CssClass = "btn green";
            btnCoverPage.CssClass = "btn blue";
            pCoverpage.Visible = true;
            PWorking.Visible = false;
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

        protected void btnNVRFTIR_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnNVRFTIR.CssClass = "btn green";
                    btnCoverPage.CssClass = "btn blue";
                    pCoverpage.Visible = true;
                    PWorking.Visible = false;
                    pLoadFile.Visible = false;
                    var items = this.Ftir.Where(x => x.data_type == 1).ToList();
                    if (items.Count > 0)
                    {
                        items[0].E = String.Format("{0} {1}", txtNVR_FTIR_B14.Text, lbNVR_FTIR_B14.Text);
                        items[1].E = String.Format("{0} {1}", txtNVR_FTIR_B14.Text, lbNVR_FTIR_B14.Text);
                        items[2].E = String.Format("{0} {1}", txtNVR_FTIR_B14.Text, lbNVR_FTIR_B14.Text);
                        //items[3].E = String.Format("{0} {1}", txtNVR_FTIR_B14.Text, lbNVR_FTIR_B14.Text);
                        //items[4].E = String.Format("{0} {1}", txtNVR_FTIR_B14.Text, lbNVR_FTIR_B14.Text);
                        //items[5].E = String.Format("{0} {1}", txtNVR_FTIR_B14.Text, lbNVR_FTIR_B14.Text);

                        items[0].C = String.Format("{0} {1}", txtNVR_FTIR_B15.Text, lbNVR_FTIR_B15.Text);
                        items[1].C = String.Format("{0} {1}", txtNVR_FTIR_B15.Text, lbNVR_FTIR_B15.Text);
                        items[2].C = String.Format("{0} {1}", txtNVR_FTIR_B15.Text, lbNVR_FTIR_B15.Text);
                        //items[3].C = String.Format("{0} {1}", txtNVR_FTIR_B15.Text, lbNVR_FTIR_B15.Text);
                        //items[4].C = String.Format("{0} {1}", txtNVR_FTIR_B15.Text, lbNVR_FTIR_B15.Text);
                        //items[5].C = String.Format("{0} {1}", txtNVR_FTIR_B15.Text, lbNVR_FTIR_B15.Text);
                    }
                    CalculateCas();
                    break;
                case "NVR-FTIR(Hex)":
                    btnNVRFTIR.CssClass = "btn green";
                    btnCoverPage.CssClass = "btn blue";
                    pCoverpage.Visible = false;
                    PWorking.Visible = true;
                    pLoadFile.Visible = false;
                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                    {
                        pLoadFile.Visible = true;

                    }
                    break;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Boolean isValid = true;
            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    this.jobSample.step1owner = userLogin.id;

                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.jobSample.step2owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    this.jobSample.date_chemist_alalyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                    //#endregion

                    foreach (template_wd_ftir_coverpage item in this.Ftir)
                    {
                        item.sample_id = this.SampleID;
                        item.detail_spec_id = Convert.ToInt32(ddlDetailSpec.SelectedValue);
                        item.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        item.ftir_unit = Convert.ToInt32(ddlUnit.SelectedValue);

                    }
                    template_wd_ftir_coverpage.DeleteBySampleID(this.SampleID);
                    template_wd_ftir_coverpage.InsertList(this.Ftir);
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    foreach (template_wd_ftir_coverpage item in this.Ftir)
                    {
                        item.detail_spec_id = Convert.ToInt32(ddlDetailSpec.SelectedValue);
                        item.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        item.ftir_unit = Convert.ToInt32(ddlUnit.SelectedValue);
                        item.td_b14 = txtNVR_FTIR_B14.Text;//Volume of solvent used:
                        item.td_b15 = txtNVR_FTIR_B15.Text;//Surface area (S):
                        item.td_b16 = txtNVR_FTIR_B16.Text;//No. of parts extracted (N):

                        #region "NVR"
                        item.nvr_b20 = txtNVR_B20.Text;//Blank (B)-Wt.of Empty Pan (µg)
                        item.nvr_b21 = txtNVR_B21.Text;//Sample (A)-Wt.of Empty Pan (µg)
                        item.nvr_c20 = txtNVR_C20.Text;//Blank (B)-Wt.of Pan + Residue (µg)
                        item.nvr_c21 = txtNVR_C21.Text;//Sample (A)-Wt.of Pan + Residue (µg)
                        item.nvr_c26 = lbC26.Text;//Calculations:-Wt. of Residue (µg)
                        #endregion
                        #region "Silicone"
                        item.ftr_b30 = txtFTIR_B30.Text;//Peak Ht at 800cm-
                        item.ftr_b31 = txtFTIR_B31.Text;//Slope of Calibration Plot
                        item.ftr_b32 = txtFTIR_B32.Text;//y-intercept
                        item.ftr_b33 = txtFTIR_B33.Text;//Amount of Amide Detected (µg)
                        item.ftr_b36 = txtFTIR_B35.Text;//Method Detection Limit, MDL
                        item.ftr_c40 = lbFTIR_C40.Text;//Calculations
                        item.ftr_c41 = txtC41.Text;//Reported as 
                        #endregion
                        #region "Amide"
                        item.ftr_b43 = txtFTIR_B42.Text; //Peak Ht at cm-1
                        item.ftr_b44 = txtFTIR_B43.Text;//Slope of Calibration Plot
                        item.ftr_b45 = txtFTIR_B44.Text;//y-intercept
                        item.ftr_b46 = txtFTIR_B45.Text; //Amount of Amide Detected (µg)
                        item.ftr_b48 = txtFTIR_B45.Text; //Method Detection Limit, MDL
                        item.ftr_c52 = lbFTIR_C49.Text;//Calculations
                        item.ftr_c53 = txtC53.Text;//Reportas
                        #endregion

                        item.amide_unit = lbAmide.Text;
                        item.silicone_unit = lbSilicone.Text;
                    }

                    template_wd_ftir_coverpage.UpdateList(this.Ftir);

                    break;
                case StatusEnum.SR_CHEMIST_CHECKING:
                    StatusEnum srChemistApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (srChemistApproveStatus)
                    {
                        case StatusEnum.SR_CHEMIST_APPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                            #region ":: STAMP COMPLETE DATE"

                            this.jobSample.date_srchemist_complate = DateTime.Now;
                            #endregion
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
                    if (btnUpload.HasFile)// && (Path.GetExtension(btnUpload.FileName).Equals(".doc") || Path.GetExtension(btnUpload.FileName).Equals(".docx")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        btnUpload.SaveAs(source_file);
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
            //########
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();
            }
            else
            {
                litErrorMessage.Text = String.Empty;
                this.jobSample.update_date = DateTime.Now;
                this.jobSample.update_by = userLogin.id;
                this.jobSample.Update();
                //Commit
                GeneralManager.Commit();

                //removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculateCas();
            btnSubmit.Enabled = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            removeSession();
            Response.Redirect(this.PreviousPath);
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            string sheetName = string.Empty;

            List<tb_m_gcms_cas> _cas = new List<tb_m_gcms_cas>();
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");

            for (int i = 0; i < FileUpload1.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = FileUpload1.PostedFiles[i];
                try
                {
                    if (_postedFile.ContentLength > 0)
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                        //String source_file_url = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));

                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        _postedFile.SaveAs(source_file);
                        #region "XLS"
                        if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")) || (Path.GetExtension(_postedFile.FileName).Equals(".xlsx")))
                        {
                            using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                            {
                                HSSFWorkbook wb = new HSSFWorkbook(fs);

                                ISheet isheet = wb.GetSheet("NVR_FTIR");
                                if (isheet == null)
                                {

                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["wd.ftir.excel.sheetname.working1"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    #region "Test Data"
                                    txtNVR_FTIR_B14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.B));
                                    txtNVR_FTIR_B15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.B));
                                    txtNVR_FTIR_B16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.B));

                                    lbNVR_FTIR_B14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.C));
                                    lbNVR_FTIR_B15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.C));

                                    #endregion
                                    #region "NVR"
                                    txtNVR_B20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.B));
                                    txtNVR_B21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.B));
                                    txtNVR_C20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.C));
                                    txtNVR_C21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.C));

                                    lbD20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.D));
                                    lbD21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.D));
                                    lbC26.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.C));

                                    //Decimal
                                    txtNVR_B20.Text = String.IsNullOrEmpty(txtNVR_B20.Text) ? "" : Math.Round(Convert.ToDecimal(txtNVR_B20.Text), Convert.ToInt32(txtDecimal01.Text)) + "";
                                    txtNVR_C20.Text = String.IsNullOrEmpty(txtNVR_C20.Text) ? "" : Math.Round(Convert.ToDecimal(txtNVR_C20.Text), Convert.ToInt32(txtDecimal01.Text)) + "";
                                    lbD20.Text = String.IsNullOrEmpty(lbD20.Text) ? "" : Math.Round(Convert.ToDecimal(lbD20.Text), Convert.ToInt32(txtDecimal01.Text)) + "";

                                    txtNVR_B21.Text = String.IsNullOrEmpty(txtNVR_B21.Text) ? "" : Math.Round(Convert.ToDecimal(txtNVR_B21.Text), Convert.ToInt32(txtDecimal02.Text)) + "";
                                    txtNVR_C21.Text = String.IsNullOrEmpty(txtNVR_C21.Text) ? "" : Math.Round(Convert.ToDecimal(txtNVR_C21.Text), Convert.ToInt32(txtDecimal02.Text)) + "";
                                    lbD21.Text = String.IsNullOrEmpty(lbD21.Text) ? "" : Math.Round(Convert.ToDecimal(lbD21.Text), Convert.ToInt32(txtDecimal02.Text)) + "";

                                    lbC26.Text = String.IsNullOrEmpty(lbC26.Text) ? "" : Math.Round(Convert.ToDecimal(lbC26.Text), Convert.ToInt32(txtDecimal08.Text)) + "";

                                    #endregion
                                    #region "FTIR-Silicone"
                                    txtFTIR_B30.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.B));//31-Peak Ht at 800cm-1
                                    txtFTIR_B31.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.B));//32-Slope of Calibration Plot
                                    txtFTIR_B32.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.B));//33-y-intercept
                                    txtFTIR_B33.Text = CustomUtils.GetCellValue(isheet.GetRow(34 - 1).GetCell(ExcelColumn.B));//34-Amount of Silicone Detected (µg)
                                    txtFTIR_B35.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.B));//35-Method Detection Limit, MDL
                                    lbFTIR_C40.Text = CustomUtils.GetCellValue(isheet.GetRow(40 - 1).GetCell(ExcelColumn.C));//Calculations:
                                    txtC41.Text = CustomUtils.GetCellValue(isheet.GetRow(41 - 1).GetCell(ExcelColumn.C));



                                    txtFTIR_B30.Text = String.IsNullOrEmpty(txtFTIR_B30.Text) ? "" : Convert.ToDouble(txtFTIR_B30.Text).ToString("N" + Convert.ToInt32(txtDecimal03.Text));
                                    txtFTIR_B31.Text = String.IsNullOrEmpty(txtFTIR_B31.Text) ? "" : Convert.ToDouble(txtFTIR_B31.Text).ToString("N" + Convert.ToInt32(txtDecimal04.Text));
                                    txtFTIR_B32.Text = String.IsNullOrEmpty(txtFTIR_B32.Text) ? "" : Convert.ToDouble(txtFTIR_B32.Text).ToString("N" + Convert.ToInt32(txtDecimal05.Text));
                                    txtFTIR_B33.Text = String.IsNullOrEmpty(txtFTIR_B33.Text) ? "" : Convert.ToDouble(txtFTIR_B33.Text).ToString("N" + Convert.ToInt32(txtDecimal06.Text));
                                    txtFTIR_B35.Text = String.IsNullOrEmpty(txtFTIR_B35.Text) ? "" : Convert.ToDouble(txtFTIR_B35.Text).ToString("N" + Convert.ToInt32(txtDecimal07.Text));
                                    lbFTIR_C40.Text = String.IsNullOrEmpty(lbFTIR_C40.Text) ? "" : Convert.ToDouble(lbFTIR_C40.Text).ToString("N" + Convert.ToInt32(txtDecimal08.Text));


                                    #endregion
                                    #region "FTIR-Amide"
                                    txtFTIR_B42.Text = CustomUtils.GetCellValue(isheet.GetRow(43 - 1).GetCell(ExcelColumn.B));//43-Peak Ht at 800cm-1
                                    txtFTIR_B43.Text = CustomUtils.GetCellValue(isheet.GetRow(44 - 1).GetCell(ExcelColumn.B));//44-Slope of Calibration Plot
                                    txtFTIR_B44.Text = CustomUtils.GetCellValue(isheet.GetRow(45 - 1).GetCell(ExcelColumn.B));//45-y-intercept
                                    txtFTIR_B45.Text = CustomUtils.GetCellValue(isheet.GetRow(46 - 1).GetCell(ExcelColumn.B));//46-Amount of Silicone Detected (µg)
                                    txtFTIR_B48.Text = CustomUtils.GetCellValue(isheet.GetRow(48 - 1).GetCell(ExcelColumn.B)); //48-Method Detection Limit, MDL
                                    lbFTIR_C49.Text = CustomUtils.GetCellValue(isheet.GetRow(52 - 1).GetCell(ExcelColumn.C)); //Calculations
                                    txtC53.Text = CustomUtils.GetCellValue(isheet.GetRow(53 - 1).GetCell(ExcelColumn.C));


                                    txtFTIR_B42.Text = String.IsNullOrEmpty(txtFTIR_B42.Text) ? "" : Convert.ToDouble(txtFTIR_B42.Text).ToString("N" + Convert.ToInt32(txtDecimal03.Text));
                                    txtFTIR_B43.Text = String.IsNullOrEmpty(txtFTIR_B43.Text) ? "" : Convert.ToDouble(txtFTIR_B43.Text).ToString("N" + Convert.ToInt32(txtDecimal04.Text));
                                    txtFTIR_B44.Text = String.IsNullOrEmpty(txtFTIR_B44.Text) ? "" : Convert.ToDouble(txtFTIR_B44.Text).ToString("N" + Convert.ToInt32(txtDecimal05.Text));
                                    txtFTIR_B45.Text = String.IsNullOrEmpty(txtFTIR_B45.Text) ? "" : Convert.ToDouble(txtFTIR_B45.Text).ToString("N" + Convert.ToInt32(txtDecimal06.Text));
                                    txtFTIR_B48.Text = String.IsNullOrEmpty(txtFTIR_B48.Text) ? "" : Convert.ToDouble(txtFTIR_B48.Text).ToString("N" + Convert.ToInt32(txtDecimal07.Text));
                                    lbFTIR_C49.Text = String.IsNullOrEmpty(lbFTIR_C49.Text) ? "" : Convert.ToDouble(lbFTIR_C49.Text).ToString("N" + Convert.ToInt32(txtDecimal08.Text));


                                    #endregion




                                    lbAmide.Text = CustomUtils.GetCellValue(isheet.GetRow(36 - 1).GetCell(ExcelColumn.C));
                                    lbSilicone.Text = CustomUtils.GetCellValue(isheet.GetRow(48 - 1).GetCell(ExcelColumn.C));

                                }
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
                CalculateCas();
            }
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {


            try
            {
                DataTable dt = Extenders.ObjectToDataTable(this.Ftir[0]);
                ReportHeader reportHeader = new ReportHeader();
                reportHeader = reportHeader.getReportHeder(this.jobSample);

                List<template_wd_ftir_coverpage> ds = this.Ftir.Where(x => x.data_type == 2 && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();

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

                reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));

                reportParameters.Add(new ReportParameter("Test", "FTIR"));
                reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
                reportParameters.Add(new ReportParameter("Remarks", String.Format("Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory.The instrument detection limit for silicone oil is  {0} {1}", lbA31.Text, lbB31.Text)));
                reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.jobSample.singapore_ref_no) ? String.Empty : this.jobSample.singapore_ref_no)));

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ftir_wd.rdlc");
                viewer.LocalReport.SetParameters(reportParameters);


                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dt)); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds.ToList().ToDataTable()));

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

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }

            //char[] item = this.Ftir.item_visible.ToCharArray();

            //DataTable dtHeader = new DataTable("MethodProcedure");

            //// Define all the columns once.
            //DataColumn[] cols ={ new DataColumn("ProcedureNo",typeof(String)),
            //                      new DataColumn("NumOfPiecesUsedForExtraction",typeof(String)),
            //                      new DataColumn("ExtractionMedium",typeof(String)),
            //                      new DataColumn("ExtractionVolume",typeof(String)),
            //                  };
            //dtHeader.Columns.AddRange(cols);
            //DataRow row = dtHeader.NewRow();
            //row["ProcedureNo"] = item[0] == '1' ? txtB21.Text : item[1] == '1' ? txtB22.Text : txtB23.Text;
            //row["NumOfPiecesUsedForExtraction"] = item[0] == '1' ? txtC21.Text : item[1] == '1' ? txtC22.Text : txtC23.Text;
            //row["ExtractionMedium"] = item[0] == '1' ? txtD21_1.Text : item[1] == '1' ? txtD22.Text : txtD23.Text;
            //row["ExtractionVolume"] = item[0] == '1' ? txtE21.Text : item[1] == '1' ? txtE22.Text : txtE23.Text;
            //dtHeader.Rows.Add(row);
            //ReportHeader reportHeader = new ReportHeader();
            //reportHeader = reportHeader.getReportHeder(this.jobSample);

            //List<ReportData> reportList = new List<ReportData>();
            ////Create ReportData NVR
            //ReportData tmp = new ReportData
            //{

            //    A = "Non-Volatile Residue (NVR)",
            //    B = lbC28.Text,
            //    C = lbD28.Text,
            //    D = lbE28.Text
            //};
            //if (item[3] == '1')
            //{
            //    reportList.Add(tmp);
            //}
            //tmp = new ReportData
            //{

            //    A = "Silicone at Wave No:2962, 1261, 1092, 1022 & 800cm-1",
            //    B = lbC30.Text,
            //    C = lbD30.Text,
            //    D = lbE30.Text
            //};
            //if (item[4] == '1')
            //{
            //    reportList.Add(tmp);
            //}


            //ReportParameterCollection reportParameters = new ReportParameterCollection();

            //reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            //reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            //reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date + ""));
            //reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            //reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2)); reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve + ""));
            //reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze + ""));
            //reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze + ""));
            //reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            //reportParameters.Add(new ReportParameter("Test", item[0] == '1' ? "NVR/FTIR" : item[1] == '1' ? "NVR" : "FTIR"));
            //reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on Western Digital's Doc {0} for {1}", lbDocRev.Text, lbDesc.Text)));
            //reportParameters.Add(new ReportParameter("Remarks", String.Format("Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory.The instrument detection limit for silicone oil is  {0} {1}", lbA31.Text, lbB31.Text)));

            //// Variables
            //Warning[] warnings;
            //string[] streamIds;
            //string mimeType = string.Empty;
            //string encoding = string.Empty;
            //string extension = string.Empty;


            //// Setup the report viewer object and get the array of bytes
            //ReportViewer viewer = new ReportViewer();
            //viewer.ProcessingMode = ProcessingMode.Local;
            //viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ftir_wd.rdlc");
            //viewer.LocalReport.SetParameters(reportParameters);
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtHeader)); // Add datasource here
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", reportList.ToDataTable())); // Add datasource here



            //string download = String.Empty;

            //StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            //switch (status)
            //{
            //    case StatusEnum.ADMIN_CONVERT_WORD:
            //        if (!String.IsNullOrEmpty(this.jobSample.path_word))
            //        {
            //            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
            //        }
            //        else
            //        {
            //            byte[] bytes = viewer.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            //            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            //            Response.Buffer = true;
            //            Response.Clear();
            //            Response.ContentType = mimeType;
            //            Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
            //            Response.BinaryWrite(bytes); // create the file
            //            Response.Flush(); // send it to the client to download
            //        }
            //        break;
            //    case StatusEnum.LABMANAGER_CHECKING:
            //    case StatusEnum.LABMANAGER_APPROVE:
            //    case StatusEnum.LABMANAGER_DISAPPROVE:
            //        if (!String.IsNullOrEmpty(this.jobSample.path_word))
            //        {
            //            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
            //        }
            //        break;
            //    case StatusEnum.ADMIN_CONVERT_PDF:
            //        if (!String.IsNullOrEmpty(this.jobSample.path_word))
            //        {
            //            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
            //        }

            //        //if (!String.IsNullOrEmpty(this.jobSample.path_word))
            //        //{
            //        //    Word2Pdf objWorPdf = new Word2Pdf();
            //        //    objWorPdf.InputLocation = String.Format("{0}{1}", Configurations.PATH_DRIVE, this.jobSample.path_word);
            //        //    objWorPdf.OutputLocation = String.Format("{0}{1}", Configurations.PATH_DRIVE, this.jobSample.path_word).Replace("doc", "pdf");
            //        //    try
            //        //    {
            //        //        objWorPdf.Word2PdfCOnversion();
            //        //        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word).Replace("doc", "pdf"));

            //        //    }
            //        //    catch (Exception ex)
            //        //    {
            //        //        Console.WriteLine();
            //        //        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));

            //        //    }
            //        //}
            //        break;
            //}




        }

        protected void lbDownloadPdf_Click(object sender, EventArgs e)
        {


            DataTable dt = Extenders.ObjectToDataTable(this.Ftir[0]);
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);

            List<template_wd_ftir_coverpage> ds = this.Ftir.Where(x => x.data_type == 2 && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();

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

            reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));

            reportParameters.Add(new ReportParameter("Test", "FTIR"));
            reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
            reportParameters.Add(new ReportParameter("Remarks", String.Format("Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory.The instrument detection limit for silicone oil is  {0} {1}", lbA31.Text, lbB31.Text)));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ftir_wd_pdf.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);


            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds.ToList().ToDataTable()));


            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download


        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue.ToString(), true);
            switch (status)
            {
                case StatusEnum.SR_CHEMIST_DISAPPROVE:
                    pRemark.Visible = true;
                    pDisapprove.Visible = false;
                    break;
                case StatusEnum.LABMANAGER_DISAPPROVE:
                    pRemark.Visible = true;
                    pDisapprove.Visible = true;
                    break;
                default:
                    pRemark.Visible = false;
                    pDisapprove.Visible = false;
                    break;
            }

        }

        protected void ddlDetailSpec_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_detail_spec detailSpec = new tb_m_detail_spec().SelectByID(int.Parse(ddlDetailSpec.SelectedValue));
            if (detailSpec != null)
            {
                lbSpecDesc.Text = String.Format("The Specification is based on Western Digital's Doc {0} {1}", detailSpec.B, detailSpec.A);

                if (this.Ftir.Count > 0)
                {
                    foreach (template_wd_ftir_coverpage item in this.Ftir.Where(x => x.data_type == 2).ToList())
                    {
                        this.Ftir.Remove(item);
                    }
                }
                #region "Result"
                template_wd_ftir_coverpage tmp = new template_wd_ftir_coverpage();
                tmp.ID = 4;
                tmp.A = "NVR";
                tmp.B = "Non-Volatile Residue (NVR) in n-Hexane";
                tmp.C = detailSpec.F;
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = 1;
                tmp.data_type = 2;
                this.Ftir.Add(tmp);
                tmp = new template_wd_ftir_coverpage();
                tmp.ID = 5;
                tmp.A = "NVR";
                tmp.B = "Non-Volatile Residue (NVR) in IPA";
                tmp.C = detailSpec.E;
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = 1;
                tmp.data_type = 2;
                this.Ftir.Add(tmp);
                tmp = new template_wd_ftir_coverpage();
                tmp.ID = 6;
                tmp.A = "FTIR";
                tmp.B = "Silicone at Wave No:2962, 1261, 1092, 1022 & 800cm - 1";
                tmp.C = detailSpec.G;
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = 1;
                tmp.data_type = 2;
                this.Ftir.Add(tmp);
                tmp = new template_wd_ftir_coverpage();
                tmp.ID = 7;
                tmp.A = "FTIR";
                tmp.B = "Amide";
                tmp.C = detailSpec.I;
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = 1;
                tmp.data_type = 2;
                this.Ftir.Add(tmp);
                tmp = new template_wd_ftir_coverpage();
                tmp.ID = 8;
                tmp.A = "FTIR";
                tmp.B = "DOP";
                tmp.C = detailSpec.H;
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = 1;
                tmp.data_type = 2;
                this.Ftir.Add(tmp);

                #endregion

                gvResult.DataSource = this.Ftir.Where(x => x.data_type == 2).ToList();
                gvResult.DataBind();


                //this.Result = listResult;
                //gvResult.DataSource = this.Result;
                //gvResult.DataBind();
                //lbC28.Text = detailSpec.E.Equals("NA") ? "NA" : String.Format("<{0}", detailSpec.E);//NVR
                //lbC30.Text = detailSpec.F.Equals("NA") ? "NA" : String.Format("<{0}", detailSpec.F); ;//FTIR

                //lbE27.Text = detailSpec.D;
                //lbD27.Text = detailSpec.D;
            }
        }

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_component component = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (component != null)
            {
                if (this.Ftir.Count > 0)
                {
                    foreach (template_wd_ftir_coverpage item in this.Ftir.Where(x => x.data_type == 1).ToList())
                    {
                        this.Ftir.Remove(item);
                    }
                }
                #region "Procedure"
                template_wd_ftir_coverpage tmp = new template_wd_ftir_coverpage();
                tmp.ID = 1;
                tmp.A = "NVR/FTIR";
                tmp.B = String.Format("2092-772141 Rev.AD,{0}", component.B);//Procedure No
                tmp.C = component.G;//"Number of pieces used for extraction"
                tmp.D = component.J;//"Extraction Medium"
                tmp.E = component.M;//"Extraction Volume"
                tmp.row_type = 1;
                tmp.data_type = 1;
                this.Ftir.Add(tmp);
                tmp = new template_wd_ftir_coverpage();
                tmp.ID = 2;
                tmp.A = "NVR";
                tmp.B = String.Format("2092-772141 Rev.AD,{0}", component.B);
                tmp.C = component.E;
                tmp.D = component.H;
                tmp.E = component.K;
                tmp.row_type = 1;
                tmp.data_type = 1;
                this.Ftir.Add(tmp);
                tmp = new template_wd_ftir_coverpage();
                tmp.ID = 3;
                tmp.A = "FTIR";
                tmp.B = String.Format("2092-772141 Rev.AD,{0}", component.B);
                tmp.C = component.F;
                tmp.D = component.I;
                tmp.E = component.L;
                tmp.row_type = 1;
                tmp.data_type = 1;
                this.Ftir.Add(tmp);
                #endregion


                gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == 1).ToList();
                gvMethodProcedure.DataBind();
            }
        }

        #region "Custom method"

        private void downloadWord()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";
            string strFileName = string.Format("{0}_{1}.doc", this.jobSample.job_number.Replace("-", "_"), DateTime.Now.ToString("yyyyMMddhhmmss"));

            HttpContext.Current.Response.ContentType = "application/vnd.ms-word";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);

            StringWriter sw = new StringWriter();
            HtmlTextWriter h = new HtmlTextWriter(sw);
            invDiv.RenderControl(h);
            string strHTMLContent = sw.GetStringBuilder().ToString();
            String html = "<html><header><style>body {max-width: 800px;margin:initial;font-family: \'Arial Unicode MS\';font-size: 10px;}table {border-collapse: collapse;}th {background: #666;color: #fff;border: 1px solid #999;padding: 0.5rem;text-align: center;}td { border: 1px solid #999;padding: 0.5rem;text-align: left;}h6 {font-weight:initial;}</style></header><body>" + strHTMLContent + "</body></html>";


            HttpContext.Current.Response.Write(html);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
        }


        private void CalculateCas()
        {


            this.Ftir[3].D = lbC26.Text;
            this.Ftir[4].D = lbC26.Text;

            this.Ftir[5].D = txtC41.Text;
            this.Ftir[6].D = txtC53.Text;
            this.Ftir[7].D = "Not Detected";
            ////

            this.Ftir[3].E = String.IsNullOrEmpty(this.Ftir[3].D) ? "" : this.Ftir[3].D.Equals("NA") || this.Ftir[3].C.Equals("NA") ? "NA" : ((Convert.ToDouble(this.Ftir[3].D) < Convert.ToDouble(this.Ftir[3].C.Replace("<", "").Trim()) || this.Ftir[3].D.Equals("Not Detected")) ? "PASS" : "FAIL");
            this.Ftir[4].E = String.IsNullOrEmpty(this.Ftir[4].D) ? "" : this.Ftir[4].D.Equals("NA") || this.Ftir[4].C.Equals("NA") ? "NA" : ((Convert.ToDouble(this.Ftir[4].D) < Convert.ToDouble(this.Ftir[4].C.Replace("<", "").Trim()) || this.Ftir[4].D.Equals("Not Detected")) ? "PASS" : "FAIL");

            //if (!this.Ftir[5].C.Equals("Not Detected"))
            //{
            this.Ftir[5].E = String.IsNullOrEmpty(this.Ftir[5].D) ? "" : this.Ftir[5].D.Equals("NA") || this.Ftir[5].C.Equals("NA") ? "NA" : (this.Ftir[5].D.Equals("< MDL")) ? "PASS" : this.Ftir[5].D.ToUpper().Equals("Not Detected".ToUpper()) || this.Ftir[5].C.ToUpper().Equals("Not Detected".ToUpper()) || (Convert.ToDouble(this.Ftir[5].D) < Convert.ToDouble(this.Ftir[5].C.Replace("<", "").Trim())) ? "PASS" : "FAIL";
            //}
            //if (!this.Ftir[6].C.Equals("Not Detected"))
            //{
            this.Ftir[6].E = String.IsNullOrEmpty(this.Ftir[6].D) ? "" : this.Ftir[6].D.Equals("Detected")? "FAIL": this.Ftir[6].D.Equals("NA") || this.Ftir[6].C.Equals("NA") ? "NA" : (this.Ftir[6].D.Equals("< MDL")) ? "PASS" : this.Ftir[6].D.Equals("Not Detected") || this.Ftir[6].C.Equals("Not Detected") || (Convert.ToDouble(this.Ftir[6].D) < Convert.ToDouble(this.Ftir[6].C.Replace("<", "").Trim())) ? "PASS" : "FAIL";
            //}
            //if (!this.Ftir[7].C.Equals("Not Detected"))
            //{
            this.Ftir[7].E = String.IsNullOrEmpty(this.Ftir[7].D) ? "" : this.Ftir[7].D.Equals("NA") || this.Ftir[7].C.Equals("NA") ? "NA" : (this.Ftir[7].D.ToUpper().Equals("Not Detected".ToUpper())) ? "PASS" : this.Ftir[7].D.ToUpper().Equals("Not Detected".ToUpper()) || this.Ftir[7].C.ToUpper().Equals("Not Detected".ToUpper()) || (Convert.ToDouble(this.Ftir[7].D) < Convert.ToDouble(this.Ftir[7].C.Replace("<", "").Trim())) ? "PASS" : "FAIL";
            //}


            gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == 1).ToList();
            gvMethodProcedure.DataBind();

            gvResult.DataSource = this.Ftir.Where(x => x.data_type == 2).ToList();
            gvResult.DataBind();


            lbA31.Text = !String.IsNullOrEmpty(txtFTIR_B35.Text) ? txtFTIR_B35.Text : txtFTIR_B48.Text;
            lbB31.Text = !String.IsNullOrEmpty(lbAmide.Text) ? lbAmide.Text : lbSilicone.Text;


            btnSubmit.Enabled = true;
        }

        #endregion

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }


        protected void gvMethodProcedure_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvMethodProcedure.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMethodProcedure.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvResult.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvResult.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvProcedure_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Edit") && !e.CommandName.Equals("Cancel") && !e.CommandName.Equals("Update"))
            {
                RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_wd_ftir_coverpage gcms = this.Ftir.Find(x => x.ID == PKID && x.data_type == 1);
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

                        gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == 1);
                        gvMethodProcedure.DataBind();
                    }
                }
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_wd_ftir_coverpage gcms = this.Ftir.Find(x => x.ID == PKID && x.data_type == 2);
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

                    gvResult.DataSource = this.Ftir.Where(x => x.data_type == 2);
                    gvResult.DataBind();
                }
            }
        }
        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "WD");
            }
            else
            {
                tb_m_detail_spec detailSpec = new tb_m_detail_spec().SelectByID(int.Parse(ddlDetailSpec.SelectedValue));
                if (detailSpec != null)
                {
                    lbSpecDesc.Text = String.Format("The Specification is based on Western Digital's Doc {0} {1}", detailSpec.B, detailSpec.A);
                }
            }

        }




        #region "method/procedure"
        protected void gvMethodProcedure_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = e.NewEditIndex;
            gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == 1);
            gvMethodProcedure.DataBind();
        }

        protected void gvMethodProcedure_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = -1;
            gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == 1);
            gvMethodProcedure.DataBind();
        }

        protected void gvMethodProcedure_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int _id = Convert.ToInt32(gvMethodProcedure.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox _txtAnalysis = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtAnalysis");
            TextBox _txtProcedureNo = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtProcedureNo");
            TextBox _txtNumberOfPiecesUsedForExtraction = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtNumberOfPiecesUsedForExtraction");
            TextBox _txtExtractionMedium = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtExtractionMedium");
            TextBox _txtExtractionVolume = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtExtractionVolume");

            if (_txtExtractionVolume != null)
            {

                template_wd_ftir_coverpage _tmp = this.Ftir.Find(x => x.ID == _id);
                if (_tmp != null)
                {
                    _tmp.A = _txtAnalysis.Text;
                    _tmp.B = _txtProcedureNo.Text;
                    _tmp.C = _txtNumberOfPiecesUsedForExtraction.Text;
                    _tmp.D = _txtExtractionMedium.Text;
                    _tmp.E = _txtExtractionVolume.Text;
                }
            }

            gvMethodProcedure.EditIndex = -1;
            gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == 1);
            gvMethodProcedure.DataBind();
        }
        #endregion


        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvResult.Columns[2].HeaderText = String.Format("Specification Limits ({0})", ddlUnit.SelectedItem.Text);
            gvResult.Columns[3].HeaderText = String.Format("Results ({0})", ddlUnit.SelectedItem.Text);
            ModolPopupExtender.Show();

            gvResult.DataSource = this.Ftir.Where(x => x.data_type == 2).ToList();
            gvResult.DataBind();
        }

    }
}