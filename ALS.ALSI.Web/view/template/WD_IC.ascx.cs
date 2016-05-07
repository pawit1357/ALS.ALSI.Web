using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
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
using ALS.ALSI.Biz.ReportObjects;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using WordToPDF;

namespace ALS.ALSI.Web.view.template
{
    public partial class WD_IC : System.Web.UI.UserControl
    {

        #region "Property"

        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
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

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public job_sample jobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
        }

        public List<template_wd_ic_coverpage> coverpages
        {
            get { return (List<template_wd_ic_coverpage>)Session[GetType().Name + "template_wd_ic_coverpage"]; }
            set { Session[GetType().Name + "template_wd_ic_coverpage"] = value; }
        }

        private void initialPage()
        {
            this.CommandName = CommandNameEnum.Add;

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

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt16(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF) + ""));


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
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_APPROVE), Convert.ToInt16(StatusEnum.SR_CHEMIST_APPROVE) + ""));
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_DISAPPROVE), Convert.ToInt16(StatusEnum.SR_CHEMIST_DISAPPROVE) + ""));
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
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_APPROVE), Convert.ToInt16(StatusEnum.LABMANAGER_APPROVE) + ""));
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_DISAPPROVE), Convert.ToInt16(StatusEnum.LABMANAGER_DISAPPROVE) + ""));
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

                #region "VISIBLE RESULT DATA"

                if (status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.SR_CHEMIST_CHECKING
               && userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST) || userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
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

                    txtProcedureNo.Enabled = true;
                    txtNumOfPiecesUsedForExtraction.Enabled = false;
                    txtExtractionMedium.Enabled = true;
                    txtExtractionVolume.Enabled = false;
                    gvAnionic.Columns[5].Visible = true;
                    gvCationic.Columns[5].Visible = true;
                    btnCoverPage.Visible = true;
                    btnWorking.Visible = true;
                }
                else
                {
                    txtProcedureNo.Enabled = false;
                    txtNumOfPiecesUsedForExtraction.Enabled = false;
                    txtExtractionMedium.Enabled = false;
                    txtExtractionVolume.Enabled = false;
                    gvAnionic.Columns[5].Visible = false;
                    gvCationic.Columns[5].Visible = false;
                    btnCoverPage.Visible = false;
                    btnWorking.Visible = false;
                }
                #endregion


            }
            #endregion

            #region "WORKING"
            this.coverpages = template_wd_ic_coverpage.FindAllBySampleID(this.SampleID);
            if (this.coverpages != null && this.coverpages.Count > 0)
            {
                this.CommandName = CommandNameEnum.Edit;
                template_wd_ic_coverpage ic = this.coverpages[0];
                /*METHOD/PROCEDURE:*/
                txtProcedureNo.Text = ic.ProcedureNo;
                txtNumOfPiecesUsedForExtraction.Text = ic.NumOfPiecesUsedForExtraction;
                txtExtractionMedium.Text = ic.ExtractionMedium;
                txtExtractionVolume.Text = (String.IsNullOrEmpty(ic.ExtractionVolume) ? String.Empty : String.Format("{0}ml", (1000 * Convert.ToDecimal(ic.ExtractionVolume))));

                ddlComponent.SelectedValue = ic.component_id.ToString();
                ddlDetailSpec.SelectedValue = ic.detail_spec_id.ToString();

                detailSpec = new tb_m_detail_spec().SelectByID(Convert.ToInt32(ic.detail_spec_id));
                if (detailSpec != null)
                {
                    /*RESULT*/
                    lbDocRev.Text = detailSpec.B;
                    lbDesc.Text = detailSpec.A;
                    txtB11.Text = ic.ExtractionVolume;
                    txtB12.Text = ic.b12;
                    txtB13.Text = ic.NumOfPiecesUsedForExtraction;

                    txtProcedureNo.Text = ic.ProcedureNo;
                    txtNumOfPiecesUsedForExtraction.Text = ic.NumOfPiecesUsedForExtraction;
                    txtExtractionMedium.Text = ic.ExtractionMedium;
                    txtExtractionVolume.Text = (String.IsNullOrEmpty(ic.ExtractionVolume) ? String.Empty : String.Format("{0}ml", (1000 * Convert.ToDecimal(ic.ExtractionVolume))));

                    if (ic.wunit != null)
                    {
                        ddlUnit.SelectedValue = ic.wunit.Value.ToString();

                        gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));

                        Label1.Text = ddlUnit.SelectedItem.Text;
                        Label2.Text = ddlUnit.SelectedItem.Text;
                        Label3.Text = ddlUnit.SelectedItem.Text;
                        Label4.Text = ddlUnit.SelectedItem.Text;
                        Label5.Text = ddlUnit.SelectedItem.Text;
                        Label6.Text = ddlUnit.SelectedItem.Text;
                        Label7.Text = ddlUnit.SelectedItem.Text;
                        Label8.Text = ddlUnit.SelectedItem.Text;

                    }
                    else
                    {
                        gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));

                        Label1.Text = ddlUnit.SelectedItem.Text;
                        Label2.Text = ddlUnit.SelectedItem.Text;
                        Label3.Text = ddlUnit.SelectedItem.Text;
                        Label4.Text = ddlUnit.SelectedItem.Text;
                        Label5.Text = ddlUnit.SelectedItem.Text;
                        Label6.Text = ddlUnit.SelectedItem.Text;
                        Label7.Text = ddlUnit.SelectedItem.Text;
                        Label8.Text = ddlUnit.SelectedItem.Text;
                    }


                    template_wd_ic_coverpage A17 = getCoverPageValue("Fluoride as F");
                    template_wd_ic_coverpage A18 = getCoverPageValue("Chloride as Cl");
                    template_wd_ic_coverpage A19 = getCoverPageValue("Bromide as Br");
                    template_wd_ic_coverpage A19_1 = getCoverPageValue("Nitrite as NO2");

                    template_wd_ic_coverpage A20 = getCoverPageValue("Nitrate as NO3");
                    template_wd_ic_coverpage A21 = getCoverPageValue("Sulfate as SO4");
                    template_wd_ic_coverpage A22 = getCoverPageValue("Phosphate as PO4");
                    template_wd_ic_coverpage A23 = getCoverPageValue("Total Anions");

                    template_wd_ic_coverpage C26 = getCoverPageValue("Lithium");
                    template_wd_ic_coverpage C27 = getCoverPageValue("Sodium");
                    template_wd_ic_coverpage C28 = getCoverPageValue("Ammonium");
                    template_wd_ic_coverpage C29 = getCoverPageValue("Potassium");
                    template_wd_ic_coverpage C30 = getCoverPageValue("Magnesium");
                    template_wd_ic_coverpage C31 = getCoverPageValue("Calcium");
                    template_wd_ic_coverpage C32 = getCoverPageValue("Total Cations");


                    #region "Fluoride, F"
                    txtB17.Text =   A17.wb;
                    txtC17.Text =   A17.wc;
                    txtD17.Text =   A17.wd;
                    lbAnE17.Text =  A17.we;
                    lbAnF17.Text =  A17.wf;
                    //lbAnG17.Text= A17.wg;
                    lbAnH17.Text =  A17.wh;
                    lbAnI17.Text =  A17.wi;
                    lbAnJ17.Text =  A17.wj;
                    #endregion
                    #region "Chloride, Cl"
                    txtB18.Text = A18.wb;
                    txtC18.Text = A18.wc;
                    txtD18.Text = A18.wd;
                    lbAnE18.Text = A18.we;
                    lbAnF18.Text = A18.wf;
                    //lbAnG18.TextA17.wg;
                    lbAnH18.Text = A18.wh;
                    lbAnI18.Text = A18.wi;
                    lbAnJ18.Text = A18.wj;
                    #endregion

                    #region "Nitrite as NO2"
                    if (A19_1 != null)
                    {
                        txtB19_1.Text = A19_1.wb;
                        txtC19_1.Text = A19_1.wc;
                        txtD19_1.Text = A19_1.wd;
                        txtAnE19.Text = A19_1.we;
                        txtAnF19.Text = A19_1.wf;
                        //lbAnG18.TextA17.wg;
                        txtAnH19.Text = A19_1.wh;
                        txtAnI19.Text = A19_1.wi;
                        txtAnJ19.Text = A19_1.wj;
                    }
                    #endregion


                    #region "Bromide as Br"
                    txtB19.Text = A19.wb;
                    txtC19.Text = A19.wc;
                    txtD19.Text = A19.wd;
                    lbAnE19.Text = A19.we;
                    lbAnF19.Text = A19.wf;
                    //lbAnG19.TextA17.wg;
                    lbAnH19.Text = A19.wh;
                    lbAnI19.Text = A19.wi;
                    lbAnJ19.Text = A19.wj;
                    #endregion
                    #region "Nitrate as NO3"
                    txtB20.Text = A20.wb;
                    txtC20.Text = A20.wc;
                    txtD20.Text = A20.wd;
                    lbAnE20.Text = A20.we;
                    lbAnF20.Text = A20.wf;
                    //lbAnG20.TextA17.wg;
                    lbAnH20.Text = A20.wh;
                    lbAnI20.Text = A20.wi;
                    lbAnJ20.Text = A20.wj;
                    #endregion
                    #region "Sulfate as SO4"
                    txtB21.Text = A21.wb;
                    txtC21.Text = A21.wc;
                    txtD21.Text = A21.wd;
                    lbAnE21.Text = A21.we;
                    lbAnF21.Text = A21.wf;
                    //lbAnG21.TextA17.wg;
                    lbAnH21.Text = A21.wh;
                    lbAnI21.Text = A21.wi;
                    lbAnJ21.Text = A21.wj;
                    #endregion
                    #region "Phosphate as PO4"
                    txtB22.Text = A22.wb;
                    txtC22.Text = A22.wc;
                    txtD22.Text = A22.wd;
                    lbAnE22.Text = A22.we;
                    lbAnF22.Text = A22.wf;
                    //lbAnG22.TextA17.wg;
                    lbAnH22.Text = A22.wh;
                    lbAnI22.Text = A22.wi;
                    lbAnJ22.Text = A22.wj;
                    #endregion
                    #region "Total"
                    lbAnI23.Text = A23.wi;
                    lbAnJ23.Text = A23.wj;
                    #endregion
                    //-------------
                    #region "FLithium as Li"
                    txtB26.Text = C26.wb;
                    txtC26.Text = C26.wc;
                    txtD26.Text = C26.wd;
                    lbAnE26.Text = C26.we;
                    lbAnF26.Text = C26.wf;
                    lbAnH26.Text = C26.wh;
                    lbAnI26.Text = C26.wi;
                    lbAnJ26.Text = C26.wj;
                    #endregion
                    #region "FSodium as Na"
                    txtB27.Text = C27.wb;
                    txtC27.Text = C27.wc;
                    txtD27.Text = C27.wd;
                    lbAnE27.Text = C27.we;
                    lbAnF27.Text = C27.wf;
                    lbAnH27.Text = C27.wh;
                    lbAnI27.Text = C27.wi;
                    lbAnJ27.Text = C27.wj;
                    #endregion
                    #region "FAmmonium as NH4"
                    txtB28.Text = C28.wb;
                    txtC28.Text = C28.wc;
                    txtD28.Text = C28.wd;
                    lbAnE28.Text = C28.we;
                    lbAnF28.Text = C28.wf;
                    lbAnH28.Text = C28.wh;
                    lbAnI28.Text = C28.wi;
                    lbAnJ28.Text = C28.wj;
                    #endregion
                    #region "FPotassium as K"
                    txtB29.Text = C29.wb;
                    txtC29.Text = C29.wc;
                    txtD29.Text = C29.wd;
                    lbAnE29.Text = C29.we;
                    lbAnF29.Text = C29.wf;
                    lbAnH29.Text = C29.wh;
                    lbAnI29.Text = C29.wi;
                    lbAnJ29.Text = C29.wj;
                    #endregion
                    #region "FMagnesium as Mg"
                    txtB30.Text = C30.wb;
                    txtC30.Text = C30.wc;
                    txtD30.Text = C30.wd;
                    lbAnE30.Text = C30.we;
                    lbAnF30.Text = C30.wf;
                    lbAnH30.Text = C30.wh;
                    lbAnI30.Text = C30.wi;
                    lbAnJ30.Text = C30.wj;
                    #endregion
                    #region "FCalcium as Ca"
                    txtB31.Text = C31.wb;
                    txtC31.Text = C31.wc;
                    txtD31.Text = C31.wd;
                    lbAnE31.Text = C31.we;
                    lbAnF31.Text = C31.wf;
                    lbAnH31.Text = C31.wh;
                    lbAnI31.Text = C31.wi;
                    lbAnJ31.Text = C31.wj;
                    #endregion
                    #region "FTotal"
                    lbAnI32.Text = C32.wi;
                    lbAnJ32.Text = C32.wj;
                    #endregion


                    calculateByFormular();
                }
                gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
                gvAnionic.DataBind();
                gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
                gvCationic.DataBind();
            }
            else
            {
                this.coverpages = new List<template_wd_ic_coverpage>();
            }

            #endregion



            //Disable Save button
            btnCoverPage.CssClass = "btn blue";
            btnWorking.CssClass = "btn green";
            pCoverpage.Visible = true;
            pWorkingIC.Visible = false;

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
            calculateByFormular();
            Boolean isValid = true;
            template_wd_ic_coverpage objWork = new template_wd_ic_coverpage();
            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    this.jobSample.step1owner = userLogin.id;

                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.jobSample.step2owner = userLogin.id;

                    foreach (template_wd_ic_coverpage _cover in this.coverpages)
                    {
                        _cover.sample_id = this.SampleID;
                        _cover.detail_spec_id = Convert.ToInt32(ddlDetailSpec.SelectedValue);
                        _cover.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        _cover.b12 = txtB12.Text;
                        _cover.ProcedureNo = txtProcedureNo.Text;
                        _cover.NumOfPiecesUsedForExtraction = txtB13.Text;
                        _cover.ExtractionMedium = txtExtractionMedium.Text;
                        _cover.ExtractionVolume = txtB11.Text;
                    }
                    switch (this.CommandName)
                    {
                        case CommandNameEnum.Add:
                            template_wd_ic_coverpage.InsertList(this.coverpages);
                            break;
                        case CommandNameEnum.Edit:
                            template_wd_ic_coverpage.UpdateList(this.coverpages);
                            break;
                    }
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;

                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    //#endregion
                    foreach (template_wd_ic_coverpage _cover in this.coverpages)
                    {
                        _cover.sample_id = this.SampleID;
                        _cover.detail_spec_id = Convert.ToInt32(ddlDetailSpec.SelectedValue);
                        _cover.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        _cover.b12 = txtB12.Text;
                        _cover.ProcedureNo = txtProcedureNo.Text;
                        _cover.NumOfPiecesUsedForExtraction = txtB13.Text;
                        _cover.ExtractionMedium = txtExtractionMedium.Text;
                        _cover.ExtractionVolume = txtB11.Text;
                        _cover.wunit = Convert.ToInt32(ddlUnit.SelectedValue);
                    }
                    template_wd_ic_coverpage.UpdateList(this.coverpages);
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
                    if (btnUpload.HasFile && (Path.GetExtension(btnUpload.FileName).Equals(".doc") || Path.GetExtension(btnUpload.FileName).Equals(".docx")))
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
            //########
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();
            }
            else
            {
                litErrorMessage.Text = String.Empty;
                this.jobSample.Update();
                //Commit
                GeneralManager.Commit();

                //removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }


        }

        protected void btnCalulate_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            calculateByFormular();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.PreviousPath);
        }

        protected void btnCoverPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnCoverPage.CssClass = "btn green";
                    btnWorking.CssClass = "btn blue";
                    pCoverpage.Visible = true;
                    pWorkingIC.Visible = false;

                    txtNumOfPiecesUsedForExtraction.Text = txtB13.Text;
                    txtExtractionVolume.Text = (String.IsNullOrEmpty(txtB11.Text) ? String.Empty : String.Format("{0}ml", (1000 * Convert.ToDecimal(txtB11.Text))));
                    calculateByFormular();
                    break;
                case "Workingpg-IC":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorking.CssClass = "btn green";
                    pCoverpage.Visible = false;
                    pWorkingIC.Visible = true;
                    break;
            }
        }


        protected DataFormatter dataFormatter;


        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            string sheetName = string.Empty;

            for (int i = 0; i < FileUpload1.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = FileUpload1.PostedFiles[i];
                try
                {
                    if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }

                        _postedFile.SaveAs(source_file);

                        using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                        {
                            HSSFWorkbook wb = new HSSFWorkbook(fs);
                            ISheet isheet = wb.GetSheet(ConfigurationManager.AppSettings["wd.ic.excel.sheetname.working1"]);
                            if (isheet == null)
                            {
                                errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["wd.ic.excel.sheetname.working1"]));
                            }
                            else
                            {
                                sheetName = isheet.SheetName;

                                txtB11.Text = CustomUtils.GetCellValue(isheet.GetRow(11 - 1).GetCell(ExcelColumn.B));
                                txtB12.Text = CustomUtils.GetCellValue(isheet.GetRow(12 - 1).GetCell(ExcelColumn.B));
                                txtB13.Text = CustomUtils.GetCellValue(isheet.GetRow(13 - 1).GetCell(ExcelColumn.B));

                                #region "Fluoride, F"
                                txtB17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.B));
                                txtC17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.C));
                                txtD17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.D));
                                lbAnE17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.E));
                                lbAnF17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnH17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.H));
                                lbAnI17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.I));
                                lbAnJ17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.J));


                                //Set Decimal
                                txtB17.Text = Math.Round(Convert.ToDecimal(txtB17.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC17.Text = Math.Round(Convert.ToDecimal(txtC17.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD17.Text = Math.Round(Convert.ToDecimal(txtD17.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE17.Text = Math.Round(Convert.ToDecimal(lbAnE17.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF17.Text = Math.Round(Convert.ToDecimal(lbAnF17.Text), Convert.ToInt16(txtDecimal05.Text)) + "";

                                lbAnH17.Text = Math.Round(Convert.ToDecimal(lbAnH17.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI17.Text = lbAnI17.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI17.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ17.Text = Math.Round(Convert.ToDecimal(lbAnJ17.Text), Convert.ToInt16(txtDecimal09.Text)) + "";





                                #endregion
                                #region "Chloride, Cl"
                                txtB18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.B));
                                txtC18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.C));
                                txtD18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.D));
                                lbAnE18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.E));
                                lbAnF18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.F));
                                //lbAnG18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.G));
                                lbAnH18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.H));
                                lbAnI18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.I));
                                lbAnJ18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB18.Text = Math.Round(Convert.ToDecimal(txtB18.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC18.Text = Math.Round(Convert.ToDecimal(txtC18.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD18.Text = Math.Round(Convert.ToDecimal(txtD18.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE18.Text = Math.Round(Convert.ToDecimal(lbAnE18.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF18.Text = Math.Round(Convert.ToDecimal(lbAnF18.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH18.Text = Math.Round(Convert.ToDecimal(lbAnH18.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI18.Text = lbAnI18.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI18.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ18.Text = Math.Round(Convert.ToDecimal(lbAnJ18.Text), Convert.ToInt16(txtDecimal09.Text)) + "";

                                #endregion
                                #region "Nitrite as NO2"
                                txtB19_1.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.B));
                                txtC19_1.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.C));
                                txtD19_1.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.D));
                                txtAnE19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.E));
                                txtAnF19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.F));
                                //lbAnG18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.G));
                                txtAnH19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.H));
                                txtAnI19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.I));
                                txtAnJ19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB19_1.Text = Math.Round(Convert.ToDecimal(txtB19_1.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC19_1.Text = Math.Round(Convert.ToDecimal(txtC19_1.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD19_1.Text = Math.Round(Convert.ToDecimal(txtD19_1.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                txtAnE19.Text = Math.Round(Convert.ToDecimal(txtAnE19.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                txtAnF19.Text = Math.Round(Convert.ToDecimal(txtAnF19.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                txtAnH19.Text = Math.Round(Convert.ToDecimal(txtAnH19.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                txtAnI19.Text = lbAnI19.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(txtAnI19.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                txtAnJ19.Text = Math.Round(Convert.ToDecimal(txtAnJ19.Text), Convert.ToInt16(txtDecimal09.Text)) + "";

                                #endregion





                                #region "Bromide as Br"
                                txtB19.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.B));
                                txtC19.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.C));
                                txtD19.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.D));
                                lbAnE19.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.E));
                                lbAnF19.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.F));
                                //lbAnG19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.G));
                                lbAnH19.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.H));
                                lbAnI19.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.I));
                                lbAnJ19.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB19.Text = Math.Round(Convert.ToDecimal(txtB19.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC19.Text = Math.Round(Convert.ToDecimal(txtC19.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD19.Text = Math.Round(Convert.ToDecimal(txtD19.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE19.Text = Math.Round(Convert.ToDecimal(lbAnE19.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF19.Text = Math.Round(Convert.ToDecimal(lbAnF19.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH19.Text = Math.Round(Convert.ToDecimal(lbAnH19.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI19.Text = lbAnI19.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI19.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ19.Text = Math.Round(Convert.ToDecimal(lbAnJ19.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Nitrate as NO3"
                                txtB20.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.B));
                                txtC20.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.C));
                                txtD20.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.D));
                                lbAnE20.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.E));
                                lbAnF20.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.F));
                                //lbAnG20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.G));
                                lbAnH20.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.H));
                                lbAnI20.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.I));
                                lbAnJ20.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB20.Text = Math.Round(Convert.ToDecimal(txtB20.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC20.Text = Math.Round(Convert.ToDecimal(txtC20.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD20.Text = Math.Round(Convert.ToDecimal(txtD20.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE20.Text = Math.Round(Convert.ToDecimal(lbAnE20.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF20.Text = Math.Round(Convert.ToDecimal(lbAnF20.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH20.Text = Math.Round(Convert.ToDecimal(lbAnH20.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI20.Text = lbAnI20.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI20.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ20.Text = Math.Round(Convert.ToDecimal(lbAnJ20.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Sulfate as SO4"
                                txtB21.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.B));
                                txtC21.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.C));
                                txtD21.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.D));
                                lbAnE21.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.E));
                                lbAnF21.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.F));
                                //lbAnG21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.G));
                                lbAnH21.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.H));
                                lbAnI21.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.I));
                                lbAnJ21.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB21.Text = Math.Round(Convert.ToDecimal(txtB21.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC21.Text = Math.Round(Convert.ToDecimal(txtC21.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD21.Text = Math.Round(Convert.ToDecimal(txtD21.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE21.Text = Math.Round(Convert.ToDecimal(lbAnE21.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF21.Text = Math.Round(Convert.ToDecimal(lbAnF21.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH21.Text = Math.Round(Convert.ToDecimal(lbAnH21.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI21.Text = lbAnI21.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI21.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ21.Text = Math.Round(Convert.ToDecimal(lbAnJ21.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Phosphate as PO4"
                                txtB22.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.B));
                                txtC22.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.C));
                                txtD22.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.D));
                                lbAnE22.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.E));
                                lbAnF22.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.F));
                                //lbAnG22.Text = CustomUtils.GetCellValue(isheet.GetRow(22 - 1).GetCell(ExcelColumn.G));
                                lbAnH22.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.H));
                                lbAnI22.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.I));
                                lbAnJ22.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB22.Text = Math.Round(Convert.ToDecimal(txtB22.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC22.Text = Math.Round(Convert.ToDecimal(txtC22.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD22.Text = Math.Round(Convert.ToDecimal(txtD22.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE22.Text = Math.Round(Convert.ToDecimal(lbAnE22.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF22.Text = Math.Round(Convert.ToDecimal(lbAnF22.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH22.Text = Math.Round(Convert.ToDecimal(lbAnH22.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI22.Text = lbAnI22.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI22.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ22.Text = Math.Round(Convert.ToDecimal(lbAnJ22.Text), Convert.ToInt16(txtDecimal09.Text)) + "";

                                #endregion
                                #region "Total"
                                lbAnI23.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.I));
                                lbAnJ23.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.J));
                                lbAnI23.Text = lbAnI23.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI23.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ23.Text = Math.Round(Convert.ToDecimal(lbAnJ23.Text), Convert.ToInt16(txtDecimal09.Text)) + "";

                                #endregion
                                //-------------
                                #region "FLithium as Li"
                                txtB26.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.B));
                                txtC26.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.C));
                                txtD26.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.D));
                                lbAnE26.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.E));
                                lbAnF26.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.F));

                                lbAnH26.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.H));
                                lbAnI26.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.I));
                                lbAnJ26.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB26.Text = Math.Round(Convert.ToDecimal(txtB26.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC26.Text = Math.Round(Convert.ToDecimal(txtC26.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD26.Text = Math.Round(Convert.ToDecimal(txtD26.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE26.Text = Math.Round(Convert.ToDecimal(lbAnE26.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF26.Text = Math.Round(Convert.ToDecimal(lbAnF26.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH26.Text = Math.Round(Convert.ToDecimal(lbAnH26.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI26.Text = lbAnI26.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI26.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ26.Text = Math.Round(Convert.ToDecimal(lbAnJ26.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "FSodium as Na"
                                txtB27.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.B));
                                txtC27.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.C));
                                txtD27.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.D));
                                lbAnE27.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.E));
                                lbAnF27.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.F));

                                lbAnH27.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.H));
                                lbAnI27.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.I));
                                lbAnJ27.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB27.Text = Math.Round(Convert.ToDecimal(txtB27.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC27.Text = Math.Round(Convert.ToDecimal(txtC27.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD27.Text = Math.Round(Convert.ToDecimal(txtD27.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE27.Text = Math.Round(Convert.ToDecimal(lbAnE27.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF27.Text = Math.Round(Convert.ToDecimal(lbAnF27.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH27.Text = Math.Round(Convert.ToDecimal(lbAnH27.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI27.Text = lbAnI27.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI27.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ27.Text = Math.Round(Convert.ToDecimal(lbAnJ27.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "FAmmonium as NH4"
                                txtB28.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.B));
                                txtC28.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.C));
                                txtD28.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.D));
                                lbAnE28.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.E));
                                lbAnF28.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.F));

                                lbAnH28.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.H));
                                lbAnI28.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.I));
                                lbAnJ28.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB28.Text = Math.Round(Convert.ToDecimal(txtB28.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC28.Text = Math.Round(Convert.ToDecimal(txtC28.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD28.Text = Math.Round(Convert.ToDecimal(txtD28.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE28.Text = Math.Round(Convert.ToDecimal(lbAnE28.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF28.Text = Math.Round(Convert.ToDecimal(lbAnF28.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH28.Text = Math.Round(Convert.ToDecimal(lbAnH28.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI28.Text = lbAnI28.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI28.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ28.Text = Math.Round(Convert.ToDecimal(lbAnJ28.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "FPotassium as K"
                                txtB29.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.B));
                                txtC29.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.C));
                                txtD29.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.D));
                                lbAnE29.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.E));
                                lbAnF29.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.F));

                                lbAnH29.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.H));
                                lbAnI29.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.I));
                                lbAnJ29.Text = CustomUtils.GetCellValue(isheet.GetRow(30 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB29.Text = Math.Round(Convert.ToDecimal(txtB29.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC29.Text = Math.Round(Convert.ToDecimal(txtC29.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD29.Text = Math.Round(Convert.ToDecimal(txtD29.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE29.Text = Math.Round(Convert.ToDecimal(lbAnE29.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF29.Text = Math.Round(Convert.ToDecimal(lbAnF29.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH29.Text = Math.Round(Convert.ToDecimal(lbAnH29.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI29.Text = lbAnI29.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI29.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ29.Text = Math.Round(Convert.ToDecimal(lbAnJ29.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "FMagnesium as Mg"
                                txtB30.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.B));
                                txtC30.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.C));
                                txtD30.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.D));
                                lbAnE30.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.E));
                                lbAnF30.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.F));

                                lbAnH30.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.H));
                                lbAnI30.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.I));
                                lbAnJ30.Text = CustomUtils.GetCellValue(isheet.GetRow(31 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB30.Text = Math.Round(Convert.ToDecimal(txtB30.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC30.Text = Math.Round(Convert.ToDecimal(txtC30.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD30.Text = Math.Round(Convert.ToDecimal(txtD30.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE30.Text = Math.Round(Convert.ToDecimal(lbAnE30.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF30.Text = Math.Round(Convert.ToDecimal(lbAnF30.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH30.Text = Math.Round(Convert.ToDecimal(lbAnH30.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI30.Text = lbAnI30.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI30.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ30.Text = Math.Round(Convert.ToDecimal(lbAnJ30.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "FCalcium as Ca"
                                txtB31.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.B));
                                txtC31.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.C));
                                txtD31.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.D));
                                lbAnE31.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.E));
                                lbAnF31.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.F));

                                lbAnH31.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.H));
                                lbAnI31.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.I));
                                lbAnJ31.Text = CustomUtils.GetCellValue(isheet.GetRow(32 - 1).GetCell(ExcelColumn.J));

                                //Set Decimal
                                txtB31.Text = Math.Round(Convert.ToDecimal(txtB31.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC31.Text = Math.Round(Convert.ToDecimal(txtC31.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD31.Text = Math.Round(Convert.ToDecimal(txtD31.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE31.Text = Math.Round(Convert.ToDecimal(lbAnE31.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnF31.Text = Math.Round(Convert.ToDecimal(lbAnF31.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH31.Text = Math.Round(Convert.ToDecimal(lbAnH31.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI31.Text = lbAnI31.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI31.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ31.Text = Math.Round(Convert.ToDecimal(lbAnJ31.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "FTotal"
                                lbAnI32.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.I));
                                lbAnJ32.Text = CustomUtils.GetCellValue(isheet.GetRow(33 - 1).GetCell(ExcelColumn.J));

                                lbAnI32.Text = lbAnI32.Text.Equals("Not Detected") ? "Not Detected" : Math.Round(Convert.ToDecimal(lbAnI32.Text), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ32.Text = Math.Round(Convert.ToDecimal(lbAnJ32.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion

                            }

                        }
                    }
                    else
                    {
                        errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));
                    }
                }
                catch (Exception ex)
                {
                    errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", sheetName, CustomUtils.ErrorIndex));
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
            }
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {



            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);

            List<template_wd_ic_coverpage> anionic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            List<template_wd_ic_coverpage> cationic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();

            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2)); reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", "IC"));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The specification is based on Western Digital's document no. {0} for {1}", lbDocRev.Text, lbDesc.Text)));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            foreach (template_wd_ic_coverpage a in anionic)
            {
                a.B = CustomUtils.isNumber(a.B) ? "<"+a.B : a.B;
            }
            foreach (template_wd_ic_coverpage b in cationic)
            {
                b.B = CustomUtils.isNumber(b.B) ? "<" + b.B : b.B;
            }
            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ic_wd.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", anionic.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", cationic.ToDataTable())); // Add datasource here



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

                        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
                        Response.BinaryWrite(bytes); // create the file
                        Response.Flush(); // send it to the client to download

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

        protected void ddlDetailSpec_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_detail_spec tem = new tb_m_detail_spec().SelectByID(int.Parse(ddlDetailSpec.SelectedValue));
            if (tem != null)
            {
                /*RESULT*/
                lbDocRev.Text = tem.B;
                lbDesc.Text = tem.A;
                gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", tem.C);
                gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", tem.C);
                gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", tem.C);
                gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", tem.C);
                gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", tem.C);
                gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", tem.C);

                List<template_wd_ic_coverpage> listCover = new List<template_wd_ic_coverpage>();
                #region "*Anionic*"
                template_wd_ic_coverpage _tmp = new template_wd_ic_coverpage();
                _tmp.id = 1;
                _tmp.A = "Fluoride as F";
                _tmp.B = tem.D;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 2;
                _tmp.A = "Chloride as Cl";
                _tmp.B = tem.E;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);

                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 3;
                _tmp.A = "Nitrite as NO2";
                _tmp.B = tem.F;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);

                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 4;
                _tmp.A = "Bromide as Br";
                _tmp.B = tem.G;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 5;
                _tmp.A = "Nitrate as NO3";
                _tmp.B = tem.H;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 6;
                _tmp.A = "Sulfate as SO4";
                _tmp.B = tem.I;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 7;
                _tmp.A = "Phosphate as PO4";
                _tmp.B = tem.J;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 8;
                _tmp.A = "Total Anions";
                _tmp.B = tem.K;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                #endregion
                #region "*Cationic*"

                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 8;
                _tmp.A = "Lithium as Li";
                _tmp.B = tem.M;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 9;
                _tmp.A = "Sodium as Na";
                _tmp.B = tem.P;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 10;
                _tmp.A = "Ammonium as NH4";
                _tmp.B = tem.L;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 11;
                _tmp.A = "Potassium as K";
                _tmp.B = tem.O;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 12;
                _tmp.A = "Magnesium as Mg";
                _tmp.B = tem.Q;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 13;
                _tmp.A = "Calcium as Ca";
                _tmp.B = tem.N;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_wd_ic_coverpage();
                _tmp.id = 14;
                _tmp.A = "Total Cations";
                _tmp.B = tem.R;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                #endregion
                this.coverpages = listCover;
                gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
                gvAnionic.DataBind();
                gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
                gvCationic.DataBind();

                btnSubmit.Enabled = true;
            }
        }

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_component comp = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (comp != null)
            {
                txtProcedureNo.Text = comp.B;
                txtNumOfPiecesUsedForExtraction.Text = comp.E;
                txtExtractionMedium.Text = comp.F;
                txtExtractionVolume.Text = comp.G;

                btnSubmit.Enabled = true;

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

        protected void gvAnionic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvAnionic.DataKeys[e.Row.RowIndex].Values[0].ToString());

                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvAnionic.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                Literal litSpecificationLimits = (Literal)e.Row.FindControl("litSpecificationLimits");

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
                    litSpecificationLimits.Text = litSpecificationLimits.Text.Equals("NA") ? litSpecificationLimits.Text : String.Format("<{0}", litSpecificationLimits.Text);
                }
            }
        }

        protected void gvCationic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvCationic.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCationic.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                Literal litSpecificationLimits = (Literal)e.Row.FindControl("litSpecificationLimits");
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
                    litSpecificationLimits.Text = litSpecificationLimits.Text.Equals("NA") ? litSpecificationLimits.Text : String.Format("<{0}", litSpecificationLimits.Text);

                }
            }
        }

        protected void gvAnionic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_wd_ic_coverpage gcms = this.coverpages.Find(x => x.id == PKID);
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
                    gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
                    gvAnionic.DataBind();
                }
            }
        }

        protected void gvCationic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_wd_ic_coverpage gcms = this.coverpages.Find(x => x.id == PKID);
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
                    gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
                    gvCationic.DataBind();
                }
            }
        }

        ////
        private String getValueByPeakName(ISheet _isIntegration, String _peakName)
        {
            Double result = 0;
            for (int i = 0; i < _isIntegration.LastRowNum; i++)
            {
                if (_isIntegration.GetRow(i) != null)
                {
                    String pkName = CustomUtils.GetCellValue(_isIntegration.GetRow(i).GetCell(2));
                    if (pkName.Equals(_peakName))
                    {
                        result = Convert.ToDouble(CustomUtils.GetCellValue(_isIntegration.GetRow(i).GetCell(6)));
                        break;
                    }
                }
            }

            return String.Format("{0:n4}", Math.Round(result, 4));
        }
        ////
        private void setCoverPageValue(String _peakname, String _wb, String _wc, String _wd, String _we, String _wf, String _wg, String _wh, String _wi, String _wj)
        {
            template_wd_ic_coverpage _val = this.coverpages.Where(x => x.A.StartsWith(_peakname)).FirstOrDefault();
            if (_val != null)
            {
                _val.wb = _wb;
                _val.wc = _wc;
                _val.wd = _wd;
                _val.we = _we;
                _val.wf = String.IsNullOrEmpty(_wf) ? "0" : CustomUtils.isNumber(_wf) ? Math.Round(Convert.ToDouble(_wf), 2) + "" : String.Empty;
                _val.wg = _wg;
                _val.wg = _wh;
                _val.wi = _wi;
                _val.wj = String.IsNullOrEmpty(_wj) ? "0" : _wj;
                //_val.wunit = _k;//unit
                if (!String.IsNullOrEmpty(_val.B)){
                    _val.E = (_val.B.Equals("NA") ? "NA" : (CustomUtils.isNumber(_wf) ? Convert.ToDouble(_wj) : 0) < Convert.ToDouble(_val.B) ? "PASS" : "FAIL");
                }
            }
        }

        private template_wd_ic_coverpage getCoverPageValue(String _peakname)
        {
            return this.coverpages.Where(x => x.A.StartsWith(_peakname)).FirstOrDefault();
        }

        //private String getUnitText(String unit)
        //{
        //    String result = String.Empty;
        //    if (!String.IsNullOrEmpty(unit))
        //    {
        //        if (unit.Equals("1"))
        //        {
        //            result = "ug";
        //        }
        //        else if (unit.Equals("1000"))
        //        {
        //            result = "ng";
        //        }
        //        else
        //        {
        //            result = "mg";
        //        }
        //    }
        //    return result;
        //}

        private void calculateByFormular()
        {

            Decimal b11 = Convert.ToDecimal(String.IsNullOrEmpty(txtB11.Text) ? "0" : txtB11.Text);
            Decimal b12 = Convert.ToDecimal(String.IsNullOrEmpty(txtB12.Text) ? "0" : txtB12.Text);
            Decimal b13 = Convert.ToDecimal(String.IsNullOrEmpty(txtB13.Text) ? "0" : txtB13.Text);

            if (b11 != 0 && b12 != 0 && b13 != 0)
            {

                setCoverPageValue("Fluoride as F", txtB17.Text, txtC17.Text, txtD17.Text, lbAnE17.Text, lbAnF17.Text, "", lbAnH17.Text, lbAnI17.Text, lbAnJ17.Text);
                setCoverPageValue("Chloride as Cl", txtB18.Text, txtC18.Text, txtD18.Text, lbAnE18.Text, lbAnF18.Text, "", lbAnH18.Text, lbAnI18.Text, lbAnJ18.Text);
                setCoverPageValue("Bromide as Br", txtB19.Text, txtC19.Text, txtD19.Text, lbAnE19.Text, lbAnF19.Text, "", lbAnH19.Text, lbAnI19.Text, lbAnJ19.Text);
                setCoverPageValue("Nitrite as NO2", txtB19_1.Text, txtC19_1.Text, txtD19_1.Text, txtAnE19.Text, txtAnF19.Text, "", txtAnH19.Text, txtAnI19.Text, txtAnJ19.Text);
                setCoverPageValue("Nitrate as NO3", txtB20.Text, txtC20.Text, txtD20.Text, lbAnE20.Text, lbAnF20.Text, "", lbAnH20.Text, lbAnI20.Text, lbAnJ20.Text);
                setCoverPageValue("Sulfate as SO4", txtB21.Text, txtC21.Text, txtD21.Text, lbAnE21.Text, lbAnF21.Text, "", lbAnH21.Text, lbAnI21.Text, lbAnJ21.Text);
                setCoverPageValue("Phosphate as PO4", txtB22.Text, txtC22.Text, txtD22.Text, lbAnE22.Text, lbAnF22.Text, "", lbAnH22.Text, lbAnI22.Text, lbAnJ22.Text);
                setCoverPageValue("Total Anions","", "", "", "", "", "", "", lbAnI23.Text, lbAnJ23.Text);
                setCoverPageValue("Lithium", txtB26.Text, txtC26.Text, txtD26.Text, lbAnE26.Text, lbAnF26.Text, "", lbAnH26.Text, lbAnI26.Text, lbAnJ26.Text);
                setCoverPageValue("Sodium", txtB27.Text, txtC27.Text, txtD27.Text, lbAnE27.Text, lbAnF27.Text, "", lbAnH27.Text, lbAnI27.Text, lbAnJ27.Text);
                setCoverPageValue("Ammonium", txtB28.Text, txtC28.Text, txtD28.Text, lbAnE28.Text, lbAnF28.Text, "", lbAnH28.Text, lbAnI28.Text, lbAnJ28.Text);
                setCoverPageValue("Potassium", txtB29.Text, txtC29.Text, txtD29.Text, lbAnE29.Text, lbAnF29.Text, "", lbAnH29.Text, lbAnI29.Text, lbAnJ29.Text);
                setCoverPageValue("Magnesium", txtB30.Text, txtC30.Text, txtD30.Text, lbAnE30.Text, lbAnF30.Text, "", lbAnH30.Text, lbAnI30.Text, lbAnJ30.Text);
                setCoverPageValue("Calcium", txtB31.Text, txtC31.Text, txtD31.Text, lbAnE31.Text, lbAnF31.Text, "", lbAnH31.Text, lbAnI31.Text, lbAnJ31.Text);
                setCoverPageValue("Total Cations","", "", "", "", "", "", "", lbAnI32.Text, lbAnJ32.Text);

                gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));

            }

            gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
            gvAnionic.DataBind();
            gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
            gvCationic.DataBind();
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));

            Label1.Text = ddlUnit.SelectedItem.Text;
            Label2.Text = ddlUnit.SelectedItem.Text;
            Label3.Text = ddlUnit.SelectedItem.Text;
            Label4.Text = ddlUnit.SelectedItem.Text;
            Label5.Text = ddlUnit.SelectedItem.Text;
            Label6.Text = ddlUnit.SelectedItem.Text;
            Label7.Text = ddlUnit.SelectedItem.Text;
            Label8.Text = ddlUnit.SelectedItem.Text;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        protected void OK_Click(object sender, EventArgs e)
        {

        }

    }
}