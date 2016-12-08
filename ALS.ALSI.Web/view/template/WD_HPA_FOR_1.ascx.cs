using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.view.request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Linq;
using ALS.ALSI.Web.Properties;
using System.Web.UI.WebControls;
using System.Collections;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using ALS.ALSI.Biz.ReportObjects;
using Microsoft.Reporting.WebForms;
using WordToPDF;
using System.Text.RegularExpressions;
using Spire.Doc;

namespace ALS.ALSI.Web.view.template
{
    public partial class WD_HPA_FOR_1 : System.Web.UI.UserControl
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_HPA_FOR_1));

        #region "Property"

        List<String> errors = new List<string>();



        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public job_sample jobSample
        {
            get { return (job_sample)Session["job_sample"]; }
            set { Session["job_sample"] = value; }
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

        public List<template_wd_hpa_for1_coverpage> HpaFor1
        {
            get { return (List<template_wd_hpa_for1_coverpage>)Session[GetType().Name + "HpaFor1"]; }
            set { Session[GetType().Name + "HpaFor1"] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "HpaFor1");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
        }

        private void initialPage()
        {

            #region "Initial UI Component"
            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt16(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF) + ""));

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

            tb_unit unit = new tb_unit();
            ddlUnit.Items.Clear();
            ddlUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("HPA")).ToList();
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            #endregion
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
                            //btnCalculate.Visible = true;

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

                        }
                        break;
                }

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



                    gvResult.Columns[5].Visible = true;
                    //gvResult_1.Columns[4].Visible = true;
                    txtA23.Enabled = true;
                    txtB23.Enabled = true;
                    txtC23.Enabled = true;
                    txtD23.Enabled = true;
                    txtE23.Enabled = true;
                }
                else
                {
                    gvResult.Columns[5].Visible = false;
                    //gvResult_1.Columns[4].Visible = false;
                    txtA23.Enabled = false;
                    txtB23.Enabled = false;
                    txtC23.Enabled = false;
                    txtD23.Enabled = false;
                    txtE23.Enabled = false;
                }
            }
            #endregion
            this.HpaFor1 = new template_wd_hpa_for1_coverpage().SeletAllBySampleID(this.SampleID);
            if (this.HpaFor1 != null && this.HpaFor1.Count > 0)
            {
                this.CommandName = CommandNameEnum.Edit;

                template_wd_hpa_for1_coverpage _cover = this.HpaFor1[0];
                txtA23.Text = _cover.ParticleAnalysisBySEMEDX;
                txtB23.Text = _cover.TapedAreaForDriveParts;
                txtC23.Text = _cover.NoofTimesTaped;
                txtD23.Text = _cover.SurfaceAreaAnalysed;
                txtE23.Text = _cover.ParticleRanges;

                ddlComponent.SelectedValue = _cover.component_id.ToString();
                ddlSpecification.SelectedValue = _cover.detail_spec_id.ToString();
                ddlUnit.SelectedValue = _cover.unit.ToString();

                detailSpec = new tb_m_detail_spec().SelectByID(_cover.detail_spec_id.Value);
                if (detailSpec != null)
                {
                    lbDocNo.Text = detailSpec.B;
                    lbComponent.Text = detailSpec.A;
                }
                CalculateCas();
                #region "Unit"
                gvResult.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);
                gvResult.Columns[3].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);
                gvResult_1.Columns[2].HeaderText = String.Format("{0}", ddlUnit.SelectedItem.Text);
                #endregion
            }
            else
            {
                this.CommandName = CommandNameEnum.Add;
            }
            //initial component
            //btnSubmit.Enabled = false;
            pCoverPage.Visible = true;
            pDSH.Visible = false;

            btnCoverPage.CssClass = "btn green";
            btnWorkSheet.CssClass = "btn blue";

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
            template_wd_hpa_for1_coverpage objWork = new template_wd_hpa_for1_coverpage();

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

                    foreach (template_wd_hpa_for1_coverpage _cover in this.HpaFor1)
                    {
                        _cover.ParticleAnalysisBySEMEDX = txtA23.Text;
                        _cover.TapedAreaForDriveParts = txtB23.Text;
                        _cover.NoofTimesTaped = txtC23.Text;
                        _cover.SurfaceAreaAnalysed = txtD23.Text;
                        _cover.ParticleRanges = txtE23.Text;

                        _cover.sample_id = this.SampleID;
                        _cover.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        _cover.detail_spec_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        _cover.unit = Convert.ToInt16(ddlUnit.SelectedValue);
                    }
                    objWork.DeleteBySampleID(this.SampleID);
                    objWork.InsertList(this.HpaFor1);

                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;

                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    //#endregion
                    foreach (template_wd_hpa_for1_coverpage _cover in this.HpaFor1)
                    {
                        _cover.ParticleAnalysisBySEMEDX = txtA23.Text;
                        _cover.TapedAreaForDriveParts = txtB23.Text;
                        _cover.NoofTimesTaped = txtC23.Text;
                        _cover.SurfaceAreaAnalysed = txtD23.Text;
                        _cover.ParticleRanges = txtE23.Text;

                        _cover.sample_id = this.SampleID;
                        _cover.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        _cover.detail_spec_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        _cover.unit = Convert.ToInt16(ddlUnit.SelectedValue);
                    }

                    objWork.UpdateList(this.HpaFor1);

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
                        isValid = false;
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
                        isValid = false;
                    }
                    this.jobSample.step7owner = userLogin.id;
                    break;

            }
            //########
            this.jobSample.Update();
            //Commit
            GeneralManager.Commit();

            //removeSession();
            MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(this.PreviousPath);
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            //List<template_wd_hpa_for1_coverpage> lists = this.HpaFor1.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)).OrderBy(x => x.seq).ToList();
            List<template_wd_hpa_for1_coverpage> itemLines = this.HpaFor1.Where(x => x.hpa_type == 7).OrderBy(x => x.seq).ToList();

            //Clear old value
            foreach (template_wd_hpa_for1_coverpage _cov in itemLines)
            {
                _cov.C = 0;
            }

            #region "LOAD"
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

                        String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));
                        String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }
                        _postedFile.SaveAs(source_file);


                        switch (Path.GetExtension(source_file))
                        {
                            case ".jpg":
                                #region "IMAGES"
                                foreach (template_wd_hpa_for1_coverpage _cov in this.HpaFor1)
                                {
                                    _cov.img_path = source_file_url;
                                }
                                #endregion
                                break;
                            default:
                                #region "Raw Data-Arm"
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

                                        //int subIndex = data[0].IndexOf('(') == -1 ? data[0].ToUpper().Replace(" ", String.Empty).Length : data[0].ToUpper().Replace(" ", String.Empty).IndexOf('(');
                                        String compareValue = data[0].Split(' ')[0].ToUpper().Trim();

                                        foreach (template_wd_hpa_for1_coverpage _cov in itemLines)
                                        {
                                            if (_cov.B.ToUpper().Trim().Equals(compareValue))
                                            {
                                                _cov.C += Convert.ToInt32(data[2]);
                                            }

                                        }

                                        Console.WriteLine("");
                                        index++;
                                    }
                                }
                                #endregion
                                break;
                        }
                    }

                }
                catch (Exception Ex)
                {
                    //logger.Error(Ex.Message);
                    Console.WriteLine();
                }

            }
            #endregion

            #region "SET DATA TO FORM"

            #endregion

            //btnSubmit.Enabled = true;
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtD23.Text))
            {
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", String.Format("alert('{0}')", "ยังไม่ได้ป้อนค่า Surface Area Analysed"), true);
                txtD23.Focus();

            }
            else {
                CalculateCas();
            }

        }

        protected void btnCoverPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnCoverPage.CssClass = "btn green";
                    btnWorkSheet.CssClass = "btn blue";
                    pCoverPage.Visible = true;
                    pDSH.Visible = false;
                    //img1.ImageUrl = Configurations.HOST + "" + this.HpaFor1[0].img_path;
                    CalculateCas();
                    break;
                case "Work Sheet":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorkSheet.CssClass = "btn green";
                    pCoverPage.Visible = false;
                    pDSH.Visible = true;
                    break;
            }
        }

        #region "Custom method"


        private void CalculateCas()
        {
            //Item-Line
            List<template_wd_hpa_for1_coverpage> itemLines = this.HpaFor1.Where(x => x.hpa_type == 7).OrderBy(x => x.seq).ToList();
            foreach (template_wd_hpa_for1_coverpage _val in itemLines)
            {
                if (!String.IsNullOrEmpty(txtB23.Text) && !String.IsNullOrEmpty(txtC23.Text) && !String.IsNullOrEmpty(txtD23.Text))
                {
                    //=(ROUND((B53/$C$23/$D$23),0))
                    double _rc = Convert.ToDouble(_val.C);
                    double c23 = Convert.ToDouble(txtC23.Text);
                    double d23 = Convert.ToDouble(txtD23.Text);
                    double result = Math.Round(_rc / c23 / d23, 0);
                    _val.D = result.ToString();
                }
            }
            //Total-Line
            List<template_wd_hpa_for1_coverpage> totalLines = this.HpaFor1.Where(x => x.hpa_type == 8).OrderBy(x => x.seq).ToList();
            foreach (template_wd_hpa_for1_coverpage _val in totalLines)
            {
                _val.C = itemLines.Where(x => x.data_group.Equals(_val.data_group)).Sum(x => Convert.ToInt32(x.C));
                _val.D = itemLines.Where(x => x.data_group.Equals(_val.data_group)).Sum(x => Convert.ToInt32(x.D)).ToString();
            }
            //Result-Line
            List<template_wd_hpa_for1_coverpage> resultLine = this.HpaFor1.Where(x => x.hpa_type == 3).OrderBy(x => x.seq).ToList();
            foreach (template_wd_hpa_for1_coverpage _val in resultLine)
            {
                template_wd_hpa_for1_coverpage mappedValue = totalLines.Where(x => x.B.Equals(mappingRawData((_val.A)))).FirstOrDefault();
                if (mappedValue != null)
                {
                    _val.C = mappedValue.C;
                    if (_val.D != null)
                    {
                        _val.E = _val.D.Equals("NA") ? "NA" : _val.D.Equals("TBD") ? "" : (_val.C >= Convert.ToInt32(_val.D)) ? "FAIL" : "PASS";
                    }
                }



            }

            gvResult.DataSource = this.HpaFor1.Where(x => x.hpa_type == 3).OrderBy(x => x.seq).ToList();
            gvResult.DataBind();

            gvResult_1.DataSource = this.HpaFor1.Where(x => x.hpa_type != 3).OrderBy(x => x.seq).ToList();
            gvResult_1.DataBind();

            //btnSubmit.Enabled = true;
        }

        #endregion

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_component comp = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (comp != null)
            {
                txtA23.Text = comp.B;
                txtB23.Text = comp.G;
                txtC23.Text = comp.D;

                lbA34.Text = comp.G;
                lbA48.Text = comp.G;
            }
        }

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<tb_m_detail_spec> detailSpecs = new tb_m_detail_spec().SelectByTemplateID(this.jobSample.template_id);

            tb_m_detail_spec detailSpec = new tb_m_detail_spec().SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (detailSpec != null)
            {
                tb_m_detail_spec dp = detailSpecs.Where(x => x.A.Equals("Indirect Material")).FirstOrDefault();

                lbDocNo.Text = detailSpec.B;
                lbComponent.Text = detailSpec.A;
                List<template_wd_hpa_for1_coverpage> _list = new List<template_wd_hpa_for1_coverpage>();

                List<String> ANameKey = new List<string>();


                if (!String.IsNullOrEmpty(dp.D))
                {
                    ANameKey.Add(dp.D);
                }
                if (!String.IsNullOrEmpty(dp.E))
                {
                    ANameKey.Add(dp.E);
                }
                if (!String.IsNullOrEmpty(dp.F))
                {
                    ANameKey.Add(dp.F);
                }
                if (!String.IsNullOrEmpty(dp.G))
                {
                    ANameKey.Add(dp.G);
                }
                if (!String.IsNullOrEmpty(dp.H))
                {
                    ANameKey.Add(dp.H);
                }
                if (!String.IsNullOrEmpty(dp.I))
                {
                    ANameKey.Add(dp.I);
                }

                if (!String.IsNullOrEmpty(dp.J))
                {
                    ANameKey.Add(dp.J);
                }
                if (!String.IsNullOrEmpty(dp.K))
                {
                    ANameKey.Add(dp.K);
                }
                if (!String.IsNullOrEmpty(dp.L))
                {
                    ANameKey.Add(dp.L);
                }
                if (!String.IsNullOrEmpty(dp.M))
                {
                    ANameKey.Add(dp.M);
                }
                if (!String.IsNullOrEmpty(dp.N))
                {
                    ANameKey.Add(dp.N);
                }
                if (!String.IsNullOrEmpty(dp.O))
                {
                    ANameKey.Add(dp.O);
                }
                if (!String.IsNullOrEmpty(dp.P))
                {
                    ANameKey.Add(dp.P);
                }
                if (!String.IsNullOrEmpty(dp.Q))
                {
                    ANameKey.Add(dp.Q);
                }

                if (!String.IsNullOrEmpty(dp.R))
                {
                    ANameKey.Add(dp.R);
                }
                if (!String.IsNullOrEmpty(dp.S))
                {
                    ANameKey.Add(dp.S);
                }
                if (!String.IsNullOrEmpty(dp.T))
                {
                    ANameKey.Add(dp.T);
                }
                if (!String.IsNullOrEmpty(dp.U))
                {
                    ANameKey.Add(dp.U);
                }
                if (!String.IsNullOrEmpty(dp.V))
                {
                    ANameKey.Add(dp.V);
                }
                if (!String.IsNullOrEmpty(dp.W))
                {
                    ANameKey.Add(dp.W);
                }
                if (!String.IsNullOrEmpty(dp.X))
                {
                    ANameKey.Add(dp.X);
                }
                if (!String.IsNullOrEmpty(dp.Y))
                {
                    ANameKey.Add(dp.Y);
                }
                if (!String.IsNullOrEmpty(dp.Z))
                {
                    ANameKey.Add(dp.Z);
                }


                int seq = 1;
                #region "Hard Particle Analysis"
                for (int i = 0; i < ANameKey.Count; i++)
                {
                    String _val = ANameKey[i];
                    template_wd_hpa_for1_coverpage _tmp = new template_wd_hpa_for1_coverpage();
                    _tmp.ID = CustomUtils.GetRandomNumberID();
                    _tmp.seq = (i + 1);
                    _tmp.A = _val;
                    switch (i)
                    {
                        case 0: _tmp.D = detailSpec.D; break;
                        case 1: _tmp.D = detailSpec.E; break;
                        case 2: _tmp.D = detailSpec.F; break;
                        case 3: _tmp.D = detailSpec.G; break;
                        case 4: _tmp.D = detailSpec.H; break;
                        case 5: _tmp.D = detailSpec.I; break;
                        case 6: _tmp.D = detailSpec.J; break;
                        case 7: _tmp.D = detailSpec.K; break;
                        case 8: _tmp.D = detailSpec.L; break;
                        case 9: _tmp.D = detailSpec.M; break;
                        case 10: _tmp.D = detailSpec.N; break;
                        case 11: _tmp.D = detailSpec.O; break;
                        case 12: _tmp.D = detailSpec.P; break;
                        case 13: _tmp.D = detailSpec.Q; break;
                        case 14: _tmp.D = detailSpec.R; break;
                        case 15: _tmp.D = detailSpec.S; break;
                        case 16: _tmp.D = detailSpec.T; break;
                        case 17: _tmp.D = detailSpec.U; break;
                        case 18: _tmp.D = detailSpec.V; break;
                        case 19: _tmp.D = detailSpec.W; break;
                        case 20: _tmp.D = detailSpec.X; break;
                        case 21: _tmp.D = detailSpec.Y; break;
                        case 22: _tmp.D = detailSpec.Z; break;
                    }
                    _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                    _tmp.hpa_type = Convert.ToInt32(GVTypeEnum.HPA);
                    _list.Add(_tmp);
                }
                #endregion
                #region "Classification"
                _list.AddRange(getTypesOfParticles(seq));
                #endregion

                this.HpaFor1 = _list;

                gvResult.DataSource = this.HpaFor1.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.HPA));
                gvResult.DataBind();

                gvResult_1.DataSource = this.HpaFor1.Where(x =>
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_TOTAL) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_SUB_TOTAL) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_GRAND_TOTAL));
                gvResult_1.DataBind();

                //btnSubmit.Enabled = true;

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


            DataTable dt = Extenders.ObjectToDataTable(this.HpaFor1[0]);
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);

            List<template_wd_hpa_for1_coverpage> listHpa = this.HpaFor1.Where(x => x.hpa_type == 3).OrderBy(x => x.seq).ToList();
            List<template_wd_hpa_for1_coverpage> listElementalComposition = this.HpaFor1.Where(x =>
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_TOTAL) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_SUB_TOTAL) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_GRAND_TOTAL)).ToList();

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));

            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));

            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", "-"));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on WD's specification Doc No  {0} for {1}", lbDocNo.Text, lbComponent.Text)));
            reportParameters.Add(new ReportParameter("img01Url", Configurations.HOST + "" + this.HpaFor1[0].img_path));
            reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/hpa_for_1_wd.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", listHpa.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", listElementalComposition.ToDataTable())); // Add datasource here






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

                        Document sourceDoc = new Document(Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension);
                        Document destinationDoc = new Document(Server.MapPath("~/template/") + "Blank Letter Head - EL.doc");
                        foreach (Section sec in sourceDoc.Sections)
                        {
                            foreach (DocumentObject obj in sec.Body.ChildObjects)
                            {
                                destinationDoc.Sections[0].Body.ChildObjects.Add(obj.Clone());
                            }
                        }
                        destinationDoc.SaveToFile(Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension);

                        Response.ContentType = mimeType;
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
                        Response.WriteFile(Server.MapPath("~/Report/" + this.jobSample.job_number + "." + extension));
                        Response.Flush();
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


            DataTable dt = Extenders.ObjectToDataTable(this.HpaFor1[0]);
            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);

            List<template_wd_hpa_for1_coverpage> listHpa = this.HpaFor1.Where(x => x.hpa_type == 3).OrderBy(x => x.seq).ToList();
            List<template_wd_hpa_for1_coverpage> listElementalComposition = this.HpaFor1.Where(x =>
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_TOTAL) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_SUB_TOTAL) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_GRAND_TOTAL)).ToList();

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));

            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));

            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("Test", "-"));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on WD's specification Doc No  {0} for {1}", lbDocNo.Text, lbComponent.Text)));
            reportParameters.Add(new ReportParameter("img01Url", Configurations.HOST + "" + this.HpaFor1[0].img_path));
            reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/hpa_for_1_wd_pdf.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", listHpa.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", listElementalComposition.ToDataTable())); // Add datasource here

            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download


        }

        #region "DHS GRID."
        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_wd_hpa_for1_coverpage gcms = this.HpaFor1.Find(x => x.ID == PKID);
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
                    gvResult.DataSource = this.HpaFor1.Where(x => x.hpa_type == 3).OrderBy(x => x.seq).ToList();
                    gvResult.DataBind();
                }
            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvResult.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvResult.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvResult_1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
        }

        protected void gvResult_1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton _btnEdit = (LinkButton)e.Row.FindControl("btnEdit");

                Literal _litB = (Literal)e.Row.FindControl("litB");
                if (gvResult_1.DataKeys[e.Row.RowIndex].Values[2] != null)
                {
                    if (_btnEdit != null) { _btnEdit.Visible = false; }

                    GVTypeEnum cmd = (GVTypeEnum)Enum.ToObject(typeof(GVTypeEnum), (int)gvResult_1.DataKeys[e.Row.RowIndex].Values[2]);
                    switch (cmd)
                    {
                        case GVTypeEnum.CLASSIFICATION_GRAND_TOTAL:
                        case GVTypeEnum.CLASSIFICATION_TOTAL:
                            e.Row.BackColor = System.Drawing.Color.Orange;
                            break;
                        case GVTypeEnum.CLASSIFICATION_SUB_TOTAL:
                            e.Row.BackColor = System.Drawing.Color.Yellow;
                            if (_btnEdit != null) { _btnEdit.Visible = true; }
                            break;
                        case GVTypeEnum.CLASSIFICATION_ITEM:
                            _litB.Text = String.Format("{0}".PadRight(20, ' '), _litB.Text);
                            if (_btnEdit != null) { _btnEdit.Visible = true; }
                            break;
                    }
                }
            }
        }




        protected void gvResult_1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvResult_1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvResult_1.EditIndex = e.NewEditIndex;
            gvResult_1.DataSource = this.HpaFor1.Where(x => x.hpa_type != 3).OrderBy(x => x.seq).ToList();
            gvResult_1.DataBind();
        }

        protected void gvResult_1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvResult_1.EditIndex = -1;
            gvResult_1.DataSource = this.HpaFor1.Where(x => x.hpa_type != 3).OrderBy(x => x.seq).ToList();
            gvResult_1.DataBind();
        }

        protected void gvResult1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvResult_1.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txtC = (TextBox)gvResult_1.Rows[e.RowIndex].FindControl("txtC");


            template_wd_hpa_for1_coverpage _cov = this.HpaFor1.Find(x => x.ID == _id);
            if (_cov != null)
            {
                _cov.C = Convert.ToInt32(txtC.Text);
            }
            gvResult_1.EditIndex = -1;
            CalculateCas();
        }

        #endregion



        private List<template_wd_hpa_for1_coverpage> getTypesOfParticles(int order)
        {
            List<template_wd_hpa_for1_coverpage> _Hpas = new List<template_wd_hpa_for1_coverpage>();

            List<String> items = new List<string>();


            /*
            # = Group
            - = Total
            $ = Grand Total
            -------------------------
            */
            items.Add("#Hard Particles");
            items.Add("Al-O");
            items.Add("Al-Si-O");
            items.Add("Si-O");
            items.Add("Si-C");
            items.Add("Al-Cu-O");
            items.Add("Al-Mg-O");
            items.Add("Al-Si-Cu-O");
            items.Add("Al-Si-Fe-O");
            items.Add("Al-Si-Mg-O");
            items.Add("Al-Ti-O");
            items.Add("Ti-O");
            items.Add("Ti-C");
            items.Add("Ti-B");
            items.Add("Ti-N");
            items.Add("W-O");
            items.Add("W-C");
            items.Add("Zr-O");
            items.Add("Zr-C");
            items.Add("Pb-Zr-Ti-O (PZT)");
            items.Add("-Total - Hard Particles");
            items.Add("#Magnetic Particles");
            //items.Add("Ce-Co");
            items.Add("Fe-Nd");
            items.Add("Sm-Co");
            items.Add("Fe-Sr");
            //items.Add("Fe-Sm");
            //items.Add("Nd-Pr");
            //items.Add("Ni-Co");
            items.Add("-Total - Magnetic Particles");
            items.Add("#Steel Particle");
            items.Add("SS 300- Fe-Cr-Ni");
            items.Add("SS 300- Fe-Cr-Ni-Mn");
            items.Add("SS 300- Fe-Cr-Ni-Si");
            items.Add("SS 400- Fe-Cr ");
            items.Add("SS 400- Fe-Cr-Mn");
            items.Add("Other Steel - Fe");
            items.Add("Other Steel - Fe-Mn");
            items.Add("Other Steel - Fe-Ni");
            items.Add("Other Steel - Fe-O");
            items.Add("-Total - Steel Particle");
            items.Add("#Cr-Rich");
            items.Add("*Mg-Si-O");
            items.Add("Cr-O");
            items.Add("Cr-Mn");
            items.Add("-Total -Cr-Rich");
            items.Add("#Fe Base");
            items.Add("Fe-Cu");
            items.Add("Fe-Cr/S");
            items.Add("SCrMn/Fe");
            items.Add("Pb");
            items.Add("-Total - Fe Base Particle");
            items.Add("#Ni-Base");
            items.Add("Ni");
            items.Add("Ni-P");
            items.Add("NiP/Al");
            items.Add("NiP/Fe");
            items.Add("NiP Base");
            items.Add("-Total - Ni-Base Particle");
            items.Add("#Al Based");
            items.Add("Al");
            items.Add("Al-Mg");
            items.Add("Al-Ti-Si");
            items.Add("Al-Cu");
            items.Add("Al-Si-Cu");
            items.Add("Al-Si/Fe");
            items.Add("Al-Si-Mg");
            items.Add("Mg-Si-O-Al");
            items.Add("Al-S/Si");
            items.Add("Al-Si Base");
            items.Add("AlSi/Fe-Cr-Ni-Mn-Cu");
            items.Add("-Total - Al Based Particle");
            items.Add("#Cu-Zn base");
            items.Add("Zn");
            items.Add("Cu");
            items.Add("Cu-Zn");
            items.Add("Cu-S");
            items.Add("Cu-Zn Base");
            items.Add("Cu-S-Al-O Base");
            items.Add("Cu-Au");
            items.Add("-Total - Cu-Zn Base Particle");
            //items.Add("#Other");
            items.Add("Sn Base");
            items.Add("Sb Base");
            items.Add("Ba-S Base");
            items.Add("Ag-S");
            items.Add("Ti-O/Al-Si-Fe");
            items.Add("Ti Base");
            items.Add("AlSi/K");
            items.Add("Ca");
            items.Add("Na-Cl");
            items.Add("F-O");
            items.Add("Other");
            items.Add("Disk Material");
            items.Add("$Grand Total of All Particles");




            String LastGroup = String.Empty;
            foreach (String item in items)
            {
                if (item.StartsWith("#"))
                {
                    LastGroup = item;
                }

                template_wd_hpa_for1_coverpage _tmp = new template_wd_hpa_for1_coverpage();
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
            foreach (template_wd_hpa_for1_coverpage _cov in _Hpas.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)))
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
            mappingValues["Total hard Particle (Including Magnetic Particles)"] = "Total - Hard Particles";
            mappingValues["Total MgSiO Particles"] = "Total - Magnetic Particles";
            mappingValues["Total Steel Particles"] = "Total - Steel Particle";
            mappingValues["Total Magnetic Particles"] = "Total - Magnetic Particles";

            //SST400s(Fe / Cr)



            foreach (DictionaryEntry entry in mappingValues)
            {
                if (entry.Key.Equals(Regex.Replace(_val, @"\r\n?|\n", "")))
                {
                    result = entry.Value.ToString();
                    break;
                }
            }
            return result;
        }


        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

            gvResult.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvResult.Columns[3].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvResult_1.Columns[2].HeaderText = String.Format("{0}", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));

            ModolPopupExtender.Show();
            CalculateCas();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }


    }
}