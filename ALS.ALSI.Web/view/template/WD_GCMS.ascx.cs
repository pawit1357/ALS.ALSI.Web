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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class WD_GCMS : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_GCMS));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public static string[] AnalytesName = {
            "D#Total C18-C40 Hydrocarbon",
            "D#Total C18-C40 Hydrocarbon and Surfactant",
            "-#- C18-C40 Hydrocarbon Peak",
            "-#- C18-C40 Hydrocarbon Hump only",
            "-#- Surfactant contained in Hydrocarbon Hump (e.g. DP154/ DP155)",
            "-#- Surfactant contained in Hydrocarbon Hump (e.g. Triton X-100)",
            "-#- Surfactant contained in Hydrocarbon Hump (e.g. PCM AL)",
            "-#Surfactant as DP154/155",
            "-#Surfactant as Triton X-100",
            "-#Surfactant as PCM AL",
            "I#Total Silanes",
            "H#Total Siloxane",
            "F#Total Hexadecyl Esters of Fatty Acids",
            "-#- Tetradecanoic acid, hexadecyl ester (C14)",
            "-#- Hexadecanoic acid, hexadecyl ester  (C16)",
            "-#- Octadecanoic acid, hexadecyl ester  (C18)",
            "Z#Phthalate",
            "-#- Diisononyl Phthalate & Isomers",
            "-#- Phthalate Hump (m/z 149, 293)",
            "E#Irgafos and Derivatives",
            "-#- Irgafos",
            "-#- Irgafos-oxidized",
            "G#Total Organic Compound"
            //"D#Phthalate",
            //"D#Diisononyl Phthalate & Isomer",
            //"D#Phthalate hump (m/z 149,293)"
        };

        public String Part
        {
            get { return (String)Session[GetType().Name + "Part"]; }
            set { Session[GetType().Name + "Part"] = value; }
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
        public List<tb_m_gcms_cas> tbCas
        {
            get { return (List<tb_m_gcms_cas>)Session[GetType().Name + "tbCas"]; }
            set { Session[GetType().Name + "tbCas"] = value; }
        }


        public List<template_wd_gcms_coverpage> coverpages
        {
            get { return (List<template_wd_gcms_coverpage>)Session[GetType().Name + "coverpages"]; }
            set { Session[GetType().Name + "coverpages"] = value; }
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
        List<String> errors = new List<string>();

        private void initialPage()
        {

            tb_m_detail_spec detailSpec = new tb_m_detail_spec();
            detailSpec.specification_id = this.jobSample.specification_id;
            detailSpec.template_id = this.jobSample.template_id;

            tb_m_component comp = new tb_m_component();
            comp.specification_id = this.jobSample.specification_id;
            comp.template_id = this.jobSample.template_id;

            this.coverpages = template_wd_gcms_coverpage.FindAllBySampleID(this.SampleID);
            this.CommandName = CommandNameEnum.Add;
            //this.allowCal = false;
            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = detailSpec.SelectAll();
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlComponent.Items.Clear();
            ddlComponent.DataSource = comp.SelectAll();
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            tb_unit unit = new tb_unit();
            ddlUnit.Items.Clear();
            ddlUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("GCMS")).ToList();
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            #region "CAS"
            this.tbCas = tb_m_gcms_cas.FindAllBySampleID(this.SampleID);
            if (this.tbCas != null && this.tbCas.Count > 0 && this.coverpages != null && this.coverpages.Count > 0)
            {
                foreach (tb_m_gcms_cas _cas in this.tbCas)
                {
                    _cas.RowState = CommandNameEnum.Edit;
                }


                gvResult.DataSource = this.tbCas.Where(x => x.row_type.Value != Convert.ToInt32(RowTypeEnum.MajorCompounds));
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
                gvCoverPages.Columns[5].Visible = false;

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
                            //gvCoverPages.Columns[5].Visible = false;
                            //gvResult.Columns[9].Visible = false;
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
                            //gvCoverPages.Columns[5].Visible = true;
                            //gvResult.Columns[9].Visible = true;
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
                            //gvCoverPages.Columns[5].Visible = false;
                            //gvResult.Columns[9].Visible = true;
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
                            //gvCoverPages.Columns[5].Visible = false;
                            //gvResult.Columns[9].Visible = false;
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
                            //gvCoverPages.Columns[5].Visible = false;
                            //gvResult.Columns[9].Visible = false;
                        }
                        break;
                }
                txtDateAnalyzed.Text = (this.jobSample.date_chemist_analyze != null) ? this.jobSample.date_chemist_analyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = userRole == RoleEnum.CHEMIST;
                #region "METHOD/PROCEDURE:"


                if (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                {

                    //#region ":: STAMP ANALYZED DATE ::"
                    //if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                    //{
                    //    if (this.jobSample.date_chemist_analyze == null)
                    //    {
                    //        this.jobSample.date_chemist_analyze = DateTime.Now;
                    //        this.jobSample.Update();
                    //    }
                    //}
                    //#endregion



                    txtProcedure.Enabled = true;
                    txtNumberOfPieces.Enabled = true;
                    txtExtractionMedium.Enabled = true;
                    txtExtractionVolumn.Enabled = true;

                    btnCoverPage.Visible = true;
                    btnDHS.Visible = true;

                    gvCoverPages.Columns[5].Visible = true;
                }
                else
                {
                    txtProcedure.Enabled = false;
                    txtNumberOfPieces.Enabled = false;
                    txtExtractionMedium.Enabled = false;
                    txtExtractionVolumn.Enabled = false;

                    btnCoverPage.Visible = false;
                    btnDHS.Visible = false;

                    gvCoverPages.Columns[5].Visible = false;
                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
                    {
                        btnCoverPage.Visible = true;
                        btnDHS.Visible = true;

                    }
                }
                #endregion
                #region "COVERPAGE"
                if (this.coverpages != null && this.coverpages.Count > 0)
                {


                    cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;
                    if (cbCheckBox.Checked)
                    {
                        lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "WD");
                    }
                    else
                    {
                        tb_m_detail_spec detailSpec1 = new tb_m_detail_spec().SelectByID(this.coverpages[0].detail_spec_id.Value);
                        if (detailSpec1 != null)
                        {
                            lbSpecDesc.Text = String.Format("The Specification is based on Western Digital's Doc {0} {1}", detailSpec1.B, detailSpec1.A);
                        }
                    }

                    //tb_m_detail_spec detailSpec1 = new tb_m_detail_spec().SelectByID(this.coverpages[0].detail_spec_id.Value);
                    //if (detailSpec1 != null)
                    //{
                    //    lbDescription.Text = String.Format("The Specification is based on Western Digital's Doc {0} for {1}", detailSpec1.B, detailSpec1.A);

                    //}
                    this.CommandName = CommandNameEnum.Edit;
                    txtProcedure.Text = this.coverpages[0].pm_procedure;
                    txtNumberOfPieces.Text = this.coverpages[0].pm_number_of_pieces;
                    txtExtractionMedium.Text = this.coverpages[0].pm_extraction_medium;
                    txtExtractionVolumn.Text = this.coverpages[0].pm_extraction_volumn;

                    hProcedureUnit.Value = this.coverpages[0].pm_unit;


                    //

                    #region "Unit"
                    if (CustomUtils.isNumber(hProcedureUnit.Value))
                    {
                        ddlUnit.SelectedValue = hProcedureUnit.Value;
                        tb_unit _unit = new tb_unit().SelectByID(int.Parse(hProcedureUnit.Value));
                        gvResult.Columns[7].HeaderText = String.Format("Amount ({0})", _unit.name);
                        gvCoverPages.Columns[2].HeaderText = String.Format("Specification Limits ({0})", _unit.name);
                        gvCoverPages.Columns[3].HeaderText = String.Format("Results ({0})", _unit.name);
                        gvMajorCompounds.Columns[2].HeaderText = String.Format("Result ({0})", _unit.name);
                    }
                    else
                    {
                        gvResult.Columns[7].HeaderText = String.Format("Amount ({0})", hProcedureUnit.Value);
                        gvCoverPages.Columns[2].HeaderText = String.Format("Specification Limits ({0})", hProcedureUnit.Value);
                        gvCoverPages.Columns[3].HeaderText = String.Format("Results ({0})", hProcedureUnit.Value);
                        gvMajorCompounds.Columns[2].HeaderText = String.Format("Result ({0})", hProcedureUnit.Value);
                    }

                    #endregion

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
            pLoadFile.Visible = false;
            //gvResult.Columns[2].Visible = false;

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

            //Boolean isValid = true;

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
                    if (this.coverpages != null && this.coverpages.Count > 0)
                    {
                        foreach (template_wd_gcms_coverpage _cov in this.coverpages)
                        {
                            _cov.detail_spec_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                            _cov.component_id = Convert.ToInt32(ddlComponent.SelectedValue);

                            _cov.pm_procedure = txtProcedure.Text;
                            _cov.pm_number_of_pieces = txtNumberOfPieces.Text;
                            _cov.pm_extraction_medium = txtExtractionMedium.Text;
                            _cov.pm_extraction_volumn = txtExtractionVolumn.Text;
                            _cov.pm_unit = hProcedureUnit.Value;
                            //_cov.test = Convert.ToInt32(ddlTest.SelectedValue);
                            _cov.RowState = this.CommandName;
                        }
                    }
                    MaintenanceBiz.ExecuteReturnDt(string.Format("delete from template_wd_gcms_coverpage where sample_id={0}", this.SampleID));

                    //template_wd_gcms_coverpage.DeleteBySampleID(this.SampleID);
                    template_wd_gcms_coverpage.InsertList(this.coverpages);
                    #endregion
                    this.jobSample.date_login_complete = DateTime.Now;
                    this.jobSample.date_chemist_analyze = DateTime.Now;
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    if (this.tbCas.Count > 0)
                    {
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                        this.jobSample.step3owner = userLogin.id;
                        //#region ":: STAMP COMPLETE DATE"
                        this.jobSample.date_chemist_analyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                        this.jobSample.date_chemist_complete = DateTime.Now;
                        this.jobSample.date_srchemist_analyze = DateTime.Now;
                        this.jobSample.path_word = String.Empty;
                        this.jobSample.path_pdf = String.Empty;
                        this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                        //#endregion
                        #region "CAS#"

                        tb_m_gcms_cas.DeleteBySampleID(this.SampleID);
                        tb_m_gcms_cas.InsertList(this.tbCas);
                        #endregion
                        #region "Cover Page#"
                        foreach (template_wd_gcms_coverpage _cov in this.coverpages)
                        {
                            _cov.pm_procedure = txtProcedure.Text;
                            _cov.pm_number_of_pieces = txtNumberOfPieces.Text;
                            _cov.pm_extraction_medium = txtExtractionMedium.Text;
                            _cov.pm_extraction_volumn = txtExtractionVolumn.Text;
                            _cov.pm_unit = hProcedureUnit.Value;
                        }
                        MaintenanceBiz.ExecuteReturnDt(string.Format("delete from template_wd_gcms_coverpage where sample_id={0}", this.SampleID));

                        //template_wd_gcms_coverpage.DeleteBySampleID(this.SampleID);
                        template_wd_gcms_coverpage.InsertList(this.coverpages);
                        //template_wd_gcms_coverpage.UpdateList(this.coverpages);
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
                    if (FileUpload1.HasFile)// && (Path.GetExtension(FileUpload1.FileName).Equals(".doc") || Path.GetExtension(FileUpload1.FileName).Equals(".docx")))
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
                        this.jobSample.date_admin_word_complete = DateTime.Now;
                        this.jobSample.date_labman_analyze = DateTime.Now;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
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
            string sheetName = string.Empty;

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

                        if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")))
                        {
                            using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                            {
                                HSSFWorkbook wb = new HSSFWorkbook(fs);

                                ISheet isheet = wb.GetSheet(ConfigurationManager.AppSettings["wd.gcs.workingsheet.hc"]);
                                #region "HC"
                                if (isheet == null)
                                {
                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["wd.gcs.workingsheet.hc"]));
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
                                            if (isheet.GetRow(j).GetCell(1) != null && !String.IsNullOrEmpty(tmp.pk))
                                            {
                                                if (isheet.GetRow(j).GetCell(1).CellType != CellType.Blank)
                                                {
                                                    if (CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1)).IndexOf("-") > 0)
                                                    {
                                                        tmp.rt = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1));
                                                    }
                                                    else {

                                                        tmp.rt = Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1))).ToString("N" + txtDecimal03.Text);
                                                    }
                                                }
                                            }
                                            tmp.library_id = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(2));
                                            tmp.classification = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(3));
                                            tmp.cas = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(4));
                                            tmp.qual = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(5));
                                            tmp.area = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                            if (isheet.GetRow(j).GetCell(7) != null)
                                            {
                                                if (isheet.GetRow(j).GetCell(7).CellType != CellType.Blank)
                                                {
                                                    if (tmp.classification.Equals("Total Organic Compound"))
                                                    {
                                                        tmp.amount = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(7))), Convert.ToInt32(txtDecimal02.Text)).ToString();

                                                    }
                                                    else
                                                    {
                                                        tmp.amount = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(7))), Convert.ToInt32(txtDecimal01.Text)).ToString();
                                                    }
                                                    //tmp.amount = Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(7))).ToString();
                                                }
                                            }
                                            tmp.row_type = (

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.classification) ||

                                                String.IsNullOrEmpty(tmp.pk) && !String.IsNullOrEmpty(tmp.library_id) && !String.IsNullOrEmpty(tmp.area)) ? Convert.ToInt32(RowTypeEnum.TotalRow) : Convert.ToInt32(RowTypeEnum.Normal);

                                            if (!String.IsNullOrEmpty(tmp.area))
                                            {
                                                _cas.Add(tmp);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                //Add major compond
                                int index = 1;
                                isheet = wb.GetSheet(ConfigurationManager.AppSettings["wd.gcs.workingsheet.majorcomp"]);
                                #region "Major Comp"
                                if (isheet == null)
                                {
                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["wd.gcs.workingsheet.majorcomp"]));
                                }
                                else
                                {
                                    sheetName = isheet.SheetName;

                                    for (int j = 15; j < isheet.LastRowNum; j++)
                                    {
                                        if (isheet.GetRow(j) != null && index <= 5)
                                        {
                                            tb_m_gcms_cas tmp = new tb_m_gcms_cas();
                                            tmp.ID = j;
                                            tmp.sample_id = this.SampleID;
                                            tmp.pk = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(0));
                                            //tmp.rt = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1));
                                            if (isheet.GetRow(j).GetCell(1) != null && !String.IsNullOrEmpty(tmp.pk))
                                            {
                                                if (isheet.GetRow(j).GetCell(1).CellType != CellType.Blank)
                                                {
                                                    tmp.rt = Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(1))).ToString("N" + txtDecimal03.Text);
                                                }
                                            }
                                            tmp.library_id = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(2));
                                            tmp.classification = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(3));
                                            tmp.cas = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(4));
                                            tmp.qual = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(5));
                                            tmp.area = CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(6));
                                            if (isheet.GetRow(j).GetCell(7) != null)
                                            {
                                                if (isheet.GetRow(j).GetCell(7).CellType != CellType.Blank)
                                                {
                                                    tmp.amount = Math.Round(Convert.ToDecimal(CustomUtils.GetCellValue(isheet.GetRow(j).GetCell(7))), Convert.ToInt32(txtDecimal01.Text)).ToString();
                                                }
                                            }

                                            tmp.row_type = Convert.ToInt32(RowTypeEnum.MajorCompounds);

                                            if (!String.IsNullOrEmpty(tmp.area))
                                            {
                                                _cas.Add(tmp);
                                                index++;
                                            }
                                        }


                                    }
                                }
                                #endregion
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));
                        }
                    }

                }
                catch (Exception )
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
                this.tbCas = _cas;
                gvResult.DataSource = this.tbCas;
                gvResult.DataBind();
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
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
                    pLoadFile.Visible = false;

                    //Generate cover-pagee
                    GenerrateCoverPage();
                    break;
                case "HC":
                    btnCoverPage.CssClass = "btn blue";
                    btnDHS.CssClass = "btn green";
                    pCoverpage.Visible = false;
                    pDSH.Visible = true;
                    pLoadFile.Visible = false;
                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                    {
                        pLoadFile.Visible = true;

                    }
                    break;
            }
        }

        //#endregion

        #region "COVERPAGES GRID."
        protected void gvCoverPages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label _lbAnalytes = (e.Row.FindControl("lbAnalytes") as Label);
                if (_lbAnalytes != null)
                {
                    if (_lbAnalytes.Text.StartsWith(Constants.CHAR_DASH))
                    {
                        _lbAnalytes.Attributes.CssStyle.Add("margin-left", "15px");
                    }
                }
            }
        }

        protected void gvCoverPage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_wd_gcms_coverpage gcms = this.coverpages.Find(x => x.ID == PKID);
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
                    gvCoverPages.DataSource = this.coverpages;
                    gvCoverPages.DataBind();
                }
            }
        }

        protected void gvCoverPage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvCoverPages.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCoverPages.DataKeys[e.Row.RowIndex].Values[1]);
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

        #endregion

        #region "DHS GRID."


        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int _id = Convert.ToInt32(gvResult.DataKeys[e.Row.RowIndex].Values[0].ToString());

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



        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_detail_spec detailSpec = new tb_m_detail_spec().SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (detailSpec != null)
            {
                lbSpecDesc.Text = String.Format("The Specification is based on Western Digital's Doc {0} {1}", detailSpec.B, detailSpec.A);
                int index = 1;
                this.coverpages = new List<template_wd_gcms_coverpage>();
                foreach (String _name in AnalytesName)
                {
                    template_wd_gcms_coverpage work = new template_wd_gcms_coverpage();
                    //work.ID = (this.CommandName == CommandNameEnum.Add) ? index : this.coverpages[index - 1].ID;
                    work.sample_id = this.SampleID;
                    work.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                    work.detail_spec_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                    work.A = (index == 1 || index == 2) ? "GCMS" : String.Empty;//Required Test
                    work.B = _name.Substring(2, _name.Length - 2);//Analytes
                    work.C = detailSpec.getValueByPrefix(_name[0].ToString());//Specification Limits 
                    if (work.C.Trim().Equals("0"))
                    {
                        work.C = "NA";
                    }
                    work.D = String.Empty;
                    work.E = String.Empty;//PASS / FAIL
                    //work.RowState = this.CommandName;
                    work.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                    this.coverpages.Add(work);
                    index++;
                }

                gvCoverPages.DataSource = this.coverpages;
                gvCoverPages.DataBind();
            }
        }

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_component component = new tb_m_component().SelectByID(Convert.ToInt32(ddlComponent.SelectedValue));
            if (component != null)
            {
                txtProcedure.Text = component.B;
                txtNumberOfPieces.Text = component.D;
                txtExtractionMedium.Text = component.E;
                txtExtractionVolumn.Text = component.F;

                //hProcedureUnit.Value = component.C;
                gvCoverPages.Columns[2].HeaderText = String.Format("Specification Limits ,({0})", component.C);
                gvCoverPages.Columns[3].HeaderText = String.Format("Results({0})", component.C);
                gvMajorCompounds.Columns[2].HeaderText = String.Format("Result({0})", component.C);
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

        #region "Custom method"


        private void GenerrateCoverPage()
        {
            //ReFresh Clas# value

            List<template_wd_gcms_coverpage> newCoverPage = new List<template_wd_gcms_coverpage>();
            foreach (template_wd_gcms_coverpage _cover in this.coverpages)
            {
                //String result = "Not Detected";

                if(_cover.ID== 71464)
                {
                    Console.WriteLine();
                }
                tb_m_gcms_cas tmp = this.tbCas.Find(x => _cover.B.Equals(x.classification) && x.row_type == Convert.ToInt32(RowTypeEnum.TotalRow));
                if (tmp != null)
                {

                    decimal val = Convert.ToDecimal(String.IsNullOrEmpty(tmp.amount) ? "0" : tmp.amount);
                    if (val > 0)
                    {
                        _cover.D = val.ToString();
                    }
                    else
                    {
                        _cover.D = "Not Detected";
                    }

                }
                else
                {
                    _cover.D = "Not Detected";
                }

                if (_cover.B.Equals("Phthalate"))
                {
                    _cover.C = _cover.C.Equals("0") ? "NA" : _cover.C;
                }




                newCoverPage.Add(_cover);
            }

            //Calculate Pass/Fail Result
            foreach (template_wd_gcms_coverpage _cover in newCoverPage)
            {
                string specificationLimits = _cover.C.Replace("<", "").Trim();

                if (!specificationLimits.StartsWith("-")  && !specificationLimits.Equals(""))
                {
                    if (!String.IsNullOrEmpty(_cover.D))
                    {
                        _cover.E = _cover.C.Equals("NA")? "NA": (_cover.D.Equals("Not Detected") ? "PASS" : (Convert.ToDecimal(_cover.D) < Convert.ToDecimal(specificationLimits) ? "PASS" : "FAIL"));
                    }
                }
                else
                {
                    _cover.E = String.Empty;
                }

                if (_cover.B.Equals("Phthalate"))
                {
                    _cover.E = _cover.C.Equals("0") ? "" : "";
                }
                _cover.C = CustomUtils.isNumber(_cover.C) ? "<" + _cover.C : _cover.C;

            }
            gvCoverPages.DataSource = newCoverPage;
            gvCoverPages.DataBind();

            List<tb_m_gcms_cas> majorCompounds = this.tbCas.FindAll(x => x.row_type == Convert.ToInt32(RowTypeEnum.MajorCompounds));

            gvMajorCompounds.DataSource = majorCompounds;
            gvMajorCompounds.DataBind();

        }


        protected void lbDownload_Click(object sender, EventArgs e)
        {

            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);
            ReportHeader reportHeader = ReportHeader.getReportHeder(this.jobSample);



            ReportParameterCollection reportParameters = new ReportParameterCollection();

            String _unit = String.Empty;
            if (CustomUtils.isNumber(hProcedureUnit.Value))
            {
                _unit = ddlUnit.SelectedItem.Text;
            }
            else
            {
                _unit = hProcedureUnit.Value;
            }
            reportParameters.Add(new ReportParameter("RemarkAmendRetest", reportHeader.remarkAmendRetest));
            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));
            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfTestComplete.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("rpt_unit", _unit));

            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", "GCMS - Hydrocarbon Residue"));
            reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
            reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.jobSample.singapore_ref_no) ? String.Empty : this.jobSample.singapore_ref_no)));
            reportParameters.Add(new ReportParameter("SupplementToReportNo", reportHeader.supplementToReportNo));


            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/gcms_wd.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);

            List<template_wd_gcms_coverpage> ds3 = this.coverpages.Where(x => x.row_type.Value == Convert.ToInt32(RowTypeEnum.Normal)).ToList();


            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            if (ds3.Count >0 && ds3.Count<=10)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds3.ToDataTable())); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", new DataTable())); // Add datasource here

            }

            if(ds3.Count>10)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds3.GetRange(0, 10).ToDataTable())); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ds3.GetRange(10, ds3.Count - 10).ToDataTable())); // Add datasource here

            }
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", this.tbCas.FindAll(x => x.row_type == Convert.ToInt32(RowTypeEnum.MajorCompounds)).ToDataTable())); // Add datasource here







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

        protected void lbDownloadPdf_Click(object sender, EventArgs e)
        {

            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);
            ReportHeader reportHeader = ReportHeader.getReportHeder(this.jobSample);



            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));
            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));

            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", "GCMS - Hydrocarbon Residue"));
            reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));



            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/gcms_wd_pdf.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);

            List<template_wd_gcms_coverpage> ds3 = this.coverpages.Where(x => x.row_type.Value == Convert.ToInt32(RowTypeEnum.Normal)).ToList();


            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            if (ds3.Count > 10)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds3.GetRange(0, 10).ToDataTable())); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", ds3.GetRange(10, ds3.Count - 10).ToDataTable())); // Add datasource here

            }
            else
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds3.ToDataTable())); // Add datasource here
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", new DataTable())); // Add datasource here
            }
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", this.tbCas.FindAll(x => x.row_type == Convert.ToInt32(RowTypeEnum.MajorCompounds)).ToDataTable())); // Add datasource here



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
            String html = "<html><header><style>body {max-width: 800px;margin:initial;font-family: \'Arial Unicode MS\';font-size: 10px;}table {border-collapse: collapse;}th {background: #666;color: #fff;border: 1px solid #999;padding: 0.5rem;text-align: center;}td { border: 1px solid #999;padding: 0.5rem;text-align: left;}h6 {font-weight:initial;}</style></header><body><form runat=\"server\" id=\"Form1\" method=\"POST\" >" + strHTMLContent + "</form></body></html>";


            HttpContext.Current.Response.Write(html);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
        }

        #endregion

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

        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "WD");
            }
            else
            {
                tb_m_detail_spec detailSpec1 = new tb_m_detail_spec().SelectByID(this.coverpages[0].detail_spec_id.Value);
                if (detailSpec1 != null)
                {
                    lbSpecDesc.Text = String.Format("The Specification is based on Western Digital's Doc {0} {1}", detailSpec1.B, detailSpec1.A);
                }
            }

        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            hProcedureUnit.Value = ddlUnit.SelectedValue;
            gvResult.Columns[7].HeaderText = String.Format("Amount ({0})", ddlUnit.SelectedItem.Text);
            gvCoverPages.Columns[2].HeaderText = String.Format("Specification Limits ({0})", ddlUnit.SelectedItem.Text);
            gvCoverPages.Columns[3].HeaderText = String.Format("Results ({0})", ddlUnit.SelectedItem.Text);
            gvMajorCompounds.Columns[2].HeaderText = String.Format("Result ({0})", ddlUnit.SelectedItem.Text);

            gvCoverPages.DataSource = this.coverpages;
            gvCoverPages.DataBind();

            gvResult.DataSource = this.tbCas.Where(x => x.row_type.Value != Convert.ToInt32(RowTypeEnum.MajorCompounds));
            gvResult.DataBind();

            ModolPopupExtender.Show();
        }

    }
}