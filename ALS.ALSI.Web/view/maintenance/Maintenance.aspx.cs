using ALS.ALSI.Biz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.initial
{
    public partial class Maintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            Boolean bResult = MaintenanceBiz.ExecuteCommand(txtSql.Text);
            lbResult.Text = bResult ? "Success" : "Fail";
            Console.WriteLine();
        }

        protected void btnGetDs_Click(object sender, EventArgs e)
        {
            DataSet ds = MaintenanceBiz.ExecuteReturnDs(txtSql.Text);

            gvResult.DataSource = ds;
            gvResult.DataBind();

        }
    }
}