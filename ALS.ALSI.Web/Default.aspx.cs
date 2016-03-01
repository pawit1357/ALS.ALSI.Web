
using System;

namespace ALS.ALSI.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", true);
            Console.WriteLine();

        }
    }
}