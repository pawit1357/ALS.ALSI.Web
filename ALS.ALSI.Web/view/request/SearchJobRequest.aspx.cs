using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using NPOI.HSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "SearchJobRequest"]; }
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
                return tmp;
            }
        }
        #endregion

        #region "Method"

        private void initialPage()
        {
            this.selectedList = new List<int>();

            ddlCompany.Items.Clear();
            ddlCompany.DataSource = new m_customer().SelectAll().OrderBy(x => x.company_name);
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            ddlJobStatus.Items.Clear();
            ddlJobStatus.DataSource = new m_status().SelectByMainStatus();
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
            btnOperationNote.Visible = (userRole == RoleEnum.ADMIN || userRole == RoleEnum.ACCOUNT);
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
                    case RoleEnum.ADMIN:
                        gvJob.Columns[4].Visible = true;
                        gvJob.Columns[5].Visible = true;
                        gvJob.Columns[12].Visible = true;
                        gvJob.Columns[13].Visible = true;
                        gvJob.Columns[20].Visible = true;
                        gvJob.Columns[21].Visible = false;
                        break;
                    case RoleEnum.LOGIN:
                    case RoleEnum.CHEMIST:
                    case RoleEnum.SR_CHEMIST:
                    case RoleEnum.LABMANAGER:
                        gvJob.Columns[4].Visible = false;
                        gvJob.Columns[5].Visible = false;
                        gvJob.Columns[12].Visible = false;
                        gvJob.Columns[13].Visible = false;
                        gvJob.Columns[20].Visible = false;
                        gvJob.Columns[21].Visible = true;
                        break;
                    default:
                        gvJob.Columns[4].Visible = false;
                        gvJob.Columns[5].Visible = false;
                        gvJob.Columns[12].Visible = false;
                        gvJob.Columns[13].Visible = false;
                        gvJob.Columns[20].Visible = false;
                        gvJob.Columns[21].Visible = false;
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
            this.JobID = (cmd == CommandNameEnum.Page) ? 0 : int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
            this.SampleID = (cmd == CommandNameEnum.Page) ? 0 : int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);

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
                        jobSample.due_date = (jobSample.update_date == null) ? DateTime.Now.AddDays(cs.value.Value) : jobSample.update_date.Value.AddDays(cs.value.Value);
                        jobSample.due_date_customer = (jobSample.update_date == null) ? DateTime.Now.AddDays(cs.lab_due_date.Value) : jobSample.update_date.Value.AddDays(cs.lab_due_date.Value);
                        jobSample.due_date_lab = (jobSample.update_date == null) ? DateTime.Now.AddDays(cs.customer_due_date.Value) : jobSample.update_date.Value.AddDays(cs.customer_due_date.Value);
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

                    StatusEnum job_status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), _valueStatus);
                    ltJobStatus.Text = Constants.GetEnumDescription(job_status);
                    RoleEnum userRole = (RoleEnum)Enum.ToObject(typeof(RoleEnum), userLogin.role_id);

                    btnInfo.Visible = (userRole == RoleEnum.LOGIN || userRole == RoleEnum.ROOT) && !isHold;
                    btnEdit.Visible = (userRole == RoleEnum.LOGIN || userRole == RoleEnum.ROOT) && !isHold;
                    btnConvertTemplete.Visible = ((userRole == RoleEnum.LOGIN || userRole == RoleEnum.ROOT) && job_status == StatusEnum.LOGIN_CONVERT_TEMPLATE) && !isHold;
                    btnChangeStatus.Visible = (userRole == RoleEnum.LOGIN || userRole == RoleEnum.ROOT) && !isHold;
                    cbSelect.Visible = false;
                    btnViewFile.Visible = job_status == StatusEnum.JOB_COMPLETE || userRole == RoleEnum.BUSINESS_MANAGER || userRole == RoleEnum.LABMANAGER || userRole == RoleEnum.SR_CHEMIST;

                    btnChangeSrChemistStartJobDate.Visible = (userRole == RoleEnum.SR_CHEMIST) && !isHold;
                    btnChangeAdminStartJobsDate.Visible = (userRole == RoleEnum.ADMIN) && !isHold;
                    btnChangeSrChemistCompleteDate.Visible = (userRole == RoleEnum.SR_CHEMIST) && !isHold || (userRole == RoleEnum.ADMIN && isGrp);
                    switch (userRole)
                    {
                        case RoleEnum.LOGIN:
                            btnWorkFlow.Visible = (job_status == StatusEnum.LOGIN_SELECT_SPEC) && !isHold;
                            cbSelect.Visible = true;

                            //switch (job_status)
                            //{
                            //    case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                            //        cbSelect.Visible = true;
                            //        break;
                            //}
                            break;
                        case RoleEnum.CHEMIST:
                            btnWorkFlow.Visible = (job_status == StatusEnum.CHEMIST_TESTING) && !isHold;
                            switch (job_status)
                            {
                                case StatusEnum.CHEMIST_TESTING:
                                    cbSelect.Visible = true;
                                    break;
                            }
                            break;
                        case RoleEnum.SR_CHEMIST:
                            btnWorkFlow.Visible = (job_status == StatusEnum.SR_CHEMIST_CHECKING) && !isHold;

                            cbSelect.Visible = true;
                            break;
                        case RoleEnum.LABMANAGER:
                            btnWorkFlow.Visible = (job_status == StatusEnum.LABMANAGER_CHECKING) && !isHold;
                            switch (job_status)
                            {
                                case StatusEnum.LABMANAGER_CHECKING:
                                    cbSelect.Visible = true && isGroupSubmit;
                                    break;
                            }
                            break;
                        case RoleEnum.ADMIN:
                            btnWorkFlow.Visible = (job_status == StatusEnum.ADMIN_CONVERT_WORD || job_status == StatusEnum.ADMIN_CONVERT_PDF) && !isHold;
                            cbSelect.Visible = true;

                            break;
                        case RoleEnum.ACCOUNT:
                            btnWorkFlow.Visible = (job_status == StatusEnum.ADMIN_CONVERT_WORD || job_status == StatusEnum.ADMIN_CONVERT_PDF) && !isHold;
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
                    btnNoteForLab.Visible = (userRole == RoleEnum.LOGIN||userRole == RoleEnum.CHEMIST || userRole == RoleEnum.SR_CHEMIST || userRole == RoleEnum.LABMANAGER) && !isHold;
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
                    else
                    {

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
                    }

                    //jobStatus icon
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    litIcon.Visible = isGroupSubmit;
                    litIcon.Text = isGroupSubmit ? "<i class=\"fa fa-object-group\"></i>" : "";

                    //cbSelect.Visible = (job_status == StatusEnum.CHEMIST_TESTING ||
                    //    job_status == StatusEnum.CHEMIST_TESTING ||
                    //    job_status == StatusEnum.SR_CHEMIST_CHECKING ||
                    //    job_status == StatusEnum.LABMANAGER_CHECKING ||
                    //     job_status == StatusEnum.ADMIN_CONVERT_WORD ||
                    //      job_status == StatusEnum.ADMIN_CONVERT_PDF);



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

            //switch (btn.ID)
            //{
            //    case "btnElp":
            //        hPrefix.Value = "1";
            //        break;
            //    case "btnEls":
            //        hPrefix.Value = "2";
            //        break;
            //    case "btnFa":
            //        hPrefix.Value = "3";
            //        break;
            //    case "btnElwa":
            //        hPrefix.Value = "4";
            //        break;
            //    case "btnGrp":
            //        hPrefix.Value = "5";
            //        break;
            //    case "btnTrb":
            //        hPrefix.Value = "6";
            //        break;
            //    case "btnEln":
            //        hPrefix.Value = "7";

            //        break;
            //}
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
                dt.Columns.Add("Job Type", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Job Status", typeof(string));
                dt.Columns.Add("Received", typeof(DateTime));
                dt.Columns.Add("Report Sent to Customer", typeof(DateTime));
                dt.Columns.Add("Receive Date", typeof(DateTime));
                dt.Columns.Add("Due Date", typeof(DateTime));
                dt.Columns.Add("TBA FLAG", typeof(string));
                dt.Columns.Add("ALS Ref", typeof(string));
                dt.Columns.Add("No.Cus Ref No", typeof(string));
                dt.Columns.Add("Other Ref No", typeof(string));
                dt.Columns.Add("Company", typeof(string));
                dt.Columns.Add("Invoice", typeof(string));
                dt.Columns.Add("Po", typeof(string));
                dt.Columns.Add("Contact", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Model", typeof(string));
                dt.Columns.Add("Surface Area", typeof(string));
                dt.Columns.Add("Specification", typeof(string));
                dt.Columns.Add("Type of test", typeof(string));
                dt.Columns.Add("Data Group", typeof(string));
                dt.Columns.Add("date_login_inprogress", typeof(DateTime));
                dt.Columns.Add("date_login_complete", typeof(DateTime));
                dt.Columns.Add("date_chemist_inprogress", typeof(DateTime));
                dt.Columns.Add("date_chemist_complete", typeof(DateTime));
                dt.Columns.Add("date_srchemist_inprogress", typeof(DateTime));
                dt.Columns.Add("date_srchemist_complate", typeof(DateTime));
                dt.Columns.Add("date_admin_word_inprogress", typeof(DateTime));
                dt.Columns.Add("date_admin_word_complete", typeof(DateTime));
                dt.Columns.Add("date_labman_inprogress", typeof(DateTime));
                dt.Columns.Add("date_labman_complete", typeof(DateTime));
                dt.Columns.Add("date_admin_pdf_inprogress", typeof(DateTime));
                dt.Columns.Add("date_admin_pdf_complete", typeof(DateTime));
                switch (userRole)
                {
                    case RoleEnum.ADMIN:
                    case RoleEnum.ACCOUNT:
                        dt.Columns.Add("Note for Admin & Account", typeof(string));
                        break;
                }
                switch (userRole)
                {
                    case RoleEnum.LOGIN:
                    case RoleEnum.CHEMIST:
                    case RoleEnum.SR_CHEMIST:
                    case RoleEnum.LABMANAGER:
                        dt.Columns.Add("Note for lab", typeof(string));

                        break;
                }
                String conSQL = Configurations.MySQLCon;
                using (MySqlConnection conn = new MySqlConnection("server = " + conSQL.Split(';')[2].Split('=')[2] + "; " + conSQL.Split(';')[3] + "; " + conSQL.Split(';')[4] + "; " + conSQL.Split(';')[5]))
                {
                    conn.Open();
                    String sql = "SELECT" +
                                "`Extent2`.`sample_prefix` AS `Job Type`," +
                                "`Extent7`.`name` AS `Status`," +
                                "(case when `Extent2`.`is_hold`='1' then 'Hold' else `Extent9`.`name` end) AS `Job Status`," +
                                "`Extent2`.`date_srchemist_complate` AS `Received`," +
                                "`Extent2`.`date_admin_sent_to_cus` AS  `Report Sent to Customer`," +
                                "`Extent1`.`date_of_receive`AS `Receive Date`,";
                    switch (userRole)
                    {
                        case RoleEnum.LOGIN:
                        case RoleEnum.CHEMIST:
                        case RoleEnum.SR_CHEMIST:
                        case RoleEnum.LABMANAGER:
                            sql += "(case when `Extent2`.`due_date_lab` = '0001-01-01' then '0001-01-01' else DATE_FORMAT(`Extent2`.`due_date_lab`,'%e %b %Y') end) AS `Due Date`,";
                            sql += "(case when `Extent2`.`due_date_lab` = '0001-01-01' then 'TBA' else '' end) AS `TBA FLAG`,";

                            break;
                        case RoleEnum.ADMIN:
                        case RoleEnum.MARKETING:
                        case RoleEnum.BUSINESS_MANAGER:
                            sql += "(CASE WHEN ISNULL(`Extent2`.`due_date_lab`) THEN '0001-01-01' ELSE (case when `Extent2`.`due_date_lab` = '0001-01-01' then '0001-01-01' else (case when `Extent2`.`due_date_customer` = '0001-01-01' then '0001-01-01' else DATE_FORMAT(`Extent2`.`due_date_customer`,'%e %b %Y') end) end) END) AS `Due Date`,";
                            sql += "(CASE WHEN ISNULL(`Extent2`.`due_date_lab`) THEN 'TBA' ELSE (case when `Extent2`.`due_date_lab` = '0001-01-01' then 'TBA' else (case when `Extent2`.`due_date_customer` = '0001-01-01' then 'TBA' else '' end) end) END) AS `TBA FLAG`,";


                            break;
                        default:
                            sql += "(case when `Extent2`.`due_date_lab` = '0001-01-01' then '0001-01-01' else DATE_FORMAT(`Extent2`.`due_date_lab`,'%e %b %Y') end) AS `Due Date`,";
                            sql += "(case when `Extent2`.`due_date_lab` = '0001-01-01' then 'TBA' else '' end) AS `TBA FLAG`,";

                            break;
                    }



                    sql += "(case when isnull(`Extent2`.`amend_or_retest`) then `Extent2`.`job_number` else concat(`Extent2`.`job_number`,(case when `Extent2`.`amend_or_retest` ='AM' then CONCAT('(',`Extent2`.`amend_or_retest`,`Extent2`.`amend_count`,')') else CONCAT('(',`Extent2`.`amend_or_retest`,`Extent2`.`retest_count`,')') end)) end) AS `ALS Ref`," +
                     "`Extent1`.`customer_ref_no` AS `No.Cus Ref No`," +
                     "`Extent2`.`other_ref_no` AS `Other Ref No`," +
                     "`Extent5`.`company_name` AS `Company`," +
                     "`Extent2`.`sample_invoice` AS `Invoice`," +
                     "`Extent2`.`sample_po` AS `Po`," +
                     "`Extent6`.`name` as `Contact`," +
                     "`Extent2`.`description` AS `Description`," +
                     "`Extent2`.`model` AS Model," +
                     "`Extent2`.`surface_area` AS `Surface Area`," +
                     "`Extent3`.`name` AS `Specification`," +
                     "`Extent4`.`name` AS `Type of test`," +
                     "`Extent4`.`data_group` AS `Data Group`," +
                     "`Extent2`.`date_login_inprogress` AS `date_login_inprogress`," +
                     "`Extent2`.`date_login_complete` AS `date_login_complete`," +
                     "`Extent2`.`date_chemist_analyze` AS `date_chemist_inprogress`," +
                     "`Extent2`.`date_chemist_complete` AS `date_chemist_complete`," +
                     "`Extent2`.`date_srchemist_analyze` AS `date_srchemist_inprogress`," +
                     "`Extent2`.`date_srchemist_complate` AS `date_srchemist_complate`," +
                     "`Extent2`.`date_admin_word_inprogress` AS `date_admin_word_inprogress`," +
                     "`Extent2`.`date_admin_word_complete` AS `date_admin_word_complete`," +
                     "`Extent2`.`date_labman_analyze` AS `date_labman_inprogress`," +
                     "`Extent2`.`date_labman_complete` AS `date_labman_complete`," +
                     "`Extent2`.`date_admin_pdf_inprogress` AS `date_admin_pdf_inprogress`," +
                     "`Extent2`.`date_admin_pdf_complete` AS `date_admin_pdf_complete`";
                    switch (userRole)
                    {
                        case RoleEnum.ADMIN:
                        case RoleEnum.ACCOUNT:
                            sql += ",`Extent2`.`note` AS `Note for Admin & Account`";
                            break;
                    }
                    switch (userRole)
                    {
                        case RoleEnum.LOGIN:
                        case RoleEnum.CHEMIST:
                        case RoleEnum.SR_CHEMIST:
                        case RoleEnum.LABMANAGER:
                            sql += ",`Extent2`.`note_lab` AS `Note for lab`";
                            break;
                    }
                    sql += " FROM `job_info` AS `Extent1`" +
                   " INNER JOIN `job_sample` AS `Extent2` ON `Extent1`.`ID` = `Extent2`.`job_id`" +
                   " LEFT OUTER JOIN `m_status` AS `Extent7` ON `Extent2`.`job_status` = `Extent7`.`ID`" +
                   " INNER JOIN `m_specification` AS `Extent3` ON `Extent2`.`specification_id` = `Extent3`.`ID`" +
                   " INNER JOIN `m_type_of_test` AS `Extent4` ON `Extent2`.`type_of_test_id` = `Extent4`.`ID`" +
                   " INNER JOIN `m_customer` AS `Extent5` ON `Extent1`.`customer_id` = `Extent5`.`ID`" +
                   " INNER JOIN `m_customer_contract_person` AS `Extent6` ON `Extent1`.`contract_person_id` = `Extent6`.`ID` " +
                   " LEFT OUTER JOIN `users_login` AS `Extent8` ON `Extent2`.`update_by` = `Extent8`.`ID`" +
                   " LEFT OUTER JOIN `m_completion_scheduled` AS `Extent9` ON `Extent2`.`status_completion_scheduled` = `Extent9`.`ID`";


                    StringBuilder sqlCri = new StringBuilder();

                    sqlCri.Append(" YEAR(`Extent1`.`date_of_receive`) = '" + ddlPhysicalYear.SelectedValue + "'");
                    sqlCri.Append(" AND ");
                    sqlCri.Append(" `Extent2`.`job_status` <> 0");
                    sqlCri.Append(" AND ");

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
                        sqlCri.Append(" `Extent2`.`sample_po` = '" + txtPo.Text + "'");
                        sqlCri.Append(" AND ");
                    }
                    if (!String.IsNullOrEmpty(txtInvoice.Text))
                    {
                        sqlCri.Append(" `Extent1`.`sample_invoice` = '" + txtInvoice.Text + "'");
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
                    sql += " ORDER BY `Extent2`.`ID` DESC";

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



            foreach (GridViewRow row in gvJob.Rows)
            {
                CheckBox chk = row.Cells[1].Controls[1] as CheckBox;

                if (chk != null && chk.Checked)
                {
                    HiddenField hf = row.Cells[1].Controls[3] as HiddenField;
                    HiddenField hIsGroup = row.Cells[1].Controls[5] as HiddenField;

                    if (this.isPoGroupOperation || this.isDuedateGroupOperation || this.isInvoiceGroupOperation || this.isSentToCusDateOperation || this.isNoteGroupOpeation || userRole == RoleEnum.LOGIN || userRole == RoleEnum.CHEMIST)
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