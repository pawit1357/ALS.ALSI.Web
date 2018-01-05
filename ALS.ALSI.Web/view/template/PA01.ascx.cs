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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class PA01 : System.Web.UI.UserControl
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


        public template_pa pa
        {
            get { return (template_pa)Session[GetType().Name + "pa"]; }
            set { Session[GetType().Name + "pa"] = value; }
        }

        public List<template_pa_detail> paDetail
        {
            get { return (List<template_pa_detail>)Session[GetType().Name + "paDetail"]; }
            set { Session[GetType().Name + "paDetail"] = value; }
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
                //pCoverPage.Visible = true;
                //pSpecification.Visible = (status == StatusEnum.LOGIN_SELECT_SPEC);
                pStatus.Visible = (status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.LABMANAGER_CHECKING);
                pUploadfile.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD);
                pDownload.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);
                btnSubmit.Visible = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);
                btnCoverPage.Visible = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));

                if (status == StatusEnum.LABMANAGER_CHECKING)
                {
                    ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_APPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_APPROVE) + ""));
                    ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_DISAPPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_DISAPPROVE) + ""));

                }
                else if (status == StatusEnum.SR_CHEMIST_CHECKING)
                {
                    ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_APPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_APPROVE) + ""));
                    ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_DISAPPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_DISAPPROVE) + ""));
                    pRemark.Visible = false;
                }

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
            }

            #endregion


            //initial button.
            btnCoverPage.CssClass = "btn blue";
            btnDHS.CssClass = "btn green";

            m_evaluation_of_particles eop = new m_evaluation_of_particles();
            m_microscopic_analysis ma = new m_microscopic_analysis();


            paDetail = new List<template_pa_detail>();
            pa = new template_pa();

            #region "Evaluation of Particles"
            List<m_evaluation_of_particles> eops = eop.SelectAll().Where(x => x.template_id == this.jobSample.template_id).ToList();
            template_pa_detail tmp = new template_pa_detail();
            foreach (var item in eops)
            {
                tmp = new template_pa_detail();
                tmp.col_a = item.A;
                tmp.col_b = item.B;
                tmp.col_c = item.C;
                tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
                tmp.row_type = Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES);
                paDetail.Add(tmp);
            }
            #endregion
            #region "Gravimetry"

            tmp = new template_pa_detail();
            tmp.col_a = "Before filtration(mg):";
            tmp.col_b = String.Empty;
            tmp.col_c = String.Empty;
            tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
            tmp.row_type = Convert.ToInt16(PAEnum.GRAVIMETRY);
            paDetail.Add(tmp);
            tmp = new template_pa_detail();
            tmp.col_a = "After filtration (mg):";
            tmp.col_b = String.Empty;
            tmp.col_c = String.Empty;
            tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
            tmp.row_type = Convert.ToInt16(PAEnum.GRAVIMETRY);
            paDetail.Add(tmp);
            tmp = new template_pa_detail();
            tmp.col_a = "Residue weight(mg):";
            tmp.col_b = String.Empty;
            tmp.col_c = String.Empty;
            tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
            tmp.row_type = Convert.ToInt16(PAEnum.GRAVIMETRY);
            paDetail.Add(tmp);

            #endregion
            #region "Microscopic Analysis"
            #endregion


            gvEop.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.EVALUATION_OF_PARTICLES)).ToList();
            gvEop.DataBind();

            gvGravimetry.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.GRAVIMETRY)).ToList();
            gvGravimetry.DataBind();

            //gvMicroscopicAnalysis.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
            //gvMicroscopicAnalysis.DataBind();



            //pCoverpage.Visible = true;
            //pDSH.Visible = false;

            //switch (lbJobStatus.Text)
            //{
            //    case "CONVERT_PDF":
            //        litDownloadIcon.Text = "<i class=\"fa fa-file-pdf-o\"></i>";
            //        break;
            //    default:
            //        litDownloadIcon.Text = "<i class=\"fa fa-file-word-o\"></i>";
            //        break;
            //}
        }

        private void calculate()
        {
            //            -----------------
            //lbLmp
            //lbLnmp
            //lbLf
            //-----------------
            //txtAlsReferenceNo
            //txtPartDescription
            //txtLotNo
            //txtDateAnalyzed
            //txtDateTestComplete
            //-----------------
            //txtExtractionProcedure
            //---------------- -
            //txtExtractionMedium
            //txtShkingRewashQty
            //txtWettedSurfacePerComponent
            //txtTotalTestedSize
            //------
            //cbTypeOfMethod
            //cbFiltrationMethod
            //lbAnalysisMembraneUsed
            //cbTypeOfDrying
            //cbParticleSizingCoungtingDetermination
            //txtPixelScaling
            //txtCameraResolution
            //cbParticleSizingCoungtingDetermination2
            //----------
            //gvGravimetry
            //lbPermembrane

            gvMicroscopicAnalysis.DataSource = paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList();
            gvMicroscopicAnalysis.DataBind();

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
                case StatusEnum.CHEMIST_TESTING:

                    if (FileUpload2.HasFile)// && (Path.GetExtension(FileUpload2.FileName).Equals(".doc") || Path.GetExtension(FileUpload2.FileName).Equals(".docx")))
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload2.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(FileUpload2.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        FileUpload2.SaveAs(source_file);
                        this.jobSample.ad_hoc_tempalte_path = source_file_url;
                    }

                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step2owner = userLogin.id;
                    this.jobSample.date_chemist_complete = DateTime.Now;

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
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .doc|.docx");
                    }
                    this.jobSample.step6owner = userLogin.id;
                    break;
                case StatusEnum.ADMIN_CONVERT_PDF:
                    if (FileUpload1.HasFile) // && (Path.GetExtension(FileUpload1.FileName).Equals(".pdf")))
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
                        this.jobSample.step7owner = userLogin.id;

                        //lbMessage.Text = string.Empty;
                    }
                    else
                    {
                        errors.Add("Invalid File. Please upload a File with extension .pdf");
                        //lbMessage.Attributes["class"] = "alert alert-error";
                        //isValid = false;
                    }
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

        protected void btnCoverPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnCoverPage.CssClass = "btn green";
                    btnDHS.CssClass = "btn blue";
                    pCoverpage.Visible = true;
                    //pDSH.Visible = false;
                    break;
                case "WorkSheet":
                    btnCoverPage.CssClass = "btn blue";
                    btnDHS.CssClass = "btn green";
                    pCoverpage.Visible = false;
                    //pDSH.Visible = true;
                    break;
            }
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            string sheetName = string.Empty;

            List<tb_m_dhs_cas> _cas = new List<tb_m_dhs_cas>();
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            for (int i = 0; i < FileUpload2.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = FileUpload2.PostedFiles[i];
                try
                {
                    if (_postedFile.ContentLength > 0)
                    {
                        string yyyy = DateTime.Now.ToString("yyyy");
                        string MM = DateTime.Now.ToString("MM");
                        string dd = DateTime.Now.ToString("dd");

                        String source_file = String.Format(ALS.ALSI.Biz.Constant.Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));

                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        _postedFile.SaveAs(source_file);

                        #region "Microscopic Analysis"
                        if ((Path.GetExtension(_postedFile.FileName).Equals(".csv")))
                        {
                            if (Path.GetFileNameWithoutExtension(_postedFile.FileName).StartsWith("ClassTable_FromNumber_FeretMaximum"))
                            {
                                lbPermembrane.Text = String.Empty;
                                foreach (template_pa_detail pd in paDetail.Where(x => x.row_type == Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS)).ToList())
                                {
                                    paDetail.Remove(pd);
                                }

                                List<char> cols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
                                DataTable table = new DataTable();
                                foreach (char c in cols)
                                {
                                    table.Columns.Add(c.ToString(), typeof(string));
                                }

                                using (var reader = new StreamReader(source_file))
                                {
                                    int row = 0;
                                    while (!reader.EndOfStream)
                                    {
                                        var line = reader.ReadLine();
                                        var values = line.Split(',');
                                        if (!String.IsNullOrEmpty(values[0]))
                                        {
                                            table.Rows.Add(
                                             (row == 0) ? values[0].Split(':')[0] : values[0],
                                             (row == 0) ? values[1].Split(':')[0] : values[1],
                                             (row == 0) ? values[2].Split(':')[0] : values[2],
                                             (row == 0) ? values[3].Split(':')[0] : values[3],
                                             (row == 0) ? values[4].Split(':')[0] : values[4],
                                             (row == 0) ? values[5].Split(':')[0] : values[5],
                                             (row == 0) ? values[6].Split(':')[0] : values[6],
                                             (row == 0) ? values[7].Split(':')[0] : values[7],
                                             (row == 0) ? values[8].Split(':')[0] : values[8],
                                             (row == 0) ? values[9].Split(':')[0] : values[9],
                                             (row == 0) ? values[10].Split(':')[0] : values[10]
                                             );
                                        }
                                        row++;
                                    }
                                }
                                for (int r = 1; r < table.Columns.Count; r++)
                                {

                                    template_pa_detail tmp = new template_pa_detail();
                                    tmp.col_a = table.Rows[0][r].ToString().Replace("\"", "");
                                    tmp.col_b = table.Rows[1][r].ToString().Replace("\"", "");
                                    tmp.col_c = table.Rows[2][r].ToString().Replace("\"", "");
                                    tmp.col_d = table.Rows[3][r].ToString().Replace("\"", "");
                                    tmp.col_e = String.Empty;
                                    tmp.col_f = String.Empty;
                                    tmp.col_g = String.Empty;
                                    tmp.col_h = String.Empty;
                                    tmp.row_status = Convert.ToInt16(RowTypeEnum.Normal);
                                    tmp.row_type = Convert.ToInt16(PAEnum.MICROSCOPIC_ANALLYSIS);
                                    if (!tmp.col_a.Equals(""))
                                    {
                                        lbPermembrane.Text += string.Format("{0}{1}/", tmp.col_a, tmp.col_c);
                                        paDetail.Add(tmp);
                                    }
                                }
                                lbPermembrane.Text = lbPermembrane.Text.Substring(0, lbPermembrane.Text.Length - 1);
                            }
                        }
                        else
                        {
                            //errors.Add(String.Format("นามสกุลไฟล์จะต้องเป็น *.csv"));
                        }
                        #endregion
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
                //this.tbCas = _cas;
                //gvResult.DataSource = this.tbCas;
                //gvResult.DataBind();
                calculate();
            }
        }

        protected void btnLoadImg1_Click(object sender, EventArgs e)
        {
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String randNumber = String.Empty;
            String source_file = String.Empty;
            String source_file_url = String.Empty;

            if ((Path.GetExtension(fileUploadImg01.FileName).ToUpper().Equals(".JPG")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg01.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg01.SaveAs(source_file);
                this.pa.img01 = source_file_url;
                img1.ImageUrl = source_file_url;
            }
            
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();

            }
            else
            {
                litErrorMessage.Text = String.Empty;
                //this.tbCas = _cas;
                //gvResult.DataSource = this.tbCas;
                //gvResult.DataBind();
                calculate();
            }
        }

        protected void btnLoadImg_Click(object sender, EventArgs e)
        {
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");

            String randNumber = String.Empty;
            String source_file = String.Empty;
            String source_file_url = String.Empty;

            //if ((Path.GetExtension(fileUploadImg01.FileName).Equals(".tif")))
            //{
            //    randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg01.FileName));
            //    source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
            //    source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

            //    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
            //    {
            //        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
            //    }
            //    fileUploadImg01.SaveAs(source_file);
            //    this.pa.img01 = source_file_url;
            //    img1.ImageUrl = source_file_url;
            //}
            if ((Path.GetExtension(fileUploadImg02.FileName).Equals(".tif")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg02.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg02.SaveAs(source_file);
                this.pa.img02 = source_file_url;
                img2.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg03.FileName).Equals(".tif")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg03.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg03.SaveAs(source_file);
                this.pa.img03 = source_file_url;
                img3.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg04.FileName).Equals(".tif")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg04.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg04.SaveAs(source_file);
                this.pa.img04 = source_file_url;
                img4.ImageUrl = source_file_url;
            }
            if ((Path.GetExtension(fileUploadImg05.FileName).Equals(".tif")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg05.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg05.SaveAs(source_file);
                this.pa.img05 = source_file_url;
                img5.ImageUrl = source_file_url;
            }
            //if ((Path.GetExtension(fileUploadImg06.FileName).Equals(".tif")))
            //{
            //    randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg06.FileName));
            //    source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
            //    source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

            //    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
            //    {
            //        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
            //    }
            //    fileUploadImg06.SaveAs(source_file);
            //    this.pa.img06 = source_file_url;
            //    img6.ImageUrl = source_file_url;
            //}
            if ((Path.GetExtension(fileUploadImg07.FileName).Equals(".tif")))
            {
                randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg07.FileName));
                source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                }
                fileUploadImg07.SaveAs(source_file);
                this.pa.img07 = source_file_url;
                img7.ImageUrl = source_file_url;
            }
            //if ((Path.GetExtension(fileUploadImg08.FileName).Equals(".tif")))
            //{
            //    randNumber = String.Format("{0}_{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(fileUploadImg08.FileName));
            //    source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
            //    source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

            //    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
            //    {
            //        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
            //    }
            //    fileUploadImg08.SaveAs(source_file);
            //    this.pa.img08 = source_file_url;
            //    img8.ImageUrl = source_file_url;
            //}
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();

            }
            else
            {
                litErrorMessage.Text = String.Empty;
                //this.tbCas = _cas;
                //gvResult.DataSource = this.tbCas;
                //gvResult.DataBind();
                calculate();
            }
        }
        #endregion

        #region "Event"

        protected void gvEop_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                //template_seagate_lpc_coverpage gcms = this.Lpcs.Where(x => x.channel_size.Equals("0.500") && x.id == PKID).FirstOrDefault();
                //if (gcms != null)
                //{
                //    switch (cmd)
                //    {
                //        case RowTypeEnum.Hide:
                //            gcms.row_state = Convert.ToInt32(RowTypeEnum.Hide);
                //            break;
                //        case RowTypeEnum.Normal:
                //            gcms.row_state = Convert.ToInt32(RowTypeEnum.Normal);
                //            break;
                //    }

                //    gvEop.DataSource = this.Lpcs.Where(x => x.row_type == 1 && x.channel_size.Equals("0.500")).ToList();
                //    gvEop.DataBind();
                //}
            }
        }

        protected void gvEop_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvEop.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvGravimetry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                //template_seagate_lpc_coverpage gcms = this.Lpcs.Where(x => x.channel_size.Equals("0.500") && x.id == PKID).FirstOrDefault();
                //if (gcms != null)
                //{
                //    switch (cmd)
                //    {
                //        case RowTypeEnum.Hide:
                //            gcms.row_state = Convert.ToInt32(RowTypeEnum.Hide);
                //            break;
                //        case RowTypeEnum.Normal:
                //            gcms.row_state = Convert.ToInt32(RowTypeEnum.Normal);
                //            break;
                //    }

                //    gvEop.DataSource = this.Lpcs.Where(x => x.row_type == 1 && x.channel_size.Equals("0.500")).ToList();
                //    gvEop.DataBind();
                //}
            }
        }

        protected void gvGravimetry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvGravimetry.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvMicroscopicAnalysis_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                //template_seagate_lpc_coverpage gcms = this.Lpcs.Where(x => x.channel_size.Equals("0.500") && x.id == PKID).FirstOrDefault();
                //if (gcms != null)
                //{
                //    switch (cmd)
                //    {
                //        case RowTypeEnum.Hide:
                //            gcms.row_state = Convert.ToInt32(RowTypeEnum.Hide);
                //            break;
                //        case RowTypeEnum.Normal:
                //            gcms.row_state = Convert.ToInt32(RowTypeEnum.Normal);
                //            break;
                //    }

                //    gvEop.DataSource = this.Lpcs.Where(x => x.row_type == 1 && x.channel_size.Equals("0.500")).ToList();
                //    gvEop.DataBind();
                //}
            }
        }

        protected void gvMicroscopicAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvMicroscopicAnalysis.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvMicroscopicAnalysis_OnDataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Text = "";
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "Particle counton membrane";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "Particle count /component";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "Particle count /1000cm2";
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Controls.Add(cell);
            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Text = "";
            row.Controls.Add(cell);

            //row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            gvMicroscopicAnalysis.HeaderRow.Parent.Controls.AddAt(0, row);
        }

        protected void ddlEop_SelectedIndexChanged(object sender, EventArgs e)
        {

            m_evaluation_of_particles tem = new m_evaluation_of_particles().SelectByID(int.Parse(ddlEop.SelectedValue));

            if (tem != null)
            {

            }
        }

        protected void ddlMa_SelectedIndexChanged(object sender, EventArgs e)
        {

            m_microscopic_analysis tem = new m_microscopic_analysis().SelectByID(int.Parse(ddlEop.SelectedValue));

            if (tem != null)
            {

            }
        }


        #endregion



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


            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.SR_CHEMIST_CHECKING:
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (!String.IsNullOrEmpty(this.jobSample.ad_hoc_tempalte_path))
                    {
                        Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.ad_hoc_tempalte_path));
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


    }
}

