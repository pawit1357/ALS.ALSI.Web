using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.customer
{
    public partial class Customer : System.Web.UI.Page
    {

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
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

        public int PKID
        {
            get { return (int)Session[GetType().Name + "PKID"]; }
            set { Session[GetType().Name + "PKID"] = value; }
        }

        public int CONTRACT_PKID
        {
            get { return (int)Session[GetType().Name + "CONTRACT_PKID"]; }
            set { Session[GetType().Name + "CONTRACT_PKID"] = value; }
        }

        public int ADDR_PKID
        {
            get { return (int)Session[GetType().Name + "ADDR_PKID"]; }
            set { Session[GetType().Name + "ADDR_PKID"] = value; }
        }
        public List<m_customer_contract_person> List
        {
            get { return (List<m_customer_contract_person>)Session[GetType().Name + "m_customer_contract_person"]; }
            set { Session[GetType().Name + "m_customer_contract_person"] = value; }
        }

        public List<m_customer_contract_person> ListShow
        {
            get { return List.FindAll(x => x.RowState != CommandNameEnum.Delete); }
        }

        public List<m_customer_address> ListCustomerAddress
        {
            get { return (List<m_customer_address>)Session[GetType().Name + "m_customer_address"]; }
            set { Session[GetType().Name + "m_customer_address"] = value; }
        }

        public List<m_customer_address> ListCustomerAddressShow
        {
            get { return ListCustomerAddress.FindAll(x => x.RowState != CommandNameEnum.Delete); }
        }



        public m_customer objCustomer
        {
            get
            {
                m_customer cus = new m_customer();
                cus.ID = PKID;
                cus.customer_code = string.Empty;
                cus.company_name = txtCompanyName.Text;
                //cus.address = txtAddress.InnerText;
                cus.sub_district = string.Empty;
                cus.mobile_number = string.Empty;
                //cus.email_address = txtEmail.Text;
                cus.branch = string.Empty;
                cus.district = string.Empty;
                cus.ext = string.Empty;
                //cus.department = txtDepartment.Text;
                cus.province = string.Empty;
                cus.code = string.Empty;
                //cus.tel_number = txtTelNumber.Text;
                cus.create_by = userLogin.id;
                cus.create_date = DateTime.Now;
                cus.update_by = userLogin.id;
                cus.update_date = DateTime.Now;
                cus.status = "A";
                cus.contractPersonList = List;
                cus.addressList = ListCustomerAddress;
                return cus;
            }
        }

        private void initialPage()
        {
            lbCommandName.Text = CommandName.ToString();
            this.List = new List<m_customer_contract_person>();
            this.ListCustomerAddress = new List<m_customer_address>();
            switch (CommandName)
            {
                case CommandNameEnum.Add:

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.Edit:
                    fillinScreen();


                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.View:
                    fillinScreen();

                    btnSave.Visible =  false;
                    btnCancel.Enabled = true;
                    //
                    btnAdd.Visible = false;
                    btnAddCustomerAddress.Visible = false;
                    txtCompanyName.ReadOnly = true;
                    break;
            }
        }

        private void fillinScreen()
        {

            m_customer cus = new m_customer().SelectByID(this.PKID);
            PKID = cus.ID;
            //cus.customer_code =string.Empty;
            txtCompanyName.Text = cus.company_name;
            //txtAddress.InnerText = cus.address;
            //cus.sub_district =string.Empty;
            //cus.mobile_number =string.Empty;
            //txtEmail.Text = cus.email_address;
            //cus.branch =string.Empty;
            //cus.district =string.Empty;
            //cus.ext =string.Empty;
            //txtDepartment.Text = cus.department;
            //cus.province=string.Empty;
            //cus.code =string.Empty;
            //txtTelNumber.Text = cus.tel_number;

            //cus.update_by = userLogin.ID;
            //cus.update_date = DateTime.Now;
            //cus.status = "A";


            List<m_customer_contract_person> contractPersonList = new m_customer_contract_person().FindAllByCompanyID(cus.ID);
            if (contractPersonList != null && contractPersonList.Count > 0)
            {
                foreach (m_customer_contract_person tmp in contractPersonList)
                {
                    tmp.RowState = CommandNameEnum.Edit;
                }
                this.List = contractPersonList;
                gvSample.DataSource = ListShow;
                gvSample.DataBind();

            }
            List<m_customer_address> addressList = new m_customer_address().FindAllByCompanyID(cus.ID);
            if (addressList != null && addressList.Count > 0)
            {
                foreach (m_customer_address tmp in addressList)
                {
                    tmp.RowState = CommandNameEnum.Edit;
                }
                this.ListCustomerAddress = addressList;
                gvCustomerAddress.DataSource = ListCustomerAddressShow;
                gvCustomerAddress.DataBind();
                
            }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "PKID");
            Session.Remove(GetType().Name + "CONTRACT_PKID");
            Session.Remove(GetType().Name + "m_customer_contract_person");
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCustomer prvPage = Page.PreviousPage as SearchCustomer;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_CUSTOMER;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    objCustomer.Insert();
                    break;
                case CommandNameEnum.Edit:
                    objCustomer.Update();
                    break;
            }
            //Commit
            GeneralManager.Commit();
            removeSession();
            MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCompanyName.Text = string.Empty;
            //txtDepartment.Text = string.Empty;
            //txtTelNumber.Text = string.Empty;
            //txtFax.Text = string.Empty;
            //txtEmail.Text = string.Empty;
            //txtAddress.InnerText = string.Empty;
            removeSession();
            Response.Redirect(PreviousPath);
        }

        protected void gvSample_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvSample.EditIndex = -1;
            gvSample.DataSource = ListShow;
            gvSample.DataBind();
            btnAdd.Enabled = true;
        }

        #region "Contact Person
        protected void gvSample_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvSample.EditIndex = e.NewEditIndex;
            gvSample.DataSource = ListShow;
            gvSample.DataBind();
        }

        protected void gvSample_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            this.CONTRACT_PKID = int.Parse(gvSample.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox _txtName = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtName");
            TextBox _txtPhone = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtPhone_number");
            TextBox _txtEmail = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtEmail");
            TextBox _txtDepartment = (TextBox)gvSample.Rows[e.RowIndex].FindControl("txtDepartment");



            m_customer_contract_person contract = List.Find(x => x.ID == this.CONTRACT_PKID);
            if (contract != null)
            {
                contract.company_id = objCustomer.ID;
                contract.name = _txtName.Text;
                contract.phone_number = _txtPhone.Text;
                contract.email = _txtEmail.Text;
                contract.department = _txtDepartment.Text;
            }



            gvSample.EditIndex = -1;
            gvSample.DataSource = ListShow;
            gvSample.DataBind();
            btnAdd.Enabled = true;
        }

        protected void gvSample_SelectedIndexChanging(object sender, System.Web.UI.WebControls.GridViewSelectEventArgs e)
        {

        }

        protected void gvSample_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                //LinkButton btnView = (LinkButton)e.Row.FindControl("btnView");

                if (btnDelete != null)
                {
                    btnDelete.Visible = !(this.CommandName == CommandNameEnum.View);
                    btnEdit.Visible = !(this.CommandName == CommandNameEnum.View);

                }
            }
        }

        protected void gvSample_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.CONTRACT_PKID = int.Parse(gvSample.DataKeys[e.RowIndex].Values[0].ToString());

            m_customer_contract_person js = List.Find(x => x.ID == this.CONTRACT_PKID);
            if (js != null)
            {
                js.RowState = CommandNameEnum.Delete;
                gvSample.DataSource = ListShow;
                gvSample.DataBind();
            }
            btnAdd.Enabled = true;
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            m_customer_contract_person tmp = new m_customer_contract_person();
            tmp.ID = CustomUtils.GetRandomNumberID();
            tmp.company_id = this.PKID;
            tmp.name = string.Empty;
            tmp.phone_number = string.Empty;
            tmp.email = string.Empty;
            tmp.status = "A";
            tmp.RowState = CommandNameEnum.Add;
            List.Add(tmp);




            gvSample.DataSource = ListShow;
            gvSample.PageIndex = 0;
            gvSample.DataBind();
        }
        #endregion



        #region "Customer Address"
        protected void gvCustomerAddress_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCustomerAddress.EditIndex = e.NewEditIndex;
            gvCustomerAddress.DataSource = ListCustomerAddressShow;
            gvCustomerAddress.DataBind();
        }
        protected void gvCustomerAddress_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCustomerAddress.EditIndex = -1;
            gvCustomerAddress.DataSource = ListCustomerAddressShow;
            gvCustomerAddress.DataBind();
            btnAddCustomerAddress.Enabled = true;
        }
        protected void gvCustomerAddress_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.ADDR_PKID = int.Parse(gvCustomerAddress.DataKeys[e.RowIndex].Values[0].ToString());

            m_customer_address js = ListCustomerAddress.Find(x => x.ID == this.ADDR_PKID);
            if (js != null)
            {
                js.RowState = CommandNameEnum.Delete;
                gvCustomerAddress.DataSource = ListCustomerAddressShow;
                gvCustomerAddress.DataBind();
            }
            btnAddCustomerAddress.Enabled = true;
        }
        protected void gvCustomerAddress_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.ADDR_PKID = int.Parse(gvCustomerAddress.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox _txtCode = (TextBox)gvCustomerAddress.Rows[e.RowIndex].FindControl("txtCode");
            TextBox _txtAddress = (TextBox)gvCustomerAddress.Rows[e.RowIndex].FindControl("txtAddress");
            TextBox _txtTelephone = (TextBox)gvCustomerAddress.Rows[e.RowIndex].FindControl("txtTelephone");
            TextBox _txtFax = (TextBox)gvCustomerAddress.Rows[e.RowIndex].FindControl("txtFax");

            m_customer_address address = ListCustomerAddress.Find(x => x.ID == this.ADDR_PKID);
            if (address != null)
            {
                address.company_id = objCustomer.ID;
                address.code = _txtCode.Text;
                address.address = _txtAddress.Text;
                address.telephone = _txtTelephone.Text;
                address.fax = _txtFax.Text;
            }



            gvCustomerAddress.EditIndex = -1;
            gvCustomerAddress.DataSource = ListCustomerAddressShow;
            gvCustomerAddress.DataBind();
            btnAdd.Enabled = true;
        }

        protected void gvCustomerAddress_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnAddCustomerAddress_Click(object sender, EventArgs e)
        {

            m_customer_address tmp = new m_customer_address();
            tmp.ID = CustomUtils.GetRandomNumberID();
            tmp.company_id = this.PKID;
            tmp.code = string.Empty;
            tmp.address = string.Empty;
            tmp.fax = string.Empty;
            tmp.telephone = string.Empty;
            tmp.RowState = CommandNameEnum.Add;
            ListCustomerAddress.Add(tmp);

            gvCustomerAddress.DataSource = ListCustomerAddressShow;
            gvCustomerAddress.PageIndex = 0;
            gvCustomerAddress.DataBind();
        }
        #endregion


    }
}