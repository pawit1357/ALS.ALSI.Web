﻿using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Biz.ReportObjects;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using ALS.ALSI.Web.view.request;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using WordToPDF;
using ALSALSI.Biz;
using System.Text;

namespace ALS.ALSI.Web.view.template
{
    public partial class Seagate_LPC : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Seagate_LPC));

        //#region "Property"
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
        public List<template_seagate_lpc_coverpage> Lpcs
        {
            get { return (List<template_seagate_lpc_coverpage>)Session[GetType().Name + "Lpcs"]; }
            set { Session[GetType().Name + "Lpcs"] = value; }
        }
        //public List<template_seagate_lpc_coverpage> ListResult
        //{
        //    get { return (List<template_seagate_lpc_coverpage>)Session[GetType().Name + "ListResult"]; }
        //    set { Session[GetType().Name + "ListResult"] = value; }
        //}
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
            Session.Remove(GetType().Name + "Lpcs");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
        }

        private void initialPage()
        {

            this.CommandName = CommandNameEnum.Add;

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = new tb_m_specification().SelectBySpecificationID(this.jobSample.specification_id, this.jobSample.template_id);
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt16(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF) + ""));




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
                //gvCoverPages.Columns[3].Visible = false;
                //gvResult.Columns[8].Visible = false;

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
                            //gvCoverPages.Columns[3].Visible = false;
                            //gvResult.Columns[8].Visible = false;
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
                            //gvCoverPages.Columns[3].Visible = true;
                            //gvResult.Columns[8].Visible = true;
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
                            //gvCoverPages.Columns[3].Visible = false;
                            //gvResult.Columns[8].Visible = true;
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
                            //gvCoverPages.Columns[3].Visible = false;
                            //gvResult.Columns[8].Visible = false;
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
                            //btnCalculate.Visible = false;
                            //gvCoverPages.Columns[3].Visible = false;
                            //gvResult.Columns[8].Visible = false;
                        }
                        break;
                }
                #region "METHOD/PROCEDURE:"

                if (status == StatusEnum.CHEMIST_TESTING ||
                    status == StatusEnum.SR_CHEMIST_CHECKING ||
                    status == StatusEnum.LOGIN_SELECT_SPEC
                    && userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST) ||
                    userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
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


                    ddlA19.Enabled = true;
                    txtB19.Enabled = true;
                    txtCVP_C19.Enabled = true;
                    txtD19.Enabled = true;
                    txtCVP_E19.Enabled = true;

                    btnCoverPage.Visible = true;
                    btnWorkSheet.Visible = true;
                    CheckBoxList1.Enabled = true;
                }
                else
                {
                    CheckBoxList1.Enabled = true;
                    ddlA19.Enabled = true;
                    txtB19.Enabled = false;
                    txtCVP_C19.Enabled = false;
                    txtD19.Enabled = false;
                    txtCVP_E19.Enabled = false;

                    btnCoverPage.Visible = false;
                    btnWorkSheet.Visible = false;

                }
                #endregion

                if (status == StatusEnum.LOGIN_SELECT_SPEC)
                {
                    template_seagate_lpc_coverpage objWork = new template_seagate_lpc_coverpage();
                    objWork.DeleteBySampleID(this.SampleID);
                    GeneralManager.Commit();

                }

            }

            #endregion

            #region "WorkingSheet"

            this.Lpcs = template_seagate_lpc_coverpage.FindAllBySampleID(this.SampleID);
            if (this.Lpcs != null && this.Lpcs.Count > 0)
            {

                //FORM COVER PAGE
                txtB19.Text = this.Lpcs[0].ProcedureNo;
                txtCVP_C19.Text = this.Lpcs[0].NumberOfPieces;
                txtD19.Text = this.Lpcs[0].ExtractionMedium;
                txtCVP_E19.Text = this.Lpcs[0].ExtractionVolume;

                lbExtractionVol.Text = txtCVP_E19.Text;
                txtSurfaceArea.Text = this.Lpcs[0].SurfaceArea.ToString();
                lbNoOfPartsUsed.Text = txtCVP_C19.Text;

                lbExtractionVol05.Text = lbExtractionVol.Text;
                txtSurfaceArea05.Text = txtSurfaceArea.Text;
                lbNoOfPartsUsed05.Text = lbNoOfPartsUsed.Text;

                lbExtractionVol06.Text = lbExtractionVol.Text;
                txtSurfaceArea06.Text = txtSurfaceArea.Text;
                lbNoOfPartsUsed06.Text = lbNoOfPartsUsed.Text;

                ddlA19.SelectedValue = this.Lpcs[0].lpc_type;
                //ddlChannel.SelectedValue = this.Lpcs[0].channel_size;
                ddlTemplateType.SelectedValue = this.Lpcs[0].template_type.ToString();

                tb_m_specification tem = new tb_m_specification().SelectByID(this.Lpcs[0].specification_id.Value);

                if (tem != null)
                {
                    switch (ddlTemplateType.SelectedValue)
                    {
                        case "1":
                            lbDocNo.Text = tem.C + "" + tem.D;
                            lbCommodity.Text = tem.B;
                            break;
                        case "2":
                            lbDocNo.Text = tem.C;
                            lbCommodity.Text = tem.B;
                            break;
                    }
                }


                var results = this.Lpcs.GroupBy(n => new { n.channel_size })
                .Select(g => new
                {
                    g.Key.channel_size
                }).ToList();

                p03.Visible = false;
                p05.Visible = false;
                p06.Visible = false;
                CheckBoxList1.Items[0].Selected = false;
                CheckBoxList1.Items[1].Selected = false;
                CheckBoxList1.Items[2].Selected = false;

                foreach (var item in results)
                {
                    switch (item.channel_size)
                    {
                        case "0.300":
                            CheckBoxList1.Items[0].Selected = true;
                            gvCoverPage03.DataSource = this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals("0.300")).ToList();
                            gvCoverPage03.DataBind();

                            gvWorkSheet_03.Visible = true;
                            gvCoverPage03.Visible = true;
                            gvCoverPage03.Columns[0].HeaderText = String.Format("Liquid Particle Count ({0})", ddlA19.SelectedItem.Text);
                            p03.Visible = true;

                            break;
                        case "0.500":
                            CheckBoxList1.Items[1].Selected = true;
                            gvCoverPage05.DataSource = this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals("0.500")).ToList();
                            gvCoverPage05.DataBind();

                            gvWorkSheet_05.Visible = true;
                            gvCoverPage05.Visible = true;
                            gvCoverPage05.Columns[0].HeaderText = String.Format("Liquid Particle Count ({0})", ddlA19.SelectedItem.Text);
                            p05.Visible = true;
                            break;
                        case "0.600":
                            CheckBoxList1.Items[2].Selected = true;
                            gvCoverPage06.DataSource = this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals("0.600")).ToList();
                            gvCoverPage06.DataBind();
                            gvWorkSheet_06.Visible = true;
                            gvCoverPage06.Visible = true;
                            gvCoverPage06.Columns[0].HeaderText = String.Format("Liquid Particle Count ({0})", ddlA19.SelectedItem.Text);
                            p06.Visible = true;

                            break;
                    }
                    Console.WriteLine();
                }




                CalculateCas();
            }
            else
            {
                gvWorkSheet_03.Visible = true;
                gvWorkSheet_05.Visible = true;
                gvWorkSheet_06.Visible = true;
                gvCoverPage03.Visible = true;
                gvCoverPage05.Visible = true;
                gvCoverPage06.Visible = true;
                gvCoverPage03.Columns[0].HeaderText = String.Format("Liquid Particle Count ({0})", ddlA19.SelectedItem.Text);
                gvCoverPage05.Columns[0].HeaderText = String.Format("Liquid Particle Count ({0})", ddlA19.SelectedItem.Text);
                gvCoverPage06.Columns[0].HeaderText = String.Format("Liquid Particle Count ({0})", ddlA19.SelectedItem.Text);
                p03.Visible = true;
                p05.Visible = true;
                p06.Visible = true;
            }

            #endregion



            //initial component
            btnCoverPage.CssClass = "btn blue";
            btnWorkSheet.CssClass = "btn green";

            pCoverpage.Visible = true;
            pDSH.Visible = false;
            //pUS_LPC03.Visible = false;
            //pUS_LPC06.Visible = false;
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


        List<String> errors = new List<String>();
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
            Boolean isValid = true;
            template_seagate_lpc_coverpage objWork = new template_seagate_lpc_coverpage();

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    this.jobSample.step1owner = userLogin.id;
                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                    this.jobSample.step2owner = userLogin.id;

                    foreach (template_seagate_lpc_coverpage _tmp in this.Lpcs)
                    {
                        _tmp.sample_id = this.jobSample.ID;
                        _tmp.lpc_type = ddlA19.SelectedValue;
                        _tmp.specification_id = Convert.ToInt16(ddlSpecification.SelectedValue);
                        _tmp.ProcedureNo = txtB19.Text;
                        _tmp.NumberOfPieces = txtCVP_C19.Text;
                        _tmp.ExtractionMedium = txtD19.Text;
                        _tmp.ExtractionVolume = txtCVP_E19.Text;
                        //_tmp.channel_size = ddlChannel.SelectedValue;
                        _tmp.template_type = Convert.ToInt16(ddlTemplateType.SelectedValue);
                        _tmp.row_state = 1;//Cover Page
                    }

                    objWork.DeleteBySampleID(this.SampleID);
                    objWork.InsertList(this.Lpcs);

                    break;
                case StatusEnum.CHEMIST_TESTING:
                    if (this.Lpcs.Count > 0)
                    {
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                        this.jobSample.step3owner = userLogin.id;
                        //#region ":: STAMP COMPLETE DATE"
                        this.jobSample.date_chemist_complete = DateTime.Now;
                        //#endregion
                        int spec_id = this.Lpcs[0].specification_id.Value;
                        string lpc_type = this.Lpcs[0].lpc_type;
                        //string ch_size = this.Lpcs[0].channel_size;
                        foreach (template_seagate_lpc_coverpage _tmp in this.Lpcs)
                        {
                            _tmp.sample_id = this.jobSample.ID;
                            _tmp.lpc_type = lpc_type;
                            _tmp.specification_id = spec_id;
                            _tmp.ProcedureNo = txtB19.Text;
                            _tmp.NumberOfPieces = txtCVP_C19.Text;
                            _tmp.ExtractionMedium = txtD19.Text;
                            _tmp.ExtractionVolume = txtCVP_E19.Text;
                            //_tmp.channel_size = ch_size;
                            String surfaceArea = "0";
                            if (CheckBoxList1.Items[0].Selected)
                            {
                                surfaceArea = txtSurfaceArea.Text;

                            }
                            if (CheckBoxList1.Items[1].Selected)
                            {
                                surfaceArea = txtSurfaceArea05.Text;

                            }
                            if (CheckBoxList1.Items[2].Selected)
                            {
                                surfaceArea = txtSurfaceArea06.Text;

                            }

                            _tmp.SurfaceArea = Convert.ToDouble(surfaceArea);
                            _tmp.template_type = Convert.ToInt16(ddlTemplateType.SelectedValue);
                        }

                        objWork.DeleteBySampleID(this.SampleID);
                        objWork.InsertList(this.Lpcs);
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
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        isValid = false;
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

                removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }



        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //removeSession();
            Response.Redirect(this.PreviousPath);

        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            string sheetName = string.Empty;

            #region "LOAD"
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");

            List<template_seagate_lpc_coverpage> listSeagateLpc = new List<template_seagate_lpc_coverpage>();

            List<template_seagate_lpc_coverpage> lpcs03 = new List<template_seagate_lpc_coverpage>();
            List<template_seagate_lpc_coverpage> lpcs05 = new List<template_seagate_lpc_coverpage>();
            List<template_seagate_lpc_coverpage> lpcs06 = new List<template_seagate_lpc_coverpage>();

            for (int i = 0; i < btnUpload.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = btnUpload.PostedFiles[i];
                try
                {
                    if (_postedFile.ContentLength > 0)
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
                                HSSFWorkbook wd = new HSSFWorkbook(fs);
                                ISheet isheet = wd.GetSheet("Sheet1");
                                int run = 0;
                                for (int row = 17; row < 120; row++)
                                {
                                    if (isheet.GetRow(row) != null) //null is when the row only contains empty cells 
                                    {
                                        if (CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.F)).Equals("Sample #"))
                                        {
                                            run++;
                                            Console.WriteLine();
                                        }
                                        if (String.IsNullOrEmpty(CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.C))))
                                        {
                                            template_seagate_lpc_coverpage lpc = new template_seagate_lpc_coverpage();

                                            switch (CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.J)))
                                            {
                                                case "0.300":
                                                    lpc.RunNumber = run;
                                                    lpc.Run = run + "";
                                                    lpc.type = Path.GetFileNameWithoutExtension(_postedFile.FileName);
                                                    lpc.channel_size = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.J));
                                                    lpc.Results = String.Format(getDecimalFormat(Convert.ToInt16(txtDecimal01.Text)), Math.Round(Convert.ToDouble(CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.S))), _postedFile.FileName.StartsWith("B") ? Convert.ToInt16(txtDecimal01.Text) : Convert.ToInt16(txtDecimal02.Text)));
                                                    lpc.row_state = 2;//WorkSheet
                                                    lpcs03.Add(lpc);
                                                    break;
                                                case "0.500":
                                                    lpc.RunNumber = run;
                                                    lpc.Run = run + "";
                                                    lpc.type = Path.GetFileNameWithoutExtension(_postedFile.FileName);
                                                    lpc.channel_size = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.J));
                                                    lpc.Results = String.Format(getDecimalFormat(Convert.ToInt16(txtDecimal01.Text)), Math.Round(Convert.ToDouble(CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.S))), _postedFile.FileName.StartsWith("B") ? Convert.ToInt16(txtDecimal01.Text) : Convert.ToInt16(txtDecimal02.Text)));
                                                    lpc.row_state = 2;//WorkSheet
                                                    lpcs05.Add(lpc);
                                                    break;
                                                case "0.600":
                                                    lpc.RunNumber = run;
                                                    lpc.Run = run + "";
                                                    lpc.type = Path.GetFileNameWithoutExtension(_postedFile.FileName);
                                                    lpc.channel_size = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.J));
                                                    lpc.Results = String.Format(getDecimalFormat(Convert.ToInt16(txtDecimal01.Text)), Math.Round(Convert.ToDouble(CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.S))), _postedFile.FileName.StartsWith("B") ? Convert.ToInt16(txtDecimal01.Text) : Convert.ToInt16(txtDecimal02.Text)));
                                                    lpc.row_state = 2;//WorkSheet
                                                    lpcs06.Add(lpc);

                                                    break;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));
                    }
                    Console.WriteLine();
                }
                catch (Exception Ex)
                {
                    errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", sheetName, CustomUtils.ErrorIndex));
                    //logger.Error(Ex.Message);
                    Console.WriteLine();
                }
            }
            #endregion


            //New Order
            if (lpcs03.Count > 0 && CheckBoxList1.Items[0].Selected)
            {
                this.Lpcs.AddRange(lpcs03);

                if (String.IsNullOrEmpty(txtSurfaceArea.Text))
                {
                    errors.Add("Surface Area (cm2) is empty.");
                }
                if (String.IsNullOrEmpty(txtDilutionFactor.Text))
                {
                    errors.Add("Dilution Factor (time) is empty.");
                }
            }
            if ( lpcs05.Count > 0 && CheckBoxList1.Items[1].Selected)
            {
                this.Lpcs.AddRange(lpcs05);

                if (String.IsNullOrEmpty(txtSurfaceArea05.Text))
                {
                    errors.Add("Surface Area (cm2) is empty.");
                }
                if (String.IsNullOrEmpty(txtDilutionFactor05.Text))
                {
                    errors.Add("Dilution Factor (time) is empty.");
                }
            }
            if ( lpcs06.Count > 0 && CheckBoxList1.Items[2].Selected)
            {
                this.Lpcs.AddRange(lpcs06);


                if (String.IsNullOrEmpty(txtSurfaceArea06.Text))
                {
                    errors.Add("Surface Area (cm2) is empty.");
                }
                if (String.IsNullOrEmpty(txtDilutionFactor06.Text))
                {
                    errors.Add("Dilution Factor (time) is empty.");
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
            //CalculateCas();
            Console.WriteLine("-END-");

        }

        protected void btnUsLPC_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkSheet.CssClass = "btn green";
                    pCoverpage.Visible = true;
                    pDSH.Visible = false;
                    CalculateCas();
                    break;
                case "WorkSheet":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkSheet.CssClass = "btn green";

                    pCoverpage.Visible = false;
                    pDSH.Visible = true;
                    lbExtractionVol.Text = txtCVP_E19.Text;
                    lbNoOfPartsUsed.Text = txtCVP_C19.Text;

                    lbExtractionVol05.Text = txtCVP_E19.Text;
                    lbNoOfPartsUsed05.Text = txtCVP_C19.Text;

                    lbExtractionVol06.Text = txtCVP_E19.Text;
                    lbNoOfPartsUsed06.Text = txtCVP_C19.Text;

                    break;
            }
        }

        #region "Custom method"


        private void CalculateCas()
        {

            string channel_size = "0.300";
            #region "0.300"
            if (CheckBoxList1.Items[0].Selected)
            {
                if (this.Lpcs.Where(x => x.row_state == 2 && x.channel_size.Equals(channel_size)).ToList().Count > 0)
                {
                    string lastSampleCount = this.Lpcs.Where(x => x.row_state.Value == 2 && !String.IsNullOrEmpty(x.type) && x.channel_size.Equals(channel_size)).Max(x => x.type);
                    if (lastSampleCount != null)
                    {
                        #region "RUN RESULT"
                        List<template_seagate_lpc_coverpage> runResults = new List<template_seagate_lpc_coverpage>();
                        lastSampleCount = lastSampleCount.Replace("Sample ", "").Trim().Replace("S", "").Trim();
                        for (int i = 1; i <= Convert.ToInt16(lastSampleCount); i++)
                        {
                            runResults.AddRange(this.Lpcs.Where(x => !String.IsNullOrEmpty(x.type) && x.type.Equals("B" + i) && x.channel_size.Equals(channel_size)).ToList());
                            runResults.AddRange(this.Lpcs.Where(x => !String.IsNullOrEmpty(x.type) && x.type.Equals("S" + i) && x.channel_size.Equals(channel_size)).ToList());

                        }
                        var AverageOfLast3 = from lpc in runResults group lpc by lpc.type into newGroup orderby newGroup.Key select newGroup;
                        List<template_seagate_lpc_coverpage> last3Results = new List<template_seagate_lpc_coverpage>();
                        foreach (var item in AverageOfLast3)
                        {
                            template_seagate_lpc_coverpage lpc = new template_seagate_lpc_coverpage();
                            //Average of last 3
                            lpc.RunNumber = 5;
                            lpc.Run = "Average of last 3";
                            lpc.type = item.Key;
                            lpc.channel_size = channel_size;
                            double _value = runResults.Where(x => x.type.Equals(item.Key) && x.RunNumber > 1 && x.channel_size.Equals(channel_size)).Average(x => Convert.ToDouble(x.Results));
                            lpc.Results = String.Format(getDecimalFormat(Convert.ToInt16(txtDecimal03.Text)), Math.Round(_value, Convert.ToInt16(txtDecimal03.Text)));
                            last3Results.Add(lpc);
                        }
                        runResults.AddRange(last3Results);
                        DataTable dt = PivotTable.GetInversedDataTable(runResults.Where(x => x.channel_size.Equals(channel_size)).ToDataTable(), "Type", "Run", "Results", "-", false);
                        gvWorkSheet_03.DataSource = dt;
                        gvWorkSheet_03.DataBind();
                        #endregion
                        #region "AVERAGE"
                        var listAverage = from lpc in last3Results where !String.IsNullOrEmpty(lpc.type) && lpc.type.StartsWith("B") group lpc by lpc.type into newGroup orderby newGroup.Key select newGroup;
                        List<LPC> listAverages = new List<LPC>();

                        int index = 1;
                        foreach (var item in listAverage)
                        {
                            LPC lpc = new LPC();
                            //Average of last 3
                            lpc.RunNumber = 6;
                            lpc.Run = "";
                            lpc.Sample = "";
                            lpc.type = item.Key.Replace("Blank", "");
                            lpc.ChannelSize = channel_size;
                            template_seagate_lpc_coverpage lpcBlank = last3Results.Where(x => x.RunNumber == 5 && x.type.Equals("B" + index)).FirstOrDefault();
                            template_seagate_lpc_coverpage lpcSaple = last3Results.Where(x => x.RunNumber == 5 && x.type.Equals("S" + index)).FirstOrDefault();


                            double _value = (Convert.ToDouble(lpcSaple.Results) - Convert.ToDouble(lpcBlank.Results)) * Convert.ToDouble(lbExtractionVol.Text) / (Convert.ToDouble(txtSurfaceArea.Text) * Convert.ToDouble(lbNoOfPartsUsed.Text)) * Convert.ToDouble(txtDilutionFactor.Text);

                            lpc.Value = String.Format(getDecimalFormat(Convert.ToInt16(txtDecimal05.Text)), Math.Round(_value, Convert.ToInt16(txtDecimal05.Text)));
                            listAverages.Add(lpc);
                            index++;
                        }
                        DataTable dtAverages = PivotTable.GetInversedDataTable(listAverages.Where(x => x.ChannelSize.Equals(channel_size)).ToDataTable(), "Type", "Sample", "Value", "No. of Particles ≥ " + channel_size.Substring(0, 3) + " μm (Counts/mL)", false);
                        gvWorkSheetAverage.DataSource = dtAverages;
                        gvWorkSheetAverage.DataBind();
                        #endregion
                        #region "AVG"
                        double average = listAverages.Average(x => Convert.ToDouble(x.Value));
                        lbAverage.Text = average.ToString().Split('.')[0];

                        List<template_seagate_lpc_coverpage> listCoverPage = this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals(channel_size)).ToList();
                        if (listCoverPage.Count > 0)
                        {
                            listCoverPage[1].Results = listAverages[0].Value;
                            listCoverPage[2].Results = listAverages[1].Value;
                            listCoverPage[3].Results = listAverages[2].Value;
                            listCoverPage[4].Results = lbAverage.Text;

                            gvCoverPage03.DataSource = listCoverPage;
                            gvCoverPage03.DataBind();
                        }

                        #endregion

                        Console.WriteLine();
                    }
                }
                else
                {

                    gvCoverPage03.DataSource = this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals("0.300")).ToList();
                    gvCoverPage03.DataBind();
                }
            }
            #endregion
            channel_size = "0.500";
            #region "0.500"
            if (CheckBoxList1.Items[1].Selected)
            {
                if (this.Lpcs.Where(x => x.row_state == 2 && x.channel_size.Equals(channel_size)).ToList().Count > 0)
                {
                    string lastSampleCount = this.Lpcs.Where(x => x.row_state.Value == 2 && !String.IsNullOrEmpty(x.type) && x.channel_size.Equals(channel_size)).Max(x => x.type);
                    if (lastSampleCount != null)
                    {
                        #region "RUN RESULT"
                        List<template_seagate_lpc_coverpage> runResults = new List<template_seagate_lpc_coverpage>();
                        lastSampleCount = lastSampleCount.Replace("Sample ", "").Trim().Replace("S", "").Trim();
                        for (int i = 1; i <= Convert.ToInt16(lastSampleCount); i++)
                        {
                            runResults.AddRange(this.Lpcs.Where(x => !String.IsNullOrEmpty(x.type) && x.type.Equals("B" + i) && x.channel_size.Equals(channel_size)).ToList());
                            runResults.AddRange(this.Lpcs.Where(x => !String.IsNullOrEmpty(x.type) && x.type.Equals("S" + i) && x.channel_size.Equals(channel_size)).ToList());

                        }
                        var AverageOfLast3 = from lpc in runResults group lpc by lpc.type into newGroup orderby newGroup.Key select newGroup;
                        List<template_seagate_lpc_coverpage> last3Results = new List<template_seagate_lpc_coverpage>();
                        foreach (var item in AverageOfLast3)
                        {
                            template_seagate_lpc_coverpage lpc = new template_seagate_lpc_coverpage();
                            //Average of last 3
                            lpc.RunNumber = 5;
                            lpc.Run = "Average of last 3";
                            lpc.type = item.Key;
                            lpc.channel_size = channel_size;
                            double _value = runResults.Where(x => x.type.Equals(item.Key) && x.RunNumber > 1 && x.channel_size.Equals(channel_size)).Average(x => Convert.ToDouble(x.Results));
                            lpc.Results = String.Format(getDecimalFormat(Convert.ToInt16(txtDecimal03.Text)), Math.Round(_value, Convert.ToInt16(txtDecimal03.Text)));
                            last3Results.Add(lpc);
                        }
                        runResults.AddRange(last3Results);
                        DataTable dt = PivotTable.GetInversedDataTable(runResults.Where(x => x.channel_size.Equals(channel_size)).ToDataTable(), "Type", "Run", "Results", "-", false);
                        gvWorkSheet_05.DataSource = dt;
                        gvWorkSheet_05.DataBind();
                        #endregion
                        #region "AVERAGE"
                        var listAverage = from lpc in last3Results where !String.IsNullOrEmpty(lpc.type) && lpc.type.StartsWith("B") group lpc by lpc.type into newGroup orderby newGroup.Key select newGroup;
                        List<LPC> listAverages = new List<LPC>();

                        int index = 1;
                        foreach (var item in listAverage)
                        {
                            LPC lpc = new LPC();
                            //Average of last 3
                            lpc.RunNumber = 6;
                            lpc.Run = "";
                            lpc.Sample = "";
                            lpc.type = item.Key.Replace("Blank", "");
                            lpc.ChannelSize = channel_size;
                            template_seagate_lpc_coverpage lpcBlank = last3Results.Where(x => x.RunNumber == 5 && x.type.Equals("B" + index)).FirstOrDefault();
                            template_seagate_lpc_coverpage lpcSaple = last3Results.Where(x => x.RunNumber == 5 && x.type.Equals("S" + index)).FirstOrDefault();


                            double _value = (Convert.ToDouble(lpcSaple.Results) - Convert.ToDouble(lpcBlank.Results)) * Convert.ToDouble(lbExtractionVol.Text) / (Convert.ToDouble(txtSurfaceArea05.Text) * Convert.ToDouble(lbNoOfPartsUsed.Text)) * Convert.ToDouble(txtDilutionFactor05.Text);

                            lpc.Value = String.Format(getDecimalFormat(Convert.ToInt16(txtDecimal03.Text)), Math.Round(_value, Convert.ToInt16(txtDecimal03.Text)));
                            listAverages.Add(lpc);
                            index++;
                        }
                        DataTable dtAverages = PivotTable.GetInversedDataTable(listAverages.Where(x => x.ChannelSize.Equals(channel_size)).ToDataTable(), "Type", "Sample", "Value", "No. of Particles ≥ " + channel_size.Substring(0, 3) + " μm (Counts/mL)", false);
                        gvWorkSheetAverage05.DataSource = dtAverages;
                        gvWorkSheetAverage05.DataBind();
                        #endregion
                        #region "AVG"
                        double average = listAverages.Average(x => Convert.ToDouble(x.Value));
                        lbAverage05.Text = average.ToString().Split('.')[0];

                        List<template_seagate_lpc_coverpage> listCoverPage = this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals(channel_size)).ToList();
                        if (listCoverPage.Count > 0)
                        {
                            listCoverPage[1].Results = listAverages[0].Value;
                            listCoverPage[2].Results = listAverages[1].Value;
                            listCoverPage[3].Results = listAverages[2].Value;
                            listCoverPage[4].Results = lbAverage05.Text;

                            gvCoverPage05.DataSource = listCoverPage;
                            gvCoverPage05.DataBind();
                        }

                        #endregion

                        Console.WriteLine();
                    }
                }
                else
                {

                    gvCoverPage05.DataSource = this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals("0.500")).ToList();
                    gvCoverPage05.DataBind();
                }
            }
            #endregion
            channel_size = "0.600";
            #region "0.600"
            if (CheckBoxList1.Items[0].Selected)
            {
                if (this.Lpcs.Where(x => x.row_state == 2 && x.channel_size.Equals(channel_size)).ToList().Count > 0)
                {
                    string lastSampleCount = this.Lpcs.Where(x => x.row_state.Value == 2 && !String.IsNullOrEmpty(x.type) && x.channel_size.Equals(channel_size)).Max(x => x.type);
                    if (lastSampleCount != null)
                    {
                        #region "RUN RESULT"
                        List<template_seagate_lpc_coverpage> runResults = new List<template_seagate_lpc_coverpage>();
                        lastSampleCount = lastSampleCount.Replace("Sample ", "").Trim().Replace("S", "").Trim();
                        for (int i = 1; i <= Convert.ToInt16(lastSampleCount); i++)
                        {
                            runResults.AddRange(this.Lpcs.Where(x => !String.IsNullOrEmpty(x.type) && x.type.Equals("B" + i) && x.channel_size.Equals(channel_size)).ToList());
                            runResults.AddRange(this.Lpcs.Where(x => !String.IsNullOrEmpty(x.type) && x.type.Equals("S" + i) && x.channel_size.Equals(channel_size)).ToList());

                        }
                        var AverageOfLast3 = from lpc in runResults group lpc by lpc.type into newGroup orderby newGroup.Key select newGroup;
                        List<template_seagate_lpc_coverpage> last3Results = new List<template_seagate_lpc_coverpage>();
                        foreach (var item in AverageOfLast3)
                        {
                            template_seagate_lpc_coverpage lpc = new template_seagate_lpc_coverpage();
                            //Average of last 3
                            lpc.RunNumber = 5;
                            lpc.Run = "Average of last 3";
                            lpc.type = item.Key;
                            lpc.channel_size = channel_size;
                            double _value = runResults.Where(x => x.type.Equals(item.Key) && x.RunNumber > 1 && x.channel_size.Equals(channel_size)).Average(x => Convert.ToDouble(x.Results));
                            lpc.Results = String.Format(getDecimalFormat(Convert.ToInt16(txtDecimal03.Text)), Math.Round(_value, Convert.ToInt16(txtDecimal03.Text)));
                            last3Results.Add(lpc);
                        }
                        runResults.AddRange(last3Results);
                        DataTable dt = PivotTable.GetInversedDataTable(runResults.Where(x => x.channel_size.Equals(channel_size)).ToDataTable(), "Type", "Run", "Results", "-", false);
                        gvWorkSheet_06.DataSource = dt;
                        gvWorkSheet_06.DataBind();
                        #endregion
                        #region "AVERAGE"
                        var listAverage = from lpc in last3Results where !String.IsNullOrEmpty(lpc.type) && lpc.type.StartsWith("B") group lpc by lpc.type into newGroup orderby newGroup.Key select newGroup;
                        List<LPC> listAverages = new List<LPC>();

                        int index = 1;
                        foreach (var item in listAverage)
                        {
                            LPC lpc = new LPC();
                            //Average of last 3
                            lpc.RunNumber = 6;
                            lpc.Run = "";
                            lpc.Sample = "";
                            lpc.type = item.Key.Replace("Blank", "");
                            lpc.ChannelSize = channel_size;
                            template_seagate_lpc_coverpage lpcBlank = last3Results.Where(x => x.RunNumber == 5 && x.type.Equals("B" + index)).FirstOrDefault();
                            template_seagate_lpc_coverpage lpcSaple = last3Results.Where(x => x.RunNumber == 5 && x.type.Equals("S" + index)).FirstOrDefault();


                            double _value = (Convert.ToDouble(lpcSaple.Results) - Convert.ToDouble(lpcBlank.Results)) * Convert.ToDouble(lbExtractionVol.Text) / (Convert.ToDouble(txtSurfaceArea06.Text) * Convert.ToDouble(lbNoOfPartsUsed.Text)) * Convert.ToDouble(txtDilutionFactor06.Text);

                            lpc.Value = String.Format(getDecimalFormat(Convert.ToInt16(txtDecimal03.Text)), Math.Round(_value, Convert.ToInt16(txtDecimal03.Text)));
                            listAverages.Add(lpc);
                            index++;
                        }
                        DataTable dtAverages = PivotTable.GetInversedDataTable(listAverages.Where(x => x.ChannelSize.Equals(channel_size)).ToDataTable(), "Type", "Sample", "Value", "No. of Particles ≥ " + channel_size.Substring(0, 3) + " μm (Counts/mL)", false);
                        gvWorkSheetAverage06.DataSource = dtAverages;
                        gvWorkSheetAverage06.DataBind();
                        #endregion
                        #region "AVG"
                        double average = listAverages.Average(x => Convert.ToDouble(x.Value));
                        lbAverage06.Text = average.ToString().Split('.')[0];

                        List<template_seagate_lpc_coverpage> listCoverPage = this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals(channel_size)).ToList();
                        if (listCoverPage.Count > 0)
                        {
                            listCoverPage[1].Results = listAverages[0].Value;
                            listCoverPage[2].Results = listAverages[1].Value;
                            listCoverPage[3].Results = listAverages[2].Value;
                            listCoverPage[4].Results = lbAverage06.Text;

                            gvCoverPage06.DataSource = listCoverPage;
                            gvCoverPage06.DataBind();
                        }

                        #endregion

                        Console.WriteLine();
                    }
                }
                else
                {

                    gvCoverPage06.DataSource = this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals("0.600")).ToList();
                    gvCoverPage06.DataBind();
                }
            }
            #endregion

        }

        #endregion

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculateCas();
            btnSubmit.Enabled = true;
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            template_seagate_lpc_coverpage objCoverPage = new template_seagate_lpc_coverpage();
            DataTable dt = Extenders.ObjectToDataTable(this.Lpcs[0]);
            List<ReportLPC> reportLpcs = objCoverPage.generateReport(this.Lpcs);
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            //GenerateHtmlBiz genHtmlBiz = new GenerateHtmlBiz();
            //genHtmlBiz.reportHeader = reportHeader;
            //genHtmlBiz.download();



            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2)); reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze + ""));
            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", ddlA19.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on Seagate's Doc {0} for {1}", lbDocNo.Text, lbCommodity.Text)));


            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/lpc_seagate.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", this.Lpcs.Where(x => x.row_state == 1 && x.channel_size.Equals("0.300")).ToList().ToDataTable())); // Add datasource here
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", reportLpcs.Where(x => x.particle_type == Convert.ToInt16(ParticleTypeEnum.PAR_06)).ToDataTable())); // Add datasource here




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

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_specification tem = new tb_m_specification().SelectByID(int.Parse(ddlSpecification.SelectedValue));

            if (tem != null)
            {
                int template_type = Convert.ToInt16(ddlTemplateType.SelectedValue);
                string lpc_type = ddlA19.SelectedValue;


                #region "0.300"
                List<template_seagate_lpc_coverpage> results03 = new List<template_seagate_lpc_coverpage>();
                results03.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.300",
                    LiquidParticleCount = "Total number of particles ≥ 0.3μm ",
                    SpecificationLimits = "",
                    Results = ""
                });
                results03.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.300",
                    LiquidParticleCount = "1st Run",
                    SpecificationLimits = "",
                    Results = ""
                });

                results03.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.300",
                    LiquidParticleCount = "2nd Run",
                    SpecificationLimits = "",
                    Results = ""
                });

                results03.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.300",
                    LiquidParticleCount = "3rd Run",
                    SpecificationLimits = "",
                    Results = ""
                });
                #endregion
                #region "0.500"
                List<template_seagate_lpc_coverpage> results05 = new List<template_seagate_lpc_coverpage>();
                results05.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.500",
                    LiquidParticleCount = "Total number of particles ≥ 0.5μm ",
                    SpecificationLimits = "",
                    Results = ""
                });
                results05.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.500",
                    LiquidParticleCount = "1st Run",
                    SpecificationLimits = "",
                    Results = ""
                });

                results05.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.500",
                    LiquidParticleCount = "2nd Run",
                    SpecificationLimits = "",
                    Results = ""
                });

                results05.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.500",
                    LiquidParticleCount = "3rd Run",
                    SpecificationLimits = "",
                    Results = ""
                });
                #endregion
                #region "0.600"
                List<template_seagate_lpc_coverpage> results06 = new List<template_seagate_lpc_coverpage>();
                results06.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.600",
                    LiquidParticleCount = "Total number of particles ≥ 0.6μm ",
                    SpecificationLimits = "",
                    Results = ""
                });
                results06.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.600",
                    LiquidParticleCount = "1st Run",
                    SpecificationLimits = "",
                    Results = ""
                });

                results06.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.600",
                    LiquidParticleCount = "2nd Run",
                    SpecificationLimits = "",
                    Results = ""
                });

                results06.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.600",
                    LiquidParticleCount = "3rd Run",
                    SpecificationLimits = "",
                    Results = ""
                });
                #endregion

                /*
                "3" > LPC
                "1" > LPC(68 KHz)      
                "2" > LPC(132 KHz)            
                "4" > LPC(Tray)
                */
                results03.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.300",
                    LiquidParticleCount = "Average",
                    //SpecificationLimits = lpc_type.Equals("1") ? tem.F : lpc_type.Equals("2") ? tem.H : lpc_type.Equals("3") ? tem.D : tem.D,//Original
                    SpecificationLimits = lpc_type.Equals("1") ? tem.F : lpc_type.Equals("2") ? tem.H : lpc_type.Equals("3") ? tem.F : "-",

                    Results = ""
                });
                results05.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.500",
                    LiquidParticleCount = "Average",
                    SpecificationLimits = lpc_type.Equals("1") ? "-" : lpc_type.Equals("2") ? "-" : lpc_type.Equals("3") ? tem.G : "-",
                    Results = ""
                });
                results06.Add(new template_seagate_lpc_coverpage
                {
                    channel_size = "0.600",
                    LiquidParticleCount = "Average",
                    SpecificationLimits = lpc_type.Equals("1") ? tem.G : lpc_type.Equals("2") ? tem.I : lpc_type.Equals("3") ? tem.H : "-",
                    Results = ""
                });

               switch(ddlTemplateType.SelectedValue)
                {
                    case "1":
                        lbDocNo.Text = tem.C + "" + tem.D;
                        lbCommodity.Text = tem.B;
                        break;
                    case "2":
                        lbDocNo.Text = tem.C;
                        lbCommodity.Text = tem.B;
                        break;
                }
                //if (lpc_type.Equals("1") || lpc_type.Equals("2"))
                //{
                //    lbDocNo.Text = tem.C +""+tem.D;
                //    lbCommodity.Text = tem.B;
                //}
                //else
                //{
                //    lbDocNo.Text = tem.C;
                //    lbCommodity.Text = tem.B;
                //}

                List<template_seagate_lpc_coverpage> tmp = new List<template_seagate_lpc_coverpage>();

                if (CheckBoxList1.Items[0].Selected)
                {
                    tmp.AddRange(results03);
                    gvCoverPage03.DataSource = results03;
                    gvCoverPage03.DataBind();
                    gvCoverPage03.Visible = true;
                    p03.Visible = true;

                }
                if (CheckBoxList1.Items[1].Selected)
                {
                    tmp.AddRange(results05);
                    gvCoverPage05.DataSource = results05;
                    gvCoverPage05.DataBind();
                    gvCoverPage05.Visible = true;
                    p03.Visible = true;
                }
                if (CheckBoxList1.Items[2].Selected)
                {
                    tmp.AddRange(results06);
                    gvCoverPage06.DataSource = results06;
                    gvCoverPage06.DataBind();
                    gvCoverPage06.Visible = true;
                    p06.Visible = true;
                }
                this.Lpcs = tmp;

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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        //protected void ddlChannel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lbParticle.Text = ddlChannel.SelectedItem.Text;
        //}

        protected void txtSurfaceArea_TextChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            p03.Visible = CheckBoxList1.Items[0].Selected;
            p05.Visible = CheckBoxList1.Items[1].Selected;
            p06.Visible = CheckBoxList1.Items[2].Selected;

            gvCoverPage03.Visible = CheckBoxList1.Items[0].Selected;
            gvCoverPage05.Visible = CheckBoxList1.Items[1].Selected;
            gvCoverPage06.Visible = CheckBoxList1.Items[2].Selected;
        }

        protected void ddlA19_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvCoverPage03.Columns[0].HeaderText = String.Format("Liquid Particle Count ({0})", ddlA19.SelectedItem.Text);
            gvCoverPage05.Columns[0].HeaderText = String.Format("Liquid Particle Count ({0})", ddlA19.SelectedItem.Text);
            gvCoverPage06.Columns[0].HeaderText = String.Format("Liquid Particle Count ({0})", ddlA19.SelectedItem.Text);

            gvCoverPage03.DataBind();
            gvCoverPage05.DataBind();
            gvCoverPage06.DataBind();



        }

        public String getDecimalFormat(int _digit)
        {
            StringBuilder result = new StringBuilder();

            result.Append("{0:0.");
            for (int i = 1; i <= _digit; i++)
            {
                result.Append("0");
            }
            result.Append("}");
            return result.ToString();
        }

    }

    public class LPC
    {
        public string type { get; set; }
        public string Run { get; set; }
        public int RunNumber { get; set; }
        public string Sample { get; set; }
        public string ChannelSize { get; set; }
        public string Value { get; set; }
    }



}