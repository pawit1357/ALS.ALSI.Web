using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ALS.ALSI.Web
{
    public partial class Login : System.Web.UI.Page
    {

        protected String Message
        {
            get { return (String)Session[GetType().Name + "Message"]; }
            set { Session[GetType().Name + "Message"] = value; }
        }

        public users_login objUser
        {
            get
            {
                users_login user = new users_login();
                user.username = txtUsername.Text;
                user.password = CustomUtils.EncodeMD5(txtPassword.Text);

                return user;
            }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name + "Message");
        }

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //try
            //{
            try
            {
                //The code that causes the error goes here.
                users_login user = objUser.Login();
                if (user != null)
                {
                    Session.Add(Constants.SESSION_USER, user);
                    if (!Convert.ToBoolean(user.is_force_change_password))
                    {
                        removeSession();
                        Response.Redirect(Constants.LINK_SEARCH_JOB_REQUEST);
                    }
                    else
                    {
                        Response.Redirect("ForceChangePassword.aspx");
                    }
                }
                else
                {
                    Message = "<div class=\"alert alert-error\"><button class=\"close\" data-dismiss=\"alert\"></button><span>Enter any username and passowrd.</span></div>";
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                //Display or log the error based on your application.
                Message = "<div class=\"alert alert-error\"><button class=\"close\" data-dismiss=\"alert\"></button><span>" + errorMessage + "</span></div>";
            }


            //}
            //catch (Exception ex)
            //{
            //    Message = "<div class=\"alert alert-error\"><button class=\"close\" data-dismiss=\"alert\"></button><span>"+ex.Message+"</span></div>";
            //}
        }

    }
}