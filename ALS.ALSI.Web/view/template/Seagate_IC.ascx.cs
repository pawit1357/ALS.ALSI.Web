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
using NPOI.XSSF.UserModel;
using Spire.Doc;
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
using WordToPDF;

namespace ALS.ALSI.Web.view.template
{
    public partial class Seagate_IC : System.Web.UI.UserControl
    {

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public int JobID
        {
            get { return (int)Session[GetType().Name + "JobID"]; }
            set { Session[GetType().Name + "JobID"] = value; }
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

        public List<template_seagate_ic_coverpage> coverpages
        {
            get { return (List<template_seagate_ic_coverpage>)Session[GetType().Name + "template_seagate_ic_coverpage"]; }
            set { Session[GetType().Name + "template_seagate_ic_coverpage"] = value; }
        }
        List<String> errors = new List<string>();

        private void initialPage()
        {
            this.CommandName = CommandNameEnum.Add;


            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = new tb_m_specification().SelectBySpecificationID(this.jobSample.specification_id, this.jobSample.template_id);
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

            tb_unit unit = new tb_unit();
            ddlUnit.Items.Clear();
            ddlUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("IC")).ToList();
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


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


            }
            #endregion

            #region "WORKING"
            this.coverpages = template_seagate_ic_coverpage.FindAllBySampleID(this.SampleID);
            if (this.coverpages != null && this.coverpages.Count > 0)
            {
                this.CommandName = CommandNameEnum.Edit;
                template_seagate_ic_coverpage ic = this.coverpages[0];
                ddlSpecification.SelectedValue = ic.specification_id.ToString();

                tb_m_specification spec = new tb_m_specification().SelectByID(Convert.ToInt32(ic.specification_id));
                if (spec != null)
                {
                    /*METHOD/PROCEDURE:*/

                    txtB18.Text = ic.procedure_no;
                    txtC18.Text = (String.IsNullOrEmpty(ic.number_of_pieces__used_for_extraction) ? String.Empty : ic.number_of_pieces__used_for_extraction);//"Number of pieces used for extraction"
                    txtD18.Text = ic.extraction_medium;
                    txtE18.Text = (String.IsNullOrEmpty(ic.extraction_volume) ? String.Empty : String.Format("{0}ml", (1000 * Convert.ToDecimal(ic.extraction_volume))));//"Number of pieces used for extraction"

                    ///*RESULT*/
                    ///
                    cbCheckBox.Checked = (this.jobSample.is_no_spec == null) ? false : this.jobSample.is_no_spec.Equals("1") ? true : false;
                    if (cbCheckBox.Checked)
                    {
                        lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
                    }
                    else
                    {
                        lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", spec.B, spec.A);

                    }

                    #region "Unit"
                    if (ic.wunit != null)
                    {
                        ddlUnit.SelectedValue = ic.wunit.Value.ToString();
                    }
                    if (ic.wunit != null)
                    {
                        ddlUnit.SelectedValue = ic.wunit.Value.ToString();

                        gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));

                        gvResultAnions.Columns[1].HeaderText = String.Format("Conc of water Blankµg/ L(B)", ddlUnit.SelectedItem.Text);
                        gvResultAnions.Columns[2].HeaderText = String.Format("Conc of Sample µg/ L(C)", ddlUnit.SelectedItem.Text);
                        gvResultAnions.Columns[4].HeaderText = String.Format("Raw Results {0}", ddlUnit.SelectedItem.Text);
                        gvResultAnions.Columns[6].HeaderText = String.Format("Method Detection Limit  {0}", ddlUnit.SelectedItem.Text);
                        gvResultAnions.Columns[5].HeaderText = String.Format("Instrument Detection Limit  (ug/L)", "");
                        gvResultAnions.Columns[8].HeaderText = String.Format("Final Conc. of Sample  {0}", ddlUnit.SelectedItem.Text);

                        gvResultCations.Columns[1].HeaderText = String.Format("Conc of water Blankµg/ L(B)", ddlUnit.SelectedItem.Text);
                        gvResultCations.Columns[2].HeaderText = String.Format("Conc of Sample µg/ L(C)", ddlUnit.SelectedItem.Text);
                        gvResultCations.Columns[4].HeaderText = String.Format("Raw Results {0}", ddlUnit.SelectedItem.Text);
                        gvResultCations.Columns[6].HeaderText = String.Format("Method Detection Limit  {0}", ddlUnit.SelectedItem.Text);
                        gvResultCations.Columns[5].HeaderText = String.Format("Instrument Detection Limit  (ug/L)", "");
                        gvResultCations.Columns[8].HeaderText = String.Format("Final Conc. of Sample  {0}", ddlUnit.SelectedItem.Text);

                    }
                    else
                    {
                        gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                        gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
                    }
                    #endregion

                    //Working Sheet-IC
                    txtB9.Text = String.IsNullOrEmpty(ic.extraction_volume) ? String.Empty : ic.extraction_volume;
                    txtB10.Text = String.IsNullOrEmpty(ic.b10) ? String.Empty : ic.b10;
                    txtB11.Text = String.IsNullOrEmpty(ic.number_of_pieces__used_for_extraction) ? String.Empty : ic.number_of_pieces__used_for_extraction;

                    calculateByFormular();
                }

                gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
                gvAnionic.DataBind();
                gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
                gvCationic.DataBind();

                gvResultAnions.DataSource = this.coverpages.Where(x => x.ic_type.Value == 1).ToList();
                gvResultAnions.DataBind();
                gvResultCations.DataSource = this.coverpages.Where(x => x.ic_type.Value == 2).ToList();
                gvResultCations.DataBind();

            }
            else
            {
                this.coverpages = new List<template_seagate_ic_coverpage>();
            }


            //Show Method/Procedure
            if (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST))
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


                txtB18.Enabled = true;
                txtC18.Enabled = false;
                txtD18.Enabled = true;
                txtD18.Enabled = true;
                txtE18.Enabled = false;
                gvAnionic.Columns[3].Visible = true;
                gvCationic.Columns[3].Visible = true;
                btnCoverPage.Visible = true;
                btnWorking.Visible = true;
                pLoadFile.Visible = true;
            }
            else
            {

                txtB18.Enabled = false;
                txtC18.Enabled = false;
                txtD18.Enabled = false;
                txtD18.Enabled = false;
                txtE18.Enabled = false;
                gvAnionic.Columns[3].Visible = false;
                gvCationic.Columns[3].Visible = false;
                btnCoverPage.Visible = false;
                btnWorking.Visible = false;
                pLoadFile.Visible = false;
                if (userLogin.role_id == Convert.ToInt32(RoleEnum.SR_CHEMIST))
                {
                    btnCoverPage.Visible = true;
                    btnWorking.Visible = true;
                }

            }

            #endregion

            //Disable Save button
            btnCoverPage.CssClass = "btn blue";
            btnWorking.CssClass = "btn green";
            pCoverpage.Visible = true;
            pWorkingIC.Visible = false;



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
            //calculateByFormular();
            template_seagate_ic_coverpage objWork = new template_seagate_ic_coverpage();

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
                    foreach (template_seagate_ic_coverpage _cover in this.coverpages)
                    {
                        _cover.procedure_no = txtB18.Text;
                        _cover.number_of_pieces__used_for_extraction = txtB11.Text;
                        _cover.extraction_medium = txtD18.Text;
                        _cover.extraction_volume = txtB9.Text;
                        _cover.sample_id = this.SampleID;
                        _cover.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        _cover.b10 = txtB10.Text;
                    }
                    switch (this.CommandName)
                    {
                        case CommandNameEnum.Add:
                            template_seagate_ic_coverpage.InsertList(this.coverpages);
                            break;
                        case CommandNameEnum.Edit:
                            template_seagate_ic_coverpage.UpdateList(this.coverpages);
                            break;
                    }
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    //#endregion
                    foreach (template_seagate_ic_coverpage _cover in this.coverpages)
                    {
                        _cover.procedure_no = txtB18.Text;
                        _cover.number_of_pieces__used_for_extraction = txtB11.Text;
                        _cover.extraction_medium = txtD18.Text;
                        _cover.extraction_volume = txtB9.Text;
                        _cover.sample_id = this.SampleID;
                        _cover.specification_id = Convert.ToInt32(ddlSpecification.SelectedValue);
                        _cover.b10 = txtB10.Text;
                        _cover.wunit = Convert.ToInt32(ddlUnit.SelectedValue);
                    }
                    template_seagate_ic_coverpage.UpdateList(this.coverpages);
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
                this.jobSample.Update();
                //Commit
                GeneralManager.Commit();

                //removeSession();
                MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
            }



        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.PreviousPath);
        }

        private void calculateByFormular()
        {
            gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));

            gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
            gvAnionic.DataBind();
            gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
            gvCationic.DataBind();

        }

        protected void btnCoverPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnCoverPage.CssClass = "btn green";
                    btnWorking.CssClass = "btn blue";
                    pCoverpage.Visible = true;
                    pWorkingIC.Visible = false;

                    txtC18.Text = txtB11.Text;
                    txtE18.Text = (String.IsNullOrEmpty(txtB9.Text) ? String.Empty : String.Format("{0}ml", (1000 * Convert.ToDecimal(txtB9.Text))));//"Number of pieces used for extraction"

                    calculateByFormular();
                    break;
                case "Workingpg-IC":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorking.CssClass = "btn green";
                    pCoverpage.Visible = false;
                    pWorkingIC.Visible = true;
                    break;
            }
        }

        protected void gvAnionic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PKID = Convert.ToInt32(gvAnionic.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvAnionic.DataKeys[e.Row.RowIndex].Values[1]);
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
        protected void gvCationic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int PKID = Convert.ToInt32(gvCationic.DataKeys[e.Row.RowIndex].Values[0].ToString());
                RowTypeEnum cmd = (RowTypeEnum)Enum.ToObject(typeof(RowTypeEnum), (int)gvCationic.DataKeys[e.Row.RowIndex].Values[1]);
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

        protected void gvAnionic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_ic_coverpage gcms = this.coverpages.Find(x => x.id == PKID);
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
                    gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
                    gvAnionic.DataBind();
                }
            }
        }
        protected void gvCationic_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowTypeEnum cmd = (RowTypeEnum)Enum.Parse(typeof(RowTypeEnum), e.CommandName, true);
            if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                template_seagate_ic_coverpage gcms = this.coverpages.Find(x => x.id == PKID);
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
                    gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
                    gvCationic.DataBind();
                }
            }
        }

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_m_specification tem = new tb_m_specification().SelectByID(int.Parse(ddlSpecification.SelectedValue));
            if (tem != null)
            {

                tb_m_specification mSpec = new tb_m_specification();
                mSpec.template_id = tem.template_id;
                List<tb_m_specification> specs = mSpec.SelectBySpecificationID(tem.specification_id.Value, tem.template_id.Value);

                /*RESULT*/
                lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", tem.B, tem.A);
                //lbDocRev.Text = tem.B;
                //lbDesc.Text = tem.A;
                if (specs.Count > 0)
                {
                    txtB18.Text = specs[1].B;
                }

                gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", tem.C);
                gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", tem.C);

                gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", tem.C);
                gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", tem.C);

                List<template_seagate_ic_coverpage> listCover = new List<template_seagate_ic_coverpage>();

                #region "*Anionic*SO4,Br,F,Cl,NO3,NO2,PO4"


                template_seagate_ic_coverpage _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 1;
                _tmp.A = "Fluoride as F";
                _tmp.B = tem.F;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 2;
                _tmp.A = "Chloride as Cl";
                _tmp.B = tem.G;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 3;
                _tmp.A = "Nitrite as NO2";
                _tmp.B = tem.I;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 4;
                _tmp.A = "Bromide as Br";
                _tmp.B = tem.E;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 5;
                _tmp.A = "Nitrate as NO3";
                _tmp.B = tem.H;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 6;
                _tmp.A = "Sulfate as SO4";
                _tmp.B = tem.D;
                _tmp.wunitText = tem.C;

                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 7;
                _tmp.A = "Phosphate as PO4";
                _tmp.B = tem.J;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);

                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 8;
                _tmp.A = "Total of 7Anions";
                _tmp.B = tem.K;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.ANIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                #endregion

                #region "*Cationic*NH4,Li,Ca,K,Na,Mg"
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 9;
                _tmp.A = "Lithium as Li";
                _tmp.B = tem.M;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 10;
                _tmp.A = "Sodium as Na";
                _tmp.B = tem.P;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 11;
                _tmp.A = "Ammonium as NH4";
                _tmp.B = tem.L;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 12;
                _tmp.A = "Potassium as K";
                _tmp.B = tem.O;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 11;
                _tmp.A = "Magnesium as Mg";
                _tmp.B = tem.Q;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 13;
                _tmp.A = "Calcium as Ca";
                _tmp.B = tem.N;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                _tmp = new template_seagate_ic_coverpage();
                _tmp.id = 14;
                _tmp.A = "Total Cations";
                _tmp.B = tem.R;
                _tmp.wunitText = tem.C;
                _tmp.ic_type = Convert.ToInt32(ICTypeEnum.CATIONIC);
                _tmp.row_type = Convert.ToInt32(RowTypeEnum.Normal);
                listCover.Add(_tmp);
                #endregion
                this.coverpages = listCover;
                gvAnionic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC));
                gvAnionic.DataBind();
                gvCationic.DataSource = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC));
                gvCationic.DataBind();
                btnSubmit.Enabled = true;
            }
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {


            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);
            List<template_seagate_ic_coverpage> anionic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            List<template_seagate_ic_coverpage> cationic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
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
            reportParameters.Add(new ReportParameter("Test", "IC"));
            reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));

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
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ic_seagate.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", anionic.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", cationic.ToDataTable())); // Add datasource here






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


            DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);
            List<template_seagate_ic_coverpage> anionic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            List<template_seagate_ic_coverpage> cationic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
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
            reportParameters.Add(new ReportParameter("Test", "IC"));
            reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));

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
            viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ic_seagate_pdf.rdlc");
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", anionic.ToDataTable())); // Add datasource here
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", cationic.ToDataTable())); // Add datasource here


            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download



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

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            string sheetName = string.Empty;


            List<template_seagate_ic_coverpage> ics = new List<template_seagate_ic_coverpage>();


            for (int i = 0; i < FileUpload1.PostedFiles.Count; i++)
            {
                HttpPostedFile _postedFile = FileUpload1.PostedFiles[i];
                try
                {
                    if ((Path.GetExtension(_postedFile.FileName).Equals(".xls")))
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
                            HSSFWorkbook wb = new HSSFWorkbook(fs);
                            ISheet isheet = wb.GetSheet(ConfigurationManager.AppSettings["seagate.ic.excel.sheetname.working1"]);
                            if (isheet == null)
                            {
                                errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", ConfigurationManager.AppSettings["wd.ic.excel.sheetname.working1"]));
                            }
                            else
                            {
                                sheetName = isheet.SheetName;


                                txtB9.Text = CustomUtils.GetCellValue(isheet.GetRow(9 - 1).GetCell(ExcelColumn.B));
                                txtB10.Text = CustomUtils.GetCellValue(isheet.GetRow(10 - 1).GetCell(ExcelColumn.B));
                                txtB11.Text = CustomUtils.GetCellValue(isheet.GetRow(11 - 1).GetCell(ExcelColumn.B));
                                int lpc_type = 1;

                                foreach (template_seagate_ic_coverpage ic in this.coverpages)
                                {

                                    for (int row = 13; row < isheet.LastRowNum; row++)
                                    {
                                        if (isheet.GetRow(row) != null)
                                        {
                                            String data_type = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.A));

                                            if (ic.A.Equals(mappingRawData(data_type)))
                                            {
                                                ic.wb = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.B));
                                                ic.wc = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.C));
                                                ic.wd = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.D));
                                                ic.we = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.E));
                                                ic.wf = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.F));
                                                ic.wg = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.G));
                                                ic.wh = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.H));
                                                ic.wi = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.I));
                                                ic.wj = CustomUtils.GetCellValue(isheet.GetRow(row).GetCell(ExcelColumn.J));

                                                ic.wb = (String.IsNullOrEmpty(ic.wb)) ? "" : Convert.ToDouble(ic.wb).ToString("N" + txtDecimal01.Text);
                                                ic.wc = (String.IsNullOrEmpty(ic.wc)) ? "" : Convert.ToDouble(ic.wc).ToString("N" + txtDecimal02.Text);
                                                ic.wd = (String.IsNullOrEmpty(ic.wd)) ? "" : Convert.ToDouble(ic.wd).ToString("N" + txtDecimal03.Text);
                                                ic.we = (String.IsNullOrEmpty(ic.we)) ? "" : Convert.ToDouble(ic.we).ToString("N" + txtDecimal04.Text);
                                                ic.wf = (String.IsNullOrEmpty(ic.wf)) ? "" : Convert.ToDouble(ic.wf).ToString("N" + txtDecimal05.Text);
                                                ic.wg = (String.IsNullOrEmpty(ic.wg)) ? "" : Convert.ToDouble(ic.wg).ToString("N" + txtDecimal06.Text);
                                                ic.wh = (String.IsNullOrEmpty(ic.wh)) ? "" : Convert.ToDouble(ic.wh).ToString("N" + txtDecimal07.Text);

                                                ic.wi = (String.IsNullOrEmpty(ic.wi)) ? "" : (!ic.wi.StartsWith("<") ? "" : "<") + Convert.ToDouble(ic.wi.Replace("<", "").Trim()).ToString("N" + txtDecimal08.Text);
                                                Console.WriteLine();
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
                }
                catch (Exception ex)
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
                gvResultAnions.DataSource = this.coverpages.Where(x => x.ic_type.Value == 1).ToList();
                gvResultAnions.DataBind();
                gvResultCations.DataSource = this.coverpages.Where(x => x.ic_type.Value == 2).ToList();
                gvResultCations.DataBind();
            }


        }


        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

            gvAnionic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvAnionic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvAnionic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvCationic.Columns[1].HeaderText = String.Format("Specification Limits, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvCationic.Columns[2].HeaderText = String.Format("Results, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));
            gvCationic.Columns[3].HeaderText = String.Format("Method Detection Limit, ({0})", ddlUnit.SelectedItem.Text);// getUnitText(this.coverpages[0].wunit));


            gvResultAnions.Columns[4].HeaderText = String.Format("Raw Results {0}", ddlUnit.SelectedItem.Text);
            gvResultAnions.Columns[6].HeaderText = String.Format("Method Detection Limit  {0}", ddlUnit.SelectedItem.Text);
            gvResultAnions.Columns[8].HeaderText = String.Format("Final Conc. of Sample  {0}", ddlUnit.SelectedItem.Text);

            gvResultCations.Columns[4].HeaderText = String.Format("Raw Results {0}", ddlUnit.SelectedItem.Text);
            gvResultCations.Columns[6].HeaderText = String.Format("Method Detection Limit  {0}", ddlUnit.SelectedItem.Text);
            gvResultCations.Columns[8].HeaderText = String.Format("Final Conc. of Sample  {0}", ddlUnit.SelectedItem.Text);
            ModolPopupExtender.Show();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        private String mappingRawData(String _val)
        {
            String result = _val;
            Hashtable mappingValues = new Hashtable();

            mappingValues["Fluoride, F"] = "Fluoride as F";
            mappingValues["Chloride, Cl"] = "Chloride as Cl";
            mappingValues["Nitrite as NO2"] = "Nitrite as NO2";
            mappingValues["Bromide, Br"] = "Bromide as Br";
            mappingValues["Nitrate, NO3"] = "Nitrate as NO3";
            mappingValues["Sulfate, SO4"] = "Sulfate as SO4";
            mappingValues["Phosphate, PO4"] = "Phosphate as PO4";
            mappingValues["Lithium, Li"] = "Lithium as Li";
            mappingValues["Sodium, Na"] = "Sodium as Na";
            mappingValues["Ammonium, NH4"] = "Ammonium as NH4";
            mappingValues["Potassium, K"] = "Potassium as K";
            mappingValues["Magnesium, Mg"] = "Magnesium as Mg";
            mappingValues["Calcium, Ca"] = "Calcium as Ca";


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

        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCheckBox.Checked)
            {
                lbSpecDesc.Text = String.Format("This sample is no {0} specification reference", "Seagate");
            }
            else
            {
                tb_m_specification tem = new tb_m_specification().SelectByID(int.Parse(ddlSpecification.SelectedValue));
                if (tem != null)
                {
                    lbSpecDesc.Text = String.Format("The Specification is based on Seagate's Doc {0} {1}", tem.B, tem.A);
                }
            }
        }
    }
}