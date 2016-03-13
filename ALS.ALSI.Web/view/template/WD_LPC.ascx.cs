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
using System.Data;
using ALS.ALSI.Biz.ReportObjects;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace ALS.ALSI.Web.view.template
{
    public partial class WD_LPC : System.Web.UI.UserControl
    {

        #region "Property"
        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }
        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }
        public List<template_wd_lpc_coverpage> Lpc
        {
            get { return (List<template_wd_lpc_coverpage>)Session[GetType().Name + "Lpc"]; }
            set { Session[GetType().Name + "Lpc"] = value; }
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

        public job_sample jobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
        }


        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "Lpc");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
            Session.Remove(GetType().Name + "B1");
            Session.Remove(GetType().Name + "B2");
            Session.Remove(GetType().Name + "B3");
            Session.Remove(GetType().Name + "B4");
            Session.Remove(GetType().Name + "B5");
            Session.Remove(GetType().Name + "S1");
            Session.Remove(GetType().Name + "S2");
            Session.Remove(GetType().Name + "S3");
            Session.Remove(GetType().Name + "S4");
            Session.Remove(GetType().Name + "S5");
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

            ddlComponent.Items.Clear();
            ddlComponent.DataSource = comp.SelectAll();
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = detailSpec.SelectAll();
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));



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
                        if (this.jobSample.date_analyzed_date == null)
                        {
                            this.jobSample.date_analyzed_date = DateTime.Now;
                            this.jobSample.Update();
                        }
                    }
                    #endregion

                    txtB21.Enabled = true;
                    txtC21.Enabled = true;
                    txtD21.Enabled = true;
                    txtE21.Enabled = true;
                    gvSpec.Columns[5].Visible = true;
                    btnCoverPage.Visible = true;
                    btnWorkSheet.Visible = true;
                }
                else
                {
                    txtB21.Enabled = false;
                    txtC21.Enabled = false;
                    txtD21.Enabled = false;
                    txtE21.Enabled = false;
                    gvSpec.Columns[5].Visible = false;
                    btnCoverPage.Visible = false;
                    btnWorkSheet.Visible = false;
                }
                #endregion
            }

            #endregion

            #region "WorkSheet"
            this.Lpc = template_wd_lpc_coverpage.FindAllBySampleID(this.SampleID);
            if (this.Lpc != null && this.Lpc.Count > 0)
            {
                template_wd_lpc_coverpage _lpc = this.Lpc[0];

                tb_m_detail_spec tem = new tb_m_detail_spec().SelectByID(Convert.ToInt32(_lpc.detail_spec_id));
                if (tem != null)
                {
                    lbSpecRev.Text = tem.B;
                    lbComponent.Text = tem.A;
                }
                //Method
                txtB21.Text = _lpc.ProcedureNo;
                txtC21.Text = _lpc.NumberOfPieces;
                txtD21.Text = _lpc.ExtractionMedium;
                txtE21.Text = _lpc.ExtractionVolume;

                #region "Test Method: 92-004230 Rev. AK"
                txtB48.Text = _lpc.ws_b15;//Surface Area, cm²
                txtB49.Text = _lpc.ExtractionVolume;
                txtB50.Text = _lpc.ws_b17;
                txtB51.Text = _lpc.NumberOfPieces;


                #endregion

                #region "Tank Conditions"
                txtB54.Text = _lpc.ws_b21;
                txtC54.Text = _lpc.ws_c21;
                txtD54.Text = _lpc.ws_d21;
                #endregion



                ddlSpecification.SelectedValue = _lpc.detail_spec_id.ToString();
                ddlComponent.SelectedValue = _lpc.component_id.ToString();

                this.CommandName = CommandNameEnum.Edit;

                gvSpec.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SPEC));
                gvSpec.DataBind();
                CalculateCas();
            }
            else
            {
                this.Lpc = new List<template_wd_lpc_coverpage>();
                this.CommandName = CommandNameEnum.Add;


                txtB54.Text = "20.2 L";
                txtC54.Text = "68 kHz";
                txtD54.Text = "4.8 W/L";
            }

            #endregion

            //initial component
            btnCoverPage.CssClass = "btn blue";
            btnWorkSheet.CssClass = "btn green";

            pCoverPage.Visible = true;
            pDSH.Visible = false;
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
            Boolean isValid = true;
            template_wd_lpc_coverpage objWork = new template_wd_lpc_coverpage();

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                    this.jobSample.step1owner = userLogin.id;

                    break;
                case StatusEnum.LOGIN_SELECT_SPEC:

                    if (Convert.ToInt32(ddlSpecification.SelectedValue) == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", String.Format("alert('{0}')", "ยังไม่ได้เลือก Specificaton"), true);
                        ddlSpecification.Focus();
                        isValid = false;
                    }
                    else if (Convert.ToInt32(ddlComponent.SelectedValue) == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", String.Format("alert('{0}')", "ยังไมได้เลือก Component"), true);
                        ddlComponent.Focus();
                        isValid = false;
                    }
                    else
                    {
                        this.jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                        this.jobSample.step2owner = userLogin.id;

                        //Add new
                        foreach (template_wd_lpc_coverpage cov in this.Lpc)
                        {
                            cov.sample_id = this.SampleID;
                            cov.detail_spec_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                            cov.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                            cov.ProcedureNo = txtB21.Text;
                            cov.NumberOfPieces = txtC21.Text;
                            cov.ExtractionMedium = txtD21.Text;
                            cov.ExtractionVolume = txtE21.Text;
                            #region "Tank Conditions"
                            cov.ws_b21 = txtB54.Text;
                            cov.ws_c21 = txtC54.Text;
                            cov.ws_d21 = txtD54.Text;
                            #endregion
                        }
                        objWork.DeleteBySampleID(this.SampleID);
                        objWork.InsertList(this.Lpc.ToList());
                    }
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    if (Convert.ToInt32(ddlSpecification.SelectedValue) == 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", String.Format("alert('{0}')", "ยังไม่ได้เลือก Specificaton"), true);
                        //ddlSpecification.Focus();
                        //isValid = false;
                        errors.Add("ยังไม่ได้เลือก Specificaton");
                    }
                    else if (Convert.ToInt32(ddlComponent.SelectedValue) == 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", String.Format("alert('{0}')", "ยังไมได้เลือก Component"), true);
                        //ddlComponent.Focus();
                        //isValid = false;
                        errors.Add("ยังไม่ได้เลือก Component");

                    }
                    else
                    {
                        if (this.Lpc.Count > 0)
                        {
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                            this.jobSample.step3owner = userLogin.id;
                            #region ":: STAMP COMPLETE DATE"
                            this.jobSample.date_test_completed = DateTime.Now;
                            #endregion
                            //Add new
                            foreach (template_wd_lpc_coverpage cov in this.Lpc)
                            {
                                cov.sample_id = this.SampleID;
                                cov.detail_spec_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                                cov.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                                cov.ProcedureNo = txtB21.Text;
                                cov.NumberOfPieces = txtC21.Text;
                                cov.ExtractionMedium = txtD21.Text;
                                cov.ExtractionVolume = txtE21.Text;
                                #region "Test Method: 92-004230 Rev. AK"
                                cov.ws_b15 = txtB48.Text;
                                cov.ExtractionVolume = txtB49.Text;
                                cov.ws_b17 = txtB50.Text;
                                cov.NumberOfPieces = txtB51.Text;
                                #endregion
                                #region "Tank Conditions"
                                cov.ws_b21 = txtB54.Text;
                                cov.ws_c21 = txtC54.Text;
                                cov.ws_d21 = txtD54.Text;
                                #endregion
                            }
                            objWork.DeleteBySampleID(this.SampleID);
                            objWork.InsertList(this.Lpc.ToList());
                        }
                        else
                        {
                            errors.Add("ไม่พบข้อมูล WorkSheet");
                        }
                    }
                    break;
                case StatusEnum.SR_CHEMIST_CHECKING:
                    StatusEnum srChemistApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (srChemistApproveStatus)
                    {
                        case StatusEnum.SR_CHEMIST_APPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                            #region ":: STAMP COMPLETE DATE"
                            this.jobSample.sr_approve_date = DateTime.Now;
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
            removeSession();
            Response.Redirect(this.PreviousPath);
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            string sheetName = string.Empty;

            #region "GET VALUE FROM XLS"
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            //Remove Old
            this.Lpc.RemoveAll(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE));
            this.Lpc.RemoveAll(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY));

            //
            for (int i = 0; i < btnUpload.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = btnUpload.PostedFiles[i];
                try
                {
                    if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")))
                    {
                        if (_postedFile.ContentLength > 0)
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
                                String fnName = Path.GetFileNameWithoutExtension(source_file);

                                HSSFWorkbook wd = new HSSFWorkbook(fs);
                                ISheet _sheet = wd.GetSheet(ConfigurationManager.AppSettings["wd.lpc.excel.sheetname.working1"]);

                                if (_sheet == null)
                                {
                                    errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["wd.lpc.excel.sheetname.working1"]));
                                }
                                else
                                {
                                    sheetName = _sheet.SheetName;
                                    txtB48.Text = CustomUtils.GetCellValue(_sheet.GetRow(15 - 1).GetCell(ExcelColumn.B));
                                    txtB49.Text = CustomUtils.GetCellValue(_sheet.GetRow(16 - 1).GetCell(ExcelColumn.B));
                                    txtB50.Text = CustomUtils.GetCellValue(_sheet.GetRow(17 - 1).GetCell(ExcelColumn.B));
                                    txtB51.Text = CustomUtils.GetCellValue(_sheet.GetRow(18 - 1).GetCell(ExcelColumn.B));

                                    String tmpA = String.Empty;
                                    String tmpGroup = String.Empty;
                                    String[] statisticsLabel = { "Average", "Standard Deviation", "%RSD Deviation" };
                                    int rowIndex = 1;
                                    int summaryIndex = 0;
                                    for (int row = 22; row <= _sheet.LastRowNum; row++)
                                    {
                                        String A = CustomUtils.GetCellValue(_sheet.GetRow(row).GetCell(ExcelColumn.A));
                                        String B = CustomUtils.GetCellValue(_sheet.GetRow(row).GetCell(ExcelColumn.B));
                                        String C = CustomUtils.GetCellValue(_sheet.GetRow(row).GetCell(ExcelColumn.C));
                                        String D = CustomUtils.GetCellValue(_sheet.GetRow(row).GetCell(ExcelColumn.D));
                                        String E = CustomUtils.GetCellValue(_sheet.GetRow(row).GetCell(ExcelColumn.E));
                                        String F = CustomUtils.GetCellValue(_sheet.GetRow(row).GetCell(ExcelColumn.F));

                                        if (A.Equals("Run") && B.StartsWith("Accumlative Size"))
                                        {
                                            tmpGroup = "Run";
                                            continue;
                                        }
                                        if (A.Equals("Statistics") && B.StartsWith("Accumlative Size"))
                                        {
                                            tmpGroup = "Statistics";
                                            continue;
                                        }
                                        if (String.IsNullOrEmpty(B))
                                        {
                                            continue;
                                        }
                                        if (!tmpA.Equals(A) && !String.IsNullOrEmpty(A))
                                        {
                                            tmpA = A;
                                        }

                                        template_wd_lpc_coverpage tmp = new template_wd_lpc_coverpage();
                                        tmp.ID = 100 + rowIndex;
                                        tmp.A = tmpA;
                                        tmp.B = B;
                                        tmp.C = String.IsNullOrEmpty(C) ? "" : Math.Round(Convert.ToDecimal(C), Convert.ToInt16(txtDecimal01.Text)).ToString();
                                        tmp.D = String.IsNullOrEmpty(D) ? "" : Math.Round(Convert.ToDecimal(D), Convert.ToInt16(txtDecimal02.Text)).ToString();
                                        tmp.E = String.IsNullOrEmpty(E) ? "" : Math.Round(Convert.ToDecimal(E), Convert.ToInt16(txtDecimal03.Text)).ToString();
                                        tmp.F = String.IsNullOrEmpty(F) ? "" : Math.Round(Convert.ToDecimal(F), Convert.ToInt16(txtDecimal04.Text)).ToString();
                                        tmp.data_type = (tmpGroup.Equals("Run")) ? Convert.ToInt32(WDLpcDataType.DATA_VALUE) : Convert.ToInt32(WDLpcDataType.SUMMARY);
                                        if (tmp.data_type.Value == Convert.ToInt32(WDLpcDataType.SUMMARY))
                                        {
                                            int indexValue = summaryIndex / 5;
                                            tmp.A = statisticsLabel[indexValue];
                                            if (tmp.A.Equals("%RSD Deviation"))
                                            {
                                                tmp.E = String.IsNullOrEmpty(E) ? "" : Math.Round(Convert.ToDecimal(E) * 100, 0).ToString();
                                                tmp.F = String.IsNullOrEmpty(F) ? "" : Math.Round(Convert.ToDecimal(F) * 100, 0).ToString();
                                            }
                                            summaryIndex++;
                                        }
                                        tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                                        this.Lpc.Add(tmp);
                                        rowIndex++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.xls"));

                    }
                }
                catch (Exception Ex)
                {
                    //logger.Error(Ex.Message);
                    errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", sheetName, CustomUtils.ErrorIndex));
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
                CalculateCas();

            }

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
                    pCoverPage.Visible = true;
                    pDSH.Visible = false;


                    break;
                case "WorkSheet":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkSheet.CssClass = "btn green";
                    pCoverPage.Visible = false;
                    pDSH.Visible = true;

                    break;
            }
            CalculateCas();
        }

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_detail_spec tem = new tb_m_detail_spec().SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (tem != null)
            {
                lbSpecRev.Text = tem.C;
                lbComponent.Text = tem.B;

                List<template_wd_lpc_coverpage> _Lpc = new List<template_wd_lpc_coverpage>();
                template_wd_lpc_coverpage spec = new template_wd_lpc_coverpage();
                spec.data_type = Convert.ToInt32(WDLpcDataType.SPEC);
                spec.ID = 1;
                spec.B = "0.2";
                spec.C = tem.D;
                spec.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                _Lpc.Add(spec);
                spec = new template_wd_lpc_coverpage();
                spec.ID = 2;
                spec.data_type = Convert.ToInt32(WDLpcDataType.SPEC);
                spec.B = "0.3";
                spec.C = tem.E;
                spec.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                _Lpc.Add(spec);
                spec = new template_wd_lpc_coverpage();
                spec.ID = 3;
                spec.data_type = Convert.ToInt32(WDLpcDataType.SPEC);
                spec.B = "0.5";
                spec.C = tem.F;
                spec.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                _Lpc.Add(spec);
                spec = new template_wd_lpc_coverpage();
                spec.ID = 4;
                spec.data_type = Convert.ToInt32(WDLpcDataType.SPEC);
                spec.B = "1.0";
                spec.C = tem.G;
                spec.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                _Lpc.Add(spec);
                this.Lpc = _Lpc;
                //add data to grid
                gvSpec.DataSource = _Lpc.Where(x => x.row_type == Convert.ToInt32(WDLpcDataType.SPEC));
                gvSpec.DataBind();

            }
        }

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_component tem = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (tem != null)
            {
                txtB21.Text = tem.B;
                txtC21.Text = tem.D;
                txtD21.Text = tem.E;
                txtE21.Text = tem.F;

                txtB49.Text = txtE21.Text;
                txtB51.Text = txtC21.Text;

                lbSpecRev.Text = tem.C;
                lbComponent.Text = tem.B;
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

            DataTable dt = Extenders.ObjectToDataTable(this.Lpc[0]);
            List<template_wd_lpc_coverpage> specs = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SPEC) && x.row_type.Value == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            List<template_wd_lpc_coverpage> values = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE)).ToList();
            List<template_wd_lpc_coverpage> sumarys = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY)).ToList();
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
            reportParameters.Add(new ReportParameter("Test", dt.Rows[0]["ws_c21"].ToString()));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The specification is based on Western Digital's document no. {0} for {1}", lbSpecRev.Text, lbComponent.Text)));
            reportParameters.Add(new ReportParameter("txtB48", txtB48.Text));
            reportParameters.Add(new ReportParameter("txtB49", txtB49.Text));
            reportParameters.Add(new ReportParameter("txtB50", txtB50.Text));
            reportParameters.Add(new ReportParameter("txtB51", txtB51.Text));
            reportParameters.Add(new ReportParameter("txtB54", txtB54.Text));
            reportParameters.Add(new ReportParameter("txtC54", txtC54.Text));
            reportParameters.Add(new ReportParameter("txtD54", txtD54.Text));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/lpc_wd.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", specs.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", values.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", sumarys.ToDataTable())); // Add datasource here




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

                    break;
            }


        }

        #region "Custom method"

        private String validateDSHFile(IList<HttpPostedFile> _files)
        {
            Boolean isFound_b1 = false;
            Boolean isFound_b2 = false;
            Boolean isFound_b3 = false;
            Boolean isFound_b4 = false;
            Boolean isFound_b5 = false;

            Boolean isFound_s1 = false;
            Boolean isFound_s2 = false;
            Boolean isFound_s3 = false;
            Boolean isFound_s4 = false;
            Boolean isFound_s5 = false;
            Boolean isFoundWrongExtension = false;

            String result = String.Empty;

            String[] files = new String[_files.Count];
            if (files.Length == 10)
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    files[i] = _files[i].FileName;
                    if (!Path.GetExtension(_files[i].FileName).Trim().Equals(".xls"))
                    {
                        isFoundWrongExtension = true;
                        break;
                    }
                }
                if (!isFoundWrongExtension)
                {

                    //Find B1
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("B1"))
                        {
                            isFound_b1 = true;
                            break;
                        }
                    }

                    //Find B2
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("B2"))
                        {
                            isFound_b2 = true;
                            break;
                        }
                    }
                    //Find B3
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("B3"))
                        {
                            isFound_b3 = true;
                            break;
                        }
                    }
                    //Find B4
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("B4"))
                        {
                            isFound_b4 = true;
                            break;
                        }
                    }
                    //Find B5
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("B5"))
                        {
                            isFound_b5 = true;
                            break;
                        }
                    }
                    //Find S1
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("S1"))
                        {
                            isFound_s1 = true;
                            break;
                        }
                    }

                    //Find S2
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("S2"))
                        {
                            isFound_s2 = true;
                            break;
                        }
                    }
                    //Find S3
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("S3"))
                        {
                            isFound_s3 = true;
                            break;
                        }
                    }
                    //Find S4
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("S4"))
                        {
                            isFound_s4 = true;
                            break;
                        }
                    }
                    //Find S5
                    foreach (String file in files)
                    {
                        if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("S5"))
                        {
                            isFound_s5 = true;
                            break;
                        }
                    }
                    result = (!isFound_b1) ? result += "File not found B1.xls" :
                                (!isFound_b2) ? result += "File not found B2.xls" :
                                (!isFound_b3) ? result += "File not found B3.xls" :
                                 (!isFound_b4) ? result += "File not found B3.xls" :
                                  (!isFound_b5) ? result += "File not found B3.xls" :
                                (!isFound_s1) ? result += "File not found S1.xls" :
                                (!isFound_s2) ? result += "File not found S2.xls" :
                                (!isFound_s3) ? result += "File not found S3.xls" :
                                 (!isFound_s4) ? result += "File not found S3.xls" :
                                  (!isFound_s5) ? result += "File not found S3.xls" : String.Empty;
                }
                else
                {
                    result = "File extension must be *.txt";
                }
            }
            else
            {
                result = "You must to select 6 files for upload.";
            }
            return result;
        }

        private void CalculateCas()
        {
            foreach (template_wd_lpc_coverpage val in this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SPEC)))
            {
                template_wd_lpc_coverpage tmp = Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.A.Equals("Average") && x.B.Equals(val.B)).FirstOrDefault();
                if (tmp != null)
                {
                    val.D = tmp.F;
                    //=IF(C29="NA","NA",IF(D29<INDEX('Detail Spec'!$A$3:$G$284,$F$3,5),"PASS","FAIL"))
                    if (!val.C.Equals("-"))
                    {
                        val.E = val.C.Equals("NA") ? "NA" : (Convert.ToDouble(val.D) < Convert.ToDouble(val.C)) ? "PASS" : "FAIL";
                    }
                    else
                    {
                        val.E = String.Empty;
                    }
                }
            }

            gvResult.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE));
            gvResult.DataBind();

            gvStatic.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY));
            gvStatic.DataBind();

            gvSpec.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SPEC));
            gvSpec.DataBind();
            btnSubmit.Enabled = true;

        }


        #endregion

        protected void gvSpec_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_wd_lpc_coverpage gcms = this.Lpc.Find(x => x.ID == PKID);
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
                    gvSpec.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SPEC));
                    gvSpec.DataBind();
                }
            }
        }

        protected void gvSpec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvSpec.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvSpec.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                Literal _litAverage = (Literal)e.Row.FindControl("litAverage");
                Literal _litSpecLimit = (Literal)e.Row.FindControl("litSpecLimit");

                if (_litAverage != null && _litSpecLimit != null)
                {
                    if (CustomUtils.isNumber(_litAverage.Text))
                    {
                        _litAverage.Text = Math.Round(Convert.ToDouble(_litAverage.Text)) + "";
                    }
                    if (!_litSpecLimit.Text.Equals("NA")) { 
                    _litSpecLimit.Text = String.Format("<{0}", _litSpecLimit.Text);
                    }
                }

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

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Literal _litAccumulativeSize = (Literal)e.Row.FindControl("litAccumulativeSize");
                Literal _litBlank = (Literal)e.Row.FindControl("litBlank");
                Literal _litSample = (Literal)e.Row.FindControl("litSample");
                Literal _litBlankCorredted = (Literal)e.Row.FindControl("litBlankCorredted");
                Literal _litBlankCorredtedCM2 = (Literal)e.Row.FindControl("litBlankCorredtedCM2");

                if (_litBlank != null && _litSample != null && _litBlankCorredted != null && _litBlankCorredtedCM2 != null)
                {
                    //_litBlank.Text = Convert.ToDouble(_litBlank.Text).ToString("N2");
                    //_litSample.Text = Convert.ToDouble(_litSample.Text).ToString("N2");
                    _litBlankCorredted.Text = Math.Round(Convert.ToDouble(_litBlankCorredted.Text)) + "";
                    _litBlankCorredtedCM2.Text = Math.Round(Convert.ToDouble(_litBlankCorredtedCM2.Text)) + "";
                }
            }
        }

        protected void gvStatic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal _lbStatistics = (Literal)e.Row.FindControl("lbStatistics");
                Literal _litBlankCorredted = (Literal)e.Row.FindControl("litBlankCorredted");
                Literal _litBlankCorredtedCM2 = (Literal)e.Row.FindControl("litBlankCorredtedCM2");
                if (_lbStatistics != null && _litBlankCorredted != null && _litBlankCorredtedCM2 != null)
                {

                    if (_lbStatistics.Text.Equals("%RSD Deviation"))
                    {
                        _litBlankCorredted.Text = String.Format("{0}%", Math.Round(Convert.ToDouble(_litBlankCorredted.Text) ));
                        _litBlankCorredtedCM2.Text = String.Format("{0}%", Math.Round(Convert.ToDouble(_litBlankCorredtedCM2.Text)));
                    }
                    else
                    {
                        _litBlankCorredtedCM2.Text = Math.Round(Convert.ToDouble(_litBlankCorredtedCM2.Text)) + "";
                        _litBlankCorredted.Text = Math.Round(Convert.ToDouble(_litBlankCorredted.Text)) + "";

                    }

                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

    }
}