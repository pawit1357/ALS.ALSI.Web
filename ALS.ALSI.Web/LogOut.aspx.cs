using ALS.ALSI.Biz.Constant;
using System;

namespace ALS.ALSI.Web
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Session.Remove(Constants.SESSION_USER);
            Response.Redirect("~/Login.aspx", true);
        }
    }
}