﻿using ALS.ALSI.Biz;
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
using System.Text;
using ALSALSI.Biz;
using Spire.Doc;

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
            get
            {
                List<template_wd_lpc_coverpage> tmps = (List<template_wd_lpc_coverpage>)Session[GetType().Name + "Lpc"];
                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);
                return (userRole == RoleEnum.CHEMIST) ? tmps : tmps.Where(x => x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();

                //return (List<template_wd_lpc_coverpage>)Session[GetType().Name + "Lpc"];
            }
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

            tb_unit unit = new tb_unit();
            ddlUnit.Items.Clear();
            ddlUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("LPC")).ToList();
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnit2.Items.Clear();
            ddlUnit2.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("LPC")).ToList();
            ddlUnit2.DataBind();
            ddlUnit2.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnit3.Items.Clear();
            ddlUnit3.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("LPC")).ToList();
            ddlUnit3.DataBind();
            ddlUnit3.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlUnit4.Items.Clear();
            ddlUnit4.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("LPC")).ToList();
            ddlUnit4.DataBind();
            ddlUnit4.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            m_type_of_test typeOfTest = new m_type_of_test();
            typeOfTest = typeOfTest.SelectByID(jobSample.type_of_test_id);

            RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);

            #region "SAMPLE"
            if (this.jobSample != null)
            {

                StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
                lbJobStatus.Text = Constants.GetEnumDescription(status);
                ddlStatus.Items.Clear();

                pRemark.Visible = false;
                pDisapprove.Visible = false;
                pCoverPage.Visible = true;
                pDSH.Visible = false;
                pTankConditions.Visible = false;
                pSpecification.Visible = (status == StatusEnum.LOGIN_SELECT_SPEC);
                pStatus.Visible = (status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.LABMANAGER_CHECKING);
                pUploadfile.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD);
                pDownload.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);
                btnSubmit.Visible = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);
                btnCoverPage.Visible = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));
                btnWorkSheet.Visible = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));
                txtB21.Enabled = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));
                txtC21.Enabled = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));
                txtD21.Enabled = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));
                txtE21.Enabled = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));
                gvSpec.Columns[5].Visible = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));
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

                txtDateAnalyzed.Text = (this.jobSample.date_chemist_alalyze != null) ? this.jobSample.date_chemist_alalyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = userRole == RoleEnum.CHEMIST;

                //#region ":: STAMP ANALYZED DATE ::"
                //if (userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
                //{
                //    if (this.jobSample.date_chemist_alalyze == null)
                //    {
                //        this.jobSample.date_chemist_alalyze = DateTime.Now;
                //        this.jobSample.Update();
                //    }
                //}
                //#endregion
            }
            #endregion

            #region "WORKSHEET"
            this.Lpc = template_wd_lpc_coverpage.FindAllBySampleID(this.SampleID);
            if (this.Lpc != null && this.Lpc.Count > 0)
            {
                template_wd_lpc_coverpage _lpc = this.Lpc[0];

                tb_m_detail_spec tem = new tb_m_detail_spec().SelectByID(Convert.ToInt32(_lpc.detail_spec_id));
                if (tem != null)
                {
                    lbSpecDesc.Text = String.Format("The Specification is based on Western Digital 's Doc {0} {1}", tem.B, tem.A);
                }

                //Method
                txtB21.Text = _lpc.ProcedureNo;
                txtC21.Text = _lpc.NumberOfPieces;
                txtD21.Text = _lpc.ExtractionMedium;
                txtE21.Text = _lpc.ExtractionVolume;

                lbTestMethod.Text = txtB21.Text;

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

                ddlWashMethod.SelectedValue = _lpc.WashMethod;
                switch (_lpc.WashMethod)
                {
                    case "Flip":
                    case "Rinse":
                    case "Shake":
                        pTankConditions.Visible = false;
                        break;
                    case "Ultrasonic":
                        pTankConditions.Visible = true;
                        break;
                    default:
                        pTankConditions.Visible = false;
                        break;
                }



                ddlSpecification.SelectedValue = _lpc.detail_spec_id.ToString();
                ddlComponent.SelectedValue = _lpc.component_id.ToString();
                ddlUnit.SelectedValue = _lpc.unit.ToString();
                ddlUnit2.SelectedValue = _lpc.unit2.ToString();
                ddlUnit3.SelectedValue = _lpc.unit3.ToString();
                ddlUnit4.SelectedValue = _lpc.unit4.ToString();


                this.CommandName = CommandNameEnum.Edit;

                gvSpec.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SPEC));
                gvSpec.DataBind();


                #region "Unit"
                gvSpec.Columns[2].HeaderText = String.Format("Specification Limits({0})", ddlUnit.SelectedItem.Text);
                gvSpec.Columns[3].HeaderText = String.Format("Average of {0} {1} points({2})", typeOfTest.name.Split(' ')[1].ToLower(), typeOfTest.name.Split(' ')[2].ToLower(), ddlUnit.SelectedItem.Text);

                gvResult.Columns[2].HeaderText = String.Format("Blank({0})", ddlUnit2.SelectedItem.Text);
                gvResult.Columns[3].HeaderText = String.Format("Sample({0})", ddlUnit2.SelectedItem.Text);

                gvResult.Columns[4].HeaderText = String.Format("Blank-corrected({0})", ddlUnit3.SelectedItem.Text);
                gvResult.Columns[5].HeaderText = String.Format("Blank-corrected({0})", ddlUnit4.SelectedItem.Text);

                gvResult.Columns[6].Visible = userRole == RoleEnum.CHEMIST;

                gvStatic.Columns[2].HeaderText = String.Format("Blank-corrected({0})", ddlUnit3.SelectedItem.Text);
                gvStatic.Columns[3].HeaderText = String.Format("Blank-corrected({0})", ddlUnit4.SelectedItem.Text);
                #endregion
                CalculateCas();
            }
            else
            {
                this.Lpc = new List<template_wd_lpc_coverpage>();
                this.CommandName = CommandNameEnum.Add;
                txtB54.Text = "20.2 L";
                txtC54.Text = "68 kHz";
                txtD54.Text = "4.8 W/L";
                //init

//44  LPC Counts/ cm2  1
//45  LPC Counts/ Swab tip 1
//46  LPC Particles/ cm2   1
//47  LPC Particles/ Tip   1
//48  LPC Particles/ Tweezer   1
//49  LPC Counts/ part 1
//50  LPC Particles/ part  1


                ddlUnit.SelectedValue = "46";
                ddlUnit2.SelectedValue = "56";
                ddlUnit3.SelectedValue = "49";
                ddlUnit4.SelectedValue = "44";

            }

            #endregion

            //initial component
            btnCoverPage.CssClass = "btn blue";
            btnWorkSheet.CssClass = "btn green";



            litDownloadIcon.Text = "<i class=\"fa fa-" + (lbJobStatus.Text.Equals("CONVERT_PDF") ? "file-pdf-o" : "file-word-o") + "\"></i>";


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
                        this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
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
                            cov.WashMethod = ddlWashMethod.SelectedValue;
                            cov.unit = Convert.ToInt32(ddlUnit.SelectedValue);
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
                            this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                            //#region ":: STAMP COMPLETE DATE"
                            this.jobSample.date_chemist_complete = DateTime.Now;
                            this.jobSample.date_chemist_alalyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                            //#endregion
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
                                cov.WashMethod = ddlWashMethod.SelectedValue;
                                cov.unit = Convert.ToInt32(ddlUnit.SelectedValue);
                                cov.unit2 = Convert.ToInt32(ddlUnit2.SelectedValue);
                                cov.unit3 = Convert.ToInt32(ddlUnit3.SelectedValue);
                                cov.unit4 = Convert.ToInt32(ddlUnit4.SelectedValue);

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
                        //isValid = false;
                    }
                    //this.jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);
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

            #region "GET VALUE FROM XLS"
            String yyyMMdd = DateTime.Now.ToString("yyyyMMdd");
            //Remove Old
            this.Lpc.RemoveAll(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE));
            this.Lpc.RemoveAll(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY));

            List<String> listOFParticle = new List<String>();
            listOFParticle.Add("0.200");
            listOFParticle.Add("0.300");
            listOFParticle.Add("0.500");
            listOFParticle.Add("0.700");
            listOFParticle.Add("1.000");
            listOFParticle.Add("2.000");

            List<template_wd_lpc_coverpage> lpcs = new List<template_wd_lpc_coverpage>();

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
                            //String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));


                            if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                            }

                            _postedFile.SaveAs(source_file);

                            using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                            {
                                HSSFWorkbook wd = new HSSFWorkbook(fs);
                                ISheet isheet = wd.GetSheet("Sheet1");
                                bool bStartAddData = false;

                                for (int row = 17; row < 120; row++)
                                {
                                    if (isheet.GetRow(row) != null) //null is when the row only contains empty cells 
                                    {
                                        if (CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.D)).Equals("Series Averages"))
                                        {
                                            bStartAddData = true;
                                        }
                                        if (bStartAddData)
                                        {
                                            if (listOFParticle.Contains(CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.G))))
                                            {
                                                template_wd_lpc_coverpage lpc = new template_wd_lpc_coverpage();
                                                lpc.A = Path.GetFileNameWithoutExtension(_postedFile.FileName).Replace('B', ' ').Replace('S', ' ').Trim();
                                                lpc.B = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.G));
                                                lpc.C = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.Q));
                                                lpc.type = Path.GetFileNameWithoutExtension(_postedFile.FileName).StartsWith("B") ? "Blank" : "Sample";
                                                switch (lpc.type)
                                                {
                                                    case "Blank":
                                                        lpc.C = String.Format(getDecimalFormat(Convert.ToInt32(txtDecimal01.Text)), Math.Round(Double.Parse(lpc.C), Convert.ToInt32(txtDecimal01.Text)));
                                                        break;
                                                    case "Sample":
                                                        lpc.C = String.Format(getDecimalFormat(Convert.ToInt32(txtDecimal02.Text)), Math.Round(Double.Parse(lpc.C), Convert.ToInt32(txtDecimal02.Text)));
                                                        break;
                                                }
                                                lpcs.Add(lpc);
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
            List<template_wd_lpc_coverpage> tmps = new List<template_wd_lpc_coverpage>();

            var grps = from lpc in lpcs group lpc by lpc.A into newGroup orderby newGroup.Key select newGroup;
            foreach (var item in grps)
            {
                foreach (String par in listOFParticle)
                {
                    template_wd_lpc_coverpage tmp = new template_wd_lpc_coverpage();
                    tmp.A = item.Key;
                    tmp.B = par;
                    string blankValue = lpcs.Where(x => x.type.Equals("Blank") && x.A.Equals(item.Key) && x.B.Equals(par)).FirstOrDefault().C;
                    tmp.C = blankValue;
                    string sampleValue = lpcs.Where(x => x.type.Equals("Sample") && x.A.Equals(item.Key) && x.B.Equals(par)).FirstOrDefault().C;
                    tmp.D = sampleValue;


                    if (!String.IsNullOrEmpty(txtB48.Text))
                    {
                        double result = (Double.Parse(sampleValue) - Double.Parse(blankValue)) * Double.Parse(String.IsNullOrEmpty(txtB49.Text) ? "0" : txtB49.Text) / Double.Parse(String.IsNullOrEmpty(txtB51.Text) ? "0" : txtB51.Text);

                        double result2 = result / Double.Parse(txtB48.Text);
                        tmp.E = result.ToString("N" + txtDecimal03.Text);
                        tmp.F = result2.ToString("N" + txtDecimal04.Text);

                        tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                        tmp.data_type = Convert.ToInt32(WDLpcDataType.DATA_VALUE);
                        tmps.Add(tmp);
                    }
                }

            }


            foreach (String par in listOFParticle)
            {
                template_wd_lpc_coverpage tmp = new template_wd_lpc_coverpage();
                tmp.A = "Average";
                tmp.B = par;
                tmp.C = string.Empty;
                tmp.D = string.Empty;
                tmp.E = CustomUtils.Average(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE) && x.B.Equals(par)).Select(x => Convert.ToDouble(x.E)).ToList()).ToString();
                tmp.F = CustomUtils.Average(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE) && x.B.Equals(par)).Select(x => Convert.ToDouble(x.F)).ToList()).ToString();
                tmp.data_type = Convert.ToInt32(WDLpcDataType.SUMMARY);
                tmps.Add(tmp);
            }
            foreach (String par in listOFParticle)
            {
                template_wd_lpc_coverpage tmp = new template_wd_lpc_coverpage();
                tmp.A = "Standard Deviation";
                tmp.B = par;
                tmp.C = string.Empty;
                tmp.D = string.Empty;
                tmp.E = CustomUtils.StandardDeviation(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE) && x.B.Equals(par)).Select(x => Convert.ToDouble(x.E)).ToList()).ToString();
                tmp.F = CustomUtils.StandardDeviation(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE) && x.B.Equals(par)).Select(x => Convert.ToDouble(x.F)).ToList()).ToString();
                tmp.data_type = Convert.ToInt32(WDLpcDataType.SUMMARY);
                tmps.Add(tmp);
            }
            foreach (String par in listOFParticle)
            {
                template_wd_lpc_coverpage tmp = new template_wd_lpc_coverpage();
                tmp.A = "%RSD Deviation";
                tmp.B = par;
                tmp.C = string.Empty;
                tmp.D = string.Empty;

                double _E = double.Parse(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.B.Equals(par) && x.A.Equals("Average")).FirstOrDefault().E);
                double _E2 = double.Parse(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.B.Equals(par) && x.A.Equals("Standard Deviation")).FirstOrDefault().E);

                double _F = double.Parse(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.B.Equals(par) && x.A.Equals("Average")).FirstOrDefault().F);
                double _F2 = double.Parse(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.B.Equals(par) && x.A.Equals("Standard Deviation")).FirstOrDefault().F);

                tmp.E = ((_E2 / _E) * 100).ToString();
                tmp.F = ((_F2 / _F) * 100).ToString();
                tmp.data_type = Convert.ToInt32(WDLpcDataType.SUMMARY);
                tmps.Add(tmp);
            }

            this.Lpc.AddRange(tmps);

            int order = 1;
            foreach (template_wd_lpc_coverpage lpc in this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE)))
            {
                lpc.ID = order;
                order++;
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
                lbSpecDesc.Text = String.Format("The Specification is based on Western Digital 's Doc {0} {1}", tem.B, tem.A);


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

                lbTestMethod.Text = txtB21.Text;

                lbSpecDesc.Text = String.Format("The Specification is based on Western Digital 's Doc {0} {1}", tem.B, tem.A);
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
            List<template_wd_lpc_coverpage> listOfBlanRows = new List<template_wd_lpc_coverpage>();
            List<template_wd_lpc_coverpage> listOfBlanRows2 = new List<template_wd_lpc_coverpage>();

            List<String> nums = new List<String>();

            DataTable dt = Extenders.ObjectToDataTable(this.Lpc[0]);
            List<template_wd_lpc_coverpage> specs = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SPEC) && x.row_type.Value == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            List<template_wd_lpc_coverpage> values = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE) && x.row_type.Value == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            foreach (template_wd_lpc_coverpage xxxx in values)
            {
                if (!nums.Contains(xxxx.B))
                {

                    nums.Add(xxxx.B);

                }
            }

            List<template_wd_lpc_coverpage> sumarys = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && nums.Contains(x.B) && !x.B.Equals("0.200")).ToList();
            template_wd_lpc_coverpage tmp = new template_wd_lpc_coverpage();
            int pc = values.Count / 5;
            for (int i = 1; i <= pc; i++)
            {

                tmp = new template_wd_lpc_coverpage();
                tmp.A = i.ToString();
                tmp.B = "3";
                tmp.C = String.Empty;
                tmp.D = String.Empty;
                tmp.E = "-99";
                tmp.F = "-99";
                tmp.data_type = Convert.ToInt32(WDLpcDataType.DATA_VALUE);
                tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listOfBlanRows.Add(tmp);

            }

            tmp = new template_wd_lpc_coverpage();
            tmp.A = "Average";
            tmp.B = "3";
            tmp.C = String.Empty;
            tmp.D = String.Empty;
            tmp.E = "-99";
            tmp.F = "-99";
            tmp.data_type = Convert.ToInt32(WDLpcDataType.SUMMARY);
            tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
            listOfBlanRows2.Add(tmp);
            tmp = new template_wd_lpc_coverpage();
            tmp.A = "Standard Deviation";
            tmp.B = "3";
            tmp.C = String.Empty;
            tmp.D = String.Empty;
            tmp.E = "-99";
            tmp.F = "-99";
            tmp.data_type = Convert.ToInt32(WDLpcDataType.SUMMARY);
            tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
            listOfBlanRows2.Add(tmp);
            tmp = new template_wd_lpc_coverpage();
            tmp.A = "%RSD Deviation";
            tmp.B = "3";
            tmp.C = String.Empty;
            tmp.D = String.Empty;
            tmp.E = "-99";
            tmp.F = "-99";
            tmp.data_type = Convert.ToInt32(WDLpcDataType.SUMMARY);
            tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
            listOfBlanRows2.Add(tmp);
            values.AddRange(listOfBlanRows);
            sumarys.AddRange(listOfBlanRows2);
            foreach (template_wd_lpc_coverpage lpc in specs)
            {
                lpc.D = Math.Round(Convert.ToDouble(lpc.D)) + "";//.ToString("N"+txtDecimal01.Text);// String.Format(getDecimalFormat(Convert.ToInt32(txtDecimal01.Text)), Math.Round(Double.Parse(lpc.D), Convert.ToInt32(txtDecimal01.Text)));
                Console.WriteLine();
            }
            foreach (template_wd_lpc_coverpage lpc in sumarys)
            {
                lpc.E = Convert.ToDouble(lpc.E).ToString("N" + txtDecimal01.Text);// String.Format(getDecimalFormat(Convert.ToInt32(txtDecimal01.Text)), Math.Round(Double.Parse(lpc.D), Convert.ToInt32(txtDecimal01.Text)));
                lpc.F = Convert.ToDouble(lpc.F).ToString("N" + txtDecimal02.Text);// String.Format(getDecimalFormat(Convert.ToInt32(txtDecimal01.Text)), Math.Round(Double.Parse(lpc.D), Convert.ToInt32(txtDecimal01.Text)));
                lpc.D = lpc.A.Replace("%", "Z");

                Console.WriteLine();
            }

            ReportHeader reportHeader = ReportHeader.getReportHeder(this.jobSample);


            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));
            reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateAnalyzed", reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            reportParameters.Add(new ReportParameter("DateTestCompleted", reportHeader.dateOfTestComplete.ToString("dd MMMM yyyy") + ""));

            reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));

            reportParameters.Add(new ReportParameter("txtB48", txtB48.Text));
            reportParameters.Add(new ReportParameter("txtB49", txtB49.Text));
            reportParameters.Add(new ReportParameter("txtB50", txtB50.Text));
            reportParameters.Add(new ReportParameter("txtB51", txtB51.Text));
            reportParameters.Add(new ReportParameter("txtB54", txtB54.Text));
            reportParameters.Add(new ReportParameter("txtC54", txtC54.Text));
            reportParameters.Add(new ReportParameter("txtD54", txtD54.Text));
            reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit2", ddlUnit2.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit3", ddlUnit3.SelectedItem.Text));
            reportParameters.Add(new ReportParameter("rpt_unit4", ddlUnit4.SelectedItem.Text));

            reportParameters.Add(new ReportParameter("method", txtB21.Text));
            reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.jobSample.singapore_ref_no) ? String.Empty : this.jobSample.singapore_ref_no)));
            reportParameters.Add(new ReportParameter("partizleSizeCount", pc.ToString()));
            reportParameters.Add(new ReportParameter("showTankCondition", (!pTankConditions.Visible).ToString()));





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
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", values.Where(x => (new String[] { "1", "2", "3", "4" }).Contains(x.A)).OrderBy(x => x.A).ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", values.Where(x => (new String[] { "5" }).Contains(x.A)).OrderBy(x => x.A).ToDataTable())); // Add datasource here

            //if (sumarys.Count > 0 && sumarys.Count <= 10)
            //{
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", sumarys.GetRange(0, sumarys.Count).OrderBy(x => x.D).ToDataTable())); // Add datasource here
                                                                                                                                                      //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", new DataTable())); // Add datasource here
                                                                                                                                                      //}
                                                                                                                                                      //if (sumarys.Count > 10)
                                                                                                                                                     //{
                                                                                                                                                      //    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", sumarys.GetRange(0, 10).ToDataTable())); // Add datasource here
                                                                                                                                                      //    viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", sumarys.GetRange(10, sumarys.Count - 10).ToDataTable())); // Add datasource here
                                                                                                                                                      //}






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
                    break;
            }


        }

        #region "Custom method"

        private void CalculateCas()
        {
            List<template_wd_lpc_coverpage> calList = this.Lpc;

            foreach (template_wd_lpc_coverpage val in calList.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SPEC)))
            {
                template_wd_lpc_coverpage tmp = calList.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.A.Equals("Average") && Convert.ToDouble(x.B) == Convert.ToDouble(val.B)).FirstOrDefault();
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

            //Cal Average

            foreach (template_wd_lpc_coverpage item in calList.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY)).ToList())
            {
                calList.Remove(item);
            }

            List<String> listOFParticle = new List<String>();

            listOFParticle.Add("0.200");
            listOFParticle.Add("0.300");
            listOFParticle.Add("0.500");
            listOFParticle.Add("0.700");
            listOFParticle.Add("1.000");
            listOFParticle.Add("2.000");


            List<template_wd_lpc_coverpage> tmps = new List<template_wd_lpc_coverpage>();

            foreach (String par in listOFParticle)
            {
                template_wd_lpc_coverpage tmp = new template_wd_lpc_coverpage();
                tmp.ID = CustomUtils.GetRandomNumberID();
                tmp.A = "Average";
                tmp.B = par;
                tmp.C = string.Empty;
                tmp.D = string.Empty;
                tmp.E = CustomUtils.Average(this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE) && x.B.Equals(par) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).Select(x => Convert.ToDouble(x.E)).ToList()).ToString();
                tmp.F = CustomUtils.Average(this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE) && x.B.Equals(par) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).Select(x => Convert.ToDouble(x.F)).ToList()).ToString();
                tmp.data_type = Convert.ToInt32(WDLpcDataType.SUMMARY);
                tmp.row_type = Convert.ToInt16(RowTypeEnum.Normal);

                tmps.Add(tmp);
            }
            foreach (String par in listOFParticle)
            {
                template_wd_lpc_coverpage tmp = new template_wd_lpc_coverpage();
                tmp.ID = CustomUtils.GetRandomNumberID();
                tmp.A = "Standard Deviation";
                tmp.B = par;
                tmp.C = string.Empty;
                tmp.D = string.Empty;
                tmp.E = CustomUtils.StandardDeviation(this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE) && x.B.Equals(par) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).Select(x => Convert.ToDouble(x.E)).ToList()).ToString();
                tmp.F = CustomUtils.StandardDeviation(this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE) && x.B.Equals(par) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).Select(x => Convert.ToDouble(x.F)).ToList()).ToString();
                tmp.data_type = Convert.ToInt32(WDLpcDataType.SUMMARY);
                tmp.row_type = Convert.ToInt16(RowTypeEnum.Normal);

                tmps.Add(tmp);
            }
            foreach (String par in listOFParticle)
            {
                template_wd_lpc_coverpage tmp = new template_wd_lpc_coverpage();
                tmp.ID = CustomUtils.GetRandomNumberID();
                tmp.A = "%RSD Deviation";
                tmp.B = par;
                tmp.C = string.Empty;
                tmp.D = string.Empty;

                double _E = double.Parse(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.B.Equals(par) && x.A.Equals("Average")).FirstOrDefault().E);
                double _E2 = double.Parse(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.B.Equals(par) && x.A.Equals("Standard Deviation")).FirstOrDefault().E);

                double _F = double.Parse(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.B.Equals(par) && x.A.Equals("Average")).FirstOrDefault().F);
                double _F2 = double.Parse(tmps.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY) && x.B.Equals(par) && x.A.Equals("Standard Deviation")).FirstOrDefault().F);
                if (_F < 0)
                {
                    Console.WriteLine();
                }
                tmp.E = (_E == 0 || _E < 0) ? "0" : ((_E2 / _E) * 100).ToString();
                tmp.F = (_F == 0 || _F < 0) ? "0" : ((_F2 / _F) * 100).ToString();

                tmp.row_type = Convert.ToInt16(RowTypeEnum.Normal);
                tmp.data_type = Convert.ToInt32(WDLpcDataType.SUMMARY);
                tmps.Add(tmp);
            }
            calList.AddRange(tmps);

            this.Lpc = calList;


            foreach (var val in this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE)).ToList())
            {
                val.E = (Convert.ToDouble(val.E) < 0) ? "0" : val.E;
                val.F = (Convert.ToDouble(val.F) < 0) ? "0" : val.F;
            }


            gvResult.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE));
            gvResult.DataBind();

            gvStatic.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SUMMARY));
            gvStatic.DataBind();

            gvSpec.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.SPEC));
            gvSpec.DataBind();

            btnSubmit.Enabled = true;

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
                    if (!_litSpecLimit.Text.Equals("NA"))
                    {
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
                int PKID = Convert.ToInt32(gvResult.DataKeys[e.Row.RowIndex].Values[0].ToString());
                if (gvResult.DataKeys[e.Row.RowIndex].Values[1] != null)
                {
                    RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvResult.DataKeys[e.Row.RowIndex].Values[1]);
                    //Literal _litBlank = (Literal)e.Row.FindControl("litBlank");
                    //Literal _litSample = (Literal)e.Row.FindControl("litSample");
                    //Literal _litBlankCorredted = (Literal)e.Row.FindControl("litBlankCorredted");
                    //Literal _litBlankCorredtedCM2 = (Literal)e.Row.FindControl("litBlankCorredtedCM2");

                    LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                    LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");
                    //if (_litBlank != null && _litSample != null && _litBlankCorredted != null && _litBlankCorredtedCM2 != null)
                    //{
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
        }


        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
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

                    gvResult.DataSource = this.Lpc.Where(x => x.data_type == Convert.ToInt32(WDLpcDataType.DATA_VALUE));
                    gvResult.DataBind();
                    CalculateCas();
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
                    try
                    {


                        if (_lbStatistics.Text.Equals("%RSD Deviation"))
                        {
                            if (!String.IsNullOrEmpty(_litBlankCorredted.Text) && !String.IsNullOrEmpty(_litBlankCorredtedCM2.Text))
                            {
                                _litBlankCorredted.Text = String.Format("{0}%", Math.Round(Convert.ToDouble(_litBlankCorredted.Text)));
                                _litBlankCorredtedCM2.Text = String.Format("{0}%", Math.Round(Convert.ToDouble(_litBlankCorredtedCM2.Text)));
                            }
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(_litBlankCorredted.Text) && !String.IsNullOrEmpty(_litBlankCorredtedCM2.Text))
                            {

                                _litBlankCorredtedCM2.Text = Math.Round(Convert.ToDouble(_litBlankCorredtedCM2.Text)) + "";
                                _litBlankCorredted.Text = Math.Round(Convert.ToDouble(_litBlankCorredted.Text)) + "";
                            }
                        }



                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        protected void ddlWashMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlWashMethod.SelectedValue)
            {
                case "Flip":
                case "Rinse":
                case "Shake":
                    pTankConditions.Visible = false;
                    break;
                case "Ultrasonic":
                    pTankConditions.Visible = true;
                    break;
            }
        }

        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "WD");
            }
            else
            {
                tb_m_detail_spec tem = new tb_m_detail_spec().SelectByID(int.Parse(ddlSpecification.SelectedValue));
                if (tem != null)
                {
                    lbSpecDesc.Text = String.Format("The Specification is based on Western Digital 's Doc {0} {1}", tem.B, tem.A);
                }
            }

        }


        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_type_of_test typeOfTest = new m_type_of_test();
            typeOfTest = typeOfTest.SelectByID(jobSample.type_of_test_id);

            #region "Unit"
            gvSpec.Columns[2].HeaderText = String.Format("Specification Limits({0})", ddlUnit.SelectedItem.Text);
            gvSpec.Columns[3].HeaderText = String.Format("Average of {0} {1} points({2})", typeOfTest.name.Split(' ')[1].ToLower(), typeOfTest.name.Split(' ')[2].ToLower(), ddlUnit.SelectedItem.Text);

            gvResult.Columns[2].HeaderText = String.Format("Blank({0})", ddlUnit2.SelectedItem.Text);
            gvResult.Columns[3].HeaderText = String.Format("Sample({0})", ddlUnit2.SelectedItem.Text);

            gvResult.Columns[4].HeaderText = String.Format("Blank-corrected({0})", ddlUnit3.SelectedItem.Text);
            gvResult.Columns[5].HeaderText = String.Format("Blank-corrected({0})", ddlUnit4.SelectedItem.Text);

            gvStatic.Columns[2].HeaderText = String.Format("Blank-corrected({0})", ddlUnit3.SelectedItem.Text);
            gvStatic.Columns[3].HeaderText = String.Format("Blank-corrected({0})", ddlUnit4.SelectedItem.Text);
            #endregion

            CalculateCas();
        }
    }
}