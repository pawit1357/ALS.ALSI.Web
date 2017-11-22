﻿using ALS.ALSI.Biz;
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
    public partial class Seagate_FTIR_Damper : System.Web.UI.UserControl
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Seagate_FTIR));

        #region "Property"
        public String[] MethodType = { "NVR/FTIR", "NVR", "NVR", "FTIR", "FTIR", "FTIR" };

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

        public List<template_seagate_ftir_coverpage> Ftir
        {
            get { return (List<template_seagate_ftir_coverpage>)Session[GetType().Name + "Ftir"]; }
            set { Session[GetType().Name + "Ftir"] = value; }
        }

        public List<ReportData> ReportData
        {
            get { return (List<ReportData>)Session[GetType().Name + "ReportData_Ftir_Seagate"]; }
            set { Session[GetType().Name + "ReportData_Ftir_Seagate"] = value; }
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

        private void initialPage()
        {

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = new tb_m_specification().SelectBySpecificationID(this.jobSample.specification_id, this.jobSample.template_id);
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

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

                #region "VISIBLE RESULT DATA"


                if (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                {
                    #region ":: STAMP ANALYZED DATE ::"
                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                    {
                        if (this.jobSample.date_chemist_alalyze == null)
                        {
                            this.jobSample.date_chemist_alalyze = DateTime.Now;
                            this.jobSample.Update();
                        }
                    }
                    #endregion
                    gvMethodProcedure.Columns[5].Visible = true;
                    gvResultNvr.Columns[3].Visible = true;
                    gvResultFtir.Columns[3].Visible = true;
                    btnWorkingFTIR.Visible = true;
                    btnWorkingNVR.Visible = true;

                }
                else
                {
                    gvMethodProcedure.Columns[5].Visible = false;
                    gvResultNvr.Columns[3].Visible = false;
                    gvResultFtir.Columns[3].Visible = false;
                    btnWorkingFTIR.Visible = false;
                    btnWorkingNVR.Visible = false;

                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
                    {
                        btnWorkingFTIR.Visible = true;
                        btnWorkingNVR.Visible = true;
                    }

                }
                #endregion
            }
            #endregion

            #region "WorkSheet"
            this.Ftir = template_seagate_ftir_coverpage.FindAllBySampleID(this.SampleID);
            if (this.Ftir != null && this.Ftir.Count > 0)
            {
                ddlSpecification.SelectedValue = this.Ftir[0].specification_id.Value.ToString();
                ddlUnit.SelectedValue = this.Ftir[0].selected_unit_ftir.ToString();
                txtWB13.Text = this.Ftir[0].w_b13;
                txtWB14.Text = this.Ftir[0].w_b14;
                txtWB15.Text = this.Ftir[0].w_b15;
                txtWD13.Text = this.Ftir[0].w_d13;
                txtWD14.Text = this.Ftir[0].w_d14;
                txtWD15.Text = this.Ftir[0].w_d15;


                cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;
                if (cbCheckBox.Checked)
                {
                    lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
                }
                else
                {
                    tb_m_specification mSpec = new tb_m_specification();
                    mSpec = mSpec.SelectByID(this.Ftir[0].specification_id.Value);
                    if (mSpec != null)
                    {
                        lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", mSpec.C, mSpec.B);
                    }
                }

                gvMethodProcedure.DataSource = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                gvMethodProcedure.DataBind();
                gvResultNvr.DataSource = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList();
                gvResultNvr.DataBind();
                gvResultFtir.DataSource = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
                gvResultFtir.DataBind();
                gvWftir.DataSource = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_RAW_DATA)).ToList();
                gvWftir.DataBind();
                CalculateCas();
                #region "Unit"
                gvResultNvr.Columns[1].HeaderText = String.Format("Specification Limits ({0})", ddlUnit.SelectedItem.Text);
                gvResultNvr.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnit.SelectedItem.Text);
                gvResultFtir.Columns[1].HeaderText = String.Format("Specification Limits ({0})", ddlUnit.SelectedItem.Text);
                gvResultFtir.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnit.SelectedItem.Text);
                #endregion
            }
            else
            {


                #region "Procedure"
                template_seagate_ftir_coverpage tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "FTIR (Adhesive side)";
                tmp.B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M";
                tmp.C = "16 pieces";
                tmp.D = "n - hexane (HPLC Grade)";
                tmp.E = "10 mL";
                tmp.row_type = 1;
                tmp.data_type = 1;
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "FTIR (Non - Adhesive side)";
                tmp.B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M";
                tmp.C = "16 pieces";
                tmp.D = "n - hexane (HPLC Grade)";
                tmp.E = "10 mL";
                tmp.row_type = 1;
                tmp.data_type = 1;
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "FTIR (Release Liner, ultra - low Silicone, facing adhesive side)";
                tmp.B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M";
                tmp.C = "20 cm²";
                tmp.D = "n - hexane (HPLC Grade)";
                tmp.E = "10 mL";
                tmp.row_type = 1;
                tmp.data_type = 1;
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "FTIR (Release Liner, ultra - low Silicone, back, not facing adhesive side)";
                tmp.B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M";
                tmp.C = "20 cm²";
                tmp.D = "n - hexane (HPLC Grade)";
                tmp.E = "10 mL";
                tmp.row_type = 1;
                tmp.data_type = 1;
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "FTIR";
                tmp.B = "20800032-001 Rev. B,20800014 - 001 Rev.G";
                tmp.C = "";
                tmp.D = "n - hexane (HPLC Grade)";
                tmp.E = "10 mL";
                tmp.row_type = 1;
                tmp.data_type = 1;
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "FTIR";
                tmp.B = "20800032-001 Rev. B,20800014 - 001 Rev.G";
                tmp.C = "";
                tmp.D = "n - hexane (HPLC Grade)";
                tmp.E = "20 mL";
                tmp.row_type = 1;
                tmp.data_type = 1;
                this.Ftir.Add(tmp);
                #endregion
                gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                gvMethodProcedure.DataBind();

                //fix unit unit.Equals("ng/cm2")
                ddlUnit.SelectedValue = "25";
                gvResultNvr.Columns[1].HeaderText = String.Format("Specification Limits ({0})", "ng/cm2");
                gvResultNvr.Columns[2].HeaderText = String.Format("Results,({0})", "ng/cm2");

            }
            #endregion

            //initial component
            btnCoverPage.CssClass = "btn green";
            btnWorkingFTIR.CssClass = "btn blue";
            btnWorkingNVR.CssClass = "btn blue";

            pCoverPage.Visible = true;
            PWorking.Visible = false;
            PNvr.Visible = false;
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

        protected void btnWorkingFTIR_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.ID)
            {
                case "btnCoverPage":
                    btnCoverPage.CssClass = "btn green";
                    btnWorkingFTIR.CssClass = "btn blue";
                    btnWorkingNVR.CssClass = "btn blue";
                    pCoverPage.Visible = true;
                    PWorking.Visible = false;
                    PNvr.Visible = false;
                    pLoadFile.Visible = false;

                    //part value to cover page method/procedure
                    var items = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                    if (items.Count > 0)
                    {
                        items[0].C = String.Format("{0} {1}", txtWB13.Text, lbWC13Unit.Text);
                        items[1].C = String.Format("{0} {1}", txtWB13.Text, lbWC13Unit.Text);
                        items[2].C = String.Format("{0} {1}", txtWB13.Text, lbWC13Unit.Text);
                        items[3].C = String.Format("{0} {1}", txtWB13.Text, lbWC13Unit.Text);
                        items[4].C = String.Format("{0} {1}", txtWB13.Text, lbWC13Unit.Text);
                        items[5].C = String.Format("{0} {1}", txtWB13.Text, lbWC13Unit.Text);

                        items[0].E = String.Format("{0} {1}", txtWB15.Text, lbWC15Unit.Text);
                        items[1].E = String.Format("{0} {1}", txtWB15.Text, lbWC15Unit.Text);
                        items[2].E = String.Format("{0} {1}", txtWB15.Text, lbWC15Unit.Text);
                        items[3].E = String.Format("{0} {1}", txtWB15.Text, lbWC15Unit.Text);
                        items[4].E = String.Format("{0} {1}", txtWB15.Text, lbWC15Unit.Text);
                        items[5].E = String.Format("{0} {1}", txtWB15.Text, lbWC15Unit.Text);


                    }

                    CalculateCas();

                    break;
                case "btnWorkingFTIR":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkingFTIR.CssClass = "btn green";
                    btnWorkingNVR.CssClass = "btn blue";
                    pCoverPage.Visible = false;
                    PWorking.Visible = true;
                    PNvr.Visible = false;
                    pLoadFile.Visible = false;

                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                    {
                        pLoadFile.Visible = true;
                    }
                    break;
                case "btnWorkingNVR":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkingFTIR.CssClass = "btn blue";
                    btnWorkingNVR.CssClass = "btn green";
                    pCoverPage.Visible = false;
                    PWorking.Visible = false;
                    PNvr.Visible = true;
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

                    //this.Ftir.Delete();

                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.jobSample.step2owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";

                    foreach (template_seagate_ftir_coverpage item in this.Ftir)
                    {
                        item.sample_id = this.SampleID;
                        item.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        item.selected_unit_ftir = Convert.ToInt32(ddlUnit.SelectedValue);
                    }
                    template_seagate_ftir_coverpage.DeleteBySampleID(this.SampleID);
                    template_seagate_ftir_coverpage.InsertList(this.Ftir);

                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    //#endregion

                    foreach (template_seagate_ftir_coverpage item in this.Ftir)
                    {
                        item.sample_id = this.SampleID;
                        item.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        item.selected_unit_ftir = Convert.ToInt32(ddlUnit.SelectedValue);
                        item.w_b13 = txtWB13.Text;
                        item.w_b14 = txtWB14.Text;
                        item.w_b15 = txtWB15.Text;
                        item.w_d13 = txtWD13.Text;
                        item.w_d14 = txtWD14.Text;
                        item.w_d15 = txtWD15.Text;
                    }
                    //template_seagate_ftir_coverpage.UpdateList(this.Ftir);
                    template_seagate_ftir_coverpage.DeleteBySampleID(this.SampleID);
                    template_seagate_ftir_coverpage.InsertList(this.Ftir);
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
                    //if (btnUpload.HasFile && (Path.GetExtension(btnUpload.FileName).Equals(".pdf")))
                    //{
                    //    string yyyy = DateTime.Now.ToString("yyyy");
                    //    string MM = DateTime.Now.ToString("MM");
                    //    string dd = DateTime.Now.ToString("dd");

                    //    String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));
                    //    String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));


                    //    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                    //    {
                    //        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                    //    }
                    //    btnUpload.SaveAs(source_file);
                    //    this.jobSample.path_pdf = source_file_url;
                    //    this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                    //    //lbMessage.Text = string.Empty;
                    //}
                    //else
                    //{
                    //    errors.Add("Invalid File. Please upload a File with extension .pdf");
                    //    //lbMessage.Attributes["class"] = "alert alert-error";
                    //    //isValid = false;
                    //}
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
                this.jobSample.Update();
                //Commit
                GeneralManager.Commit();

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

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.Ftir.Count > 0)
            {
                foreach (template_seagate_ftir_coverpage _item in this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList())
                {
                    this.Ftir.Remove(_item);
                }
                foreach (template_seagate_ftir_coverpage _item in this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList())
                {
                    this.Ftir.Remove(_item);
                }
            }

            tb_m_specification item = new tb_m_specification();
            List<tb_m_specification> datas = item.SelectBySpecificationID(this.jobSample.specification_id, this.jobSample.template_id);
            item = datas.Where(x => x.ID == Convert.ToInt32(ddlSpecification.SelectedValue)).FirstOrDefault();
            if (item != null)
            {
                lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", item.C, item.B);

                /*
//Damper, Adhesive side   
//Damper, non - Adhesive side 
//Release Liner, Silicone 

// Liner, non - Silicone 
//Release Liner, ultra-low Silicone (facing adhesive side)
//Release Liner, ultra-low Silicone (back, not facing adhesive side)
	
//Damper, Adhesive side	
//Damper, non-Adhesive side	
//Release Liner, Silicone	
//Release Liner, non-Silicone	
//Release Liner, ultra-low Silicone (facing adhesive side)
//Release Liner, ultra-low Silicone (back, not facing adhesive side)

    */

                #region "NVR"
                template_seagate_ftir_coverpage tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Damper, Adhesive side";
                tmp.B = item.E;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Damper, non-Adhesive side";
                tmp.B = item.F;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Release Liner, Silicone";
                tmp.B = item.G;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Release Liner, non-Silicone";
                tmp.B = item.H;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Release Liner, ultra-low Silicone (facing adhesive side)";
                tmp.B = item.I;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Release Liner, ultra-low Silicone (back, not facing adhesive side)";
                tmp.B = item.J;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC);
                this.Ftir.Add(tmp);
                #endregion
                #region "FTIR"
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Damper, Adhesive side";
                tmp.B = item.L;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Damper, non-Adhesive side";
                tmp.B = item.M;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Release Liner, Silicone";
                tmp.B = item.N;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Release Liner, non-Silicone";
                tmp.B = item.O;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Release Liner, ultra-low Silicone (facing adhesive side)";
                tmp.B = item.P;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC);
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage();
                tmp.ID = this.Ftir.Count + 1;
                tmp.A = "Release Liner, ultra-low Silicone (back, not facing adhesive side)";
                tmp.B = item.Q;
                tmp.C = "";
                tmp.D = "";
                tmp.E = "";
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC);
                this.Ftir.Add(tmp);
                #endregion

                gvResultNvr.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList();
                gvResultNvr.DataBind();
                gvResultFtir.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
                gvResultFtir.DataBind();

            }
        }

        #region "Custom method"

        private void CalculateCas()
        {

            List<template_seagate_ftir_coverpage> ftirList = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_RAW_DATA)).ToList();
            if (ftirList.Count > 0)
            {
                String unit = ddlUnit.SelectedItem.Text;// Convert.ToInt32(ddlUnit.SelectedValue);

                List<template_seagate_ftir_coverpage> ftir2 = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();

                if (ftir2.Count > 0)
                {
                    template_seagate_ftir_coverpage tmp = ftir2.Where(x => x.A.Equals("Damper, Adhesive side")).FirstOrDefault();
                    if (tmp != null)
                    {
                        tmp.C = (unit.Equals("ng/cm2")) ? ftirList[7].B : ftirList[8].B;//Silicone
                    }
                    tmp = ftir2.Where(x => x.A.Equals("Damper, non-Adhesive side")).FirstOrDefault();
                    if (tmp != null)
                    {
                        tmp.C = (unit.Equals("ng/cm2")) ? ftirList[7].C : ftirList[8].B;//Silicone
                    }
                    tmp = ftir2.Where(x => x.A.Equals("Release Liner, ultra-low Silicone (facing adhesive side)")).FirstOrDefault();
                    if (tmp != null)
                    {
                        tmp.C = (unit.Equals("ng/cm2")) ? ftirList[7].D : ftirList[8].B;//Silicone
                    }
                    tmp = ftir2.Where(x => x.A.Equals("Release Liner, ultra-low Silicone (back, not facing adhesive side)")).FirstOrDefault();
                    if (tmp != null)
                    {
                        tmp.C = (unit.Equals("ng/cm2")) ? ftirList[7].E : ftirList[8].B;//Silicone
                    }

                    tmp.C = tmp.C.Replace("<", "&lt;");
                }
             

                //FTIR
                //this.Ftir[6].C = (unit == 1) ? ftirList[7].B : ftirList[8].B;//Silicone Oil
                //this.Ftir[7].C = (unit == 1) ? ftirList[7].C : ftirList[8].C;//Hydrocarbon
                //this.Ftir[8].C = "";// (unit == 1) ? ftirList[7].B : ftirList[8].B;
                //this.Ftir[9].C = "";// (unit == 1) ? ftirList[7].B : ftirList[8].B;
                //this.Ftir[10].C = (unit == 1) ? ftirList[7].D : ftirList[8].D;
                //this.Ftir[11].C = (unit == 1) ? ftirList[7].E : ftirList[8].E;

                //part value to cover page method/procedure
                var items = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                if (items.Count > 0)
                {
                    items[0].C = String.Format("{0} pieces", txtWB14.Text);
                    items[1].C = String.Format("{0} pieces", txtWB14.Text);
                }

                gvWftir.DataSource = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_RAW_DATA)).ToList();
                gvWftir.DataBind();


                gvResultNvr.DataSource = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList();
                gvResultNvr.DataBind();

                gvResultFtir.DataSource = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
                gvResultFtir.DataBind();


                gvMethodProcedure.DataSource = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                gvMethodProcedure.DataBind();
                //remark
                lbA42.Text = String.Format(" {0}  ug/part  or {1} ng/cm2.", ftirList[5].B, ftirList[6].B);
            }
            btnSubmit.Enabled = true;
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dt = Extenders.ObjectToDataTable(this.Ftir[0]);
                List<template_seagate_ftir_coverpage> methods = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                List<template_seagate_ftir_coverpage> ftirs = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
                List<template_seagate_ftir_coverpage> nvrs = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList();
                ReportHeader reportHeader = new ReportHeader();
                reportHeader = reportHeader.getReportHeder(this.jobSample);


                ReportParameterCollection reportParameters = new ReportParameterCollection();

                reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
                reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
                reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
                reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));

                reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
                reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
                reportParameters.Add(new ReportParameter("Test", " "));
                reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));
                reportParameters.Add(new ReportParameter("rpt_unit2", ddlUnit.SelectedItem.Text));

                reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
                reportParameters.Add(new ReportParameter("Remarks", String.Format("Note: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is {0}", lbA42.Text)));

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ftir_nvr_seagate_damper.rdlc");
                viewer.LocalReport.SetParameters(reportParameters);
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", methods.ToDataTable())); // Add datasource here
                foreach (var item in this.Ftir)
                {
                    item.C = item.C.Replace("&lt;", "<");
                }

                if (nvrs.Count > 0 && nvrs.Count <= 4)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", nvrs.GetRange(0, nvrs.Count).ToDataTable())); // Add datasource here
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", new DataTable())); // Add datasource here
                }
                if (nvrs.Count > 4)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", nvrs.GetRange(0, 4).ToDataTable())); // Add datasource here
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", nvrs.GetRange(4, nvrs.Count - 4).ToDataTable())); // Add datasource here
                }
                if (nvrs.Count == 0)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", nvrs.ToDataTable())); // Add datasource here
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", nvrs.ToDataTable())); // Add datasource here

                    if (ftirs.Count > 0 && ftirs.Count <= 3)
                    {
                        viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ftirs.GetRange(0, ftirs.Count).ToDataTable())); // Add datasource here
                        viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", new DataTable())); // Add datasource here
                    }
                    if (ftirs.Count > 3)
                    {
                        viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ftirs.GetRange(0, 3).ToDataTable())); // Add datasource here
                        viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", ftirs.GetRange(3, ftirs.Count - 3).ToDataTable())); // Add datasource here
                    }

                }
                else
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ftirs.ToDataTable())); // Add datasource here
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", new DataTable())); // Add datasource here
                }

                if (ftirs.Count == 0)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ftirs.ToDataTable())); // Add datasource here
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", ftirs.ToDataTable())); // Add datasource here
                }



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



        }

        protected void lbDownloadPdf_Click(object sender, EventArgs e)
        {

            //DataTable dt = Extenders.ObjectToDataTable(this.Ftir[0]);
            List<template_seagate_ftir_coverpage> methods = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
            List<template_seagate_ftir_coverpage> ftirs = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
            List<template_seagate_ftir_coverpage> nvrs = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList();
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);


            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));

            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", " "));
            reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit2", ""));

            reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
            reportParameters.Add(new ReportParameter("Remarks", String.Format("Note: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is {0}", lbA42.Text)));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ftir_nvr_seagate_damper_pdf.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", methods.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", nvrs.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ftirs.ToDataTable())); // Add datasource here


            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download



        }
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

        #endregion


        protected void btnLoadFile_Click(object sender, EventArgs e)
        {


            String sheetName = string.Empty;

            List<tb_m_gcms_cas> _cas = new List<tb_m_gcms_cas>();
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");

            for (int i = 0; i < FileUpload1.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = FileUpload1.PostedFiles[i];
                try
                {
                    if (_postedFile.ContentLength > 0)
                    {
                        if (this.Ftir.Count > 0)
                        {
                            foreach (template_seagate_ftir_coverpage _item in this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_RAW_DATA)).ToList())
                            {
                                this.Ftir.Remove(_item);
                            }
                        }
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
                        if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")))
                        {
                            using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                            {
                                HSSFWorkbook wb = new HSSFWorkbook(fs);
                                #region "FTIR"
                                ISheet isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.ftir.excel.sheetname.working1"]);
                                if (isheet != null)
                                {
                                    txtWB13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.B));//Surface area per part (e) =
                                    txtWB14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.B));//No. of parts extracted (f) = 
                                    txtWB15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.B));//Total Surface area (A) =

                                    txtWD13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.D));//Surface area per part (e) =
                                    txtWD14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.D));//No. of parts extracted (f) = 
                                    txtWD15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.D));//Total Surface area (A) =

                                    lbWC13Unit.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.C));
                                    lbWC15Unit.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.C));

                                    lbWE13Unit.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.E));
                                    lbWE15Unit.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.E));

                                    for (int row = 18; row < 29; row++)
                                    {
                                        template_seagate_ftir_coverpage tmp = new template_seagate_ftir_coverpage();
                                        tmp.ID = row;
                                        tmp.A = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.A));
                                        tmp.B = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.B));
                                        tmp.C = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.C));
                                        tmp.D = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.D));
                                        tmp.E = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.E));

                                        switch ((row - 18) + 1)
                                        {
                                            case 2://Peak height measured, y(mAbs)
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : !Utils.CustomUtils.isNumber(tmp.B)? tmp.B :  Convert.ToDouble(tmp.B).ToString("N" + txtDecimal01.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : !Utils.CustomUtils.isNumber(tmp.C)? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal01.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : !Utils.CustomUtils.isNumber(tmp.D)? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal01.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : !Utils.CustomUtils.isNumber(tmp.E)? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal01.Text);
                                                break;
                                            case 3://Slope of curve, m
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : !Utils.CustomUtils.isNumber(tmp.B)? tmp.B : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal02.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : !Utils.CustomUtils.isNumber(tmp.C)? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal02.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : !Utils.CustomUtils.isNumber(tmp.D)? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal02.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : !Utils.CustomUtils.isNumber(tmp.E)? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal02.Text);
                                                break;
                                            case 4://y - intercept, c
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : !Utils.CustomUtils.isNumber(tmp.B)? tmp.B : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal03.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : !Utils.CustomUtils.isNumber(tmp.C)? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal03.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : !Utils.CustomUtils.isNumber(tmp.D)? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal03.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : !Utils.CustomUtils.isNumber(tmp.E) ? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal03.Text);
                                                break;
                                            case 5:// Amount, x (ug) = (y - c) / m
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : !Utils.CustomUtils.isNumber(tmp.B)? tmp.B : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal04.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : !Utils.CustomUtils.isNumber(tmp.C)? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal04.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : !Utils.CustomUtils.isNumber(tmp.D)? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal04.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : !Utils.CustomUtils.isNumber(tmp.E) ? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal04.Text);
                                                break;
                                            case 6:// Instrument Detection Limit(ug)
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : !Utils.CustomUtils.isNumber(tmp.B)? tmp.B : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal05.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : !Utils.CustomUtils.isNumber(tmp.C)? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal05.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : !Utils.CustomUtils.isNumber(tmp.D)? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal05.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : !Utils.CustomUtils.isNumber(tmp.E) ? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal05.Text);
                                                break;
                                            case 7:// Instrument Detection Limit(ng / sqcm)
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : !Utils.CustomUtils.isNumber(tmp.B)? tmp.B : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal06.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : !Utils.CustomUtils.isNumber(tmp.C)? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal06.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : !Utils.CustomUtils.isNumber(tmp.D)? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal06.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : !Utils.CustomUtils.isNumber(tmp.E) ? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal06.Text);
                                                break;
                                            case 8:
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : !Utils.CustomUtils.isNumber(tmp.B)? tmp.B : tmp.B.Equals("Detected") ? tmp.B : tmp.B.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.B :( tmp.B.Trim().Equals("<IDL") || tmp.B.Trim().Equals("< IDL")) ? tmp.B : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal07.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : !Utils.CustomUtils.isNumber(tmp.C)? tmp.C : tmp.C.Equals("Detected") ? tmp.C : tmp.C.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.C :( tmp.C.Trim().Equals("<IDL") || tmp.C.Trim().Equals("< IDL")) ? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal07.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : !Utils.CustomUtils.isNumber(tmp.D)? tmp.D : tmp.D.Equals("Detected") ? tmp.D : tmp.D.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.D :( tmp.D.Trim().Equals("<IDL") || tmp.D.Trim().Equals("< IDL")) ? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal07.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : !Utils.CustomUtils.isNumber(tmp.E) ? tmp.E : tmp.E.Equals("Detected") ? tmp.E : tmp.E.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.E :( tmp.E.Trim().Equals("<IDL") || tmp.E.Trim().Equals("< IDL")) ? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal07.Text);
                                                break;
                                            case 9:
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : !Utils.CustomUtils.isNumber(tmp.B)? tmp.B : tmp.B.Equals("Detected") ? tmp.B : tmp.B.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.B : (tmp.B.Trim().Equals("<IDL") || tmp.B.Trim().Equals("< IDL")) ? tmp.B : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal07.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : !Utils.CustomUtils.isNumber(tmp.C)? tmp.C : tmp.C.Equals("Detected") ? tmp.C : tmp.C.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.C : (tmp.C.Trim().Equals("<IDL") || tmp.C.Trim().Equals("< IDL")) ? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal07.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : !Utils.CustomUtils.isNumber(tmp.D)? tmp.D : tmp.D.Equals("Detected") ? tmp.D : tmp.D.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.D : (tmp.D.Trim().Equals("<IDL") || tmp.D.Trim().Equals("< IDL")) ? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal07.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : !Utils.CustomUtils.isNumber(tmp.E) ? tmp.E : tmp.E.Equals("Detected") ? tmp.E : tmp.E.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.E : (tmp.E.Trim().Equals("<IDL") || tmp.E.Trim().Equals("< IDL")) ? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal07.Text);
                                                break;
                                                //Amount(ng / cm2)
                                                //Amount(ug / cm2)
                                        }
                                        tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                                        tmp.data_type = 3;
                                        if (!String.IsNullOrEmpty(tmp.A))
                                        {
                                            this.Ftir.Add(tmp);
                                        }

                                        //CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.G));
                                    }
                                }
                                #endregion
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

        }

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

        protected void gvResultNvr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvResultNvr.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvResultNvr.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvResultFtir_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvResultFtir.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvResultFtir.DataKeys[e.Row.RowIndex].Values[1]);
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
                    template_seagate_ftir_coverpage gcms = this.Ftir.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE));
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

                        gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE));
                        gvMethodProcedure.DataBind();
                    }
                }
            }
        }

        protected void gvResultNvr_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_ftir_coverpage gcms = this.Ftir.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC));
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

                    gvResultNvr.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC));
                    gvResultNvr.DataBind();
                }
            }
        }

        protected void gvResultFtir_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_ftir_coverpage gcms = this.Ftir.Find(x => x.ID == PKID && x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC));
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

                    gvResultFtir.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC));
                    gvResultFtir.DataBind();
                }
            }
        }


        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
            }
            else
            {
                tb_m_specification mSpec = new tb_m_specification();
                mSpec = mSpec.SelectByID(this.Ftir[0].specification_id.Value);
                if (mSpec != null)
                {
                    //lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", mSpec.B, mSpec.A);
                    lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", mSpec.C, mSpec.B);

                }
            }

        }

        #region "method/procedure"
        protected void gvMethodProcedure_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = e.NewEditIndex;
            gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE));
            gvMethodProcedure.DataBind();
        }

        protected void gvMethodProcedure_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = -1;
            gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE));
            gvMethodProcedure.DataBind();
        }

        protected void gvMethodProcedure_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int _id = Convert.ToInt32(gvMethodProcedure.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox _txtExtractionVolume = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtExtractionVolume");

            TextBox _txtAnalysis = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtAnalysis");
            TextBox _txtProcedureNo = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtProcedureNo");
            TextBox _txtNumberOfPiecesUsedForExtraction = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtNumberOfPiecesUsedForExtraction");
            TextBox _txtExtractionMedium = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txtExtractionMedium");
            if (_txtExtractionVolume != null)
            {

                template_seagate_ftir_coverpage _tmp = this.Ftir.Find(x => x.ID == _id);
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
            gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE));
            gvMethodProcedure.DataBind();
        }
        #endregion

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvResultNvr.Columns[1].HeaderText = String.Format("Specification Limits ({0})", ddlUnit.SelectedItem.Text);
            gvResultNvr.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnit.SelectedItem.Text);
            gvResultFtir.Columns[1].HeaderText = String.Format("Specification Limits ({0})", ddlUnit.SelectedItem.Text);
            gvResultFtir.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnit.SelectedItem.Text);
            ModolPopupExtender.Show();
            CalculateCas();
        }

    }
}

