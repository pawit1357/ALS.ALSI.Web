using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;

namespace ALS.ALSI.Web.view.request
{

    public partial class JobRequest : System.Web.UI.Page
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(JobRequest));

        #region "Property"

        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }

        public job_info objJobInfo
        {
            get
            {
                job_info job = new job_info();
                job.ID = int.Parse(hJobID.Value);
                job.contract_person_id = String.IsNullOrEmpty(ddlContract_person_id.SelectedValue) ? 0 : int.Parse(ddlContract_person_id.SelectedValue);
                job.customer_address_id = String.IsNullOrEmpty(ddlAddress.SelectedValue) ? 0 : int.Parse(ddlAddress.SelectedValue);

                job.customer_id = String.IsNullOrEmpty(ddlCustomer_id.SelectedValue) ? 0 : int.Parse(ddlCustomer_id.SelectedValue);
                job.date_of_request = String.IsNullOrEmpty(txtDateOfRequest.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtDateOfRequest.Text);
                job.customer_ref_no = txtCustomer_ref_no.Text;
                job.company_name_to_state_in_report = txtCompany_name_to_state_in_report.InnerText;
                job.job_number = String.IsNullOrEmpty(txtJob_number.Text) ? 0 : int.Parse(txtJob_number.Text);
                job.job_prefix = String.IsNullOrEmpty(ddlJobNumber.SelectedValue) ? 0 : Convert.ToInt32(ddlJobNumber.SelectedValue);
                job.date_of_receive = String.IsNullOrEmpty(txtDate_of_receive.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtDate_of_receive.Text);
                //job.s_pore_ref_no = txtS_pore_ref_no.Text;
                job.spec_ref_rev_no = txtSpecRefRevNo.Text;
                //job.customer_po_ref = string.Empty;//invoice

                job.sample_diposition = rdSample_dipositionYes.Checked ? "Y" : "N";
                job.status_sample_enough = rdSample_enoughNo.Checked ? "Y" : "N";
                job.status_sample_full = rdPersonel_and_workloadYes.Checked ? "Y" : "N";
                job.status_personel_and_workload = rdTest_toolYes.Checked ? "Y" : "N";
                job.status_test_tool = rdTest_toolYes.Checked ? "Y" : "N";
                job.status_test_method = rdTest_methodYes.Checked ? "Y" : "N";




                job.create_by = userLogin.id;
                job.update_by = userLogin.id;
                job.create_date = DateTime.Now;
                job.update_date = DateTime.Now;
                job.document_type = "1";

                job.jobSample = listSample;
                return job;
            }
        }

        public job_reiew_requistion objReviewRequistion
        {
            get
            {
                job_reiew_requistion JobReviewRequistion = new job_reiew_requistion();
                JobReviewRequistion.job_id = objJobInfo.ID;
                JobReviewRequistion.detail = string.Empty;
                JobReviewRequistion.status = string.Empty;
                JobReviewRequistion.create_by = userLogin.id;
                JobReviewRequistion.create_date = DateTime.Now;
                JobReviewRequistion.update_by = userLogin.id;
                JobReviewRequistion.update_date = DateTime.Now;
                return JobReviewRequistion;
            }
        }

        public m_customer obj
        {
            get
            {
                m_customer tmp = new m_customer();
                //tmp.customer_code = txtCustomerCode.Text;
                tmp.company_name = txtCompanyName.Text;

                return tmp;
            }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        public int JobID
        {
            get { return (int)Session[GetType().Name + "JobID"]; }
            set { Session[GetType().Name + "JobID"] = value; }
        }

        public int SampleID
        {
            get { return (int)Session[GetType().Name + "SampleID"]; }
            set { Session[GetType().Name + "SampleID"] = value; }
        }

        public List<job_sample> listSample
        {
            get { return (List<job_sample>)Session[GetType().Name + "Sample"]; }
            set { Session[GetType().Name + "Sample"] = value; }
        }

        public List<job_sample> listSampleShow
        {
            get { return listSample.FindAll(x => x.RowState != CommandNameEnum.Delete); }
        }

        public IEnumerable searchCustomerResult
        {
            get { return (IEnumerable)Session[GetType().Name + "searchCustomerResult"]; }
            set { Session[GetType().Name + "searchCustomerResult"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            SearchJobRequest prvPage = Page.PreviousPage as SearchJobRequest;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.JobID = (prvPage == null) ? this.JobID : prvPage.JobID;
            this.SampleID = (prvPage == null) ? this.SampleID : prvPage.SampleID;
            this.PreviousPath = Constants.LINK_SEARCH_JOB_REQUEST;

            if (!Page.IsPostBack)
            {
                //initialRequire();
                initialPage();
            }
        }

        #region ""
        private void initialPage()
        {
            this.listSample = new List<job_sample>();

            m_customer customer = new m_customer();
            ddlCustomer_id.Items.Clear();
            ddlCustomer_id.DataSource = customer.SelectAll();
            ddlCustomer_id.DataBind();
            ddlCustomer_id.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            ddlContract_person_id.Items.Clear();
            ddlContract_person_id.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            m_specification specification = new m_specification();

            ddlSecification_id.Items.Clear();
            ddlSecification_id.DataSource = specification.SelectAll();
            ddlSecification_id.DataBind();
            ddlSecification_id.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));



            ddlJobNumber.Items.Clear();
            ddlJobNumber.DataSource = new job_running().SelectAll();
            ddlJobNumber.DataBind();
            ddlJobNumber.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));


            m_completion_scheduled m_completion_scheduled = new m_completion_scheduled();
            ddlCompletionScheduled.DataSource = m_completion_scheduled.SelectAll();
            ddlCompletionScheduled.DataBind();

            ddlAddress.Items.Clear();
            ddlAddress.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            ddlStatus.Items.Clear();
            ddlStatus.DataSource = new m_status().SelectByMainStatus();
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));


            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    txtDateOfRequest.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate_of_receive.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    ddlCustomer_id.Enabled = true;
                    ddlContract_person_id.Enabled = true;
                    ddlSecification_id.Enabled = true;
                    txtDateOfRequest.Enabled = true;
                    txtCustomer_ref_no.Enabled = true;
                    txtCompany_name_to_state_in_report.Attributes.Remove("readonly");
                    //txtCompany_name_to_state_in_report.Attributes["readonly"] = "readonly";
                    txtJob_number.Enabled = true;
                    txtDate_of_receive.Enabled = true;
                    //txtS_pore_ref_no.Enabled = true;
                    txtSpecRefRevNo.Enabled = true;
                    txtCustomer_ref_no.Enabled = true;
                    rdSample_enoughNo.Enabled = true;
                    rdPersonel_and_workloadYes.Enabled = true;
                    rdPersonel_and_workloadYes.Enabled = true;
                    rdTest_toolYes.Enabled = true;
                    rdTest_methodYes.Enabled = true;
                    //rdCompletion_scheduledNormal_Normal.Enabled = true;
                    //rdCompletion_scheduledNormal_Urgent.Enabled = true;
                    //rdCompletion_scheduledNormal_Express.Enabled = true;
                    rdSample_dipositionYes.Enabled = true;

                    //txtSpecification_other.Enabled = true;
                    //lbOther.Visible = false;


                    btnAddSampleInfo.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.Edit:
                    fillinScreen();


                    btnAddSampleInfo.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.View:
                    fillinScreen();

                    ddlCustomer_id.Attributes["readonly"] = "readonly";
                    ddlContract_person_id.Attributes["readonly"] = "readonly";
                    ddlSecification_id.Attributes["readonly"] = "readonly";
                    txtDateOfRequest.Attributes["readonly"] = "readonly";
                    txtCustomer_ref_no.Attributes["readonly"] = "readonly";
                    txtCompany_name_to_state_in_report.Attributes["readonly"] = "readonly";
                    txtJob_number.Attributes["readonly"] = "readonly";
                    txtDate_of_receive.Attributes["readonly"] = "readonly";
                    //txtS_pore_ref_no.Attributes["readonly"] = "readonly";
                    txtSpecRefRevNo.Attributes["readonly"] = "readonly";
                    txtCustomer_ref_no.Attributes["readonly"] = "readonly";
                    rdSample_enoughNo.Attributes["readonly"] = "readonly";
                    rdPersonel_and_workloadYes.Attributes["readonly"] = "readonly";
                    rdPersonel_and_workloadYes.Attributes["readonly"] = "readonly";
                    rdTest_toolYes.Attributes["readonly"] = "readonly";
                    rdTest_methodYes.Attributes["readonly"] = "readonly";
                    //rdCompletion_scheduledNormal_Normal.Attributes["readonly"] = "readonly";
                    //rdCompletion_scheduledNormal_Urgent.Attributes["readonly"] = "readonly";
                    //rdCompletion_scheduledNormal_Express.Attributes["readonly"] = "readonly";
                    rdSample_dipositionYes.Attributes["readonly"] = "readonly";

                    //txtSpecification_other.Enabled = false;
                    //lbOther.Visible = false;
                    btnAddSampleInfo.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;

                    break;
            }

            //txtSpecification_other.Visible = false;
        }

        private void fillinScreen()
        {
            job_info job = new job_info().SelectByID(this.JobID);

            hJobID.Value = job.ID.ToString();
            ddlCustomer_id.SelectedValue = job.customer_id.ToString();
            txtDate_of_receive.Text = job.date_of_receive.Value.ToString("MM/dd/yyyy");

            m_customer_contract_person contact = new m_customer_contract_person();
            List<m_customer_contract_person> listContractPerson = contact.SelectAllByCusID(job.customer_id);
            if (listContractPerson != null && listContractPerson.Count > 0)
            {
                ddlContract_person_id.DataSource = listContractPerson;
                ddlContract_person_id.DataBind();

                ddlContract_person_id.SelectedValue = job.contract_person_id.ToString();
                m_customer_contract_person contactPerson = new m_customer_contract_person().SelectByID(job.contract_person_id);
                if (contactPerson != null)
                {
                    txtDepartment.Text = contactPerson.department;
                    txtEmail.Text = contactPerson.email;

                }
            }
            //
            m_customer_address cusAddress = new m_customer_address();
            List<m_customer_address> litAddress = cusAddress.SelectAllByCusID(job.customer_id);
            if (litAddress != null && litAddress.Count > 0)
            {
                ddlAddress.DataSource = litAddress;
                ddlAddress.DataBind();
                if (job.customer_address_id != null && job.customer_address_id > 0)
                {
                    m_customer_address addr = new m_customer_address().SelectByID(job.customer_address_id.Value);
                    if (addr != null)
                    {
                        ddlAddress.SelectedValue = job.customer_address_id.ToString();
                        txtTelNumber.Text = addr.telephone;
                        txtFax.Text = addr.fax;
                    }
                }
                else
                {
                    ddlAddress.SelectedIndex = 0;
                }
            }

            m_customer customer = new m_customer().SelectByID(job.customer_id);
            if (customer != null)
            {
                hCompanyId.Value = customer.ID + "";
                txtCompanyName.Text = customer.company_name;
            }
            else
            {
                //TODO
            }



            ddlJobNumber.SelectedValue = job.job_prefix.ToString();
            txtJob_number.Text = Convert.ToInt32(job.job_number).ToString("00000");
            ddlSecification_id.SelectedIndex = -1;
            txtDateOfRequest.Text = Convert.ToDateTime(job.date_of_request).ToString("dd/MM/yyyy");
            txtCustomer_ref_no.Text = job.customer_ref_no;
            txtCompany_name_to_state_in_report.InnerText = job.company_name_to_state_in_report;

            txtDate_of_receive.Text = Convert.ToDateTime(job.date_of_receive).ToString("dd/MM/yyyy");

            //txtS_pore_ref_no.Text = job.s_pore_ref_no;
            txtSpecRefRevNo.Text = job.spec_ref_rev_no;
            txtCustomer_ref_no.Text = job.customer_ref_no;

            rdSample_enoughNo.Checked = job.status_sample_enough.Equals("Y") ? true : false;
            rdPersonel_and_workloadYes.Checked = job.status_sample_full.Equals("Y") ? true : false;
            rdPersonel_and_workloadYes.Checked = job.status_personel_and_workload.Equals("Y") ? true : false;
            rdTest_toolYes.Checked = job.status_test_tool.Equals("Y") ? true : false;
            rdTest_methodYes.Checked = job.status_test_method.Equals("Y") ? true : false;
            rdSample_dipositionYes.Checked = job.sample_diposition.Equals("Y") ? true : false;



            this.listSample = job_sample.FindAllByJobID(job.ID);
            this.listSample = (this.CommandName == CommandNameEnum.Edit) ? this.listSample.Where(x => x.ID.Equals(this.SampleID)).ToList() : this.listSample;
            foreach (job_sample tmp in this.listSample)
            {
                tmp.RowState = CommandNameEnum.Edit;
            }

            ddlStatus.SelectedValue = listSample[0].job_status.ToString();
            ddlStatus.Enabled = (this.CommandName == CommandNameEnum.Edit);
            gvSample.DataSource = listSample;
            gvSample.DataBind();


        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "JobID");
            Session.Remove(GetType().Name + "SampleID");
        }

        #endregion

        protected void ddlCustomer_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlCustomer_id.SelectedValue.Equals(""))
            {
                m_customer cus = new m_customer().SelectByID(Convert.ToInt32(ddlCustomer_id.SelectedValue));
                if (cus != null)
                {
                    hCompanyId.Value = cus.ID + "";
                    txtCompanyName.Text = cus.company_name;

                    //txtDepartment.Text = cus.department;
                    //txtTelNumber.Text = cus.tel_number;
                    //txtEmail.Text = cus.email_address;
                    //txtAddress.InnerText = cus.address;

                    //List<m_customer_contract_person> contractPersonList = new m_customer_contract_person().FindAllByCompanyID(cus.ID);
                    //if (contractPersonList != null && contractPersonList.Count > 0)
                    //{
                    //    ddlContract_person_id.Items.Clear();
                    //    ddlContract_person_id.Items.Add(new ListItem(Constants.PLEASE_SELECT, ""));
                    //    ddlContract_person_id.DataSource = contractPersonList;
                    //    ddlContract_person_id.DataBind();

                    //    ddlContract_person_id.Enabled = true;
                    //}
                    //else
                    //{
                    //    //TODO
                    //}

                    //
                    m_customer_contract_person contact = new m_customer_contract_person();
                    List<m_customer_contract_person> listContractPerson = contact.SelectAllByCusID(cus.ID);
                    if (listContractPerson != null && listContractPerson.Count > 0)
                    {
                        ddlContract_person_id.DataSource = listContractPerson;
                        ddlContract_person_id.DataBind();

                        contact = listContractPerson[0];
                        txtDepartment.Text = contact.department;
                        txtEmail.Text = contact.email;
                    }
                    //
                    m_customer_address cusAddress = new m_customer_address();
                    List<m_customer_address> litAddress = cusAddress.SelectAllByCusID(cus.ID);
                    if (litAddress != null && litAddress.Count > 0)
                    {
                        ddlAddress.DataSource = litAddress;
                        ddlAddress.DataBind();
                        cusAddress = litAddress[0];
                        txtTelNumber.Text = cusAddress.telephone;
                        txtFax.Text = cusAddress.fax;
                    }
                }
                else
                {
                    //TODO
                }
            }
        }
        protected void ddlAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_customer_address addr = new m_customer_address().SelectByID(Convert.ToInt32(ddlAddress.SelectedValue));
            if (addr != null)
            {
                //txtAddress.InnerText = addr.address;
                txtTelNumber.Text = addr.telephone;
                txtFax.Text = addr.fax;
            }
        }
        protected void ddlContract_person_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_customer_contract_person contact = new m_customer_contract_person().FindByID(Convert.ToInt32(ddlContract_person_id.SelectedValue));
            if (contact != null)
            {
                txtDepartment.Text = contact.department;
                txtEmail.Text = contact.email;
            }
        }

        protected void ddlSecification_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlSecification_id.SelectedValue.Equals(""))
            {
                //if (ddlSecification_id.SelectedItem.Text.ToUpper().Contains("OTHER"))
                //{
                //    txtSpecification_other.Visible = true;
                //    lbOther.Visible = true;
                //}
                //else
                //{
                //List<m_type_of_test> disPlayTT = new List<m_type_of_test>();
                List<m_type_of_test> lists = new m_type_of_test().SelectBySpecificationId(int.Parse(ddlSecification_id.SelectedValue));//.SelectParent(int.Parse(ddlSecification_id.SelectedValue));
                //List<m_type_of_test> parents = lists.FindAll(x => x.parent == -1);
                foreach (m_type_of_test val in lists)
                {

                    val.customName = String.Format("{0}-[{1}]{2}", val.data_group, val.prefix, val.name);

                    //disPlayTT.Add(parent);

                    //List<m_type_of_test> childs = new m_type_of_test().SelectChild(parent.ID);
                    //if (childs != null)
                    //{
                    //    if (childs.Count > 0)
                    //    {
                    //        foreach (m_type_of_test child in childs)
                    //        {
                    //            child.customName = parent.name + "-" + child.name + "(" + child.prefix + ")";
                    //            disPlayTT.Add(child);
                    //        }
                    //    }
                    //}
                }
                lstTypeOfTest.Attributes.Add("multiple", "");
                lstTypeOfTest.DataSource = lists;
                lstTypeOfTest.DataBind();
                //}
                //txtSpecification_other.Text = string.Empty;
                //txtSpecification_other.Visible = false;
                //lbOther.Visible = false;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Remove Delete
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    objJobInfo.Insert();
                    break;
                case CommandNameEnum.Edit:
                    foreach (job_sample s in this.listSample)
                    {
                        s.job_status = Convert.ToInt16(ddlStatus.SelectedValue);
                        s.sample_prefix = ddlJobNumber.SelectedItem.Text;
                        s.update_by = userLogin.id;
                        s.update_date = DateTime.Now;
                    }

                    objJobInfo.Update();
                    break;
            }
            //Commit
            GeneralManager.Commit();
            removeSession();
            MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
        }

        protected void btnR_Click(object sender, EventArgs e)
        {

        }

        protected void btnAM_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddSampleInfo_Click(object sender, EventArgs e)
        {

            foreach (ListItem item in lstTypeOfTest.Items)
            {

                if (item.Selected)
                {

                    m_type_of_test tmp = new m_type_of_test().SelectByID(int.Parse(item.Value));
                    if (tmp != null)
                    {
                        job_sample jobSample = new job_sample();
                        jobSample.ID = CustomUtils.GetRandomNumberID();
                        jobSample.template_id = -1;


                        jobSample.job_id = Convert.ToInt32(hJobID.Value);
                        jobSample.type_of_test_id = tmp.ID;
                        jobSample.specification_id = String.IsNullOrEmpty(ddlSecification_id.SelectedValue) ? 0 : int.Parse(ddlSecification_id.SelectedValue);
                        jobSample.job_number = ddlJobNumber.SelectedItem.Text + "-" + txtJob_number.Text + "-" + tmp.prefix;
                        jobSample.description = txtDescriptoin.Text;
                        jobSample.model = txtModel.Text;
                        jobSample.surface_area = txtSurfaceArea.Text;
                        jobSample.remarks = txtRemark.Text;
                        jobSample.no_of_report = 1;// Convert.ToInt16(ddlNoOfReport.SelectedValue);
                        jobSample.uncertainty = (rdUncertaintyYes.Checked) ? "Y" : "N";
                        jobSample.RowState = CommandNameEnum.Add;
                        jobSample.job_status = Convert.ToInt32(StatusEnum.LOGIN_CONVERT_TEMPLATE);


                        jobSample.job_role = userLogin.role_id;
                        jobSample.status_completion_scheduled = Convert.ToInt32(ddlCompletionScheduled.SelectedValue);
                        //jobSample.due_date = Convert.ToDateTime(objJobInfo.date_of_receive.Value);
                        jobSample.update_date = Convert.ToDateTime(objJobInfo.date_of_receive.Value);
                        jobSample.update_by = userLogin.id;
                        jobSample.is_hold = "0";//0=Unhold
                        jobSample.part_no = txtPartNo.Text;
                        jobSample.part_name = txtPartName.Text;
                        jobSample.lot_no = txtLotNo.Text;
                        jobSample.other_ref_no = txtOtherRefNo.Text;
                        #region "Special Flow"

                        /*
                        *ELP:       Login(convert template , select spec) > Chemist testing > Sr. chemist checking > Admin(Word) > Lab Manager > Admin (Upload file PDF)
                        ELS:       	Login(convert template(Ad hoc template)) >Chemist testing(Upload file) > Sr. chemist checking
                        ELN:       	Login(convert template(Ad hoc template)) > Chemist testing(Upload file) > Sr. chemist checking > Admin(Word) > Lab Manager  Admin (Upload file PDF)
                        FA:         Login > Chemist testing(Upload file) > Admin(Word) > Lab Manager > Admin (Upload file PDF)
                        GRP:       	Login > Admin(Upload file PDF)
                        ELWA :    	Login > Chemist testing(Upload file) > Sr. chemist checking > Admin(Word) > Lab Manager > Admin (Upload file PDF)
                         * */

                        if (!String.IsNullOrEmpty(ddlJobNumber.SelectedValue))
                        {
                            switch (ddlJobNumber.SelectedItem.Text.ToUpper())
                            {
                                case "ELS":
                                    jobSample.template_id = 1;
                                    jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                                    break;
                                case "ELN":

                                    if (tmp.ref_template_id != null)
                                    {
                                        jobSample.template_id = tmp.ref_template_id.Value;
                                    }
                                    else
                                    {
                                        jobSample.template_id = 2;
                                    }


                                    jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);

                                    break;
                                case "FA":
                                    jobSample.template_id = 3;
                                    jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                                    break;
                                case "GRP":
                                    jobSample.template_id = 4;
                                    jobSample.job_status = Convert.ToInt32(StatusEnum.ADMIN_CONVERT_PDF);
                                    break;
                                case "ELWA":
                                    jobSample.template_id = 5;
                                    jobSample.job_status = Convert.ToInt32(StatusEnum.CHEMIST_TESTING);
                                    break;
                                default://ELP 
                                    break;
                            }
                        }
                        #endregion

                        m_completion_scheduled cs = new m_completion_scheduled().SelectByID(Convert.ToInt32(jobSample.status_completion_scheduled));
                        if (cs != null)
                        {
                            if (objJobInfo != null)
                            {
                                if (Constants.OTHER_REF_NOS.Contains(jobSample.other_ref_no))
                                {
                                    jobSample.due_date_lab = new DateTime(1, 1, 1);
                                    jobSample.due_date_customer = Convert.ToDateTime(objJobInfo.date_of_receive.Value).AddDays(Convert.ToInt32(cs.customer_due_date));
                                }
                                else
                                {
                                    jobSample.due_date = Convert.ToDateTime(objJobInfo.date_of_receive.Value).AddDays(Convert.ToInt32(cs.value));
                                    jobSample.due_date_customer = Convert.ToDateTime(objJobInfo.date_of_receive.Value).AddDays(Convert.ToInt32(cs.customer_due_date));
                                    jobSample.due_date_lab = Convert.ToDateTime(objJobInfo.date_of_receive.Value).AddDays(Convert.ToInt32(cs.lab_due_date));
                                }
                            }
                        }
                        else
                        {
                            //logger.Debug("m_completion_scheduled is Empty!");
                        }
                        jobSample.sample_prefix = ddlJobNumber.SelectedItem.Text;

                        listSample.Add(jobSample);
                    }
                    else
                    {
                        // not find type of test
                    }
                }

            }

            gvSample.DataSource = listSampleShow;
            gvSample.PageIndex = 0;
            gvSample.DataBind();
            //Clear old value

            //txtDescriptoin.Text = string.Empty;
            //txtModel.Text = string.Empty;
            //txtSurfaceArea.Text = string.Empty;
            //txtRemark.Text = string.Empty;
            //rdUncertaintyNo.Checked = true;
            //ddlSecification_id.SelectedIndex = -1;
            //ddlCompletionScheduled.SelectedIndex = -1;
            lstTypeOfTest.ClearSelection();

        }

        #region "GV SAMPLE"

        protected void gvSample_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {


                    LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                    Literal litRefNo = (Literal)e.Row.FindControl("litRefNo");

                    if (litRefNo != null)
                    {

                        #region "Check Amend/Retest"
                        int amCount = Convert.ToInt16(gvSample.DataKeys[e.Row.RowIndex][6]);
                        int reCount = Convert.ToInt16(gvSample.DataKeys[e.Row.RowIndex][7]);
                        String amendOrRetest = gvSample.DataKeys[e.Row.RowIndex][8] == null ? String.Empty : gvSample.DataKeys[e.Row.RowIndex][8].ToString();

                        switch (amendOrRetest)
                        {
                            case "AM":
                                litRefNo.Text = String.Format("{0}({1}{2})", litRefNo.Text, amendOrRetest, amCount);
                                break;
                            case "R":
                                litRefNo.Text = String.Format("{0}({1}{2})", litRefNo.Text, amendOrRetest, reCount);
                                break;
                        }
                        #endregion

                    }

                    Literal _litStatus_completion_scheduled = (Literal)e.Row.FindControl("litStatus_completion_scheduled");
                    if (btnDelete != null)
                    {
                        m_completion_scheduled m_completion_scheduled = new m_completion_scheduled();
                        m_completion_scheduled = m_completion_scheduled.SelectByID(Convert.ToInt32(_litStatus_completion_scheduled.Text));
                        if (m_completion_scheduled != null)
                        {
                            _litStatus_completion_scheduled.Text = m_completion_scheduled.name;
                        }
                        //btnDelete.Visible = (this.CommandName == CommandNameEnum.Add);
                        btnEdit.Visible = !(this.CommandName == CommandNameEnum.View);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                }

            }
        }

        protected void gvSample_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSample.EditIndex = -1;
            gvSample.DataSource = listSampleShow;
            gvSample.DataBind();
            btnAddSampleInfo.Enabled = true;
        }

        protected void gvSample_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.SampleID = int.Parse(gvSample.DataKeys[e.RowIndex].Values[0].ToString());

            job_sample js = listSample.Find(x => x.ID == this.SampleID);
            if (js != null)
            {
                js.RowState = CommandNameEnum.Delete;
                gvSample.DataSource = listSampleShow;
                gvSample.DataBind();
            }
            btnAddSampleInfo.Enabled = true;
        }

        protected void gvSample_RowEditing(object sender, GridViewEditEventArgs e)
        {


            gvSample.EditIndex = e.NewEditIndex;
            gvSample.DataSource = listSampleShow;
            gvSample.DataBind();

            String _no_of_report = gvSample.DataKeys[e.NewEditIndex].Values[3].ToString();
            String _uncertainty = gvSample.DataKeys[e.NewEditIndex].Values[4].ToString();

            String _status_completion_scheduled = gvSample.DataKeys[e.NewEditIndex].Values[5].ToString();
            DropDownList _ddlNoOfReport = (DropDownList)gvSample.Rows[e.NewEditIndex].FindControl("ddlNoOfReport");
            DropDownList _ddlUncertaint = (DropDownList)gvSample.Rows[e.NewEditIndex].FindControl("ddlUncertaint");
            DropDownList _ddlCompletionScheduled = (DropDownList)gvSample.Rows[e.NewEditIndex].FindControl("ddlCompletionScheduled");

            TextBox txtRefNo = (TextBox)gvSample.Rows[e.NewEditIndex].FindControl("txtRefNo");


            #region "Check Amend/Retest"
            String amendOrRetest = gvSample.DataKeys[e.NewEditIndex][8] == null ? String.Empty : gvSample.DataKeys[e.NewEditIndex][8].ToString();
            #endregion



            txtRefNo.Enabled = String.IsNullOrEmpty(amendOrRetest);



            _ddlNoOfReport.Items.Clear();
            for (int i = 1; i <= 5; i++)
            {
                ListItem lt = new ListItem(i + "", i.ToString());
                _ddlNoOfReport.Items.Add(lt);
            }
            _ddlNoOfReport.SelectedValue = _no_of_report;

            _ddlUncertaint.Items.Clear();
            _ddlUncertaint.Items.Add(new ListItem("Y", "Y"));
            _ddlUncertaint.Items.Add(new ListItem("N", "N"));
            _ddlUncertaint.SelectedValue = _uncertainty;


            m_completion_scheduled m_completion_scheduled = new m_completion_scheduled();
            _ddlCompletionScheduled.DataSource = m_completion_scheduled.SelectAll();
            _ddlCompletionScheduled.DataBind();

        }

        protected void gvSample_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String jobNumber = gvSample.DataKeys[e.RowIndex].Values[2].ToString();
            int Id = Convert.ToInt32(gvSample.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox _txtRefNo = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtRefNo");
            TextBox _txtOtherRefNo = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtOtherRefNo");

            TextBox _txtDescriptoin = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtDescriptoin");
            TextBox _txtModel = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtModel");
            TextBox _txtSurfaceArea = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtSurfaceArea");
            TextBox _txtRemark = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtRemark");
            DropDownList _ddlNoOfReport = (DropDownList)gvSample.Rows[e.RowIndex].FindControl("ddlNoOfReport");
            DropDownList _ddlUncertaint = (DropDownList)gvSample.Rows[e.RowIndex].FindControl("ddlUncertaint");
            DropDownList _ddlCompletionScheduled = (DropDownList)gvSample.Rows[e.RowIndex].FindControl("ddlCompletionScheduled");

            TextBox _txtPartNo = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtPartNo");
            TextBox _txtPartName = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtPartName");
            TextBox _txtLotNo = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtLotNo");


            job_sample jobSample = listSample.Find(x => x.ID == Id);
            if (jobSample != null)
            {
                jobSample.job_id = objJobInfo.ID;
                //if (this.CommandName == CommandNameEnum.Add)
                //{
                    jobSample.job_number = _txtRefNo.Text;
                //}
                jobSample.description = _txtDescriptoin.Text;
                jobSample.model = _txtModel.Text;
                jobSample.surface_area = _txtSurfaceArea.Text;
                jobSample.remarks = _txtRemark.Text;
                jobSample.other_ref_no = _txtOtherRefNo.Text;
                jobSample.no_of_report = Convert.ToInt16(_ddlNoOfReport.SelectedValue);
                jobSample.uncertainty = _ddlUncertaint.SelectedValue;
                jobSample.status_completion_scheduled = Convert.ToInt32(_ddlCompletionScheduled.SelectedValue);

                jobSample.part_no = _txtPartNo.Text;
                jobSample.part_name = _txtPartName.Text;
                jobSample.lot_no = _txtLotNo.Text;

            }
            gvSample.EditIndex = -1;
            gvSample.DataSource = listSampleShow;
            gvSample.DataBind();
            btnAddSampleInfo.Enabled = true;
        }

        protected void gvSample_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
        }
        #endregion

        protected void ddlJobNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlJobNumber.SelectedValue.Equals(""))
            {
                int rn = job_running.GetRunning(int.Parse(ddlJobNumber.SelectedValue));
                if (rn != -1)
                {
                    txtJob_number.Text = rn.ToString("0000");
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + ddlJobNumber.SelectedItem.Text + " hot have initial running.');", true);
                }
            }
            else
            {
                txtJob_number.Text = string.Empty;
            }
        }

        protected void gvSample_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = listSampleShow;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }








    }
}