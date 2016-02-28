using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using System;

namespace ALS.ALSI.Web
{
    public partial class ForgotPassword : System.Web.UI.Page
    {

        protected String Message
        {
            get { return (String)Session[GetType().Name + "Message"]; }
            set { Session[GetType().Name + "Message"] = value; }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name + "Message");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtEmail.Text))
            {
                users_login user = new users_login().SelectByEmail(txtEmail.Text);
                if (user != null)
                {
                    //force change password
                    user.password = CustomUtils.EncodeMD5("1234");
                    user.is_force_change_password = true;
                    user.Update();

                    removeSession();
                    txtEmail.Text = String.Empty;
                    Message = "<div class=\"alert alert-error\"><button class=\"close\" data-dismiss=\"alert\"></button><span>Account information has been sent to you email.</span></div>";
                }
                else
                {
                    Message = "<div class=\"alert alert-error\"><button class=\"close\" data-dismiss=\"alert\"></button><span>This email is not found.</span></div>";
                }

            }
            else
            {
                Message = "<div class=\"alert alert-error\"><button class=\"close\" data-dismiss=\"alert\"></button><span>Please enter email.</span></div>";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}