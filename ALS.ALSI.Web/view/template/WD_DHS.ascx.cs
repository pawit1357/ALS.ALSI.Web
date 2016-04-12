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
using System.Collections;
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
    public partial class WD_DHS : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_DHS));
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        #region "Property"
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
        public List<tb_m_dhs_cas> tbCas
        {
            get { return (List<tb_m_dhs_cas>)Session[GetType().Name + "tbCas"]; }
            set { Session[GetType().Name + "tbCas"] = value; }
        }

        public List<template_wd_dhs_coverpage> coverpages
        {
            get { return (List<template_wd_dhs_coverpage>)Session[GetType().Name + "coverpages"]; }
            set { Session[GetType().Name + "coverpages"] = value; }
        }
        public List<template_wd_dhs_coverpage> reportCovers
        {
            get { return (List<template_wd_dhs_coverpage>)Session[GetType().Name + "reportCovers"]; }
            set { Session[GetType().Name + "reportCovers"] = value; }
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

        List<String> errors = new List<string>();

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
            tb_m_detail_spec detailSpec = new tb_m_detail_spec();
            detailSpec.specification_id = this.jobSample.specification_id;
            detailSpec.template_id = this.jobSample.template_id;

            tb_m_component comp = new tb_m_component();
            comp.specification_id = this.jobSample.specification_id;
            comp.template_id = this.jobSample.template_id;

            this.coverpages = template_wd_dhs_coverpage.FindAllBySampleID(this.SampleID);

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt16(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF) + ""));

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = detailSpec.SelectAll();
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlComponent.Items.Clear();
            ddlComponent.DataSource = comp.SelectAll();
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            #region "CAS"
            this.tbCas = tb_m_dhs_cas.FindAllBySampleID(this.SampleID);
            if (this.tbCas != null && this.tbCas.Count > 0 && this.coverpages != null && this.coverpages.Count > 0)
            {
                foreach (tb_m_dhs_cas _cas in this.tbCas)
                {
                    _cas.RowState = CommandNameEnum.Edit;
                }


                txtProcedureNo.Text = this.coverpages[0].pm_procedure_no;
                txtNumberOfPiecesUsedForExtraction.Text = this.coverpages[0].pm_number_of_pieces_used_for_extraction;
                txtExtractionMedium.Text = this.coverpages[0].pm_extraction_medium;
                txtExtractionVolume.Text = this.coverpages[0].pm_extraction_volume;



                gvResult.DataSource = this.tbCas;
                gvResult.DataBind();
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
                #region "METHOD/PROCEDURE:"
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
                    txtNumberOfPiecesUsedForExtraction.Enabled = true;
                    txtExtractionMedium.Enabled = true;
                    txtExtractionVolume.Enabled = true;
                    gvCoverPages.Columns[5].Visible = true;
                    btnCoverPage.Visible = true;
                    btnDHS.Visible = true;
                }
                else
                {
                    txtProcedureNo.Enabled = false;
                    txtNumberOfPiecesUsedForExtraction.Enabled = false;
                    txtExtractionMedium.Enabled = false;
                    txtExtractionVolume.Enabled = false;
                    gvCoverPages.Columns[5].Visible = false;
                    btnCoverPage.Visible = false;
                    btnDHS.Visible = false;
                }
                #endregion
                #region "COVERPAGE"
                if (this.coverpages != null && this.coverpages.Count > 0)
                {
                    this.CommandName = CommandNameEnum.Edit;
                    ddlComponent.SelectedValue = this.coverpages[0].component_id.ToString();
                    ddlSpecification.SelectedValue = this.coverpages[0].detail_spec_id.ToString();

                    txtProcedureNo.Text = this.coverpages[0].pm_procedure_no;
                    txtNumberOfPiecesUsedForExtraction.Text = this.coverpages[0].pm_number_of_pieces_used_for_extraction;
                    txtExtractionMedium.Text = this.coverpages[0].pm_extraction_medium;
                    txtExtractionVolume.Text = this.coverpages[0].pm_extraction_volume;

                    tb_m_detail_spec _detailSpec = new tb_m_detail_spec().SelectByID(this.coverpages[0].detail_spec_id.Value);// this.coverpages[0].tb_m_detail_spec;
                    if (_detailSpec != null)
                    {
                        lbDocRev.Text = _detailSpec.B;
                        lbDesc.Text = _detailSpec.A;
                    }
                    GenerrateCoverPage();
                }
                #endregion
            }

            #endregion


            //initial button.
            btnCoverPage.CssClass = "btn blue";
            btnDHS.CssClass = "btn green";
            pCoverpage.Visible = true;
            pDSH.Visible = false;

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

        #region "Button"

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

                    #region "Cover Page#"
                    foreach (template_wd_dhs_coverpage _val in this.coverpages)
                    {
                        _val.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        _val.pm_procedure_no = txtProcedureNo.Text;
                        _val.pm_number_of_pieces_used_for_extraction = txtNumberOfPiecesUsedForExtraction.Text;
                        _val.pm_extraction_medium = txtExtractionMedium.Text;
                        _val.pm_extraction_volume = txtExtractionVolume.Text;
                    }

                    template_wd_dhs_coverpage.DeleteBySampleID(this.SampleID);
                    template_wd_dhs_coverpage.InsertList(this.coverpages);

                    #endregion
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    if (this.tbCas.Count > 0)
                    {
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                        this.jobSample.step3owner = userLogin.id;

                        //#region ":: STAMP COMPLETE DATE"
                        this.jobSample.date_chemist_complete = DateTime.Now;
                        //#endregion
                        #region "CAS#"
                        tb_m_dhs_cas.DeleteBySampleID(this.SampleID);
                        tb_m_dhs_cas.InsertList(this.tbCas);

                        #endregion
                        #region "Cover Page#"
                        foreach (template_wd_dhs_coverpage _val in this.coverpages)
                        {
                            _val.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                            _val.pm_procedure_no = txtProcedureNo.Text;
                            _val.pm_number_of_pieces_used_for_extraction = txtNumberOfPiecesUsedForExtraction.Text;
                            _val.pm_extraction_medium = txtExtractionMedium.Text;
                            _val.pm_extraction_volume = txtExtractionVolume.Text;
                        }
                        template_wd_dhs_coverpage.UpdateList(this.coverpages);
                    }
                    else
                    {
                        errors.Add("ไม่พบข้อมูล WorkSheet");
                    }
                    #endregion
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
                    if (FileUpload1.HasFile && (Path.GetExtension(FileUpload1.FileName).Equals(".doc") || Path.GetExtension(FileUpload1.FileName).Equals(".docx")))
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
                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add( "Invalid File. Please upload a File with extension .doc|.docx");
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

                removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }




        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.PreviousPath);
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            string sheetName = string.Empty;

            List<tb_m_dhs_cas> _cas = new List<tb_m_dhs_cas>();
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
                        String source_file_url = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(btnUpload.FileName));

                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        _postedFile.SaveAs(source_file);

                        if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")))
                        {
                            using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                            {
                                HSSFWorkbook wb = new HSSFWorkbook(fs);

                                ISheet isheet = wb.GetSheet(ConfigurationManager.AppSettings["wd.dhs.excel.sheetname.dhs"]);
                                if (isheet == null)
                                {
                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["wd.dhs.excel.sheetname.dhs"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    for (int j = 15; j < isheet.LastRowNum; j++)
                                    {
                                        if (isheet.GetRow(j) != null)
                                        {
                                            tb_m_dhs_cas tmp = new tb_m_dhs_cas();
                                            tmp.ID = j;
                                            tmp.sample_id = this.SampleID;
                                            tmp.pk = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(0));
                                            tmp.rt = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1));
                                            tmp.library_id = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(2));
                                            tmp.classification = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(3));
                                            tmp.cas = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(4));
                                            tmp.qual = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(5));
                                            tmp.area = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                            tmp.amount = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(7));

                                            String compare_date = (string.IsNullOrEmpty(tmp.pk) ? "0" : "1") + "" +
                                               (string.IsNullOrEmpty(tmp.rt) ? "0" : "1") + "" +
                                               (string.IsNullOrEmpty(tmp.library_id) ? "0" : "1") + "" +
                                               (string.IsNullOrEmpty(tmp.classification) ? "0" : "1") + "" +
                                               (string.IsNullOrEmpty(tmp.cas) ? "0" : "1") + "" +
                                               (string.IsNullOrEmpty(tmp.qual) ? "0" : "1") + "" +
                                               (string.IsNullOrEmpty(tmp.area) ? "0" : "1") + "" +
                                               (string.IsNullOrEmpty(tmp.amount) ? "0" : "1");

                                            Boolean bAdd = true;
                                            switch (compare_date)
                                            {
                                                case "11111110":
                                                    tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                                                    break;
                                                case "11111111"://NORMAL
                                                    tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                                                    tmp.amount = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(7))), Convert.ToInt16(txtDecimal01.Text)).ToString();
                                                    break;
                                                case "00010011"://TOTAL
                                                    tmp.row_type = Convert.ToInt32(RowTypeEnum.TotalRow);
                                                    tmp.amount = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(7))), Convert.ToInt16(txtDecimal01.Text)).ToString();
                                                    break;
                                                case "00100011"://TOTAL OUT GAS
                                                    tmp.row_type = Convert.ToInt32(RowTypeEnum.TotalOutGas);
                                                    tmp.amount = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(7))), Convert.ToInt16(txtDecimal02.Text)).ToString();
                                                    break;
                                                case "00100010"://SAMPLE SIZE
                                                    tmp.row_type = Convert.ToInt32(RowTypeEnum.SampleSize);
                                                    break;
                                                default:
                                                    bAdd = false;
                                                    break;
                                            }
                                            if (bAdd)
                                            {
                                                _cas.Add(tmp);
                                            }
                                            

                                        }
                                    }
                                }
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));

                        }


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
                gvResult.DataSource = this.tbCas;
                gvResult.DataBind();
            }
        }


        protected void btnCoverPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnCoverPage.CssClass = "btn green";
                    btnDHS.CssClass = "btn blue";
                    pCoverpage.Visible = true;
                    pDSH.Visible = false;
                    //Generate cover-pagee
                    GenerrateCoverPage();
                    break;
                case "DHS":
                    btnCoverPage.CssClass = "btn blue";
                    btnDHS.CssClass = "btn green";
                    pCoverpage.Visible = false;
                    pDSH.Visible = true;
                    break;
            }
        }

        #endregion

        #region "COVERPAGES GRID."

        protected void gvCoverPages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvCoverPages.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCoverPages.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                Literal _litRequiredTest = (Literal)e.Row.FindControl("litRequiredTest");
                Label _lbAnalytes = (e.Row.FindControl("litAnalytes") as Label);
                //Literal _litResult = (Literal)e.Row.FindControl("litResult");

                //แก้เรื่องซ่อนในหน้า cover page
                //if (_litResult != null)
                //{
                //    _litResult.Text = String.IsNullOrEmpty(_litResult.Text) || _litResult.Text.Equals("Not Detected") ? "Not Detected" : Convert.ToDecimal(_litResult.Text).ToString("N2");
                //}
                if (_btnHide != null && _btnUndo != null && _litRequiredTest != null)
                {
                    switch (cmd)
                    {
                        case RowTypeEnum.Hide:

                            _btnHide.Visible = false;
                            _btnUndo.Visible = (_lbAnalytes.Text.StartsWith(Constants.CHAR_DASH)) ? false : true;
                            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                            break;
                        default:
                            _btnHide.Visible = _lbAnalytes.Text.StartsWith(Constants.CHAR_DASH) ? false : true;
                            _btnUndo.Visible = false;
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                    if (e.Row.RowIndex == 0)
                    {
                        _litRequiredTest.Text = "DHS";

                    }
                }
                if (_lbAnalytes != null)
                {
                    if (_lbAnalytes.Text.StartsWith(Constants.CHAR_DASH))
                    {
                        _lbAnalytes.Attributes.CssStyle.Add("margin-left", "15px");
                    }
                }
            }


        }

        protected void gvCoverPages_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {

                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_wd_dhs_coverpage gcms = this.coverpages.Find(x => x.ID == PKID);

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
                    GenerrateCoverPage();
                }
            }

        }

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_detail_spec detailSpec = new tb_m_detail_spec();
            detailSpec.specification_id = this.jobSample.specification_id;
            detailSpec.template_id = this.jobSample.template_id;



            List<tb_m_detail_spec> _headerDs = detailSpec.SelectAll().Take(3).ToList();
            List<tb_m_detail_spec> headerDs = new List<tb_m_detail_spec>();
            headerDs.Add(_headerDs[1]);
            headerDs.Add(_headerDs[2]);


            detailSpec = detailSpec.SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (detailSpec != null)
            {
                List<int> ignoreIndex = new List<int>() { 0 };

                List<template_wd_dhs_coverpage> tmp = new List<template_wd_dhs_coverpage>();
                foreach (DetailSpecComponent item in CustomUtils.GetComponent(headerDs, detailSpec, ignoreIndex))
                {
                    template_wd_dhs_coverpage work = new template_wd_dhs_coverpage();
                    work.sample_id = this.SampleID;
                    work.detail_spec_id = detailSpec.ID;
                    work.analytes = item.name;
                    work.specification_limits = item.value;
                    work.specification_limits = ((work.specification_limits.Equals(Constants.GetEnumDescription(ResultEnum.NA)) || work.specification_limits.Equals(Constants.GetEnumDescription(ResultEnum.ND))) ? work.specification_limits : String.Format("<{0}", work.specification_limits));
                    work.result = string.Empty;
                    work.result_pass_or_false = String.Empty;
                    work.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                    tmp.Add(work);
                }

                //Result Description
                gvCoverPages.Columns[2].HeaderText = String.Format("Specification Limits ,({0})", detailSpec.C);
                gvCoverPages.Columns[3].HeaderText = String.Format("Results,({0})", detailSpec.C);
                lbDocRev.Text = detailSpec.B;
                lbDesc.Text = detailSpec.A;
                //lbResultDesc.Text = String.Format("The specification is based on Western Digital's document no {0} for {1}", detailSpec.B, detailSpec.A);
                //this.Unit = CustomUtils.getUnitByName(detailSpec.C);

                //lbD18.Text = component.D;
                this.coverpages = tmp;
                gvCoverPages.DataSource = this.coverpages;
                gvCoverPages.DataBind();

            }
        }

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_component component = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (component != null)
            {
                txtProcedureNo.Text = component.B;
                txtNumberOfPiecesUsedForExtraction.Text = component.D;
                //txtExtractionMedium.Text = String.Empty;
                txtExtractionVolume.Text = component.E;
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

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            //DataTable dt = Extenders.ObjectToDataTable(this.reportCovers[0]);
            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);
            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1 + reportHeader.addr2));
            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", "DHS"));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The specification is based on Western Digital's document no. {0} for {1}", lbDocRev.Text, lbDesc.Text)));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/dhs_wd.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", this.reportCovers.ToDataTable())); // Add datasource here

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
                        objWorPdf.InputLocation = String.Format("{0}{1}", Configurations.PATH_DRIVE, this.jobSample.path_word);
                        objWorPdf.OutputLocation = String.Format("{0}{1}", Configurations.PATH_DRIVE, this.jobSample.path_word).Replace("doc", "pdf");
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

        #endregion

        #region "DHS GRID."
        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvResult_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvResult.EditIndex = e.NewEditIndex;
            //gvResult.Columns[2].Visible = true;
            //CalculateCas(false);
            //DropDownList _ddlLibrary = (DropDownList)gvResult.Rows[e.NewEditIndex].FindControl("ddlLibrary");

            //_ddlLibrary.DataSource = this.libs;
            //_ddlLibrary.DataValueField = "id";
            //_ddlLibrary.DataTextField = "SelectedText";
            //_ddlLibrary.DataBind();
        }

        protected void gvResult_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvResult.EditIndex = -1;
            //CalculateCas(false);
        }



        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int _id = Convert.ToInt32(gvResult.DataKeys[e.Row.RowIndex].Values[0].ToString());

                //Literal _litAmount = (Literal)e.Row.FindControl("litAmount");

                //if (_litAmount != null)
                //{
                //    _litAmount.Text = String.IsNullOrEmpty(_litAmount.Text) ? "" : Convert.ToDecimal(_litAmount.Text).ToString("N2");
                //}

                tb_m_dhs_cas _cas = this.tbCas.Find(x => x.ID == _id);
                if (_cas != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)_cas.row_type);
                    switch (cmd)
                    {
                        case RowTypeEnum.Normal:
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                        case RowTypeEnum.SampleSize:
                        case RowTypeEnum.TotalOutGas:
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
            if (this.tbCas != null && this.tbCas.Count > 0)
            {

                List<template_wd_dhs_coverpage> newCoverPage = new List<template_wd_dhs_coverpage>();
                foreach (template_wd_dhs_coverpage _cover in this.coverpages)
                {
                    tb_m_dhs_cas _cas = this.tbCas.Where(x => x.classification.Equals(_cover.analytes) && x.row_type.Value == Convert.ToInt32(RowTypeEnum.TotalRow)).FirstOrDefault();
                    if (_cas != null)
                    {
                        _cover.result = _cas.amount.ToString();
                    }
                    if (_cover.analytes.Equals("Hydrocarbon Hump"))
                    {
                        _cas = this.tbCas.Where(x => x.classification.Equals(_cover.analytes)).FirstOrDefault();
                        if (_cas != null)
                        {
                            _cover.result = _cas.amount.ToString();
                        }
                    }
                    if (_cover.analytes.StartsWith("Total Outgassing"))
                    {
                        _cas = this.tbCas.Where(x => x.library_id.StartsWith("Total Outgassing")).FirstOrDefault();
                        if (_cas != null)
                        {
                            _cover.result = _cas.amount.ToString();
                        }
                    }

                    newCoverPage.Add(_cover);
                    #region "ADD CHILD"
                    switch (_cover.analytes.Trim())
                    {
                        case "Total Unknown":
                            //Skip
                            break;
                        case "Total Others":
                            //Take 2 Max Result
                            List<tb_m_dhs_cas> childsTake2 = this.tbCas.Where(x => x.classification.Equals(_cover.analytes) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList().OrderByDescending(x => x.amount).Take(2).ToList();
                            foreach (tb_m_dhs_cas child in childsTake2)
                            {
                                template_wd_dhs_coverpage work = new template_wd_dhs_coverpage();
                                work.ID = CustomUtils.GetRandomNumberID();
                                work.detail_spec_id = _cover.detail_spec_id;
                                work.sample_id = this.SampleID;
                                work.analytes = "- " + child.library_id;
                                work.result = child.amount == null ? string.Empty : child.amount.ToString();
                                work.result_pass_or_false = String.Empty;
                                work.specification_limits = "-";
                                work.row_type = _cover.row_type;
                                newCoverPage.Add(work);
                            }
                            break;
                        case "Hydrocarbon Hump":
                            //Skip
                            break;
                        default:
                            List<tb_m_dhs_cas> childs = this.tbCas.Where(x => x.classification.Equals(_cover.analytes.Replace("Total", "").Trim()) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
                            foreach (tb_m_dhs_cas child in childs)
                            {
                                template_wd_dhs_coverpage work = new template_wd_dhs_coverpage();
                                work.ID = CustomUtils.GetRandomNumberID();
                                work.detail_spec_id = _cover.detail_spec_id;
                                work.sample_id = this.SampleID;
                                work.analytes = "- " + child.library_id;
                                work.result = child.amount == null ? string.Empty : child.amount.ToString();
                                work.result_pass_or_false = String.Empty;
                                work.specification_limits = "-";
                                work.row_type = _cover.row_type;
                                newCoverPage.Add(work);
                            }
                            break;
                    }
                    #endregion
                }
                foreach (template_wd_dhs_coverpage _cover in newCoverPage)
                {

                    double _limit = CustomUtils.isNumber(_cover.specification_limits.Replace("<", "").Trim()) ? Convert.ToDouble(_cover.specification_limits.Replace("<", "").Trim()) : 0;
                    double _result = CustomUtils.isNumber(_cover.result) ? Convert.ToDouble(_cover.result) : 0;
                    _cover.result = CustomUtils.isNumber(_cover.result) ? Convert.ToDouble(_cover.result).ToString() : "Not Detected";



                    _cover.result_pass_or_false = (_cover.specification_limits.Equals("NA") || _cover.specification_limits.Equals("-")) ? "" :
                        (_cover.result.Equals("Not Detected") || (_result < _limit) || _cover.specification_limits.Equals("ND")) ? "PASS" : "FAIL";

                    if (_cover.analytes.Equals("Total Acrylate and Methacrylate"))
                    {
                        _cover.result_pass_or_false = "NA";
                    }

                }

                this.reportCovers = newCoverPage;
                gvCoverPages.DataSource = newCoverPage;
                gvCoverPages.DataBind();
            }
            else
            {
                gvCoverPages.DataSource = this.coverpages;
                gvCoverPages.DataBind();
            }

        }
        #endregion

        protected void _ddlRotType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlLibrary = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddlLibrary.NamingContainer;

            TextBox _txtLibrary = (TextBox)gvr.FindControl("_txtLibrary");
            TextBox _txtClassification = (TextBox)gvr.FindControl("_txtClassification");
            TextBox _txtCas = (TextBox)gvr.FindControl("_txtCas");
            TextBox _txtQual = (TextBox)gvr.FindControl("_txtQual");
            TextBox _txtArea = (TextBox)gvr.FindControl("_txtArea");
            TextBox _txtAmout = (TextBox)gvr.FindControl("_txtAmout");
            DropDownList _ddlRotType = (DropDownList)gvr.FindControl("_ddlRotType");
            RowTypeEnum rowType = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), Convert.ToInt32(_ddlRotType.SelectedValue));
            switch (rowType)
            {
                case RowTypeEnum.CoverPageValue:
                    _txtLibrary.Visible = false;
                    _txtClassification.Visible = true;
                    _txtCas.Visible = false;
                    _txtQual.Visible = false;
                    _txtArea.Visible = false;
                    _txtAmout.Visible = true;
                    break;
                default:
                    _txtLibrary.Visible = true;
                    _txtClassification.Visible = true;
                    _txtCas.Visible = true;
                    _txtQual.Visible = true;
                    _txtArea.Visible = true;
                    _txtAmout.Visible = false;
                    break;
            }


        }

        protected void lbPk_Click(object sender, EventArgs e)
        {
            gvResult.DataSource = this.tbCas.OrderBy(x => x.pkInt).ToList();
            gvResult.DataBind();
        }

        protected void lbArea_Click(object sender, EventArgs e)
        {
            gvResult.DataSource = this.tbCas.OrderBy(x => x.areaInt).ToList();
            gvResult.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }




    }
}

