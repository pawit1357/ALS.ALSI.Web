using ALS.ALSI.Biz;
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

namespace ALS.ALSI.Web.view.template
{
    public partial class WD_Corrosion : System.Web.UI.UserControl
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WD_Corrosion));

        #region "Property"

        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public List<template_wd_corrosion_coverpage> coverpages
        {
            get { return (List<template_wd_corrosion_coverpage>)Session[GetType().Name + "coverpages"]; }
            set { Session[GetType().Name + "coverpages"] = value; }
        }

        public List<template_wd_corrosion_img> refImg
        {
            get { return (List<template_wd_corrosion_img>)Session[GetType().Name + "refImg"]; }
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
        List<String> errors = new List<String>();

        private void initialPage()
        {

            ddlAssignTo.Items.Clear();
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
            ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));

            tb_m_specification comp = new tb_m_specification();
            comp.specification_id = this.jobSample.specification_id;
            comp.template_id = this.jobSample.template_id;

            List<tb_m_specification> listSpec = comp.SelectAll().ToList();
            ddlMethod.Items.Clear();
            ddlMethod.DataSource = listSpec.Where(x => !x.A.Equals("Procedure") && !x.A.Equals("0"));
            ddlMethod.DataBind();
            ddlMethod.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = listSpec.Where(x => !x.B.Equals("Specification") && !x.B.Equals("0"));
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            ddlTemp.Items.Clear();
            ddlTemp.DataSource = listSpec.Where(x => !x.C.Equals("Temperature Humidity Parameters") && !x.C.Equals("0"));
            ddlTemp.DataBind();
            ddlTemp.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            //ddlComponent.Items.Clear();
            //ddlComponent.DataSource = comp.SelectAll();
            //ddlComponent.DataBind();
            //ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            //ddlSpecification.Items.Clear();
            //ddlSpecification.DataSource = comp.SelectAll();
            //ddlSpecification.DataBind();
            //ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));



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
            this.coverpages = template_wd_corrosion_coverpage.FindAllBySampleID(this.SampleID);
            this.refImg = template_wd_corrosion_img.FindAllBySampleID(this.SampleID);
            if (this.refImg != null && this.refImg.Count > 0)
            {
                gvRefImages.DataSource = this.refImg;
                gvRefImages.DataBind();
            }
            if (this.coverpages != null && this.coverpages.Count > 0)
            {
                this.CommandName = CommandNameEnum.Edit;
                template_wd_corrosion_coverpage cover = this.coverpages[0];
                ddlSpecification.SelectedValue = cover.specification_id.ToString();
                txtNumberOfPiecesUsedForExtraction.Text = this.coverpages[0].number_of_pieces_used_for_extraction;

               


                tb_m_specification procedure = new tb_m_specification().SelectByID(this.coverpages[0].procedureNo_id.Value);
                if (procedure != null)
                {
                    ddlMethod.SelectedValue = this.coverpages[0].procedureNo_id.Value.ToString();
                    txtProcedureNo.Text = procedure.A;
                    cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;

                    tb_m_specification specification = new tb_m_specification().SelectByID(this.coverpages[0].specification_id.Value);
                    if (specification != null)
                    {
                        ddlSpecification.SelectedValue = this.coverpages[0].specification_id.Value.ToString();
                        if (cbCheckBox.Checked)
                        {
                            lbResultDesc.Text = String.Format("This sample is no {0} specification reference", "WD");
                        }
                        else
                        {
                            lbResultDesc.Text = String.Format("The specification is based on Western Digital 's document no. {0}", specification.B);

                        }
                    }

                    if (this.coverpages[0].temperature_humidity_parameters_id != null)
                    {
                        ddlTemp.SelectedValue = this.coverpages[0].temperature_humidity_parameters_id.Value.ToString();

                        tb_m_specification temp = new tb_m_specification().SelectByID(this.coverpages[0].temperature_humidity_parameters_id.Value);
                        if (temp != null)
                        {
                            this.coverpages[0].temperature_humidity_parameters = temp.C;
                        }
                    }
                }

                gvResult.DataSource = this.coverpages;
                gvResult.DataBind();
            }
            else
            {
                this.CommandName = CommandNameEnum.Add;


                this.coverpages = new List<template_wd_corrosion_coverpage>();
                template_wd_corrosion_coverpage cov = new template_wd_corrosion_coverpage();

                cov.ID = 1;
                cov.sample_id = this.SampleID;
                cov.number_of_pieces_used_for_extraction = txtNumberOfPiecesUsedForExtraction.Text;

                cov.temperature_humidity_parameters = "85oC, 85%RH, 24hours";
                cov.specification = "No observable discoloration or spots at 40x";
                cov.result = "";
                this.coverpages.Add(cov);
                gvResult.DataSource = this.coverpages;
                gvResult.DataBind();
            }
            #endregion

            if (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
            {
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


                pRefImage.Visible = true;
                gvResult.Columns[3].Visible = true;
                gvResult.Columns[4].Visible = false;
                gvRefImages.Columns[2].Visible = true;
                txtProcedureNo.ReadOnly = false;
                txtNumberOfPiecesUsedForExtraction.ReadOnly = false;

            }
            else
            {
                pRefImage.Visible = false;
                gvResult.Columns[3].Visible = false;
                gvResult.Columns[4].Visible = false;
                gvRefImages.Columns[2].Visible = false;
                txtProcedureNo.ReadOnly = true;
                txtNumberOfPiecesUsedForExtraction.ReadOnly = true;

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
                    template_wd_corrosion_coverpage.DeleteBySampleID(this.SampleID);
                    foreach (template_wd_corrosion_coverpage _cover in this.coverpages)
                    {
                        _cover.procedureNo_id = Convert.ToInt32(ddlMethod.SelectedValue);
                        _cover.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        _cover.temperature_humidity_parameters_id = Convert.ToInt32(ddlTemp.SelectedValue);
                        _cover.number_of_pieces_used_for_extraction = txtNumberOfPiecesUsedForExtraction.Text;
                    }
                    template_wd_corrosion_coverpage.InsertList(this.coverpages);

                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    this.jobSample.date_chemist_alalyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                    //#endregion

                    foreach (template_wd_corrosion_coverpage _cover in this.coverpages)
                    {
                        _cover.procedureNo_id = Convert.ToInt32(ddlMethod.SelectedValue);
                        _cover.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        _cover.temperature_humidity_parameters_id = Convert.ToInt32(ddlTemp.SelectedValue);
                        _cover.number_of_pieces_used_for_extraction = txtNumberOfPiecesUsedForExtraction.Text;
                        _cover.procedureNo = txtProcedureNo.Text;
                    }

                    template_wd_corrosion_coverpage.UpdateList(this.coverpages);
                    template_wd_corrosion_img.DeleteBySampleID(this.SampleID);
                    template_wd_corrosion_img.InsertList(this.refImg);

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
                        //isValid = false;
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

        protected void ddlMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProcedureNo.Text = ddlMethod.SelectedItem.Text;
        }

        protected void ddlTemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.coverpages.Count > 0)
            {

                this.coverpages[0].temperature_humidity_parameters = ddlTemp.SelectedItem.Text;
                this.coverpages[0].specification = "No observable discoloration or spots at 40x";
                this.coverpages[0].result = "";
                gvResult.DataSource = this.coverpages;
                gvResult.DataBind();
            }
        }
        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbResultDesc.Text = String.Format("The specification is based on Western Digital 's document no. {0}", ddlSpecification.SelectedItem.Text);
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

            HiddenField _hResult = (HiddenField)gvResult.Rows[e.NewEditIndex].FindControl("hResult");
            DropDownList _ddlResult = (DropDownList)gvResult.Rows[e.NewEditIndex].FindControl("ddlResult");
            if (_ddlResult != null)
            {
                _ddlResult.Items.Clear();
                _ddlResult.Items.Add(new ListItem("Pass", "Pass"));
                _ddlResult.Items.Add(new ListItem("Fail", "Fail"));
            }
            _ddlResult.SelectedValue = _hResult.Value;



            //tb_m_specification comp = new tb_m_specification();
            //comp.specification_id = this.jobSample.specification_id;
            //comp.template_id = this.jobSample.template_id;

            //HiddenField _temperature_humidity_parameters_id = (HiddenField)gvResult.Rows[e.NewEditIndex].FindControl("temperature_humidity_parameters_id");
            //DropDownList _ddlhTemperature_humidity_parameters = (DropDownList)gvResult.Rows[e.NewEditIndex].FindControl("ddlhTemperature_humidity_parameters");
            //if (_ddlhTemperature_humidity_parameters != null)
            //{
            //    _ddlhTemperature_humidity_parameters.DataSource = comp.SelectAll();
            //    _ddlhTemperature_humidity_parameters.DataBind();
            //}
            //_ddlhTemperature_humidity_parameters.SelectedValue = _temperature_humidity_parameters_id.Value;
        }

        protected void gvResult_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvResult.EditIndex = -1;
            gvResult.DataSource = this.coverpages;
            gvResult.DataBind();
        }

        protected void gvResult_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int _id = Convert.ToInt32(gvResult.DataKeys[e.RowIndex].Values[0].ToString());
            DropDownList _ddlResult = (DropDownList)gvResult.Rows[e.RowIndex].FindControl("ddlResult");
            //DropDownList _ddlhTemperature_humidity_parameters = (DropDownList)gvResult.Rows[e.RowIndex].FindControl("ddlhTemperature_humidity_parameters");

            //TextBox _txtSpecification = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtSpecification");


            if (_ddlResult != null)
            {
                template_wd_corrosion_coverpage _tmp = this.coverpages.Find(x => x.ID == _id);
                if (_tmp != null)
                {
                    _tmp.result = _ddlResult.SelectedValue;
                    //_tmp.temperature_humidity_parameters_id = Convert.ToInt32(_ddlhTemperature_humidity_parameters.SelectedValue);
                    //_tmp.specification = _txtSpecification.Text;
                    //_tmp.temperature_humidity_parameters = _ddlhTemperature_humidity_parameters.SelectedItem.Text;
                    //

                }
            }

            gvResult.EditIndex = -1;
            gvResult.DataSource = this.coverpages;
            gvResult.DataBind();

        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvRefImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_wd_corrosion_img _mesa = this.refImg.Find(x => x.id == PKID);
                if (_mesa != null)
                {
                    switch (cmd)
                    {
                        case CommandNameEnum.Delete:
                            this.refImg.Remove(_mesa);
                            break;

                    }
                    gvRefImages.DataSource = this.refImg;
                    gvRefImages.DataBind();
                }
            }
        }

        protected void gvRefImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvRefImages_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvRefImages.EditIndex = e.NewEditIndex;
            gvRefImages.DataSource = this.refImg;
            gvRefImages.DataBind();

            HiddenField _hImgType = (HiddenField)gvRefImages.Rows[e.NewEditIndex].FindControl("hImgType");
            DropDownList _ddlImgType = (DropDownList)gvRefImages.Rows[e.NewEditIndex].FindControl("ddlImgType");
            if (_ddlImgType != null)
            {
                _ddlImgType.Items.Clear();
                _ddlImgType.Items.Add(new ListItem("Photo Before", "1"));
                _ddlImgType.Items.Add(new ListItem("Photo After", "2"));
            }
            _ddlImgType.SelectedValue = _hImgType.Value;

        }

        protected void gvRefImages_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRefImages.EditIndex = -1;
            gvRefImages.DataSource = this.refImg;
            gvRefImages.DataBind();
        }

        protected void gvRefImages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField _hImgType = (HiddenField)e.Row.FindControl("hImgType");
                Literal _litImgType = (Literal)e.Row.FindControl("litImgType");
                if (!String.IsNullOrEmpty(_hImgType.Value) && _litImgType != null)
                {
                    IMAGE_ORDER_TYPE imgOrderType = (IMAGE_ORDER_TYPE)Enum.ToObject(typeof(IMAGE_ORDER_TYPE), Convert.ToInt32(_hImgType.Value));
                    _litImgType.Text = imgOrderType.ToString();
                }
            }
        }
        protected void gvRefImages_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvRefImages.DataKeys[e.RowIndex].Values[0].ToString());
            DropDownList _ddlImgType = (DropDownList)gvRefImages.Rows[e.RowIndex].FindControl("ddlImgType");
            if (_ddlImgType != null)
            {
                template_wd_corrosion_img _tmp = this.refImg.Find(x => x.id == _id);
                if (_tmp != null)
                {
                    _tmp.img_type = Convert.ToInt32(_ddlImgType.SelectedValue);
                }
            }

            gvRefImages.EditIndex = -1;
            gvRefImages.DataSource = this.refImg;
            gvRefImages.DataBind();
        }
        #endregion

        protected void lbDownload_Click(object sender, EventArgs e)
        {

            tb_m_specification component = new tb_m_specification().SelectByID(this.coverpages[0].specification_id.Value);
            DataTable dtHeader = new DataTable("MethodProcedure");

            // Define all the columns once.
            DataColumn[] cols ={ new DataColumn("ProcedureNo",typeof(String)),
                                  new DataColumn("NumOfPiecesUsedForExtraction",typeof(String)),
                              };
            dtHeader.Columns.AddRange(cols);
            DataRow row = dtHeader.NewRow();
            row["ProcedureNo"] = txtProcedureNo.Text;
            row["NumOfPiecesUsedForExtraction"] = this.coverpages[0].number_of_pieces_used_for_extraction;
            dtHeader.Rows.Add(row);

            DataTable dtResult = new DataTable("Result");
            DataColumn[] cols1 ={ new DataColumn("B",typeof(String)),
                                  new DataColumn("C",typeof(String)),
                                  new DataColumn("result",typeof(String))
                              };
            dtResult.Columns.AddRange(cols1);
            DataRow row1 = dtResult.NewRow();
            row1["B"] = ddlTemp.SelectedItem.Text;
            row1["C"] = component.D;
            row1["result"] = this.coverpages[0].result;
            dtResult.Rows.Add(row1);


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
            reportParameters.Add(new ReportParameter("Test", "-"));
            reportParameters.Add(new ReportParameter("ResultDesc", lbResultDesc.Text));
            reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.jobSample.singapore_ref_no) ? String.Empty : this.jobSample.singapore_ref_no)));

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            List<template_wd_corrosion_img> dat = new List<template_wd_corrosion_img>();
            template_wd_corrosion_img tmp = new template_wd_corrosion_img();
            template_wd_corrosion_img corImg = this.refImg.Where(x => x.img_type.Value == 1).FirstOrDefault();
            if (corImg != null)
            {
                tmp.img1 = CustomUtils.GetBytesFromImage(corImg.path_img1);
            }
            corImg = this.refImg.Where(x => x.img_type.Value == 2).FirstOrDefault();
            if (corImg != null)
            {
                tmp.img2 = CustomUtils.GetBytesFromImage(corImg.path_img1);
            }
            dat.Add(tmp);
            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/corrosion_wd.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtHeader)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dtResult)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dat.ToDataTable())); // Add datasource here



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



        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < FileUpload1.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = FileUpload1.PostedFiles[i];
                if ((Path.GetExtension(_postedFile.FileName).Equals(".jpg")))
                {
                    string yyyy = DateTime.Now.ToString("yyyy");
                    string MM = DateTime.Now.ToString("MM");
                    string dd = DateTime.Now.ToString("dd");

                    String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName));
                    String source_file_url = String.Concat(Configurations.HOST, String.Format(Configurations.PATH_URL, yyyy, MM, dd, this.jobSample.job_number, Path.GetFileName(_postedFile.FileName)));


                    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                    }
                    _postedFile.SaveAs(source_file);
                    template_wd_corrosion_img _img = new template_wd_corrosion_img();
                    _img.id = CustomUtils.GetRandomNumberID();
                    _img.sample_id = this.SampleID;
                    _img.img_type = i + 1;
                    _img.path_img1 = source_file_url;
                    this.refImg.Add(_img);
                }
            }

            gvRefImages.DataSource = this.refImg;
            gvRefImages.DataBind();

        }


        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbResultDesc.Text = String.Format("This sample is no {0} specification reference", "WD");
            }
            else
            {

                lbResultDesc.Text = String.Format("The specification is based on Western Digital's document no. {0}", ddlMethod.SelectedItem.Text);

            }


        }




    }
}