﻿using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.request
{
    public partial class SearchJobRequest : System.Web.UI.Page
    {

        #region "Property"

        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }

        public IEnumerable<SearchResult> searchResult
        {
            get { return (IEnumerable<SearchResult>)Session[GetType().Name + "SearchJobRequest"]; }
            set { Session[GetType().Name + "SearchJobRequest"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public List<int> selectedList
        {
            get { return (List<int>)Session[GetType().Name + "selectedList"]; }
            set { Session[GetType().Name + "selectedList"] = value; }
        }
        public Boolean isPoGroupOperation
        {
            get { return (Boolean)Session[GetType().Name + "isPoGroupOperation"]; }
            set { Session[GetType().Name + "isPoGroupOperation"] = value; }
        }
        public Boolean isSentToCusDateOperation
        {
            get { return (Boolean)Session[GetType().Name + "isSentToCusDateOperation"]; }
            set { Session[GetType().Name + "isSentToCusDateOperation"] = value; }

        }
        public Boolean isNoteGroupOpeation
        {
            get { return (Boolean)Session[GetType().Name + "isNoteGroupOpeation"]; }
            set { Session[GetType().Name + "isNoteGroupOpeation"] = value; }
        }
        public Boolean isDuedateGroupOperation
        {
            get { return (Boolean)Session[GetType().Name + "isDuedateGroupOperation"]; }
            set { Session[GetType().Name + "isDuedateGroupOperation"] = value; }
        }

        public Boolean isInvoiceGroupOperation
        {
            get { return (Boolean)Session[GetType().Name + "isInvoiceGroupOperation"]; }
            set { Session[GetType().Name + "isInvoiceGroupOperation"] = value; }
        }
        public Boolean isCusRefNoGroupOperation
        {
            get { return (Boolean)Session[GetType().Name + "isCusRefNoGroupOperation"]; }
            set { Session[GetType().Name + "isCusRefNoGroupOperation"] = value; }
        }
        public Boolean isGroupApproveOperation
        {
            get { return (Boolean)Session[GetType().Name + "isGroupApproveOperation"]; }
            set { Session[GetType().Name + "isGroupApproveOperation"] = value; }
        }

        public SortDirection SortDirection
        {
            get { return (SortDirection)Session[GetType().Name + "SortDirection"]; }
            set { Session[GetType().Name + "SortDirection"] = value; }
        }

        public int JobID { get; set; }

        public int SampleID { get; set; }

        public job_info obj
        {
            get
            {
                job_info tmp = new job_info();

                tmp.status = String.IsNullOrEmpty(ddlJobStatus.SelectedValue) ? 0 : int.Parse(ddlJobStatus.SelectedValue);
                tmp.jobRefNo = txtREfNo.Text.TrimEnd();
                tmp.customer_id = String.IsNullOrEmpty(ddlCompany.SelectedValue) ? 0 : int.Parse(ddlCompany.SelectedValue);
                tmp.customerText = ddlCompany.SelectedItem.Text;
                tmp.spec_id = String.IsNullOrEmpty(ddlSpecification.SelectedValue) ? 0 : int.Parse(ddlSpecification.SelectedValue);
                tmp.dataGroup = String.IsNullOrEmpty(ddlTypeOfTest.SelectedValue) ? "" : ddlTypeOfTest.SelectedItem.Text;

                tmp.preFixText = hPrefix.Value;// String.IsNullOrEmpty(hPrefix.Value) ? 1 : Convert.ToInt16(hPrefix.Value);
                RoleEnum role = (RoleEnum)Enum.ToObject(typeof(RoleEnum), userLogin.role_id);
                switch (role)
                {
                    case RoleEnum.LOGIN:
                        break;
                    case RoleEnum.ROOT:
                        break;
                    case RoleEnum.CHEMIST:
                        tmp.responsible_test = userLogin.responsible_test.Split(Constants.CHAR_COMMA);
                        break;
                    case RoleEnum.SR_CHEMIST:
                        break;
                    case RoleEnum.LABMANAGER:
                        break;
                    case RoleEnum.ADMIN:
                        break;
                }

                tmp.sample_po = txtPo.Text;
                tmp.sample_invoice = txtInvoice.Text;
                tmp.receive_report_from = String.IsNullOrEmpty(txtReceivedReportFrom.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtReceivedReportFrom.Text);
                tmp.receive_report_to = String.IsNullOrEmpty(txtReceivedReportTo.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtReceivedReportTo.Text);

                tmp.duedate_from = String.IsNullOrEmpty(txtDuedateFrom.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtDuedateFrom.Text);
                tmp.duedate_to = String.IsNullOrEmpty(txtDuedateTo.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtDuedateTo.Text);

                tmp.report_to_customer_from = String.IsNullOrEmpty(txtReportToCustomerFrom.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtReportToCustomerFrom.Text);
                tmp.report_to_customer_to = String.IsNullOrEmpty(txtReportToCustomerTo.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtReportToCustomerTo.Text);
                tmp.userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);
                tmp.physicalYear = Convert.ToInt16(ddlPhysicalYear.SelectedValue);
                tmp.section = ddlBoiNonBoi.SelectedValue.ToString();
                return tmp;
            }
        }
        #endregion

        #region "Method"

        private void initialPage()
        {
            this.selectedList = new List<int>();
            SortDirection = new SortDirection();
            ddlCompany.Items.Clear();
            ddlCompany.DataSource = new m_customer().SelectAll().OrderBy(x => x.company_name);
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            ddlJobStatus.Items.Clear();
            ddlJobStatus.DataSource = new m_status().SelectByMainStatusNoDelete();
            ddlJobStatus.DataBind();
            ddlJobStatus.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = new m_specification().SelectAll();
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            ddlTypeOfTest.Items.Clear();
            ddlTypeOfTest.DataSource = new m_type_of_test().SelectDistinct();
            ddlTypeOfTest.DataBind();
            ddlTypeOfTest.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            List<PhysicalYear> yesrList = new List<PhysicalYear>();
            for (int i = DateTime.Now.Year - 3; i < DateTime.Now.Year + 3; i++)
            {
                PhysicalYear _year = new PhysicalYear();
                _year.year = i;
                yesrList.Add(_year);
            }

            ddlPhysicalYear.Items.Clear();
            ddlPhysicalYear.DataSource = yesrList;
            ddlPhysicalYear.DataBind();

            if (DateTime.Now.Month < Constants.PHYSICAL_YEAR)
            {
                ddlPhysicalYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
            }
            else
            {
                ddlPhysicalYear.SelectedValue = (DateTime.Now.Year).ToString();
            }

            //
            //สถานะการใช้(A = Available, I = ISSUED SOME, N = ISSUED ALL, C = Cancel)
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("NAME", typeof(string));


            // Here we add five DataRows.
            dt.Rows.Add("B", "BOI");
            dt.Rows.Add("NB", "NON-BOI");

            ddlBoiNonBoi.DataSource = dt;
            ddlBoiNonBoi.DataTextField = "NAME";
            ddlBoiNonBoi.DataValueField = "ID";
            ddlBoiNonBoi.DataBind();

            dt.Clear();
            dt.Dispose();

            ListItem item = new ListItem();
            item.Text = "";
            item.Value = "";
            ddlBoiNonBoi.Items.Insert(0, item);
            //
            bindingData();


            RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);
            switch (userRole)
            {
                case RoleEnum.LOGIN:
                    lbAddJob.Visible = true;
                    break;
                default:
                    lbAddJob.Visible = false;
                    break;
            }
            btnOperation.Visible = (userRole != RoleEnum.ACCOUNT);
            btnOperationPo.Visible = (userRole == RoleEnum.ADMIN);
            btnOperationDueDate.Visible = (userRole == RoleEnum.SR_CHEMIST || userRole == RoleEnum.ADMIN || userRole == RoleEnum.LOGIN);
            btnOperationSentToCus.Visible = (userRole == RoleEnum.ADMIN);
            btnOperationNote.Visible = (userRole == RoleEnum.ADMIN || userRole == RoleEnum.ACCOUNT || userRole == RoleEnum.CHEMIST);
            btnOperationCusRefNo.Visible = (userRole == RoleEnum.MARKETING);
            btnElp.CssClass = "btn blue";
            //btnOperationDueDate.Text =  "Due date" : "";
            btnOperationGroupInvoice.Visible = (userRole == RoleEnum.ACCOUNT);
        }

        private void bindingData()
        {
            if (!String.IsNullOrEmpty(txtREfNo.Text) && txtREfNo.Text.Split('-').Length == 3)
            {
                hPrefix.Value = String.IsNullOrEmpty(txtREfNo.Text) ? "ELP" : txtREfNo.Text.Split('-')[0];
                btnElp.CssClass = "btn btn-default btn-sm";
                btnEln.CssClass = "btn btn-default btn-sm";

                btnEls.CssClass = "btn btn-default btn-sm";
                btnFa.CssClass = "btn btn-default btn-sm";
                btnElwa.CssClass = "btn btn-default btn-sm";
                btnGrp.CssClass = "btn btn-default btn-sm";
                btnTrb.CssClass = "btn btn-default btn-sm";

                switch (hPrefix.Value.ToUpper())
                {
                    case "ELP":
                        btnElp.CssClass = "btn blue";
                        break;
                    case "ELS":
                        btnEls.CssClass = "btn blue";
                        break;
                    case "ELN":
                        btnEln.CssClass = "btn blue";
                        break;
                    case "FA":
                        btnFa.CssClass = "btn blue";
                        break;
                    case "ELWA":
                        btnElwa.CssClass = "btn blue";
                        break;
                    case "GRP":
                        btnGrp.CssClass = "btn blue";
                        break;
                    case "TRB":
                        btnTrb.CssClass = "btn blue";
                        break;
                }

            }
            searchResult = obj.SearchData();
            /* 
                ////////////////////////////////////////////////ADMIN////////////////////////////////////////////////
                |Job Type	
                |Status	
                -------------------------
                3|Job Status	
                4|Received	
                5|Report Sent to Customer	
                6|Receive Date	
                7|Due Date	
                8|ALS Ref	No.
                9|Cus Ref No	
                10|Other Ref No	
                11|Company	
                12|Invoice	
                13|Po	
                14|Contact	
                15|Description	
                18|Specification	
                19|Type of test	
                -------------------------
                |Data Group	
                |date_login_complete	
                |date_chemist_complete	
                |date_admin_word_complete	
                |date_labman_complete	
                |date_admin_pdf_complete	
                20|Note for Admin & Account
                |Remark (AM & Retest)
                ////////////////////////////////////////////////OTHER//////////////////////////////////////////////////////
                |Job Type	
                |Status	
                -------------------------
                3|Job Status	
                |Report Sent to Customer	
                4|Receive Date	
                |Due Date	
                |ALS Ref	No.
                |Cus Ref No	
                |Other Ref No	
                |Company	
                |Description	
                |Model	
                |Surface Area	
                |Specification	
                |Type of test	
                -------------------------
                |Data Group	
                |date_login_complete	
                |date_chemist_complete	
                |date_srchemist_complate	
                |date_admin_word_complete	
                |date_labman_complete	
                |date_admin_pdf_complete	
                20|Note for lab	
                |Remark (AM & Retest)
            */
            /*                                       
                0|#                                      |
                1|Select                                 |
                2|#                                      |
                3|Status                                 |
                4|Received                               |
                5|Report Sent to Customer                |
                6|Receive Date.                          |
                7|Due Date.                              |
                8|ALS Ref No.                            |
                9|Cus Ref No.                            |
                10|Other Ref No                          |
                11|Company                               |
                12|Invoice                               |
                13|Po                                    |
                14|Contact                               |
                15|Description                           |
                16|Model                                 |
                17|Surface Area                          |
                18|Specification                         |
                19|Type of test                          |
                20|Note for Admin & Account              |
                21|Note for lab                          |
            */

            gvJob.DataSource = searchResult;
            gvJob.DataBind();
            gvJob.UseAccessibleHeader = true;
            gvJob.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvJob.Rows.Count > 0)
            {
                lbTotalRecords.Text = String.Format(Constants.TOTAL_RECORDS, gvJob.Rows.Count);

                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);
                switch (userRole)
                {
                    case RoleEnum.ACCOUNT:
                        gvJob.Columns[0].Visible = true;
                        gvJob.Columns[1].Visible = true;
                        gvJob.Columns[2].Visible = true;
                        gvJob.Columns[3].Visible = true;
                        gvJob.Columns[4].Visible = true;
                        gvJob.Columns[5].Visible = true;
                        gvJob.Columns[6].Visible = true;
                        gvJob.Columns[7].Visible = true;
                        gvJob.Columns[8].Visible = true;
                        gvJob.Columns[9].Visible = true;
                        gvJob.Columns[10].Visible = true;
                        gvJob.Columns[11].Visible = true;
                        gvJob.Columns[12].Visible = true;
                        gvJob.Columns[13].Visible = true;
                        gvJob.Columns[14].Visible = true;
                        gvJob.Columns[15].Visible = false;
                        gvJob.Columns[16].Visible = false;
                        gvJob.Columns[17].Visible = false;
                        gvJob.Columns[18].Visible = true;
                        gvJob.Columns[19].Visible = true;
                        gvJob.Columns[20].Visible = true;
                        gvJob.Columns[21].Visible = false;

                        gvJob.Columns[22].Visible = true;
                        gvJob.Columns[23].Visible = true;
                        gvJob.Columns[24].Visible = true;

                        break;
                    case RoleEnum.ROOT:
                    case RoleEnum.ADMIN:
                    case RoleEnum.BUSINESS_MANAGER:
                    case RoleEnum.MARKETING:
                        gvJob.Columns[0].Visible = true;
                        gvJob.Columns[1].Visible = true;
                        gvJob.Columns[2].Visible = true;
                        gvJob.Columns[3].Visible = true;
                        gvJob.Columns[4].Visible = true;
                        gvJob.Columns[5].Visible = true;
                        gvJob.Columns[6].Visible = true;
                        gvJob.Columns[7].Visible = true;
                        gvJob.Columns[8].Visible = true;
                        gvJob.Columns[9].Visible = true;
                        gvJob.Columns[10].Visible = true;
                        gvJob.Columns[11].Visible = true;
                        gvJob.Columns[12].Visible = true;
                        gvJob.Columns[13].Visible = true;
                        gvJob.Columns[14].Visible = true;
                        gvJob.Columns[15].Visible = true;
                        gvJob.Columns[16].Visible = true;
                        gvJob.Columns[17].Visible = true;
                        gvJob.Columns[18].Visible = true;
                        gvJob.Columns[19].Visible = true;
                        gvJob.Columns[20].Visible = true;
                        gvJob.Columns[21].Visible = false;

                        gvJob.Columns[22].Visible = false;
                        gvJob.Columns[23].Visible = true;
                        gvJob.Columns[24].Visible = false;

                        break;
                    case RoleEnum.LOGIN:
                    case RoleEnum.CHEMIST:
                    case RoleEnum.SR_CHEMIST:
                    case RoleEnum.LABMANAGER:
                        gvJob.Columns[0].Visible = true;
                        gvJob.Columns[1].Visible = true;
                        gvJob.Columns[2].Visible = true;
                        gvJob.Columns[3].Visible = true;
                        gvJob.Columns[4].Visible = false;
                        gvJob.Columns[5].Visible = true;
                        gvJob.Columns[6].Visible = true;
                        gvJob.Columns[7].Visible = true;
                        gvJob.Columns[8].Visible = true;
                        gvJob.Columns[9].Visible = true;
                        gvJob.Columns[10].Visible = true;
                        gvJob.Columns[11].Visible = true;
                        gvJob.Columns[12].Visible = false;
                        gvJob.Columns[13].Visible = false;
                        gvJob.Columns[14].Visible = true;
                        gvJob.Columns[15].Visible = true;
                        gvJob.Columns[16].Visible = true;
                        gvJob.Columns[17].Visible = true;
                        gvJob.Columns[18].Visible = true;
                        gvJob.Columns[19].Visible = true;
                        gvJob.Columns[20].Visible = false;
                        gvJob.Columns[21].Visible = true;

                        gvJob.Columns[22].Visible = false;
                        gvJob.Columns[23].Visible = false;
                        gvJob.Columns[24].Visible = false;

                        break;
                    default:
                        gvJob.Columns[0].Visible = false;
                        gvJob.Columns[1].Visible = false;
                        gvJob.Columns[2].Visible = false;
                        gvJob.Columns[3].Visible = false;
                        gvJob.Columns[4].Visible = false;
                        gvJob.Columns[5].Visible = false;
                        gvJob.Columns[6].Visible = false;
                        gvJob.Columns[7].Visible = false;
                        gvJob.Columns[8].Visible = false;
                        gvJob.Columns[9].Visible = false;
                        gvJob.Columns[10].Visible = false;
                        gvJob.Columns[11].Visible = false;
                        gvJob.Columns[12].Visible = false;
                        gvJob.Columns[13].Visible = false;
                        gvJob.Columns[14].Visible = false;
                        gvJob.Columns[15].Visible = false;
                        gvJob.Columns[16].Visible = false;
                        gvJob.Columns[17].Visible = false;
                        gvJob.Columns[18].Visible = false;
                        gvJob.Columns[19].Visible = false;
                        gvJob.Columns[20].Visible = false;
                        gvJob.Columns[21].Visible = false;

                        gvJob.Columns[22].Visible = false;
                        gvJob.Columns[23].Visible = false;
                        break;
                }
            }
            else
            {
                lbTotalRecords.Text = string.Empty;
            }

        }

        private void removeSession()
        {
            txtInvoice.Text = String.Empty;
            txtPo.Text = String.Empty;
            txtReceivedReportFrom.Text = String.Empty;
            txtReceivedReportTo.Text = String.Empty;
            txtDuedateFrom.Text = String.Empty;
            txtDuedateTo.Text = String.Empty;
            txtReceivedReportFrom.Text = String.Empty;
            txtReceivedReportTo.Text = String.Empty;

            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "SearchJobRequest");
            Session.Remove(GetType().Name + "selectedList");
            Session.Remove(GetType().Name + "isPoGroupOperation");
            Session.Remove(GetType().Name + "isSentToCusDateOperation");
            Session.Remove(GetType().Name + "isNoteGroupOpeation");
            Session.Remove(GetType().Name + "isDuedateGroupOperation");
            Session.Remove(GetType().Name + "isInvoiceGroupOperation");
            Session.Remove(GetType().Name + "isCusRefNoGroupOperation");
            Session.Remove(GetType().Name + "isGroupApproveOperation");


        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (userLogin == null) Response.Redirect(Constants.LINK_LOGIN);

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void lbAddJob_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_JOB_REQUEST);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindingData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            removeSession();

            bindingData();
        }

        protected void gvJob_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            m_completion_scheduled cs = new m_completion_scheduled().SelectByID(Convert.ToInt32(CompletionScheduledEnum.NORMAL));

            this.CommandName = cmd;
            this.JobID = (cmd == CommandNameEnum.Page || cmd == CommandNameEnum.Sort) ? 0 : int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            this.SampleID = (cmd == CommandNameEnum.Page || cmd == CommandNameEnum.Sort) ? 0 : int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);

            switch (cmd)
            {
                case CommandNameEnum.Edit:
                //Server.Transfer(Constants.LINK_EDIT_SAMPLE);
                //break;
                case CommandNameEnum.View:
                    Server.Transfer(Constants.LINK_JOB_REQUEST);
                    break;
                case CommandNameEnum.ConvertTemplate:
                    Server.Transfer(Constants.LINK_JOB_CONVERT_TEMPLATE);
                    break;
                case CommandNameEnum.Workflow:
                    Server.Transfer(Constants.LINK_JOB_WORK_FLOW);
                    break;
                case CommandNameEnum.ChangeStatus:
                    Server.Transfer(Constants.LINK_JOB_CHANGE_STATUS);
                    break;
                case CommandNameEnum.ChangeDueDate:
                    Server.Transfer(Constants.LINK_JOB_CHANGE_DUEDATE);
                    break;
                case CommandNameEnum.ChangeSrChemistStartJobDate:
                    Server.Transfer(Constants.LINK_JOB_SR_CHEMIST_STARTJOB_DATE);
                    break;
                case CommandNameEnum.ChangeAdminStartJobDate:
                    Server.Transfer(Constants.LINK_JOB_ADMIN_STARTJOB_DATE);
                    break;
                case CommandNameEnum.ChangeSrChemistCompleteDate:
                    Server.Transfer(Constants.LINK_JOB_SR_CHEMIST_COMPLATE_DATE);
                    break;
                case CommandNameEnum.ChangePo:
                    Server.Transfer(Constants.LINK_JOB_CHANGE_PO);
                    break;
                case CommandNameEnum.ChangeInvoice:
                    Server.Transfer(Constants.LINK_JOB_CHANGE_INVOICE);
                    break;
                case CommandNameEnum.Print:
                    if (userLogin.role_id == Convert.ToInt32(RoleEnum.ADMIN))
                    {
                        Server.Transfer(Constants.LINK_ADMIN_PRINT);
                    }
                    else
                    {
                        Server.Transfer(Constants.LINK_JOB_PRINT_LABEL);
                    }
                    break;
                case CommandNameEnum.ChangeReportDate:
                    Server.Transfer(Constants.LINK_REPORT_DATE);
                    break;
                case CommandNameEnum.ChangeOtherRefNo:
                    Server.Transfer(Constants.LINK_CHANGE_OTHER_REF_NO);
                    break;
                case CommandNameEnum.ChangeSingaporeRefNo:
                    Server.Transfer(Constants.LINK_CHANGE_SINGAPORE_REF_NO);
                    break;
                case CommandNameEnum.Amend:
                case CommandNameEnum.Retest:
                    Server.Transfer(Constants.LINK_RETEST);
                    break;
                case CommandNameEnum.Hold:
                    if (cs != null)
                    {
                        job_sample jobSample = new job_sample().SelectByID(this.SampleID);
                        jobSample.update_date = DateTime.Now;
                        jobSample.is_hold = "1";
                        jobSample.Update();
                        //Commit
                        GeneralManager.Commit();
                        bindingData();
                    }
                    break;
                case CommandNameEnum.NoteForLab:
                    Server.Transfer(Constants.LINK_NOTE_FOR_LAB);
                    break;
                case CommandNameEnum.UnHold:
                    if (cs != null)
                    {
                        job_sample jobSample = new job_sample().SelectByID(this.SampleID);
                        jobSample.update_date = DateTime.Now;

                        jobSample.due_date = (jobSample.update_date == null) ? DateTime.Now.AddDays(cs.lab_due_date.Value) : jobSample.update_date.Value.AddDays(cs.lab_due_date.Value);
                        jobSample.due_date_customer = (jobSample.update_date == null) ? DateTime.Now.AddDays(cs.customer_due_date.Value) : jobSample.update_date.Value.AddDays(cs.customer_due_date.Value);
                        jobSample.due_date_lab = (jobSample.update_date == null) ? DateTime.Now.AddDays(cs.lab_due_date.Value) : jobSample.update_date.Value.AddDays(cs.lab_due_date.Value);

                        jobSample.is_hold = "0";
                        jobSample.job_status = Convert.ToInt16(StatusEnum.CHEMIST_TESTING);
                        jobSample.Update();
                        //Commit
                        GeneralManager.Commit();
                        bindingData();
                    }
                    break;
                case CommandNameEnum.ViewFile:
                    Server.Transfer(Constants.LINK_VIEW_FILE);
                    break;
            }

        }

        protected void gvJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }

        protected void gvJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    //0|ID,
                    //1|job_status,
                    //2|job_role,
                    //3|status_completion_scheduled,
                    //4|step1owner,
                    //5|tep2owner,
                    //6|step3owner,
                    //7|step4owner,
                    //8|step5owner,
                    //9|step6owner,
                    //10|due_date,
                    //11|is_hold,
                    //12|due_date_customer,
                    //13|due_date_lab,
                    //14|amend_count,
                    //15|retest_count,
                    //16|group_submit,
                    //17|amend_or_retest
                    GridView gv = (GridView)sender;

                    int _valueStatus = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][1]);
                    int _valueCompletion_scheduled = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][3]);
                    int _step1owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][4]);
                    int _step2owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][5]);
                    int _step3owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][6]);
                    int _step4owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][7]);
                    int _step5owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][8]);
                    int _step6owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][9]);

                    Boolean isHold = gv.DataKeys[e.Row.RowIndex][11].Equals("1") ? true : false;
                    DateTime due_date = Convert.ToDateTime(gv.DataKeys[e.Row.RowIndex][10]);
                    DateTime due_date_customer = Convert.ToDateTime(gv.DataKeys[e.Row.RowIndex][12]);
                    DateTime due_date_lab = Convert.ToDateTime(gv.DataKeys[e.Row.RowIndex][13]);

                    Boolean isGroupSubmit = Convert.ToBoolean(gv.DataKeys[e.Row.RowIndex][16]);
                    Boolean isGrp = gv.DataKeys[e.Row.RowIndex][18].Equals("GRP");


                    LinkButton btnInfo = (LinkButton)e.Row.FindControl("btnInfo");
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                    LinkButton btnConvertTemplete = (LinkButton)e.Row.FindControl("btnConvertTemplete");
                    LinkButton btnWorkFlow = (LinkButton)e.Row.FindControl("btnWorkFlow");
                    LinkButton btnChangeStatus = (LinkButton)e.Row.FindControl("btnChangeStatus");
                    LinkButton btnChangeDueDate = (LinkButton)e.Row.FindControl("btnChangeDueDate");
                    LinkButton btnChangePo = (LinkButton)e.Row.FindControl("btnChangePo");
                    LinkButton btnChangeInvoice = (LinkButton)e.Row.FindControl("btnChangeInvoice");
                    LinkButton btnPrintLabel = (LinkButton)e.Row.FindControl("btnPrintLabel");
                    LinkButton btnChangeReportDate = (LinkButton)e.Row.FindControl("btnChangeReportDate");
                    LinkButton btnChangeOtherRefNo = (LinkButton)e.Row.FindControl("btnChangeOtherRefNo");
                    LinkButton btnChangeSingaporeRefNo = (LinkButton)e.Row.FindControl("btnChangeSingaporeRefNo");
                    LinkButton btnViewFile = (LinkButton)e.Row.FindControl("btnViewFile");
                    LinkButton btnChangeSrChemistStartJobDate = (LinkButton)e.Row.FindControl("btnChangeSrChemistStartJobDate");
                    LinkButton btnChangeAdminStartJobsDate = (LinkButton)e.Row.FindControl("btnChangeAdminStartJobsDate");
                    LinkButton btnChangeSrChemistCompleteDate = (LinkButton)e.Row.FindControl("btnChangeSrChemistCompleteDate");
                    LinkButton btnNoteForLab = (LinkButton)e.Row.FindControl("btnNoteForLab");





                    LinkButton btnAmend = (LinkButton)e.Row.FindControl("btnAmend");
                    LinkButton btnReTest = (LinkButton)e.Row.FindControl("btnReTest");

                    LinkButton btnHold = (LinkButton)e.Row.FindControl("btnHold");
                    LinkButton btnUnHold = (LinkButton)e.Row.FindControl("btnUnHold");

                    Literal litStatus = (Literal)e.Row.FindControl("litStatus");

                    Literal ltPaymentStatus = (Literal)e.Row.FindControl("ltPaymentStatus");
                    Literal ltJobStatus = (Literal)e.Row.FindControl("ltJobStatus");
                    Literal litDueDate = (Literal)e.Row.FindControl("litDueDate");
                    Label lbJobNumber = (Label)e.Row.FindControl("lbJobNumber");
                    Literal litOtherRefNo = (Literal)e.Row.FindControl("litOtherRefNo");
                    Literal litIcon = (Literal)e.Row.FindControl("litIcon");
                    CheckBox cbSelect = (CheckBox)e.Row.FindControl("cbSelect");

                    #region "Check Amend/Retest"
                    int amCount = Convert.ToInt16(gv.DataKeys[e.Row.RowIndex][14]);
                    int reCount = Convert.ToInt16(gv.DataKeys[e.Row.RowIndex][15]);
                    String amendOrRetest = gv.DataKeys[e.Row.RowIndex][17] == null ? String.Empty : gv.DataKeys[e.Row.RowIndex][17].ToString();

                    switch (amendOrRetest)
                    {
                        case "AM":
                            lbJobNumber.Text = String.Format("{0}({1}{2})", lbJobNumber.Text, amendOrRetest, amCount);
                            break;
                        case "R":
                            lbJobNumber.Text = String.Format("{0}({1}{2})", lbJobNumber.Text, amendOrRetest, reCount);
                            break;
                    }
                    #endregion

                    CompletionScheduledEnum status_completion_scheduled = (CompletionScheduledEnum)Enum.ToObject(typeof(CompletionScheduledEnum), _valueCompletion_scheduled);
                    if (!ltPaymentStatus.Text.Equals(""))
                    {
                        PaymentStatus paymentStatus = (PaymentStatus)Enum.ToObject(typeof(PaymentStatus), Convert.ToInt16(ltPaymentStatus.Text));
                        ltPaymentStatus.Text = "<span class=\"label label-sm label-" + ((paymentStatus == PaymentStatus.PAYMENT_INPROCESS) ? "warning" : "success") + "\">" + Constants.GetEnumDescription(paymentStatus) + " </span>"; ;
                    }


                    StatusEnum job_status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), _valueStatus);
                    ltJobStatus.Text = Constants.GetEnumDescription(job_status);
                    RoleEnum userRole = (RoleEnum)Enum.ToObject(typeof(RoleEnum), userLogin.role_id);

                    btnInfo.Visible = (userRole == RoleEnum.LOGIN || userRole == RoleEnum.ROOT) && !isHold;
                    btnEdit.Visible = (userRole == RoleEnum.LOGIN || userRole == RoleEnum.ROOT) && !isHold;
                    btnConvertTemplete.Visible = ((userRole == RoleEnum.LOGIN || userRole == RoleEnum.ROOT) && job_status == StatusEnum.LOGIN_CONVERT_TEMPLATE) && !isHold;
                    btnChangeStatus.Visible = (userRole == RoleEnum.LOGIN || userRole == RoleEnum.ROOT) && !isHold;
                    cbSelect.Visible = false;
                    btnViewFile.Visible = job_status == StatusEnum.JOB_COMPLETE || userRole == RoleEnum.BUSINESS_MANAGER || userRole == RoleEnum.LABMANAGER || userRole == RoleEnum.SR_CHEMIST || userRole == RoleEnum.ROOT || userRole == RoleEnum.ADMIN;

                    btnChangeSrChemistStartJobDate.Visible = (userRole == RoleEnum.SR_CHEMIST) && !isHold;
                    btnChangeAdminStartJobsDate.Visible = (userRole == RoleEnum.ADMIN) && !isHold;
                    btnChangeSrChemistCompleteDate.Visible = (userRole == RoleEnum.SR_CHEMIST) && !isHold || (userRole == RoleEnum.ADMIN && isGrp);
                    switch (userRole)
                    {
                        case RoleEnum.LOGIN:
                            btnWorkFlow.Visible = (job_status == StatusEnum.LOGIN_SELECT_SPEC) && !isHold;
                            cbSelect.Visible = true;
                            break;
                        case RoleEnum.CHEMIST:
                            btnWorkFlow.Visible = (job_status == StatusEnum.CHEMIST_TESTING) && !isHold;
                            cbSelect.Visible = true;
                            break;
                        case RoleEnum.SR_CHEMIST:
                            btnWorkFlow.Visible = (job_status == StatusEnum.SR_CHEMIST_CHECKING) && !isHold;

                            cbSelect.Visible = true;
                            break;
                        case RoleEnum.LABMANAGER:
                            btnWorkFlow.Visible = (job_status == StatusEnum.LABMANAGER_CHECKING) && !isHold;
                            //switch (job_status)
                            //{
                            //    case StatusEnum.LABMANAGER_CHECKING:
                            //        cbSelect.Visible = true && isGroupSubmit;
                            //        break;
                            //}
                            cbSelect.Visible = true;

                            break;
                        case RoleEnum.ADMIN:
                            btnWorkFlow.Visible = (job_status == StatusEnum.ADMIN_CONVERT_WORD || job_status == StatusEnum.ADMIN_CONVERT_PDF) && !isHold;
                            cbSelect.Visible = true;

                            break;
                        case RoleEnum.ACCOUNT:
                            btnWorkFlow.Visible = (job_status == StatusEnum.ADMIN_CONVERT_WORD || job_status == StatusEnum.ADMIN_CONVERT_PDF) && !isHold;
                            cbSelect.Visible = true;
                            break;
                        case RoleEnum.MARKETING:
                            cbSelect.Visible = true;
                            break;
                        default:
                            btnWorkFlow.Visible = false;
                            break;
                    }
                    //btnChangeSrChemistCompleteDate.Visible = (userRole == RoleEnum.ADMIN) && isGrp;
                    btnChangeOtherRefNo.Visible = (userRole == RoleEnum.LOGIN) && !isHold;
                    btnChangeSingaporeRefNo.Visible = (userRole == RoleEnum.CHEMIST) && !isHold;
                    btnChangeDueDate.Visible = ((userRole == RoleEnum.SR_CHEMIST)) && !isHold;
                    btnChangePo.Visible = ((userRole == RoleEnum.ACCOUNT || userRole == RoleEnum.ROOT || userRole == RoleEnum.ADMIN || userRole == RoleEnum.LABMANAGER)) && !isHold;
                    btnChangeInvoice.Visible = ((userRole == RoleEnum.ACCOUNT || userRole == RoleEnum.ROOT)) && !isHold;
                    btnPrintLabel.Visible = (userRole == RoleEnum.LOGIN || userRole == RoleEnum.ROOT) && !isHold;
                    btnChangeReportDate.Visible = ((userRole == RoleEnum.ADMIN)) && !isHold;
                    btnAmend.Visible = (userRole == RoleEnum.LABMANAGER) && (job_status == StatusEnum.JOB_COMPLETE) && !isHold;
                    btnReTest.Visible = (userRole == RoleEnum.LABMANAGER) && (job_status == StatusEnum.JOB_COMPLETE) && !isHold;
                    btnHold.Visible = ((userRole == RoleEnum.LOGIN) && !isHold);
                    btnUnHold.Visible = ((userRole == RoleEnum.LOGIN) && isHold);
                    btnNoteForLab.Visible = (userRole == RoleEnum.LOGIN || userRole == RoleEnum.CHEMIST || userRole == RoleEnum.SR_CHEMIST || userRole == RoleEnum.LABMANAGER) && !isHold;
                    //btnWorkFlow.Visible = !isHold;




                    switch (userRole)
                    {
                        case RoleEnum.LOGIN:
                        case RoleEnum.CHEMIST:
                        case RoleEnum.SR_CHEMIST:
                        case RoleEnum.LABMANAGER:
                            litDueDate.Text = due_date_lab.ToString("dd MMM yyyy");
                            break;
                        case RoleEnum.ADMIN:
                        case RoleEnum.MARKETING:
                        case RoleEnum.BUSINESS_MANAGER:
                        case RoleEnum.ACCOUNT:
                            litDueDate.Text = due_date_customer.ToString("dd MMM yyyy");
                            break;
                        default:
                            litDueDate.Text = due_date_lab.ToString("dd MMM yyyy");
                            break;
                    }

                    if (DateTime.Equals(due_date_lab, new DateTime(1, 1, 1)))
                    {
                        litDueDate.Text = "TBA";
                    }
                    else { }
                    if (isHold)
                    {
                        litDueDate.Text = "-";
                    }

                    #region "Job color status"

                    switch (status_completion_scheduled)
                    {
                        case CompletionScheduledEnum.NORMAL:
                            litStatus.Text = "<span class=\"label label-sm label-success\">N </span>";
                            break;
                        case CompletionScheduledEnum.URGENT:
                            litStatus.Text = "<span class=\"label label-sm label-info\">U </span>";
                            break;
                        case CompletionScheduledEnum.EXPRESS:
                            litStatus.Text = "<span class=\"label label-sm label-warning\">E </span>";
                            break;
                        case CompletionScheduledEnum.EXTEND1:
                            litStatus.Text = "<span class=\"label label-sm label-success\">E1 </span>";
                            break;
                        case CompletionScheduledEnum.EXTEND2:
                            litStatus.Text = "<span class=\"label label-sm label-success\">E2 </span>";
                            break;
                    }

                    //jobStatus icon
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    litIcon.Visible = isGroupSubmit;
                    litIcon.Text = isGroupSubmit ? "<i class=\"fa fa-object-group\"></i>" : "";

                    switch (job_status)
                    {
                        case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                            ltJobStatus.Text = "<i class=\"fa fa-desktop\" ></i>";
                            break;
                        case StatusEnum.LOGIN_SELECT_SPEC:
                            ltJobStatus.Text = "<i class=\"fa fa-book\" ></i>";
                            break;
                        case StatusEnum.CHEMIST_TESTING:
                            ltJobStatus.Text = "<i class=\"fa fa-flask\" ></i>";
                            //cbSelect.Visible = true;
                            break;
                        case StatusEnum.SR_CHEMIST_CHECKING:
                            ltJobStatus.Text = "<i class=\"fa fa-check-square-o\" ></i>";
                            break;
                        case StatusEnum.LABMANAGER_CHECKING:
                            ltJobStatus.Text = "<i class=\"fa fa-user-md\" ></i>";

                            break;
                        case StatusEnum.ADMIN_CONVERT_WORD:
                            ltJobStatus.Text = "<i class=\"fa fa-file-word-o\" ></i>";

                            break;
                        case StatusEnum.ADMIN_CONVERT_PDF:
                            ltJobStatus.Text = "<i class=\"fa fa-file-pdf-o\" ></i>";

                            break;
                        case StatusEnum.JOB_COMPLETE:
                            e.Row.ForeColor = System.Drawing.Color.Green;
                            ltJobStatus.Text = "<i class=\"fa fa-truck\" ></i>";

                            break;
                        //case StatusEnum.JOB_HOLD:
                        //    ltJobStatus.Text = "<i class=\"fa fa-lock\" ></i>";
                        //    break;
                        case StatusEnum.JOB_CANCEL:
                            e.Row.ForeColor = System.Drawing.Color.Red;
                            ltJobStatus.Text = "<i class=\"fa fa-trash-o\"></i>";
                            break;
                        case StatusEnum.JOB_RETEST:
                            e.Row.ForeColor = System.Drawing.Color.Gray;
                            ltJobStatus.Text = "<i class=\"fa fa-retweet\"></i>";
                            break;
                        case StatusEnum.JOB_AMEND:
                            e.Row.ForeColor = System.Drawing.Color.Gray;
                            ltJobStatus.Text = "<i class=\"fa fa-wrench\"></i>";
                            break;
                    }
                    if (isHold)
                    {
                        e.Row.ForeColor = System.Drawing.Color.Gray;
                        ltJobStatus.Text = "<i class=\"fa fa-lock\" ></i>";
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        protected void gvJob_Sorting(object sender, GridViewSortEventArgs e)
        {

            Func<SearchResult, object> prop = null;
            switch (e.SortExpression)
            {
                case "ID": prop = p => p.ID; break;
                case "other_ref_no": prop = p => p.other_ref_no; break;
                case "date_srchemist_complate": prop = p => p.date_srchemist_complate; break;
                case "date_admin_sent_to_cus": prop = p => p.date_admin_sent_to_cus; break;
                case "receive_date": prop = p => p.receive_date; break;
                case "due_date": prop = p => p.due_date; break;
                case "due_date_customer": prop = p => p.due_date_customer; break;
                case "due_date_lab": prop = p => p.due_date_lab; break;
                case "job_number": prop = p => p.job_number; break;
                case "customer_ref_no": prop = p => p.customer_ref_no; break;
                case "s_pore_ref_no": prop = p => p.s_pore_ref_no; break;
                case "customer": prop = p => p.customer; break;
                case "sample_invoice": prop = p => p.sample_invoice; break;
                case "sample_invoice_date": prop = p => p.sample_invoice_date; break;
                case "sample_invoice_amount": prop = p => p.sample_invoice_amount; break;
                case "sample_po": prop = p => p.sample_po; break;
                case "contract_person": prop = p => p.contract_person; break;
                case "description": prop = p => p.description; break;
                case "model": prop = p => p.model; break;
                case "surface_area": prop = p => p.surface_area; break;
                case "specification": prop = p => p.specification; break;
                case "type_of_test": prop = p => p.type_of_test; break;
                case "customer_id": prop = p => p.customer_id; break;
                case "job_status": prop = p => p.job_status; break;
                case "create_date": prop = p => p.create_date; break;
                case "sn": prop = p => p.sn; break;
                case "remarks": prop = p => p.remarks; break;
                case "contract_person_id": prop = p => p.contract_person_id; break;
                case "job_role": prop = p => p.job_role; break;
                case "status_completion_scheduled": prop = p => p.status_completion_scheduled; break;
                case "step1owner": prop = p => p.step1owner; break;
                case "step2owner": prop = p => p.step2owner; break;
                case "step3owner": prop = p => p.step3owner; break;
                case "step4owner": prop = p => p.step4owner; break;
                case "step5owner": prop = p => p.step5owner; break;
                case "step6owner": prop = p => p.step6owner; break;
                case "job_prefix": prop = p => p.job_prefix; break;
                case "data_group": prop = p => p.data_group; break;
                case "type_of_test_id": prop = p => p.type_of_test_id; break;
                case "type_of_test_name": prop = p => p.type_of_test_name; break;
                case "spec_id": prop = p => p.spec_id; break;
                case "date_login_inprogress": prop = p => p.date_login_inprogress; break;
                case "date_chemist_analyze": prop = p => p.date_chemist_analyze; break;
                case "date_labman_complete": prop = p => p.date_labman_complete; break;
                case "is_hold": prop = p => p.is_hold; break;
                case "amend_count": prop = p => p.amend_count; break;
                case "retest_count": prop = p => p.retest_count; break;
                case "group_submit": prop = p => p.group_submit; break;
                case "status_name": prop = p => p.status_name; break;
                case "sample_prefix": prop = p => p.sample_prefix; break;
                case "amend_or_retest": prop = p => p.amend_or_retest; break;
                case "note": prop = p => p.note; break;
                case "note_lab": prop = p => p.note_lab; break;
                case "am_retest_remark": prop = p => p.am_retest_remark; break;
                case "sample_invoice_status": prop = p => p.sample_invoice_status; break;
                case "fisicalY": prop = p => p.fisicalY; break;

            }

            Func<IEnumerable<SearchResult>, Func<SearchResult, object>, IEnumerable<SearchResult>> func = null;

            



            switch (SortDirection)
            {
                case SortDirection.Ascending:
                    {
                        func = (c, p) => c.OrderBy(p);
                        SortDirection = SortDirection.Descending;
                        break;
                    }
                case SortDirection.Descending:
                    {
                        func = (c, p) => c.OrderByDescending(p);
                        SortDirection = SortDirection.Ascending;

                        break;
                    }
            }


            IEnumerable<SearchResult> persons = this.searchResult;
            persons = func(persons, prop);

            gvJob.DataSource = persons.ToArray();
            gvJob.DataBind();

        }


        protected void btnElp_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            hPrefix.Value = btn.Text.ToUpper();
            btnElp.CssClass = (btnElp.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnEln.CssClass = (btnEln.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";

            btnEls.CssClass = (btnEls.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnFa.CssClass = (btnFa.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnElwa.CssClass = (btnElwa.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnGrp.CssClass = (btnGrp.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnTrb.CssClass = (btnTrb.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";

            bindingData();
            Console.WriteLine();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ExportToExcel()
        {

            RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);

            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("DT");

                switch (userRole)
                {
                    case RoleEnum.ADMIN:
                    case RoleEnum.ACCOUNT:
                    case RoleEnum.BUSINESS_MANAGER:
                    case RoleEnum.MARKETING:
                        dt.Columns.Add("Job_Type", typeof(string));
                        dt.Columns.Add("Status", typeof(string));
                        dt.Columns.Add("Job_Status", typeof(string));
                        dt.Columns.Add("Received", typeof(DateTime));
                        dt.Columns.Add("Report_Sent_to_Customer", typeof(DateTime));
                        dt.Columns.Add("Receive_Date", typeof(DateTime));
                        dt.Columns.Add("Due_Date", typeof(DateTime));
                        dt.Columns.Add("TBA_FLAG", typeof(string));
                        dt.Columns.Add("ALS_Ref", typeof(string));
                        dt.Columns.Add("No_Cus_Ref_No", typeof(string));
                        dt.Columns.Add("Other_Ref_No", typeof(string));
                        dt.Columns.Add("Company", typeof(string));
                        dt.Columns.Add("Invoice", typeof(string));
                        dt.Columns.Add("Po", typeof(string));
                        dt.Columns.Add("Contact", typeof(string));
                        dt.Columns.Add("Description", typeof(string));
                        dt.Columns.Add("Model", typeof(string));
                        dt.Columns.Add("Surface_Area", typeof(string));
                        dt.Columns.Add("Specification", typeof(string));
                        dt.Columns.Add("Type_of_test", typeof(string));
                        dt.Columns.Add("Data_Group", typeof(string));
                        dt.Columns.Add("date_login_complete", typeof(DateTime));
                        dt.Columns.Add("date_chemist_complete", typeof(DateTime));
                        dt.Columns.Add("date_srchemist_complate", typeof(DateTime));
                        dt.Columns.Add("date_admin_word_complete", typeof(DateTime));
                        dt.Columns.Add("date_labman_complete", typeof(DateTime));
                        dt.Columns.Add("date_admin_pdf_complete", typeof(DateTime));
                        dt.Columns.Add("Note_for_Admin_Account", typeof(string));
                        dt.Columns.Add("Remark_AM_Retest", typeof(string));
                        dt.Columns.Add("Invoice_Date", typeof(DateTime));
                        dt.Columns.Add("Invoice_Amount", typeof(double));
                        dt.Columns.Add("Invoice_status", typeof(string));
                        dt.Columns.Add("remarks", typeof(string));


                        break;
                    case RoleEnum.LOGIN:
                    case RoleEnum.CHEMIST:
                    case RoleEnum.SR_CHEMIST:
                    case RoleEnum.LABMANAGER:
                        dt.Columns.Add("Job_Type", typeof(string));
                        dt.Columns.Add("Status", typeof(string));
                        dt.Columns.Add("Job_Status", typeof(string));
                        dt.Columns.Add("Received", typeof(DateTime));
                        dt.Columns.Add("Report_Sent_to_Customer", typeof(DateTime));
                        dt.Columns.Add("Receive_Date", typeof(DateTime));
                        dt.Columns.Add("Due_Date", typeof(DateTime));
                        dt.Columns.Add("TBA_FLAG", typeof(string));
                        dt.Columns.Add("ALS_Ref", typeof(string));
                        dt.Columns.Add("No_Cus_Ref_No", typeof(string));
                        dt.Columns.Add("Other_Ref_No", typeof(string));
                        dt.Columns.Add("Company", typeof(string));
                        dt.Columns.Add("Description", typeof(string));
                        dt.Columns.Add("Model", typeof(string));
                        dt.Columns.Add("Surface_Area", typeof(string));
                        dt.Columns.Add("Specification", typeof(string));
                        dt.Columns.Add("Type_of_test", typeof(string));
                        dt.Columns.Add("Data_Group", typeof(string));
                        dt.Columns.Add("date_login_complete", typeof(DateTime));
                        dt.Columns.Add("date_chemist_complete", typeof(DateTime));
                        dt.Columns.Add("date_srchemist_complate", typeof(DateTime));
                        dt.Columns.Add("date_admin_word_complete", typeof(DateTime));
                        dt.Columns.Add("date_labman_complete", typeof(DateTime));
                        dt.Columns.Add("date_admin_pdf_complete", typeof(DateTime));
                        dt.Columns.Add("Note_for_lab", typeof(string));
                        dt.Columns.Add("Remark_AM_Retest", typeof(string));
                        dt.Columns.Add("remarks", typeof(string));

                        break;
                }
                String conSQL = Configurations.MySQLCon;
                using (MySqlConnection conn = new MySqlConnection("server = " + conSQL.Split(';')[2].Split('=')[2] + "; " + conSQL.Split(';')[3] + "; " + conSQL.Split(';')[4] + "; " + conSQL.Split(';')[5]))
                {
                    conn.Open();
                    String sql = "SELECT ";
                    switch (userRole)
                    {
                        case RoleEnum.ADMIN:
                        case RoleEnum.ACCOUNT:
                        case RoleEnum.BUSINESS_MANAGER:
                        case RoleEnum.MARKETING:
                            sql += "Job_Type" +
                                   ",Status" +
                                   ",Job_Status" +
                                   ",Received" +
                                   ",Report_Sent_to_Customer" +
                                   ",Receive_Date" +
                                   ",Due_Date" +
                                   ",TBA_FLAG" +
                                   ",ALS_Ref" +
                                   ",No_Cus_Ref_No" +
                                   ",Other_Ref_No" +
                                   ",Company" +
                                   ",Invoice" +
                                   ",Po" +
                                   ",Contact" +
                                   ",Description" +
                                    ",Model" +
                                    ",Surface_Area" +
                                   ",Specification" +
                                   ",Type_of_test" +
                                   ",Data_Group" +
                                   ",date_login_complete" +
                                   ",date_chemist_complete" +
                                   ",date_srchemist_complate" +
                                   ",date_admin_word_complete" +
                                   ",date_labman_complete" +
                                   ",date_admin_pdf_complete" +
                                   ",Note_for_Admin_Account,Remark_AM_Retest,Invoice_Date,Invoice_Amount,Invoice_status,remarks";
                            break;
                        case RoleEnum.LOGIN:
                        case RoleEnum.CHEMIST:
                        case RoleEnum.SR_CHEMIST:
                        case RoleEnum.LABMANAGER:
                            sql += "Job_Type" +
                                    ",Status" +
                                    ",Job_Status" +
                                    ",Received" +
                                    ",Report_Sent_to_Customer" +
                                    ",Receive_Date" +
                                    ",Due_Date" +
                                    ",TBA_FLAG" +
                                    ",ALS_Ref" +
                                    ",No_Cus_Ref_No" +
                                    ",Other_Ref_No" +
                                    ",Company" +
                                    ",Description" +
                                    ",Model" +
                                    ",Surface_Area" +
                                    ",Specification" +
                                    ",Type_of_test" +
                                    ",Data_Group" +
                                    ",date_login_complete" +
                                    ",date_chemist_complete" +
                                    ",date_srchemist_complate" +
                                    ",date_admin_word_complete" +
                                    ",date_labman_complete" +
                                    ",date_admin_pdf_complete" +
                                    ",Note_for_lab,Remark_AM_Retest,remarks";
                            break;
                        default:
                            break;
                    }
                    sql += " FROM (SELECT      `Extent2`.`remarks`,                                                                                                                                        ";
                    sql += " `Extent2`.`sample_prefix` AS `Job_Type`,                                                                                                           ";
                    sql += " `Extent7`.`name` AS `Status`,                                                                                                                      ";
                    sql += " (CASE WHEN `Extent2`.`is_hold` = '1' THEN 'Hold' ELSE `Extent9`.`name` END) AS `Job_Status`,                                                       ";
                    sql += " `Extent2`.`date_srchemist_complate` AS `Received`,                                                                                                 ";
                    sql += " `Extent2`.`date_admin_sent_to_cus` AS `Report_Sent_to_Customer`,                                                                                   ";
                    sql += " `Extent1`.`date_of_receive` AS `Receive_Date`,                                                                                                     ";
                    switch (userRole)
                    {
                        case RoleEnum.LOGIN:
                        case RoleEnum.CHEMIST:
                        case RoleEnum.SR_CHEMIST:
                        case RoleEnum.LABMANAGER:
                            sql += "(case when `Extent2`.`due_date_lab` = '0001-01-01' then '0001-01-01' else DATE_FORMAT(`Extent2`.`due_date_lab`,'%e %b %Y') end) AS `Due_Date`,";
                            sql += "(case when `Extent2`.`due_date_lab` = '0001-01-01' then 'TBA' else '' end) AS `TBA_FLAG`,";

                            break;
                        case RoleEnum.ADMIN:
                        case RoleEnum.MARKETING:
                        case RoleEnum.BUSINESS_MANAGER:
                            sql += "(CASE WHEN ISNULL(`Extent2`.`due_date_lab`) THEN '0001-01-01' ELSE (case when `Extent2`.`due_date_lab` = '0001-01-01' then '0001-01-01' else (case when `Extent2`.`due_date_customer` = '0001-01-01' then '0001-01-01' else DATE_FORMAT(`Extent2`.`due_date_customer`,'%e %b %Y') end) end) END) AS `Due_Date`,";
                            sql += "(CASE WHEN ISNULL(`Extent2`.`due_date_lab`) THEN 'TBA' ELSE (case when `Extent2`.`due_date_lab` = '0001-01-01' then 'TBA' else (case when `Extent2`.`due_date_customer` = '0001-01-01' then 'TBA' else '' end) end) END) AS `TBA_FLAG`,";


                            break;
                        default:
                            sql += "(case when `Extent2`.`due_date_lab` = '0001-01-01' then '0001-01-01' else DATE_FORMAT(`Extent2`.`due_date_lab`,'%e %b %Y') end) AS `Due_Date`,";
                            sql += "(case when `Extent2`.`due_date_lab` = '0001-01-01' then 'TBA' else '' end) AS `TBA_FLAG`,";

                            break;
                    }

                    //sql += " (CASE WHEN `Extent2`.`due_date_lab` = '0001-01-01' THEN '0001-01-01' ELSE DATE_FORMAT(`Extent2`.`due_date_lab`, '%e %b %Y') END) AS `Due_Date`,    ";
                    //sql += " (CASE WHEN `Extent2`.`due_date_lab` = '0001-01-01' THEN 'TBA' ELSE '' END) AS `TBA_FLAG`,                                                          ";
                    sql += " (CASE                                                                                                                                              ";
                    sql += "     WHEN ISNULL(`Extent2`.`amend_or_retest`) THEN `Extent2`.`job_number`                                                                           ";
                    sql += "     ELSE CONCAT(`Extent2`.`job_number`,                                                                                                            ";
                    sql += "             (CASE                                                                                                                                  ";
                    sql += "                 WHEN                                                                                                                               ";
                    sql += "                     `Extent2`.`amend_or_retest` = 'AM'                                                                                             ";
                    sql += "                 THEN                                                                                                                               ";
                    sql += "                     CONCAT('(',                                                                                                                    ";
                    sql += "                             `Extent2`.`amend_or_retest`,                                                                                           ";
                    sql += "                             `Extent2`.`amend_count`,                                                                                               ";
                    sql += "                             ')')                                                                                                                   ";
                    sql += "                 ELSE CONCAT('(',                                                                                                                   ";
                    sql += "                         `Extent2`.`amend_or_retest`,                                                                                               ";
                    sql += "                         `Extent2`.`retest_count`,                                                                                                  ";
                    sql += "                         ')')                                                                                                                       ";
                    sql += "             END))                                                                                                                                  ";
                    sql += " END) AS `ALS_Ref`,                                                                                                                                 ";
                    sql += " `Extent1`.`customer_ref_no` AS `No_Cus_Ref_No`,                                                                                                    ";
                    sql += " `Extent2`.`other_ref_no` AS `Other_Ref_No`,                                                                                                        ";
                    sql += " `Extent5`.`company_name` AS `Company`,                                                                                                             ";
                    sql += " `Extent2`.`sample_invoice` AS `Invoice`,                                                                                                           ";
                    sql += " `Extent2`.`sample_invoice_date` AS `Invoice_Date`,                                                                                                           ";
                    sql += " `Extent2`.`sample_invoice_amount` AS `Invoice_Amount`,                                                                                                           ";
                    //sql += " `Extent2`.`sample_invoice_status` AS `Invoice_status`,                                                                                                           ";
                    sql += " (CASE WHEN `Extent2`.`sample_invoice_status` = '1' THEN 'In Process' ELSE 'Complete' END) AS `Invoice_status`,                                                       ";

                    sql += " `Extent2`.`sample_po` AS `Po`,                                                                                                                     ";
                    sql += " `Extent6`.`name` AS `Contact`,                                                                                                                     ";
                    sql += " `Extent2`.`description` AS `Description`,                                                                                                          ";
                    sql += " `Extent2`.`model` AS Model,                                                                                                                        ";
                    sql += " `Extent2`.`surface_area` AS `Surface_Area`,                                                                                                        ";
                    sql += " `Extent3`.`name` AS `Specification`,                                                                                                               ";
                    sql += " `Extent4`.`name` AS `Type_of_test`,                                                                                                                ";
                    sql += " `Extent4`.`data_group` AS `Data_Group`,                                                                                                            ";
                    sql += " `Extent2`.`date_login_inprogress` AS `date_login_inprogress`,                                                                                      ";
                    sql += " `Extent2`.`date_login_complete` AS `date_login_complete`,                                                                                          ";
                    sql += " `Extent2`.`date_chemist_analyze` AS `date_chemist_inprogress`,                                                                                     ";
                    sql += " `Extent2`.`date_chemist_complete` AS `date_chemist_complete`,                                                                                      ";
                    sql += " `Extent2`.`date_srchemist_analyze` AS `date_srchemist_inprogress`,                                                                                 ";
                    sql += " `Extent2`.`date_srchemist_complate` AS `date_srchemist_complate`,                                                                                  ";
                    sql += " `Extent2`.`date_admin_word_inprogress` AS `date_admin_word_inprogress`,                                                                            ";
                    sql += " `Extent2`.`date_admin_word_complete` AS `date_admin_word_complete`,                                                                                ";
                    sql += " `Extent2`.`date_labman_analyze` AS `date_labman_inprogress`,                                                                                       ";
                    sql += " `Extent2`.`date_labman_complete` AS `date_labman_complete`,                                                                                        ";
                    sql += " `Extent2`.`date_admin_pdf_inprogress` AS `date_admin_pdf_inprogress`,                                                                              ";
                    sql += " `Extent2`.`date_admin_pdf_complete` AS `date_admin_pdf_complete`,                                                                                  ";
                    sql += " `Extent2`.`note_lab` AS `Note_for_lab`,                                                                                                             ";
                    sql += " `Extent2`.`note` AS `Note_for_Admin_Account`,                                                                                                             ";

                    sql += " `Extent1`.`date_of_receive`,";


                    //sql += " YEAR(`Extent1`.`date_of_receive`) AS physicalYear,";
                    sql += " `Extent2`.`am_retest_remark` AS `Remark_AM_Retest`,";
                    sql += " (case when  MONTH(`Extent1`.`date_of_receive`) <4 then YEAR(`Extent1`.`date_of_receive`)-1 else YEAR(`Extent1`.`date_of_receive`) end) as fisYear";
                    //sql += " `Extent2`.`job_status` AS `job_status_id`,";
                    //sql += " `Extent5`.`company_name`,`Extent3`.`id` as spec_id,`Extent7`.`id` as jstatus_id,`Extent2`.`job_number`,`Extent2`.`sample_po`,`Extent2`.`sample_invoice`,`Extent2`.`due_date_customer`,`Extent2`.`due_date_lab`,`Extent2`.`date_admin_sent_to_cus`,`Extent2`.`ID` sample_id";
                    sql += " FROM `job_info` AS `Extent1`                                                                                                                        ";
                    sql += " INNER JOIN `job_sample` AS `Extent2` ON `Extent1`.`ID` = `Extent2`.`job_id`                                                                         ";
                    sql += " LEFT OUTER JOIN `m_status` AS `Extent7` ON `Extent2`.`job_status` = `Extent7`.`ID`                                                                  ";
                    sql += " INNER JOIN `m_specification` AS `Extent3` ON `Extent2`.`specification_id` = `Extent3`.`ID`                                                          ";
                    sql += " INNER JOIN `m_type_of_test` AS `Extent4` ON `Extent2`.`type_of_test_id` = `Extent4`.`ID`                                                            ";
                    sql += " INNER JOIN `m_customer` AS `Extent5` ON `Extent1`.`customer_id` = `Extent5`.`ID`                                                                    ";
                    sql += " INNER JOIN `m_customer_contract_person` AS `Extent6` ON `Extent1`.`contract_person_id` = `Extent6`.`ID`                                             ";
                    sql += " LEFT OUTER JOIN `users_login` AS `Extent8` ON `Extent2`.`update_by` = `Extent8`.`ID`                                                                ";
                    sql += " LEFT OUTER JOIN `m_completion_scheduled` AS `Extent9` ON `Extent2`.`status_completion_scheduled` = `Extent9`.`ID`                                    ";
                    StringBuilder sqlCri = new StringBuilder();

                    sqlCri.Append(" (case when  MONTH(`Extent1`.`date_of_receive`) <4 then YEAR(`Extent1`.`date_of_receive`)-1 else YEAR(`Extent1`.`date_of_receive`) end)= '" + ddlPhysicalYear.SelectedValue + "'");
                    sqlCri.Append(" AND ");
                    sqlCri.Append(" `Extent2`.`job_status` <> 0");
                    sqlCri.Append(" AND ");

                    if (!String.IsNullOrEmpty(ddlBoiNonBoi.SelectedValue.ToString()))
                    {
                        if (ddlBoiNonBoi.SelectedValue.ToString().Equals("NB"))
                        {
                            sqlCri.Append(" RIGHT(`Extent2`.`job_number`, 1)  <> 'B'");
                            sqlCri.Append(" AND ");

                        }
                        else
                        {
                            sqlCri.Append(" RIGHT(`Extent2`.`job_number`, 1)  = 'B'");
                            sqlCri.Append(" AND ");
                        }
                    }

                    if (!String.IsNullOrEmpty(ddlTypeOfTest.SelectedValue))
                    {
                        sqlCri.Append(" `Extent4`.`data_group` = '" + ddlTypeOfTest.SelectedValue + "'");
                        sqlCri.Append(" AND ");
                    }
                    if (Convert.ToInt16(ddlCompany.SelectedValue) > 0)
                    {
                        if (!String.IsNullOrEmpty(ddlCompany.SelectedItem.Text))
                        {
                            sqlCri.Append(" `Extent5`.`company_name` like '%" + ddlCompany.SelectedItem.Text + "%'");
                            sqlCri.Append(" AND ");
                        }
                    }
                    if (!String.IsNullOrEmpty(ddlSpecification.SelectedValue))
                    {
                        sqlCri.Append(" `Extent3`.`id` = " + Convert.ToInt16(ddlSpecification.SelectedValue));
                        sqlCri.Append(" AND ");
                    }
                    if (!String.IsNullOrEmpty(ddlJobStatus.SelectedValue))
                    {
                        sqlCri.Append(" `Extent7`.`id` = " + Convert.ToInt16(ddlJobStatus.SelectedValue));
                        sqlCri.Append(" AND ");
                    }
                    if (!String.IsNullOrEmpty(txtREfNo.Text))
                    {
                        sqlCri.Append(" `Extent2`.`job_number` like '%" + txtREfNo.Text.Trim() + "%'");
                        sqlCri.Append(" AND ");

                    }
                    if (!String.IsNullOrEmpty(txtPo.Text))
                    {
                        sqlCri.Append(" `Extent2`.`sample_po` like '%" + txtPo.Text + "%'");
                        sqlCri.Append(" AND ");
                    }
                    if (!String.IsNullOrEmpty(txtInvoice.Text))
                    {
                        sqlCri.Append(" `Extent2`.`sample_invoice` like '%" + txtInvoice.Text + "%'");
                        sqlCri.Append(" AND ");
                    }
                    DateTime receive_report_from = String.IsNullOrEmpty(txtReceivedReportFrom.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtReceivedReportFrom.Text);
                    DateTime receive_report_to = String.IsNullOrEmpty(txtReceivedReportTo.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtReceivedReportTo.Text);
                    if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
                    {
                        sqlCri.Append(" `Extent1`.`date_of_receive` between'" + receive_report_from.ToString("yyyy-MM-dd") + "' AND '" + receive_report_to.ToString("yyyy-MM-dd") + "'");
                        sqlCri.Append(" AND ");
                    }
                    DateTime duedate_from = String.IsNullOrEmpty(txtDuedateFrom.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtDuedateFrom.Text);
                    DateTime duedate_to = String.IsNullOrEmpty(txtDuedateTo.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtDuedateTo.Text);
                    if (duedate_from != DateTime.MinValue && duedate_to != DateTime.MinValue)
                    {
                        switch (userRole)
                        {
                            case RoleEnum.LOGIN:
                            case RoleEnum.CHEMIST:
                            case RoleEnum.SR_CHEMIST:
                            case RoleEnum.LABMANAGER:
                                sqlCri.Append(" `Extent2`.`due_date_lab` between '" + duedate_from.ToString("yyyy-MM-dd") + "' AND '" + duedate_to.ToString("yyyy-MM-dd") + "'");
                                break;
                            case RoleEnum.ADMIN:
                            case RoleEnum.MARKETING:
                            case RoleEnum.BUSINESS_MANAGER:
                                sqlCri.Append(" `Extent2`.`due_date_customer` between '" + duedate_from.ToString("yyyy-MM-dd") + "' AND '" + duedate_to.ToString("yyyy-MM-dd") + "'");
                                break;
                            default:
                                sqlCri.Append(" `Extent2`.`due_date_lab` between '" + duedate_from.ToString("yyyy-MM-dd") + "' AND '" + duedate_to.ToString("yyyy-MM-dd") + "'");
                                break;
                        }
                        sqlCri.Append(" AND ");
                    }
                    DateTime report_to_customer_from = String.IsNullOrEmpty(txtReportToCustomerFrom.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtReportToCustomerFrom.Text);
                    DateTime report_to_customer_to = String.IsNullOrEmpty(txtReportToCustomerTo.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtReportToCustomerTo.Text);
                    if (report_to_customer_from != DateTime.MinValue && report_to_customer_to != DateTime.MinValue)
                    {
                        sqlCri.Append(" `Extent2`.`date_admin_sent_to_cus` between '" + report_to_customer_from.ToString("yyyy-MM-dd") + "' AND '" + report_to_customer_to.ToString("yyyy-MM-dd") + "'");
                        sqlCri.Append(" AND ");
                    }
                    sql += (sqlCri.ToString().Length > 0) ? " WHERE " + sqlCri.ToString().Substring(0, sqlCri.ToString().Length - 5) : "";
                    sql += " ORDER BY `Extent2`.`ID` DESC ) TMP";



                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    MySqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    Console.WriteLine();
                }
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=jobList_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            ExportToExcel();
        }

        protected void btnOperation_Click(object sender, EventArgs e)
        {
            RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);
            //btnOpera
            List<int> listOfNoGroup = new List<int>();
            LinkButton btn = (LinkButton)sender;

            this.isPoGroupOperation = btn.ID.Equals("btnOperationPo");
            this.isDuedateGroupOperation = btn.ID.Equals("btnOperationDueDate");
            this.isInvoiceGroupOperation = btn.ID.Equals("btnOperationGroupInvoice");
            this.isSentToCusDateOperation = btn.ID.Equals("btnOperationSentToCus");
            this.isNoteGroupOpeation = btn.ID.Equals("btnOperationNote");
            this.isCusRefNoGroupOperation = btn.ID.Equals("btnOperationCusRefNo");
            this.isGroupApproveOperation = userRole == RoleEnum.LABMANAGER ? btn.ID.Equals("btnOperation") : false;

            foreach (GridViewRow row in gvJob.Rows)
            {
                CheckBox chk = row.Cells[1].Controls[1] as CheckBox;

                if (chk != null && chk.Checked)
                {
                    HiddenField hf = row.Cells[1].Controls[3] as HiddenField;
                    HiddenField hIsGroup = row.Cells[1].Controls[5] as HiddenField;

                    if (this.isPoGroupOperation || this.isDuedateGroupOperation || this.isInvoiceGroupOperation || this.isSentToCusDateOperation || this.isNoteGroupOpeation || this.isCusRefNoGroupOperation || userRole == RoleEnum.LOGIN || userRole == RoleEnum.CHEMIST || this.isGroupApproveOperation)
                    {
                        this.selectedList.Add(Convert.ToInt32(hf.Value));
                    }
                    else
                    {
                        if ("1".Equals(hIsGroup.Value))
                        {
                            this.selectedList.Add(Convert.ToInt32(hf.Value));
                        }
                        else
                        {
                            listOfNoGroup.Add(Convert.ToInt32(hf.Value));
                        }
                    }
                }
            }
            Server.Transfer(Constants.LINK_CHANGE_JOB_GROUP);
        }


    }
}