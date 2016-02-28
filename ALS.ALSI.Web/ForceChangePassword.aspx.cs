using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using System;

namespace ALS.ALSI.Web
{
    public partial class ForceChangePassword : System.Web.UI.Page
    {
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            users_login user = new users_login().SelectByID(userLogin.id);
            txtUsername.Text = user.username;
                //txtPassword.Text = user.password;
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            userLogin.password = CustomUtils.EncodeMD5(txtPassword.Text);
            userLogin.is_force_change_password = false;
            userLogin.Update();
            //Commit
            GeneralManager.Commit();
            Response.Redirect("Login.aspx");
        }
    }
}