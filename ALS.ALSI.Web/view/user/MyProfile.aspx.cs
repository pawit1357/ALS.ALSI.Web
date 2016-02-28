using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.user
{
    public partial class MyProfile : System.Web.UI.Page
    {

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
        }

        public users_login obj
        {
            get
            {
                users_login user = new users_login();
                user.id = userLogin.id;
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
                user.is_force_change_password = false;
                return user;
            }
        }

        private void initialPage()
        {

            lbCommandName.Text = CommandNameEnum.Edit.ToString();

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
            
            m_type_of_test type_of_test = new m_type_of_test();
            
                lstTypeOfTest.Items.Clear();
                lstTypeOfTest.DataSource = type_of_test.SelectDistinct();
                lstTypeOfTest.DataBind();
                lstTypeOfTest.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));
            

            fillinScreen();

            ddlRole.Enabled = false;
            txtUser.Enabled = false;
            txtPassword.Enabled = true;
            txtEmail.Enabled = true;
            ddlTitle.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;

            btnSave.Enabled = true;
            btnCancel.Enabled = true;


        }

        private void fillinScreen()
        {
            ddlRole.SelectedValue = userLogin.role_id.ToString();
            txtUser.Text = userLogin.username;
            txtPassword.Text = userLogin.password;
            txtEmail.Text = userLogin.email;

            if (!String.IsNullOrEmpty(userLogin.responsible_test) && userLogin.responsible_test.Length > 0)
            {
                String[] repo = userLogin.responsible_test.Split(Constants.CHAR_COMMA);

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
            ddlTitle.SelectedValue = userLogin.personal_title.ToString();
            txtFirstName.Text = userLogin.first_name;
            txtLastName.Text = userLogin.last_name;


        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            obj.Update();
            removeSession();
            Response.Redirect(Constants.LINK_SEARCH_JOB_REQUEST);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            removeSession();
            Response.Redirect(Constants.LINK_SEARCH_JOB_REQUEST);
        }
    }
}