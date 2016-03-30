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
    public partial class WD_HPA_FOR_1 : System.Web.UI.UserControl
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_HPA_FOR_1));

        #region "Property"

        //private String[] GetHPAHeader = { "Total Hard Particles", "Total MgSiO Particles", "Total Steel Particles", "Total Magnetic Particles", "Other Particle" };
        List<String> errors = new List<string>();



        private String[] ANameKey = {
            "Total Hard Particles",
            "Total MgSiO Particles",
            "Total Steel Particles",
            "Total Magnetic Particles"
        };

        //private Hashtable GetHPAData()
        //{
        //    Hashtable hashtable = new Hashtable();
        //    hashtable.Add("Hard Particles",
        //        new String[]{
        //        "Al-O",
        //        "Al-Si-O",
        //        "Si-O",
        //        "Si-C",
        //        "Al-Cu-O",
        //        "Al-Mg-O",
        //        "Al-Si-Cu-O",
        //        "Al-Si-Fe-O",
        //        "Al-Si-Mg-O",
        //        "Al-Ti-O",
        //        "Ti-O",
        //        "Ti-C",
        //        "Ti-B",
        //        "Ti-N",
        //        "W-O",
        //        "W-C",
        //        "Zr-O",
        //        "Zr-C",
        //        "Pb-Zr-Ti-O (PZT)",
        //        });
        //    hashtable.Add("MgSiO Particles",new String[]{});
        //    hashtable.Add("Steel Particles",new String[]{});
        //    hashtable.Add("Magnetic Particles",
        //        new String[]{
        //        "Ce-Co",
        //        "Fe-Nd",
        //        "Fe-Sr",
        //        "Fe-Sm",
        //        "Nd-Pr",
        //        "Ni-Co",
        //        "Sm-Co",
        //        });
        //    return hashtable;
        //}

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

                //img1.ImageUrl = Configurations.HOST + "" + _cover.img_path;

                gvResult.DataSource = this.HpaFor1.Where(x => x.parent == -1).OrderBy(x => x.seq);
                gvResult.DataBind();

                //gvResult_1.DataSource = this.HpaFor1.OrderBy(x => x.seq);
                //gvResult_1.DataBind();


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
                    }
                    objWork.DeleteBySampleID(this.SampleID);

                    //switch (this.CommandName)
                    //{
                    //    case CommandNameEnum.Add:
                            objWork.InsertList(this.HpaFor1);
                    //        break;
                    //    case CommandNameEnum.Edit:
                    //        objWork.UpdateList(this.HpaFor1);
                    //        break;
                    //}
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
            List<template_wd_hpa_for1_coverpage> lists = this.HpaFor1.Where(x => x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)).OrderBy(x => x.seq).ToList();

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

                                        string subfix = Path.GetFileNameWithoutExtension(source_file);

                                        switch (subfix.Substring(subfix.Length - 1))
                                        {
                                            case "B":
                                                #region "HPA(B)"
                                                foreach (template_wd_hpa_for1_coverpage _cov in lists)
                                                {
                                                    int subIndex = data[0].IndexOf('(') == -1 ? data[0].ToUpper().Replace(" ", String.Empty).Length : data[0].ToUpper().Replace(" ", String.Empty).IndexOf('(');

                                                    if (mappingRawData(_cov.B).ToUpper().Replace(" ", String.Empty).Equals(data[0].ToUpper().Replace(" ", String.Empty).Substring(0, subIndex)))
                                                    {
                                                        template_wd_hpa_for1_coverpage _hpa = this.HpaFor1.Where(x => x.ID == _cov.ID).FirstOrDefault();
                                                        if (_hpa != null)
                                                        {
                                                            _hpa.C = (_hpa.C == null) ? 0 : _hpa.C + Convert.ToInt32(data[2]);
                                                        }
                                                    }
                                                }
                                                Console.WriteLine("");
                                                #endregion
                                                break;
                                            //case "S":
                                            //    #region "HPA(S)"
                                            //    foreach (template_wd_hpa_for1_coverpage _cov in lists)
                                            //    {
                                            //        if (_cov.B.Equals(data[0]))
                                            //        {
                                            //            template_wd_hpa_for1_coverpage _hpa = this.HpaFor1.Where(x => x.ID == _cov.ID).FirstOrDefault();
                                            //            if (_hpa != null)
                                            //            {
                                            //                _hpa.RawCounts = Convert.ToInt32(data[2]);
                                            //            }
                                            //        }
                                            //    }

                                            //    Console.WriteLine("");
                                            //    #endregion
                                            //    break;
                                        }
                                        index++;
                                    }
                                }
                                //using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                                //{
                                //    HSSFWorkbook wd = new HSSFWorkbook(fs);
                                //    ISheet sheet = wd.GetSheet("Raw Data-Arm");
                                //    if (sheet == null)
                                //    {
                                //        MessageBox.Show(this.Page, String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", sheet.SheetName));
                                //    }
                                //    else
                                //    {
                                //        if (sheet != null)
                                //        {

                                //            foreach (template_wd_hpa_for1_coverpage _cov in lists)
                                //            {
                                //                int rc = 0;
                                //                for (int c = 0; c < 100; c++)
                                //                {
                                //                    String typesOfParticles = CustomUtils.GetCellValue(sheet.GetRow(0).GetCell(c));
                                //                    if (_cov.B.Equals(typesOfParticles))
                                //                    {
                                //                        for (int row = 1; row <= sheet.LastRowNum; row++)
                                //                        {
                                //                            String rank = CustomUtils.GetCellValue(sheet.GetRow(row).GetCell(3));
                                //                            String value = CustomUtils.GetCellValue(sheet.GetRow(row).GetCell(c));
                                //                            if (!rank.Equals("Rejected (ED)") && value.Equals("1"))
                                //                            {
                                //                                rc++;
                                //                            }
                                //                        }
                                //                        break;
                                //                    }
                                //                }
                                //                template_wd_hpa_for1_coverpage _hpa = this.HpaFor1.Where(x => x.ID == _cov.ID).FirstOrDefault();
                                //                if (_hpa != null)
                                //                {
                                //                    _hpa.C = rc;
                                //                }
                                //            }
                                //            Console.WriteLine("");

                                //        }
                                //    }
                                //}
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
            CalculateCas();
            btnSubmit.Enabled = true;
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
            List<template_wd_hpa_for1_coverpage> lists = this.HpaFor1.Where(x => x.parent == 0).OrderBy(x => x.seq).ToList();
            foreach (template_wd_hpa_for1_coverpage _val in lists)
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

            lists = this.HpaFor1.Where(x => x.parent == -1).OrderBy(x => x.seq).ToList();
            foreach (template_wd_hpa_for1_coverpage _val in lists)
            {
                //=IF(D28="NA","NA",IF(C28>=INDEX('Detail Spec'!$A$3:$G$238,$F$1,4),"FAIL","PASS"))
                template_wd_hpa_for1_coverpage tmp = this.HpaFor1.Where(x => x.ID == _val.ID).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.C = Convert.ToInt32(this.HpaFor1.Where(x => x.A.StartsWith(_val.A) && !String.IsNullOrEmpty(x.D) && x.parent == 0).Sum(x => Convert.ToInt32(x.D)));
                    if (tmp.D != null)
                    {
                        tmp.E = tmp.D.Equals("NA") ? "NA" : (tmp.C >= Convert.ToInt32(tmp.D)) ? "FAIL" : "PASS";
                    }
                }
            }

            lists = this.HpaFor1.Where(x => x.parent == -2).OrderBy(x => x.seq).ToList();
            foreach (template_wd_hpa_for1_coverpage _val in lists)
            {
                template_wd_hpa_for1_coverpage tmp = this.HpaFor1.Where(x => x.ID == _val.ID).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.C = Convert.ToInt32(this.HpaFor1.Where(x => x.A.StartsWith(_val.A) && x.parent == 0).Sum(x => Convert.ToInt32(x.C)));
                    tmp.D = Convert.ToInt32(this.HpaFor1.Where(x => x.A.StartsWith(_val.A) && !String.IsNullOrEmpty(x.D) && x.parent == 0).Sum(x => Convert.ToInt32(x.D))).ToString();
                }
            }

            gvResult.DataSource = this.HpaFor1.Where(x => x.parent == -1).OrderBy(x => x.seq).ToList();
            gvResult.DataBind();

            gvResult_1.DataSource = this.HpaFor1.OrderBy(x => x.seq).ToList();
            gvResult_1.DataBind();

            btnSubmit.Enabled = true;
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
            tb_m_detail_spec detailSpec = new tb_m_detail_spec().SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (detailSpec != null)
            {
                lbDocNo.Text = detailSpec.B;
                lbComponent.Text = detailSpec.A;
                List<template_wd_hpa_for1_coverpage> _list = new List<template_wd_hpa_for1_coverpage>();

                int seq = 1;
                #region "Hard Particle Analysis"
                for (int i = 0; i < ANameKey.Length; i++)
                {
                    String _val = ANameKey[i];
                    template_wd_hpa_for1_coverpage _tmp = new template_wd_hpa_for1_coverpage();
                    _tmp.ID = CustomUtils.GetRandomNumberID();
                    _tmp.seq = (i + 1);
                    _tmp.A = _val;
                    switch (i)
                    {
                        case 0:
                            _tmp.D = detailSpec.D;
                            break;  //"Hard Particles",
                        case 1:
                            _tmp.D = detailSpec.E;
                            break;  //"Magnetic Particles",
                        case 2:
                            _tmp.D = detailSpec.F;
                            break;  //"SST Particles",
                        case 3:
                            _tmp.D = detailSpec.G;
                            break;  //"MgSiO Particles",
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
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_ITEM)||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_TOTAL) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_SUB_TOTAL) ||
                x.hpa_type == Convert.ToInt32(GVTypeEnum.CLASSIFICATION_GRAND_TOTAL));
                gvResult_1.DataBind();

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


            DataTable dt = Extenders.ObjectToDataTable(this.HpaFor1[0]);
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
            reportParameters.Add(new ReportParameter("Test", "-"));
            reportParameters.Add(new ReportParameter("ResultDesc", String.Format("The Specification is based on WD's specification Doc No  {0} for {1}", lbDocNo.Text, lbComponent.Text)));
            reportParameters.Add(new ReportParameter("img01Url", Configurations.HOST + "" + this.HpaFor1[0].img_path));
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
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", this.HpaFor1.Where(x => x.parent == -1).OrderBy(x => x.seq).ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", this.HpaFor1.Where(x => x.parent != -1).OrderBy(x => x.seq).ToDataTable())); // Add datasource here






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
                    gvResult.DataSource = this.HpaFor1.Where(x => x.parent == -1).OrderBy(x => x.seq).ToList();
                    gvResult.DataBind();
                }
            }
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int PKID = Convert.ToInt32(gvResult.DataKeys[e.Row.RowIndex].Values[0].ToString());
                //RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvResult.DataKeys[e.Row.RowIndex].Values[1]);
                //LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                //LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                //HiddenField _hParent = (HiddenField)e.Row.FindControl("hParent");
                //Literal _litB = (Literal)e.Row.FindControl("litB");
                //Literal _litTest = (Literal)e.Row.FindControl("litTest");

                //if (e.Row.RowIndex == 0)
                //{
                //    _litTest.Text = "Particle Analysis by SEM EDX";
                //}
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
                //    if (_litB != null)
                //    {
                //        int parent = Convert.ToInt32(_hParent.Value);
                //        switch (parent)
                //        {
                //            case -1://Total
                //                _litB.Text = String.Format("Total {0}", _litB.Text);
                //                break;
                //        }
                //    }
                //}
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
                Literal _litB = (Literal)e.Row.FindControl("litB");
                if (gvResult_1.DataKeys[e.Row.RowIndex].Values[2] != null)
                {

                    GVTypeEnum cmd = (GVTypeEnum)Enum.ToObject(typeof(GVTypeEnum), (int)gvResult_1.DataKeys[e.Row.RowIndex].Values[2]);
                    switch (cmd)
                    {
                        case GVTypeEnum.CLASSIFICATION_GRAND_TOTAL:
                        case GVTypeEnum.CLASSIFICATION_TOTAL:
                            e.Row.BackColor = System.Drawing.Color.Orange;
                            break;
                        case GVTypeEnum.CLASSIFICATION_SUB_TOTAL:
                            e.Row.BackColor = System.Drawing.Color.Yellow;
                            break;
                        case GVTypeEnum.CLASSIFICATION_ITEM:
                            _litB.Text = String.Format("{0}".PadRight(20, ' '), _litB.Text);
                            break;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int PKID = Convert.ToInt32(gvResult_1.DataKeys[e.Row.RowIndex].Values[0].ToString());
                //RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvResult_1.DataKeys[e.Row.RowIndex].Values[1]);
                ////LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                ////LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                //HiddenField _hParent = (HiddenField)e.Row.FindControl("hParent");
                //Literal _litB = (Literal)e.Row.FindControl("litB");
                //Literal _litC = (Literal)e.Row.FindControl("litC");
                //Literal _litD = (Literal)e.Row.FindControl("litD");
                //Literal _litE = (Literal)e.Row.FindControl("litE");

                //if (_litB != null)
                //{
                //    int parent = Convert.ToInt32(_hParent.Value);
                //    switch (parent)
                //    {
                //        case -1://Total
                //            _litB.Text = String.Format("{0}", _litB.Text);
                //            _litC.Text = String.Empty;
                //            _litD.Text = String.Empty;
                //            _litE.Text = String.Empty;
                //            e.Row.ForeColor = System.Drawing.Color.Blue;
                //            break;
                //        case -2://Sub Total
                //            _litB.Text = String.Format("Subtotal - {0}", _litB.Text);
                //            e.Row.ForeColor = System.Drawing.Color.Blue;
                //            break;
                //    }
                //}
                
            }
        }



        #region "DHS GRID."
        protected void gvResult_1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvResult_1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvResult_1.EditIndex = e.NewEditIndex;
            gvResult_1.DataSource = this.HpaFor1.OrderBy(x => x.seq).ToList();
            gvResult_1.DataBind();
        }

        protected void gvResult_1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvResult_1.EditIndex = -1;
            gvResult_1.DataSource = this.HpaFor1.OrderBy(x => x.seq).ToList();
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
            items.Add("Ce-Co");
            items.Add("Fe-Nd");
            items.Add("Fe-Sr");
            items.Add("Fe-Sm");
            items.Add("Nd-Pr");
            items.Add("Ni-Co");
            items.Add("Sm-Co");
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
            //mappingValues["SST300s with possible Si"] = "SST300s (Fe/Cr/Ni)";
            //mappingValues["SST300s with possible Si and Mn"] = "SST400s (Fe/Cr)";
            //mappingValues["SST400s with possible Si"] = "SST400s (Fe/Cr)";


            //SST400s(Fe / Cr)



            foreach (DictionaryEntry entry in mappingValues)
            {
                if (entry.Key.Equals(_val))
                {
                    result = entry.Value.ToString();
                    break;
                }
            }
            return result;
        }


    }
}