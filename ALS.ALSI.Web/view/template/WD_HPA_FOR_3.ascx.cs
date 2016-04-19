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

namespace ALS.ALSI.Web.view.template
{
    public partial class WD_HPA_FOR_3 : System.Web.UI.UserControl
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_HPA_FOR_3));

        #region "Property"

        //private String[] GetHPAHeader = { "Total Hard Particles", "Total MgSiO Particles", "Total Steel Particles", "Total Magnetic Particles", "Other Particle" };

        private Hashtable GetHPAData()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("Hard Particles",
                new String[]{
                          "Al-O",
                          "Al-Si-O",
                          "Si-O",
                          "Si-C",
                          "Al-Cu-O",
                          "Al-Mg-O",
                          "Al-Si-Cu-O",
                          "Al-Si-Fe-O",
                          "Al-Si-Mg-O",
                          "Al-Ti-O",
                          "Ti-O",
                          "Ti-C",
                          "Ti-B",
                          "Ti-N",
                          "W-O",
                          "W-C",
                          "Zr-O",
                          "Zr-C",
                          "Pb-Zr-Ti-O"
                           });
            hashtable.Add("MgSiO Particles",
                new String[]{
                          "Mg-Si-O",
                           });
            hashtable.Add("Steel Particles",
                new String[]{
                          "SS 300- Fe-Cr-Ni",
                          "SS 300- Fe-Cr-Ni-Mn",
                          "SS 300- Fe-Cr-Ni-Si",
                          "SS 400- Fe-Cr",
                          "SS 400- Fe-Cr-Mn",
                          "Other Steel - Fe",
                          "Other Steel - Fe-Mn",
                          "Other Steel - Fe-Ni",
                          "Other Steel - Fe-O"
                           });
            hashtable.Add("Magnetic Particles",
                new String[]{
                          "Ce-Co",
                          "Fe-Nd",
                          "Fe-Sm",
                          "Fe-Sr",
                          "Nd-Pr",
                          "Ni-Co",
                          "Sm-Co"
                           });
            hashtable.Add("Other Particle",
                new String[]{
                          "No Element",
                          "Ni",
                          "Ni-P",
                          "Sn base",
                          "Other",
                          "Al",
                          "Al-Mg",
                          "Al-Ti",
                          "Al-Si (1)",
                          "Al-Si (2)",
                          "Si",
                          "Al-Cu",
                          "Al-Si-Cu",
                          "Al-Si-Fe",
                          "Al-Si-Mg",
                          "Mg-Si-O-Al",
                          "Ti",
                          "Nd",
                          "S-Cr-Mn",
                          "Zn base",
                          "Ca base",
                          "Ni base",
                          "Cr base",
                          "Zr base",
                          "Cl base",
                          "Na-Cl",
                          "Cu",
                          "Cu-Au",
                          "Ag-S",
                          "Au-Ni",
                          "Ag",
                          "Au",
                          "Cu-Au-Ni",
                          "Cu-Zn",
                          "Cu-Zn-Ni",
                          "Cu-Zn-Au-Ni",
                          "Zn-O",
                          "Pb",
                          "Fe-Cu",
                          "Cr-Mn",
                          "Al-Si Base",
                          "F-O",
                          "Cu-S-Al-O Base",
                          "Ti-O/Al-Si-Fe",
                          "Cr-Rich (Cr Base + Cr-Mn)"
                           });
            return hashtable;
        }
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

        public List<template_wd_hpa_for3_coverpage> HpaFor3
        {
            get { return (List<template_wd_hpa_for3_coverpage>)Session[GetType().Name + "HpaFor3"]; }
            set { Session[GetType().Name + "HpaFor3"] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "HpaFor3");
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
                btnCalculate.Visible = false;

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
                            btnCalculate.Visible = false;
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
                            btnCalculate.Visible = true;

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
                            btnCalculate.Visible = true;

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
                            btnCalculate.Visible = false;

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
                            btnCalculate.Visible = false;

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
                    gvARM.Columns[4].Visible = true;
                    gvPivot.Columns[4].Visible = true;
                    gvSwage.Columns[4].Visible = true;
                    txtA23.ReadOnly = false;
                    txtB23.ReadOnly = false;
                    txtC23.ReadOnly = false;
                    txtD23.ReadOnly = false;
                    txtE23.ReadOnly = false;

                    txtA24.ReadOnly = false;
                    txtB24.ReadOnly = false;
                    txtC24.ReadOnly = false;
                    txtD24.ReadOnly = false;
                    txtE24.ReadOnly = false;

                    txtA25.ReadOnly = false;
                    txtB25.ReadOnly = false;
                    txtC25.ReadOnly = false;
                    txtD25.ReadOnly = false;
                    txtE25.ReadOnly = false;
                }
                else
                {
                    gvResult.Columns[5].Visible = false;
                    gvARM.Columns[4].Visible = false;
                    gvPivot.Columns[4].Visible = false;
                    gvSwage.Columns[4].Visible = false;
                    txtA23.ReadOnly = true;
                    txtB23.ReadOnly = true;
                    txtC23.ReadOnly = true;
                    txtD23.ReadOnly = true;
                    txtE23.ReadOnly = true;

                    txtA24.ReadOnly = true;
                    txtB24.ReadOnly = true;
                    txtC24.ReadOnly = true;
                    txtD24.ReadOnly = true;
                    txtE24.ReadOnly = true;

                    txtA25.ReadOnly = true;
                    txtB25.ReadOnly = true;
                    txtC25.ReadOnly = true;
                    txtD25.ReadOnly = true;
                    txtE25.ReadOnly = true;
                }
            }
            #endregion
            this.HpaFor3 = template_wd_hpa_for3_coverpage.FindAllBySampleID(this.SampleID);
            if (this.HpaFor3 != null && this.HpaFor3.Count > 0)
            {
                this.CommandName = CommandNameEnum.Edit;

                template_wd_hpa_for3_coverpage _cover = this.HpaFor3[0];
                txtA23.Text = _cover.ParticleAnalysisBySEMEDX;
                txtB23.Text = _cover.TapedAreaForDriveParts;
                txtC23.Text = _cover.NoofTimesTaped;
                txtD23.Text = _cover.SurfaceAreaAnalysed;
                txtE23.Text = _cover.ParticleRanges;

                txtA24.Text = _cover.ParticleAnalysisBySEMEDX_1;
                txtB24.Text = _cover.TapedAreaForDriveParts_1;
                txtC24.Text = _cover.NoofTimesTaped_1;
                txtD24.Text = _cover.SurfaceAreaAnalysed_1;
                txtE24.Text = _cover.ParticleRanges_1;

                txtA25.Text = _cover.ParticleAnalysisBySEMEDX_2;
                txtB25.Text = _cover.TapedAreaForDriveParts_2;
                txtC25.Text = _cover.NoofTimesTaped_2;
                txtD25.Text = _cover.SurfaceAreaAnalysed_2;
                txtE25.Text = _cover.ParticleRanges_2;


                ddlComponent.SelectedValue = _cover.component_id.ToString();
                ddlSpecification.SelectedValue = _cover.detail_spec_id.ToString();

                //img1.ImageUrl = Configurations.HOST + "" + this.HpaFor3[0].img_path;
                //img2.ImageUrl = Configurations.HOST + "" + this.HpaFor3[0].img_path1;
                //img3.ImageUrl = Configurations.HOST + "" + this.HpaFor3[0].img_path2;

                //gvResult.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_ARM) ||
                //                    x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_PIVOT) ||
                //                    x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_SWAGE) ||
                //                    x.row_group == Convert.ToInt32(HPAFor3Group.AVERAGE_RESULT) ||
                //                    x.row_group == Convert.ToInt32(HPAFor3Group.SPECIFICATION) ||
                //                    x.row_group == Convert.ToInt32(HPAFor3Group.PASS_SLASH_FAIL)).OrderBy(x => x.seq);
                //gvResult.DataBind();

                //gvARM.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) ||
                //                                    x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) ||
                //                                    x.row_group == Convert.ToInt32(HPAFor3Group.ARM_GRAND_TOTAL)).OrderBy(x => x.seq);
                //gvARM.DataBind();

                //gvPivot.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) ||
                //                    x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) ||
                //                    x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_GRAND_TOTAL)).OrderBy(x => x.seq);
                //gvPivot.DataBind();

                //gvSwage.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) ||
                //        x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) ||
                //        x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_GRAND_TOTAL)).OrderBy(x => x.seq);
                //gvSwage.DataBind();

                CalculateCas();
            }
            else
            {
                this.CommandName = CommandNameEnum.Add;
            }
            //initial component
            btnSubmit.Enabled = false;
            pCoverPage.Visible = true;
            pDSH.Visible = false;

            btnCoverPage.CssClass = "btn green";
            btnWorkSheet.CssClass = "btn blue";
            btnSubmit.Enabled = false;


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
            template_wd_hpa_for3_coverpage objWork = new template_wd_hpa_for3_coverpage();

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

                    foreach (template_wd_hpa_for3_coverpage _cover in this.HpaFor3)
                    {
                        _cover.ParticleAnalysisBySEMEDX = txtA23.Text;
                        _cover.TapedAreaForDriveParts = txtB23.Text;
                        _cover.NoofTimesTaped = txtC23.Text;
                        _cover.SurfaceAreaAnalysed = txtD23.Text;
                        _cover.ParticleRanges = txtE23.Text;

                        _cover.ParticleAnalysisBySEMEDX_1 = txtA24.Text;
                        _cover.TapedAreaForDriveParts_1 = txtB24.Text;
                        _cover.NoofTimesTaped_1 = txtC24.Text;
                        _cover.SurfaceAreaAnalysed_1 = txtD24.Text;
                        _cover.ParticleRanges_1 = txtE24.Text;

                        _cover.ParticleAnalysisBySEMEDX_2 = txtA25.Text;
                        _cover.TapedAreaForDriveParts_2 = txtB25.Text;
                        _cover.NoofTimesTaped_2 = txtC25.Text;
                        _cover.SurfaceAreaAnalysed_2 = txtD25.Text;
                        _cover.ParticleRanges_2 = txtE25.Text;

                        _cover.sample_id = this.SampleID;
                        _cover.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        _cover.detail_spec_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                    }
                    switch (this.CommandName)
                    {
                        case CommandNameEnum.Add:
                            objWork.InsertList(this.HpaFor3);
                            break;
                        case CommandNameEnum.Edit:
                            objWork.UpdateList(this.HpaFor3);
                            break;
                    }
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;

                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    //#endregion
                    foreach (template_wd_hpa_for3_coverpage _cover in this.HpaFor3)
                    {
                        _cover.ParticleAnalysisBySEMEDX = txtA23.Text;
                        _cover.TapedAreaForDriveParts = txtB23.Text;
                        _cover.NoofTimesTaped = txtC23.Text;
                        _cover.SurfaceAreaAnalysed = txtD23.Text;
                        _cover.ParticleRanges = txtE23.Text;

                        _cover.ParticleAnalysisBySEMEDX_1 = txtA24.Text;
                        _cover.TapedAreaForDriveParts_1 = txtB24.Text;
                        _cover.NoofTimesTaped_1 = txtC24.Text;
                        _cover.SurfaceAreaAnalysed_1 = txtD24.Text;
                        _cover.ParticleRanges_1 = txtE24.Text;

                        _cover.ParticleAnalysisBySEMEDX_2 = txtA25.Text;
                        _cover.TapedAreaForDriveParts_2 = txtB25.Text;
                        _cover.NoofTimesTaped_2 = txtC25.Text;
                        _cover.SurfaceAreaAnalysed_2 = txtD25.Text;
                        _cover.ParticleRanges_2 = txtE25.Text;

                        _cover.sample_id = this.SampleID;
                        _cover.component_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        _cover.detail_spec_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                    }

                    objWork.UpdateList(this.HpaFor3);
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
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();
            }
            else {
                this.jobSample.Update();
                //Commit
                GeneralManager.Commit();

                //removeSession();
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

            List<template_wd_hpa_for3_coverpage> listArm = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM)).OrderBy(x => x.seq).ToList();
            List<template_wd_hpa_for3_coverpage> listPivot = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT)).OrderBy(x => x.seq).ToList();
            List<template_wd_hpa_for3_coverpage> listSwage = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE)).OrderBy(x => x.seq).ToList();

            #region "LOAD"
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");

            //int imgIndex = 1;
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
                                template_wd_hpa_for3_coverpage _covp = this.HpaFor3[0];
                                if (string.IsNullOrEmpty(_covp.img_path))
                                {
                                    _covp.img_path = source_file_url;
                                }
                                else if (string.IsNullOrEmpty(_covp.img_path1))
                                {
                                    _covp.img_path1 = source_file_url;
                                }
                                else if (string.IsNullOrEmpty(_covp.img_path2))
                                {
                                    _covp.img_path2 = source_file_url;
                                }
                                //switch (imgIndex)
                                //{
                                //    case 1:
                                //        _covp.img_path = source_file_url;
                                //        break;
                                //    case 2:
                                //        _covp.img_path1 = source_file_url;
                                //        break;
                                //    case 3:
                                //        _covp.img_path2 = source_file_url;
                                //        break;
                                //}
                                //imgIndex++;
                                #endregion
                                break;
                            default:
                                int rc = 0;
                                #region "Raw Data-Arm"
                                using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                                {
                                    HSSFWorkbook wd = new HSSFWorkbook(fs);
                                    #region "Raw Data-Arm"
                                    ISheet sheet1 = wd.GetSheet("Raw Data-Arm");
                                    if (sheet1 == null)
                                    {
                                        MessageBox.Show(this.Page, String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", sheet1.SheetName));
                                    }
                                    else
                                    {
                                        foreach (template_wd_hpa_for3_coverpage _cov in listArm)
                                        {

                                            for (int c = 0; c < 100; c++)
                                            {
                                                String typesOfParticles = CustomUtils.GetCellValue(sheet1.GetRow(0).GetCell(c));
                                                if (_cov.B.Equals(typesOfParticles))
                                                {
                                                    for (int row = 1; row <= sheet1.LastRowNum; row++)
                                                    {
                                                        String rank = CustomUtils.GetCellValue(sheet1.GetRow(row).GetCell(3));
                                                        String value = CustomUtils.GetCellValue(sheet1.GetRow(row).GetCell(c));
                                                        if (!rank.Equals("Rejected (ED)") && value.Equals("1"))
                                                        {
                                                            rc++;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            template_wd_hpa_for3_coverpage _hpa = this.HpaFor3.Where(x => x.ID == _cov.ID).FirstOrDefault();
                                            if (_hpa != null)
                                            {
                                                _hpa.C = rc.ToString();
                                                rc = 0;
                                            }
                                        }

                                        Console.WriteLine("");

                                    }
                                    #endregion
                                    #region "Raw Data-Arm"
                                    ISheet sheet2 = wd.GetSheet("Raw Data-Pivot");
                                    if (sheet2 == null)
                                    {
                                        MessageBox.Show(this.Page, String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", sheet2.SheetName));
                                    }
                                    else
                                    {
                                        rc = 0;
                                        foreach (template_wd_hpa_for3_coverpage _cov in listPivot)
                                        {

                                            for (int c = 0; c < 100; c++)
                                            {
                                                String typesOfParticles = CustomUtils.GetCellValue(sheet2.GetRow(0).GetCell(c));
                                                if (_cov.B.Equals(typesOfParticles))
                                                {
                                                    for (int row = 1; row <= sheet2.LastRowNum; row++)
                                                    {
                                                        String rank = CustomUtils.GetCellValue(sheet2.GetRow(row).GetCell(3));
                                                        String value = CustomUtils.GetCellValue(sheet2.GetRow(row).GetCell(c));
                                                        if (!rank.Equals("Rejected (ED)") && value.Equals("1"))
                                                        {
                                                            rc++;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            //_cov.RawCounts = rc;
                                            template_wd_hpa_for3_coverpage _hpa = this.HpaFor3.Where(x => x.ID == _cov.ID).FirstOrDefault();
                                            if (_hpa != null)
                                            {
                                                _hpa.C = rc.ToString();
                                                rc = 0;
                                            }
                                        }
                                        Console.WriteLine("");

                                    }
                                    #endregion
                                    #region "Raw Data-Arm"
                                    ISheet sheet3 = wd.GetSheet("Raw Data-Swage");
                                    if (sheet3 == null)
                                    {
                                        MessageBox.Show(this.Page, String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", sheet3.SheetName));
                                    }
                                    else
                                    {
                                        rc = 0;
                                        foreach (template_wd_hpa_for3_coverpage _cov in listSwage)
                                        {
                                            for (int c = 0; c < 100; c++)
                                            {
                                                String typesOfParticles = CustomUtils.GetCellValue(sheet3.GetRow(0).GetCell(c));
                                                if (_cov.B.Equals(typesOfParticles))
                                                {
                                                    for (int row = 1; row <= sheet3.LastRowNum; row++)
                                                    {
                                                        String rank = CustomUtils.GetCellValue(sheet3.GetRow(row).GetCell(3));
                                                        String value = CustomUtils.GetCellValue(sheet3.GetRow(row).GetCell(c));
                                                        if (!rank.Equals("Rejected (ED)") && value.Equals("1"))
                                                        {
                                                            rc++;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            //_cov.RawCounts = rc;
                                            template_wd_hpa_for3_coverpage _hpa = this.HpaFor3.Where(x => x.ID == _cov.ID).FirstOrDefault();
                                            if (_hpa != null)
                                            {
                                                _hpa.C = rc.ToString();
                                                rc = 0;
                                            }
                                        }
                                        Console.WriteLine("");

                                    }
                                    #endregion
                                }
                                #endregion
                                break;
                        }
                    }

                }
                catch (Exception Ex)
                {
                    ////logger.Error(Ex.Message);
                    Console.WriteLine();
                }

            }
            #endregion

            #region "SET DATA TO FORM"

            #endregion
            CalculateCas();
            btnSubmit.Enabled = true;
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculateCas();
            btnSubmit.Enabled = true;
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
                    //img1.ImageUrl = this.HpaFor3[0].img_path;
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

        //private String validateDSHFile(IList<HttpPostedFile> _files)
        //{
        //    Boolean isFound_b1 = false;
        //    Boolean isFound_s1 = false;
        //    Boolean isFound_hb1 = false;
        //    Boolean isFound_hs1 = false;
        //    Boolean isFoundWrongExtension = false;

        //    String result = String.Empty;

        //    String[] files = new String[_files.Count];
        //    if (files.Length == 4)
        //    {
        //        for (int i = 0; i < _files.Count; i++)
        //        {
        //            files[i] = _files[i].FileName;
        //            if (!Path.GetExtension(_files[i].FileName).Trim().ToLower().Equals(".xls"))
        //            {
        //                isFoundWrongExtension = true;
        //                break;
        //            }
        //        }
        //        if (!isFoundWrongExtension)
        //        {

        //            //Find B1
        //            foreach (String file in files)
        //            {
        //                if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("B"))
        //                {
        //                    isFound_b1 = true;
        //                    break;
        //                }
        //            }

        //            //Find S1
        //            foreach (String file in files)
        //            {
        //                if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("S"))
        //                {
        //                    isFound_s1 = true;
        //                    break;
        //                }
        //            }

        //            //Find S1
        //            foreach (String file in files)
        //            {
        //                if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("HPA(B)"))
        //                {
        //                    isFound_hb1 = true;
        //                    break;
        //                }
        //            }

        //            //Find S1
        //            foreach (String file in files)
        //            {
        //                if (Path.GetFileNameWithoutExtension(file).ToUpper().Equals("HPA(S)"))
        //                {
        //                    isFound_hs1 = true;
        //                    break;
        //                }
        //            }
        //            result = (!isFound_b1) ? result += "File not found B.xls" :
        //                        (!isFound_s1) ? result += "File not found S.xls" :
        //                        (!isFound_hb1) ? result += "File not found HPA(B).xls" :
        //                        (!isFound_hs1) ? result += "File not found HPA(S)xls" : String.Empty;
        //        }
        //        else
        //        {
        //            result = "File extension must be *.xls";
        //        }
        //    }
        //    else
        //    {
        //        result = "You must to select 4 files for upload.";
        //    }
        //    return result;
        //}

        private void CalculateCas()
        {

            #region "CAL::Particle/cm2"

            List<template_wd_hpa_for3_coverpage> lists = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) ||
                        x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) ||
                        x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE)
                ).OrderBy(x => x.seq).ToList();

            foreach (template_wd_hpa_for3_coverpage _val in lists)
            {
                if (!String.IsNullOrEmpty(_val.C) && !String.IsNullOrEmpty(txtC23.Text) && !String.IsNullOrEmpty(txtD23.Text))
                {
                    //=(ROUND((B53/$C$23/$D$23),0))
                    double _rc = Convert.ToDouble(_val.C);
                    double c23 = 0.0;
                    double d23 = Convert.ToDouble(txtD23.Text);
                    HPAFor3Group group = (HPAFor3Group)Enum.ToObject(typeof(HPAFor3Group), _val.row_group);
                    switch (group)
                    {
                        case HPAFor3Group.RAWDATA_ARM:
                            c23 = Convert.ToDouble(txtC23.Text);
                            break;
                        case HPAFor3Group.RAWDATA_PIVOT:
                            c23 = Convert.ToDouble(txtC24.Text);
                            break;
                        case HPAFor3Group.RAWDATA_SWAGE:
                            c23 = Convert.ToDouble(txtC25.Text);
                            break;
                    }
                    double result = Math.Round(_rc / c23 / d23, 0);
                    _val.D = result.ToString();
                }
            }
            #endregion
            #region "CAL::Result on Arm"
            template_wd_hpa_for3_coverpage arm = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_ARM)).FirstOrDefault();
            if (arm != null)
            {

                Double result = (Double)this.HpaFor3.Where(x => x.A.Equals("Hard Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                arm.B = result.ToString();
                template_wd_hpa_for3_coverpage tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) && x.B.Equals("Hard Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Hard Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("MgSiO Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                arm.C = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) && x.B.Equals("MgSiO Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("MgSiO Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("Steel Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                arm.D = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) && x.B.Equals("Steel Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Steel Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("Magnetic Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                arm.E = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) && x.B.Equals("Magnetic Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Magnetic Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("Other Particle") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                arm.E = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) && x.B.Equals("Other Particle")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Other Particle") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
            }
            #endregion

            #region "CAL::Result on Pivot"
            template_wd_hpa_for3_coverpage pivot = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_PIVOT)).FirstOrDefault();
            if (pivot != null)
            {

                Double result = (Double)this.HpaFor3.Where(x => x.A.Equals("Hard Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                pivot.B = result.ToString();
                template_wd_hpa_for3_coverpage tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) && x.B.Equals("Hard Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Hard Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("MgSiO Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                pivot.C = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) && x.B.Equals("MgSiO Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("MgSiO Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("Steel Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                pivot.D = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) && x.B.Equals("Steel Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Steel Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("Magnetic Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                pivot.E = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) && x.B.Equals("Magnetic Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Magnetic Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("Other Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                pivot.E = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) && x.B.Equals("Other Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Other Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
            }
            #endregion

            #region "CAL::Result on Swage"
            template_wd_hpa_for3_coverpage swage = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_SWAGE)).FirstOrDefault();
            if (pivot != null)
            {

                Double result = (Double)this.HpaFor3.Where(x => x.A.Equals("Hard Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                swage.B = result.ToString();
                template_wd_hpa_for3_coverpage tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) && x.B.Equals("Hard Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Hard Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("MgSiO Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                swage.C = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) && x.B.Equals("MgSiO Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("MgSiO Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("Steel Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                swage.D = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) && x.B.Equals("Steel Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Steel Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("Magnetic Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                swage.E = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) && x.B.Equals("Magnetic Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Magnetic Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
                result = (Double)this.HpaFor3.Where(x => x.A.Equals("Other Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                swage.E = result.ToString();
                tmp = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) && x.B.Equals("Other Particles")).FirstOrDefault();
                if (tmp != null)
                {
                    Double resultC = (Double)this.HpaFor3.Where(x => x.A.Equals("Other Particles") && x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                    tmp.C = resultC.ToString();
                    tmp.D = result.ToString();
                }
            }
            #endregion

            #region "Grand Total"
            template_wd_hpa_for3_coverpage grandTotalArm = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.ARM_GRAND_TOTAL)).FirstOrDefault();
            if (grandTotalArm != null)
            {
                Double resultC = (Double)this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                grandTotalArm.C = resultC.ToString();
                Double resultD = (Double)this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                grandTotalArm.D = resultD.ToString();
            }
            template_wd_hpa_for3_coverpage grandTotalPivot = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_GRAND_TOTAL)).FirstOrDefault();
            if (grandTotalPivot != null)
            {
                Double resultC = (Double)this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                grandTotalPivot.C = resultC.ToString();
                Double resultD = (Double)this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                grandTotalPivot.D = resultD.ToString();
            }
            template_wd_hpa_for3_coverpage grandTotalSwage = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_GRAND_TOTAL)).FirstOrDefault();
            if (grandTotalSwage != null)
            {
                Double resultC = (Double)this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.C)).Sum(x => Convert.ToInt32(x.C));
                grandTotalSwage.C = resultC.ToString();
                Double resultD = (Double)this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) && !String.IsNullOrEmpty(x.D)).Sum(x => Convert.ToInt32(x.D));
                grandTotalSwage.D = resultD.ToString();
            }
            #endregion


            #region "CAL::Average result"
            template_wd_hpa_for3_coverpage _arm = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_ARM)).FirstOrDefault();
            template_wd_hpa_for3_coverpage _pivot = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_PIVOT)).FirstOrDefault();
            template_wd_hpa_for3_coverpage _swage = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_SWAGE)).FirstOrDefault();
            template_wd_hpa_for3_coverpage _average = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.AVERAGE_RESULT)).FirstOrDefault();
            if (_average != null && _arm != null && _pivot != null && _swage != null)
            {
                _average.B = Math.Round((Convert.ToDouble(_arm.B) + Convert.ToDouble(_pivot.B) + Convert.ToDouble(_swage.B)) / 3).ToString();
                _average.C = Math.Round((Convert.ToDouble(_arm.C) + Convert.ToDouble(_pivot.C) + Convert.ToDouble(_swage.C)) / 3).ToString();
                _average.D = Math.Round((Convert.ToDouble(_arm.D) + Convert.ToDouble(_pivot.D) + Convert.ToDouble(_swage.D)) / 3).ToString();
                _average.E = Math.Round((Convert.ToDouble(_arm.E) + Convert.ToDouble(_pivot.E) + Convert.ToDouble(_swage.E)) / 3).ToString();
            }


            #endregion

            #region "PASS / FAIL"
            //=IF(B34="NA","NA",IF(B33>=INDEX('Detail Spec'!$A$3:$G$63,$F$1,4),"FAIL","PASS"))
            template_wd_hpa_for3_coverpage _spec = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.AVERAGE_RESULT)).FirstOrDefault();
            template_wd_hpa_for3_coverpage _pass_fail = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.PASS_SLASH_FAIL)).FirstOrDefault();
            if (_pass_fail != null && _spec != null)
            {
                _pass_fail.B = _spec.B.Equals("NA") ? "NA" : Convert.ToDouble(_average.B) > Convert.ToDouble(_spec.B.Replace('<', ' ').Trim()) ? "FAIL" : "PASS";
                _pass_fail.C = _spec.C.Equals("NA") ? "NA" : Convert.ToDouble(_average.C) > Convert.ToDouble(_spec.C.Replace('<', ' ').Trim()) ? "FAIL" : "PASS";
                _pass_fail.D = _spec.D.Equals("NA") ? "NA" : Convert.ToDouble(_average.D) > Convert.ToDouble(_spec.D.Replace('<', ' ').Trim()) ? "FAIL" : "PASS";
                _pass_fail.E = _spec.E.Equals("NA") ? "NA" : Convert.ToDouble(_average.E) > Convert.ToDouble(_spec.E.Replace('<', ' ').Trim()) ? "FAIL" : "PASS";

            }
            #endregion


            img1.ImageUrl = Configurations.HOST + "" + this.HpaFor3[0].img_path;
            img2.ImageUrl = Configurations.HOST + "" + this.HpaFor3[0].img_path1;
            img3.ImageUrl = Configurations.HOST + "" + this.HpaFor3[0].img_path2;

            gvResult.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_ARM) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_PIVOT) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_SWAGE) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.AVERAGE_RESULT) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.SPECIFICATION) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.PASS_SLASH_FAIL)).OrderBy(x => x.seq);
            gvResult.DataBind();

            gvARM.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) ||
                                                x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) ||
                                                x.row_group == Convert.ToInt32(HPAFor3Group.ARM_GRAND_TOTAL)).OrderBy(x => x.seq);
            gvARM.DataBind();

            gvPivot.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_GRAND_TOTAL)).OrderBy(x => x.seq);
            gvPivot.DataBind();

            gvSwage.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) ||
                    x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) ||
                    x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_GRAND_TOTAL)).OrderBy(x => x.seq);
            gvSwage.DataBind();

        }

        #endregion

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_component comp = new tb_m_component().SelectByID(int.Parse(ddlComponent.SelectedValue));
            if (comp != null)
            {
                txtA23.Text = String.Format("Procedure No:{0}", comp.B);
                txtC23.Text = comp.D;
                txtC24.Text = comp.E;
                txtC25.Text = comp.F;
            }
        }

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_detail_spec detailSpec = new tb_m_detail_spec().SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (detailSpec != null)
            {
                lbDocNo.Text = detailSpec.B;
                lbComponent.Text = detailSpec.A;

                List<template_wd_hpa_for3_coverpage> list = new List<template_wd_hpa_for3_coverpage>();

                template_wd_hpa_for3_coverpage _tmp = new template_wd_hpa_for3_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = 1;
                _tmp.A = Constants.GetEnumDescription(HPAFor3Group.RESULT_ON_ARM);
                _tmp.B = String.Empty;
                _tmp.C = String.Empty;
                _tmp.D = String.Empty;
                _tmp.E = String.Empty;
                _tmp.row_group = Convert.ToInt32(HPAFor3Group.RESULT_ON_ARM);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                list.Add(_tmp);

                _tmp = new template_wd_hpa_for3_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = 2;
                _tmp.A = Constants.GetEnumDescription(HPAFor3Group.RESULT_ON_PIVOT);
                _tmp.B = String.Empty;
                _tmp.C = String.Empty;
                _tmp.D = String.Empty;
                _tmp.E = String.Empty;
                _tmp.row_group = Convert.ToInt32(HPAFor3Group.RESULT_ON_PIVOT);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                list.Add(_tmp);

                _tmp = new template_wd_hpa_for3_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = 3;
                _tmp.A = Constants.GetEnumDescription(HPAFor3Group.RESULT_ON_SWAGE);
                _tmp.B = String.Empty;
                _tmp.C = String.Empty;
                _tmp.D = String.Empty;
                _tmp.E = String.Empty;
                _tmp.row_group = Convert.ToInt32(HPAFor3Group.RESULT_ON_SWAGE);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                list.Add(_tmp);

                _tmp = new template_wd_hpa_for3_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = 4;
                _tmp.A = Constants.GetEnumDescription(HPAFor3Group.AVERAGE_RESULT);
                _tmp.B = String.Empty;
                _tmp.C = String.Empty;
                _tmp.D = String.Empty;
                _tmp.E = String.Empty;
                _tmp.row_group = Convert.ToInt32(HPAFor3Group.AVERAGE_RESULT);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                list.Add(_tmp);

                _tmp = new template_wd_hpa_for3_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = 5;
                _tmp.A = Constants.GetEnumDescription(HPAFor3Group.SPECIFICATION);
                _tmp.B = (detailSpec.D.Equals("NA") ? "NA" : (detailSpec.D.Equals("0") ? "0" : String.Format("<{0}", detailSpec.D)));
                _tmp.C = (detailSpec.E.Equals("NA") ? "NA" : (detailSpec.E.Equals("0") ? "0" : String.Format("<{0}", detailSpec.E)));
                _tmp.D = (detailSpec.F.Equals("NA") ? "NA" : (detailSpec.F.Equals("0") ? "0" : String.Format("<{0}", detailSpec.F)));
                _tmp.E = (detailSpec.G.Equals("NA") ? "NA" : (detailSpec.G.Equals("0") ? "0" : String.Format("<{0}", detailSpec.G)));
                _tmp.row_group = Convert.ToInt32(HPAFor3Group.SPECIFICATION);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                list.Add(_tmp);

                _tmp = new template_wd_hpa_for3_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = 6;
                _tmp.A = Constants.GetEnumDescription(HPAFor3Group.PASS_SLASH_FAIL);
                _tmp.B = String.Empty;
                _tmp.C = String.Empty;
                _tmp.D = String.Empty;
                _tmp.E = String.Empty;
                _tmp.row_group = Convert.ToInt32(HPAFor3Group.PASS_SLASH_FAIL);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                list.Add(_tmp);

                int seq = 7;
                #region "ARM"
                foreach (DictionaryEntry entry in GetHPAData())
                {
                    seq++;
                    String[] values = (String[])entry.Value;
                    if (values != null && values.Length > 0)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            _tmp = new template_wd_hpa_for3_coverpage();
                            _tmp.ID = CustomUtils.GetRandomNumberID();
                            _tmp.seq = seq;
                            _tmp.A = entry.Key.ToString();
                            _tmp.B = values[i];
                            _tmp.C = String.Empty;
                            _tmp.D = String.Empty;
                            _tmp.E = String.Empty;
                            _tmp.row_group = Convert.ToInt32(HPAFor3Group.RAWDATA_ARM);
                            _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            list.Add(_tmp);
                            seq++;
                        }
                    }
                    seq++;
                    _tmp = new template_wd_hpa_for3_coverpage();
                    _tmp.ID = CustomUtils.GetRandomNumberID();
                    _tmp.seq = seq;
                    _tmp.A = entry.Key.ToString();
                    _tmp.B = entry.Key.ToString();
                    _tmp.C = String.Empty;
                    _tmp.D = String.Empty;
                    _tmp.E = String.Empty;
                    _tmp.row_group = Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL);
                    _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                    list.Add(_tmp);
                    seq++;
                }
                _tmp = new template_wd_hpa_for3_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = seq;
                _tmp.A = String.Empty;
                _tmp.B = String.Empty;
                _tmp.C = String.Empty;
                _tmp.D = String.Empty;
                _tmp.E = String.Empty;
                _tmp.row_group = Convert.ToInt32(HPAFor3Group.ARM_GRAND_TOTAL);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                list.Add(_tmp);
                seq++;
                #endregion
                #region "PIVOT"
                foreach (DictionaryEntry entry in GetHPAData())
                {
                    seq++;
                    String[] values = (String[])entry.Value;
                    if (values != null && values.Length > 0)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            _tmp = new template_wd_hpa_for3_coverpage();
                            _tmp.ID = CustomUtils.GetRandomNumberID();
                            _tmp.seq = seq;
                            _tmp.A = entry.Key.ToString();
                            _tmp.B = values[i];
                            _tmp.C = String.Empty;
                            _tmp.D = String.Empty;
                            _tmp.E = String.Empty;
                            _tmp.row_group = Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT);
                            _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            list.Add(_tmp);
                            seq++;
                        }
                    }
                    seq++;
                    _tmp = new template_wd_hpa_for3_coverpage();
                    _tmp.ID = CustomUtils.GetRandomNumberID();
                    _tmp.seq = seq;
                    _tmp.A = entry.Key.ToString();
                    _tmp.B = entry.Key.ToString();
                    _tmp.C = String.Empty;
                    _tmp.D = String.Empty;
                    _tmp.E = String.Empty;
                    _tmp.row_group = Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL);
                    _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                    list.Add(_tmp);
                    seq++;
                }
                _tmp = new template_wd_hpa_for3_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = seq;
                _tmp.A = String.Empty;
                _tmp.B = String.Empty;
                _tmp.C = String.Empty;
                _tmp.D = String.Empty;
                _tmp.E = String.Empty;
                _tmp.row_group = Convert.ToInt32(HPAFor3Group.PIVOT_GRAND_TOTAL);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                list.Add(_tmp);
                seq++;
                #endregion
                #region "SWAGE"
                foreach (DictionaryEntry entry in GetHPAData())
                {
                    seq++;
                    String[] values = (String[])entry.Value;
                    if (values != null && values.Length > 0)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            _tmp = new template_wd_hpa_for3_coverpage();
                            _tmp.ID = CustomUtils.GetRandomNumberID();
                            _tmp.seq = seq;
                            _tmp.A = entry.Key.ToString();
                            _tmp.B = values[i];
                            _tmp.C = String.Empty;
                            _tmp.D = String.Empty;
                            _tmp.E = String.Empty;
                            _tmp.row_group = Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE);
                            _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                            list.Add(_tmp);
                            seq++;
                        }
                    }
                    seq++;
                    _tmp = new template_wd_hpa_for3_coverpage();
                    _tmp.ID = CustomUtils.GetRandomNumberID();
                    _tmp.seq = seq;
                    _tmp.A = entry.Key.ToString();
                    _tmp.B = entry.Key.ToString();
                    _tmp.C = String.Empty;
                    _tmp.D = String.Empty;
                    _tmp.E = String.Empty;
                    _tmp.row_group = Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL);
                    _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                    list.Add(_tmp);
                    seq++;
                }
                _tmp = new template_wd_hpa_for3_coverpage();
                _tmp.ID = CustomUtils.GetRandomNumberID();
                _tmp.seq = seq;
                _tmp.A = String.Empty;
                _tmp.B = String.Empty;
                _tmp.C = String.Empty;
                _tmp.D = String.Empty;
                _tmp.E = String.Empty;
                _tmp.row_group = Convert.ToInt32(HPAFor3Group.SWAGE_GRAND_TOTAL);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                list.Add(_tmp);
                seq++;
                #endregion


                this.HpaFor3 = list;

                gvResult.DataSource = list.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_ARM) ||
                                                    x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_PIVOT) ||
                                                    x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_SWAGE) ||
                                                    x.row_group == Convert.ToInt32(HPAFor3Group.AVERAGE_RESULT) ||
                                                    x.row_group == Convert.ToInt32(HPAFor3Group.SPECIFICATION) ||
                                                    x.row_group == Convert.ToInt32(HPAFor3Group.PASS_SLASH_FAIL)
                    );
                gvResult.DataBind();


                gvARM.DataSource = list.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) ||
                                                    x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) ||
                                                    x.row_group == Convert.ToInt32(HPAFor3Group.ARM_GRAND_TOTAL));
                gvARM.DataBind();

                gvPivot.DataSource = list.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) ||
                                    x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) ||
                                    x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_GRAND_TOTAL));
                gvPivot.DataBind();

                gvSwage.DataSource = list.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) ||
                        x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) ||
                        x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_GRAND_TOTAL));
                gvSwage.DataBind();

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

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            //DataTable dt = Extenders.ObjectToDataTable(this.HpaFor3[0]);
            DataTable dtHeader = new DataTable("MethodProcedure");

            // Define all the columns once.
            DataColumn[] cols ={ new DataColumn("ParticleAnalysisBySEMEDX",typeof(String)),
                                  new DataColumn("TapedAreaForDriveParts",typeof(String)),
                                  new DataColumn("NoofTimesTaped",typeof(String)),
                                  new DataColumn("SurfaceAreaAnalysed",typeof(String)),
                                  new DataColumn("ParticleRanges",typeof(String)),
                              };
            dtHeader.Columns.AddRange(cols);
            DataRow row = dtHeader.NewRow();
            row["ParticleAnalysisBySEMEDX"] = txtA23.Text;
            row["TapedAreaForDriveParts"] = txtB23.Text;
            row["NoofTimesTaped"] = txtC23.Text;
            row["SurfaceAreaAnalysed"] = txtD23.Text;
            row["ParticleRanges"] = txtE23.Text;
            dtHeader.Rows.Add(row);
            row = dtHeader.NewRow();
            row["ParticleAnalysisBySEMEDX"] = txtA24.Text;
            row["TapedAreaForDriveParts"] = txtB24.Text;
            row["NoofTimesTaped"] = txtC24.Text;
            row["SurfaceAreaAnalysed"] = txtD24.Text;
            row["ParticleRanges"] = txtE24.Text;
            dtHeader.Rows.Add(row);
            row = dtHeader.NewRow();
            row["ParticleAnalysisBySEMEDX"] = txtA25.Text;
            row["TapedAreaForDriveParts"] = txtB25.Text;
            row["NoofTimesTaped"] = txtC25.Text;
            row["SurfaceAreaAnalysed"] = txtD25.Text;
            row["ParticleRanges"] = txtE25.Text;
            dtHeader.Rows.Add(row);

            List<template_wd_hpa_for3_coverpage> specs = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_ARM) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_PIVOT) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_SWAGE) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.AVERAGE_RESULT) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.SPECIFICATION) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.PASS_SLASH_FAIL)).OrderBy(x => x.seq).ToList();

            List<template_wd_hpa_for3_coverpage> arms = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) ||
                                            x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) ||
                                            x.row_group == Convert.ToInt32(HPAFor3Group.ARM_GRAND_TOTAL)).OrderBy(x => x.seq).ToList();

            List<template_wd_hpa_for3_coverpage> pivots = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) ||
                            x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) ||
                            x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_GRAND_TOTAL)).OrderBy(x => x.seq).ToList();

            List<template_wd_hpa_for3_coverpage> swage = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) ||
                x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) ||
                x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_GRAND_TOTAL)).OrderBy(x => x.seq).ToList();

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
            reportParameters.Add(new ReportParameter("Test", "-"));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on WD's specification Doc No  {0} for {1}", lbDocNo.Text, lbComponent.Text)));
            reportParameters.Add(new ReportParameter("img01Url", Configurations.HOST + "" + this.HpaFor3[0].img_path));
            reportParameters.Add(new ReportParameter("img02Url", Configurations.HOST + "" + this.HpaFor3[0].img_path1));
            reportParameters.Add(new ReportParameter("img03Url", Configurations.HOST + "" + this.HpaFor3[0].img_path2));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/hpa_for_3_wd.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtHeader)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", specs.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", arms.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", pivots.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", swage.ToDataTable())); // Add datasource here





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

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_wd_hpa_for3_coverpage gcms = this.HpaFor3.Find(x => x.ID == PKID);
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
                    gvResult.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_ARM) ||
                                    x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_PIVOT) ||
                                    x.row_group == Convert.ToInt32(HPAFor3Group.RESULT_ON_SWAGE) ||
                                    x.row_group == Convert.ToInt32(HPAFor3Group.AVERAGE_RESULT) ||
                                    x.row_group == Convert.ToInt32(HPAFor3Group.SPECIFICATION) ||
                                    x.row_group == Convert.ToInt32(HPAFor3Group.PASS_SLASH_FAIL)).OrderBy(x => x.seq);
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

        protected void gvARM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvARM.DataKeys[e.Row.RowIndex].Values[0].ToString());
                int _row_group = Convert.ToInt32(gvARM.DataKeys[e.Row.RowIndex].Values[2].ToString());

                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvARM.DataKeys[e.Row.RowIndex].Values[1]);
                //LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                //LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                Literal _litB = (Literal)e.Row.FindControl("litB");
                Literal _litC = (Literal)e.Row.FindControl("litC");
                Literal _litD = (Literal)e.Row.FindControl("litD");
                Literal _litE = (Literal)e.Row.FindControl("litE");

                //if (_btnHide != null && _btnUndo != null)
                //{
                //    switch (cmd)
                //    {
                //        case RowTypeEnum.Hide:
                //            _btnHide.Visible = false;
                //            _btnUndo.Visible = true;
                //            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                //            break;
                //        default:
                //            _btnHide.Visible = true;
                //            _btnUndo.Visible = false;
                //            e.Row.ForeColor = System.Drawing.Color.Black;
                //            break;
                //    }
                if (_litB != null)
                {
                    HPAFor3Group group = (HPAFor3Group)Enum.ToObject(typeof(HPAFor3Group), _row_group);
                    switch (group)
                    {
                        case HPAFor3Group.ARM_SUB_TOTAL:
                        case HPAFor3Group.PIVOT_SUB_TOTAL:
                        case HPAFor3Group.SWAGE_SUB_TOTAL:
                            _litB.Text = String.Format("Subtotal - {0}", _litB.Text);
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            break;
                        case HPAFor3Group.ARM_GRAND_TOTAL:
                        case HPAFor3Group.PIVOT_GRAND_TOTAL:
                        case HPAFor3Group.SWAGE_GRAND_TOTAL:
                            _litB.Text = String.Format("Grand Total of Particles");
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            break;
                    }
                }
                //}
            }
        }

        protected void gvARM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            //if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            //{
            //    int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            //    template_wd_hpa_for3_coverpage gcms = this.HpaFor3.Find(x => x.ID == PKID);
            //    if (gcms != null)
            //    {
            //        switch (cmd)
            //        {
            //            case RowTypeEnum.Hide:
            //                gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);

            //                break;
            //            case RowTypeEnum.Normal:
            //                gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
            //                break;
            //        }

            //        gvARM.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM)).OrderBy(x => x.seq).ToList();
            //        gvARM.DataBind();
            //    }
            //}
        }

        protected void gvPivot_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            //if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            //{
            //    int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            //    template_wd_hpa_for3_coverpage gcms = this.HpaFor3.Find(x => x.ID == PKID);
            //    if (gcms != null)
            //    {
            //        switch (cmd)
            //        {
            //            case RowTypeEnum.Hide:
            //                gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);

            //                break;
            //            case RowTypeEnum.Normal:
            //                gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
            //                break;
            //        }

            //        gvARM.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT)).OrderBy(x => x.seq).ToList();
            //        gvARM.DataBind();
            //    }
            //}
        }

        protected void gvPivot_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvPivot.DataKeys[e.Row.RowIndex].Values[0].ToString());
                int _row_group = Convert.ToInt32(gvPivot.DataKeys[e.Row.RowIndex].Values[2].ToString());

                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvPivot.DataKeys[e.Row.RowIndex].Values[1]);
                //LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                //LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                Literal _litB = (Literal)e.Row.FindControl("litB");
                Literal _litC = (Literal)e.Row.FindControl("litC");
                Literal _litD = (Literal)e.Row.FindControl("litD");
                Literal _litE = (Literal)e.Row.FindControl("litE");

                //if (_btnHide != null && _btnUndo != null)
                //{
                //    switch (cmd)
                //    {
                //        case RowTypeEnum.Hide:
                //            _btnHide.Visible = false;
                //            _btnUndo.Visible = true;
                //            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                //            break;
                //        default:
                //            _btnHide.Visible = true;
                //            _btnUndo.Visible = false;
                //            e.Row.ForeColor = System.Drawing.Color.Black;
                //            break;
                //    }
                if (_litB != null)
                {
                    HPAFor3Group group = (HPAFor3Group)Enum.ToObject(typeof(HPAFor3Group), _row_group);
                    switch (group)
                    {
                        case HPAFor3Group.ARM_SUB_TOTAL:
                        case HPAFor3Group.PIVOT_SUB_TOTAL:
                        case HPAFor3Group.SWAGE_SUB_TOTAL:
                            _litB.Text = String.Format("Subtotal - {0}", _litB.Text);
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            break;
                        case HPAFor3Group.ARM_GRAND_TOTAL:
                        case HPAFor3Group.PIVOT_GRAND_TOTAL:
                        case HPAFor3Group.SWAGE_GRAND_TOTAL:
                            _litB.Text = String.Format("Grand Total of {0}", _litB.Text);
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            break;
                    }
                }
                //}
            }
        }

        protected void gvSwage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            //if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            //{
            //    int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            //    template_wd_hpa_for3_coverpage gcms = this.HpaFor3.Find(x => x.ID == PKID);
            //    if (gcms != null)
            //    {
            //        switch (cmd)
            //        {
            //            case RowTypeEnum.Hide:
            //                gcms.row_type = Convert.ToInt32(RowTypeEnum.Hide);

            //                break;
            //            case RowTypeEnum.Normal:
            //                gcms.row_type = Convert.ToInt32(RowTypeEnum.Normal);
            //                break;
            //        }

            //        gvARM.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE)).OrderBy(x => x.seq).ToList();
            //        gvARM.DataBind();
            //    }
            //}
        }

        protected void gvSwage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvSwage.DataKeys[e.Row.RowIndex].Values[0].ToString());
                int _row_group = Convert.ToInt32(gvSwage.DataKeys[e.Row.RowIndex].Values[2].ToString());

                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvSwage.DataKeys[e.Row.RowIndex].Values[1]);
                //LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                //LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                Literal _litB = (Literal)e.Row.FindControl("litB");
                Literal _litC = (Literal)e.Row.FindControl("litC");
                Literal _litD = (Literal)e.Row.FindControl("litD");
                Literal _litE = (Literal)e.Row.FindControl("litE");

                //if (_btnHide != null && _btnUndo != null)
                //{
                //    switch (cmd)
                //    {
                //        case RowTypeEnum.Hide:
                //            _btnHide.Visible = false;
                //            _btnUndo.Visible = true;
                //            e.Row.ForeColor = System.Drawing.Color.WhiteSmoke;
                //            break;
                //        default:
                //            _btnHide.Visible = true;
                //            _btnUndo.Visible = false;
                //            e.Row.ForeColor = System.Drawing.Color.Black;
                //            break;
                //    }
                if (_litB != null)
                {
                    HPAFor3Group group = (HPAFor3Group)Enum.ToObject(typeof(HPAFor3Group), _row_group);
                    switch (group)
                    {
                        case HPAFor3Group.ARM_SUB_TOTAL:
                        case HPAFor3Group.PIVOT_SUB_TOTAL:
                        case HPAFor3Group.SWAGE_SUB_TOTAL:
                            _litB.Text = String.Format("Subtotal - {0}", _litB.Text);
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            break;
                        case HPAFor3Group.ARM_GRAND_TOTAL:
                        case HPAFor3Group.PIVOT_GRAND_TOTAL:
                        case HPAFor3Group.SWAGE_GRAND_TOTAL:
                            _litB.Text = String.Format("Grand Total of {0}", _litB.Text);
                            e.Row.ForeColor = System.Drawing.Color.Blue;
                            break;
                    }
                }
                //}
            }
        }

        protected void gvARM_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvARM_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvARM.EditIndex = e.NewEditIndex;
            gvARM.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) ||
                                                x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) ||
                                                x.row_group == Convert.ToInt32(HPAFor3Group.ARM_GRAND_TOTAL)).OrderBy(x => x.seq);
            gvARM.DataBind();
        }

        protected void gvARM_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvARM.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txtC = (TextBox)gvARM.Rows[e.RowIndex].FindControl("txtC");


            template_wd_hpa_for3_coverpage _cov = this.HpaFor3.Find(x => x.ID == _id);
            if (_cov != null)
            {
                _cov.C = txtC.Text;
            }
            gvARM.EditIndex = -1;
            CalculateCas();
        }

        protected void gvPivot_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gvPivot_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPivot.EditIndex = e.NewEditIndex;
            gvPivot.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) ||
                                x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_GRAND_TOTAL)).OrderBy(x => x.seq);
            gvPivot.DataBind();
        }
        protected void gvPivot_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvPivot.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txtC = (TextBox)gvPivot.Rows[e.RowIndex].FindControl("txtC");


            template_wd_hpa_for3_coverpage _cov = this.HpaFor3.Find(x => x.ID == _id);
            if (_cov != null)
            {
                _cov.C = txtC.Text;
            }
            gvPivot.EditIndex = -1;
            CalculateCas();
        }

        protected void gvSwage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvSwage_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSwage.EditIndex = e.NewEditIndex;
            gvSwage.DataSource = this.HpaFor3.OrderBy(x => x.seq).ToList();
            gvSwage.DataBind();
        }

        protected void gvSwage_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvSwage.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txtC = (TextBox)gvSwage.Rows[e.RowIndex].FindControl("txtC");


            template_wd_hpa_for3_coverpage _cov = this.HpaFor3.Find(x => x.ID == _id);
            if (_cov != null)
            {
                _cov.C = txtC.Text;
            }
            gvSwage.EditIndex = -1;
            CalculateCas();
        }

        protected void gvARM_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvARM.EditIndex = -1;
            gvARM.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_ARM) ||
                                                x.row_group == Convert.ToInt32(HPAFor3Group.ARM_SUB_TOTAL) ||
                                                x.row_group == Convert.ToInt32(HPAFor3Group.ARM_GRAND_TOTAL)).OrderBy(x => x.seq);
            gvARM.DataBind();
        }

        protected void gvPivot_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPivot.EditIndex = -1;
            gvPivot.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_PIVOT) ||
                    x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_SUB_TOTAL) ||
                    x.row_group == Convert.ToInt32(HPAFor3Group.PIVOT_GRAND_TOTAL)).OrderBy(x => x.seq);
            gvPivot.DataBind();
        }

        protected void gvSwage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSwage.EditIndex = -1;
            gvSwage.DataSource = this.HpaFor3.Where(x => x.row_group == Convert.ToInt32(HPAFor3Group.RAWDATA_SWAGE) ||
        x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_SUB_TOTAL) ||
        x.row_group == Convert.ToInt32(HPAFor3Group.SWAGE_GRAND_TOTAL)).OrderBy(x => x.seq);
            gvSwage.DataBind();
        }



    }
}