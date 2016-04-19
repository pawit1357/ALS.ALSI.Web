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
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WordToPDF;

namespace ALS.ALSI.Web.view.template
{
    public partial class Seagate_IC : System.Web.UI.UserControl
    {

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public int JobID
        {
            get { return (int)Session[GetType().Name + "JobID"]; }
            set { Session[GetType().Name + "JobID"] = value; }
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

        public List<template_seagate_ic_coverpage> coverpages
        {
            get { return (List<template_seagate_ic_coverpage>)Session[GetType().Name + "template_seagate_ic_coverpage"]; }
            set { Session[GetType().Name + "template_seagate_ic_coverpage"] = value; }
        }
        List<String> errors = new List<string>();

        private void initialPage()
        {
            this.CommandName = CommandNameEnum.Add;
            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = new tb_m_specification().SelectBySpecificationID(this.jobSample.specification_id, this.jobSample.template_id);
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

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


            }
            #endregion

            #region "WORKING"
            this.coverpages = template_seagate_ic_coverpage.FindAllBySampleID(this.SampleID);
            if (this.coverpages != null && this.coverpages.Count > 0)
            {
                this.CommandName = CommandNameEnum.Edit;
                template_seagate_ic_coverpage ic = this.coverpages[0];
                ddlSpecification.SelectedValue = ic.specification_id.ToString();

                tb_m_specification spec = new tb_m_specification().SelectByID(Convert.ToInt32(ic.specification_id));
                if (spec != null)
                {
                    /*METHOD/PROCEDURE:*/

                    txtB18.Text = ic.procedure_no;
                    txtC18.Text = (String.IsNullOrEmpty(ic.number_of_pieces__used_for_extraction) ? String.Empty : ic.number_of_pieces__used_for_extraction);//"Number of pieces used for extraction"
                    txtD18.Text = ic.extraction_medium;
                    txtE18.Text = (String.IsNullOrEmpty(ic.extraction_volume) ? String.Empty : String.Format("{0}ml", (1000 * Convert.ToDecimal(ic.extraction_volume))));//"Number of pieces used for extraction"



                    ///*RESULT*/
                    lbDocRev.Text = spec.B;
                    lbDesc.Text = spec.A;
                    if (ic.wunit != null)
                    {
                        ddlUnit.SelectedValue = ic.wunit.Value.ToString();
                        gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);
                        gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);
                        gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);
                        gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);

                        Label1.Text = ddlUnit.SelectedItem.Text;
                        Label2.Text = ddlUnit.SelectedItem.Text;
                        Label3.Text = ddlUnit.SelectedItem.Text;
                        Label4.Text = ddlUnit.SelectedItem.Text;
                        Label5.Text = ddlUnit.SelectedItem.Text;
                        Label6.Text = ddlUnit.SelectedItem.Text;
                    }
                    else
                    {
                        gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);
                        gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);
                        gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);
                        gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);


                        Label1.Text = ddlUnit.SelectedItem.Text;
                        Label2.Text = ddlUnit.SelectedItem.Text;
                        Label3.Text = ddlUnit.SelectedItem.Text;
                        Label4.Text = ddlUnit.SelectedItem.Text;
                        Label5.Text = ddlUnit.SelectedItem.Text;
                        Label6.Text = ddlUnit.SelectedItem.Text;
                    }

                    template_seagate_ic_coverpage A14 = getCoverPageValue("Fluoride as F");
                    template_seagate_ic_coverpage A15 = getCoverPageValue("Chloride as Cl");
                    template_seagate_ic_coverpage A16 = getCoverPageValue("Nitrite as NO2");
                    template_seagate_ic_coverpage A17 = getCoverPageValue("Bromide as Br");
                    template_seagate_ic_coverpage A18 = getCoverPageValue("Nitrate as NO3");
                    template_seagate_ic_coverpage A19 = getCoverPageValue("Sulfate as SO4");
                    template_seagate_ic_coverpage A20 = getCoverPageValue("Phosphate as PO4");
                    template_seagate_ic_coverpage A21 = getCoverPageValue("Total of 7Anions");


                    template_seagate_ic_coverpage C23 = getCoverPageValue("Lithium as Li");
                    template_seagate_ic_coverpage C24 = getCoverPageValue("Sodium as Na");
                    template_seagate_ic_coverpage C25 = getCoverPageValue("Ammonium as NH4");
                    template_seagate_ic_coverpage C26 = getCoverPageValue("Potassium as K");
                    template_seagate_ic_coverpage C27 = getCoverPageValue("Magnesium as Mg");
                    template_seagate_ic_coverpage C28 = getCoverPageValue("Calcium as Ca");
                    template_seagate_ic_coverpage C29 = getCoverPageValue("Total Cations");

                    //Working Sheet-IC
                    txtB9.Text = String.IsNullOrEmpty(ic.extraction_volume) ? String.Empty : ic.extraction_volume;
                    txtB10.Text = String.IsNullOrEmpty(ic.b10) ? String.Empty : ic.b10;
                    txtB11.Text = String.IsNullOrEmpty(ic.number_of_pieces__used_for_extraction) ? String.Empty : ic.number_of_pieces__used_for_extraction;

                    #region "Fluoride, F"
                    txtB14_Chem.Text = A14.wb;
                    txtC14_Chem.Text = A14.wc;
                    txtD14_Chem.Text = A14.wd;
                    lbAnE14.Text = A14.we;
                    //lbAnF14.Text =    A14.wf;
                    lbAnG14.Text = A14.wg;
                    lbAnH14.Text = A14.wh;
                    lbAnI14.Text = A14.wi;
                    lbAnJ14.Text = A14.wj;
                    #endregion
                    #region "Chloride, Cl"
                    txtB15_Chem.Text = A15.wb;
                    txtC15_Chem.Text = A15.wc;
                    txtD15_Chem.Text = A15.wd;
                    lbAnE15.Text = A15.we;
                    //lbAnF14.Text = CA14.wf;
                    lbAnG15.Text = A15.wg;
                    lbAnH15.Text = A15.wh;
                    lbAnI15.Text = A15.wi;
                    lbAnJ15.Text = A15.wj;
                    #endregion
                    #region "Nitrite as NO2"
                    txtB16_Chem.Text = A16.wb;
                    txtC16_Chem.Text = A16.wc;
                    txtD16_Chem.Text = A16.wd;
                    lbAnE16.Text = A16.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG16.Text = A16.wg;
                    lbAnH16.Text = A16.wh;
                    lbAnI16.Text = A16.wi;
                    lbAnJ16.Text = A16.wj;
                    #endregion
                    #region "Bromide, Br"
                    txtB17_Chem.Text = A17.wb;
                    txtC17_Chem.Text = A17.wc;
                    txtD17_Chem.Text = A17.wd;
                    lbAnE17.Text = A17.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG17.Text = A17.wg;
                    lbAnH17.Text = A17.wh;
                    lbAnI17.Text = A17.wi;
                    lbAnJ17.Text = A17.wj;
                    #endregion
                    #region "Bromide, Br"
                    txtB18_Chem.Text = A18.wb;
                    txtC18_Chem.Text = A18.wc;
                    txtD18_Chem.Text = A18.wd;
                    lbAnE18.Text = A18.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG18.Text = A18.wg;
                    lbAnH18.Text = A18.wh;
                    lbAnI18.Text = A18.wi;
                    lbAnJ18.Text = A18.wj;
                    #endregion
                    #region "Sulfate, SO4"
                    txtB19_Chem.Text = A19.wb;
                    txtC19_Chem.Text = A19.wc;
                    txtD19_Chem.Text = A19.wd;
                    lbAnE19.Text = A19.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG19.Text = A19.wg;
                    lbAnH19.Text = A19.wh;
                    lbAnI19.Text = A19.wi;
                    lbAnJ19.Text = A19.wj;
                    #endregion
                    #region "Phosphate, PO4"
                    txtB20_Chem.Text = A20.wb;
                    txtC20_Chem.Text = A20.wc;
                    txtD20_Chem.Text = A20.wd;
                    lbAnE20.Text = A20.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG20.Text = A20.wg;
                    lbAnH20.Text = A20.wh;
                    lbAnI20.Text = A20.wi;
                    lbAnJ20.Text = A20.wj;
                    #endregion

                    #region "Total"
                    lbAnH21.Text = A21.wh;
                    lbAnI21.Text = A21.wi;
                    lbAnJ21.Text = A21.wj;
                    #endregion
                    //-------------
                    #region "Lithium, Li"
                    txtB23_Chem.Text = C23.wb;
                    txtC23_Chem.Text = C23.wc;
                    txtD23_Chem.Text = C23.wd;
                    lbAnE23.Text = C23.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG23.Text = C23.wg;
                    lbAnH23.Text = C23.wh;
                    lbAnI23.Text = C23.wi;
                    lbAnJ23.Text = C23.wj;
                    #endregion
                    #region "Sodium, Na"
                    txtB24_Chem.Text = C24.wb;
                    txtC24_Chem.Text = C24.wc;
                    txtD24_Chem.Text = C24.wd;
                    lbAnE24.Text = C24.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG24.Text = C24.wg;
                    lbAnH24.Text = C24.wh;
                    lbAnI24.Text = C24.wi;
                    lbAnJ24.Text = C24.wj;
                    #endregion
                    #region "Ammonium, NH4"
                    txtB25_Chem.Text = C25.wb;
                    txtC25_Chem.Text = C25.wc;
                    txtD25_Chem.Text = C25.wd;
                    lbAnE25.Text = C25.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG25.Text = C25.wg;
                    lbAnH25.Text = C25.wh;
                    lbAnI25.Text = C25.wi;
                    lbAnJ25.Text = C25.wj;
                    #endregion
                    #region "Potassium, K"
                    txtB26_Chem.Text = C26.wb;
                    txtC26_Chem.Text = C26.wc;
                    txtD26_Chem.Text = C26.wd;
                    lbAnE26.Text = C26.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG26.Text = C26.wg;
                    lbAnH26.Text = C26.wh;
                    lbAnI26.Text = C26.wi;
                    lbAnJ26.Text = C26.wj;
                    #endregion
                    #region "Magnesium, Mg"
                    txtB27_Chem.Text = C27.wb;
                    txtC27_Chem.Text = C27.wc;
                    txtD27_Chem.Text = C27.wd;
                    lbAnE27.Text = C27.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG27.Text = C27.wg;
                    lbAnH27.Text = C27.wh;
                    lbAnI27.Text = C27.wi;
                    lbAnJ27.Text = C27.wj;
                    #endregion
                    #region "Calcium, Ca"
                    txtB28_Chem.Text = C28.wb;
                    txtC28_Chem.Text = C28.wc;
                    txtD28_Chem.Text = C28.wd;
                    lbAnE28.Text = C28.we;
                    //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                    lbAnG28.Text = C28.wg;
                    lbAnH28.Text = C28.wh;
                    lbAnI28.Text = C28.wi;
                    lbAnJ28.Text = C28.wj;
                    #endregion
                    #region "FTotal"
                    lbAnH29.Text = C29.wh;
                    lbAnI29.Text = C29.wi;
                    lbAnJ29.Text = C29.wj;
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
                this.coverpages = new List<template_seagate_ic_coverpage>();
            }


            //Show Method/Procedure
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

     
                txtB18.Enabled = true;
                txtC18.Enabled = false;
                txtD18.Enabled = true;
                txtD18.Enabled = true;
                txtE18.Enabled = false;
                gvAnionic.Columns[3].Visible = true;
                gvCationic.Columns[3].Visible = true;
                btnCoverPage.Visible = true;
                btnWorking.Visible = true;
            }
            else
            {

                txtB18.Enabled = false;
                txtC18.Enabled = false;
                txtD18.Enabled = false;
                txtD18.Enabled = false;
                txtE18.Enabled = false;
                gvAnionic.Columns[3].Visible = false;
                gvCationic.Columns[3].Visible = false;
                btnCoverPage.Visible = false;
                btnWorking.Visible = false;
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
            template_seagate_ic_coverpage objWork = new template_seagate_ic_coverpage();

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

                    foreach (template_seagate_ic_coverpage _cover in this.coverpages)
                    {
                        _cover.procedure_no = txtB18.Text;
                        _cover.number_of_pieces__used_for_extraction = txtB11.Text;
                        _cover.extraction_medium = txtD18.Text;
                        _cover.extraction_volume = txtB9.Text;
                        _cover.sample_id = this.SampleID;
                        _cover.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        _cover.b10 = txtB10.Text;
                    }
                    switch (this.CommandName)
                    {
                        case CommandNameEnum.Add:
                            template_seagate_ic_coverpage.InsertList(this.coverpages);
                            break;
                        case CommandNameEnum.Edit:
                            template_seagate_ic_coverpage.UpdateList(this.coverpages);
                            break;
                    }
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;

                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    //#endregion
                    foreach (template_seagate_ic_coverpage _cover in this.coverpages)
                    {
                        _cover.procedure_no = txtB18.Text;
                        _cover.number_of_pieces__used_for_extraction = txtB11.Text;
                        _cover.extraction_medium = txtD18.Text;
                        _cover.extraction_volume = txtB9.Text;
                        _cover.sample_id = this.SampleID;
                        _cover.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        _cover.b10 = txtB10.Text;
                        _cover.wunit = Convert.ToInt32(ddlUnit.SelectedValue);
                    }
                    template_seagate_ic_coverpage.UpdateList(this.coverpages);
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
                    if (btnUpload.HasFile && (Path.GetExtension(btnUpload.FileName).Equals(".pdf")))
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
                        this.jobSample.path_pdf = source_file_url;
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .pdf");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
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

        private void calculateByFormular()
        {

            setCoverPageValue("Fluoride as F", txtB14_Chem.Text, txtC14_Chem.Text, txtD14_Chem.Text, lbAnE14.Text, "", lbAnG14.Text, lbAnH14.Text, lbAnI14.Text, lbAnJ14.Text);
            setCoverPageValue("Chloride as Cl", txtB15_Chem.Text, txtC15_Chem.Text, txtD15_Chem.Text, lbAnE15.Text, "", lbAnG15.Text, lbAnH15.Text, lbAnI15.Text, lbAnJ15.Text);
            setCoverPageValue("Nitrite as NO2", txtB16_Chem.Text, txtC16_Chem.Text, txtD16_Chem.Text, lbAnE16.Text, "", lbAnG16.Text, lbAnH16.Text, lbAnI16.Text, lbAnJ16.Text);
            setCoverPageValue("Bromide as Br", txtB17_Chem.Text, txtC17_Chem.Text, txtD17_Chem.Text, lbAnE17.Text, "", lbAnG17.Text, lbAnH17.Text, lbAnI17.Text, lbAnJ17.Text);
            setCoverPageValue("Nitrate as NO3", txtB18_Chem.Text, txtC18_Chem.Text, txtD18_Chem.Text, lbAnE18.Text, "", lbAnG18.Text, lbAnH18.Text, lbAnI18.Text, lbAnJ18.Text);
            setCoverPageValue("Sulfate as SO4", txtB19_Chem.Text, txtC19_Chem.Text, txtD19_Chem.Text, lbAnE19.Text, "", lbAnG18.Text, lbAnH19.Text, lbAnI19.Text, lbAnJ19.Text);
            setCoverPageValue("Phosphate as PO4", txtB20_Chem.Text, txtC20_Chem.Text, txtD20_Chem.Text, lbAnE20.Text, "", lbAnG20.Text, lbAnH20.Text, lbAnI20.Text, lbAnJ20.Text);
            setCoverPageValue("Total of 7Anions", "", "", "", "", "", "", lbAnH21.Text, lbAnI21.Text, lbAnJ21.Text);

            setCoverPageValue("Lithium as Li", txtB23_Chem.Text, txtC23_Chem.Text, txtD23_Chem.Text, lbAnE23.Text, "", lbAnG23.Text, lbAnH23.Text, lbAnI23.Text, lbAnJ23.Text);
            setCoverPageValue("Sodium as Na", txtB24_Chem.Text, txtC24_Chem.Text, txtD24_Chem.Text, lbAnE24.Text, "", lbAnG24.Text, lbAnH24.Text, lbAnI24.Text, lbAnJ24.Text);
            setCoverPageValue("Ammonium as NH4", txtB25_Chem.Text, txtC25_Chem.Text, txtD25_Chem.Text, lbAnE25.Text, "", lbAnG25.Text, lbAnH25.Text, lbAnI25.Text, lbAnJ25.Text);
            setCoverPageValue("Potassium as K", txtB26_Chem.Text, txtC26_Chem.Text, txtD26_Chem.Text, lbAnE26.Text, "", lbAnG26.Text, lbAnH26.Text, lbAnI26.Text, lbAnJ26.Text);
            setCoverPageValue("Magnesium as Mg", txtB27_Chem.Text, txtC27_Chem.Text, txtD27_Chem.Text, lbAnE27.Text, "", lbAnG27.Text, lbAnH27.Text, lbAnI27.Text, lbAnJ27.Text);
            setCoverPageValue("Calcium as Ca", txtB28_Chem.Text, txtC28_Chem.Text, txtD28_Chem.Text, lbAnE28.Text, "", lbAnG28.Text, lbAnH28.Text, lbAnI28.Text, lbAnJ28.Text);
            setCoverPageValue("Total Cations", "", "", "", "", "", "", lbAnH29.Text, lbAnI29.Text, lbAnJ29.Text);

            gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
            gvAnionic.DataBind();
            gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
            gvCationic.DataBind();

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

                    txtC18.Text = txtB11.Text;
                    txtE18.Text = (String.IsNullOrEmpty(txtB9.Text) ? String.Empty : String.Format("{0}ml", (1000 * Convert.ToDecimal(txtB9.Text))));//"Number of pieces used for extraction"

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

        protected void gvAnionic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvAnionic.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvAnionic.DataKeys[e.Row.RowIndex].Values[1]);
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
        protected void gvCationic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvCationic.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCationic.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvAnionic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_ic_coverpage gcms = this.coverpages.Find(x => x.id == PKID);
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
                template_seagate_ic_coverpage gcms = this.coverpages.Find(x => x.id == PKID);
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

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_specification tem = new tb_m_specification().SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (tem != null)
            {
                /*RESULT*/
                lbDocRev.Text = tem.B;
                lbDesc.Text = tem.A;
                txtB18.Text = tem.B;

                gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", tem.C);
                gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", tem.C);

                gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", tem.C);
                gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", tem.C);

                List<template_seagate_ic_coverpage> listCover = new List<template_seagate_ic_coverpage>();

                #region "*Anionic*SO4,Br,F,Cl,NO3,NO2,PO4"


                template_seagate_ic_coverpage _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 1;
                _tmp.A = "Fluoride as F";
                _tmp.B = tem.F;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 2;
                _tmp.A = "Chloride as Cl";
                _tmp.B = tem.G;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 3;
                _tmp.A = "Nitrite as NO2";
                _tmp.B = tem.I;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 4;
                _tmp.A = "Bromide as Br";
                _tmp.B = tem.E;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 5;
                _tmp.A = "Nitrate as NO3";
                _tmp.B = tem.H;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 6;
                _tmp.A = "Sulfate as SO4";
                _tmp.B = tem.D;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 7;
                _tmp.A = "Phosphate as PO4";
                _tmp.B = tem.J;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);

                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 8;
                _tmp.A = "Total of 7Anions";
                _tmp.B = tem.K;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                #endregion

                #region "*Cationic*NH4,Li,Ca,K,Na,Mg"
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 9;
                _tmp.A = "Lithium as Li";
                _tmp.B = tem.M;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 10;
                _tmp.A = "Sodium as Na";
                _tmp.B = tem.P;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 11;
                _tmp.A = "Ammonium as NH4";
                _tmp.B = tem.L;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 12;
                _tmp.A = "Potassium as K";
                _tmp.B = tem.O;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 11;
                _tmp.A = "Magnesium as Mg";
                _tmp.B = tem.Q;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 13;
                _tmp.A = "Calcium as Ca";
                _tmp.B = tem.N;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
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

        protected void lbDownload_Click(object sender, EventArgs e)
        {


            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);
            List<template_seagate_ic_coverpage> anionic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            List<template_seagate_ic_coverpage> cationic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);


            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1 ));
            reportParameters.Add(new ReportParameter("Company_addr",  reportHeader.addr2));

            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", "IC"));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on Seagate's Doc {0} for {1}", lbDocRev.Text, lbDesc.Text)));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ic_seagate.rdlc");
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
                        Word2Pdf objWorPdf = new Word2Pdf();
                        objWorPdf.InputLocation = String.Format("{0}{1}",Configurations.PATH_DRIVE,this.jobSample.path_word);
                        objWorPdf.OutputLocation = String.Format("{0}{1}", Configurations.PATH_DRIVE, this.jobSample.path_word).Replace("doc","pdf");
                        try
                        {
                            objWorPdf.Word2PdfCOnversion();
                            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word).Replace("doc", "pdf"));

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine();
                            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));

                        }
                    }
                    //if (!String.IsNullOrEmpty(this.jobSample.path_pdf))
                    //{
                    //    Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_pdf));
                    //}
                    //else
                    //{
                    //    byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    //    // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                    //    Response.Buffer = true;
                    //    Response.Clear();
                    //    Response.ContentType = mimeType;
                    //    Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
                    //    Response.BinaryWrite(bytes); // create the file
                    //    Response.Flush(); // send it to the client to download
                    //}
                    break;
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

                        //TransferXLToTable();
                        using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.ReadWrite))
                        {
                            HSSFWorkbook wb = new HSSFWorkbook(fs);
                            ISheet isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.ic.excel.sheetname.working1"]);
                            if (isheet == null)
                            {
                                errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.ic.excel.sheetname.working1"]));
                            }
                            else
                            {
                                sheetName = isheet.SheetName;

                                txtB9.Text = CustomUtils.GetCellValue(isheet.GetRow(9 - 1).GetCell(ExcelColumn.B));
                                txtB10.Text = CustomUtils.GetCellValue(isheet.GetRow(10 - 1).GetCell(ExcelColumn.B));
                                txtB11.Text = CustomUtils.GetCellValue(isheet.GetRow(11 - 1).GetCell(ExcelColumn.B));

                                #region "Fluoride, F"
                                txtB14_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.B));
                                txtC14_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.C));
                                txtD14_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.D));
                                lbAnE14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.G));
                                lbAnH14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.H));
                                lbAnI14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.I));
                                lbAnJ14.Text = CustomUtils.GetCellValue(isheet.GetRow(14 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB14_Chem.Text = Math.Round(Convert.ToDecimal(txtB14_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC14_Chem.Text = Math.Round(Convert.ToDecimal(txtC14_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD14_Chem.Text = Math.Round(Convert.ToDecimal(txtD14_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE14.Text = Math.Round(Convert.ToDecimal(lbAnE14.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG14.Text = Math.Round(Convert.ToDecimal(lbAnG14.Text), Convert.ToInt16(txtDecimal05.Text)) + "";

                                lbAnH14.Text = Math.Round(Convert.ToDecimal(lbAnH14.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI14.Text = (lbAnI14.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI14.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ14.Text = Math.Round(Convert.ToDecimal(lbAnJ14.Text), Convert.ToInt16(txtDecimal09.Text)) + "";



                                #endregion
                                #region "Chloride, Cl"
                                txtB15_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.B));
                                txtC15_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.C));
                                txtD15_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.D));
                                lbAnE15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.G));
                                lbAnH15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.H));
                                lbAnI15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.I));
                                lbAnJ15.Text = CustomUtils.GetCellValue(isheet.GetRow(15 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB15_Chem.Text = Math.Round(Convert.ToDecimal(txtB15_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC15_Chem.Text = Math.Round(Convert.ToDecimal(txtC15_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD15_Chem.Text = Math.Round(Convert.ToDecimal(txtD15_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE15.Text = Math.Round(Convert.ToDecimal(lbAnE15.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG15.Text = Math.Round(Convert.ToDecimal(lbAnG15.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH15.Text = Math.Round(Convert.ToDecimal(lbAnH15.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI15.Text = (lbAnI15.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI15.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ15.Text = Math.Round(Convert.ToDecimal(lbAnJ15.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Nitrite as NO2"
                                txtB16_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.B));
                                txtC16_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.C));
                                txtD16_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.D));
                                lbAnE16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.G));
                                lbAnH16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.H));
                                lbAnI16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.I));
                                lbAnJ16.Text = CustomUtils.GetCellValue(isheet.GetRow(16 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB16_Chem.Text = Math.Round(Convert.ToDecimal(txtB16_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC16_Chem.Text = Math.Round(Convert.ToDecimal(txtC16_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD16_Chem.Text = Math.Round(Convert.ToDecimal(txtD16_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE16.Text = Math.Round(Convert.ToDecimal(lbAnE16.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG16.Text = Math.Round(Convert.ToDecimal(lbAnG16.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH16.Text = Math.Round(Convert.ToDecimal(lbAnH16.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI16.Text = (lbAnI16.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI16.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ16.Text = Math.Round(Convert.ToDecimal(lbAnJ16.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Bromide, Br"
                                txtB17_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.B));
                                txtC17_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.C));
                                txtD17_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.D));
                                lbAnE17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.G));
                                lbAnH17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.H));
                                lbAnI17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.I));
                                lbAnJ17.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB17_Chem.Text = Math.Round(Convert.ToDecimal(txtB17_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC17_Chem.Text = Math.Round(Convert.ToDecimal(txtC17_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD17_Chem.Text = Math.Round(Convert.ToDecimal(txtD17_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE17.Text = Math.Round(Convert.ToDecimal(lbAnE17.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG17.Text = Math.Round(Convert.ToDecimal(lbAnG17.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH17.Text = Math.Round(Convert.ToDecimal(lbAnH17.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI17.Text = (lbAnI17.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI17.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ17.Text = Math.Round(Convert.ToDecimal(lbAnJ17.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Bromide, Br"
                                txtB18_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.B));
                                txtC18_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.C));
                                txtD18_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.D));
                                lbAnE18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.G));
                                lbAnH18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.H));
                                lbAnI18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.I));
                                lbAnJ18.Text = CustomUtils.GetCellValue(isheet.GetRow(18 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB18_Chem.Text = Math.Round(Convert.ToDecimal(txtB18_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC18_Chem.Text = Math.Round(Convert.ToDecimal(txtC18_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD18_Chem.Text = Math.Round(Convert.ToDecimal(txtD18_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE18.Text = Math.Round(Convert.ToDecimal(lbAnE18.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG18.Text = Math.Round(Convert.ToDecimal(lbAnG18.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH18.Text = Math.Round(Convert.ToDecimal(lbAnH18.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI18.Text = (lbAnI18.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI18.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ18.Text = Math.Round(Convert.ToDecimal(lbAnJ18.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Sulfate, SO4"
                                txtB19_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.B));
                                txtC19_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.C));
                                txtD19_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.D));
                                lbAnE19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.G));
                                lbAnH19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.H));
                                lbAnI19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.I));
                                lbAnJ19.Text = CustomUtils.GetCellValue(isheet.GetRow(19 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB19_Chem.Text = Math.Round(Convert.ToDecimal(txtB19_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC19_Chem.Text = Math.Round(Convert.ToDecimal(txtC19_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD19_Chem.Text = Math.Round(Convert.ToDecimal(txtD19_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE19.Text = Math.Round(Convert.ToDecimal(lbAnE19.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG19.Text = Math.Round(Convert.ToDecimal(lbAnG19.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH19.Text = Math.Round(Convert.ToDecimal(lbAnH19.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI19.Text = (lbAnI19.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI19.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ19.Text = Math.Round(Convert.ToDecimal(lbAnJ19.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Phosphate, PO4"
                                txtB20_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.B));
                                txtC20_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.C));
                                txtD20_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.D));
                                lbAnE20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.G));
                                lbAnH20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.H));
                                lbAnI20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.I));
                                lbAnJ20.Text = CustomUtils.GetCellValue(isheet.GetRow(20 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB20_Chem.Text = Math.Round(Convert.ToDecimal(txtB20_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC20_Chem.Text = Math.Round(Convert.ToDecimal(txtC20_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD20_Chem.Text = Math.Round(Convert.ToDecimal(txtD20_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE20.Text = Math.Round(Convert.ToDecimal(lbAnE20.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG20.Text = Math.Round(Convert.ToDecimal(lbAnG20.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH20.Text = Math.Round(Convert.ToDecimal(lbAnH20.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI20.Text = (lbAnI20.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI20.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ20.Text = Math.Round(Convert.ToDecimal(lbAnJ20.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion

                                #region "Total"
                                lbAnH21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.H));
                                lbAnI21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.I));
                                lbAnJ21.Text = CustomUtils.GetCellValue(isheet.GetRow(21 - 1).GetCell(ExcelColumn.J));


                                //Decimal              
                                lbAnH21.Text = Math.Round(Convert.ToDecimal(lbAnH21.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI21.Text = (lbAnI21.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI21.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ21.Text = Math.Round(Convert.ToDecimal(lbAnJ21.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                //-------------
                                #region "Lithium, Li"
                                txtB23_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.B));
                                txtC23_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.C));
                                txtD23_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.D));
                                lbAnE23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.G));
                                lbAnH23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.H));
                                lbAnI23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.I));
                                lbAnJ23.Text = CustomUtils.GetCellValue(isheet.GetRow(23 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB23_Chem.Text = Math.Round(Convert.ToDecimal(txtB23_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC23_Chem.Text = Math.Round(Convert.ToDecimal(txtC23_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD23_Chem.Text = Math.Round(Convert.ToDecimal(txtD23_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE23.Text = Math.Round(Convert.ToDecimal(lbAnE23.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG23.Text = Math.Round(Convert.ToDecimal(lbAnG23.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH23.Text = Math.Round(Convert.ToDecimal(lbAnH23.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI23.Text = (lbAnI23.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI23.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ23.Text = Math.Round(Convert.ToDecimal(lbAnJ23.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Sodium, Na"
                                txtB24_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.B));
                                txtC24_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.C));
                                txtD24_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.D));
                                lbAnE24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.G));
                                lbAnH24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.H));
                                lbAnI24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.I));
                                lbAnJ24.Text = CustomUtils.GetCellValue(isheet.GetRow(24 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB24_Chem.Text = Math.Round(Convert.ToDecimal(txtB24_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC24_Chem.Text = Math.Round(Convert.ToDecimal(txtC24_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD24_Chem.Text = Math.Round(Convert.ToDecimal(txtD24_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE24.Text = Math.Round(Convert.ToDecimal(lbAnE24.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG24.Text = Math.Round(Convert.ToDecimal(lbAnG24.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH24.Text = Math.Round(Convert.ToDecimal(lbAnH24.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI24.Text = (lbAnI24.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI24.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ24.Text = Math.Round(Convert.ToDecimal(lbAnJ24.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Ammonium, NH4"
                                txtB25_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.B));
                                txtC25_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.C));
                                txtD25_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.D));
                                lbAnE25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.G));
                                lbAnH25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.H));
                                lbAnI25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.I));
                                lbAnJ25.Text = CustomUtils.GetCellValue(isheet.GetRow(25 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB25_Chem.Text = Math.Round(Convert.ToDecimal(txtB25_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC25_Chem.Text = Math.Round(Convert.ToDecimal(txtC25_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD25_Chem.Text = Math.Round(Convert.ToDecimal(txtD25_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE25.Text = Math.Round(Convert.ToDecimal(lbAnE25.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG25.Text = Math.Round(Convert.ToDecimal(lbAnG25.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH25.Text = Math.Round(Convert.ToDecimal(lbAnH25.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI25.Text = (lbAnI25.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI25.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ25.Text = Math.Round(Convert.ToDecimal(lbAnJ25.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Potassium, K"
                                txtB26_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.B));
                                txtC26_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.C));
                                txtD26_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.D));
                                lbAnE26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.G));
                                lbAnH26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.H));
                                lbAnI26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.I));
                                lbAnJ26.Text = CustomUtils.GetCellValue(isheet.GetRow(26 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB26_Chem.Text = Math.Round(Convert.ToDecimal(txtB26_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC26_Chem.Text = Math.Round(Convert.ToDecimal(txtC26_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD26_Chem.Text = Math.Round(Convert.ToDecimal(txtD26_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE26.Text = Math.Round(Convert.ToDecimal(lbAnE26.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG26.Text = Math.Round(Convert.ToDecimal(lbAnG26.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH26.Text = Math.Round(Convert.ToDecimal(lbAnH26.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI26.Text = (lbAnI26.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI26.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ26.Text = Math.Round(Convert.ToDecimal(lbAnJ26.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Magnesium, Mg"
                                txtB27_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.B));
                                txtC27_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.C));
                                txtD27_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.D));
                                lbAnE27.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG27.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.G));
                                lbAnH27.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.H));
                                lbAnI27.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.I));
                                lbAnJ27.Text = CustomUtils.GetCellValue(isheet.GetRow(27 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB27_Chem.Text = Math.Round(Convert.ToDecimal(txtB27_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC27_Chem.Text = Math.Round(Convert.ToDecimal(txtC27_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD27_Chem.Text = Math.Round(Convert.ToDecimal(txtD27_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE27.Text = Math.Round(Convert.ToDecimal(lbAnE27.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG27.Text = Math.Round(Convert.ToDecimal(lbAnG27.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH27.Text = Math.Round(Convert.ToDecimal(lbAnH27.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI27.Text = (lbAnI27.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI27.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ27.Text = Math.Round(Convert.ToDecimal(lbAnJ27.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "Calcium, Ca"
                                txtB28_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.B));
                                txtC28_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.C));
                                txtD28_Chem.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.D));
                                lbAnE28.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.E));
                                //lbAnF14.Text = CustomUtils.GetCellValue(isheet.GetRow(17 - 1).GetCell(ExcelColumn.F));
                                lbAnG28.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.G));
                                lbAnH28.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.H));
                                lbAnI28.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.I));
                                lbAnJ28.Text = CustomUtils.GetCellValue(isheet.GetRow(28 - 1).GetCell(ExcelColumn.J));

                                //Decimal
                                txtB28_Chem.Text = Math.Round(Convert.ToDecimal(txtB28_Chem.Text), Convert.ToInt16(txtDecimal01.Text)) + "";
                                txtC28_Chem.Text = Math.Round(Convert.ToDecimal(txtC28_Chem.Text), Convert.ToInt16(txtDecimal02.Text)) + "";
                                txtD28_Chem.Text = Math.Round(Convert.ToDecimal(txtD28_Chem.Text), Convert.ToInt16(txtDecimal03.Text)) + "";
                                lbAnE28.Text = Math.Round(Convert.ToDecimal(lbAnE28.Text), Convert.ToInt16(txtDecimal04.Text)) + "";
                                lbAnG28.Text = Math.Round(Convert.ToDecimal(lbAnG28.Text), Convert.ToInt16(txtDecimal05.Text)) + "";
                                lbAnH28.Text = Math.Round(Convert.ToDecimal(lbAnH28.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI28.Text = (lbAnI28.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI28.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ28.Text = Math.Round(Convert.ToDecimal(lbAnJ28.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
                                #endregion
                                #region "FTotal"
                                lbAnH29.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.H));
                                lbAnI29.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.I));
                                lbAnJ29.Text = CustomUtils.GetCellValue(isheet.GetRow(29 - 1).GetCell(ExcelColumn.J));

                                //Decimal              
                                lbAnH29.Text = Math.Round(Convert.ToDecimal(lbAnH29.Text), Convert.ToInt16(txtDecimal07.Text)) + "";
                                lbAnI29.Text = (lbAnI29.Text.StartsWith("<") ? "<" : "") + Math.Round(Convert.ToDecimal(lbAnI29.Text.Replace('<', ' ').Trim()), Convert.ToInt16(txtDecimal08.Text)) + "";
                                lbAnJ29.Text = Math.Round(Convert.ToDecimal(lbAnJ29.Text), Convert.ToInt16(txtDecimal09.Text)) + "";
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
            template_seagate_ic_coverpage _val = this.coverpages.Where(x => x.A.StartsWith(_peakname)).FirstOrDefault();
            if (_val != null)
            {
                _val.wb = _wb;
                _val.wc = _wc;
                _val.wd = _wd;
                _val.we = _we;
                _val.wf = CustomUtils.isNumber(_wf) ? Math.Round(Convert.ToDouble(_wf), 2) + "" : String.Empty;
                _val.wg = _wg;
                _val.wg = _wh;
                _val.wi = _wi;
                _val.wj = _wj;
                //_val.wunit = _k;//unit
                //_val.E = (_val.B.Equals("NA") ? "NA" : (Convert.ToDouble(_wj) < Convert.ToDouble(_val.B)) ? "PASS" : "FAIL");
            }
        }
        //private void setCoverPageValue(String _peakname, String _b, String _c, String _d,  String _result)
        //{
        //    template_seagate_ic_coverpage _val = this.coverpages.Where(x => x.A.StartsWith(_peakname)).FirstOrDefault();
        //    if (_val != null)
        //    {
        //            _val.wb = _b;
        //            _val.wc = _c;
        //            _val.wd = _d;
        //            _val.C = _result;
        //    }
        //}

        private template_seagate_ic_coverpage getCoverPageValue(String _peakname)
        {
            return this.coverpages.Where(x => x.A.Equals(_peakname)).FirstOrDefault();
        }


        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

            gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);
            gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);
            gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);
            gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);


            Label1.Text = ddlUnit.SelectedItem.Text;
            Label2.Text = ddlUnit.SelectedItem.Text;
            Label3.Text = ddlUnit.SelectedItem.Text;
            Label4.Text = ddlUnit.SelectedItem.Text;
            Label5.Text = ddlUnit.SelectedItem.Text;
            Label6.Text = ddlUnit.SelectedItem.Text;

        }



        public static void TransferXLToTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("Zip", typeof(string));

            using (FileStream stream = new FileStream(@"E:\test2.xlsx", FileMode.Open, FileAccess.Read))
            {
                IWorkbook wb = new XSSFWorkbook(stream);
                ISheet sheet = wb.GetSheet("Sheet1");
                string holder;
                int i = 0;
                do
                {
                    DataRow dr = dt.NewRow();
                    IRow row = sheet.GetRow(i);
                    try
                    {
                        holder = row.GetCell(0, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    //string city = holder.Substring(0, holder.IndexOf(','));
                    //string state = holder.Substring(holder.IndexOf(',') + 2, 2);
                    //string zip = holder.Substring(holder.IndexOf(',') + 5, 5);
                    dr[0] = "1";
                    dr[1] = "2";
                    dr[2] = "3";
                    dt.Rows.Add(dr);
                    i++;
                } while (!String.IsNullOrEmpty(holder));
            }

            using (FileStream stream = new FileStream(@"E:\FieldedAddresses.xlsx", FileMode.Create, FileAccess.Write))
            {
                IWorkbook wb = new XSSFWorkbook();
                ISheet sheet = wb.CreateSheet("Sheet1");
                ICreationHelper cH = wb.GetCreationHelper();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i);
                    for (int j = 0; j < 3; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(cH.CreateRichTextString(dt.Rows[i].ItemArray[j].ToString()));
                    }
                }
                wb.Write(stream);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();

        }

    }
}