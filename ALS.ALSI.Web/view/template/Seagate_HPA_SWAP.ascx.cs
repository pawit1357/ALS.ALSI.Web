﻿using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.view.request;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using ALS.ALSI.Web.Properties;
using System.Collections;
using System.Data;
using ALS.ALSI.Biz.ReportObjects;
using Microsoft.Reporting.WebForms;
using Spire.Doc;

namespace ALS.ALSI.Web.view.template
{
    public partial class Seagate_HPA_SWAP : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Seagate_HPA));

        #region "Property"

        private String[] lpc03A = {
                                    "Total number of particles ≥ 0.3μm",
                                    "1st Run",
                                    "2nd Run",
                                    "3rd Run",
                                    "Average"
                                  };
        private String[] lpc06A = {
                                    "Total number of particles ≥ 0.6μm",
                                    "1st Run",
                                    "2nd Run",
                                    "3rd Run",
                                    "Average"
                                  };

        private String[] ANameKey = {
                        "Class 1, Total Hard Particle",
                        "Class 2, Total Magnetic Particle(Counts/part)",
                        " *Class 3, Total Semi-hard Metal Particle",
                        "    3.1  SST + Fe base + FeO",
                        "    3.2  Sn base Particle",
                        "    **3.3  PZT Particles",
                        "Class 4, Total Soft-Metal Particle",
                        "Class 5, Total Environment Particle",
                        "    5.1  MgSiO Particle",
                        "Class 6, Other Particle",
                        "MgTiO",
                        "NiP with AlMgSi"
                                   };


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

        public List<template_seagate_hpa_coverpage> Hpas
        {
            get {
                List<template_seagate_hpa_coverpage> tmps = (List<template_seagate_hpa_coverpage>)Session[GetType().Name + "Hpas"];
                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);
                return (userRole == RoleEnum.CHEMIST) ? tmps : tmps.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
                //return (List<template_seagate_hpa_coverpage>)Session[GetType().Name + "Hpas"];
            }
            set { Session[GetType().Name + "Hpas"] = value; }
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
            //Session.Remove(GetType().Name + "tbCas");
            //Session.Remove(GetType().Name + "coverpages");
            Session.Remove(GetType().Name + "Hpas");
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
            ddlLiquidParticleUnit.Items.Clear();
            ddlLiquidParticleUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("HPA")).ToList();
            ddlLiquidParticleUnit.DataBind();
            ddlLiquidParticleUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlHardParticleAlalysisUnit.Items.Clear();
            ddlHardParticleAlalysisUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("HPA")).ToList();
            ddlHardParticleAlalysisUnit.DataBind();
            ddlHardParticleAlalysisUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlClassificationUnit.Items.Clear();
            ddlClassificationUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("HPA")).ToList();
            ddlClassificationUnit.DataBind();
            ddlClassificationUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));




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

                            gvHpa.Columns[3].Visible = false;
                            gvClassification.Columns[3].Visible = false;

                            txtProcedureNo.ReadOnly = true;
                            txtNumberOfPieces.ReadOnly = true;


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

                            gvHpa.Columns[3].Visible = true;
                            gvClassification.Columns[3].Visible = true;

                            txtProcedureNo.ReadOnly = false;
                            txtNumberOfPieces.ReadOnly = false;


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


                            gvHpa.Columns[3].Visible = false;
                            gvClassification.Columns[3].Visible = false;

                            txtProcedureNo.ReadOnly = true;
                            txtNumberOfPieces.ReadOnly = true;


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


                            gvHpa.Columns[3].Visible = false;
                            gvClassification.Columns[3].Visible = false;

                            txtProcedureNo.ReadOnly = true;
                            txtNumberOfPieces.ReadOnly = true;


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

                            gvHpa.Columns[3].Visible = false;
                            gvClassification.Columns[3].Visible = false;

                            txtProcedureNo.ReadOnly = true;
                            txtNumberOfPieces.ReadOnly = true;


                        }
                        break;
                }

                txtDateAnalyzed.Text = (this.jobSample.date_chemist_alalyze != null) ? this.jobSample.date_chemist_alalyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = (userRole == RoleEnum.CHEMIST);


                calPercentAreaCoverage();

            }
            #endregion

            this.Hpas = template_seagate_hpa_coverpage.FindAllBySampleID(this.SampleID);
            if (this.Hpas != null && this.Hpas.Count > 0)
            {
                this.CommandName = CommandNameEnum.Edit;
                template_seagate_hpa_coverpage _cover = this.Hpas[0];
                ddlLiquidParticleUnit.SelectedValue = this.Hpas[0].unit == null ? "0" : this.Hpas[0].unit.Value.ToString();
                ddlHardParticleAlalysisUnit.SelectedValue = this.Hpas[0].unit2 == null ? "0" : this.Hpas[0].unit2.Value.ToString();
                ddlClassificationUnit.SelectedValue = this.Hpas[0].unit3 == null ? "0" : this.Hpas[0].unit3.Value.ToString();


                tb_m_specification tem = new tb_m_specification().SelectByID(Convert.ToInt32(_cover.specification_id));
                if (tem != null)
                {
                    ddlSpecification.SelectedValue = tem.ID.ToString();


                    #region "Method"
                    txtProcedureNo.Text = _cover.ProcedureNo;
                    txtNumberOfPieces.Text = _cover.NumberOfPieces;


                    cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;

                    if (cbCheckBox.Checked)
                    {
                        lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
                    }
                    else
                    {
                        lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", tem.B + " ", tem.A);

                    }
                    #endregion

                    #region "region "US-LPC(0.3)"
                    txt_UsLPC03_B14.Text = _cover.us03_b14;
                    txt_UsLPC03_B15.Text = _cover.us03_b15;
                    txt_UsLPC03_B16.Text = _cover.us03_b16;
                    txt_UsLPC03_B17.Text = _cover.us03_b17;

                    txt_UsLPC03_C14.Text = _cover.us03_c14;
                    txt_UsLPC03_C15.Text = _cover.us03_c15;
                    txt_UsLPC03_C16.Text = _cover.us03_c16;
                    txt_UsLPC03_C17.Text = _cover.us03_c17;

                    txt_UsLPC03_D14.Text = _cover.us03_d14;
                    txt_UsLPC03_D15.Text = _cover.us03_d15;
                    txt_UsLPC03_D16.Text = _cover.us03_d16;
                    txt_UsLPC03_D17.Text = _cover.us03_d17;

                    txt_UsLPC03_E14.Text = _cover.us03_e14;
                    txt_UsLPC03_E15.Text = _cover.us03_e15;
                    txt_UsLPC03_E16.Text = _cover.us03_e16;
                    txt_UsLPC03_E17.Text = _cover.us03_e17;

                    txt_UsLPC03_F14.Text = _cover.us03_f14;
                    txt_UsLPC03_F15.Text = _cover.us03_f15;
                    txt_UsLPC03_F16.Text = _cover.us03_f16;
                    txt_UsLPC03_F17.Text = _cover.us03_f17;

                    txt_UsLPC03_G14.Text = _cover.us03_g14;
                    txt_UsLPC03_G15.Text = _cover.us03_g15;
                    txt_UsLPC03_G16.Text = _cover.us03_g16;
                    txt_UsLPC03_G17.Text = _cover.us03_g17;

                    txt_UsLPC03_B25_1.Text = _cover.us03_b25;
                    txt_UsLPC03_D25.Text = _cover.us03_d25;
                    txt_UsLPC03_F25.Text = _cover.us03_f25;
                    #endregion

                    #region "region "US-LPC(0.6)"
                    txt_UsLPC06_B14.Text = _cover.us06_b14;
                    txt_UsLPC06_B15.Text = _cover.us06_b15;
                    txt_UsLPC06_B16.Text = _cover.us06_b16;
                    txt_UsLPC06_B17.Text = _cover.us06_b17;

                    txt_UsLPC06_C14.Text = _cover.us06_c14;
                    txt_UsLPC06_C15.Text = _cover.us06_c15;
                    txt_UsLPC06_C16.Text = _cover.us06_c16;
                    txt_UsLPC06_C17.Text = _cover.us06_c17;

                    txt_UsLPC06_D14.Text = _cover.us06_d14;
                    txt_UsLPC06_D15.Text = _cover.us06_d15;
                    txt_UsLPC06_D16.Text = _cover.us06_d16;
                    txt_UsLPC06_D17.Text = _cover.us06_d17;

                    txt_UsLPC06_E14.Text = _cover.us06_e14;
                    txt_UsLPC06_E15.Text = _cover.us06_e15;
                    txt_UsLPC06_E16.Text = _cover.us06_e16;
                    txt_UsLPC06_E17.Text = _cover.us06_e17;

                    txt_UsLPC06_F14.Text = _cover.us06_f14;
                    txt_UsLPC06_F15.Text = _cover.us06_f15;
                    txt_UsLPC06_F16.Text = _cover.us06_f16;
                    txt_UsLPC06_F17.Text = _cover.us06_f17;

                    txt_UsLPC06_G14.Text = _cover.us06_g14;
                    txt_UsLPC06_G15.Text = _cover.us06_g15;
                    txt_UsLPC06_G16.Text = _cover.us06_g16;
                    txt_UsLPC06_G17.Text = _cover.us06_g17;

                    txt_UsLPC06_B25.Text = _cover.us06_b25;
                    txt_UsLPC06_D25.Text = _cover.us06_d25;
                    txt_UsLPC06_F25.Text = _cover.us06_f25;
                    #endregion

                    #region "Worksheet for HPA - Filtration"
                    txtB3.Text = _cover.ws_b3;
                    txtB4.Text = _cover.ws_b4;
                    txtB5.Text = _cover.ws_b5;
                    txtB6.Text = (_cover.ws_b6 == null) ? "50" : _cover.ws_b6;
                    txtB7.Text = (_cover.ws_b7 == null) ? "7.071" : _cover.ws_b7;
                    //txtB8.Text = _cover.ws_b8;
                    txtB8.Text = _cover.ws_b8;
                    #endregion

                    #region "Unit"

                    gvHpa.Columns[1].HeaderText = String.Format("Specification Limit, ({0})", ddlHardParticleAlalysisUnit.SelectedItem.Text);
                    gvHpa.Columns[2].HeaderText = String.Format("Results,({0})", ddlHardParticleAlalysisUnit.SelectedItem.Text);

                    gvClassification.Columns[2].HeaderText = String.Format("Results, {0}", ddlClassificationUnit.SelectedItem.Text);
                    #endregion
                    CalculateCas();
                }
            }
            else
            {
                this.CommandName = CommandNameEnum.Add;
                this.Hpas = new List<template_seagate_hpa_coverpage>();
            }



            //initial component
            btnCoverPage.CssClass = "btn green";
            btnUsLPC03.CssClass = "btn blue";
            btnUsLPC06.CssClass = "btn blue";
            btnWorksheetForHPAFiltration.CssClass = "btn blue";
            pCoverPage.Visible = true;
            pLoadFile.Visible = false;
            pUS_LPC03.Visible = false;
            pUS_LPC06.Visible = false;
            btnUsLPC03.Visible = false;
            btnUsLPC06.Visible = false;
            pWorksheetForHPAFiltration.Visible = false;


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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            template_seagate_hpa_coverpage objWork = new template_seagate_hpa_coverpage();

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
                    foreach (template_seagate_hpa_coverpage ws in this.Hpas)
                    {
                        ws.sample_id = this.SampleID;
                        ws.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        #region "Method/Procedure"
                        ws.ProcedureNo = txtProcedureNo.Text;
                        ws.NumberOfPieces = txtNumberOfPieces.Text;
                        #endregion

                        ws.unit = Convert.ToInt32(ddlLiquidParticleUnit.SelectedValue);
                        ws.unit2 = Convert.ToInt32(ddlHardParticleAlalysisUnit.SelectedValue);
                        ws.unit3 = Convert.ToInt32(ddlClassificationUnit.SelectedValue);
                    }
                    template_seagate_hpa_coverpage.DeleteBySampleID(this.SampleID);
                    objWork.InsertList(this.Hpas);

                    break;
                case StatusEnum.CHEMIST_TESTING:
                    if (this.Hpas.Count > 0)
                    {
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                        this.jobSample.step3owner = userLogin.id;
                        this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                        this.jobSample.date_chemist_alalyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                        //#region ":: STAMP COMPLETE DATE"
                        this.jobSample.date_chemist_complete = DateTime.Now;
                        //#endregion
                        foreach (template_seagate_hpa_coverpage ws in this.Hpas)
                        {
                            ws.sample_id = this.SampleID;
                            ws.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                            ws.unit = Convert.ToInt32(ddlLiquidParticleUnit.SelectedValue);
                            ws.unit2 = Convert.ToInt32(ddlHardParticleAlalysisUnit.SelectedValue);
                            ws.unit3 = Convert.ToInt32(ddlClassificationUnit.SelectedValue);
                            #region "Method/Procedure"
                            ws.ProcedureNo = txtProcedureNo.Text;
                            ws.NumberOfPieces = txtNumberOfPieces.Text;
                            #endregion

                            #region "region "US-LPC(0.3)"
                            ws.us03_b14 = txt_UsLPC03_B14.Text;
                            ws.us03_b15 = txt_UsLPC03_B15.Text;
                            ws.us03_b16 = txt_UsLPC03_B16.Text;
                            ws.us03_b17 = txt_UsLPC03_B17.Text;

                            ws.us03_c14 = txt_UsLPC03_C14.Text;
                            ws.us03_c15 = txt_UsLPC03_C15.Text;
                            ws.us03_c16 = txt_UsLPC03_C16.Text;
                            ws.us03_c17 = txt_UsLPC03_C17.Text;

                            ws.us03_d14 = txt_UsLPC03_D14.Text;
                            ws.us03_d15 = txt_UsLPC03_D15.Text;
                            ws.us03_d16 = txt_UsLPC03_D16.Text;
                            ws.us03_d17 = txt_UsLPC03_D17.Text;

                            ws.us03_e14 = txt_UsLPC03_E14.Text;
                            ws.us03_e15 = txt_UsLPC03_E15.Text;
                            ws.us03_e16 = txt_UsLPC03_E16.Text;
                            ws.us03_e17 = txt_UsLPC03_E17.Text;

                            ws.us03_f14 = txt_UsLPC03_F14.Text;
                            ws.us03_f15 = txt_UsLPC03_F15.Text;
                            ws.us03_f16 = txt_UsLPC03_F16.Text;
                            ws.us03_f17 = txt_UsLPC03_F17.Text;

                            ws.us03_g14 = txt_UsLPC03_G14.Text;
                            ws.us03_g15 = txt_UsLPC03_G15.Text;
                            ws.us03_g16 = txt_UsLPC03_G16.Text;
                            ws.us03_g17 = txt_UsLPC03_G17.Text;

                            ws.us03_b25 = txt_UsLPC03_B25_1.Text;
                            ws.us03_d25 = txt_UsLPC03_D25.Text;
                            ws.us03_f25 = txt_UsLPC03_F25.Text;
                            #endregion

                            #region "region "US-LPC(0.6)"
                            ws.us06_b14 = txt_UsLPC06_B14.Text;
                            ws.us06_b15 = txt_UsLPC06_B15.Text;
                            ws.us06_b16 = txt_UsLPC06_B16.Text;
                            ws.us06_b17 = txt_UsLPC06_B17.Text;

                            ws.us06_c14 = txt_UsLPC06_C14.Text;
                            ws.us06_c15 = txt_UsLPC06_C15.Text;
                            ws.us06_c16 = txt_UsLPC06_C16.Text;
                            ws.us06_c17 = txt_UsLPC06_C17.Text;

                            ws.us06_d14 = txt_UsLPC06_D14.Text;
                            ws.us06_d15 = txt_UsLPC06_D15.Text;
                            ws.us06_d16 = txt_UsLPC06_D16.Text;
                            ws.us06_d17 = txt_UsLPC06_D17.Text;

                            ws.us06_e14 = txt_UsLPC06_E14.Text;
                            ws.us06_e15 = txt_UsLPC06_E15.Text;
                            ws.us06_e16 = txt_UsLPC06_E16.Text;
                            ws.us06_e17 = txt_UsLPC06_E17.Text;

                            ws.us06_f14 = txt_UsLPC06_F14.Text;
                            ws.us06_f15 = txt_UsLPC06_F15.Text;
                            ws.us06_f16 = txt_UsLPC06_F16.Text;
                            ws.us06_f17 = txt_UsLPC06_F17.Text;

                            ws.us06_g14 = txt_UsLPC06_G14.Text;
                            ws.us06_g15 = txt_UsLPC06_G15.Text;
                            ws.us06_g16 = txt_UsLPC06_G16.Text;
                            ws.us06_g17 = txt_UsLPC06_G17.Text;

                            ws.us06_b25 = txt_UsLPC06_B25.Text;
                            ws.us06_d25 = txt_UsLPC06_D25.Text;
                            ws.us06_f25 = txt_UsLPC06_F25.Text;
                            #endregion

                            #region "Worksheet for HPA - Filtration"
                            ws.ws_b3 = txtB3.Text;
                            ws.ws_b4 = txtB4.Text;
                            ws.ws_b5 = txtB5.Text;
                            ws.ws_b6 = txtB6.Text;
                            ws.ws_b7 = txtB7.Text;
                            //ws.ws_b8 = txtB8.Text;
                            ws.ws_b8 = txtB8.Text;
                            #endregion

                        }
                        template_seagate_hpa_coverpage.DeleteBySampleID(this.SampleID);
                        objWork.InsertList(this.Hpas);
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
                    if (FileUpload1.HasFile)// && (Path.GetExtension(btnUpload.FileName).Equals(".doc") || Path.GetExtension(btnUpload.FileName).Equals(".docx")))
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
                        this.jobSample.path_word = source_file_url;
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING);
                        lbMessage.Text = string.Empty;
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
                    if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).Equals(".pdf")))
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
                        this.jobSample.path_pdf = source_file_url;
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .pdf");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        isValid = false;
                    }
                    //this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                    //this.jobSample.step7owner = userLogin.id;
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

                //removeSession();
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
            string sheetName = string.Empty;

            if (String.IsNullOrEmpty(txtB3.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Volume of Extraction (ml), Vt")); }
            if (String.IsNullOrEmpty(txtB4.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Surface Area (cm2), C")); }
            if (String.IsNullOrEmpty(txtB5.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Number of Parts Extracted, N")); }
            if (String.IsNullOrEmpty(txtB6.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Volume of Filtration (ml), Vf")); }
            if (String.IsNullOrEmpty(txtB7.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Filter Area (sqmm), At")); }
            //if (String.IsNullOrEmpty(txtB8.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Percent Area Coverage (%)")); }
            if (String.IsNullOrEmpty(txtB8.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Number of Parts Extracted, N")); }

            if (errors.Count == 0)
            {
                List<template_seagate_hpa_coverpage> lists = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)).OrderBy(x => x.seq).ToList();
                #region "LOAD"
                String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");

                String[] B1_03 = new String[4];
                String[] B1_06 = new String[4];
                String[] S1_03 = new String[4];
                String[] S1_06 = new String[4];

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

                            String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));
                            //String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));


                            if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                            }
                            _postedFile.SaveAs(source_file);

                            switch (Path.GetExtension(source_file))
                            {
                                case ".csv":
                                case ".txt":
                                    using (StreamReader reader = new StreamReader(source_file))
                                    {
                                        int index = 0;
                                        string line;
                                        while ((line = reader.ReadLine()) != null)
                                        {

                                            if (index == 0)
                                            {
                                                index++;
                                                continue;
                                            }

                                            String[] data = line.Split(',');

                                            string subfix = Path.GetFileNameWithoutExtension(source_file);

                                            switch (subfix.Substring(subfix.Length - 1))
                                            {
                                                case "B":
                                                    #region "HPA(B)"
                                                    foreach (template_seagate_hpa_coverpage _cov in lists)
                                                    {
                                                        if (mappingRawData(_cov.B).ToUpper().Replace(" ", String.Empty).Equals(data[0].ToUpper().Replace(" ", String.Empty)))
                                                        {
                                                            template_seagate_hpa_coverpage _hpa = this.Hpas.Where(x => x.ID == _cov.ID).FirstOrDefault();
                                                            if (_hpa != null)
                                                            {
                                                                _hpa.BlankCouts = Convert.ToInt32(data[2]);
                                                            }
                                                        }
                                                    }
                                                    Console.WriteLine("");
                                                    #endregion
                                                    break;
                                                case "S":
                                                    #region "HPA(S)"
                                                    foreach (template_seagate_hpa_coverpage _cov in lists)
                                                    {
                                                        if (mappingRawData(_cov.B).ToUpper().Replace(" ", String.Empty).Equals(data[0].ToUpper().Replace(" ", String.Empty)))
                                                        {
                                                            template_seagate_hpa_coverpage _hpa = this.Hpas.Where(x => x.ID == _cov.ID).FirstOrDefault();
                                                            if (_hpa != null)
                                                            {
                                                                _hpa.RawCounts = Convert.ToInt32(data[2]);
                                                            }
                                                        }
                                                    }

                                                    Console.WriteLine("");
                                                    #endregion
                                                    break;
                                            }
                                            index++;
                                        }
                                    }
                                    break;
                                default:
                                    using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                                    {

                                        HSSFWorkbook wd = new HSSFWorkbook(fs);
                                        ISheet sheet = null;

                                        switch (Path.GetFileNameWithoutExtension(source_file))
                                        {
                                            case "S":
                                            case "B":
                                                sheet = wd.GetSheet("Sheet1");
                                                if (sheet != null)
                                                {
                                                    sheetName = sheet.SheetName;

                                                    #region "LPC (S,B)"
                                                    int index_03 = 0;
                                                    int index_06 = 0;
                                                    for (int row = 22; row <= sheet.LastRowNum; row++)
                                                    {
                                                        if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                                                        {
                                                            String CS = CustomUtils.GetCellValue(sheet.GetRow(row).GetCell(9));
                                                            #region "0.300"
                                                            if (CS.Equals("0.300"))
                                                            {
                                                                String CD = CustomUtils.GetCellValue(sheet.GetRow(row).GetCell(18));
                                                                switch (Path.GetFileNameWithoutExtension(source_file))
                                                                {
                                                                    case "B":
                                                                        B1_03[index_03] = String.Format("{0:n2}", Math.Round(Convert.ToDouble(CD), 2));
                                                                        break;
                                                                    case "S":
                                                                        S1_03[index_03] = String.Format("{0:n2}", Math.Round(Convert.ToDouble(CD), 2));
                                                                        break;
                                                                }
                                                                index_03++;
                                                            }
                                                            #endregion
                                                            #region "0.600"
                                                            if (CS.Equals("0.600"))
                                                            {
                                                                String CD = CustomUtils.GetCellValue(sheet.GetRow(row).GetCell(18));
                                                                switch (Path.GetFileNameWithoutExtension(source_file))
                                                                {
                                                                    case "B":
                                                                        B1_06[index_06] = String.Format("{0:n2}", Math.Round(Convert.ToDouble(CD), 2));
                                                                        break;
                                                                    case "S":
                                                                        S1_06[index_06] = String.Format("{0:n2}", Math.Round(Convert.ToDouble(CD), 2));
                                                                        break;
                                                                }
                                                                index_06++;
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", "Sheet1"));
                                                }
                                                #endregion
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", sheetName, CustomUtils.ErrorIndex));
                        Console.WriteLine();
                    }
                }
                #endregion


                if (errors.Count > 0)
                {
                    litErrorMessage.Text = MessageBox.GenWarnning(errors);
                    modalErrorList.Show();

                }
                else
                {
                    litErrorMessage.Text = String.Empty;



                    #region "SET DATA TO FORM"
                    #region "US-LPC(0.3)"
                    txt_UsLPC03_B14.Text = B1_03[0];
                    txt_UsLPC03_B15.Text = B1_03[1];
                    txt_UsLPC03_B16.Text = B1_03[2];
                    txt_UsLPC03_B17.Text = B1_03[3];

                    txt_UsLPC03_C14.Text = S1_03[0];
                    txt_UsLPC03_C15.Text = S1_03[1];
                    txt_UsLPC03_C16.Text = S1_03[2];
                    txt_UsLPC03_C17.Text = S1_03[3];

                    #endregion
                    #region "US-LPC(0.6)"
                    txt_UsLPC06_B14.Text = B1_06[0];
                    txt_UsLPC06_B15.Text = B1_06[1];
                    txt_UsLPC06_B16.Text = B1_06[2];
                    txt_UsLPC06_B17.Text = B1_06[3];

                    txt_UsLPC06_C14.Text = S1_06[0];
                    txt_UsLPC06_C15.Text = S1_06[1];
                    txt_UsLPC06_C16.Text = S1_06[2];
                    txt_UsLPC06_C17.Text = S1_06[3];

                    #endregion

                    #region "Worksheet for HPA - Filtration"
                    gvWsClassification.DataSource = lists;
                    gvWsClassification.DataBind();
                    #endregion
                    #endregion
                    CalculateCas();
                    //btnSubmit.Enabled = false;
                }
            }
            else
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();


            }

        }

        protected void btnUsLPC_Click(object sender, EventArgs e)
        {
            List<String> errors = new List<string>();

            //NumOfPiecesUse
            txt_UsLPC03_B22.Text = txtNumberOfPieces.Text;
            txt_UsLPC06_B22.Text = txtNumberOfPieces.Text;
            //txtB5.Text = txtNumberOfPieces.Text;


            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnCoverPage.CssClass = "btn green";
                    btnUsLPC03.CssClass = "btn blue";
                    btnUsLPC06.CssClass = "btn blue";
                    btnWorksheetForHPAFiltration.CssClass = "btn blue";
                    pCoverPage.Visible = true;
                    pLoadFile.Visible = false;
                    pUS_LPC03.Visible = false;
                    pUS_LPC06.Visible = false;
                    pWorksheetForHPAFiltration.Visible = false;

                    break;
                case "US-LPC(0.3)":
                    if (String.IsNullOrEmpty(txtNumberOfPieces.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Number Of Pieces")); }
                    if (String.IsNullOrEmpty(txtB4.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Surface Area")); }

                    if (errors.Count > 0)
                    {
                        litErrorMessage.Text = MessageBox.GenWarnning(errors);
                    }
                    else
                    {
                        litErrorMessage.Text = string.Empty;
                        btnCoverPage.CssClass = "btn blue";
                        btnUsLPC03.CssClass = "btn green";
                        btnUsLPC06.CssClass = "btn blue";
                        btnWorksheetForHPAFiltration.CssClass = "btn blue";
                        pCoverPage.Visible = false;
                        pLoadFile.Visible = true;
                        pUS_LPC03.Visible = true;
                        pUS_LPC06.Visible = false;
                        pWorksheetForHPAFiltration.Visible = false;

                    }
                    break;
                case "US-LPC(0.6)":
                    if (String.IsNullOrEmpty(txtNumberOfPieces.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Number Of Pieces")); }
                    if (String.IsNullOrEmpty(txtB4.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Surface Area")); }

                    if (errors.Count > 0)
                    {
                        litErrorMessage.Text = MessageBox.GenWarnning(errors);
                    }
                    else
                    {
                        litErrorMessage.Text = string.Empty;

                        btnCoverPage.CssClass = "btn blue";
                        btnUsLPC03.CssClass = "btn blue";
                        btnUsLPC06.CssClass = "btn green";
                        btnWorksheetForHPAFiltration.CssClass = "btn blue";
                        pCoverPage.Visible = false;
                        pLoadFile.Visible = true;
                        pUS_LPC03.Visible = false;
                        pUS_LPC06.Visible = true;
                        pWorksheetForHPAFiltration.Visible = false;


                    }
                    break;
                case "Worksheet for HPA - Filtration":
                    if (String.IsNullOrEmpty(txtNumberOfPieces.Text)) { errors.Add(String.Format("กรุณาตรวจสอบ {0}", "Number Of Pieces")); }
                    if (errors.Count > 0)
                    {

                        litErrorMessage.Text = MessageBox.GenWarnning(errors);
                        modalErrorList.Show();
                    }
                    else
                    {
                        litErrorMessage.Text = string.Empty;

                        btnCoverPage.CssClass = "btn blue";
                        btnUsLPC03.CssClass = "btn blue";
                        btnUsLPC06.CssClass = "btn blue";
                        btnWorksheetForHPAFiltration.CssClass = "btn green";

                        pCoverPage.Visible = false;
                        pLoadFile.Visible = true;
                        pUS_LPC03.Visible = false;
                        pUS_LPC06.Visible = false;
                        pWorksheetForHPAFiltration.Visible = true;
                    }
                    break;
            }
        }

        #region "Custom method"

        private void CalculateCas()
        {

            List<String> ignoreItemList = new List<string>();
            ignoreItemList.Add("NiP with AlMgSi");

            #region "US-LPC(0.3)"
            //=AVERAGE(B15,B16,B17)
            if (!String.IsNullOrEmpty(txt_UsLPC03_B15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B17.Text))
            {
                txt_UsLPC03_B18.Text = ((
                    Convert.ToDouble(txt_UsLPC03_B15.Text) +
                    Convert.ToDouble(txt_UsLPC03_B16.Text) +
                    Convert.ToDouble(txt_UsLPC03_B17.Text)) / 3).ToString();

                txt_UsLPC03_B18.Text = String.Format("{0:n3}", Math.Round(Convert.ToDouble(txt_UsLPC03_B18.Text), 3));
            }
            //=AVERAGE(C15,C16,C17)
            if (!String.IsNullOrEmpty(txt_UsLPC03_C15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_C16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_C17.Text))
            {
                txt_UsLPC03_C18.Text = ((
                    Convert.ToDouble(txt_UsLPC03_C15.Text) +
                    Convert.ToDouble(txt_UsLPC03_C16.Text) +
                    Convert.ToDouble(txt_UsLPC03_C17.Text)) / 3).ToString();
                txt_UsLPC03_C18.Text = String.Format("{0:n3}", Math.Round(Convert.ToDouble(txt_UsLPC03_C18.Text), 3));
            }
            //=AVERAGE(D15,D16,D17)
            if (!String.IsNullOrEmpty(txt_UsLPC03_D15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_D16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_D17.Text))
            {
                txt_UsLPC03_D18.Text = ((
                    Convert.ToDouble(txt_UsLPC03_D15.Text) +
                    Convert.ToDouble(txt_UsLPC03_D16.Text) +
                    Convert.ToDouble(txt_UsLPC03_D17.Text)) / 3).ToString();
                txt_UsLPC03_D18.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC03_D18.Text), 2));
            }
            //=AVERAGE(E15,E16,E17)
            if (!String.IsNullOrEmpty(txt_UsLPC03_E15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_E16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_E17.Text))
            {
                txt_UsLPC03_E18.Text = ((
                    Convert.ToDouble(txt_UsLPC03_E15.Text) +
                    Convert.ToDouble(txt_UsLPC03_E16.Text) +
                    Convert.ToDouble(txt_UsLPC03_E17.Text)) / 3).ToString();
                txt_UsLPC03_E18.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC03_E18.Text), 2));
            }
            //=AVERAGE(F15,F16,F17)
            if (!String.IsNullOrEmpty(txt_UsLPC03_F15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_F16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_F17.Text))
            {
                txt_UsLPC03_F18.Text = ((
                    Convert.ToDouble(txt_UsLPC03_F15.Text) +
                    Convert.ToDouble(txt_UsLPC03_F16.Text) +
                    Convert.ToDouble(txt_UsLPC03_F17.Text)) / 3).ToString();
                txt_UsLPC03_F18.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC03_F18.Text), 2));
            }
            //=AVERAGE(G15,G16,G17)
            if (!String.IsNullOrEmpty(txt_UsLPC03_G15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_G16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_G17.Text))
            {
                txt_UsLPC03_G18.Text = ((
                    Convert.ToDouble(txt_UsLPC03_G15.Text) +
                    Convert.ToDouble(txt_UsLPC03_G16.Text) +
                    Convert.ToDouble(txt_UsLPC03_G17.Text)) / 3).ToString();
                txt_UsLPC03_G18.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC03_G18.Text), 2));
            }
            //FORM COVER PAGE

            //txt_UsLPC03_B20.Text = txtB3.Text;//khz68_03.ws_b3;
            //txt_UsLPC03_B21.Text = txtB4.Text;//khz68_03.ws_b4;
            //txt_UsLPC03_B22.Text = txtB5.Text;//khz68_03.ws_b5;

            if (!String.IsNullOrEmpty(txt_UsLPC03_C18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B20.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B21.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B22.Text))
            {
                //=(C18-B18)*$B$20/($B$21*$B$22)
                Double sbv = ((Convert.ToDouble(txt_UsLPC03_C18.Text) - Convert.ToDouble(txt_UsLPC03_B18.Text)) * Convert.ToDouble(txt_UsLPC03_B20.Text));
                Double an = Convert.ToDouble(txt_UsLPC03_B21.Text) * Convert.ToDouble(txt_UsLPC03_B22.Text);
                //txt_UsLPC03_B25_1.Text = "99";// (sbv / an).ToString();


                txt_UsLPC03_B25_1.Text = Math.Round(sbv / an).ToString();
            }
            //=(E18-D18)*$B$20/($B$21*$B$22)
            if (!String.IsNullOrEmpty(txt_UsLPC03_E18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_D18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B20.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B21.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B22.Text))
            {
                txt_UsLPC03_D25.Text = (((Convert.ToDouble(txt_UsLPC03_E18.Text) - Convert.ToDouble(txt_UsLPC03_D18.Text)) * Convert.ToDouble(txt_UsLPC03_B20.Text)) /
                                        (Convert.ToDouble(txt_UsLPC03_B21.Text) * Convert.ToDouble(txt_UsLPC03_B22.Text))).ToString();
                txt_UsLPC03_D25.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC03_D25.Text)));
            }
            //=(G18-F18)*$B$20/($B$21*$B$22)
            if (!String.IsNullOrEmpty(txt_UsLPC03_G18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_F18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B20.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B21.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC03_B22.Text))
            {
                txt_UsLPC03_F25.Text = (((Convert.ToDouble(txt_UsLPC03_G18.Text) - Convert.ToDouble(txt_UsLPC03_F18.Text)) * Convert.ToDouble(txt_UsLPC03_B20.Text)) /
                                        (Convert.ToDouble(txt_UsLPC03_B21.Text) * Convert.ToDouble(txt_UsLPC03_B22.Text))).ToString();
                txt_UsLPC03_F25.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC03_F25.Text)));
            }
            //=AVERAGE(B25,D25,F25)
            if (!String.IsNullOrEmpty(txt_UsLPC03_B25_1.Text))
            {
                txt_UsLPC03_B26.Text = Math.Round(Convert.ToDouble(txt_UsLPC03_B25_1.Text)).ToString();
            }
            #endregion

            #region "US-LPC(0.6)"
            //=AVERAGE(B15,B16,B17)
            if (!String.IsNullOrEmpty(txt_UsLPC06_B15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B17.Text))
            {
                txt_UsLPC06_B18.Text = ((
                    Convert.ToDouble(txt_UsLPC06_B15.Text) +
                    Convert.ToDouble(txt_UsLPC06_B16.Text) +
                    Convert.ToDouble(txt_UsLPC06_B17.Text)) / 3).ToString();

                txt_UsLPC06_B18.Text = String.Format("{0:n3}", Math.Round(Convert.ToDouble(txt_UsLPC06_B18.Text), 3));
            }
            //=AVERAGE(C15,C16,C17)
            if (!String.IsNullOrEmpty(txt_UsLPC06_C15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_C16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_C17.Text))
            {
                txt_UsLPC06_C18.Text = ((
                    Convert.ToDouble(txt_UsLPC06_C15.Text) +
                    Convert.ToDouble(txt_UsLPC06_C16.Text) +
                    Convert.ToDouble(txt_UsLPC06_C17.Text)) / 3).ToString();
                txt_UsLPC06_C18.Text = String.Format("{0:n3}", Math.Round(Convert.ToDouble(txt_UsLPC06_C18.Text), 3));
            }
            //=AVERAGE(D15,D16,D17)
            if (!String.IsNullOrEmpty(txt_UsLPC06_D15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_D16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_D17.Text))
            {
                txt_UsLPC06_D18.Text = ((
                    Convert.ToDouble(txt_UsLPC06_D15.Text) +
                    Convert.ToDouble(txt_UsLPC06_D16.Text) +
                    Convert.ToDouble(txt_UsLPC06_D17.Text)) / 3).ToString();
                txt_UsLPC06_D18.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC06_D18.Text), 2));
            }
            //=AVERAGE(E15,E16,E17)
            if (!String.IsNullOrEmpty(txt_UsLPC06_E15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_E16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_E17.Text))
            {
                txt_UsLPC06_E18.Text = ((
                    Convert.ToDouble(txt_UsLPC06_E15.Text) +
                    Convert.ToDouble(txt_UsLPC06_E16.Text) +
                    Convert.ToDouble(txt_UsLPC06_E17.Text)) / 3).ToString();
                txt_UsLPC06_E18.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC06_E18.Text), 2));
            }
            //=AVERAGE(F15,F16,F17)
            if (!String.IsNullOrEmpty(txt_UsLPC06_F15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_F16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_F17.Text))
            {
                txt_UsLPC06_F18.Text = ((
                    Convert.ToDouble(txt_UsLPC06_F15.Text) +
                    Convert.ToDouble(txt_UsLPC06_F16.Text) +
                    Convert.ToDouble(txt_UsLPC06_F17.Text)) / 3).ToString();
                txt_UsLPC06_F18.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC06_F18.Text), 2));
            }
            //=AVERAGE(G15,G16,G17)
            if (!String.IsNullOrEmpty(txt_UsLPC06_G15.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_G16.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_G17.Text))
            {
                txt_UsLPC06_G18.Text = ((
                    Convert.ToDouble(txt_UsLPC06_G15.Text) +
                    Convert.ToDouble(txt_UsLPC06_G16.Text) +
                    Convert.ToDouble(txt_UsLPC06_G17.Text)) / 3).ToString();
                txt_UsLPC06_G18.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC06_G18.Text), 2));
            }
            //FORM COVER PAGE

            //txt_UsLPC06_B20.Text = txtB3.Text;// khz68_03.ws_b3;
            //txt_UsLPC06_B21.Text = txtB4.Text;//khz68_03.ws_b4;
            //txt_UsLPC06_B22.Text = txtB5.Text;// khz68_03.ws_b5;
            if (!String.IsNullOrEmpty(txt_UsLPC06_C18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B20.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B21.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B22.Text))
            {
                //=(C18-B18)*$B$20/($B$21*$B$22)
                txt_UsLPC06_B25.Text = (((Convert.ToDouble(txt_UsLPC06_C18.Text) - Convert.ToDouble(txt_UsLPC06_B18.Text)) * Convert.ToDouble(txt_UsLPC06_B20.Text)) /
                                        (Convert.ToDouble(txt_UsLPC06_B21.Text) * Convert.ToDouble(txt_UsLPC06_B22.Text))).ToString();
                txt_UsLPC06_B25.Text = Math.Round(Convert.ToDouble(txt_UsLPC06_B25.Text)).ToString();
            }
            //=(E18-D18)*$B$20/($B$21*$B$22)
            if (!String.IsNullOrEmpty(txt_UsLPC06_E18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_D18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B20.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B21.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B22.Text))
            {
                txt_UsLPC06_D25.Text = (((Convert.ToDouble(txt_UsLPC06_E18.Text) - Convert.ToDouble(txt_UsLPC06_D18.Text)) * Convert.ToDouble(txt_UsLPC06_B20.Text)) /
                                        (Convert.ToDouble(txt_UsLPC06_B21.Text) * Convert.ToDouble(txt_UsLPC06_B22.Text))).ToString();
                txt_UsLPC06_D25.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC06_D25.Text)));
            }
            //=(G18-F18)*$B$20/($B$21*$B$22)
            if (!String.IsNullOrEmpty(txt_UsLPC06_G18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_F18.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B20.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B21.Text) &&
                !String.IsNullOrEmpty(txt_UsLPC06_B22.Text))
            {
                txt_UsLPC06_F25.Text = (((Convert.ToDouble(txt_UsLPC06_G18.Text) - Convert.ToDouble(txt_UsLPC06_F18.Text)) * Convert.ToDouble(txt_UsLPC06_B20.Text)) /
                                        (Convert.ToDouble(txt_UsLPC06_B21.Text) * Convert.ToDouble(txt_UsLPC06_B22.Text))).ToString();
                txt_UsLPC06_F25.Text = String.Format("{0:n2}", Math.Round(Convert.ToDouble(txt_UsLPC06_F25.Text)));
            }
            //=AVERAGE(B25,D25,F25)
            if (!String.IsNullOrEmpty(txt_UsLPC06_B25.Text))
            {
                txt_UsLPC06_B26.Text = Math.Round(Convert.ToDouble(txt_UsLPC06_B25.Text)).ToString();
            }
            #endregion


            template_seagate_hpa_coverpage _tmp = this.Hpas.Where(x => x.A.Contains("1st Run") && x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC03)).FirstOrDefault();
            if (_tmp != null)
            {
                _tmp.C = Convert.ToDouble(String.IsNullOrEmpty(txt_UsLPC03_B25_1.Text) ? "0" : txt_UsLPC03_B25_1.Text) + "";
            }
            _tmp = this.Hpas.Where(x => x.A.Contains("2nd Run") && x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC03)).FirstOrDefault();
            if (_tmp != null)
            {
                _tmp.C = Convert.ToDouble(String.IsNullOrEmpty(txt_UsLPC03_D25.Text) ? "0" : txt_UsLPC03_D25.Text) + "";
            }
            _tmp = this.Hpas.Where(x => x.A.Contains("3rd Run") && x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC03)).FirstOrDefault();
            if (_tmp != null)
            {
                _tmp.C = Convert.ToDouble(String.IsNullOrEmpty(txt_UsLPC03_F25.Text) ? "0" : txt_UsLPC03_F25.Text) + "";
            }
            _tmp = this.Hpas.Where(x => x.A.Contains("Average") && x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC03)).FirstOrDefault();
            if (_tmp != null)
            {
                _tmp.C = Convert.ToDouble(String.IsNullOrEmpty(txt_UsLPC03_B26.Text) ? "0" : txt_UsLPC03_B26.Text) + "";
            }
            _tmp = this.Hpas.Where(x => x.A.Contains("1st Run") && x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC06)).FirstOrDefault();
            if (_tmp != null)
            {
                _tmp.C = Convert.ToDouble(String.IsNullOrEmpty(txt_UsLPC06_B25.Text) ? "0" : txt_UsLPC06_B25.Text) + "";
            }
            _tmp = this.Hpas.Where(x => x.A.Contains("2nd Run") && x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC06)).FirstOrDefault();
            if (_tmp != null)
            {
                _tmp.C = Convert.ToDouble(String.IsNullOrEmpty(txt_UsLPC06_D25.Text) ? "0" : txt_UsLPC06_D25.Text) + "";
            }
            _tmp = this.Hpas.Where(x => x.A.Contains("3rd Run") && x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC06)).FirstOrDefault();
            if (_tmp != null)
            {
                _tmp.C = Convert.ToDouble(String.IsNullOrEmpty(txt_UsLPC06_F25.Text) ? "0" : txt_UsLPC06_F25.Text) + "";
            }
            _tmp = this.Hpas.Where(x => x.A.Contains("Average") && x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC06)).FirstOrDefault();
            if (_tmp != null)
            {
                _tmp.C = Convert.ToDouble(String.IsNullOrEmpty(txt_UsLPC06_B26.Text) ? "0" : txt_UsLPC06_B26.Text) + "";
            }

            #region "Worksheet for HPA - Filtration"
            if (!String.IsNullOrEmpty(txtB3.Text) &&//Volume of Extraction (ml), Vt 
                !String.IsNullOrEmpty(txtB4.Text) &&//Surface Area (cm2), C 
                !String.IsNullOrEmpty(txtB5.Text) &&//Number of Parts Extracted, N 
                !String.IsNullOrEmpty(txtB6.Text) &&//Volume of Filtration (ml), Vf 
                !String.IsNullOrEmpty(txtB7.Text) &&//Filter Area (sqmm), At 
                                                    //!String.IsNullOrEmpty(txtB8.Text) &&//Percent Area Coverage (%) 
                !String.IsNullOrEmpty(txtB8.Text) //Number of Parts Extracted, N 

                )
            {
                List<template_seagate_hpa_coverpage> lists = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)).OrderBy(x => x.seq).ToList();
                foreach (template_seagate_hpa_coverpage _val in lists)
                {
                    //=ROUND(MAX(0,(C13-B13))*($B$7/$B$9)*($B$3/$B$6)/($B$4*$B$5),1)
                    //=ROUND(MAX(0,(C13-B13))*($B$7/$B$9)*($B$3/$B$6)/($B$4*$B$5),1)
                    //=($B$3*((C15/$B$8)-(B15/$B$6)))/($B$5*$B$4)
                    Console.WriteLine();
                    Double b3 = !CustomUtils.isNumber(txtB3.Text) ? 0 : Convert.ToDouble(txtB3.Text);
                    Double b4 = !CustomUtils.isNumber(txtB4.Text) ? 0 : Convert.ToDouble(txtB4.Text);
                    Double b5 = !CustomUtils.isNumber(txtB5.Text) ? Convert.ToDouble(txtB5.Text.Split(' ')[0]) : Convert.ToDouble(txtB5.Text);
                    Double b6 = !CustomUtils.isNumber(txtB6.Text) ? 0 : Convert.ToDouble(txtB6.Text);
                    Double b8 = !CustomUtils.isNumber(txtB8.Text) ? 0 : Convert.ToDouble(txtB8.Text);
                    int B = (_val.BlankCouts == null) ? 0 : _val.BlankCouts.Value;
                    int C = (_val.RawCounts == null) ? 0 : _val.RawCounts.Value;
                    if (b8 > 0 && b6 > 0)
                    {
                        Double cq = (b3 * ((C / b8) - (B / b6))) / (b5 * b4);
                        _val.C = Math.Round(cq, 1).ToString();

                    }
                }


                foreach (template_seagate_hpa_coverpage _cov in this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_HEAD)))
                {
                    Double _BlankCouts = (Double)this.Hpas.Where(x => !ignoreItemList.Contains(x.B) && x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) && x.data_group.Equals(_cov.data_group)).Sum(x => x.BlankCouts);
                    Double _RawCounts = (Double)this.Hpas.Where(x => !ignoreItemList.Contains(x.B) && x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) && x.data_group.Equals(_cov.data_group)).Sum(x => x.RawCounts);
                    Double _sumC = (Double)this.Hpas.Where(x => !ignoreItemList.Contains(x.B) && x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) && x.data_group.Equals(_cov.data_group)).Sum(x => Convert.ToDouble(x.C));

                    template_seagate_hpa_coverpage tmp = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_TOTAL) && x.data_group.Equals(_cov.data_group)).FirstOrDefault();
                    if (tmp != null)
                    {


                        tmp.BlankCouts = Convert.ToInt32(_BlankCouts);
                        tmp.RawCounts = Convert.ToInt32(_RawCounts);
                        tmp.C = _sumC + "";
                    }
                }

                Double _GrandBlankCouts = (Double)this.Hpas.Where(x => !ignoreItemList.Contains(x.B) && x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)).Sum(x => x.BlankCouts);
                Double _GrandRawCounts = (Double)this.Hpas.Where(x => !ignoreItemList.Contains(x.B) && x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)).Sum(x => x.RawCounts);
                Double _GrandsumC = (Double)this.Hpas.Where(x => !ignoreItemList.Contains(x.B) && x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)).Sum(x => Convert.ToDouble(x.C));

                template_seagate_hpa_coverpage tmpGrand = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_GRAND_TOTAL)).FirstOrDefault();
                if (tmpGrand != null)
                {
                    tmpGrand.BlankCouts = Convert.ToInt32(_GrandBlankCouts);
                    tmpGrand.RawCounts = Convert.ToInt32(_GrandRawCounts);
                    tmpGrand.C = _GrandsumC + "";
                }
                //Grand Total

                #region "ADHOC"
                //Total SST
                List<String> listOfSst = new List<string>();
                listOfSst.Add("SST300s with possible Si");
                listOfSst.Add("SST300s with possible Si and Mn");
                listOfSst.Add("SST400s with possible Si");

                Double _sumTotalSST_BlankCounts = (Double)this.Hpas.Where(x => listOfSst.Contains(x.B)).Sum(x => x.BlankCouts);
                Double _sumTotalSST_RawCounts = (Double)this.Hpas.Where(x => listOfSst.Contains(x.B)).Sum(x => x.RawCounts);
                Double _sumTotalSST = (Double)this.Hpas.Where(x => listOfSst.Contains(x.B)).Sum(x => Convert.ToDouble(x.C));
                template_seagate_hpa_coverpage cov = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_SUB_TOTAL) && x.B.Equals("Total SST")).FirstOrDefault();
                if (cov != null)
                {
                    cov.BlankCouts = Convert.ToInt32(_sumTotalSST_BlankCounts);
                    cov.RawCounts = Convert.ToInt32(_sumTotalSST_RawCounts);
                    cov.C = _sumTotalSST + "";
                }
                //Total Ni
                List<String> listOfNi = new List<string>();
                listOfNi.Add("Ni based");
                listOfNi.Add("NiP");
                Double _sumTotalNi_BlankCounts = (Double)this.Hpas.Where(x => listOfNi.Contains(x.B)).Sum(x => x.BlankCouts);
                Double _sumTotalNi_RawCounts = (Double)this.Hpas.Where(x => listOfNi.Contains(x.B)).Sum(x => x.RawCounts);
                Double _sumTotalNi = (Double)this.Hpas.Where(x => listOfNi.Contains(x.B)).Sum(x => Convert.ToDouble(x.C));
                template_seagate_hpa_coverpage covNi = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_SUB_TOTAL) && x.B.Equals("Total Ni")).FirstOrDefault();
                if (covNi != null)
                {
                    covNi.BlankCouts = Convert.ToInt32(_sumTotalNi_BlankCounts);
                    covNi.RawCounts = Convert.ToInt32(_sumTotalNi_RawCounts);
                    covNi.C = _sumTotalNi + "";
                }
                #endregion

                #region "Hard Particle Analysis"

                /*

                        "[0]Class 1, Total Hard Particle",
                        "[1]Class 2, Total Magnetic Particle",
                        "[2]Class 3, Total Semi-hard Metal Particle",
                        "[3]    3.1  SST + Fe base + FeO",
                        "[4]    3.2  Sn base Particle",
                        "[5]    3.3  PZT Particles",
                        "[6]Class 4, Total Soft-Metal Particle",
                        "[7]Class 5, Total Environment Particle",
                        "[8]    5.1  MgSiO Particle",
                        "[9]Class 6, Other Particle"


                */

                List<template_seagate_hpa_coverpage> sumOfHpas = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_TOTAL)).OrderBy(x => x.data_group).ToList();
                List<template_seagate_hpa_coverpage> hpas = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.HPA)).OrderBy(x => x.seq).ToList();

                string TotalSST = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_SUB_TOTAL) && x.B.Equals("Total SST")).FirstOrDefault().C;
                string Fe_based = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) && x.B.Equals("Fe based")).FirstOrDefault().C;
                string FeO = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) && x.B.Equals("FeO")).FirstOrDefault().C;
                string Sn_based = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) && x.B.Equals("Sn based")).FirstOrDefault().C;
                string PbZrTi = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) && x.B.Equals("PbZrTi")).FirstOrDefault().C;
                string MgSiO = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) && x.B.Equals("MgSiO")).FirstOrDefault().C;


                hpas[0].C = Convert.ToDouble(sumOfHpas[0].C).ToString("N1");//[0]
                hpas[1].C = Convert.ToDouble(sumOfHpas[1].C).ToString("N1");//[1]
                hpas[2].C = Convert.ToDouble(sumOfHpas[2].C).ToString("N1");//[2]
                hpas[3].C = Convert.ToDouble(Convert.ToDouble(TotalSST) + Convert.ToDouble(Fe_based) + Convert.ToDouble(FeO)).ToString("N1");//[3]
                hpas[4].C = Convert.ToDouble(Sn_based).ToString("N1");//[4]
                hpas[5].C = Convert.ToDouble(PbZrTi).ToString("N1");//[5]
                hpas[6].C = Convert.ToDouble(sumOfHpas[3].C).ToString("N1");//[6]
                hpas[7].C = Convert.ToDouble(sumOfHpas[4].C).ToString("N1");//[7]
                hpas[8].C = Convert.ToDouble(MgSiO).ToString("N1");//[8]
                hpas[9].C = Convert.ToDouble(sumOfHpas[5].C).ToString("N1");//[9]








                #endregion
            }
            gvWsClassification.DataSource = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_HEAD) ||
            x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) ||
            x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_SUB_TOTAL) ||
            x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_TOTAL) ||
            x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_GRAND_TOTAL)
            ).OrderBy(x => x.seq);
            gvWsClassification.DataBind();
            List<template_seagate_hpa_coverpage> listClass = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)).ToList();
            foreach (template_seagate_hpa_coverpage item in listClass)
            {
                item.C = Convert.ToDouble(item.C).ToString("N1") + "";
            }
            ;
            foreach (template_seagate_hpa_coverpage item in this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.HPA)).ToList())
            {
                item.C = item.C = (item.C == null) ? 0.ToString("N1") : Convert.ToDouble(item.C).ToString("N1") + "";

            }
            gvClassification.DataSource = listClass.OrderBy(x => x.seq);
            gvClassification.DataBind();
            gvHpa.DataSource = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.HPA)).OrderBy(x => x.seq);
            gvHpa.DataBind();
            #endregion

            #region  "Analysis Details"
            lbC144.Text = String.Format("{0:n2}", Convert.ToDouble(String.IsNullOrEmpty(txtB3.Text) ? "0" : txtB3.Text));
            lbC145.Text = String.Format("{0:n2}", Convert.ToDouble(String.IsNullOrEmpty(txtB4.Text) ? "0" : txtB4.Text));
            lbC146.Text = String.Format("{0:n2}", !CustomUtils.isNumber(txtB5.Text) ? Convert.ToDouble(txtB5.Text.Split(' ')[0]) : Convert.ToDouble(!CustomUtils.isNumber(txtB5.Text) ? "0" : txtB5.Text));
            lbC147.Text = String.Format("{0:n2}", Convert.ToDouble(String.IsNullOrEmpty(txtB6.Text) ? "0" : txtB6.Text));
            lbC148.Text = String.Format("{0:n2}", Convert.ToDouble(String.IsNullOrEmpty(txtB7.Text) ? "0" : txtB7.Text));
            lbC148_1.Text = String.Format("{0:n2}", Convert.ToDouble(String.IsNullOrEmpty(txtB8.Text) ? "0" : txtB8.Text));
            #endregion

        }

        #endregion


        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculateCas();
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            DataTable dtImgs = new DataTable("hpa_imgs");
            if (this.Hpas[0].img_path != null)
            {
                // Define all the columns once.
                DataColumn[] imgCol = { new DataColumn("img1", typeof(Byte[])) };
                dtImgs.Columns.AddRange(imgCol);
                DataRow imgRow = dtImgs.NewRow();
                imgRow["img1"] = CustomUtils.GetBytesFromImage(this.Hpas[0].img_path);
                dtImgs.Rows.Add(imgRow);
            }
            DataTable dtHeader = new DataTable("MethodProcedure");

            // Define all the columns once.
            DataColumn[] cols ={ new DataColumn("Analysis",typeof(String)),
                                  new DataColumn("ProcedureNo",typeof(String)),
                                  new DataColumn("NumOfPiecesUsedForExtraction",typeof(String)),
                                  new DataColumn("ExtractionMedium",typeof(String)),
                                  new DataColumn("ExtractionVolume",typeof(String)),
                              };
            dtHeader.Columns.AddRange(cols);
            DataRow row = dtHeader.NewRow();
            row["Analysis"] = String.Format("{0}\n,{1}", lbA20.Text, lbA20_1.Text);
            row["ProcedureNo"] = txtProcedureNo.Text;
            row["NumOfPiecesUsedForExtraction"] = txtNumberOfPieces.Text;
            dtHeader.Rows.Add(row);

            ReportHeader reportHeader = ReportHeader.getReportHeder(this.jobSample);


            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));
            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfTestComplete.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", String.Format("{0}\n,{1}", lbA20.Text, lbA20_1.Text)));
            reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
            reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.jobSample.singapore_ref_no) ? String.Empty : this.jobSample.singapore_ref_no)));



            reportParameters.Add(new ReportParameter("rpt_unit", ddlLiquidParticleUnit.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit2", ddlHardParticleAlalysisUnit.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit3", ddlClassificationUnit.SelectedItem.Text));
            //}
            DataTable dtSummary = new DataTable("Summary");
            DataColumn[] cols1 ={ new DataColumn("A",typeof(String)),
                                  new DataColumn("B",typeof(String)),
                                  new DataColumn("C",typeof(String)),
                              };
            dtSummary.Columns.AddRange(cols1);
            row = dtSummary.NewRow();
            row["A"] = "Analysis Details";
            row["B"] = "Swab print area (At, sq.mm.)";
            row["C"] = lbC144.Text;
            dtSummary.Rows.Add(row);
            row = dtSummary.NewRow();
            row["A"] = "";
            row["B"] = "Surface Area (cm2), C";
            row["C"] = lbC145.Text;
            dtSummary.Rows.Add(row);
            row = dtSummary.NewRow();
            row["A"] = "";
            row["B"] = "Number of Parts Extracted, N";
            row["C"] = lbC146.Text;
            dtSummary.Rows.Add(row);
            row = dtSummary.NewRow();
            row["A"] = "";
            row["B"] = "Area analyse of blank swab (sqmm), Ab";
            row["C"] = lbC147.Text;
            dtSummary.Rows.Add(row);
            row = dtSummary.NewRow();
            row["A"] = "";
            row["B"] = "Percent Area Coverage (%)";
            row["C"] = lbC148.Text;
            dtSummary.Rows.Add(row);
            row = dtSummary.NewRow();
            row["A"] = "";
            row["B"] = "Area analyse of sample swab (sqmm), As";
            row["C"] = lbC148_1.Text;
            dtSummary.Rows.Add(row);
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/hpa_seagate_swap.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtHeader)); // Add datasource here
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC03) && x.row_type.Value == Convert.ToInt32(RowTypeEnum.Normal)).OrderBy(x => x.seq).ToDataTable())); // Add datasource here
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.LPC06) && x.row_type.Value == Convert.ToInt32(RowTypeEnum.Normal)).OrderBy(x => x.seq).ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.HPA)).OrderBy(x => x.seq).ToDataTable())); // Add datasource here


            List<template_seagate_hpa_coverpage> ds5 = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)).OrderBy(x => x.seq).ToList();

            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", ds5.GetRange(0, 25).ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", ds5.GetRange(25, 25).ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet7", ds5.GetRange(50, ds5.Count - 50).ToDataTable())); // Add datasource here



            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet8", dtSummary)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet9", dtImgs)); // Add datasource here





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


        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {


            List<template_seagate_hpa_coverpage> _Hpas = new List<template_seagate_hpa_coverpage>();
            tb_m_specification tem = new tb_m_specification().SelectByID(int.Parse(ddlSpecification.SelectedValue));


            if (tem != null)
            {
                #region "Method"
                txtProcedureNo.Text = tem.D;
                txtNumberOfPieces.Text = tem.E;

                lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", tem.B, tem.A);

                #endregion
                #region "LPC"
                int cOrderSwap = 1;
                #region "Hard Particle Analysis"

                for (int i = 0; i < ANameKey.Length; i++)
                {
                    String _val = ANameKey[i];
                    template_seagate_hpa_coverpage _tmp = new template_seagate_hpa_coverpage();
                    _tmp.ID = CustomUtils.GetRandomNumberID();
                    _tmp.seq = (i + 1);
                    _tmp.A = _val;
                    switch (i)
                    {
                        case 0: _tmp.B = tem.E; break;// Class 1, Total Hard Particle
                        case 1: _tmp.B = tem.F; break;// Class 2, Total Magnetic Particle
                        case 2: _tmp.B = tem.G; break;// Class 3, Total Semi-hard Metal Particle
                        case 3: _tmp.B = tem.H; break;//3.1  SST + Fe base + FeO
                        case 4: _tmp.B = tem.I; break;//3.2  Sn base Particle
                        case 5: _tmp.B = tem.J; break;//3.3  PZT Particles
                        case 6: _tmp.B = tem.K; break;// Class 4, Total Soft-Metal Particle
                        case 7: _tmp.B = tem.L; break;// Class 5, Total Environment Particle
                        case 8: _tmp.B = tem.M; break;//5.1  MgSiO Particle
                        case 9: _tmp.B = tem.N; break;// Class 6, Other Particle
                        case 10: _tmp.B = tem.O; break;
                        case 11: _tmp.B = tem.P; break;
                    }
                    _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                    _tmp.hpa_type = Convert.ToInt32(GVTypeEnum.HPA);
                    _Hpas.Add(_tmp);

                }
                #endregion
                #region "Classification"
                _Hpas.AddRange(getTypesOfParticles(cOrderSwap));
                #endregion

                #endregion
                #region "Header Text"
                gvHpa.Columns[0].HeaderText = String.Format("Hard Particle Analysis({0})", String.Empty);
                gvHpa.Columns[1].HeaderText = String.Format("Specification Limit,({0})", tem.C);
                gvHpa.Columns[2].HeaderText = String.Format("Results,({0})", tem.C);
                #endregion
                #region "Datasource"
                this.Hpas = _Hpas;
                gvHpa.DataSource = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.HPA));
                gvHpa.DataBind();
                gvClassification.DataSource = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM));
                gvClassification.DataBind();
                #endregion

            }
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

        protected void gvHpa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_hpa_coverpage tmp = this.Hpas.Find(x => x.ID == PKID);
                if (tmp != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            tmp.row_type = Convert.ToInt32(RowTypeEnum.Hide);

                            break;
                        case RowTypeEnum.Normal:
                            tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvHpa.DataSource = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.HPA)).OrderBy(x => x.seq);
                    gvHpa.DataBind();
                }
            }
        }

        protected void gvHpa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvHpa.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvHpa.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvClassification_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_hpa_coverpage tmp = this.Hpas.Find(x => x.ID == PKID);
                if (tmp != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:
                            tmp.row_type = Convert.ToInt32(RowTypeEnum.Hide);

                            break;
                        case RowTypeEnum.Normal:
                            tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            break;
                    }
                    gvClassification.DataSource = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION)).OrderBy(x => x.seq);
                    gvClassification.DataBind();
                }
            }
        }

        protected void gvClassification_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvClassification.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvClassification.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvWsClassification_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal _litB = (Literal)e.Row.FindControl("litB");
                Label _litC = (Label)e.Row.FindControl("litC");
                Label _litD = (Label)e.Row.FindControl("litD");

                GVTypeEnum cmd = (GVTypeEnum)Enum.ToObject(typeof(GVTypeEnum), (int)gvWsClassification.DataKeys[e.Row.RowIndex].Values[2]);
                switch (cmd)
                {
                    case GVTypeEnum.CLASSIFICATION_GRAND_TOTAL:
                    case GVTypeEnum.CLASSIFICATION_TOTAL:
                        e.Row.BackColor = System.Drawing.Color.Orange;
                        _litD.Text = !CustomUtils.isNumber(_litC.Text) ? "0" : ((Convert.ToDouble(_litC.Text) <= 0) ? "0" : _litC.Text);

                        break;
                    case GVTypeEnum.CLASSIFICATION_SUB_TOTAL:
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                        _litD.Text = !CustomUtils.isNumber(_litC.Text) ? "0" : ((Convert.ToDouble(_litC.Text) <= 0) ? "0" : _litC.Text);

                        break;
                    case GVTypeEnum.CLASSIFICATION_ITEM:
                        _litB.Text = String.Format("{0}".PadRight(20, ' '), _litB.Text);
                        _litD.Text = !CustomUtils.isNumber(_litC.Text) ? "0" : ((Convert.ToDouble(_litC.Text) <= 0) ? "0" : _litC.Text);

                        break;
                }

            }
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
            //invDiv.RenderControl(h);
            string strHTMLContent = sw.GetStringBuilder().ToString();
            String html = "<html><header><style>body {max-width: 800px;margin:initial;font-family: \'Arial Unicode MS\';font-size: 10px;}table {border-collapse: collapse;}th {background: #666;color: #fff;border: 1px solid #999;padding: 0.5rem;text-align: center;}td { border: 1px solid #999;padding: 0.5rem;text-align: left;}h6 {font-weight:initial;}</style></header><body>" + strHTMLContent + "</body></html>";


            HttpContext.Current.Response.Write(html);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();


            gvHpa.Columns[0].HeaderText = String.Format("Hard Particle Analysis({0})", String.Empty);
            gvHpa.DataSource = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.HPA)).OrderBy(x => x.seq);
            gvHpa.DataBind();
            gvClassification.DataSource = this.Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION)).OrderBy(x => x.seq);
            gvClassification.DataBind();

        }

        protected void txtB4_TextChanged(object sender, EventArgs e)
        {
            txt_UsLPC06_B21.Text = txtB4.Text;
            txt_UsLPC03_B21.Text = txtB4.Text;
        }

        //protected void txtB8_TextChanged(object sender, EventArgs e)
        //{
        //    if (!String.IsNullOrEmpty(txtB8.Text) && !String.IsNullOrEmpty(txtB7.Text))
        //    {
        //        txtB8.Text = (Convert.ToDouble(txtB8.Text) / Convert.ToDouble(txtB7.Text) * 100).ToString();

        //    }
        //}



        private List<template_seagate_hpa_coverpage> getTypesOfParticles(int order)
        {
            List<template_seagate_hpa_coverpage> _Hpas = new List<template_seagate_hpa_coverpage>();

            List<String> items = new List<string>();

            /*
            # = Group
            - = Total
            $ = Grand Total
            -------------------------
            */

            items.Add("#Class 1 Particles,i.e.hard particles, etc.");
            items.Add("AlArO");
            items.Add("AlMgO");
            items.Add("AlO");
            items.Add("AlOTiC");
            items.Add("CrO");
            items.Add("NbB");
            items.Add("SiC");
            items.Add("SiO");
            items.Add("TiB");
            items.Add("TiC");
            items.Add("TiO");
            items.Add("TiSn");
            items.Add("TiV");
            items.Add("WC");
            items.Add("ZrC");
            items.Add("ZrO");
            items.Add("-Total Hard Particles");
            items.Add("#Class 2 Particles,i.e.magnetic particles");
            items.Add("Nd based");
            items.Add("Sm based");
            items.Add("Sr based");
            items.Add("-Total Magnetic Particles");
            items.Add("#Class 3 Particles,i.e.semi - hard metal particles");
            items.Add("SST300s with possible Si");
            items.Add("SST300s with possible Si and Mn");
            items.Add("SST400s with possible Si");
            items.Add("*Total SST");
            items.Add("Fe based");
            items.Add("FeO");
            items.Add("MnCrS");
            items.Add("Ni based");
            items.Add("NiP");
            items.Add("*Total Ni");
            items.Add("Sn based");
            items.Add("Ti based");
            items.Add("PbZrTi");
            items.Add("MgTiO");
            items.Add("SnPb");
            items.Add("AlSi (FeCrCuZnMn)");
            items.Add("CuZn");
            items.Add("FeMgSiO");
            items.Add("Pt based");
            items.Add("CrCoNiP (disc material)");
            items.Add("NiP with AlMgSi");
            items.Add("-Total Semi-Hard Metal Particles");
            items.Add("#Class 4 Particles,i.e.soft - metal particles");
            items.Add("Ag based");
            items.Add("Al based");
            items.Add("Au based");
            items.Add("Cu based");
            items.Add("Sb based");
            items.Add("Zn based");
            items.Add("Bi/SnBi");
            items.Add("NiFe");
            items.Add("ZnPFe");
            items.Add("NiPCr");
            items.Add("NiPCrFe");
            items.Add("AlFeO");
            items.Add("AlNiO");
            items.Add("Anodised Al");
            items.Add("Pb based");
            items.Add("AlSiO");
            items.Add("FeSiO");
            items.Add("-Total Soft Metal Particles");
            items.Add("#Class 5 Particles,i.e.environment particles");
            items.Add("Cl based");
            items.Add("MgCaO");
            items.Add("MgSiO");
            items.Add("CaSiO");
            items.Add("S based");
            items.Add("Ca based");
            items.Add("Na based");
            items.Add("K based");
            items.Add("F based");
            items.Add("-Total Environment Particles");
            items.Add("#Class 6 Particles,i.e.all other particle which not in the above classes");
            items.Add("Others");
            items.Add("-Total Others");
            items.Add("$Total Particles");

            String LastGroup = String.Empty;
            foreach (String item in items)
            {
                if (item.StartsWith("#"))
                {
                    LastGroup = item;
                }

                template_seagate_hpa_coverpage _tmp = new template_seagate_hpa_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = order;
                _tmp.A = LastGroup.Substring(1);
                _tmp.data_group = LastGroup.Substring(1);
                _tmp.B = item;
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                _tmp.hpa_type = (item.StartsWith("#")) ? Convert.ToInt32(GVTypeEnum.CLASSIFICATION_HEAD) :
                (item.StartsWith("-")) ? Convert.ToInt32(GVTypeEnum.CLASSIFICATION_TOTAL) :
                (item.StartsWith("$")) ? Convert.ToInt32(GVTypeEnum.CLASSIFICATION_GRAND_TOTAL) :
                (item.StartsWith("*")) ? Convert.ToInt32(GVTypeEnum.CLASSIFICATION_SUB_TOTAL) : Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM);
                if (_tmp.hpa_type != Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM))
                {
                    _tmp.B = item.Substring(1);
                }
                _Hpas.Add(_tmp);
                order++;
            }
            String dub = String.Empty;
            foreach (template_seagate_hpa_coverpage _cov in _Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)))
            {
                if (!_cov.A.Equals(dub))
                {
                    dub = _cov.A;
                }
                else
                {
                    _cov.A = string.Empty;
                }
            }

            return _Hpas;
        }

        private String mappingRawData(String _val)
        {
            String result = _val;
            Hashtable mappingValues = new Hashtable();
            //mappingValues["SST300s with possible Si"] = "SST300s (Fe/Cr/Ni)";
            mappingValues["SST300s with possible Si and Mn"] = "SST3xx with possible Si and/or Mn";
            //mappingValues["SST400s with possible Si"] = "SST400s (Fe/Cr)";


            //SST400s(Fe / Cr)



            foreach (DictionaryEntry entry in mappingValues)
            {
                if (entry.Key.Equals(_val))
                {
                    result = entry.Value.ToString();
                    break;
                }
            }
            return result;
        }


        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
            }
            else
            {

                tb_m_specification tem = new tb_m_specification().SelectByID(Convert.ToInt32(ddlSpecification.SelectedValue));
                if (tem != null)
                {
                    lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", tem.B + " " + tem.C, tem.A);
                }
            }

        }

        protected void ddlLiquidParticleUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateCas();
            ModolPopupExtender.Show();
        }

        protected void ddlHardParticleAlalysisUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

            gvHpa.Columns[1].HeaderText = String.Format("Specification Limit, ({0})", ddlHardParticleAlalysisUnit.SelectedItem.Text);
            gvHpa.Columns[2].HeaderText = String.Format("Results,({0})", ddlHardParticleAlalysisUnit.SelectedItem.Text);
            CalculateCas();
            ModolPopupExtender.Show();
        }
        protected void ddlClassificationUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvClassification.Columns[2].HeaderText = String.Format("Results, {0}", ddlClassificationUnit.SelectedItem.Text);
            CalculateCas();
            ModolPopupExtender.Show();

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        protected void txtB3_TextChanged(object sender, EventArgs e)
        {
            calPercentAreaCoverage();
        }

        private void calPercentAreaCoverage()
        {
            Double swabPrintArea = String.IsNullOrEmpty(txtB3.Text) ? 0 : Convert.ToDouble(txtB3.Text);
            Double areaAnalyseOfBlankSwab = String.IsNullOrEmpty(txtB6.Text) ? 0 : Convert.ToDouble(txtB6.Text);
            if (areaAnalyseOfBlankSwab != 0)
            {
                txtB7.Text = String.Format("{0}", ((areaAnalyseOfBlankSwab / swabPrintArea) * 100).ToString("N2"));
            }
            else
            {
                txtB7.Text = "0";
            }
        }
    }
}


