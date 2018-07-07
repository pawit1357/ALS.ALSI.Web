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
using Spire.Doc;
//using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Collections;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ALS.ALSI.Web.view.template
{
    public partial class FT_IC : System.Web.UI.UserControl
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

        public List<sample_method_procedure> ListSampleMethodProcedure
        {
            get { return (List<sample_method_procedure>)Session[GetType().Name + "sample_method_procedure"]; }
            set { Session[GetType().Name + "sample_method_procedure"] = value; }
        }
        public List<template_f_ic> ics
        {
            get { return (List<template_f_ic>)Session[GetType().Name + "ics"]; }
            set { Session[GetType().Name + "ics"] = value; }
        }

        //public ExcelWorksheet wCoverPage
        //{
        //    get { return (ExcelWorksheet)Session[GetType().Name + "wCoverPage"]; }
        //    set { Session[GetType().Name + "wCoverPage"] = value; }
        //}
        //public ExcelWorksheet wSpecification
        //{
        //    get { return (ExcelWorksheet)Session[GetType().Name + "wCoverPage"]; }
        //    set { Session[GetType().Name + "wCoverPage"] = value; }
        //}
        public Hashtable configs
        {
            get { return (Hashtable)Session[GetType().Name + "configs"]; }
            set { Session[GetType().Name + "configs"] = value; }
        }
        public Hashtable workSheetData
        {
            get { return (Hashtable)Session[GetType().Name + "workSheetData"]; }
            set { Session[GetType().Name + "workSheetData"] = value; }
        }
        public List<tb_m_specification> listSpecificatons
        {
            get { return (List<tb_m_specification>)Session[GetType().Name + "listSpecificatons"]; }
            set { Session[GetType().Name + "listSpecificatons"] = value; }
        }
        //public List<template_f_ic> listResult
        //{
        //    get { return (List<template_f_ic>)Session[GetType().Name + "listResult"]; }
        //    set { Session[GetType().Name + "listResult"] = value; }
        //}

        private String SheetCoverPageName = "Coverpage-TH";
        private String SheetWorkSheetName = "IC";


        //private DataTable ConvertToDataTable(ExcelWorksheet oSheet)
        //{
        //    int totalRows = 24;// oSheet.Dimension.End.Row;
        //    int totalCols = 5;// oSheet.Dimension.End.Column;
        //    DataTable dt = new DataTable(oSheet.Name);
        //    DataRow dr = null;
        //    for (int r = 23; r <= totalRows; r++)
        //    {
        //        if (r > 23) dr = dt.Rows.Add();
        //        for (int c = 1; c <= totalCols; c++)
        //        {
        //            if (r == 23)
        //                dt.Columns.Add(oSheet.Cells[r, c].Value.ToString());
        //            else
        //                dr[c - 1] = oSheet.Cells[r, c].Value.ToString();
        //        }
        //    }
        //    return dt;
        //}

        private void initialPage()
        {
            this.CommandName = CommandNameEnum.Add;
            this.ics = new List<template_f_ic>();

            m_template template = new m_template().SelectByID(this.jobSample.template_id);

            //Workbook book = new Workbook();
            //book.LoadFromFile("sample.xlsx");
            //book.SaveToFile("result.xls", ExcelVersion.Version97to2003);



            //FileInfo excel = new FileInfo(template.path_source_file);
            //using (var package = new ExcelPackage(excel))
            //{
            //    var workbook = package.Workbook;

            //    //*** Sheet 1
            //    var worksheet = workbook.Worksheets["Coverpage-TH"];//.First();

            //    //*** DataTable & DataSource
            //    DataTable dt = ConvertToDataTable(worksheet);

            //    GridView5.DataSource = dt;
            //    GridView5.DataBind();
            //}



            FileInfo fileInfo = new FileInfo(template.path_source_file);
            using (var package = new ExcelPackage(fileInfo))
            {
                FreeTemplateUtil ftu = new FreeTemplateUtil(fileInfo);
                this.configs = ftu.getConfigValue();
                this.listSpecificatons = ftu.getSpecification();
                ExcelWorksheet wCoverPage = package.Workbook.Worksheets["Coverpage-TH"];
                ExcelWorksheet wSpecification = package.Workbook.Worksheets["Specification"];

                #region "METHOD/PROCEDURE"
                String[] methodAndProcedureHaders = configs["method.procedure.header.values"].ToString().Split(FreeTemplateUtil.DELIMITER);

                sample_method_procedure _tmp = new sample_method_procedure();
                _tmp.id = 1;
                for (int c = 0; c < gvMethodProcedure.Columns.Count - 1; c++)
                {
                    if (c < methodAndProcedureHaders.Length)
                    {
                        gvMethodProcedure.Columns[c].Visible = true;
                        gvMethodProcedure.Columns[c].HeaderText = methodAndProcedureHaders[c];

                        String data = this.configs[String.Format("method.procedure.row.1.col.{0}", (c + 1))].ToString();
                        switch (c + 1)
                        {
                            case 1: _tmp.col_1 = data; break;
                            case 2: _tmp.col_2 = data; break;
                            case 3: _tmp.col_3 = data; break;
                            case 4: _tmp.col_4 = data; break;
                            case 5: _tmp.col_5 = data; break;
                            case 6: _tmp.col_6 = data; break;
                            case 7: _tmp.col_7 = data; break;
                            case 8: _tmp.col_8 = data; break;
                            case 9: _tmp.col_9 = data; break;
                            case 10: _tmp.col_10 = data; break;
                            case 11: _tmp.col_11 = data; break;
                            case 12: _tmp.col_12 = data; break;
                            case 13: _tmp.col_13 = data; break;
                            case 14: _tmp.col_14 = data; break;
                            case 15: _tmp.col_15 = data; break;
                            case 16: _tmp.col_16 = data; break;
                            case 17: _tmp.col_17 = data; break;
                            case 18: _tmp.col_18 = data; break;
                            case 19: _tmp.col_19 = data; break;
                            case 20: _tmp.col_20 = data; break;
                        }

                    }
                    else
                    {
                        gvMethodProcedure.Columns[c].Visible = false;
                    }
                }
                ListSampleMethodProcedure.Add(_tmp);
                gvMethodProcedure.DataSource = ListSampleMethodProcedure;
                gvMethodProcedure.DataBind();

                #endregion
                #region "RESULT"
                String[] resultGv1 = configs["results.group.1.header.values"].ToString().Split(FreeTemplateUtil.DELIMITER);

                for (int i = 0; i < GridView1.Columns.Count - 1; i++)
                {
                    if (i < resultGv1.Length)
                    {
                        GridView1.Columns[i].Visible = true;
                        GridView1.Columns[i].HeaderText = resultGv1[i];
                    }
                    else
                    {
                        GridView1.Columns[i].Visible = false;
                    }
                }
                #endregion

            }
            //this.listResult.Add(new template_f_ic { id = 0, row_type = 1 });//header
            //GridView1.DataSource = this.listResult.Where(x => x.row_type == 1);
            //GridView1.DataBind();


            ddlComponent.Items.Clear();
            ddlComponent.DataSource = listSpecificatons;
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));


            //tb_unit unit = new tb_unit();
            //ddlUnit.Items.Clear();
            //ddlUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("IC")).ToList();
            //ddlUnit.DataBind();
            //ddlUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));





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

                txtDateAnalyzed.Text = (this.jobSample.date_chemist_analyze != null) ? this.jobSample.date_chemist_analyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = userRole == RoleEnum.CHEMIST;
                #region "VISIBLE RESULT DATA"


                if (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                {


                    //txtProcedureNo.Enabled = true;
                    //txtNumOfPiecesUsedForExtraction.Enabled = false;
                    //txtExtractionMedium.Enabled = true;
                    //txtExtractionVolume.Enabled = false;
                    //gvAnionic.Columns[5].Visible = true;
                    //gvCationic.Columns[5].Visible = true;
                    btnCoverPage.Visible = true;
                    btnWorking.Visible = true;
                    pLoadFile.Visible = true;
                }
                else
                {
                    //txtProcedureNo.Enabled = false;
                    //txtNumOfPiecesUsedForExtraction.Enabled = false;
                    //txtExtractionMedium.Enabled = false;
                    //txtExtractionVolume.Enabled = false;
                    //gvAnionic.Columns[5].Visible = false;
                    //gvCationic.Columns[5].Visible = false;
                    btnCoverPage.Visible = false;
                    btnWorking.Visible = false;
                    pLoadFile.Visible = false;
                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
                    {
                        btnCoverPage.Visible = true;
                        btnWorking.Visible = true;
                    }

                }
                #endregion


            }
            #endregion

            #region "WORKING"
            //this.coverpages = template_wd_ic_coverpage.FindAllBySampleID(this.SampleID);
            //if (this.coverpages != null && this.coverpages.Count > 0)
            //{
            //    this.CommandName = CommandNameEnum.Edit;
            //    template_wd_ic_coverpage ic = this.coverpages[0];
            //    /*METHOD/PROCEDURE:*/
            //    //txtProcedureNo.Text = ic.ProcedureNo;
            //    //txtNumOfPiecesUsedForExtraction.Text = ic.NumOfPiecesUsedForExtraction;
            //    //txtExtractionMedium.Text = ic.ExtractionMedium;
            //    //txtExtractionVolume.Text = (String.IsNullOrEmpty(ic.ExtractionVolume) ? String.Empty : String.Format("{0}mL", Convert.ToInt32((1000 * Convert.ToDouble(ic.ExtractionVolume)))));

            //    ddlComponent.SelectedValue = ic.component_id.ToString();
                //ddlDetailSpec.SelectedValue = ic.detail_spec_id.ToString();

                //detailSpec = new tb_m_detail_spec().SelectByID(Convert.ToInt32(ic.detail_spec_id));
                //if (detailSpec != null)
                //{
                //    /*RESULT*/
                //    cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;
                //    if (cbCheckBox.Checked)
                //    {
                //        lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "WD");
                //    }
                //    else
                //    {

                //        lbSpecDesc.Text = String.Format("The specification is based on Western Digital's document no. {0} {1}", detailSpec.B, detailSpec.A);

                //    }


                //    txtB11.Text = ic.ExtractionVolume;
                //    txtB12.Text = ic.b12;
                //    txtB13.Text = ic.NumOfPiecesUsedForExtraction;

                //    txtProcedureNo.Text = ic.ProcedureNo;
                //    txtNumOfPiecesUsedForExtraction.Text = ic.NumOfPiecesUsedForExtraction;
                //    txtExtractionMedium.Text = ic.ExtractionMedium;
                //    txtExtractionVolume.Text = (String.IsNullOrEmpty(ic.ExtractionVolume) ? String.Empty : String.Format("{0}mL", (1000 * Convert.ToDouble(ic.ExtractionVolume))));
                //    #region "Unit"
                //    if (ic.wunit != null)
                //    {
                //        ddlUnit.SelectedValue = ic.wunit.Value.ToString();
                //    }
                //    if (ic.wunit != null)
                //    {
                //        ddlUnit.SelectedValue = ic.wunit.Value.ToString();

                //        gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));

                //        gvResultAnions.Columns[1].HeaderText = String.Format("Conc of water Blankµg/ L(B)", ddlUnit.SelectedItem.Text);
                //        gvResultAnions.Columns[2].HeaderText = String.Format("Conc of Sample µg/ L(C)", ddlUnit.SelectedItem.Text);
                //        gvResultAnions.Columns[4].HeaderText = String.Format("Raw Results {0}", ddlUnit.SelectedItem.Text);
                //        gvResultAnions.Columns[5].HeaderText = String.Format("Method Detection Limit  {0}", ddlUnit.SelectedItem.Text);
                //        gvResultAnions.Columns[6].HeaderText = String.Format("Instrument Detection Limit  {0}", ddlUnit.SelectedItem.Text);
                //        gvResultAnions.Columns[8].HeaderText = String.Format("Final Conc. of Sample  {0}", ddlUnit.SelectedItem.Text);

                //        gvResultCations.Columns[1].HeaderText = String.Format("Conc of water Blankµg/ L(B)", ddlUnit.SelectedItem.Text);
                //        gvResultCations.Columns[2].HeaderText = String.Format("Conc of Sample µg/ L(C)", ddlUnit.SelectedItem.Text);
                //        gvResultCations.Columns[4].HeaderText = String.Format("Raw Results {0}", ddlUnit.SelectedItem.Text);
                //        gvResultCations.Columns[5].HeaderText = String.Format("Method Detection Limit  {0}", ddlUnit.SelectedItem.Text);
                //        gvResultCations.Columns[6].HeaderText = String.Format("Instrument Detection Limit  {0}", ddlUnit.SelectedItem.Text);
                //        gvResultCations.Columns[8].HeaderText = String.Format("Final Conc. of Sample  {0}", ddlUnit.SelectedItem.Text);

                //    }
                //    else
                //    {
                //        gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //        gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                //    }
                //    #endregion  
                //    calculateByFormular();
                //}
                //gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
                //gvAnionic.DataBind();
                //gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
                //gvCationic.DataBind();

                //gvResultAnions.DataSource = this.coverpages.Where(x => x.ic_type.Value == 1).ToList();
                //gvResultAnions.DataBind();
                //gvResultCations.DataSource = this.coverpages.Where(x => x.ic_type.Value == 2).ToList();
                //gvResultCations.DataBind();
            //}
            //else
            //{
            //    this.coverpages = new List<template_wd_ic_coverpage>();
            //}

            #endregion



            //Disable Save button
            btnCoverPage.CssClass = "btn blue";
            btnWorking.CssClass = "btn green";
            pCoverpage.Visible = true;
            pWorkingIC.Visible = true;
            pUploadfile.Visible = true;
            btnCoverPage.Visible = true;
            btnWorking.Visible = true;
            pLoadFile.Visible = true;

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
                this.ListSampleMethodProcedure = new List<sample_method_procedure>();
                initialPage();
            }
        }


        #region "METHOD/PROCEDURE GRIDVIEW (EVENT)"
        protected void gvMethodProcedure_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                //{
                //    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                //    template_pa_detail _cov = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.EVALUATION_OF_PARTICLES) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
                //    if (_cov != null)
                //    {
                //        RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), _cov.row_type.ToString(), true);
                //        switch (cmd)
                //        {
                //            case RowTypeEnum.Hide:
                //                _cov.row_status = Convert.ToInt32(RowTypeEnum.Hide);
                //                break;
                //            case RowTypeEnum.Normal:
                //                _cov.row_status = Convert.ToInt32(RowTypeEnum.Normal);
                //                break;
                //        }

                //        gvMethodProcedure.DataSource = PaDetail.Where(x => x.row_type == Convert.ToInt32(PAEnum.EVALUATION_OF_PARTICLES)).ToList();
                //        gvMethodProcedure.DataBind();
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }
        protected void gvMethodProcedure_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMethodProcedure.DataKeys[e.Row.RowIndex].Values[1]);

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

        protected void gvMethodProcedure_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvMethodProcedure_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = e.NewEditIndex;
            gvMethodProcedure.DataSource = this.ListSampleMethodProcedure;
            gvMethodProcedure.DataBind();
        }

        protected void gvMethodProcedure_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvMethodProcedure.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txt_col_1 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_1");
            TextBox txt_col_2 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_2");
            TextBox txt_col_3 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_3");
            TextBox txt_col_4 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_4");
            TextBox txt_col_5 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_5");
            TextBox txt_col_6 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_6");
            TextBox txt_col_7 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_7");
            TextBox txt_col_8 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_8");
            TextBox txt_col_9 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_9");
            TextBox txt_col_10 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_10");
            TextBox txt_col_11 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_11");
            TextBox txt_col_12 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_12");
            TextBox txt_col_13 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_13");
            TextBox txt_col_14 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_14");
            TextBox txt_col_15 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_15");
            TextBox txt_col_16 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_16");
            TextBox txt_col_17 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_17");
            TextBox txt_col_18 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_18");
            TextBox txt_col_19 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_19");
            TextBox txt_col_20 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_20");


            sample_method_procedure _smp = this.ListSampleMethodProcedure.Where(x => x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_smp != null)
            {
                _smp.col_1 = (txt_col_1 != null) ? txt_col_1.Text : String.Empty;
                _smp.col_2 = (txt_col_2 != null) ? txt_col_2.Text : String.Empty;
                _smp.col_3 = (txt_col_3 != null) ? txt_col_3.Text : String.Empty;
                _smp.col_4 = (txt_col_4 != null) ? txt_col_4.Text : String.Empty;
                _smp.col_5 = (txt_col_5 != null) ? txt_col_5.Text : String.Empty;
                _smp.col_6 = (txt_col_6 != null) ? txt_col_6.Text : String.Empty;
                _smp.col_7 = (txt_col_7 != null) ? txt_col_7.Text : String.Empty;
                _smp.col_8 = (txt_col_8 != null) ? txt_col_8.Text : String.Empty;
                _smp.col_9 = (txt_col_9 != null) ? txt_col_9.Text : String.Empty;
                _smp.col_10 = (txt_col_10 != null) ? txt_col_10.Text : String.Empty;
                _smp.col_11 = (txt_col_11 != null) ? txt_col_11.Text : String.Empty;
                _smp.col_12 = (txt_col_12 != null) ? txt_col_12.Text : String.Empty;
                _smp.col_13 = (txt_col_13 != null) ? txt_col_13.Text : String.Empty;
                _smp.col_14 = (txt_col_14 != null) ? txt_col_14.Text : String.Empty;
                _smp.col_15 = (txt_col_15 != null) ? txt_col_15.Text : String.Empty;
                _smp.col_16 = (txt_col_16 != null) ? txt_col_16.Text : String.Empty;
                _smp.col_17 = (txt_col_17 != null) ? txt_col_17.Text : String.Empty;
                _smp.col_18 = (txt_col_18 != null) ? txt_col_18.Text : String.Empty;
                _smp.col_19 = (txt_col_19 != null) ? txt_col_19.Text : String.Empty;
                _smp.col_20 = (txt_col_20 != null) ? txt_col_20.Text : String.Empty;


            }
            gvMethodProcedure.EditIndex = -1;
            gvMethodProcedure.DataSource = this.ListSampleMethodProcedure;
            gvMethodProcedure.DataBind();
        }

        protected void gvMethodProcedure_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = -1;
            gvMethodProcedure.DataSource = this.ListSampleMethodProcedure;
            gvMethodProcedure.DataBind();
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //calculateByFormular();
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
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    //foreach (template_wd_ic_coverpage _cover in this.coverpages)
                    //{
                    //    _cover.sample_id = this.SampleID;
                    //    //_cover.detail_spec_id = Convert.ToInt32(ddlDetailSpec.SelectedValue);
                    //    _cover.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                    //    _cover.b12 = txtB12.Text;
                    //    //_cover.ProcedureNo = txtProcedureNo.Text;
                    //    _cover.NumOfPiecesUsedForExtraction = txtB13.Text;
                    //    //_cover.ExtractionMedium = txtExtractionMedium.Text;
                    //    _cover.ExtractionVolume = txtB11.Text;
                    //}

                    template_wd_ic_coverpage.DeleteBySampleID(this.SampleID);
                    //template_wd_ic_coverpage.InsertList(this.coverpages);
                    this.jobSample.date_login_complete = DateTime.Now;
                    this.jobSample.date_chemist_analyze = DateTime.Now;
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_analyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    this.jobSample.date_srchemist_analyze = DateTime.Now;
                    this.jobSample.path_word = String.Empty;
                    this.jobSample.path_pdf = String.Empty;
                    //#endregion
                    //foreach (template_wd_ic_coverpage _cover in this.coverpages)
                    //{
                    //    _cover.sample_id = this.SampleID;
                    //    //_cover.detail_spec_id = Convert.ToInt32(ddlDetailSpec.SelectedValue);
                    //    _cover.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                    //    _cover.b12 = txtB12.Text;
                    //    //_cover.ProcedureNo = txtProcedureNo.Text;
                    //    _cover.NumOfPiecesUsedForExtraction = txtB13.Text;
                    //    //_cover.ExtractionMedium = txtExtractionMedium.Text;
                    //    _cover.ExtractionVolume = txtB11.Text;
                    //    //_cover.wunit = Convert.ToInt32(ddlUnit.SelectedValue);
                    //}
                    //template_wd_ic_coverpage.UpdateList(this.coverpages);
                    break;
                case StatusEnum.SR_CHEMIST_CHECKING:
                    StatusEnum srChemistApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (srChemistApproveStatus)
                    {
                        case StatusEnum.SR_CHEMIST_APPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                            #region ":: STAMP COMPLETE DATE"
                            this.jobSample.date_srchemist_complate = DateTime.Now;
                            this.jobSample.date_admin_word_inprogress = DateTime.Now;
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
                            this.jobSample.date_admin_pdf_inprogress = DateTime.Now;
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
                        this.jobSample.date_admin_word_complete = DateTime.Now;
                        this.jobSample.date_labman_analyze = DateTime.Now;
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
                        this.jobSample.date_admin_pdf_complete = DateTime.Now;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .pdf");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
                    //this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
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

        protected void btnCalulate_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            //calculateByFormular();
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

                    //txtNumOfPiecesUsedForExtraction.Text = txtB13.Text;
                    //txtExtractionVolume.Text = (String.IsNullOrEmpty(txtB11.Text) ? String.Empty : String.Format("{0}mL", Convert.ToInt32((1000 * Convert.ToDouble(txtB11.Text)))));
                    //calculateByFormular();
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

            //List<template_f_ic> ics = new List<template_f_ic>();
            this.workSheetData = new Hashtable();

            List<template_f_ic> removeList = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS) || x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS)).ToList();
            for (int i=0;i< removeList.Count; i++)
            {
                this.ics.Remove(removeList[i]);
            }


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


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }

                        _postedFile.SaveAs(source_file);



                        using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                        {
                            HSSFWorkbook wb = new HSSFWorkbook(fs);
                            ISheet isheet = wb.GetSheet("IC");
                            if (isheet == null)
                            {
                                errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", "IC"));
                            }
                            else
                            {
                                txtB11.Text = FreeTemplateUtil.GetCellValue(isheet, this.configs["ic.total.volume"].ToString());
                                txtB12.Text = FreeTemplateUtil.GetCellValue(isheet, this.configs["ic.surface.area"].ToString());
                                txtB13.Text = FreeTemplateUtil.GetCellValue(isheet, this.configs["ic.no.of.parts.extracted"].ToString());

                                #region "anions"
                                String[] anionsRanges = this.configs["ic.anions.ranges"].ToString().Split(FreeTemplateUtil.DELIMITER_SEMI_COLON);
                                int anionsColBegin = FreeTemplateUtil.GetColIndex(anionsRanges[0]);
                                int anionsColEnd = FreeTemplateUtil.GetColIndex(anionsRanges[1]);
                                int anionsRowBegin = FreeTemplateUtil.GetRowIndex(anionsRanges[0]);
                                int anionsRowEnd = FreeTemplateUtil.GetRowIndex(anionsRanges[1]);
                                for (int r = anionsRowBegin; r < anionsRowEnd; r++)
                                {
                                    template_f_ic ic = new template_f_ic();
                                    for (int c = anionsColBegin; c < anionsColEnd; c++)
                                    {
                                        String key = String.Format("{0}!{1}{2}", this.SheetWorkSheetName, FreeTemplateUtil.GetColName(c), (r + 1));
                                        String value = CustomUtils.GetCellValue(isheet.GetRow(r).GetCell(c));
                                        this.workSheetData[key] = String.Format("{0}", value);
                                        setValueToTemplate(ref ic, c, value);

                                    }
                                    ic.row_type = Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS);
                                    ic.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                                   this.ics.Add(ic);
                                }
                                #endregion
                                #region "cations"
                                String[] cationsRanges = this.configs["ic.cations.ranges"].ToString().Split(FreeTemplateUtil.DELIMITER_SEMI_COLON);
                                int cationsColBegin = FreeTemplateUtil.GetColIndex(anionsRanges[0]);
                                int cationsColEnd = FreeTemplateUtil.GetColIndex(anionsRanges[1]);
                                int cationsRowBegin = FreeTemplateUtil.GetRowIndex(anionsRanges[0]);
                                int cationsRowEnd = FreeTemplateUtil.GetRowIndex(anionsRanges[1]);
                                for (int r = cationsRowBegin; r < cationsRowEnd; r++)
                                {
                                    template_f_ic ic = new template_f_ic();

                                    for (int c = cationsColBegin; c < cationsColEnd; c++)
                                    {
                                        String key = String.Format("{0}!{1}{2}", this.SheetWorkSheetName, FreeTemplateUtil.GetColName(c), (r + 1));
                                        String value = CustomUtils.GetCellValue(isheet.GetRow(r).GetCell(c));
                                        this.workSheetData[key] = String.Format("{0}", value);
                                        setValueToTemplate(ref ic, c, value);

                                    }
                                    ic.row_type = Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS);
                                    ic.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                                    ics.Add(ic);
                                }
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
                    errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", this.SheetCoverPageName, CustomUtils.ErrorIndex));
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
                Cal();
            }
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {



            //DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);

            //List<template_wd_ic_coverpage> anionic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            //List<template_wd_ic_coverpage> cationic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();

            //ReportHeader reportHeader = ReportHeader.getReportHeder(this.jobSample);


            //ReportParameterCollection reportParameters = new ReportParameterCollection();

            //reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            //reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            //reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            //reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            //reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));
            //reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            //reportParameters.Add(new ReportParameter("DateAnalyzed", (reportHeader.dateOfAnalyze.Year == 1) ? reportHeader.cur_date.ToString("dd MMMM yyyy") : reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            //reportParameters.Add(new ReportParameter("DateTestCompleted", (reportHeader.dateOfTestComplete.Year == 1) ? reportHeader.cur_date.ToString("dd MMMM yyyy") : reportHeader.dateOfTestComplete.ToString("dd MMMM yyyy") + ""));
            //reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            //reportParameters.Add(new ReportParameter("Test", "IC"));
            //reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
            ////reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));
            //reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.jobSample.singapore_ref_no) ? " " : this.jobSample.singapore_ref_no)));

            //// Variables
            //Warning[] warnings;
            //string[] streamIds;
            //string mimeType = string.Empty;
            //string encoding = string.Empty;
            //string extension = string.Empty;

            //foreach (template_wd_ic_coverpage a in anionic)
            //{
            //    a.B = CustomUtils.isNumber(a.B) ? "<" + a.B : a.B;
            //}
            //foreach (template_wd_ic_coverpage b in cationic)
            //{
            //    b.B = CustomUtils.isNumber(b.B) ? "<" + b.B : b.B;
            //}
            //// Setup the report viewer object and get the array of bytes
            //ReportViewer viewer = new ReportViewer();
            //viewer.ProcessingMode = ProcessingMode.Local;
            //viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ic_wd.rdlc");
            //viewer.LocalReport.SetParameters(reportParameters);
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", anionic.ToDataTable())); // Add datasource here
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", cationic.ToDataTable())); // Add datasource here



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

            //            if (!Directory.Exists(Server.MapPath("~/Report/")))
            //            {
            //                Directory.CreateDirectory(Server.MapPath("~/Report/"));
            //            }
            //            using (FileStream fs = File.Create(Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension))
            //            {
            //                fs.Write(bytes, 0, bytes.Length);
            //            }

            //            #region "Insert Footer & Header from template"
            //            Document doc1 = new Document();
            //            doc1.LoadFromFile(Server.MapPath("~/template/") + "Blank Letter Head - EL.doc");
            //            Spire.Doc.HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
            //            Spire.Doc.HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
            //            Document doc2 = new Document(Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension);
            //            foreach (Section section in doc2.Sections)
            //            {
            //                foreach (DocumentObject obj in header.ChildObjects)
            //                {
            //                    section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
            //                }
            //                foreach (DocumentObject obj in footer.ChildObjects)
            //                {
            //                    section.HeadersFooters.Footer.ChildObjects.Add(obj.Clone());
            //                }
            //            }



            //            doc2.SaveToFile(Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension);
            //            #endregion
            //            Response.ContentType = mimeType;
            //            Response.AddHeader("Content-Disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
            //            Response.WriteFile(Server.MapPath("~/Report/" + this.jobSample.job_number + "." + extension));
            //            Response.Flush();

            //            #region "Delete After Download"
            //            String deleteFile1 = Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension;
            //            String deleteFile2 = Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension;

            //            if (File.Exists(deleteFile1))
            //            {
            //                File.Delete(deleteFile1);
            //            }
            //            if (File.Exists(deleteFile2))
            //            {
            //                File.Delete(deleteFile2);
            //            }
            //            #endregion
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
            //        break;
            //}





        }

        //protected void ddlDetailSpec_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    tb_m_detail_spec tem = new tb_m_detail_spec().SelectByID(int.Parse(ddlDetailSpec.SelectedValue));
        //    if (tem != null)
        //    {
        //        /*RESULT*/
        //        lbSpecDesc.Text = String.Format("The specification is based on Western Digital's document no. {0} {1}", tem.B, tem.A);

        //        gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", tem.C);
        //        gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", tem.C);
        //        gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", tem.C);
        //        gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", tem.C);
        //        gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", tem.C);
        //        gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", tem.C);

        //        List<template_wd_ic_coverpage> listCover = new List<template_wd_ic_coverpage>();
        //        #region "*Anionic*"
        //        template_wd_ic_coverpage _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 1;
        //        _tmp.A = "Fluoride as F";
        //        _tmp.B = tem.D;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 2;
        //        _tmp.A = "Chloride as Cl";
        //        _tmp.B = tem.E;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);

        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 3;
        //        _tmp.A = "Nitrite as NO2";
        //        _tmp.B = tem.F;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);

        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 4;
        //        _tmp.A = "Bromide as Br";
        //        _tmp.B = tem.G;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 5;
        //        _tmp.A = "Nitrate as NO3";
        //        _tmp.B = tem.H;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 6;
        //        _tmp.A = "Sulfate as SO4";
        //        _tmp.B = tem.I;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 7;
        //        _tmp.A = "Phosphate as PO4";
        //        _tmp.B = tem.J;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 8;
        //        _tmp.A = "Total Anions";
        //        _tmp.B = tem.K;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        #endregion
        //        #region "*Cationic*"

        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 8;
        //        _tmp.A = "Lithium as Li";
        //        _tmp.B = tem.M;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 9;
        //        _tmp.A = "Sodium as Na";
        //        _tmp.B = tem.P;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 10;
        //        _tmp.A = "Ammonium as NH4";
        //        _tmp.B = tem.L;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 11;
        //        _tmp.A = "Potassium as K";
        //        _tmp.B = tem.O;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 12;
        //        _tmp.A = "Magnesium as Mg";
        //        _tmp.B = tem.Q;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 13;
        //        _tmp.A = "Calcium as Ca";
        //        _tmp.B = tem.N;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        _tmp = new template_wd_ic_coverpage();
        //        _tmp.id = 14;
        //        _tmp.A = "Total Cations";
        //        _tmp.B = tem.R;
        //        _tmp.wunitText = tem.C;
        //        _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
        //        _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //        listCover.Add(_tmp);
        //        #endregion
        //        this.coverpages = listCover;
        //        gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
        //        gvAnionic.DataBind();
        //        gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
        //        gvCationic.DataBind();

        //        btnSubmit.Enabled = true;
        //    }
        //}

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_template template = new m_template().SelectByID(this.jobSample.template_id);

            //FileInfo fileInfo = new FileInfo(template.path_source_file);
            //using (var package = new ExcelPackage(fileInfo))
            //{
            //    FreeTemplateUtil ftu = new FreeTemplateUtil(fileInfo);
            //    this.configs = ftu.getConfigValue();
            //    this.listSpecificatons = ftu.getSpecification();
            //    this.wCoverPage = package.Workbook.Worksheets["Coverpage-TH"];
            //    this.wSpecification = package.Workbook.Worksheets["Specification"];
            //    String yyyy = this.wSpecification.Cells["F17"].Value.ToString();
            //    Console.WriteLine();
            //}

            int maxRow = Convert.ToInt16(this.configs["results.group.1.maxrow"].ToString());
            for (int r = 0; r < maxRow; r++)
            {
                try
                {
                    template_f_ic ic = new template_f_ic();
                    ic.id = (r + 1);
                    ic.row_type = 2;
                    String[] cols = this.configs["results.group.1.header.values"].ToString().Split(FreeTemplateUtil.DELIMITER);
                    for (int c = 0; c < cols.Length; c++)
                    {
                        String _val = this.configs[String.Format("results.group.1.row.{0}.col.{1}", (r + 1), (c + 1))].ToString();
                        setValueToTemplate(ref ic, c, _val);
                    }
                    this.ics.Add(ic);
                }
                catch (Exception ex) { }
            }
            Cal();

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

        //protected void gvAnionic_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        int PKID = Convert.ToInt32(gvAnionic.DataKeys[e.Row.RowIndex].Values[0].ToString());

        //        RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvAnionic.DataKeys[e.Row.RowIndex].Values[1]);
        //        LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
        //        LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
        //        Literal litSpecificationLimits = (Literal)e.Row.FindControl("litSpecificationLimits");

        //        if (_btnHide != null && _btnUndo != null)
        //        {
        //            switch (cmd)
        //            {
        //                case RowTypeEnum.Hide:

        //                    _btnHide.Visible = false;
        //                    _btnUndo.Visible = true;
        //                    e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
        //                    break;
        //                default:
        //                    _btnHide.Visible = true;
        //                    _btnUndo.Visible = false;
        //                    e.Row.ForeColor = System.Drawing.Color.Black;
        //                    break;
        //            }
        //            litSpecificationLimits.Text = litSpecificationLimits.Text.Equals("NA") ? litSpecificationLimits.Text : String.Format("{0}{1}", (litSpecificationLimits.Text.IndexOf("-") != -1 ? "" : "<"), litSpecificationLimits.Text);
        //        }
        //    }
        //}

        //protected void gvCationic_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        int PKID = Convert.ToInt32(gvCationic.DataKeys[e.Row.RowIndex].Values[0].ToString());
        //        RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCationic.DataKeys[e.Row.RowIndex].Values[1]);
        //        LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
        //        LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
        //        Literal litSpecificationLimits = (Literal)e.Row.FindControl("litSpecificationLimits");
        //        if (_btnHide != null && _btnUndo != null)
        //        {
        //            switch (cmd)
        //            {
        //                case RowTypeEnum.Hide:

        //                    _btnHide.Visible = false;
        //                    _btnUndo.Visible = true;
        //                    e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
        //                    break;
        //                default:
        //                    _btnHide.Visible = true;
        //                    _btnUndo.Visible = false;
        //                    e.Row.ForeColor = System.Drawing.Color.Black;
        //                    break;
        //            }
        //            litSpecificationLimits.Text = litSpecificationLimits.Text.Equals("NA") ? litSpecificationLimits.Text : String.Format("{0}{1}", (litSpecificationLimits.Text.IndexOf("-") != -1 ? "" : "<"), litSpecificationLimits.Text);

        //        }
        //    }
        //}

        //protected void gvAnionic_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
        //    if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
        //    {
        //        int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
        //        template_wd_ic_coverpage gcms = this.coverpages.Find(x => x.id == PKID);
        //        if (gcms != null)
        //        {
        //            switch (cmd)
        //            {
        //                case RowTypeEnum.Hide:
        //                    gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);

        //                    break;
        //                case RowTypeEnum.Normal:
        //                    gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //                    break;
        //            }
        //            gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
        //            gvAnionic.DataBind();
        //        }
        //    }
        //}

        //protected void gvCationic_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
        //    if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
        //    {
        //        int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
        //        template_wd_ic_coverpage gcms = this.coverpages.Find(x => x.id == PKID);
        //        if (gcms != null)
        //        {
        //            switch (cmd)
        //            {
        //                case RowTypeEnum.Hide:
        //                    gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);

        //                    break;
        //                case RowTypeEnum.Normal:
        //                    gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
        //                    break;
        //            }
        //            gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
        //            gvCationic.DataBind();
        //        }
        //    }
        //}

        private void Cal()
        {


            GridView1.DataSource = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.METHOD_PROCECURE));
            GridView1.DataBind();
            GridView3.DataSource = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS));
            GridView3.DataBind();
            GridView4.DataSource = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS));
            GridView4.DataBind();



            //Decimal b11 = Convert.ToDecimal(String.IsNullOrEmpty(txtB11.Text) ? "0" : txtB11.Text);
            //Decimal b12 = Convert.ToDecimal(String.IsNullOrEmpty(txtB12.Text) ? "0" : txtB12.Text);
            //Decimal b13 = Convert.ToDecimal(String.IsNullOrEmpty(txtB13.Text) ? "0" : txtB13.Text);

            //if (b11 != 0 && b12 != 0 && b13 != 0)
            //{

            //    gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            //    gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            //    gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            //    gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            //    gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            //    gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));

            //}
            //try
            //{
            //    foreach (template_wd_ic_coverpage _val in this.coverpages)
            //    {

            //        if (!String.IsNullOrEmpty(_val.B))
            //        {
            //            if (_val.wf != null)
            //            {
            //                String secValue = _val.B.Split(' ')[0];
            //                _val.E = (secValue.Equals("NA") || (secValue.Equals("-")) ? "NA" : (CustomUtils.isNumber(_val.wj) ? Convert.ToDouble(_val.wj) : 0) < Convert.ToDouble(secValue) ? "PASS" : "FAIL");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("");
            //}


            //gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
            //gvAnionic.DataBind();
            //gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
            //gvCationic.DataBind();
        }

        //protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
        //    gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
        //    gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
        //    gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
        //    gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
        //    gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));


        //    gvResultAnions.Columns[1].HeaderText = String.Format("Conc of water Blankµg/ L(B)", ddlUnit.SelectedItem.Text);
        //    gvResultAnions.Columns[2].HeaderText = String.Format("Conc of Sample µg/ L(C)", ddlUnit.SelectedItem.Text);
        //    gvResultAnions.Columns[4].HeaderText = String.Format("Raw Results {0}", ddlUnit.SelectedItem.Text);
        //    gvResultAnions.Columns[5].HeaderText = String.Format("Method Detection Limit  {0}", ddlUnit.SelectedItem.Text);
        //    gvResultAnions.Columns[6].HeaderText = String.Format("Instrument Detection Limit  {0}", ddlUnit.SelectedItem.Text);
        //    gvResultAnions.Columns[8].HeaderText = String.Format("Final Conc. of Sample  {0}", ddlUnit.SelectedItem.Text);

        //    gvResultCations.Columns[1].HeaderText = String.Format("Conc of water Blankµg/ L(B)", ddlUnit.SelectedItem.Text);
        //    gvResultCations.Columns[2].HeaderText = String.Format("Conc of Sample µg/ L(C)", ddlUnit.SelectedItem.Text);
        //    gvResultCations.Columns[4].HeaderText = String.Format("Raw Results {0}", ddlUnit.SelectedItem.Text);
        //    gvResultCations.Columns[5].HeaderText = String.Format("Method Detection Limit  {0}", ddlUnit.SelectedItem.Text);
        //    gvResultCations.Columns[6].HeaderText = String.Format("Instrument Detection Limit  {0}", ddlUnit.SelectedItem.Text);
        //    gvResultCations.Columns[8].HeaderText = String.Format("Final Conc. of Sample  {0}", ddlUnit.SelectedItem.Text);
        //    ModolPopupExtender.Show();
        //    calculateByFormular();
        //}

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        protected void OK_Click(object sender, EventArgs e)
        {

        }


        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "WD");
            }
            else
            {
                //tb_m_detail_spec tem = new tb_m_detail_spec().SelectByID(int.Parse(ddlDetailSpec.SelectedValue));
                //if (tem != null)
                //{
                //    lbSpecDesc.Text = String.Format("The specification is based on Western Digital's document no. {0} {1}", tem.B, tem.A);
                //}
            }

        }


        private void setValueToTemplate(ref template_f_ic ic, int c, String _val)
        {
            switch (c + 1)
            {
                case 1: ic.col_1 = _val; break;
                case 2: ic.col_2 = _val; break;
                case 3: ic.col_3 = _val; break;
                case 4: ic.col_4 = _val; break;
                case 5: ic.col_5 = _val; break;
                case 6: ic.col_6 = _val; break;
                case 7: ic.col_7 = _val; break;
                case 8: ic.col_8 = _val; break;
                case 9: ic.col_9 = _val; break;
                case 10: ic.col_10 = _val; break;
                case 11: ic.col_11 = _val; break;
                case 12: ic.col_12 = _val; break;
                case 13: ic.col_13 = _val; break;
                case 14: ic.col_14 = _val; break;
                case 15: ic.col_15 = _val; break;
                case 16: ic.col_16 = _val; break;
                case 17: ic.col_17 = _val; break;
                case 18: ic.col_18 = _val; break;
                case 19: ic.col_19 = _val; break;
                case 20: ic.col_20 = _val; break;
            }
        }


    }
}