using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        public Boolean isGroupApproveOperation
        {
            get { return (Boolean)Session[GetType().Name + "isGroupApproveOperation"]; }
            set { Session[GetType().Name + "isGroupApproveOperation"] = value; }
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

            btnSave.Visible = this.dataList.Count > 0;
            pChemist.Visible = (this.dataList.Count > 0) && !this.isChangeDueDateGroup && !this.isPoGroupOperation && !this.isInvoiceGroupOperation && !this.isSentToCusDateOperation && !this.isNoteGroupOpeation && !this.isCusRefNoGroupOperation;
            pChangeDueDate.Visible = (this.dataList.Count > 0) && !this.isPoGroupOperation && (this.isChangeDueDateGroup || this.isSentToCusDateOperation) && !this.isInvoiceGroupOperation && !this.isNoteGroupOpeation && !this.isCusRefNoGroupOperation;
            pAccount2.Visible = (this.dataList.Count > 0) && !this.isPoGroupOperation && !this.isChangeDueDateGroup && !isNoteGroupOpeation && this.isInvoiceGroupOperation && !this.isCusRefNoGroupOperation;
            pCusRefNo.Visible = this.isCusRefNoGroupOperation;
            if (!btnSave.Visible)
            {
                lbDesc.Text = "รายการที่เลือกไม่ได้ถูกกำหนดเป็นงานแบบกลุ่ม";

            }

            if (isInvoiceGroupOperation && this.dataList.Count > 0)
            {
                txtInvoice.Text = this.dataList[0].sample_invoice;
                txtInvoiceAmt.Text = this.dataList[0].sample_invoice_amount.ToString();
                txtInvoiceDate.Text = this.dataList[0].sample_invoice_date.Value.ToString("dd/MM/yyyy");
                txtPaymentDate.Text = this.dataList[0].sample_invoice_complete_date.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                gvSample.Columns[0].Visible = true;
                gvSample.Columns[1].Visible = false;
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

                String desc2 = String.Empty;
                if (isPoGroupOperation) desc2 = "(PO)";
                if (isChangeDueDateGroup) desc2 = "(Duedate)";
                if (isInvoiceGroupOperation) desc2 = "(Invoice)";
                if (isSentToCusDateOperation) desc2 = "(Date Admin Sent to Customer)";
                if (isCusRefNoGroupOperation) desc2 = "(Customer ref no)";

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
                                    //5|Extend 2

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
                                            jobSample.due_date_customer = hc.GetWorkingDayLab(CustomUtils.converFromDDMMYYYY(txtDuedate.Text),0);
                                            break;
                                    }
                                }
                            }
                            break;
                        case RoleEnum.ADMIN:
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
                    jobSample.sample_invoice = txtInvoice.Text;
                    jobSample.sample_invoice_amount = String.IsNullOrEmpty(txtInvoiceAmt.Text) ? Convert.ToDouble("0") : Convert.ToDouble(txtInvoiceAmt.Text);
                    if (!String.IsNullOrEmpty(txtInvoiceDate.Text))
                    {
                        jobSample.sample_invoice_date = CustomUtils.converFromDDMMYYYY(txtInvoiceDate.Text);
                        jobSample.sample_invoice_status = Convert.ToInt16(PaymentStatus.PAYMENT_INPROCESS);
                    }
                    if (!String.IsNullOrEmpty(txtPaymentDate.Text))
                    {
                        jobSample.sample_invoice_status = Convert.ToInt16(PaymentStatus.PAYMENT_COMPLETE);
                        jobSample.sample_invoice_complete_date = CustomUtils.converFromDDMMYYYY(txtInvoiceDate.Text);
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
                }else if (this.isGroupApproveOperation)
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
    }
}