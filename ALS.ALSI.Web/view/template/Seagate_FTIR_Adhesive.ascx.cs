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
    public partial class Seagate_FTIR_Adhesive : System.Web.UI.UserControl
    {


        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Seagate_FTIR));

        #region "Property"
        public String[] MethodType = { "NVR/FTIR", "NVR", "NVR", "FTIR", "FTIR", "FTIR" };

        public users_login UserLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }

        public job_sample JobSample
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
        public List<template_img>  RefImg
        {
            get { return (List<template_img>)Session[GetType().Name + "template_img"]; }
            set { Session[GetType().Name + "template_img"] = value; }
        }

        private void RemoveSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "Ftir");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
        }

        private void InitialPage()
        {
            this.RefImg = new List<template_img>();
            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));

            List<tb_m_specification> listSpec = new tb_m_specification().SelectBySpecificationID(this.JobSample.specification_id, this.JobSample.template_id);
            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = listSpec.ToList();
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            tb_unit unit = new tb_unit();
            ddlUnit.Items.Clear();
            ddlUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("FTIR")).ToList();
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            #region "SAMPLE"
            this.JobSample = new job_sample().SelectByID(this.SampleID);
            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.JobSample.job_status.ToString(), true);
            if (this.JobSample != null)
            {
                lbJobStatus.Text = Constants.GetEnumDescription(status);
                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), UserLogin.role_id.ToString(), true);

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


                txtDateAnalyzed.Text = (this.JobSample.date_chemist_alalyze != null) ? this.JobSample.date_chemist_alalyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = userRole == RoleEnum.CHEMIST;

                #region "VISIBLE RESULT DATA"

                if (status == StatusEnum.CHEMIST_TESTING || UserLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
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
                    gvMethodProcedure.Columns[5].Visible = true;
                    gvResult.Columns[3].Visible = true;
                    gvResult1.Columns[3].Visible = true;

                    btnWorkingFTIR.Visible = true;
                    btnWorkingNVR.Visible = true;

                }
                else
                {
                    gvMethodProcedure.Columns[5].Visible = false;
                    gvResult.Columns[3].Visible = false;
                    gvResult1.Columns[3].Visible = false;

                    btnWorkingFTIR.Visible = false;
                    btnWorkingNVR.Visible = false;

                    if (UserLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
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

                cbCheckBox.Checked = (this.JobSample.is_no_spec == null) ? false : this.JobSample.is_no_spec.Equals("1") ? true : false;
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

                gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                gvMethodProcedure.DataBind();

                gvWftir.DataSource = this.Ftir.Where(x => x.data_type == 3).ToList();
                gvWftir.DataBind();

                gvResult.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList();
                gvResult.DataBind();

                gvResult1.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
                gvResult1.DataBind();


                #region "IMG"
                this.RefImg = template_img.FindAllBySampleID(this.SampleID);
                #endregion
                CalculateCas();
            }
            else
            {
                #region "Procedure"
                template_seagate_ftir_coverpage tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR (Seal & Label for HDA internal, Facestock)",
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = 1,
                    data_type = 1
                };
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR (Seal & Label for HDA internal, Adhesive side)",
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = 1,
                    data_type = 1
                };
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR (Release Liner, non-Silicone, facing adhesive side)",
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = 1,
                    data_type = 1
                };
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR (Release Liner, Silicone (HDA Product Label), facing adhesive side)",//
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = 1,
                    data_type = 1
                };
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR Release Liner, ultra-low Silicone facing adhesive(inside)",//
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = 1,
                    data_type = 1
                };
                this.Ftir.Add(tmp);
                //tmp = new template_seagate_ftir_coverpage();
                //tmp.ID = this.Ftir.Count + 1;
                //tmp.A = "FTIR Release Liner, ultra-low Silicone facing adhesive(outside)";
                //tmp.B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M";
                //tmp.C = "20 cm²";
                //tmp.D = "n-hexane(HPLC Grade)";
                //tmp.E = "10 mL";
                //tmp.row_type = 1;
                //tmp.data_type = 1;
                //this.Ftir.Add(tmp);
                #endregion

                gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                gvMethodProcedure.DataBind();
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
                InitialPage();
            }
        }

        protected void BtnWorkingFTIR_Click(object sender, EventArgs e)
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

                    //btnLoadFile.Visible = false;
                    pLoadFile.Visible = false;
                    var items = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                    if (items.Count > 0)
                    {
                        items[0].C = String.Format("{0} {1}", txtWB13.Text, lbW13Unit.Text);
                        items[1].C = String.Format("{0} {1}", txtWB13.Text, lbW13Unit.Text);
                        items[2].C = String.Format("{0} {1}", txtWB13.Text, lbW13Unit.Text);
                        items[3].C = String.Format("{0} {1}", txtWB13.Text, lbW13Unit.Text);
                        items[4].C = String.Format("{0} {1}", txtWB13.Text, lbW13Unit.Text);
                        items[5].C = String.Format("{0} {1}", txtWB13.Text, lbW13Unit.Text);
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

                    if (UserLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
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

                    if (UserLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                    {
                        pLoadFile.Visible = true;
                    }
                    break;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Boolean isValid = true;

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.JobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    this.JobSample.step1owner = UserLogin.id;

                    //this.Ftir.Delete();

                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.JobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.JobSample.step2owner = UserLogin.id;
                    this.JobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
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
                    this.JobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.JobSample.step3owner = UserLogin.id;
                    this.JobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";

                    //#region ":: STAMP COMPLETE DATE"
                    this.JobSample.date_chemist_complete = DateTime.Now;
                    this.JobSample.date_chemist_alalyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                    //#endregion

                    #region "NVR"


                    //this.Ftir.nvrb16 = nvrB16.Text;
                    //this.Ftir.nvrb17 = nvrB17.Text;
                    //this.Ftir.nvrb18 = nvrB18.Text;
                    //this.Ftir.nvrb20 = nvrB20.Text;
                    //this.Ftir.nvrb21 = nvrB21.Text;
                    //this.Ftir.nvrb22 = nvrB22.Text;
                    //this.Ftir.nvrb24 = nvrB24.Text;
                    //this.Ftir.nvrb25 = nvrB25.Text;
                    //this.Ftir.nvrb26 = nvrB26.Text;
                    //this.Ftir.nvrb28 = nvrB28.Text;
                    //this.Ftir.nvrb29 = nvrB29.Text;
                    //this.Ftir.nvrb30 = nvrB30.Text;
                    //this.Ftir.nvrb32 = nvrB32.Text;
                    //this.Ftir.nvrb33 = nvrB33.Text;
                    //this.Ftir.nvrb34 = nvrB34.Text;
                    //this.Ftir.nvrb36 = nvrB36.Text;
                    //this.Ftir.nvrb37 = nvrB37.Text;


                    //this.Ftir.nvrc16 = nvrC16.Text;
                    //this.Ftir.nvrc17 = nvrC17.Text;
                    //this.Ftir.nvrc18 = nvrC18.Text;
                    //this.Ftir.nvrc20 = nvrC20.Text;
                    //this.Ftir.nvrc21 = nvrC21.Text;
                    //this.Ftir.nvrc22 = nvrC22.Text;
                    //this.Ftir.nvrc24 = nvrC24.Text;
                    //this.Ftir.nvrc25 = nvrC25.Text;
                    //this.Ftir.nvrc26 = nvrC26.Text;
                    //this.Ftir.nvrc28 = nvrC28.Text;
                    //this.Ftir.nvrc29 = nvrC29.Text;
                    //this.Ftir.nvrc30 = nvrC30.Text;
                    //this.Ftir.nvrc32 = nvrC32.Text;
                    //this.Ftir.nvrc33 = nvrC33.Text;
                    //this.Ftir.nvrc34 = nvrC34.Text;
                    //this.Ftir.nvrc36 = nvrC36.Text;
                    //this.Ftir.nvrc37 = nvrC37.Text;

                    #endregion
                    foreach (template_seagate_ftir_coverpage item in this.Ftir)
                    {
                        item.sample_id = this.SampleID;
                        item.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        item.selected_unit_ftir = Convert.ToInt32(ddlUnit.SelectedValue);
                        item.w_b13 = txtWB13.Text;
                        item.w_b14 = txtWB14.Text;
                        item.w_b15 = txtWB15.Text;
                    }
                    //template_seagate_ftir_coverpage.UpdateList(this.Ftir);
                    template_seagate_ftir_coverpage.DeleteBySampleID(this.SampleID);
                    template_seagate_ftir_coverpage.InsertList(this.Ftir);

                    template_img.DeleteBySampleID(this.SampleID);
                    template_img.InsertList(this.RefImg);

                    this.JobSample.path_word = String.Empty;
                    this.JobSample.path_pdf = String.Empty;


                    break;
                case StatusEnum.SR_CHEMIST_CHECKING:
                    StatusEnum srChemistApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (srChemistApproveStatus)
                    {
                        case StatusEnum.SR_CHEMIST_APPROVE:
                            this.JobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                            #region ":: STAMP COMPLETE DATE"

                            this.JobSample.date_srchemist_complate = DateTime.Now;
                            #endregion
                            break;
                        case StatusEnum.SR_CHEMIST_DISAPPROVE:
                            this.JobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                            #region "LOG"
                            job_sample_logs jobSampleLog = new job_sample_logs
                            {
                                ID = 0,
                                job_sample_id = this.JobSample.ID,
                                log_title = String.Format("Sr.Chemist DisApprove"),
                                job_remark = txtRemark.Text,
                                is_active = "0",
                                date = DateTime.Now
                            };
                            jobSampleLog.Insert();
                            #endregion
                            break;
                    }
                    this.JobSample.step4owner = UserLogin.id;

                    break;
                case StatusEnum.LABMANAGER_CHECKING:
                    StatusEnum labApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (labApproveStatus)
                    {
                        case StatusEnum.LABMANAGER_APPROVE:
                            this.JobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF);

                            this.JobSample.date_labman_complete = DateTime.Now;
                            break;
                        case StatusEnum.LABMANAGER_DISAPPROVE:
                            this.JobSample.job_status = Convert.ToInt32(ddlAssignTo.SelectedValue);
                            #region "LOG"
                            job_sample_logs jobSampleLog = new job_sample_logs
                            {
                                ID = 0,
                                job_sample_id = this.JobSample.ID,
                                log_title = String.Format("Lab Manager DisApprove"),
                                job_remark = txtRemark.Text,
                                is_active = "0",
                                date = DateTime.Now
                            };
                            jobSampleLog.Insert();
                            #endregion
                            break;
                    }
                    this.JobSample.step5owner = UserLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (btnUpload.HasFile)// && (Path.GetExtension(btnUpload.FileName).Equals(".doc") || Path.GetExtension(btnUpload.FileName).Equals(".docx")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(btnUpload.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(btnUpload.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        btnUpload.SaveAs(source_file);
                        this.JobSample.path_word = source_file_url;
                        this.JobSample.job_status = Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING);
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
                    this.JobSample.step6owner = UserLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:

                    if (btnUpload.HasFile && (Path.GetExtension(btnUpload.FileName).Equals(".pdf")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(btnUpload.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(btnUpload.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        btnUpload.SaveAs(source_file);
                        this.JobSample.path_pdf = source_file_url;
                        this.JobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .pdf");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
                    //this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                    this.JobSample.step7owner = UserLogin.id;
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
                this.JobSample.update_date = DateTime.Now;
                this.JobSample.update_by = UserLogin.id;
                this.JobSample.Update();
                //Commit
                GeneralManager.Commit();

                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }


        }

        protected void BtnCalculate_Click(object sender, EventArgs e)
        {
            CalculateCas();
            btnSubmit.Enabled = true;
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect(this.PreviousPath);
        }

        protected void DdlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                if (this.Ftir.Count > 0)
                {
                    foreach (template_seagate_ftir_coverpage _item in this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList())
                    {
                        this.Ftir.Remove(_item);
                    }
                    foreach (template_seagate_ftir_coverpage _item in this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList())
                    {
                        this.Ftir.Remove(_item);
                    }
                    foreach (template_seagate_ftir_coverpage _item in this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList())
                    {
                        this.Ftir.Remove(_item);
                    }
                }

                #region "Procedure"
                template_seagate_ftir_coverpage tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR (Seal & Label for HDA internal, Facestock)",
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = Convert.ToInt32(RowTypeEnum.Normal),
                    data_type = Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)
                };
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR (Seal & Label for HDA internal, Adhesive side)",
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = Convert.ToInt32(RowTypeEnum.Normal),
                    data_type = Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)
                };
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR (Release Liner, non-Silicone, facing adhesive side)",
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = Convert.ToInt32(RowTypeEnum.Normal),
                    data_type = Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)
                };
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR (Release Liner, Silicone (HDA Product Label), facing adhesive side)",//
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = Convert.ToInt32(RowTypeEnum.Normal),
                    data_type = Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)
                };
                this.Ftir.Add(tmp);
                tmp = new template_seagate_ftir_coverpage
                {
                    ID = this.Ftir.Count + 1,
                    A = "FTIR Release Liner, ultra-low Silicone facing adhesive(inside)",//
                    B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M",
                    C = "20 cm²",
                    D = "n-hexane(HPLC Grade)",
                    E = "10 mL",
                    row_type = Convert.ToInt32(RowTypeEnum.Normal),
                    data_type = Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)
                };
                this.Ftir.Add(tmp);
                //tmp = new template_seagate_ftir_coverpage();
                //tmp.ID = this.Ftir.Count + 1;
                //tmp.A = "FTIR Release Liner, ultra-low Silicone facing adhesive(outside)";
                //tmp.B = "20800032-001 Rev. C,20800014 - 001 Rev.G,20800033 - 001 Rev.M";
                //tmp.C = "20 cm²";
                //tmp.D = "n-hexane(HPLC Grade)";
                //tmp.E = "10 mL";
                //tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                //tmp.data_type = Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE);
                //this.Ftir.Add(tmp);
                #endregion

                gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                gvMethodProcedure.DataBind();

                tb_m_specification item = new tb_m_specification();
                List<tb_m_specification> datas = item.SelectBySpecificationID(this.JobSample.specification_id, this.JobSample.template_id);
                item = datas.Where(x => x.ID == Convert.ToInt32(ddlSpecification.SelectedValue)).FirstOrDefault();
                if (item != null)
                {
                    //lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", item.B, item.A);
                    lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", item.C, item.B);

                    #region "NVR"
                    //template_seagate_ftir_coverpage tmp = new template_seagate_ftir_coverpage();

                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].E,
                        B = item.E,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].F,
                        B = item.F,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].G,
                        B = item.G,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].H,
                        B = item.H,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].I,
                        B = item.I,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].J,
                        B = item.J,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.NVR_SPEC)
                    };
                    this.Ftir.Add(tmp);

                    #endregion
                    #region "FTIR"
                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].L,
                        B = item.L,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].M,
                        B = item.M,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].N,
                        B = item.N,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].O,
                        B = item.O,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    //Add on

                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].P,
                        B = item.P,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)
                    };
                    this.Ftir.Add(tmp);

                    //tmp = new template_seagate_ftir_coverpage();
                    //tmp.ID = this.Ftir.Count + 1;
                    //tmp.A = "Release Liner, ultra-low Silicone facing adhesive (outside)";
                    //tmp.B = item.P;
                    //tmp.C = "";
                    //tmp.D = "";
                    //tmp.E = "";
                    //tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                    //tmp.data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC);
                    //this.Ftir.Add(tmp);

                    tmp = new template_seagate_ftir_coverpage
                    {
                        ID = this.Ftir.Count + 1,
                        A = datas[4].Q,
                        B = item.Q,
                        C = "",
                        D = "",
                        E = "",
                        row_type = Convert.ToInt32(RowTypeEnum.Normal),
                        data_type = Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)
                    };
                    this.Ftir.Add(tmp);
                    #endregion
                    #region "METHOD/PROCEDURE"
                    foreach (template_seagate_ftir_coverpage _ftir in this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)))
                    {
                        _ftir.B = String.IsNullOrEmpty(datas[1].C) ? datas[1].B : datas[1].C;
                    }
                    #endregion


                    gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                    gvMethodProcedure.DataBind();

                    gvResult.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList();
                    gvResult.DataBind();
                    gvResult1.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
                    gvResult1.DataBind();

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine();

            }
        }

        #region "Custom method"

        private void CalculateCas()
        {

            List<template_seagate_ftir_coverpage> ftirList = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_RAW_DATA)).ToList();
            if (ftirList.Count > 0)
            {
                String unit = ddlUnit.SelectedItem.Text;// Convert.ToInt32(ddlUnit.SelectedValue);

                List<template_seagate_ftir_coverpage> ftir2 = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC) ).ToList();

                if (ftir2.Count > 0)
                {
                    //[0]Seal & Label for HDA internal, Facestock
                    //[1]Seal & Label for HDA internal, Adhesive side
                    //[2]Release Liner, non-Silicone, facing adhesive side
                    //[3]Release Liner, Silicone(HDA Product Label), facing adhesive side
                    //[4]Release Liner, ultra-low Silicone facing adhesive(inside)
                    //[5]Release Liner, ultra-low Silicone facing adhesive(outside)

                    ftir2[0].C = (unit.Equals("ng/cm2")) ? ftirList[7].B : ftirList[8].B;
                    ftir2[1].C = (unit.Equals("ng/cm2")) ? ftirList[7].C : ftirList[8].C;
                    ftir2[2].C = (unit.Equals("ng/cm2")) ? ftirList[7].D : ftirList[8].D;
                    ftir2[3].C = (unit.Equals("ng/cm2")) ? ftirList[7].E : ftirList[8].E;
                    ftir2[4].C = (unit.Equals("ng/cm2")) ? ftirList[7].F : ftirList[8].F;
                    ftir2[5].C = (unit.Equals("ng/cm2")) ? ftirList[7].G : ftirList[8].G;                   

                }

                //FTIR
                //var nvrs = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
                //if (nvrs.Count > 0)
                //{
                //    nvrs[0].C = (unit == 1) ? ftirList[7].B : ftirList[8].B;//Silicone
                //    nvrs[1].C = (unit == 1) ? ftirList[7].C : ftirList[8].C;//Silicone (Release side)
                //    nvrs[2].C = (unit == 1) ? ftirList[7].D : ftirList[8].D;//Silicone (Non-release side)
                //    nvrs[3].C = (unit == 1) ? ftirList[7].E : ftirList[8].E;//Hydrocarbon
                //    nvrs[4].C = (unit == 1) ? ftirList[7].F : ftirList[8].F;//Silicone Oil
                //    nvrs[5].C = (unit == 1) ? ftirList[7].G : ftirList[8].G;//Phthalate
                //}
                //var ftir = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_RAW_DATA)).ToList();
                //if (ftir.Count > 0)
                //{
                //    ftir[0].C = (unit == 1) ? ftirList[7].B : ftirList[8].B;//Silicone
                //    ftir[1].C = (unit == 1) ? ftirList[7].C : ftirList[8].C;//Silicone (Release side)
                //    ftir[2].C = (unit == 1) ? ftirList[7].D : ftirList[8].D;//Silicone (Non-release side)
                //    ftir[3].C = (unit == 1) ? ftirList[7].E : ftirList[8].E;//Hydrocarbon
                //    ftir[4].C = (unit == 1) ? ftirList[7].F : ftirList[8].F;//Silicone Oil
                //    ftir[5].C = (unit == 1) ? ftirList[7].G : ftirList[8].G;//Phthalate
                //}

                gvWftir.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_RAW_DATA)).ToList();
                gvWftir.DataBind();

                gvResult.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList();
                gvResult.DataBind();

                gvResult1.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
                gvResult1.DataBind();

                gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                gvMethodProcedure.DataBind();



                //remark
                //lbA42.Text = String.Format(" {0}  ug/part  or {1} ng/cm2.", ftirList[5].B, ftirList[6].B);
                lbA42.Text = String.Format(" {0}  ug/part", String.IsNullOrEmpty(ftirList[5].B) ? String.Empty : Convert.ToDouble(ftirList[5].B).ToString("N"+txtDecimal10));
            }
            gvRefImages.DataSource = this.RefImg;
            gvRefImages.DataBind();
            pImage.Visible = this.RefImg != null && this.RefImg.Count > 0;
            btnSubmit.Enabled = true;
        }

        protected void LbDownload_Click(object sender, EventArgs e)
        {

            try
            {


                //DataTable dt = Extenders.ObjectToDataTable(this.Ftir[0]);
                List<template_seagate_ftir_coverpage> methods = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE)).ToList();
                List<template_seagate_ftir_coverpage> ftirs = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC)).ToList();
                List<template_seagate_ftir_coverpage> nvrs = this.Ftir.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal) && x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC)).ToList();
                ReportHeader reportHeader = ReportHeader.getReportHeder(this.JobSample);

                List<template_img> dat = this.RefImg.OrderBy(x => x.seq).ToList();
                foreach (template_img _i in dat)
                {
                    _i.img1 = CustomUtils.GetBytesFromImage(_i.img_path);
                }

                ReportParameterCollection reportParameters = new ReportParameterCollection
                {
                    new ReportParameter("CustomerPoNo", reportHeader.cusRefNo),
                    new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo),
                    new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""),
                    new ReportParameter("Company", reportHeader.addr1),
                    new ReportParameter("Company_addr", reportHeader.addr2),

                    new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""),
                    new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""),
                    new ReportParameter("DateTestCompleted", reportHeader.dateOfTestComplete.ToString("dd MMMM yyyy") + ""),
                    new ReportParameter("SampleDescription", reportHeader.description),
                    new ReportParameter("Test", " "),
                    new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text),
                    new ReportParameter("rpt_unit2", ddlUnit.SelectedItem.Text),

                    new ReportParameter("ResultDesc", lbSpecDesc.Text),
                    new ReportParameter("Remarks", String.Format("Note: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is {0}", lbA42.Text)),
                    new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.JobSample.singapore_ref_no) ? String.Empty : this.JobSample.singapore_ref_no))
                };

                // Variables
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer
                {
                    ProcessingMode = ProcessingMode.Local
                };
                viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ftir_nvr_seagate.rdlc");
                viewer.LocalReport.SetParameters(reportParameters);
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", methods.ToDataTable())); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", nvrs.ToDataTable())); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ftirs.ToDataTable())); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", dat.ToDataTable())); // Add datasource here

                if ((nvrs.Count + ftirs.Count) >= 4)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", nvrs.ToDataTable())); // Add datasource here
                }
                else
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", new DataTable())); // Add datasource here
                }
                if ((nvrs.Count + ftirs.Count) > 10 && dat.Count>0)
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", nvrs.ToDataTable())); // Add datasource here
                }
                else
                {
                    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", new DataTable())); // Add datasource here
                }





                string download = String.Empty;

                StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.JobSample.job_status.ToString(), true);
                switch (status)
                {
                    case StatusEnum.ADMIN_CONVERT_WORD:
                        if (!String.IsNullOrEmpty(this.JobSample.path_word))
                        {
                            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.JobSample.path_word));
                        }
                        else
                        {
                            byte[] bytes = viewer.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out string[] streamIds, out Warning[] warnings);

                            if (!Directory.Exists(Server.MapPath("~/Report/")))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Report/"));
                            }
                            using (FileStream fs = File.Create(Server.MapPath("~/Report/") + this.JobSample.job_number + "_orginal." + extension))
                            {
                                fs.Write(bytes, 0, bytes.Length);
                            }


                            #region "Insert Footer & Header from template"
                            Document doc1 = new Document();
                            doc1.LoadFromFile(Server.MapPath("~/template/") + "Blank Letter Head - EL.doc");
                            Spire.Doc.HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
                            Spire.Doc.HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
                            Document doc2 = new Document(Server.MapPath("~/Report/") + this.JobSample.job_number + "_orginal." + extension);
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



                            doc2.SaveToFile(Server.MapPath("~/Report/") + this.JobSample.job_number + "." + extension);
                            #endregion
                            Response.ContentType = mimeType;
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + this.JobSample.job_number + "." + extension);
                            Response.WriteFile(Server.MapPath("~/Report/" + this.JobSample.job_number + "." + extension));
                            Response.Flush();

                            #region "Delete After Download"
                            String deleteFile1 = Server.MapPath("~/Report/") + this.JobSample.job_number + "." + extension;
                            String deleteFile2 = Server.MapPath("~/Report/") + this.JobSample.job_number + "_orginal." + extension;

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
                        if (!String.IsNullOrEmpty(this.JobSample.path_word))
                        {
                            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.JobSample.path_word));
                        }
                        break;
                    case StatusEnum.ADMIN_CONVERT_PDF:
                        if (!String.IsNullOrEmpty(this.JobSample.path_word))
                        {
                            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.JobSample.path_word));
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }




        }
        
        private void DownloadWord()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";
            string strFileName = string.Format("{0}_{1}.doc", this.JobSample.job_number.Replace("-", "_"), DateTime.Now.ToString("yyyyMMddhhmmss"));

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

        protected void DdlStatus_SelectedIndexChanged(object sender, EventArgs e)
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


        protected void BtnLoadFile_Click(object sender, EventArgs e)
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
                            foreach (template_seagate_ftir_coverpage _item in this.Ftir.Where(x => x.data_type == 3).ToList())
                            {
                                this.Ftir.Remove(_item);
                            }
                        }
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, Path.GetFileName(_postedFile.FileName));
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
                                    lbW13Unit.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.C));
                                    lbW15Unit.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.C));
                                    for (int row = 18; row < 29; row++)
                                    {
                                        template_seagate_ftir_coverpage tmp = new template_seagate_ftir_coverpage
                                        {
                                            ID = row,
                                            A = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.A)),
                                            B = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.B)),
                                            C = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.C)),
                                            D = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.D)),
                                            E = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.E)),
                                            F = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.F)),
                                            G = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.G))
                                        };

                                        switch ((row - 18) + 1)
                                        {
                                            case 2://Peak height measured, y(mAbs)
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal01.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal01.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal01.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal01.Text);
                                                tmp.F = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal01.Text);
                                                tmp.G = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal01.Text);
                                                break;
                                            case 3://Slope of curve, m
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal02.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal02.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal02.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal02.Text);
                                                tmp.F = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal02.Text);
                                                tmp.G = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal02.Text);
                                                break;
                                            case 4://y - intercept, c
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal03.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal03.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal03.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal03.Text);
                                                tmp.F = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal03.Text);
                                                tmp.G = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal03.Text);
                                                break;
                                            case 5:// Amount, x (ug) = (y - c) / m
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal04.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal04.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal04.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal04.Text);
                                                tmp.F = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal04.Text);
                                                tmp.G = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal04.Text);
                                                break;
                                            case 6:// Instrument Detection Limit(ug)
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal05.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal05.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal05.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal05.Text);
                                                tmp.F = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal05.Text);
                                                tmp.G = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal05.Text);
                                                break;
                                            case 7:// Instrument Detection Limit(ng / sqcm)
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal06.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal06.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal06.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal06.Text);
                                                tmp.F = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal06.Text);
                                                tmp.G = (String.IsNullOrEmpty(tmp.F)) ? "" : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal06.Text);
                                                break;
                                            case 8:
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : tmp.B.Equals("Detected") ? tmp.B : tmp.B.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.B : tmp.B.Equals("< IDL") ? tmp.B : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal07.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : tmp.C.Equals("Detected") ? tmp.C : tmp.C.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.C : tmp.C.Equals("< IDL") ? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal07.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : tmp.D.Equals("Detected") ? tmp.D : tmp.D.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.D : tmp.D.Equals("< IDL") ? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal07.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : tmp.E.Equals("Detected") ? tmp.E : tmp.E.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.E : tmp.E.Equals("< IDL") ? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal07.Text);
                                                tmp.F = (String.IsNullOrEmpty(tmp.F)) ? "" : tmp.F.Equals("Detected") ? tmp.F : tmp.F.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.F : tmp.F.Equals("< IDL") ? tmp.F : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal07.Text);
                                                tmp.G = (String.IsNullOrEmpty(tmp.G)) ? "" : tmp.G.Equals("Detected") ? tmp.G : tmp.G.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.G : tmp.G.Equals("< IDL") ? tmp.G : Convert.ToDouble(tmp.G).ToString("N" + txtDecimal07.Text);
                                                break;
                                            case 9:
                                                tmp.B = (String.IsNullOrEmpty(tmp.B)) ? "" : tmp.B.Equals("Detected") ? tmp.B : tmp.B.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.B : tmp.B.Equals("< IDL") ? tmp.B : Convert.ToDouble(tmp.B).ToString("N" + txtDecimal07.Text);
                                                tmp.C = (String.IsNullOrEmpty(tmp.C)) ? "" : tmp.C.Equals("Detected") ? tmp.C : tmp.C.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.C : tmp.C.Equals("< IDL") ? tmp.C : Convert.ToDouble(tmp.C).ToString("N" + txtDecimal07.Text);
                                                tmp.D = (String.IsNullOrEmpty(tmp.D)) ? "" : tmp.D.Equals("Detected") ? tmp.D : tmp.D.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.D : tmp.D.Equals("< IDL") ? tmp.D : Convert.ToDouble(tmp.D).ToString("N" + txtDecimal07.Text);
                                                tmp.E = (String.IsNullOrEmpty(tmp.E)) ? "" : tmp.E.Equals("Detected") ? tmp.E : tmp.E.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.E : tmp.E.Equals("< IDL") ? tmp.E : Convert.ToDouble(tmp.E).ToString("N" + txtDecimal07.Text);
                                                tmp.F = (String.IsNullOrEmpty(tmp.F)) ? "" : tmp.F.Equals("Detected") ? tmp.F : tmp.F.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.F : tmp.F.Equals("< IDL") ? tmp.F : Convert.ToDouble(tmp.F).ToString("N" + txtDecimal07.Text);
                                                tmp.G = (String.IsNullOrEmpty(tmp.G)) ? "" : tmp.G.Equals("Detected") ? tmp.G : tmp.G.ToUpper().Equals("Not Detected".ToUpper()) ? tmp.G : tmp.G.Equals("< IDL") ? tmp.G : Convert.ToDouble(tmp.G).ToString("N" + txtDecimal07.Text);
                                                break;
                                                //Amount(ng / cm2)
                                                //Amount(ug / cm2)
                                        }
                                        tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                                        tmp.data_type = Convert.ToInt32(FtirNvrEnum.FTIR_RAW_DATA);
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
                        //else
                        //{
                        //    errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));
                        //}
                        #endregion
                        #region "IMG"
                        if ((Path.GetExtension(_postedFile.FileName).ToLower().Equals(".jpg")))
                        {
                            template_img _img = new template_img
                            {
                                id = CustomUtils.GetRandomNumberID(),
                                sample_id = this.SampleID,
                                seq = this.RefImg.Count + 1
                            };
                            String fn = String.Format("{0}_IMG_{1}{2}{3}", this.JobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(_postedFile.FileName));

                            String _source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.JobSample.job_number, fn);
                            String _source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.JobSample.job_number, fn));

                            if (!Directory.Exists(Path.GetDirectoryName(_source_file)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                            }
                            _img.img_path = _source_file_url;
                            this.RefImg.Add(_img);
                            _postedFile.SaveAs(_source_file);
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


        protected void GvMethodProcedure_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void GvProcedure_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void GvResult_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void GvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Edit") && !e.CommandName.Equals("Cancel") && !e.CommandName.Equals("Update"))
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

                        gvResult.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.NVR_SPEC));
                        gvResult.DataBind();
                    }
                }
            }
        }

        protected void GvResult1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvResult1.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvResult1.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");


                if (_btnHide != null && _btnUndo != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            _btnHide.Visible = false;
                            _btnUndo.Visible = true;
                            e.Row.ForeColor = System.Drawing.Color.Red;
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

        protected void GvResult1_RowCommand(object sender, GridViewCommandEventArgs e)
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

                    gvResult1.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.FTIR_SPEC));
                    gvResult1.DataBind();
                }
            }
        }

        protected void CbCheckBox_CheckedChanged(object sender, EventArgs e)
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
        protected void GvMethodProcedure_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = e.NewEditIndex;
            gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE));
            gvMethodProcedure.DataBind();
        }

        protected void GvMethodProcedure_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = -1;
            gvMethodProcedure.DataSource = this.Ftir.Where(x => x.data_type == Convert.ToInt32(FtirNvrEnum.METHOD_PROCEDURE));
            gvMethodProcedure.DataBind();
        }

        protected void GvMethodProcedure_RowUpdating(object sender, GridViewUpdateEventArgs e)
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


        protected void DdlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

            gvResult.Columns[1].HeaderText = String.Format("Specification Limits ({0})", ddlUnit.SelectedItem.Text);
            gvResult.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnit.SelectedItem.Text);

            gvResult1.Columns[1].HeaderText = String.Format("Specification Limits ({0})", ddlUnit.SelectedItem.Text);
            gvResult1.Columns[2].HeaderText = String.Format("Results,({0})", ddlUnit.SelectedItem.Text);
            ModolPopupExtender.Show();
            CalculateCas();
        }



        protected void GvRefImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_img _mesa = this.RefImg.Find(x => x.id == PKID);
                if (_mesa != null)
                {
                    switch (cmd)
                    {
                        case CommandNameEnum.Delete:
                            this.RefImg.Remove(_mesa);
                            break;

                    }
                    gvRefImages.DataSource = this.RefImg.OrderBy(x => x.seq);
                    gvRefImages.DataBind();
                }
            }
        }

        protected void GvRefImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void TxtDecimal10_TextChanged(object sender, EventArgs e)
        {
            CalculateCas();
        }
    }
}

