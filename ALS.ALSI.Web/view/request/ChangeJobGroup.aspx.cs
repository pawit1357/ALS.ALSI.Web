using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.request
{
    public partial class ChangeJobGroup : System.Web.UI.Page
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ChangeJobPo));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "ChangeJobPo"]; }
            set { Session[GetType().Name + "ChangeJobPo"] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        public List<int> selectedList
        {
            get { return (List<int>)Session[GetType().Name + "selectedList"]; }
            set { Session[GetType().Name + "selectedList"] = value; }
        }
        public List<job_sample> dataList
        {
            get { return (List<job_sample>)Session[GetType().Name + "dataList"]; }
            set { Session[GetType().Name + "dataList"] = value; }
        }
        public Boolean isPoGroupOperation
        {
            get { return (Boolean)Session[GetType().Name + "isPoGroupOperation"]; }
            set { Session[GetType().Name + "isPoGroupOperation"] = value; }
        }
        public Boolean isChangeDueDateGroup
        {
            get { return (Boolean)Session[GetType().Name + "isChangeDueDateGroup"]; }
            set { Session[GetType().Name + "isChangeDueDateGroup"] = value; }
        }
        public Boolean isInvoiceGroupOperation
        {
            get { return (Boolean)Session[GetType().Name + "isInvoiceGroupOperation"]; }
            set { Session[GetType().Name + "isInvoiceGroupOperation"] = value; }
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
        public Boolean isCusRefNoGroupOperation
        {
            get { return (Boolean)Session[GetType().Name + "isCusRefNoGroupOperation"]; }
            set { Session[GetType().Name + "isCusRefNoGroupOperation"] = value; }
        }
        public Boolean isOtherRefGroupOpeation
        {
            get { return (Boolean)Session[GetType().Name + "isOtherRefGroupOpeation"]; }
            set { Session[GetType().Name + "isOtherRefGroupOpeation"] = value; }
        }
        public Boolean isGroupApproveOperation
        {
            get { return (Boolean)Session[GetType().Name + "isGroupApproveOperation"]; }
            set { Session[GetType().Name + "isGroupApproveOperation"] = value; }
        }


        protected String Message
        {
            get { return (String)Session[GetType().Name + "Message"]; }
            set { Session[GetType().Name + "Message"] = value; }
        }

        private void initialPage()
        {
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            bindingData();
        }



        private void bindingData()
        {
            gvSample.DataSource = this.dataList;
            gvSample.DataBind();

            foreach (job_sample js in this.dataList)
            {
                js.isChecked = true;
            }
            foreach (GridViewRow row in gvSample.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("cbSelect");
                chkcheck.Checked = true;
            }

            btnSave.Visible = this.dataList.Count > 0;
            pChemist.Visible = (this.dataList.Count > 0) && !this.isChangeDueDateGroup && !this.isPoGroupOperation && !this.isInvoiceGroupOperation && !this.isSentToCusDateOperation && !this.isNoteGroupOpeation && !this.isCusRefNoGroupOperation && !this.isOtherRefGroupOpeation;
            pChangeDueDate.Visible = (this.dataList.Count > 0) && !this.isPoGroupOperation && (this.isChangeDueDateGroup || this.isSentToCusDateOperation) && !this.isInvoiceGroupOperation && !this.isNoteGroupOpeation && !this.isCusRefNoGroupOperation;
            pAccount2.Visible = (this.dataList.Count > 0) && !this.isPoGroupOperation && !this.isChangeDueDateGroup && !isNoteGroupOpeation && this.isInvoiceGroupOperation && !this.isCusRefNoGroupOperation;
            pCusRefNo.Visible = this.isCusRefNoGroupOperation;
            pOtherRef.Visible = this.isOtherRefGroupOpeation;

            if (!btnSave.Visible)
            {
                lbDesc.Text = "รายการที่เลือกไม่ได้ถูกกำหนดเป็นงานแบบกลุ่ม";

            }

            if (isPoGroupOperation || isInvoiceGroupOperation && this.dataList.Count > 0)
            {
                txtInvoice.Text = this.dataList[0].sample_invoice;
                txtInvoiceAmt.Text = (this.dataList[0].sample_invoice_amount == null) ? "" : this.dataList[0].sample_invoice_amount.ToString();
                txtInvoiceDate.Text = (this.dataList[0].sample_invoice_date == null) ? "" : this.dataList[0].sample_invoice_date.Value.ToString("dd/MM/yyyy");
                txtPaymentDate.Text = (this.dataList[0].sample_invoice_complete_date == null) ? "" : this.dataList[0].sample_invoice_complete_date.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                gvSample.Columns[0].Visible = true;
                gvSample.Columns[1].Visible = true;
                gvSample.Columns[2].Visible = false;
                gvSample.Columns[3].Visible = false;
                gvSample.Columns[4].Visible = false;
                gvSample.Columns[5].Visible = false;
                gvSample.Columns[6].Visible = true;
                gvSample.Columns[7].Visible = true;
                gvSample.Columns[8].Visible = true;
                gvSample.Columns[9].Visible = true;
            }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
            Session.Remove(GetType().Name + "selectedList");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;

            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;
            if (!Page.IsPostBack)
            {
                this.selectedList = new List<int>();
                this.selectedList = prvPage.selectedList;
                this.isPoGroupOperation = prvPage.isPoGroupOperation;
                this.isChangeDueDateGroup = prvPage.isDuedateGroupOperation;
                this.isInvoiceGroupOperation = prvPage.isInvoiceGroupOperation;
                this.isSentToCusDateOperation = prvPage.isSentToCusDateOperation;
                this.isNoteGroupOpeation = prvPage.isNoteGroupOpeation;
                this.isCusRefNoGroupOperation = prvPage.isCusRefNoGroupOperation;
                this.isGroupApproveOperation = prvPage.isGroupApproveOperation;
                this.isOtherRefGroupOpeation = prvPage.isOtherRefGroupOpeation;

                this.dataList = job_sample.FindAllByIds(this.selectedList);

                ddlAssignTo.Items.Clear();
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LOGIN_SELECT_SPEC), Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.CHEMIST_TESTING), Convert.ToInt32(StatusEnum.CHEMIST_TESTING) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_CHECKING), Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_WORD), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_CHECKING), Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING) + ""));
                ddlAssignTo.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.ADMIN_CONVERT_PDF), Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF) + ""));

                m_template template = new m_template();


                var data = template.SelectAllByActive();
                ddlTemplate.Items.Clear();
                ddlTemplate.DataSource = data;
                ddlTemplate.DataBind();
                ddlTemplate.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));
                ddlTemplate.SelectedValue = "";

                pLogin.Visible = false;
                pChemist.Visible = false;
                pSrChemist.Visible = false;
                pRemark.Visible = false;
                pDisapprove.Visible = false;
                pAccount.Visible = false;
                pChangeDueDate.Visible = false;
                pAccount2.Visible = false;
                pCusRefNo.Visible = false;
                pShowChemistFileUpload.Visible = false;
                pOtherRef.Visible = false;

                String desc2 = String.Empty;
                if (isPoGroupOperation) desc2 = "(PO)";
                if (isChangeDueDateGroup) desc2 = "(Duedate)";
                if (isInvoiceGroupOperation) desc2 = "(Invoice)";
                if (isSentToCusDateOperation) desc2 = "(Date Admin Sent to Customer)";
                if (isCusRefNoGroupOperation) desc2 = "(Customer ref no)";
                if (isOtherRefGroupOpeation) desc2 = "(Other Ref No)";
                pCusRefNo.Visible = this.isCusRefNoGroupOperation;

                RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);
                switch (userRole)
                {
                    case RoleEnum.LOGIN:
                        pLogin.Visible = true;
                        pShowChemistFileUpload.Visible = false;
                        pChemist.Visible = false;
                        pSrChemist.Visible = false;
                        pRemark.Visible = false;
                        pDisapprove.Visible = false;
                        pAccount.Visible = false;
                        pChangeDueDate.Visible = false;
                        pAccount2.Visible = false;
                        pNote.Visible = false;
                        pCusRefNo.Visible = false;
                        pOtherRef.Visible = false;
                        lbDesc.Text = "Login: ทำรายการแบบกลุ่ม" + desc2;
                        break;
                    case RoleEnum.CHEMIST:
                        pLogin.Visible = false;
                        pShowChemistFileUpload.Visible = false;
                        pChemist.Visible = true;
                        pSrChemist.Visible = false;
                        pRemark.Visible = false;
                        pDisapprove.Visible = false;
                        pAccount.Visible = false;
                        pChangeDueDate.Visible = false;
                        pAccount2.Visible = false;
                        pNote.Visible = this.isNoteGroupOpeation;
                        pCusRefNo.Visible = false;
                        pOtherRef.Visible = false;
                        lbDesc.Text = "Chemist: ทำรายการแบบกลุ่ม" + desc2;
                        break;
                    case RoleEnum.SR_CHEMIST:
                        pLogin.Visible = false;
                        ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_APPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_APPROVE) + ""));
                        ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.SR_CHEMIST_DISAPPROVE), Convert.ToInt32(StatusEnum.SR_CHEMIST_DISAPPROVE) + ""));
                        pShowChemistFileUpload.Visible = false;
                        pChemist.Visible = false;
                        pSrChemist.Visible = !isChangeDueDateGroup;
                        pRemark.Visible = false;
                        pDisapprove.Visible = false;
                        pAccount.Visible = false;
                        pChangeDueDate.Visible = isChangeDueDateGroup;
                        pAccount2.Visible = false;
                        pNote.Visible = false;
                        pCusRefNo.Visible = false;
                        pOtherRef.Visible = false;
                        lbDesc.Text = "Sr.Chemist: ทำรายการแบบกลุ่ม" + desc2;

                        break;
                    case RoleEnum.LABMANAGER:
                        pLogin.Visible = false;
                        ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_APPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_APPROVE) + ""));
                        ddlStatus.Items.Add(new ListItem(Constants.GetEnumDescription(StatusEnum.LABMANAGER_DISAPPROVE), Convert.ToInt32(StatusEnum.LABMANAGER_DISAPPROVE) + ""));
                        pShowChemistFileUpload.Visible = false;
                        pChemist.Visible = false;
                        pSrChemist.Visible = true;
                        pRemark.Visible = false;
                        pDisapprove.Visible = false;
                        pAccount.Visible = false;
                        pChangeDueDate.Visible = false;
                        pAccount2.Visible = false;
                        pNote.Visible = false;
                        pCusRefNo.Visible = false;
                        pOtherRef.Visible = false;
                        lbDesc.Text = "Lab Mnager: ทำรายการแบบกลุ่ม" + desc2;

                        break;
                    case RoleEnum.ADMIN:
                        pLogin.Visible = false;
                        pShowChemistFileUpload.Visible = false;
                        pChemist.Visible = false;
                        pSrChemist.Visible = false;
                        pRemark.Visible = false;
                        pDisapprove.Visible = false;
                        //Boolean isChangePo = this.dataList.Exists(x => x.job_status == Convert.ToInt16(StatusEnum.JOB_COMPLETE)|| x.job_status == Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC));
                        pChangeDueDate.Visible = isChangeDueDateGroup || isSentToCusDateOperation;
                        pAccount.Visible = this.isPoGroupOperation;
                        pAccount2.Visible = false;
                        pNote.Visible = this.isNoteGroupOpeation;
                        pCusRefNo.Visible = false;
                        pOtherRef.Visible = false;
                        lbDesc.Text = "Admin: ทำรายการแบบกลุ่ม" + desc2;

                        break;
                    case RoleEnum.ACCOUNT:
                        pLogin.Visible = false;
                        pChemist.Visible = false;
                        pSrChemist.Visible = false;
                        pRemark.Visible = false;
                        pDisapprove.Visible = false;
                        pAccount.Visible = false;
                        pChangeDueDate.Visible = false;
                        pNote.Visible = this.isNoteGroupOpeation;
                        pAccount2.Visible = this.isInvoiceGroupOperation;
                        pCusRefNo.Visible = false;
                        pOtherRef.Visible = false;
                        lbDesc.Text = "Account: ทำรายการแบบกลุ่ม" + desc2;
                        break;
                    case RoleEnum.MARKETING:
                        pLogin.Visible = false;
                        pShowChemistFileUpload.Visible = false;
                        pChemist.Visible = false;
                        pSrChemist.Visible = false;
                        pRemark.Visible = false;
                        pDisapprove.Visible = false;
                        pAccount.Visible = false;
                        pChangeDueDate.Visible = false;
                        pAccount2.Visible = false;
                        pNote.Visible = false;
                        pCusRefNo.Visible = true;
                        pOtherRef.Visible = true;
                        lbDesc.Text = "Marketting: ทำรายการแบบกลุ่ม" + desc2;
                        break;
                    default:
                        pLogin.Visible = false;
                        pShowChemistFileUpload.Visible = false;
                        pChemist.Visible = false;
                        pSrChemist.Visible = false;
                        pRemark.Visible = false;
                        pDisapprove.Visible = false;
                        pAccount.Visible = false;
                        pChangeDueDate.Visible = false;
                        pAccount2.Visible = false;
                        pNote.Visible = false;
                        pCusRefNo.Visible = false;
                        break;
                }
                initialPage();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            holiday_calendar hc = new holiday_calendar();

            string yyyy = DateTime.Now.ToString("yyyy");
            string MM = DateTime.Now.ToString("MM");
            string dd = DateTime.Now.ToString("dd");
            RoleEnum userRole = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString(), true);

            foreach (job_sample jobSample in this.dataList)
            {
                if (this.isPoGroupOperation)
                {
                    jobSample.sample_po = txtPo.Text;
                }
                else if (this.isChangeDueDateGroup)
                {
                    holiday_calendar h = new holiday_calendar();
                    //DateTime[] dt = h.GetDueDate(Convert.ToInt32(jobSample.status_completion_scheduled), CustomUtils.converFromDDMMYYYY(txtDuedate.Text));

                    switch (userRole)
                    {
                        case RoleEnum.LOGIN:
                        case RoleEnum.SR_CHEMIST:
                            if (String.IsNullOrEmpty(txtDuedate.Text) && !cbIsTba.Checked)
                            {
                                jobSample.due_date_lab = null;
                            }
                            else
                            {
                                if (cbIsTba.Checked)
                                {
                                    jobSample.due_date_lab = new DateTime(1, 1, 1);
                                    jobSample.due_date_customer = new DateTime(1, 1, 1);
                                }
                                else
                                {
                                    //1|Normal
                                    //2|Urgent
                                    //3|Express
                                    //4|Extend 1


                                    //jobSample.due_date_lab = dt[0];


                                    switch (jobSample.status_completion_scheduled.Value)
                                    {
                                        case 1:
                                        case 2:
                                        case 4:
                                        case 5:
                                            jobSample.due_date_lab = hc.GetWorkingDayLab(CustomUtils.converFromDDMMYYYY(txtDuedate.Text), 0);
                                            jobSample.due_date_customer = hc.GetWorkingDayLab(CustomUtils.converFromDDMMYYYY(txtDuedate.Text), 1);
                                            break;
                                        case 3://Express
                                            jobSample.due_date_lab = hc.GetWorkingDayLab(CustomUtils.converFromDDMMYYYY(txtDuedate.Text), 0);
                                            jobSample.due_date_customer = hc.GetWorkingDayLab(CustomUtils.converFromDDMMYYYY(txtDuedate.Text), 0);
                                            break;
                                    }
                                }
                            }
                            break;
                        case RoleEnum.ADMIN:
                            //jobSample.due_date_customer = dt[1];
                            jobSample.due_date_customer = hc.GetWorkingDayLab(CustomUtils.converFromDDMMYYYY(txtDuedate.Text), 0);
                            break;
                    }
                }
                else if (this.isSentToCusDateOperation)
                {
                    switch (userRole)
                    {
                        case RoleEnum.ADMIN:
                            if (String.IsNullOrEmpty(txtDuedate.Text))
                            {
                                jobSample.date_admin_sent_to_cus = null;
                            }
                            else
                            {
                                jobSample.date_admin_sent_to_cus = CustomUtils.converFromDDMMYYYY(txtDuedate.Text);
                            }
                            break;
                    }
                }
                else if (this.isInvoiceGroupOperation)
                {
                    //CheckBox chk = row.Cells[1].Controls[1] as CheckBox;

                    Console.WriteLine();
                    if (jobSample.isChecked)
                    {
                        jobSample.sample_invoice = txtInvoice.Text;
                        jobSample.sample_invoice_amount = String.IsNullOrEmpty(txtInvoiceAmt.Text) ? Convert.ToDouble("0") : Convert.ToDouble(txtInvoiceAmt.Text);
                        jobSample.sample_invoice_amount_rpt = String.IsNullOrEmpty(txtInvoiceAmt.Text) ? Convert.ToDouble("0") : Convert.ToDouble(txtInvoiceAmt.Text);

                        if (!String.IsNullOrEmpty(txtInvoiceDate.Text))
                        {
                            jobSample.sample_invoice_date = CustomUtils.converFromDDMMYYYY(txtInvoiceDate.Text);
                            jobSample.sample_invoice_status = Convert.ToInt16(PaymentStatus.PAYMENT_INPROCESS);
                        }
                        if (!String.IsNullOrEmpty(txtPaymentDate.Text))
                        {
                            jobSample.sample_invoice_status = Convert.ToInt16(PaymentStatus.PAYMENT_COMPLETE);
                            jobSample.sample_invoice_complete_date = CustomUtils.converFromDDMMYYYY(txtPaymentDate.Text);
                        }
                    }
                }
                else if (this.isCusRefNoGroupOperation)
                {
                    job_info jobInfo = new job_info();
                    jobInfo = jobInfo.SelectByID(jobSample.job_id);
                    if (jobInfo != null)
                    {
                        jobInfo.customer_ref_no = txtCusRefNo.Text;
                    }
                }
                else if (this.isNoteGroupOpeation)
                {
                    switch (userRole)
                    {
                        case RoleEnum.CHEMIST:
                            jobSample.note_lab = txtNote.Text;

                            break;
                        default:
                            jobSample.note = txtNote.Text;
                            break;
                    }
                }
                else if (this.isGroupApproveOperation)
                {
                    if (!String.IsNullOrEmpty(ddlStatus.SelectedValue))
                    {
                        StatusEnum _labmanSelectedStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                        switch (_labmanSelectedStatus)
                        {
                            case StatusEnum.LABMANAGER_APPROVE:
                                jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF);
                                break;
                            case StatusEnum.LABMANAGER_DISAPPROVE:
                                jobSample.job_status = Convert.ToInt32(ddlAssignTo.SelectedValue);
                                break;
                        }

                    }
                }
                else if (this.isOtherRefGroupOpeation)
                {
                    jobSample.other_ref_no = txtOtherRefNo.Text;
                }
                else
                {
                    StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), jobSample.job_status.ToString(), true);
                    switch (status)
                    {
                        case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                            if (FileUpload1.HasFile)// && (Path.GetExtension(FileUpload1.FileName).Equals(".xls") || Path.GetExtension(FileUpload1.FileName).Equals(".xlt")))
                            {

                                String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, jobSample.job_number, Path.GetFileName(FileUpload1.FileName));
                                String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, jobSample.job_number, Path.GetFileName(FileUpload1.FileName));

                                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                                }
                                FileUpload1.SaveAs(source_file);
                                jobSample.ad_hoc_tempalte_path = source_file_url;
                            }

                            if (!String.IsNullOrEmpty(ddlTemplate.SelectedValue))
                            {
                                jobSample.template_id = String.IsNullOrEmpty(ddlTemplate.SelectedValue) ? 0 : int.Parse(ddlTemplate.SelectedValue);
                                switch (int.Parse(ddlTemplate.SelectedValue))
                                {
                                    case 901://PA TAMPLATE(BLANK)
                                        jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                                        break;
                                    default:
                                        if (pUploadfile.Visible)
                                        {
                                            jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                                        }
                                        else
                                        {
                                            jobSample.job_status = Convert.ToInt32(StatusEnum.LOGIN_SELECT_SPEC);
                                        }
                                        break;
                                }
                            }
                            break;
                        case StatusEnum.CHEMIST_TESTING:
                            jobSample.job_status = Convert.ToInt32(StatusEnum.SR_CHEMIST_CHECKING);
                            jobSample.date_chemist_complete = DateTime.Now;
                            //jobSample.date_chemist_analyze = CustomUtils.converFromDDMMYYYY(txtDateAnalyzed.Text);
                            if (rdWithReport.Checked)
                            {

                                String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, jobSample.job_number, Path.GetFileName(FileUpload2.FileName));
                                String source_file_url = String.Format(Configurations.PATH_URL, yyyy, MM, dd, jobSample.job_number, Path.GetFileName(FileUpload2.FileName));


                                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                                }
                                FileUpload2.SaveAs(source_file);
                                jobSample.path_word = source_file_url;

                            }
                            else
                            {

                                string rawFileTemplate = Server.MapPath("~/template/") + "no_report.doc";

                                String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, jobSample.job_number, Path.GetFileName(rawFileTemplate));
                                String source_file_url = String.Concat(String.Empty, String.Format(Configurations.PATH_URL, yyyy, MM, dd, jobSample.job_number, Path.GetFileName(rawFileTemplate)));

                                if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                                }
                                if (!File.Exists(source_file))
                                {
                                    File.Copy(rawFileTemplate, source_file);
                                }
                                jobSample.path_word = source_file_url;
                            }
                            break;
                        case StatusEnum.SR_CHEMIST_CHECKING:
                            if (!String.IsNullOrEmpty(ddlStatus.SelectedValue))
                            {
                                StatusEnum _srChemistSelectedStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                                switch (_srChemistSelectedStatus)
                                {
                                    case StatusEnum.SR_CHEMIST_APPROVE:
                                        jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_WORD);
                                        #region ":: STAMP COMPLETE DATE"
                                        jobSample.date_srchemist_complate = DateTime.Now;
                                        #endregion
                                        break;
                                    case StatusEnum.SR_CHEMIST_DISAPPROVE:
                                        jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                                        break;
                                }
                            }
                            break;

                        case StatusEnum.LABMANAGER_CHECKING:

                            if (!String.IsNullOrEmpty(ddlStatus.SelectedValue))
                            {
                                StatusEnum _labmanSelectedStatus = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue, true);
                                switch (_labmanSelectedStatus)
                                {
                                    case StatusEnum.LABMANAGER_APPROVE:
                                        jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF);
                                        break;
                                    case StatusEnum.LABMANAGER_DISAPPROVE:
                                        jobSample.job_status = Convert.ToInt32(ddlAssignTo.SelectedValue);
                                        break;
                                }
                            }
                            break;
                        case StatusEnum.ADMIN_CONVERT_WORD:

                            jobSample.job_status = Convert.ToInt32(StatusEnum.LABMANAGER_CHECKING);

                            break;
                        case StatusEnum.ADMIN_CONVERT_PDF:

                            jobSample.job_status = Convert.ToInt32(StatusEnum.JOB_COMPLETE);

                            break;

                    }
                    jobSample.group_submit = Convert.ToSByte(1);
                }
                jobSample.Update();
            }

            //Commit
            GeneralManager.Commit();
            removeSession();
            MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }

        protected void rdWithReport_CheckedChanged(object sender, EventArgs e)
        {
            pShowChemistFileUpload.Visible = rdWithReport.Checked;
        }

        protected void rdNoReport_CheckedChanged(object sender, EventArgs e)
        {
            pShowChemistFileUpload.Visible = !rdNoReport.Checked;
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), ddlStatus.SelectedValue.ToString(), true);
            switch (status)
            {
                case StatusEnum.SR_CHEMIST_DISAPPROVE:
                    pRemark.Visible = true;
                    pDisapprove.Visible = true;
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

        protected void ddlAssignTo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FixCode 407,408 Blank template
            if (ddlTemplate.SelectedValue.Equals("407") || ddlTemplate.SelectedValue.Equals("408"))
            {
                pUploadfile.Visible = true;
            }
            else
            {
                pUploadfile.Visible = false;
            }
        }

        protected void gvSample_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSample.EditIndex = -1;
            gvSample.DataSource = dataList;
            gvSample.DataBind();
        }
        protected void gvSample_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSample.EditIndex = e.NewEditIndex;
            gvSample.DataSource = dataList;
            gvSample.DataBind();
        }
        protected void gvSample_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String jobNumber = gvSample.DataKeys[e.RowIndex].Values[1].ToString();
            int Id = Convert.ToInt32(gvSample.DataKeys[e.RowIndex].Values[0].ToString());

            TextBox txt_sample_so = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txt_sample_so");
            TextBox txt_sample_invoice = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txt_sample_invoice");
            TextBox txt_sample_invoice_date = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txt_sample_invoice_date");
            TextBox txt_sample_invoice_amount = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txt_sample_invoice_amount");
            TextBox txt_sample_invoice_complete_date = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txt_sample_invoice_complete_date");

            job_sample jobSample = this.dataList.Find(x => x.ID == Id);
            if (jobSample != null)
            {
                jobSample.sample_so = txt_sample_so.Text;
                jobSample.sample_invoice = txt_sample_invoice.Text;
                jobSample.sample_invoice_date = CustomUtils.converFromDDMMYYYY(txt_sample_invoice_date.Text);
                jobSample.sample_invoice_amount_rpt = CustomUtils.isNumber(txt_sample_invoice_amount.Text) ? Convert.ToDouble(txt_sample_invoice_amount.Text) : 0;
                jobSample.sample_invoice_complete_date = CustomUtils.converFromDDMMYYYY(txt_sample_invoice_complete_date.Text);
                jobSample.isChecked = true;
                jobSample.Update();
                GeneralManager.Commit();
            }
            gvSample.EditIndex = -1;
            gvSample.DataSource = this.dataList;
            gvSample.DataBind();
        }

        protected void chkAllSign_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            foreach (GridViewRow row in gvSample.Rows)
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("cbSelect");
                chkcheck.Checked = cb.Checked;

                HiddenField sample_id = (HiddenField)row.FindControl("hid");
                job_sample jobSample = this.dataList.Find(x => x.ID == Convert.ToInt32(sample_id.Value));
                if (jobSample != null)
                {
                    jobSample.isChecked = cb.Checked;
                }
            }
        }

        protected void cbSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            var sample_id = cb.Parent.FindControl("hid") as HiddenField;
            job_sample jobSample = this.dataList.Find(x => x.ID == Convert.ToInt32(sample_id.Value));
            if (jobSample != null)
            {
                jobSample.isChecked = cb.Checked;
            }
        }


        protected void ExportToExcel()
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("INV");


                dt.Columns.Add("job_number", typeof(string));
                dt.Columns.Add("sample_so", typeof(string));
                dt.Columns.Add("sample_invoice", typeof(string));
                dt.Columns.Add("sample_invoice_date", typeof(string));
                dt.Columns.Add("sample_invoice_amount_rpt", typeof(Double));
                dt.Columns.Add("sample_invoice_complete_date", typeof(string));

                //this.searchResult

                String conSQL = Configurations.MySQLCon;
                using (MySqlConnection conn = new MySqlConnection("server = " + conSQL.Split(';')[2].Split('=')[2] + "; " + conSQL.Split(';')[3] + "; " + conSQL.Split(';')[4] + "; " + conSQL.Split(';')[5]))
                {
                    conn.Open();
                    String sql = string.Format("select job_number,sample_so,sample_invoice,DATE_FORMAT(sample_invoice_date, '%Y-%m-%d') as sample_invoice_date,sample_invoice_amount_rpt,DATE_FORMAT(sample_invoice_complete_date, '%Y-%m-%d') as sample_invoice_complete_date from job_sample where sample_so='{0}'", this.dataList[0].sample_so);


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

                Response.AddHeader("content-disposition", "attachment;filename=jobListBy_" + (this.dataList[0].sample_so == null ? "" : this.dataList[0].sample_so) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {


                List<string> extension = new List<string>() { ".xls", ".xlsx" };

                if (extension.Contains(Path.GetExtension(FileUpload3.FileName)))
                {
                    string yyyy = DateTime.Now.ToString("yyyy");
                    string MM = DateTime.Now.ToString("MM");
                    string dd = DateTime.Now.ToString("dd");

                    String source_file = String.Format(Configurations.PATH_SOURCE, yyyy, MM, dd, this.dataList[0].sample_so, Path.GetFileName(FileUpload3.FileName));

                    if (!Directory.Exists(Path.GetDirectoryName(source_file)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(source_file));
                    }
                    FileUpload3.SaveAs(source_file);

                    FileInfo excel = new FileInfo(source_file);
                    using (var package = new ExcelPackage(excel))
                    {
                        var workbook = package.Workbook;

                        //*** Sheet 1
                        var worksheet = workbook.Worksheets.First();

                        //*** Result
                        int totalRows = worksheet.Dimension.End.Row;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            string job_number = worksheet.Cells[i, 1].Text.ToString();
                            string sample_so = worksheet.Cells[i, 2].Text.ToString();
                            string sample_invoice = worksheet.Cells[i, 3].Text.ToString();
                            string sample_invoice_date = Convert.ToDateTime(worksheet.Cells[i, 4].Value).ToString("yyyy-MM-dd");
                            Double sample_invoice_amount_rpt = Convert.ToDouble(worksheet.Cells[i, 5].Text.ToString());
                            string sample_invoice_complete_date = Convert.ToDateTime(worksheet.Cells[i, 6].Value).ToString("yyyy-MM-dd");

                            string updateSQL = "UPDATE job_sample SET sample_invoice_date = '{0}', sample_invoice_complete_date = '{1}', sample_so = '{2}', sample_invoice_amount_rpt = '{3}',sample_invoice = '{4}' WHERE(job_number = '{5}' and sample_so = '{6}')";
                            updateSQL = string.Format(updateSQL, sample_invoice_date, sample_invoice_complete_date, sample_so, sample_invoice_amount_rpt, sample_invoice, job_number, sample_so);
                            MaintenanceBiz.ExecuteReturnDt(updateSQL);

                        }
                        removeSession();
                        MessageBox.Show(this.Page, Resources.MSG_SAVE_SUCCESS, PreviousPath);
                    }
                }
                else
                {
                    Message = "<div class=\"alert alert-danger\"><strong>Error!</strong>สามารถอัพโหลดได้เฉพาะ ไฟล์ *.xls|xlxs </div>";
                }
            }catch(Exception ex)
            {
                MessageBox.Show(this.Page, "เกิดข้อผิดพลาดในการอัพโหลดไฟล์ กรุณาตรวจสอบความถูกต้องของข้อมูล");
            }
        }

    }
}