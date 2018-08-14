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
using ALS.ALSI.Biz.ReportObjects;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using Spire.Doc;
using System.Text.RegularExpressions;
using System.Collections;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ALS.ALSI.Web.view.template
{
    public partial class FT_IC : System.Web.UI.UserControl
    {

        #region "Property"

        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
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

        //public List<sample_method_procedure> ListSampleMethodProcedure
        //{
        //    get { return (List<sample_method_procedure>)Session[GetType().Name + "sample_method_procedure"]; }
        //    set { Session[GetType().Name + "sample_method_procedure"] = value; }
        //}
        public List<template_f_ic> ics
        {
            get { return (List<template_f_ic>)Session[GetType().Name + "ics"]; }
            set { Session[GetType().Name + "ics"] = value; }
        }

        public Hashtable configs
        {
            get { return (Hashtable)Session[GetType().Name + "configs"]; }
            set { Session[GetType().Name + "configs"] = value; }
        }
        public Hashtable workSheetData
        {
            get { return (Hashtable)Session[GetType().Name + "workSheetData"]; }
            set { Session[GetType().Name + "workSheetData"] = value; }
        }
        public List<tb_m_specification> listSpecificatons
        {
            get { return (List<tb_m_specification>)Session[GetType().Name + "listSpecificatons"]; }
            set { Session[GetType().Name + "listSpecificatons"] = value; }
        }

        private String SheetSpecification = "Specification";
        private String SheetCoverPageName = "Coverpage-TH";
        private String SheetWorkSheetName = "IC";


        //public ExcelWorksheet wSpecification
        //{
        //    get { return (ExcelWorksheet)Session[GetType().Name + "wSpecification"]; }
        //    set { Session[GetType().Name + "wSpecification"] = value; }
        //}
        //public ExcelWorksheet wCoverPage
        //{
        //    get { return (ExcelWorksheet)Session[GetType().Name + "wCoverPage"]; }
        //    set { Session[GetType().Name + "wCoverPage"] = value; }
        //}

        private void initialPage()
        {

            m_template template = new m_template().SelectByID(this.jobSample.template_id);
            this.ics = new template_f_ic().SelectBySampleID(this.SampleID);
            if (this.ics == null)
            {
                this.ics = new List<template_f_ic>();
            }

            FileInfo fileInfo = new FileInfo(template.path_source_file);
            using (var package = new ExcelPackage(fileInfo))
            {
                FreeTemplateUtil ftu = new FreeTemplateUtil(fileInfo);
                this.configs = ftu.getConfigValue();
                this.listSpecificatons = ftu.getSpecification();
                ExcelWorksheet wCoverPage = package.Workbook.Worksheets["Coverpage-TH"];
                ExcelWorksheet wSpecification = package.Workbook.Worksheets["Specification"];

                #region "SHOW COMPONENT"

                ddlComponent.Items.Clear();
                ddlComponent.DataSource = listSpecificatons;
                ddlComponent.DataBind();
                ddlComponent.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

                ddlAssignTo.Items.Clear();
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));


                tb_unit unit = new tb_unit();
                ddlUnit.Items.Clear();
                ddlUnit.DataSource = unit.SelectAll().Where(x => x.unit_group.Equals("IC")).ToList();
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));



                StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);

                switch (userRole)
                {
                    case RoleEnum.LOGIN:
                        #region "METHOD/PROCEDURE"
                        //HEADER
                        String[] headers = this.configs["method.procedure.data.header"].ToString().Split('|')[1].Split(',');
                        int hc = 0;
                        template_f_ic hIc = new template_f_ic();

                        foreach (String value in headers)
                        {
                            hIc.id = CustomUtils.GetRandomNumberID();
                            setValueToTemplate(ref hIc, hc, value);
                            hIc.row_type = Convert.ToInt16(FreeTemplateIcEnum.METHOD_PROCECURE_HEADER);
                            hIc.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                            hc++;
                        }
                        this.ics.Add(hIc);

                        //DETAIL ITEM
                        String[] itemDetails = this.configs["method.procedure.data.ranges"].ToString().Split('|')[1].Split(':');

                        int colBegin = FreeTemplateUtil.GetColIndex(itemDetails[0]);
                        int colEnd = FreeTemplateUtil.GetColIndex(itemDetails[1]);
                        int rowBegin = FreeTemplateUtil.GetRowIndex(itemDetails[0]);
                        int rowEnd = FreeTemplateUtil.GetRowIndex(itemDetails[1]);
                        for (int r = rowBegin; r <= rowEnd; r++)
                        {
                            template_f_ic ic = new template_f_ic();
                            ic.id = CustomUtils.GetRandomNumberID();
                            int colIndex = 1;
                            for (int c = colBegin; c <= colEnd; c++)
                            {
                                try
                                {
                                    String type = this.configs[String.Format("method.procedure.type.col.{0}", colIndex)].ToString().Split('|')[0];

                                    String value = "";
                                    switch (type)
                                    {
                                        case "text":
                                            value = wCoverPage.Cells[String.Format("{0}{1}", FreeTemplateUtil.GetColName(c), (r + 1))].Text;
                                            break;
                                        case "formula":
                                            //String detail = this.configs[String.Format("method.procedure.data.type.col.{0}", colIndex)].ToString().Split('|')[1];
                                            //String _key = String.Format("{0}{1}", detail.Split(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK)[1], ddlComponent.SelectedValue);
                                            //value = wSpecification.Cells[_key].Value.ToString();
                                            //value = Regex.IsMatch(value, @"^\d+$") ? String.Format("< {0}", value) : value;
                                            Console.WriteLine();
                                            break;
                                    }
                                    setValueToTemplate(ref ic, c, value);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine();
                                }
                                colIndex++;
                            }
                            ic.row_type = Convert.ToInt16(FreeTemplateIcEnum.METHOD_PROCECURE);
                            ic.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                            this.ics.Add(ic);
                        }
                        #endregion
                        break;
                    case RoleEnum.CHEMIST:
                        ddlComponent.SelectedValue = this.ics[0].specification_id.ToString();
                        ddlUnit.SelectedValue = this.ics[0].unit.ToString();
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
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_APPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_APPROVE) + ""));
                            ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_DISAPPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_DISAPPROVE) + ""));
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




                //Disable Save button
                btnCoverPage.CssClass = "btn blue";
                btnWorking.CssClass = "btn green";
                btnCoverPage.Visible = true;
                btnWorking.Visible = true;


                pCoverpage.Visible = true;
                pWorkingIC.Visible = false;
                pLoadFile.Visible = (status == StatusEnum.CHEMIST_TESTING || userLogin.role_id == Convert.ToInt32(RoleEnum.CHEMIST));
                pRemark.Visible = false;
                pDisapprove.Visible = false;
                pSpecification.Visible = (status == StatusEnum.LOGIN_SELECT_SPEC);
                pStatus.Visible = (status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.LABMANAGER_CHECKING);
                pUploadfile.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD);
                pDownload.Visible = (status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);
                pAnalyzeDate.Visible = (status == StatusEnum.CHEMIST_TESTING);
                btnSubmit.Visible = (status == StatusEnum.LOGIN_SELECT_SPEC || status == StatusEnum.CHEMIST_TESTING || status == StatusEnum.SR_CHEMIST_CHECKING || status == StatusEnum.ADMIN_CONVERT_PDF || status == StatusEnum.ADMIN_CONVERT_WORD || status == StatusEnum.LABMANAGER_CHECKING);


                switch (lbJobStatus.Text)
                {
                    case "CONVERT_PDF":
                        litDownloadIcon.Text = "<i class=\"fa fa-file-pdf-o\"></i>";
                        break;
                    default:
                        litDownloadIcon.Text = "<i class=\"fa fa-file-word-o\"></i>";
                        break;
                }
                #endregion

                #region "remark"
                if (this.configs["remark.item.1"] != null)
                {
                    txtRemark1.Text = this.configs["remark.item.1"].ToString().Split('|')[1];
                }
                else
                {
                    txtRemark1.Visible = false;
                }
                if (this.configs["remark.item.2"] != null)
                {
                    txtRemark2.Text = this.configs["remark.item.2"].ToString().Split('|')[1];
                }
                else
                {
                    txtRemark2.Visible = false;
                }
                if (this.configs["remark.item.3"] != null)
                {
                    txtRemark3.Text = this.configs["remark.item.3"].ToString().Split('|')[1];
                }
                else
                {
                    txtRemark3.Visible = false;
                }
                if (this.configs["remark.item.4"] != null)
                {
                    txtRemark4.Text = this.configs["remark.item.4"].ToString().Split('|')[1];
                }
                else
                {
                    txtRemark4.Visible = false;
                }
                if (this.configs["remark.item.5"] != null)
                {
                    txtRemark5.Text = this.configs["remark.item.5"].ToString().Split('|')[1];
                }
                else
                {
                    txtRemark5.Visible = false;
                }

                #endregion
                #region "result"
                if (Convert.ToInt32(ddlComponent.SelectedValue) > 0)
                {
                    String param1 = this.configs["result.desc.param.1"].ToString().Split('|')[1];
                    String param2 = this.configs["result.desc.param.2"].ToString().Split('|')[1];
                    String _param1 = wSpecification.Cells[param1].Value.ToString();
                    String _param2 = wSpecification.Cells[String.Format(param2, ddlComponent.SelectedValue)].Value.ToString();
                    lbSpecDesc.Text = String.Format(this.configs["result.desc"].ToString().Split('|')[1], _param1, _param2);
                }
                #endregion
                Cal();//
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
                //this.ListSampleMethodProcedure = new List<sample_method_procedure>();
                initialPage();
            }
        }

        #region "METHOD/PROCEDURE GRIDVIEW (EVENT)"

        protected void gvMethodProcedure_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_f_ic _ic = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.METHOD_PROCECURE) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
                    if (_ic != null)
                    {
                        FreeTemplateStatusEnum cmd = (FreeTemplateStatusEnum)Enum.Parse(typeof(RowTypeEnum), _ic.status.ToString(), true);
                        switch (cmd)
                        {
                            case FreeTemplateStatusEnum.ACTIVE:
                                _ic.status = Convert.ToInt32(FreeTemplateStatusEnum.IN_ACTIVE);
                                break;
                            case FreeTemplateStatusEnum.IN_ACTIVE:
                                _ic.status = Convert.ToInt32(FreeTemplateStatusEnum.ACTIVE);
                                break;
                        }

                        Cal();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void gvMethodProcedure_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    FreeTemplateStatusEnum cmd = (FreeTemplateStatusEnum)Enum.ToObject(typeof(FreeTemplateStatusEnum), (int)gvMethodProcedure.DataKeys[e.Row.RowIndex].Values[1]);

                    switch (cmd)
                    {
                        case FreeTemplateStatusEnum.IN_ACTIVE:
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

        protected void gvMethodProcedure_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvMethodProcedure_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = e.NewEditIndex;
            Cal();
            //gvMethodProcedure.DataSource = this.ListSampleMethodProcedure;
            //gvMethodProcedure.DataBind();
        }

        protected void gvMethodProcedure_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvMethodProcedure.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txt_col_1 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_1");
            TextBox txt_col_2 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_2");
            TextBox txt_col_3 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_3");
            TextBox txt_col_4 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_4");
            TextBox txt_col_5 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_5");
            TextBox txt_col_6 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_6");
            TextBox txt_col_7 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_7");
            TextBox txt_col_8 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_8");
            TextBox txt_col_9 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_9");
            TextBox txt_col_10 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_10");
            TextBox txt_col_11 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_11");
            TextBox txt_col_12 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_12");
            TextBox txt_col_13 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_13");
            TextBox txt_col_14 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_14");
            TextBox txt_col_15 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_15");
            TextBox txt_col_16 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_16");
            TextBox txt_col_17 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_17");
            TextBox txt_col_18 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_18");
            TextBox txt_col_19 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_19");
            TextBox txt_col_20 = (TextBox)gvMethodProcedure.Rows[e.RowIndex].FindControl("txt_col_20");


            template_f_ic _smp = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.METHOD_PROCECURE) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_smp != null)
            {
                _smp.col_1 = (txt_col_1 != null) ? txt_col_1.Text : String.Empty;
                _smp.col_2 = (txt_col_2 != null) ? txt_col_2.Text : String.Empty;
                _smp.col_3 = (txt_col_3 != null) ? txt_col_3.Text : String.Empty;
                _smp.col_4 = (txt_col_4 != null) ? txt_col_4.Text : String.Empty;
                _smp.col_5 = (txt_col_5 != null) ? txt_col_5.Text : String.Empty;
                _smp.col_6 = (txt_col_6 != null) ? txt_col_6.Text : String.Empty;
                _smp.col_7 = (txt_col_7 != null) ? txt_col_7.Text : String.Empty;
                _smp.col_8 = (txt_col_8 != null) ? txt_col_8.Text : String.Empty;
                _smp.col_9 = (txt_col_9 != null) ? txt_col_9.Text : String.Empty;
                _smp.col_10 = (txt_col_10 != null) ? txt_col_10.Text : String.Empty;
                _smp.col_11 = (txt_col_11 != null) ? txt_col_11.Text : String.Empty;
                _smp.col_12 = (txt_col_12 != null) ? txt_col_12.Text : String.Empty;
                _smp.col_13 = (txt_col_13 != null) ? txt_col_13.Text : String.Empty;
                _smp.col_14 = (txt_col_14 != null) ? txt_col_14.Text : String.Empty;
                _smp.col_15 = (txt_col_15 != null) ? txt_col_15.Text : String.Empty;
                _smp.col_16 = (txt_col_16 != null) ? txt_col_16.Text : String.Empty;
                _smp.col_17 = (txt_col_17 != null) ? txt_col_17.Text : String.Empty;
                _smp.col_18 = (txt_col_18 != null) ? txt_col_18.Text : String.Empty;
                _smp.col_19 = (txt_col_19 != null) ? txt_col_19.Text : String.Empty;
                _smp.col_20 = (txt_col_20 != null) ? txt_col_20.Text : String.Empty;


            }
            gvMethodProcedure.EditIndex = -1;
            Cal();
        }

        protected void gvMethodProcedure_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMethodProcedure.EditIndex = -1;
            Cal();
            //gvMethodProcedure.DataSource = this.ListSampleMethodProcedure;
            //gvMethodProcedure.DataBind();
        }

        #endregion
        #region "GRIDVIEW-CP-AN (EVENT)"

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_f_ic _ic = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_ANIONS) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
                    if (_ic != null)
                    {
                        FreeTemplateStatusEnum cmd = (FreeTemplateStatusEnum)Enum.Parse(typeof(RowTypeEnum), _ic.status.ToString(), true);
                        switch (cmd)
                        {
                            case FreeTemplateStatusEnum.ACTIVE:
                                _ic.status = Convert.ToInt32(FreeTemplateStatusEnum.IN_ACTIVE);
                                break;
                            case FreeTemplateStatusEnum.IN_ACTIVE:
                                _ic.status = Convert.ToInt32(FreeTemplateStatusEnum.ACTIVE);
                                break;
                        }

                        Cal();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    FreeTemplateStatusEnum cmd = (FreeTemplateStatusEnum)Enum.ToObject(typeof(FreeTemplateStatusEnum), (int)GridView1.DataKeys[e.Row.RowIndex].Values[1]);

                    switch (cmd)
                    {
                        case FreeTemplateStatusEnum.IN_ACTIVE:
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Cal();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txt_col_1 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_1");
            TextBox txt_col_2 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_2");
            TextBox txt_col_3 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_3");
            TextBox txt_col_4 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_4");
            TextBox txt_col_5 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_5");
            TextBox txt_col_6 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_6");
            TextBox txt_col_7 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_7");
            TextBox txt_col_8 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_8");
            TextBox txt_col_9 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_9");
            TextBox txt_col_10 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_10");
            TextBox txt_col_11 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_11");
            TextBox txt_col_12 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_12");
            TextBox txt_col_13 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_13");
            TextBox txt_col_14 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_14");
            TextBox txt_col_15 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_15");
            TextBox txt_col_16 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_16");
            TextBox txt_col_17 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_17");
            TextBox txt_col_18 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_18");
            TextBox txt_col_19 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_19");
            TextBox txt_col_20 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txt_col_20");


            template_f_ic _smp = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_ANIONS) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_smp != null)
            {
                _smp.col_1 = (txt_col_1 != null) ? txt_col_1.Text : String.Empty;
                _smp.col_2 = (txt_col_2 != null) ? txt_col_2.Text : String.Empty;
                _smp.col_3 = (txt_col_3 != null) ? txt_col_3.Text : String.Empty;
                _smp.col_4 = (txt_col_4 != null) ? txt_col_4.Text : String.Empty;
                _smp.col_5 = (txt_col_5 != null) ? txt_col_5.Text : String.Empty;
                _smp.col_6 = (txt_col_6 != null) ? txt_col_6.Text : String.Empty;
                _smp.col_7 = (txt_col_7 != null) ? txt_col_7.Text : String.Empty;
                _smp.col_8 = (txt_col_8 != null) ? txt_col_8.Text : String.Empty;
                _smp.col_9 = (txt_col_9 != null) ? txt_col_9.Text : String.Empty;
                _smp.col_10 = (txt_col_10 != null) ? txt_col_10.Text : String.Empty;
                _smp.col_11 = (txt_col_11 != null) ? txt_col_11.Text : String.Empty;
                _smp.col_12 = (txt_col_12 != null) ? txt_col_12.Text : String.Empty;
                _smp.col_13 = (txt_col_13 != null) ? txt_col_13.Text : String.Empty;
                _smp.col_14 = (txt_col_14 != null) ? txt_col_14.Text : String.Empty;
                _smp.col_15 = (txt_col_15 != null) ? txt_col_15.Text : String.Empty;
                _smp.col_16 = (txt_col_16 != null) ? txt_col_16.Text : String.Empty;
                _smp.col_17 = (txt_col_17 != null) ? txt_col_17.Text : String.Empty;
                _smp.col_18 = (txt_col_18 != null) ? txt_col_18.Text : String.Empty;
                _smp.col_19 = (txt_col_19 != null) ? txt_col_19.Text : String.Empty;
                _smp.col_20 = (txt_col_20 != null) ? txt_col_20.Text : String.Empty;


            }
            GridView1.EditIndex = -1;
            Cal();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Cal();
        }
        #endregion
        #region "GRIDVIEW-CP-CA (EVENT)"

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(e.CommandArgument.ToString()))
                {
                    int _id = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    template_f_ic _ic = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_CATIONS) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
                    if (_ic != null)
                    {
                        FreeTemplateStatusEnum cmd = (FreeTemplateStatusEnum)Enum.Parse(typeof(RowTypeEnum), _ic.status.ToString(), true);
                        switch (cmd)
                        {
                            case FreeTemplateStatusEnum.ACTIVE:
                                _ic.status = Convert.ToInt32(FreeTemplateStatusEnum.IN_ACTIVE);
                                break;
                            case FreeTemplateStatusEnum.IN_ACTIVE:
                                _ic.status = Convert.ToInt32(FreeTemplateStatusEnum.ACTIVE);
                                break;
                        }

                        Cal();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                LinkButton _btnHide = (LinkButton)e.Row.FindControl("btnHide");
                LinkButton _btnUndo = (LinkButton)e.Row.FindControl("btnUndo");

                if (_btnHide != null && _btnUndo != null)
                {
                    FreeTemplateStatusEnum cmd = (FreeTemplateStatusEnum)Enum.ToObject(typeof(FreeTemplateStatusEnum), (int)GridView2.DataKeys[e.Row.RowIndex].Values[1]);

                    switch (cmd)
                    {
                        case FreeTemplateStatusEnum.IN_ACTIVE:
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

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            Cal();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox txt_col_1 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_1");
            TextBox txt_col_2 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_2");
            TextBox txt_col_3 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_3");
            TextBox txt_col_4 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_4");
            TextBox txt_col_5 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_5");
            TextBox txt_col_6 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_6");
            TextBox txt_col_7 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_7");
            TextBox txt_col_8 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_8");
            TextBox txt_col_9 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_9");
            TextBox txt_col_10 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_10");
            TextBox txt_col_11 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_11");
            TextBox txt_col_12 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_12");
            TextBox txt_col_13 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_13");
            TextBox txt_col_14 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_14");
            TextBox txt_col_15 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_15");
            TextBox txt_col_16 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_16");
            TextBox txt_col_17 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_17");
            TextBox txt_col_18 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_18");
            TextBox txt_col_19 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_19");
            TextBox txt_col_20 = (TextBox)GridView2.Rows[e.RowIndex].FindControl("txt_col_20");


            template_f_ic _smp = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_CATIONS) && x.id == Convert.ToInt32(_id)).FirstOrDefault();
            if (_smp != null)
            {
                _smp.col_1 = (txt_col_1 != null) ? txt_col_1.Text : String.Empty;
                _smp.col_2 = (txt_col_2 != null) ? txt_col_2.Text : String.Empty;
                _smp.col_3 = (txt_col_3 != null) ? txt_col_3.Text : String.Empty;
                _smp.col_4 = (txt_col_4 != null) ? txt_col_4.Text : String.Empty;
                _smp.col_5 = (txt_col_5 != null) ? txt_col_5.Text : String.Empty;
                _smp.col_6 = (txt_col_6 != null) ? txt_col_6.Text : String.Empty;
                _smp.col_7 = (txt_col_7 != null) ? txt_col_7.Text : String.Empty;
                _smp.col_8 = (txt_col_8 != null) ? txt_col_8.Text : String.Empty;
                _smp.col_9 = (txt_col_9 != null) ? txt_col_9.Text : String.Empty;
                _smp.col_10 = (txt_col_10 != null) ? txt_col_10.Text : String.Empty;
                _smp.col_11 = (txt_col_11 != null) ? txt_col_11.Text : String.Empty;
                _smp.col_12 = (txt_col_12 != null) ? txt_col_12.Text : String.Empty;
                _smp.col_13 = (txt_col_13 != null) ? txt_col_13.Text : String.Empty;
                _smp.col_14 = (txt_col_14 != null) ? txt_col_14.Text : String.Empty;
                _smp.col_15 = (txt_col_15 != null) ? txt_col_15.Text : String.Empty;
                _smp.col_16 = (txt_col_16 != null) ? txt_col_16.Text : String.Empty;
                _smp.col_17 = (txt_col_17 != null) ? txt_col_17.Text : String.Empty;
                _smp.col_18 = (txt_col_18 != null) ? txt_col_18.Text : String.Empty;
                _smp.col_19 = (txt_col_19 != null) ? txt_col_19.Text : String.Empty;
                _smp.col_20 = (txt_col_20 != null) ? txt_col_20.Text : String.Empty;


            }
            GridView2.EditIndex = -1;
            Cal();
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            Cal();
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //calculateByFormular();
            Boolean isValid = true;
            template_wd_ic_coverpage objWork = new template_wd_ic_coverpage();
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
                    foreach (template_f_ic ic in this.ics)
                    {
                        ic.sample_id = this.SampleID;
                        ic.specification_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        ic.isNoSpec = Convert.ToSByte(cbCheckBox.Checked);
                        ic.unit = Convert.ToInt16(ddlUnit.SelectedValue);
                    }
                    template_f_ic.DeleteBySampleID(this.SampleID);
                    template_f_ic.InsertList(this.ics);
                    this.jobSample.date_login_complete = DateTime.Now;
                    this.jobSample.date_chemist_analyze = DateTime.Now;
                    break;
                case StatusEnum.CHEMIST_TESTING:
                    this.jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                    this.jobSample.step3owner = userLogin.id;
                    this.jobSample.is_no_spec = cbCheckBox.Checked ? "1" : "0";
                    //#region ":: STAMP COMPLETE DATE"
                    this.jobSample.date_chemist_analyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                    this.jobSample.date_chemist_complete = DateTime.Now;
                    this.jobSample.date_srchemist_analyze = DateTime.Now;
                    this.jobSample.path_word = String.Empty;
                    this.jobSample.path_pdf = String.Empty;
                    //#endregion
                    foreach (template_f_ic ic in this.ics)
                    {
                        ic.sample_id = this.SampleID;
                        ic.specification_id = Convert.ToInt32(ddlComponent.SelectedValue);
                        ic.isNoSpec = Convert.ToSByte(cbCheckBox.Checked);
                        ic.unit = Convert.ToInt16(ddlUnit.SelectedValue);
                    }
                    template_f_ic.DeleteBySampleID(this.SampleID);
                    template_f_ic.InsertList(this.ics);
                    break;
                case StatusEnum.SR_CHEMIST_CHECKING:
                    StatusEnum srChemistApproveStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                    switch (srChemistApproveStatus)
                    {
                        case StatusEnum.SR_CHEMIST_APPROVE:
                            this.jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                            #region ":: STAMP COMPLETE DATE"
                            this.jobSample.date_srchemist_complate = DateTime.Now;
                            this.jobSample.date_admin_word_inprogress = DateTime.Now;
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
                            this.jobSample.date_admin_pdf_inprogress = DateTime.Now;
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
                        this.jobSample.date_admin_word_complete = DateTime.Now;
                        this.jobSample.date_labman_analyze = DateTime.Now;
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
                        this.jobSample.date_admin_pdf_complete = DateTime.Now;
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
            //########
            if (errors.Count > 0)
            {
                litErrorMessage.Text = MessageBox.GenWarnning(errors);
                modalErrorList.Show();
            }
            else
            {
                litErrorMessage.Text = String.Empty;
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
            Response.Redirect(this.PreviousPath);
        }

        protected void btnCoverPage_Click(object sender, EventArgs e)
        {

            pCoverpage.Visible = false;
            pWorkingIC.Visible = false;

            Button btn = (Button)sender;
            switch (btn.Text)
            {
                case "Cover Page":
                    btnCoverPage.CssClass = "btn green";
                    btnWorking.CssClass = "btn blue";
                    pCoverpage.Visible = true;
                    pWorkingIC.Visible = false;
                    //pSpecification.Visible = true;
                    pLoadFile.Visible = false;
                    Cal();
                    break;
                case "Workingpg-IC":
                    btnCoverPage.CssClass = "btn blue";
                    btnWorking.CssClass = "btn green";
                    pCoverpage.Visible = false;
                    pWorkingIC.Visible = true;
                    //pSpecification.Visible = false;
                    pLoadFile.Visible = true;
                    break;
            }
        }



        protected void btnLoadFile_Click(object sender, EventArgs e)
        {

            this.workSheetData = new Hashtable();

            List<template_f_ic> removeList = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS) || x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS) || x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS_HEADER) || x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS_HEADER)).ToList();
            for (int i = 0; i < removeList.Count; i++)
            {
                this.ics.Remove(removeList[i]);
            }


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


                        if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                        }

                        _postedFile.SaveAs(source_file);



                        using (FileStream fs = new FileStream(source_file, FileMode.Open, FileAccess.Read))
                        {
                            HSSFWorkbook wb = new HSSFWorkbook(fs);
                            ISheet isheet = wb.GetSheet(this.SheetWorkSheetName);
                            if (isheet == null)
                            {
                                errors.Add(String.Format("กรุณาตรวจสอบ WorkSheet จะต้องตั้งชื่อว่า {0}", this.SheetWorkSheetName));
                            }
                            else
                            {
                                String _b11 = this.configs["ic.total.volume"].ToString().Split('|')[1];
                                String _b12 = this.configs["ic.surface.area"].ToString().Split('|')[1];
                                String _b13 = this.configs["ic.no.of.parts.extracted"].ToString().Split('|')[1];

                                txtB11.Text = FreeTemplateUtil.GetCellValue(isheet, _b11);
                                txtB12.Text = FreeTemplateUtil.GetCellValue(isheet, _b12);
                                txtB13.Text = FreeTemplateUtil.GetCellValue(isheet, _b13);
                                this.workSheetData[String.Format("{0}!{1}", this.SheetWorkSheetName, _b11.ToUpper())] = txtB11.Text;
                                this.workSheetData[String.Format("{0}!{1}", this.SheetWorkSheetName, _b12.ToUpper())] = txtB12.Text;
                                this.workSheetData[String.Format("{0}!{1}", this.SheetWorkSheetName, _b13.ToUpper())] = txtB13.Text;
                                #region "anions"
                                String[] anionsRanges = this.configs["ic.anions.data.ranges"].ToString().Split('|')[1].Split(':');
                                int anionsColBegin = FreeTemplateUtil.GetColIndex(anionsRanges[0]);
                                int anionsColEnd = FreeTemplateUtil.GetColIndex(anionsRanges[1]);
                                int anionsRowBegin = FreeTemplateUtil.GetRowIndex(anionsRanges[0]);
                                int anionsRowEnd = FreeTemplateUtil.GetRowIndex(anionsRanges[1]);
                                for (int r = anionsRowBegin; r <= anionsRowEnd; r++)
                                {
                                    template_f_ic ic = new template_f_ic();
                                    int colIndex = 1;
                                    for (int c = anionsColBegin; c <= anionsColEnd; c++)
                                    {
                                        String key = String.Format("{0}!{1}{2}", this.SheetWorkSheetName, FreeTemplateUtil.GetColName(c), (r + 1));
                                        String value = CustomUtils.GetCellValue(isheet.GetRow(r).GetCell(c));
                                        this.workSheetData[key] = (Regex.IsMatch(value, @"^[0-9][\.\d]*(,\d+)?$")) ? Convert.ToDouble(value).ToString("N" + getDigit(colIndex)) : String.Format("{0}", value);
                                        setValueToTemplate(ref ic, c, value);
                                        colIndex++;
                                    }
                                    ic.row_type = (r == anionsRowBegin) ? Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS_HEADER) : Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS);
                                    ic.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                                    this.ics.Add(ic);
                                }
                                #endregion
                                #region "cations"
                                String[] cationsRanges = this.configs["ic.cations.data.ranges"].ToString().Split('|')[1].Split(':');
                                int cationsColBegin = FreeTemplateUtil.GetColIndex(cationsRanges[0]);
                                int cationsColEnd = FreeTemplateUtil.GetColIndex(cationsRanges[1]);
                                int cationsRowBegin = FreeTemplateUtil.GetRowIndex(cationsRanges[0]);
                                int cationsRowEnd = FreeTemplateUtil.GetRowIndex(cationsRanges[1]);
                                for (int r = cationsRowBegin; r <= cationsRowEnd; r++)
                                {
                                    template_f_ic ic = new template_f_ic();
                                    int colIndex = 1;
                                    for (int c = cationsColBegin; c <= cationsColEnd; c++)
                                    {
                                        String key = String.Format("{0}!{1}{2}", this.SheetWorkSheetName, FreeTemplateUtil.GetColName(c), (r + 1));
                                        String value = CustomUtils.GetCellValue(isheet.GetRow(r).GetCell(c));
                                        this.workSheetData[key] = (Regex.IsMatch(value, @"^[0-9][\.\d]*(,\d+)?$")) ? Convert.ToDouble(value).ToString("N" + getDigit(colIndex)) : String.Format("{0}", value);
                                        setValueToTemplate(ref ic, c, value);
                                        colIndex++;
                                    }
                                    ic.row_type = (r == cationsRowBegin) ? Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS_HEADER) : Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS);
                                    ic.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                                    ics.Add(ic);
                                }
                                #endregion
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
                    errors.Add(String.Format("กรุณาตรวจสอบ {0}:{1}", this.SheetCoverPageName, CustomUtils.ErrorIndex));
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
                Cal();
            }
        }

        protected void lbDownload_Click(object sender, EventArgs e)
        {



            //DataTable dt = Extenders.ObjectToDataTable(this.coverpages[0]);

            //List<template_wd_ic_coverpage> anionic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.ANIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();
            //List<template_wd_ic_coverpage> cationic = this.coverpages.Where(x => x.ic_type == Convert.ToInt32(ICTypeEnum.CATIONIC) && x.row_type == Convert.ToInt32(RowTypeEnum.Normal)).ToList();

            //ReportHeader reportHeader = ReportHeader.getReportHeder(this.jobSample);


            //ReportParameterCollection reportParameters = new ReportParameterCollection();

            //reportParameters.Add(new ReportParameter("CustomerPoNo", reportHeader.cusRefNo));
            //reportParameters.Add(new ReportParameter("AlsThailandRefNo", reportHeader.alsRefNo));
            //reportParameters.Add(new ReportParameter("Date", reportHeader.cur_date.ToString("dd MMMM yyyy") + ""));
            //reportParameters.Add(new ReportParameter("Company", reportHeader.addr1));
            //reportParameters.Add(new ReportParameter("Company_addr", reportHeader.addr2));
            //reportParameters.Add(new ReportParameter("DateSampleReceived", reportHeader.dateOfDampleRecieve.ToString("dd MMMM yyyy") + ""));
            //reportParameters.Add(new ReportParameter("DateAnalyzed", (reportHeader.dateOfAnalyze.Year == 1) ? reportHeader.cur_date.ToString("dd MMMM yyyy") : reportHeader.dateOfAnalyze.ToString("dd MMMM yyyy") + ""));
            //reportParameters.Add(new ReportParameter("DateTestCompleted", (reportHeader.dateOfTestComplete.Year == 1) ? reportHeader.cur_date.ToString("dd MMMM yyyy") : reportHeader.dateOfTestComplete.ToString("dd MMMM yyyy") + ""));
            //reportParameters.Add(new ReportParameter("SampleDescription", reportHeader.description));
            //reportParameters.Add(new ReportParameter("Test", "IC"));
            //reportParameters.Add(new ReportParameter("ResultDesc", lbSpecDesc.Text));
            ////reportParameters.Add(new ReportParameter("rpt_unit", ddlUnit.SelectedItem.Text));
            //reportParameters.Add(new ReportParameter("AlsSingaporeRefNo", (String.IsNullOrEmpty(this.jobSample.singapore_ref_no) ? " " : this.jobSample.singapore_ref_no)));

            //// Variables
            //Warning[] warnings;
            //string[] streamIds;
            //string mimeType = string.Empty;
            //string encoding = string.Empty;
            //string extension = string.Empty;

            //foreach (template_wd_ic_coverpage a in anionic)
            //{
            //    a.B = CustomUtils.isNumber(a.B) ? "<" + a.B : a.B;
            //}
            //foreach (template_wd_ic_coverpage b in cationic)
            //{
            //    b.B = CustomUtils.isNumber(b.B) ? "<" + b.B : b.B;
            //}
            //// Setup the report viewer object and get the array of bytes
            //ReportViewer viewer = new ReportViewer();
            //viewer.ProcessingMode = ProcessingMode.Local;
            //viewer.LocalReport.ReportPath = Server.MapPath("~/ReportObject/ic_wd.rdlc");
            //viewer.LocalReport.SetParameters(reportParameters);
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); // Add datasource here
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", anionic.ToDataTable())); // Add datasource here
            //viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", cationic.ToDataTable())); // Add datasource here



            //string download = String.Empty;

            //StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), this.jobSample.job_status.ToString(), true);
            //switch (status)
            //{
            //    case StatusEnum.ADMIN_CONVERT_WORD:
            //        if (!String.IsNullOrEmpty(this.jobSample.path_word))
            //        {
            //            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
            //        }
            //        else
            //        {
            //            byte[] bytes = viewer.LocalReport.Render("Word", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            //            if (!Directory.Exists(Server.MapPath("~/Report/")))
            //            {
            //                Directory.CreateDirectory(Server.MapPath("~/Report/"));
            //            }
            //            using (FileStream fs = File.Create(Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension))
            //            {
            //                fs.Write(bytes, 0, bytes.Length);
            //            }

            //            #region "Insert Footer & Header from template"
            //            Document doc1 = new Document();
            //            doc1.LoadFromFile(Server.MapPath("~/template/") + "Blank Letter Head - EL.doc");
            //            Spire.Doc.HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
            //            Spire.Doc.HeaderFooter footer = doc1.Sections[0].HeadersFooters.Footer;
            //            Document doc2 = new Document(Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension);
            //            foreach (Section section in doc2.Sections)
            //            {
            //                foreach (DocumentObject obj in header.ChildObjects)
            //                {
            //                    section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
            //                }
            //                foreach (DocumentObject obj in footer.ChildObjects)
            //                {
            //                    section.HeadersFooters.Footer.ChildObjects.Add(obj.Clone());
            //                }
            //            }



            //            doc2.SaveToFile(Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension);
            //            #endregion
            //            Response.ContentType = mimeType;
            //            Response.AddHeader("Content-Disposition", "attachment; filename=" + this.jobSample.job_number + "." + extension);
            //            Response.WriteFile(Server.MapPath("~/Report/" + this.jobSample.job_number + "." + extension));
            //            Response.Flush();

            //            #region "Delete After Download"
            //            String deleteFile1 = Server.MapPath("~/Report/") + this.jobSample.job_number + "." + extension;
            //            String deleteFile2 = Server.MapPath("~/Report/") + this.jobSample.job_number + "_orginal." + extension;

            //            if (File.Exists(deleteFile1))
            //            {
            //                File.Delete(deleteFile1);
            //            }
            //            if (File.Exists(deleteFile2))
            //            {
            //                File.Delete(deleteFile2);
            //            }
            //            #endregion
            //        }
            //        break;
            //    case StatusEnum.LABMANAGER_CHECKING:
            //    case StatusEnum.LABMANAGER_APPROVE:
            //    case StatusEnum.LABMANAGER_DISAPPROVE:
            //        if (!String.IsNullOrEmpty(this.jobSample.path_word))
            //        {
            //            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
            //        }
            //        break;
            //    case StatusEnum.ADMIN_CONVERT_PDF:
            //        if (!String.IsNullOrEmpty(this.jobSample.path_word))
            //        {
            //            Response.Redirect(String.Format("{0}{1}", Configurations.HOST, this.jobSample.path_word));
            //        }
            //        break;
            //}





        }


        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<template_f_ic> removeList = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_ANIONS_HEADER) || x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_ANIONS) || x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_CATIONS_HEADER) || x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_CATIONS)).ToList();
            for (int i = 0; i < removeList.Count; i++)
            {
                this.ics.Remove(removeList[i]);
            }

            m_template template = new m_template().SelectByID(this.jobSample.template_id);

            FileInfo fileInfo = new FileInfo(template.path_source_file);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet wSpecification = package.Workbook.Worksheets["Specification"];
                ExcelWorksheet wCoverPage = package.Workbook.Worksheets["Coverpage-TH"];
                #region "anions"
                //HEADER
                String[] headers = this.configs["result.anions.data.header"].ToString().Split('|')[1].Split(',');
                int hc = 0;
                template_f_ic hIc = new template_f_ic();

                foreach (String value in headers)
                {
                    hIc.id = CustomUtils.GetRandomNumberID();
                    setValueToTemplate(ref hIc, hc, value);
                    hIc.row_type = Convert.ToInt16(FreeTemplateIcEnum.CP_ANIONS_HEADER);
                    hIc.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                    hc++;
                }
                this.ics.Add(hIc);

                //DETAIL ITEM
                String[] itemDetails = this.configs["result.anions.data.ranges"].ToString().Split('|')[1].Split(':');

                int colBegin = FreeTemplateUtil.GetColIndex(itemDetails[0]);
                int colEnd = FreeTemplateUtil.GetColIndex(itemDetails[1]);
                int rowBegin = FreeTemplateUtil.GetRowIndex(itemDetails[0]);
                int rowEnd = FreeTemplateUtil.GetRowIndex(itemDetails[1]);

                for (int r = rowBegin; r <= rowEnd; r++)
                {
                    template_f_ic ic = new template_f_ic();
                    ic.id = CustomUtils.GetRandomNumberID();
                    int colIndex = 1;
                    for (int c = colBegin; c <= colEnd; c++)
                    {

                        String type = this.configs[String.Format("result.anions.type.col.{0}", colIndex)].ToString().Split('|')[0];
                        String[] formularCell = null;
                        String value = "";
                        switch (type)
                        {
                            case "text":
                                value = wCoverPage.Cells[String.Format("{0}{1}", FreeTemplateUtil.GetColName(c), (r + 1))].Text;
                                break;
                            case "formula.sp":
                                formularCell = this.configs[String.Format("result.anions.type.col.{0}", colIndex)].ToString().Split('|')[1].Split(',');
                                string cell = String.Format("{0}{1}", formularCell[r - rowBegin], ddlComponent.SelectedValue);
                                value = wSpecification.Cells[cell].Value.ToString();
                                value = Regex.IsMatch(value, @"^\d+$") ? String.Format("< {0}", value) : value;
                                Console.WriteLine();
                                break;
                            case "formula.ws":
                                Console.WriteLine();
                                break;
                        }
                        setValueToTemplate(ref ic, c, value);
                        colIndex++;
                    }
                    ic.row_type = Convert.ToInt16(FreeTemplateIcEnum.CP_ANIONS);
                    ic.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                    this.ics.Add(ic);
                }
                #endregion
                #region "cations"
                //HEADER
                headers = this.configs["result.cations.data.header"].ToString().Split('|')[1].Split(',');
                hc = 0;
                hIc = new template_f_ic();

                foreach (String value in headers)
                {
                    hIc.id = CustomUtils.GetRandomNumberID();
                    setValueToTemplate(ref hIc, hc, value);
                    hIc.row_type = Convert.ToInt16(FreeTemplateIcEnum.CP_CATIONS_HEADER);
                    hIc.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                    hc++;
                }
                this.ics.Add(hIc);

                //DETAIL ITEM
                itemDetails = this.configs["result.cations.data.ranges"].ToString().Split('|')[1].Split(':');

                colBegin = FreeTemplateUtil.GetColIndex(itemDetails[0]);
                colEnd = FreeTemplateUtil.GetColIndex(itemDetails[1]);
                rowBegin = FreeTemplateUtil.GetRowIndex(itemDetails[0]);
                rowEnd = FreeTemplateUtil.GetRowIndex(itemDetails[1]);
                for (int r = rowBegin; r <= rowEnd; r++)
                {
                    template_f_ic ic = new template_f_ic();
                    ic.id = CustomUtils.GetRandomNumberID();
                    int colIndex = 1;
                    for (int c = colBegin; c <= colEnd; c++)
                    {

                        String type = this.configs[String.Format("result.cations.type.col.{0}", colIndex)].ToString().Split('|')[0];
                        String[] formularCell = null;
                        String value = "";
                        switch (type)
                        {
                            case "text":
                                value = wCoverPage.Cells[String.Format("{0}{1}", FreeTemplateUtil.GetColName(c), (r + 1))].Text;
                                break;
                            case "formula.sp":
                                formularCell = this.configs[String.Format("result.cations.type.col.{0}", colIndex)].ToString().Split('|')[1].Split(',');
                                string cell = String.Format("{0}{1}", formularCell[c], ddlComponent.SelectedValue);
                                value = wSpecification.Cells[cell].Value.ToString();
                                value = Regex.IsMatch(value, @"^\d+$") ? String.Format("< {0}", value) : value;
                                Console.WriteLine();
                                break;
                            case "formula.ws":
                                Console.WriteLine();
                                break;
                        }
                        setValueToTemplate(ref ic, c, value);
                        colIndex++;
                    }
                    ic.row_type = Convert.ToInt16(FreeTemplateIcEnum.CP_CATIONS);
                    ic.status = Convert.ToInt16(FreeTemplateStatusEnum.ACTIVE);
                    this.ics.Add(ic);
                }

                #endregion
                #region "result"
                String param1 = this.configs["result.desc.param.1"].ToString().Split('|')[1];
                String param2 = this.configs["result.desc.param.2"].ToString().Split('|')[1];
                String _param1 = wSpecification.Cells[param1].Value.ToString();
                String _param2 = wSpecification.Cells[String.Format(param2, ddlComponent.SelectedValue)].Value.ToString();
                lbSpecDesc.Text = String.Format(this.configs["result.desc"].ToString().Split('|')[1], _param1, _param2);
                #endregion
            }
            Cal();
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

        private void Cal()
        {

            #region "render header"
            template_f_ic mpHeader = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.METHOD_PROCECURE_HEADER)).FirstOrDefault();

            template_f_ic hGv1 = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_ANIONS_HEADER)).FirstOrDefault();
            template_f_ic hGv2 = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_CATIONS_HEADER)).FirstOrDefault();
            template_f_ic hGv3 = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS_HEADER)).FirstOrDefault();
            template_f_ic hGv4 = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS_HEADER)).FirstOrDefault();

            //String[] pHeader = this.configs["method.procedure.data.header"].ToString().Split('|')[1].Split(',');

            //String[] aHeader = this.configs["result.anions.data.header"].ToString().Split('|')[1].Split(',');
            //String[] cHeader = this.configs["result.cations.data.header"].ToString().Split('|')[1].Split(',');

            setGridViewHeader(ref gvMethodProcedure, mpHeader);
            setGridViewHeader(ref GridView1, hGv1);
            setGridViewHeader(ref GridView2, hGv2);
            setGridViewHeader(ref GridView3, hGv3);
            setGridViewHeader(ref GridView4, hGv4);
            initUnit();
            #endregion

            #region "render data"
            List<template_f_ic> listMP = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.METHOD_PROCECURE)).ToList();
            List<template_f_ic> listCpAnions = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_ANIONS)).ToList();
            List<template_f_ic> listCpCations = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.CP_CATIONS)).ToList();
            List<template_f_ic> listWsAnions = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS)).ToList();
            List<template_f_ic> listWsCations = this.ics.Where(x => x.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS)).ToList();

            for (int r = 0; r < listMP.Count; r++)
            {
                setCalValue(r, "method.procedure.type.col.{0}", listMP[r]);
            }
            for (int r = 0; r < listCpAnions.Count; r++)
            {
                setCalValue(r, "result.anions.type.col.{0}", listCpAnions[r]);
            }
            for (int r = 0; r < listCpCations.Count; r++)
            {
                setCalValue(r, "result.cations.type.col.{0}", listCpCations[r]);
            }

            try
            {



                gvMethodProcedure.DataSource = listMP;
                gvMethodProcedure.DataBind();
                GridView1.DataSource = listCpAnions;
                GridView1.DataBind();
                GridView2.DataSource = listCpCations;
                GridView2.DataBind();
                GridView3.DataSource = listWsAnions;
                GridView3.DataBind();
                GridView4.DataSource = listWsCations;
                GridView4.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
            }
            #endregion
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cal();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ModolPopupExtender.Show();
        }

        protected void OK_Click(object sender, EventArgs e)
        {

        }


        protected void cbCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            m_template template = new m_template().SelectByID(this.jobSample.template_id);

            FileInfo fileInfo = new FileInfo(template.path_source_file);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet wSpecification = package.Workbook.Worksheets["Specification"];
                if (cbCheckBox.Checked)
                {
                    lbSpecDesc.Text = this.configs["result.desc.no"].ToString().Split('|')[1];
                }
                else
                {
                    #region "result"
                    String param1 = this.configs["result.desc.param.1"].ToString().Split('|')[1];
                    String param2 = this.configs["result.desc.param.2"].ToString().Split('|')[1];
                    String _param1 = wSpecification.Cells[param1].Value.ToString();
                    String _param2 = wSpecification.Cells[String.Format(param2, ddlComponent.SelectedValue)].Value.ToString();
                    lbSpecDesc.Text = String.Format(this.configs["result.desc"].ToString().Split('|')[1], _param1, _param2);
                    #endregion
                }

            }


        }


        private void setValueToTemplate(ref template_f_ic ic, int c, String _val)
        {
            switch (c + 1)
            {
                case 1:
                    ic.col_1 = _val;
                    break;
                case 2:
                    ic.col_2 = _val;
                    break;
                case 3:
                    ic.col_3 = _val;
                    break;
                case 4:
                    ic.col_4 = _val;
                    break;
                case 5:
                    ic.col_5 = _val;
                    break;
                case 6:
                    ic.col_6 = _val;
                    break;
                case 7:
                    ic.col_7 = _val;
                    break;
                case 8:
                    ic.col_8 = _val;
                    break;
                case 9:
                    ic.col_9 = _val;
                    break;
                case 10:
                    ic.col_10 = _val;
                    break;
                case 11:
                    ic.col_11 = _val;
                    break;
                case 12:
                    ic.col_12 = _val;
                    break;
                case 13:
                    ic.col_13 = _val;
                    break;
                case 14:
                    ic.col_14 = _val;
                    break;
                case 15:
                    ic.col_15 = _val;
                    break;
                case 16:
                    ic.col_16 = _val;
                    break;
                case 17:
                    ic.col_17 = _val;
                    break;
                case 18:
                    ic.col_18 = _val;
                    break;
                case 19:
                    ic.col_19 = _val;
                    break;
                case 20:
                    ic.col_20 = _val;
                    break;
            }
        }

        private void setGridViewHeader(ref GridView gv, template_f_ic _header)
        {
            if (_header != null)
            {
                for (int c = 0; c < gv.Columns.Count - 2; c++)
                {
                    String headerName = String.Empty;
                    switch (c + 1)
                    {
                        case 1: headerName = _header.col_1; break;
                        case 2: headerName = _header.col_2; break;
                        case 3: headerName = _header.col_3; break;
                        case 4: headerName = _header.col_4; break;
                        case 5: headerName = _header.col_5; break;
                        case 6: headerName = _header.col_6; break;
                        case 7: headerName = _header.col_7; break;
                        case 8: headerName = _header.col_8; break;
                        case 9: headerName = _header.col_9; break;
                        case 10: headerName = _header.col_10; break;
                        case 11: headerName = _header.col_11; break;
                        case 12: headerName = _header.col_12; break;
                        case 13: headerName = _header.col_13; break;
                        case 14: headerName = _header.col_14; break;
                        case 15: headerName = _header.col_15; break;
                        case 16: headerName = _header.col_16; break;
                        case 17: headerName = _header.col_17; break;
                        case 18: headerName = _header.col_18; break;
                        case 19: headerName = _header.col_19; break;
                        case 20: headerName = _header.col_20; break;
                    }

                    if (!String.IsNullOrEmpty(headerName))
                    {
                        gv.Columns[c].Visible = true;
                        gv.Columns[c].HeaderText = headerName;
                    }
                    else
                    {
                        gv.Columns[c].Visible = false;
                    }
                }
            }
        }

        private void setCalValue(int r, String prefix, template_f_ic ic)
        {
            if (this.workSheetData != null)
            {
                String sheet = this.SheetWorkSheetName;
                String type = String.Empty;
                String[] value = null;
                int colIndex = 1;
                try
                {


                    if (ic.col_1 != null)
                    {
                        type = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[0];
                        switch (type)
                        {
                            case "formula.ws":
                                value = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[1].Split(',');
                                if (this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())] != null)
                                {
                                    ic.col_1 = this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())].ToString();
                                }
                                break;
                        }
                        colIndex++;
                    }
                    if (ic.col_2 != null)
                    {
                        type = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[0];
                        switch (type)
                        {
                            case "formula.ws":
                                value = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[1].Split(',');
                                if (this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())] != null)
                                {
                                    ic.col_2 = this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())].ToString();
                                }
                                break;
                        }
                        colIndex++;
                    }
                    if (ic.col_3 != null)
                    {
                        type = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[0];
                        switch (type)
                        {
                            case "formula.ws":
                                value = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[1].Split(',');
                                if (this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())] != null)
                                {
                                    ic.col_3 = this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())].ToString();
                                }
                                break;
                        }
                        colIndex++;
                    }
                    if (ic.col_4 != null)
                    {
                        type = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[0];
                        switch (type)
                        {
                            case "formula.ws":
                                value = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[1].Split(',');
                                if (this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())] != null)
                                {
                                    ic.col_4 = this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())].ToString();
                                }
                                break;
                        }
                        colIndex++;
                    }
                    if (ic.col_5 != null)
                    {
                        type = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[0];
                        switch (type)
                        {
                            case "formula.ws":
                                value = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[1].Split(',');
                                if (this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())] != null)
                                {
                                    ic.col_5 = this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())].ToString();
                                }
                                break;
                        }
                        colIndex++;
                    }
                    if (ic.col_6 != null)
                    {
                        type = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[0];
                        switch (type)
                        {
                            case "formula.ws":
                                value = this.configs[String.Format(prefix, colIndex)].ToString().Split('|')[1].Split(',');
                                if (this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())] != null)
                                {
                                    ic.col_6 = this.workSheetData[String.Format("{0}!{1}", sheet, value[r].ToUpper())].ToString();
                                }
                                break;
                        }
                        colIndex++;
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //if (ic.col_7 != null)
                //{
                //    ic.col_7 = ((ic.col_7.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_7] == null) ? null : this.workSheetData[ic.col_7].ToString()) : ic.col_7);
                //    if (ic.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS) || ic.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS))
                //    {
                //        if (Regex.IsMatch(ic.col_7, @"^[0-9][\.\d]*(,\d+)?$"))
                //            ic.col_7 = Convert.ToDouble(ic.col_7).ToString("N" + txtDecimal06.Text);
                //    }
                //}
                //if (ic.col_8 != null)
                //{
                //    ic.col_8 = ((ic.col_8.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_8] == null) ? null : this.workSheetData[ic.col_8].ToString()) : ic.col_8);
                //    if (ic.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS) || ic.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS))
                //    {
                //        if (Regex.IsMatch(ic.col_8, @"^[0-9][\.\d]*(,\d+)?$"))
                //            ic.col_8 = Convert.ToDouble(ic.col_8).ToString("N" + txtDecimal07.Text);
                //    }
                //}
                //if (ic.col_9 != null)
                //{
                //    ic.col_9 = ((ic.col_9.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_9] == null) ? null : this.workSheetData[ic.col_9].ToString()) : ic.col_9);
                //    if (ic.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_ANIONS) || ic.row_type == Convert.ToInt16(FreeTemplateIcEnum.WS_CATIONS))
                //    {
                //        if (Regex.IsMatch(ic.col_9, @"^[0-9][\.\d]*(,\d+)?$"))
                //            ic.col_8 = Convert.ToDouble(ic.col_9).ToString("N" + txtDecimal08.Text);
                //    }
                //}
                //if (ic.col_10 != null)
                //{
                //    ic.col_10 = ((ic.col_10.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_10] == null) ? null : this.workSheetData[ic.col_10].ToString()) : ic.col_10);
                //}

                //if (ic.col_11 != null)
                //{
                //    ic.col_11 = ((ic.col_11.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_11] == null) ? null : this.workSheetData[ic.col_11].ToString()) : ic.col_11);
                //}
                //if (ic.col_12 != null)
                //{
                //    ic.col_12 = ((ic.col_12.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_12] == null) ? null : this.workSheetData[ic.col_12].ToString()) : ic.col_12);
                //}
                //if (ic.col_13 != null)
                //{
                //    ic.col_13 = ((ic.col_13.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_13] == null) ? null : this.workSheetData[ic.col_13].ToString()) : ic.col_13);
                //}
                //if (ic.col_14 != null)
                //{
                //    ic.col_14 = ((ic.col_14.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_14] == null) ? null : this.workSheetData[ic.col_14].ToString()) : ic.col_14);
                //}
                //if (ic.col_15 != null)
                //{
                //    ic.col_15 = ((ic.col_15.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_15] == null) ? null : this.workSheetData[ic.col_15].ToString()) : ic.col_15);
                //}
                //if (ic.col_16 != null)
                //{
                //    ic.col_16 = ((ic.col_16.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_16] == null) ? null : this.workSheetData[ic.col_16].ToString()) : ic.col_16);
                //}
                //if (ic.col_17 != null)
                //{
                //    ic.col_17 = ((ic.col_17.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_17] == null) ? null : this.workSheetData[ic.col_17].ToString()) : ic.col_17);
                //}
                //if (ic.col_18 != null)
                //{
                //    ic.col_18 = ((ic.col_18.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_18] == null) ? null : this.workSheetData[ic.col_18].ToString()) : ic.col_18);
                //}
                //if (ic.col_19 != null)
                //{
                //    ic.col_19 = ((ic.col_19.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_19] == null) ? null : this.workSheetData[ic.col_19].ToString()) : ic.col_19);
                //}
                //if (ic.col_20 != null)
                //{
                //    ic.col_20 = ((ic.col_20.IndexOf(FreeTemplateUtil.DELIMITER_EXCLAMATION_MARK) > 0) ? ((this.workSheetData[ic.col_20] == null) ? null : this.workSheetData[ic.col_20].ToString()) : ic.col_20);
                //}
            }

        }

        private int getDigit(int colIndex)
        {
            int digit = 0;
            switch (colIndex)
            {
                case 1: break;//Col Name
                case 2: digit = Convert.ToInt32(txtDecimal01.Text); break;
                case 3: digit = Convert.ToInt32(txtDecimal02.Text); break;
                case 4: digit = Convert.ToInt32(txtDecimal03.Text); break;
                case 5: digit = Convert.ToInt32(txtDecimal04.Text); break;
                case 6: digit = Convert.ToInt32(txtDecimal05.Text); break;
                case 7: digit = Convert.ToInt32(txtDecimal06.Text); break;
                case 8: digit = Convert.ToInt32(txtDecimal07.Text); break;

            }
            return digit;
        }

        private void initUnit()
        {
            //String[] pHeader = this.configs["method.procedure.data.header"].ToString().Split('|')[1].Split(',');
            String[] aHeader = this.configs["result.anions.data.header"].ToString().Split('|')[1].Split(',');
            String[] cHeader = this.configs["result.cations.data.header"].ToString().Split('|')[1].Split(',');
            //for (int c = 0; c < pHeader.Length; c++)
            //{
            //    gvMethodProcedure.Columns[c].HeaderText = String.Format(pHeader[c], ddlUnit.SelectedItem.Text);
            //}
            for (int c = 0; c < aHeader.Length; c++)
            {
                GridView1.Columns[c].HeaderText = String.Format(aHeader[c], ddlUnit.SelectedItem.Text);
            }
            for (int c = 0; c < cHeader.Length; c++)
            {
                GridView2.Columns[c].HeaderText = String.Format(cHeader[c], ddlUnit.SelectedItem.Text);
            }
        }
    }
}