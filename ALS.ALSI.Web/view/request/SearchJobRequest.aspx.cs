using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        public int JobID { get; set; }

        public int SampleID { get; set; }

        public job_info obj
        {
            get
            {
                job_info tmp = new job_info();
                //tmp.job_number = txtJobNumber.Text;
                //tmp.date_of_receive = String.IsNullOrEmpty(txtDateOfRecieve.Text) ? DateTime.MinValue : Convert.ToDateTime(txtDateOfRecieve.Text);
                //tmp.contract_person_id = String.IsNullOrEmpty(ddlContract_person.SelectedValue) ? 0 : int.Parse(ddlContract_person.SelectedValue);
                //tmp.customer_id = String.IsNullOrEmpty(ddlCompany.SelectedValue) ? 0 : int.Parse(ddlCompany.SelectedValue);

                tmp.status = String.IsNullOrEmpty(ddlJobStatus.SelectedValue) ? 0 : int.Parse(ddlJobStatus.SelectedValue);
                tmp.jobRefNo = txtREfNo.Text;
                tmp.customer_id = String.IsNullOrEmpty(ddlCompany.SelectedValue) ? 0 : int.Parse(ddlCompany.SelectedValue);
                tmp.spec_id = String.IsNullOrEmpty(ddlSpecification.SelectedValue) ? 0 : int.Parse(ddlSpecification.SelectedValue);
                tmp.type_of_test_name = String.IsNullOrEmpty(ddlTypeOfTest.SelectedValue) ? "" : ddlTypeOfTest.SelectedItem.Text;

                tmp.job_prefix = String.IsNullOrEmpty(hPrefix.Value) ? 1 : Convert.ToInt16(hPrefix.Value);
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

                return tmp;
            }
        }

        #endregion

        #region "Method"

        private void initialPage()
        {

            ddlCompany.Items.Clear();
            ddlCompany.DataSource = new m_customer().SelectAll();
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

            bindingData();



            //prefix tab
            //btnElp.CssClass = "btn green";
            //btnEls.CssClass = "btn blue";
            //btnFa.CssClass = "btn blue";
            //btnElwa.CssClass = "btn blue";
            //btnGrp.CssClass = "btn blue";
            //btnTrb.CssClass = "btn blue";

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

        }

        private void bindingData()
        {
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
                        gvJob.Columns[3].Visible = true;
                        gvJob.Columns[4].Visible = true;
                        gvJob.Columns[11].Visible = true;
                        gvJob.Columns[12].Visible = true;
                        break;
                    default:
                        gvJob.Columns[3].Visible = false;
                        gvJob.Columns[4].Visible = false;
                        gvJob.Columns[11].Visible = false;
                        gvJob.Columns[12].Visible = false;
                        break;
                }


                //DropDownList ddlCompany = (DropDownList)gvJob.HeaderRow.FindControl("ddlCompany");
                //ddlCompany.DataSource = new m_customer().SelectAll();
                //ddlCompany.DataBind();
                //ddlCompany.Items.Insert(0, new ListItem("Company", "0"));
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
            this.CommandName = cmd;
            switch (cmd)
            {
                case CommandNameEnum.Edit:
                case CommandNameEnum.View:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    Server.Transfer(Constants.LINK_JOB_REQUEST);
                    break;
                case CommandNameEnum.ConvertTemplate:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
                    Server.Transfer(Constants.LINK_JOB_CONVERT_TEMPLATE);
                    break;
                case CommandNameEnum.Workflow:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
                    Server.Transfer(Constants.LINK_JOB_WORK_FLOW);
                    break;
                case CommandNameEnum.ChangeStatus:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
                    Server.Transfer(Constants.LINK_JOB_CHANGE_STATUS);
                    break;
                case CommandNameEnum.ChangeDueDate:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
                    Server.Transfer(Constants.LINK_JOB_CHANGE_DUEDATE);
                    break;
                case CommandNameEnum.ChangePo:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
                    Server.Transfer(Constants.LINK_JOB_CHANGE_PO);
                    break;
                case CommandNameEnum.ChangeInvoice:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
                    Server.Transfer(Constants.LINK_JOB_CHANGE_INVOICE);
                    break;
                case CommandNameEnum.Print:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
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
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
                    Server.Transfer(Constants.LINK_REPORT_DATE);
                    break;

                case CommandNameEnum.Amend:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
                    Server.Transfer(Constants.LINK_AMEND);
                    break;
                case CommandNameEnum.Retest:
                    this.JobID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    this.SampleID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[1]);
                    Server.Transfer(Constants.LINK_RETEST);
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

        //protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlContract_person.Items.Clear();
        //    ddlContract_person.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
        //    ddlContract_person.DataBind();


        //    m_customer cus = new m_customer().SelectByID(Convert.ToInt32(ddlCompany.SelectedValue));
        //    if (cus != null)
        //    {

        //        List<m_customer_contract_person> contractPersonList = new m_customer_contract_person().FindAllByCompanyID(cus.ID);
        //        if (contractPersonList != null)
        //        {
        //            ddlContract_person.Items.Clear();
        //            ddlContract_person.Items.Add(new ListItem(Constants.PLEASE_SELECT, ""));
        //            ddlContract_person.DataSource = contractPersonList;
        //            ddlContract_person.DataBind();

        //            //ddlContract_person.Enabled = true;
        //        }
        //        else
        //        {
        //            //TODO            

        //        }
        //    }
        //    else
        //    {
        //        //TODO
        //    }


        //}

        protected void gvJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    GridView gv = (GridView)sender;

                    int _valueStatus = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][1]);
                    int _valueCompletion_scheduled = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][3]);
                    int _step1owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][4]);
                    int _step2owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][5]);
                    int _step3owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][6]);
                    int _step4owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][7]);
                    int _step5owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][8]);
                    int _step6owner = Convert.ToInt32(gv.DataKeys[e.Row.RowIndex][9]);

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

                    LinkButton btnAmend = (LinkButton)e.Row.FindControl("btnAmend");
                    LinkButton btnReTest = (LinkButton)e.Row.FindControl("btnReTest");



                    Literal litStatus = (Literal)e.Row.FindControl("litStatus");
                    Literal ltJobStatus = (Literal)e.Row.FindControl("ltJobStatus");
                    Label lbJobNumber = (Label)e.Row.FindControl("lbJobNumber");
                    CompletionScheduledEnum status_completion_scheduled = (CompletionScheduledEnum)Enum.ToObject(typeof(CompletionScheduledEnum), _valueCompletion_scheduled);

                    StatusEnum job_status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), _valueStatus);
                    ltJobStatus.Text = Constants.GetEnumDescription(job_status);
                    RoleEnum role = (RoleEnum)Enum.ToObject(typeof(RoleEnum), userLogin.role_id);
                    switch (role)
                    {
                        case RoleEnum.ROOT:
                            btnInfo.Visible = true;
                            btnEdit.Visible = true;
                            btnConvertTemplete.Visible = true;
                            btnChangeStatus.Visible = true;
                            btnWorkFlow.Visible = true;
                            btnChangeDueDate.Visible = true;
                            btnChangePo.Visible = true;
                            btnChangeInvoice.Visible = true;
                            btnPrintLabel.Visible = true;
                            btnChangeReportDate.Visible = false;
                            btnAmend.Visible = false;
                            btnReTest.Visible = false;
                            break;
                        case RoleEnum.LOGIN:
                            btnInfo.Visible = true;
                            btnEdit.Visible = true;
                            btnConvertTemplete.Visible = (job_status == StatusEnum.LOGIN_CONVERT_TEMPLATE) ? true : false;
                            btnChangeStatus.Visible = true;
                            btnWorkFlow.Visible = (job_status == StatusEnum.LOGIN_CONVERT_TEMPLATE || job_status == StatusEnum.CHEMIST_TESTING) ? false : true;
                            btnChangeDueDate.Visible = false;
                            btnChangePo.Visible = false;
                            btnChangeInvoice.Visible = false;
                            btnPrintLabel.Visible = true;
                            btnChangeReportDate.Visible = false;
                            btnAmend.Visible = false;
                            btnReTest.Visible = false;
                            break;
                        case RoleEnum.CHEMIST:
                            btnInfo.Visible = false;
                            btnEdit.Visible = false;
                            btnConvertTemplete.Visible = false;
                            btnChangeStatus.Visible = false;
                            btnWorkFlow.Visible = (job_status == StatusEnum.LOGIN_CONVERT_TEMPLATE || job_status == StatusEnum.LOGIN_SELECT_SPEC) ? false : true;
                            btnChangeDueDate.Visible = false;
                            btnChangePo.Visible = false;
                            btnChangeInvoice.Visible = false;
                            btnPrintLabel.Visible = false;
                            btnChangeReportDate.Visible = false;
                            btnAmend.Visible = false;
                            btnReTest.Visible = false;
                            break;
                        case RoleEnum.SR_CHEMIST:
                            btnInfo.Visible = false;
                            btnEdit.Visible = false;
                            btnConvertTemplete.Visible = false;
                            btnChangeStatus.Visible = false;
                            btnWorkFlow.Visible = (job_status == StatusEnum.LOGIN_CONVERT_TEMPLATE || job_status == StatusEnum.LOGIN_SELECT_SPEC || job_status == StatusEnum.CHEMIST_TESTING) ? false : true;
                            btnChangeDueDate.Visible = true;
                            btnChangePo.Visible = false;
                            btnChangeInvoice.Visible = false;
                            btnPrintLabel.Visible = false;
                            btnChangeReportDate.Visible = false;
                            btnAmend.Visible = false;
                            btnReTest.Visible = false;
                            break;
                        case RoleEnum.LABMANAGER:
                            btnInfo.Visible = false;
                            btnEdit.Visible = false;
                            btnConvertTemplete.Visible = false;
                            btnChangeStatus.Visible = false;
                            btnWorkFlow.Visible = (job_status == StatusEnum.LOGIN_CONVERT_TEMPLATE || job_status == StatusEnum.LOGIN_SELECT_SPEC || job_status == StatusEnum.CHEMIST_TESTING) ? false : true;
                            btnChangeDueDate.Visible = false;
                            btnChangePo.Visible = false;
                            btnChangeInvoice.Visible = false;
                            btnPrintLabel.Visible = false;
                            btnChangeReportDate.Visible = false;
                            btnAmend.Visible = false;
                            btnReTest.Visible = false;
                            break;
                        case RoleEnum.ADMIN:
                            btnInfo.Visible = false;
                            btnEdit.Visible = false;
                            btnConvertTemplete.Visible = false;
                            btnChangeStatus.Visible = false;
                            btnWorkFlow.Visible = (job_status == StatusEnum.LOGIN_CONVERT_TEMPLATE || job_status == StatusEnum.LOGIN_SELECT_SPEC || job_status == StatusEnum.CHEMIST_TESTING) ? false : true;
                            btnChangeDueDate.Visible = false;
                            btnChangePo.Visible = true;
                            btnChangeInvoice.Visible = false;
                            btnPrintLabel.Visible = true;
                            btnChangeReportDate.Visible = true;
                            btnAmend.Visible = false;
                            btnReTest.Visible = false;
                            break;
                        case RoleEnum.ACCOUNT:
                            btnInfo.Visible = false;
                            btnEdit.Visible = false;
                            btnConvertTemplete.Visible = false;
                            btnChangeStatus.Visible = false;
                            btnWorkFlow.Visible = (job_status == StatusEnum.LOGIN_CONVERT_TEMPLATE || job_status == StatusEnum.LOGIN_SELECT_SPEC || job_status == StatusEnum.CHEMIST_TESTING) ? false : true;
                            btnChangeDueDate.Visible = false;
                            btnChangePo.Visible = false;
                            btnChangeInvoice.Visible = true;
                            btnPrintLabel.Visible = false;
                            btnChangeReportDate.Visible = false;
                            btnAmend.Visible = false;
                            btnReTest.Visible = false;
                            break;
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

                    switch (job_status)
                    {

                        case StatusEnum.JOB_CANCEL:
                            e.Row.ForeColor = System.Drawing.Color.Red;
                            btnInfo.Visible = false;
                            btnEdit.Visible = false;
                            btnConvertTemplete.Visible = false;
                            btnChangeStatus.Visible = true;
                            btnWorkFlow.Visible = false;
                            btnChangeDueDate.Visible = false;
                            btnChangePo.Visible = false;
                            btnChangeInvoice.Visible = false;
                            btnAmend.Visible = false;
                            btnReTest.Visible = false;
                            break;
                        case StatusEnum.JOB_COMPLETE:
                            e.Row.ForeColor = System.Drawing.Color.Green;
                            btnInfo.Visible = false;
                            btnEdit.Visible = false;
                            btnConvertTemplete.Visible = false;
                            btnChangeStatus.Visible = true;
                            btnWorkFlow.Visible = false;
                            btnChangeDueDate.Visible = false;
                            btnChangePo.Visible = false;
                            btnChangeInvoice.Visible = false;
                            btnPrintLabel.Visible = (userLogin.role_id == Convert.ToInt32(RoleEnum.ADMIN)) ? true : false;
                            btnAmend.Visible = true;
                            btnReTest.Visible = true;
                            break;
                        case StatusEnum.JOB_HOLD:
                            e.Row.ForeColor = System.Drawing.Color.Violet;
                            btnInfo.Visible = false;
                            btnEdit.Visible = false;
                            btnConvertTemplete.Visible = false;
                            btnChangeStatus.Visible = true;
                            btnWorkFlow.Visible = false;
                            btnChangeDueDate.Visible = false;
                            btnChangePo.Visible = false;
                            btnChangeInvoice.Visible = false;
                            btnAmend.Visible = false;
                            btnReTest.Visible = false;
                            break;
                        default:
                            e.Row.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                    //jobStatus icon
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
                            ltJobStatus.Text = "<i class=\"fa fa-truck\" ></i>";
                            break;
                        case StatusEnum.JOB_HOLD:
                            ltJobStatus.Text = "<i class=\"fa fa-lock\" ></i>";
                            break;
                        case StatusEnum.JOB_CANCEL:
                            ltJobStatus.Text = "<i class=\"fa fa-trash-o\"></i>";
                            break;
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

            btnElp.CssClass = (btnElp.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnEls.CssClass = (btnEls.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnFa.CssClass = (btnFa.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnElwa.CssClass = (btnElwa.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnGrp.CssClass = (btnGrp.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";
            btnTrb.CssClass = (btnTrb.ID == btn.ID) ? "btn blue" : "btn btn-default btn-sm";

            switch (btn.ID)
            {
                case "btnElp":
                    hPrefix.Value = "1";
                    break;
                case "btnEls":
                    hPrefix.Value = "2";
                    break;
                case "btnFa":
                    hPrefix.Value = "3";
                    break;
                case "btnElwa":
                    hPrefix.Value = "4";
                    break;
                case "btnGrp":
                    hPrefix.Value = "5";
                    break;
                case "btnTrb":
                    hPrefix.Value = "6";
                    break;
                case "btnEln":
                                        hPrefix.Value = "7";

                    break;
            }
            bindingData();
            Console.WriteLine();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddlCompany = (DropDownList)sender;
            //job_info jobInfo = new job_info();
            //jobInfo.customer_id = Convert.ToInt32(ddlCompany.SelectedValue);
            //searchResult = jobInfo.SearchData();

            //gvJob.DataSource = searchResult;
            //gvJob.DataBind();
            //gvJob.UseAccessibleHeader = true;
            //gvJob.HeaderRow.TableSection = TableRowSection.TableHeader;
            //if (gvJob.Rows.Count > 0)
            //{
            //    lbTotalRecords.Text = String.Format(Constants.TOTAL_RECORDS, gvJob.Rows.Count);

            //}
            //else
            //{
            //    lbTotalRecords.Text = string.Empty;
            //}
            //ddlCompany = (DropDownList)gvJob.HeaderRow.FindControl("ddlCompany");
            //ddlCompany.DataSource = new m_customer().SelectAll();
            //ddlCompany.DataBind();
            //ddlCompany.Items.Insert(0, new ListItem("Company", "0"));
            //Console.WriteLine();
        }




        protected void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvJob.AllowPaging = false;
                //this.BindGrid();

                gvJob.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvJob.HeaderRow.Cells)
                {
                    cell.BackColor = gvJob.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvJob.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvJob.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvJob.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvJob.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
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
    }
}