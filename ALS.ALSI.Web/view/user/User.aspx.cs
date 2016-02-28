using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.user
{
    public partial class User : System.Web.UI.Page
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

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "PKID");
        }

        public users_login obj
        {
            get
            {
                users_login user = new users_login();
                user.id = PKID;
                user.role_id = String.IsNullOrEmpty(ddlRole.SelectedValue) ? 0 : int.Parse(ddlRole.SelectedValue);
                user.username = txtUser.Text;
                user.password = CustomUtils.EncodeMD5(txtPassword.Text);
                user.latest_login = DateTime.Now;
                user.email = txtEmail.Text;
                user.create_by = userLogin.id;
                user.create_date = DateTime.Now;
                user.status = rdStatusA.Checked ? "A" : "N";

                StringBuilder sb = new StringBuilder();

                foreach (ListItem item in lstTypeOfTest.Items)
                {
                    if (item.Selected)
                    {
                        sb.Append(item.Value);
                        sb.Append(Constants.CHAR_COMMA);
                    }
                }
                if (sb.ToString().Length > 0)
                {
                    user.responsible_test = sb.ToString().Substring(0, sb.ToString().Length - 1);
                }
                user.personal_title = String.IsNullOrEmpty(ddlTitle.SelectedValue) ? 0 : int.Parse(ddlTitle.SelectedValue);
                user.first_name = txtFirstName.Text;
                user.last_name = txtLastName.Text;
                //user.is_force_change_password = true;
                return user;
            }
        }

        private void initialPage()
        {

            lbCommandName.Text = CommandName.ToString();
            m_role role = new m_role();

            ddlRole.Items.Clear();
            ddlRole.DataSource = role.SelectAll();
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            m_title title = new m_title();

            ddlTitle.Items.Clear();
            ddlTitle.DataSource = title.SelectAll();
            ddlTitle.DataBind();
            ddlTitle.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));



                lstTypeOfTest.Items.Clear();
                lstTypeOfTest.DataSource = new m_type_of_test().SelectDistinct();
                lstTypeOfTest.DataBind();
                lstTypeOfTest.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));
            
            switch (CommandName)
            {
                case CommandNameEnum.Add:

                    ddlRole.Enabled = true;
                    txtUser.Enabled = true;
                    txtPassword.Enabled = true;
                    txtEmail.Enabled = true;
                    ddlTitle.Enabled = true;
                    txtFirstName.Enabled = true;
                    txtLastName.Enabled = true;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.Edit:
                    fillinScreen();
                    ddlRole.Enabled = true;
                    txtUser.Enabled = false;
                    txtPassword.Enabled = true;
                    txtEmail.Enabled = true;
                    ddlTitle.Enabled = true;
                    txtFirstName.Enabled = true;
                    txtLastName.Enabled = true;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.View:
                    fillinScreen();
                    ddlRole.Enabled = false;
                    txtUser.Enabled = false;
                    txtPassword.Enabled = false;
                    txtEmail.Enabled = false;
                    ddlTitle.Enabled = false;
                    txtFirstName.Enabled = false;
                    txtLastName.Enabled = false;

                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;

                    break;
            }
        }

        private void fillinScreen()
        {

                users_login user = new users_login().SelectByID(this.PKID);
                ddlRole.SelectedValue = user.role_id.ToString();
                txtUser.Text = user.username;
                txtPassword.Text = user.password;
                txtEmail.Text = user.email;

                if (!String.IsNullOrEmpty(user.responsible_test) && user.responsible_test.Length > 0)
                {
                    String[] repo = user.responsible_test.Split(Constants.CHAR_COMMA);



                    lstTypeOfTest.SelectionMode = ListSelectionMode.Multiple;
                    foreach (ListItem item in lstTypeOfTest.Items)
                    {
                        foreach (String r in repo)
                        {
                            if (item.Value == r)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }

                ddlTitle.SelectedValue = user.personal_title.ToString();
                txtFirstName.Text = user.first_name;
                txtLastName.Text = user.last_name;

            
        }


        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            SearchUser prvPage = Page.PreviousPage as SearchUser;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_USER;

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
                    obj.Insert();
                    break;
                case CommandNameEnum.Edit:
                    obj.Update();
                    break;
            }
            //Commit
            GeneralManager.Commit();

            removeSession();
            Response.Redirect(PreviousPath);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(PreviousPath);
        }
    }
}