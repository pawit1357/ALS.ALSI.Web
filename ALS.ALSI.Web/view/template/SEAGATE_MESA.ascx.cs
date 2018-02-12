﻿using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using ALS.ALSI.Web.view.request;
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
using Spire.Doc;
using Spire.Doc.Documents;

namespace ALS.ALSI.Web.view.template
{
    public partial class SEAGATE_MESA : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_MESA));

        #region "Property"

        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public List<template_seagate_mesa_coverpage> coverpages
        {
            get { return (List<template_seagate_mesa_coverpage>)Session[GetType().Name + "coverpages"]; }
            set { Session[GetType().Name + "coverpages"] = value; }
        }

        public List<template_seagate_mesa_img> refImg
        {
            get { return (List<template_seagate_mesa_img>)Session[GetType().Name + "refImg"]; }
            set { Session[GetType().Name + "refImg"] = value; }
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

        public int SampleSize
        {
            get { return (int)Session[GetType().Name + "SampleSize"]; }
            set { Session[GetType().Name + "SampleSize"] = value; }
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

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "coverpages");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "SampleID");
            Session.Remove(GetType().Name + "SampleSize");
        }

        private void initialPage()
        {
            //this.refImg = new List<template_seagate_mesa_img>();
            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));


            tb_m_component comp = new tb_m_component();
            comp.specification_id = this.jobSample.specification_id;
            comp.template_id = this.jobSample.template_id;

            ddlComponent.Items.Clear();
            ddlComponent.DataSource = comp.SelectAll();
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

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
                            gvResult.Columns[4].Visible = false;
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
                            gvResult.Columns[4].Visible = true;
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
                            gvResult.Columns[4].Visible = false;
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
                            gvResult.Columns[4].Visible = false;
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
                            gvResult.Columns[4].Visible = false;
                        }
                        break;
                }
                txtDateAnalyzed.Text = (this.jobSample.date_chemist_alalyze != null) ? this.jobSample.date_chemist_alalyze.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                pAnalyzeDate.Visible = userRole == RoleEnum.CHEMIST;

            }
            #endregion
            #region "WORKING"
            this.coverpages = template_seagate_mesa_coverpage.FindAllBySampleID(this.SampleID);
            this.refImg = template_seagate_mesa_img.FindAllBySampleID(this.SampleID);
            if (this.refImg != null && this.refImg.Count > 0)
            {
                gvRefImages.DataSource = this.refImg.OrderBy(x => x.area).OrderBy(x => x.descripton).ToList();
                gvRefImages.DataBind();
            }
            if (this.coverpages != null && this.coverpages.Count > 0)
            {
                this.CommandName = CommandNameEnum.Edit;

                template_seagate_mesa_coverpage coverpage = this.coverpages[0];
                if (coverpage != null)
                {
                    ddlComponent.SelectedValue = coverpage.component_id.ToString();

                    txtProcedureNo_Extraction.Text = coverpage.ProcedureNo_Extraction;
                    txtSampleSize_Extraction.Text = coverpage.SampleSize_Extraction;
                    txtOvenCondition_Extraction.Text = coverpage.OvenCondition_Extraction;

                    lbProcedureNo_Extraction.Text = txtProcedureNo_Extraction.Text;
                    lbSampleSize_Extraction.Text = txtSampleSize_Extraction.Text;
                    lbOvenCondition_Extraction.Text = txtOvenCondition_Extraction.Text;

                }




                cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;
                if (cbCheckBox.Checked)
                {
                    lbSpecDesc.Text = "";
                }
                else
                {

                    tb_m_component component = new tb_m_component().SelectByID(this.coverpages[0].component_id.Value);// this.coverpages[0].tb_m_component;
                    if (component != null)
                    {
                        lbSpecDesc.Text = String.Format("The Specification is based on Western Digital 's Doc {0} {1}", component.B, component.A);
                    }
                }


                foreach (template_seagate_mesa_coverpage _val in this.coverpages)
                {
                    if (_val.row_type == null)
                    {
                        _val.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                    }
                }
                gvResult.DataSource = this.coverpages;
                gvResult.DataBind();
            }
            else
            {
                this.CommandName = CommandNameEnum.Add;
                this.coverpages = new List<template_seagate_mesa_coverpage>();
            }
            #endregion


            if (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
            {
                txtProcedureNo_Extraction.Visible = true;
                txtSampleSize_Extraction.Visible = true;
                txtOvenCondition_Extraction.Visible = true;


                lbProcedureNo_Extraction.Visible = false;
                lbSampleSize_Extraction.Visible = false;
                lbOvenCondition_Extraction.Visible = false;

                pRefImage.Visible = true;
                gvResult.Columns[4].Visible = true;
                //gvResult.Columns[5].Visible = true;
                //gvRefImages.Columns[6].Visible = true;
            }
            else
            {
                txtProcedureNo_Extraction.Visible = false;
                txtSampleSize_Extraction.Visible = false;
                txtOvenCondition_Extraction.Visible = false;

                lbProcedureNo_Extraction.Visible = true;
                lbSampleSize_Extraction.Visible = true;
                lbOvenCondition_Extraction.Visible = true;

                pRefImage.Visible = false;
                gvResult.Columns[4].Visible = false;
                //gvResult.Columns[5].Visible = false;
                //gvRefImages.Columns[6].Visible = false;


            }

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
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    foreach (template_seagate_mesa_coverpage _cover in this.coverpages)
                    {
                        _cover.ProcedureNo_Extraction = txtProcedureNo_Extraction.Text;
                        _cover.SampleSize_Extraction = txtSampleSize_Extraction.Text;
                        _cover.OvenCondition_Extraction = txtOvenCondition_Extraction.Text;
                    }
                    switch (this.CommandName)
                    {
                        case CommandNameEnum.Add:
                            template_seagate_mesa_coverpage.InsertList(this.coverpages);

                            break;
                        case CommandNameEnum.Edit:
                            template_seagate_mesa_coverpage.UpdateList(this.coverpages);

                            break;
                    }

                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    this.jobSample.date_chemist_alalyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                    //#endregion

                    foreach (template_seagate_mesa_coverpage _cover in this.coverpages)
                    {
                        _cover.ProcedureNo_Extraction = txtProcedureNo_Extraction.Text;
                        _cover.SampleSize_Extraction = txtSampleSize_Extraction.Text;
                        _cover.OvenCondition_Extraction = txtOvenCondition_Extraction.Text;
                    }
                    template_seagate_mesa_coverpage.UpdateList(this.coverpages);
                    template_seagate_mesa_img.InsertList(this.refImg);

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
                            this.jobSample.path_word = string.Empty;
                            this.jobSample.path_pdf = string.Empty;
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
                    if (btnUpload.HasFile)// && (Path.GetExtension(btnUpload.FileName).Equals(".doc") || Path.GetExtension(btnUpload.FileName).Equals(".docx")))
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
                this.jobSample.update_date = DateTime.Now;
                this.jobSample.update_by = userLogin.id;
                this.jobSample.Update();
                //Commit
                GeneralManager.Commit();

                //removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //removeSession();
            Response.Redirect(this.PreviousPath);
        }

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_component component = new tb_m_component().SelectByID(Convert.ToInt32(ddlComponent.SelectedValue));
            if (component != null)
            {
                lbSpecDesc.Text = String.Format("The Specification is based on Western Digital 's Doc {0} {1}", component.B, component.A);

                txtSampleSize_Extraction.Text = component.D;

                tb_m_detail_spec_ref detailSpecRef = new tb_m_detail_spec_ref();
                detailSpecRef.spec_ref = Convert.ToInt32(component.E);
                detailSpecRef.template_id = this.jobSample.template_id;


                List<tb_m_detail_spec_ref> detailSpecRefs = (List<tb_m_detail_spec_ref>)detailSpecRef.SelectAll();
                if (detailSpecRefs != null)
                {
                    this.coverpages = new List<template_seagate_mesa_coverpage>();
                    foreach (tb_m_detail_spec_ref spec in detailSpecRefs)
                    {
                        template_seagate_mesa_coverpage work = new template_seagate_mesa_coverpage();
                        work.sample_id = this.SampleID;
                        work.component_id = component.ID;
                        work.location_of_parts = spec.B;
                        work.specification = spec.C;
                        work.result = String.Empty;
                        work.pass_fail = string.Empty;
                        work.RowState = CommandNameEnum.Add;
                        work.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                        this.coverpages.Add(work);
                    }
                    gvResult.DataSource = this.coverpages;
                    gvResult.DataBind();
                }
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

        #endregion

        #region "COVERPAGES GRID."
        protected void gvCoverPages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        #endregion

        #region "DHS GRID."

        protected void gvResult_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvResult.EditIndex = e.NewEditIndex;
            gvResult.DataSource = this.coverpages;
            gvResult.DataBind();
            DropDownList _ddlResult = (DropDownList)gvResult.Rows[e.NewEditIndex].FindControl("ddlResult");
            if (_ddlResult != null)
            {
                _ddlResult.Items.Clear();
                _ddlResult.Items.Add(new ListItem("Not Detected", "Not Detected"));
                _ddlResult.Items.Add(new ListItem("Detected", "Detected"));
            }
        }

        protected void gvResult_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvResult.EditIndex = -1;
        }

        protected void gvResult_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int _id = Convert.ToInt32(gvResult.DataKeys[e.RowIndex].Values[0].ToString());
            DropDownList _ddlResult = (DropDownList)gvResult.Rows[e.RowIndex].FindControl("ddlResult");
            if (_ddlResult != null)
            {
                template_seagate_mesa_coverpage _tmp = this.coverpages.Find(x => x.ID == _id);
                if (_tmp != null)
                {
                    _tmp.result = _ddlResult.SelectedValue;
                    _tmp.pass_fail = _ddlResult.SelectedValue.Equals("Detected") ? "FAIL" : "PASS";
                    _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                }
            }

            gvResult.EditIndex = -1;
            gvResult.DataSource = this.coverpages;
            gvResult.DataBind();
        }

        //protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        int _id = Convert.ToInt32(gvResult.DataKeys[e.Row.RowIndex].Values[0].ToString());
        //        LinkButton _btnEdit = (LinkButton)e.Row.FindControl("btnEdit");

        //    }
        //    else if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //    }
        //}

        //protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
        //    //if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
        //    //{
        //    //    int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
        //    //    tb_m_dhs_cas _cas = this.tbCas.Find(x => x.ID == PKID);
        //    //    if (_cas != null)
        //    //    {
        //    //        switch (cmd)
        //    //        {
        //    //            case CommandNameEnum.Hum:
        //    //                _cas.RowState = CommandNameEnum.Hum;
        //    //                break;
        //    //            case CommandNameEnum.ReUse:
        //    //                switch (_cas.RowState)
        //    //                {
        //    //                    case CommandNameEnum.Hum:
        //    //                        _cas.RowState = CommandNameEnum.TotalRow;
        //    //                        break;
        //    //                    default:
        //    //                        _cas.RowState = CommandNameEnum.Add;
        //    //                        break;
        //    //                }

        //    //                break;
        //    //            case CommandNameEnum.D34:
        //    //                _cas.RowState = CommandNameEnum.D34;
        //    //                break;
        //    //        }
        //    //        CalculateCas();
        //    //    }
        //    //}
        //}

        #endregion

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            //ReportBiz reportBiz = new ReportBiz();

            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);


            ReportHeader reportHeader = new ReportHeader();
            reportHeader = reportHeader.getReportHeder(this.jobSample);

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
            reportParameters.Add(new ReportParameter("Test", " "));
            reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
            reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.jobSample.singapore_ref_no) ? String.Empty : this.jobSample.singapore_ref_no)));


            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            List<template_seagate_mesa_img> dat = this.refImg.OrderBy(x => x.seq).OrderBy(x => x.descripton).ToList();
            foreach (template_seagate_mesa_img _i in dat)
            {

                if (_i.path_sem_image_at_250x != null)
                {
                    _i.img1 = CustomUtils.GetBytesFromImage(_i.path_sem_image_at_250x);
                }
                if (_i.path_sem_image_at_2000x != null)
                {
                    _i.img2 = CustomUtils.GetBytesFromImage(_i.path_sem_image_at_2000x);
                }
                if (_i.path_edx_spectrum != null)
                {
                    _i.img3 = CustomUtils.GetBytesFromImage(_i.path_edx_spectrum);
                }
            }

            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/mesa_seagate.rdlc");

            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here


            List<template_seagate_mesa_img> tmp = new List<template_seagate_mesa_img>();
            DataTable dtQ1 = new DataTable();
            DataTable dtQ2 = new DataTable();
            DataTable dtQ3 = new DataTable();
            DataTable dtQ4 = new DataTable();
            DataTable dtQ5 = new DataTable();
            DataTable dtQ6 = new DataTable();


            if (dat.Count >= 1)
            {
                tmp.Add(dat[0]);
                dtQ1 = tmp.ToDataTable();
            }
            if (dat.Count >= 2)
            {
                tmp = new List<template_seagate_mesa_img>();
                tmp.Add(dat[2]);
                dtQ2 = tmp.ToDataTable();
            }
            if (dat.Count >= 3)
            {
                tmp = new List<template_seagate_mesa_img>();
                tmp.Add(dat[1]);
                dtQ3 = tmp.ToDataTable();
            }
            if (dat.Count >= 4)
            {
                tmp = new List<template_seagate_mesa_img>();
                tmp.Add(dat[3]);
                dtQ4 = tmp.ToDataTable();
            }
            if (dat.Count >= 5)
            {
                tmp = new List<template_seagate_mesa_img>();
                tmp.Add(dat[4]);
                dtQ5 = tmp.ToDataTable();
            }
            if (dat.Count >= 6)
            {
                tmp = new List<template_seagate_mesa_img>();
                tmp.Add(dat[5]);
                dtQ6 = tmp.ToDataTable();
            }

            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", this.coverpages.Where(x => !x.specification.Equals("-")).ToDataTable())); // Add datasource here

            if (dtQ1 != null && dtQ1.Rows.Count > 0)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dtQ1)); // Add datasource here
            }
            else
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", new DataTable())); // Add datasource here

            }
            if (dtQ2 != null && dtQ2.Rows.Count > 0)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", dtQ2)); // Add datasource here
            }
            else
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", new DataTable())); // Add datasource here

            }
            if (dtQ3 != null && dtQ3.Rows.Count > 0)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", dtQ3)); // Add datasource here
            }
            else
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", new DataTable())); // Add datasource here
            }
            if (dtQ4 != null && dtQ4.Rows.Count > 0)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", dtQ4)); // Add datasource here
            }
            else
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", new DataTable())); // Add datasource here
            }
            if (dtQ5 != null && dtQ5.Rows.Count > 0)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet7", dtQ5)); // Add datasource here
            }
            else
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet7", new DataTable())); // Add datasource here
            }
            if (dtQ6 != null && dtQ6.Rows.Count > 0)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet8", dtQ6)); // Add datasource here
            }
            else
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet8", new DataTable())); // Add datasource here
            }



            string download = String.Empty;

            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            switch (status)
            {
                case StatusEnum.ADMIN_CONVERT_WORD:
                    if (!String.IsNullOrEmpty(this.jobSample.path_word))
                    {
                        Response.Redirect(String.Concat(Configurations.HOST, this.jobSample.path_word));
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
                        HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
                        HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
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


        private void downloadWord()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";

            string strFileName = string.Format("{0}_{1}.doc", this.jobSample.job_number.Replace("-", "_"), DateTime.Now.ToString("yyyyMMddhhmmss"));

            HttpContext.Current.Response.ContentType = "application/vnd.ms-word";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);

            StringWriter sw = new StringWriter();
            HtmlTextWriter h = new HtmlTextWriter(sw);
            //invDiv.RenderControl(h);
            string strHTMLContent = sw.GetStringBuilder().ToString();
            String html = "<html><header><style>body {max-width: 800px;margin:initial;font-family: \'Arial Unicode MS\';font-size: 10px;}table {border-collapse: collapse;}th {background: #666;color: #fff;border: 1px solid #999;padding: 0.5rem;text-align: center;}td { border: 1px solid #999;padding: 0.5rem;text-align: left;}h6 {font-weight:initial;}</style></header><body><form runat=\"server\" id=\"Form1\" method=\"POST\" >" + strHTMLContent + "</form></body></html>";


            HttpContext.Current.Response.Write(html);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();
        }

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            template_seagate_mesa_img _img = new template_seagate_mesa_img();
            _img.id = CustomUtils.GetRandomNumberID();
            _img.sample_id = this.SampleID;
            _img.descripton = txtDesc.Text;
            _img.seq = this.refImg.Count + 1;
            if (!String.IsNullOrEmpty(txtDesc.Text))
            {

                #region "SEM IMAGE AT 200X"
                if ((Path.GetExtension(FileUpload1.FileName).Equals(".jpg")))
                {
                    string yyyy = DateTime.Now.ToString("yyyy");
                    string MM = DateTime.Now.ToString("MM");
                    string dd = DateTime.Now.ToString("dd");

                    String randNumber = String.Format("{0}_250X{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(FileUpload1.FileName));

                    String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                    String source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                    }
                    FileUpload1.SaveAs(source_file);
                    _img.path_sem_image_at_250x = source_file_url;
                }
                #endregion
                #region "SEM IMAGE AT 2000X"
                if ((Path.GetExtension(FileUpload3.FileName).Equals(".jpg")))
                {
                    string yyyy = DateTime.Now.ToString("yyyy");
                    string MM = DateTime.Now.ToString("MM");
                    string dd = DateTime.Now.ToString("dd");

                    String randNumber = String.Format("{0}_2000X{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(FileUpload3.FileName));

                    String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                    String source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                    }
                    FileUpload3.SaveAs(source_file);
                    _img.path_sem_image_at_2000x = source_file_url;
                }
                #endregion
                #region "EDX SPECTRUM"
                if ((Path.GetExtension(FileUpload4.FileName).Equals(".jpg")))
                {
                    string yyyy = DateTime.Now.ToString("yyyy");
                    string MM = DateTime.Now.ToString("MM");
                    string dd = DateTime.Now.ToString("dd");

                    String randNumber = String.Format("{0}_SPECTRUM{1}{2}{3}", this.jobSample.job_number, DateTime.Now.ToString("yyyyMMdd"), CustomUtils.GenerateRandom(1000000, 9999999), Path.GetExtension(FileUpload4.FileName));

                    String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, randNumber);
                    String source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, randNumber));

                    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                    }
                    FileUpload4.SaveAs(source_file);
                    _img.path_edx_spectrum = source_file_url;
                }
                #endregion

                Boolean isExist = this.refImg.Where(x => x.area == _img.area && x.descripton.Equals(_img.descripton)).Any();
                if (!isExist)
                {
                    this.refImg.Add(_img);
                    txtDesc.Text = string.Empty;
                }
                gvRefImages.DataSource = this.refImg.OrderBy(x => x.seq).OrderBy(x => x.descripton).ToList();
                gvRefImages.DataBind();
            }
        }

        protected void gvRefImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_mesa_img _mesa = this.refImg.Find(x => x.id == PKID);
                if (_mesa != null)
                {
                    switch (cmd)
                    {
                        case CommandNameEnum.Delete:
                            this.refImg.Remove(_mesa);
                            break;

                    }
                    gvRefImages.DataSource = this.refImg.OrderBy(x => x.seq).OrderBy(x => x.descripton).ToList();
                    gvRefImages.DataBind();
                }
            }
        }

        protected void gvRefImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = "There is no Seagate's Specification for Reference.";
            }
            else
            {
                if (this.coverpages.Count > 0)
                {
                    tb_m_component component = new tb_m_component().SelectByID(Convert.ToInt32(this.coverpages[0].component_id));
                    if (component != null)
                    {
                        lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", component.B, component.A);
                    }
                }
            }

        }
    }
}