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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WordToPDF;

namespace ALS.ALSI.Web.view.template
{
    public partial class Seagate_DHS_V2 : System.Web.UI.UserControl
    {

        //////private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Seagate_DHS));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }
        public List<tb_m_dhs_cas> tbCas
        {
            get { return (List<tb_m_dhs_cas>)Session[GetType().Name + "tbCas"]; }
            set { Session[GetType().Name + "tbCas"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }


        public List<template_seagate_dhs_coverpage> coverpages
        {
            get { return (List<template_seagate_dhs_coverpage>)Session[GetType().Name + "coverpages"]; }
            set { Session[GetType().Name + "coverpages"] = value; }
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
            this.CommandName = CommandNameEnum.Add;
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;
            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;

            this.coverpages = template_seagate_dhs_coverpage.FindAllBySampleID(this.SampleID);
            this.jobSample = new job_sample().SelectByID(this.SampleID);

            tb_m_component comp = new tb_m_component();
            comp.specification_id = this.jobSample.specification_id;
            comp.template_id = this.jobSample.template_id;

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt16(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF) + ""));


            ddlComponent.Items.Clear();
            ddlComponent.DataSource = comp.SelectAll();
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            #region "CAS"
            this.tbCas = tb_m_dhs_cas.FindAllBySampleID(this.SampleID);
            if (this.tbCas != null && this.tbCas.Count > 0 && this.coverpages != null && this.coverpages.Count > 0)
            {
                this.coverpages = this.coverpages.FindAll(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal));

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
                gvCoverPages.Columns[3].Visible = false;

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
                    txtSampleSize.Enabled = true;
                    txtSamplingTime.Enabled = true;
                    gvCoverPages.Columns[3].Visible = true;

                    btnCoverPage.Visible = true;
                    btnDHS.Visible = true;
                }
                else
                {
                    txtProcedureNo.Enabled = false;
                    txtSampleSize.Enabled = false;
                    txtSamplingTime.Enabled = false;
                    gvCoverPages.Columns[3].Visible = false;
                    btnCoverPage.Visible = false;
                    btnDHS.Visible = false;
                }
                #endregion
                #region "COVERPAGE"
                if (this.coverpages != null && this.coverpages.Count > 0)
                {

                    this.CommandName = CommandNameEnum.Edit;
                    //Result Description



                    tb_m_component component = new tb_m_component().SelectByID(this.coverpages[0].component_id.Value);
                    ddlComponent.SelectedValue = component.ID.ToString();

                    gvCoverPages.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", component.C);
                    gvCoverPages.Columns[2].HeaderText = String.Format("Results,({0})", component.C);
                    lbDocRev.Text = component.B;
                    lbDesc.Text = component.A;
                    //lbResultDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", component.B, component.A);


                    ddlComponent.SelectedValue = coverpages[0].component_id.ToString();

                    txtProcedureNo.Text = this.coverpages[0].procedureNo;
                    txtSamplingTime.Text = this.coverpages[0].SamplingTime;
                    txtSampleSize.Text = this.coverpages[0].sampleSize;
                    //this.SampleSize = String.IsNullOrEmpty(this.coverpages[0].sampleSize) ? 100 : Convert.ToInt32(this.coverpages[0].sampleSize);
                    //this.Unit = Convert.ToInt32(this.coverpages[0].unit);
                    //CalculateCas(true);
                    GenerrateCoverPage();
                }
                #endregion
            }

            #endregion




            //initial button.
            btnCoverPage.CssClass = "btn green";
            btnDHS.CssClass = "btn blue";
            pCoverpage.Visible = true;
            pDSH.Visible = false;
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
                    foreach (template_seagate_dhs_coverpage _val in this.coverpages)
                    {
                        _val.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        _val.procedureNo = txtProcedureNo.Text;
                        _val.sampleSize = txtSampleSize.Text;
                        _val.SamplingTime = txtSamplingTime.Text;
                    }
                    template_seagate_dhs_coverpage.DeleteBySampleID(this.SampleID);
                    template_seagate_dhs_coverpage.InsertList(this.coverpages);
                    //switch (this.CommandName)
                    //{
                    //    case CommandNameEnum.Add:
                    //        template_seagate_dhs_coverpage.InsertList(this.coverpages);
                    //        break;
                    //    case CommandNameEnum.Edit:
                    //        template_seagate_dhs_coverpage.UpdateList(this.coverpages);
                    //        break;
                    //}
                    #endregion
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    //CalculateCas(false);
                    if (this.tbCas.Count > 0)
                    {
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                        this.jobSample.step3owner = userLogin.id;
                        this.jobSample.date_chemist_complete = DateTime.Now;

                        #region "CAS#"
                        tb_m_dhs_cas.DeleteBySampleID(this.SampleID);
                        tb_m_dhs_cas.InsertList(this.tbCas);
                        #endregion
                        #region "Cover Page#"
                        foreach (template_seagate_dhs_coverpage _val in this.coverpages)
                        {
                            _val.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                            _val.procedureNo = txtProcedureNo.Text;
                            _val.sampleSize = txtSampleSize.Text;
                            _val.SamplingTime = txtSamplingTime.Text;
                            //_val.unit = this.Unit;
                        }
                        template_seagate_dhs_coverpage.UpdateList(this.coverpages);
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

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                        String source_file_url = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));


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

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                        String source_file_url = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload1.FileName));


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
            //removeSession();
            Response.Redirect(this.PreviousPath);
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            string sheetName =string.Empty;

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

                                ISheet isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.dhs.excel.sheetname.dhs"]);
                                if (isheet == null)
                                {
                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["seagate.dhs.excel.sheetname.dhs"]));
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
        protected void gvCoverPages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_dhs_coverpage gcms = this.coverpages.Find(x => x.ID == PKID);
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
        protected void gvCoverPages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvCoverPages.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCoverPages.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                Label _lbAnalytes = (e.Row.FindControl("lbAnalytes") as Label);
                //Literal _litResult = (Literal)e.Row.FindControl("litResult");

                if (_lbAnalytes != null)
                {
                    if (_lbAnalytes.Text.StartsWith(Constants.CHAR_DASH))
                    {
                        _lbAnalytes.Attributes.CssStyle.Add("margin-left", "15px");
                    }
                }

                //if (_litResult != null)
                //{
                //    _litResult.Text = String.IsNullOrEmpty(_litResult.Text) || _litResult.Text.Equals("Not Detected") ? "" : Convert.ToDecimal(_litResult.Text).ToString("N2");
                //}

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

                //Literal _litAmount = (Literal)e.Row.FindControl("litAmount");

                //if (_litAmount != null)
                //{
                //    _litAmount.Text = String.IsNullOrEmpty(_litAmount.Text) || _litAmount.Text.Equals("Not Detected") ? "" : Convert.ToDecimal(_litAmount.Text).ToString("N2");
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
                        case RowTypeEnum.TotalOutGas:
                        case RowTypeEnum.SampleSize:
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
            //ReFresh Clas# value
            if (this.tbCas != null && this.tbCas.Count > 0)
            {

                List<template_seagate_dhs_coverpage> newCoverPage = new List<template_seagate_dhs_coverpage>();
                foreach (template_seagate_dhs_coverpage _cover in this.coverpages)
                {
                    tb_m_dhs_cas tmp = this.tbCas.Find(x => _cover.name.Equals(x.classification) && x.row_type == Convert.ToInt32(RowTypeEnum.TotalRow));
                    if (tmp != null)
                    {
                        switch (tmp.amount)
                        {
                            case "Not Detected":
                                _cover.result = "Not Detected";
                                break;
                            default:
                                Double amt = Convert.ToDouble(tmp.amount);
                                //switch (_cover.name)
                                //{
                                //    case "Total of All Compounds":
                                //        _cover.result = Math.Round(amt).ToString();
                                //        break;
                                //    default:
                                _cover.result = amt.ToString();
                                //        break;
                                //}
                                break;
                        }
                    }
                    else
                    {
                        _cover.result = "Not Detected";
                    }

                    tmp = this.tbCas.Find(x => _cover.name.Equals(x.library_id) && x.row_type == Convert.ToInt32(RowTypeEnum.TotalOutGas));
                    if (tmp != null)
                    {
                        switch (tmp.amount)
                        {
                            case "Not Detected":
                                _cover.result = "Not Detected";
                                break;
                            default:
                                Double amt = Convert.ToDouble(tmp.amount);
                                _cover.result = amt.ToString();
                                break;
                        }
                    }

                    newCoverPage.Add(_cover);
                }

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

        protected void lbDownload_Click(object sender, EventArgs e)
        {


            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);
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
            reportParameters.Add(new ReportParameter("Test", "DHS"));
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
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/dhs_seagate_v2.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", this.coverpages.ToDataTable())); // Add datasource here




            string download = String.Empty;

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (!String.IsNullOrEmpty(this.jobSample.path_word))
                    {
                        Response.Redirect(String.Format("{0}{1}", ALS.ALSI.Biz.Constant.Configurations.HOST, this.jobSample.path_word));
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

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {

            tb_m_component component = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (component != null)
            {
                if (!String.IsNullOrEmpty(component.E))
                {
                    tb_m_detail_spec_ref detailSpecRef = new tb_m_detail_spec_ref();
                    detailSpecRef.spec_ref = Convert.ToInt32(component.E);
                    detailSpecRef.template_id = this.jobSample.template_id;


                    List<tb_m_detail_spec_ref> detailSpecRefs = (List<tb_m_detail_spec_ref>)detailSpecRef.SelectAll();

                    List<template_seagate_dhs_coverpage> newCoverPage = new List<template_seagate_dhs_coverpage>();
                    int index = 0;
                    foreach (tb_m_detail_spec_ref spec in detailSpecRefs)
                    {
                        template_seagate_dhs_coverpage work = new template_seagate_dhs_coverpage();
                        //work.ID = (this.CommandName == CommandNameEnum.Add) ? index : this.coverpages[index].ID;
                        work.sample_id = this.SampleID;
                        work.component_id = component.ID;
                        work.chemical_id = spec.B;
                        work.name = spec.C;
                        work.ng_part = spec.D;
                        work.result = String.Empty;
                        //work.RowState = this.CommandName;
                        work.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                        newCoverPage.Add(work);
                        index++;
                    }
                    this.coverpages = newCoverPage;
                    //Result Description

                    gvCoverPages.Columns[1].HeaderText = String.Format("Maximum Allowable Amount,({0})", component.C);
                    gvCoverPages.Columns[2].HeaderText = String.Format("Results,({0})", component.C);
                    lbDocRev.Text = component.B;
                    lbDesc.Text = component.A;
                    //lbResultDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} for {1}", component.B, component.A);
                    txtSamplingTime.Text = component.D;
                    txtProcedureNo.Text = component.F;
                    //this.Unit = CustomUtils.getUnitByName(component.C);
                    gvCoverPages.DataSource = this.coverpages;
                    gvCoverPages.DataBind();

                }
                else
                {
                    gvCoverPages.DataSource = new List<template_seagate_dhs_coverpage>();
                    gvCoverPages.DataBind();
                }

            }
        }

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

        protected void btnAdhocSave_Click(object sender, EventArgs e)
        {

        }

        protected void lbPKSoert_Click(object sender, EventArgs e)
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