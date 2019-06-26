using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.request
{
    public partial class SumIncome : System.Web.UI.Page
    {

        #region "Property"

        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }

        public DataTable searchResult
        {
            get { return (DataTable)Session[GetType().Name + "SearchJobRequest"]; }
            set { Session[GetType().Name + "SearchJobRequest"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }


        #endregion

        #region "Method"

        private void initialPage()
        {

            ddlCompany.Items.Clear();
            ddlCompany.DataSource = new m_customer().SelectAll().OrderBy(x => x.company_name);
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));


            List<PhysicalYear> yesrList = new List<PhysicalYear>();
            for (int i = DateTime.Now.Year - 3; i < DateTime.Now.Year + 3; i++)
            {
                PhysicalYear _year = new PhysicalYear();
                _year.year = i;
                yesrList.Add(_year);
            }

            ddlPhysicalYear.Items.Clear();
            ddlPhysicalYear.DataSource = yesrList;
            ddlPhysicalYear.DataBind();

            if (DateTime.Now.Month < Constants.PHYSICAL_YEAR)
            {
                ddlPhysicalYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
            }
            else
            {
                ddlPhysicalYear.SelectedValue = (DateTime.Now.Year).ToString();
            }

            //
            //สถานะการใช้(A = Available, I = ISSUED SOME, N = ISSUED ALL, C = Cancel)
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("NAME", typeof(string));

            // Here we add five DataRows.
            dt.Rows.Add("B", "BOI");
            dt.Rows.Add("NB", "NON-BOI");

            ddlBoiNonBoi.DataSource = dt;
            ddlBoiNonBoi.DataTextField = "NAME";
            ddlBoiNonBoi.DataValueField = "ID";
            ddlBoiNonBoi.DataBind();

            dt.Clear();
            dt.Dispose();

            ListItem item = new ListItem();
            item.Text = "";
            item.Value = "";
            ddlBoiNonBoi.Items.Insert(0, item);
            //
            bindingData();





        }

        private void bindingData()
        {

            DataTable dt = new DataTable("DT");

            dt.Columns.Add("InvoiceDate", typeof(DateTime));
            dt.Columns.Add("Terms", typeof(string));
            dt.Columns.Add("Number", typeof(string));
            dt.Columns.Add("PurchaseOrder", typeof(string));
            dt.Columns.Add("Customer", typeof(string));
            dt.Columns.Add("TotalSubAmount", typeof(string));
            dt.Columns.Add("Vat7", typeof(string));
            dt.Columns.Add("GrandTotalAmountVat", typeof(string));
            dt.Columns.Add("WithholdingTax3", typeof(string));
            dt.Columns.Add("TotaAmount", typeof(string));
            dt.Columns.Add("DeptBOI", typeof(string));
            dt.Columns.Add("CustomerTypeHDD", typeof(string));



            String conSQL = Configurations.MySQLCon;
            using (MySqlConnection conn = new MySqlConnection("server = " + conSQL.Split(';')[2].Split('=')[2] + "; " + conSQL.Split(';')[3] + "; " + conSQL.Split(';')[4] + "; " + conSQL.Split(';')[5]))
            {
                conn.Open();
                StringBuilder sqlCri = new StringBuilder();
                String sql = "SELECT * FROM (" +
                "                SELECT                                                                            " +
                "                    InvoiceDate,                                                                  " +
                "                    Terms,                                                                        " +
                "                    Number,                                                                       " +
                "                    PurchaseOrder,                                                                " +
                "                    Customer,                                                                     " +
                "                    TotalSubAmount,                                                               " +
                "                    (TotalSubAmount * 0.07) as Vat7,                                              " +
                "    (TotalSubAmount + (TotalSubAmount * 0.07)) as GrandTotalAmountVat,                            " +
                "	(TotalSubAmount * 0.03) as WithholdingTax3,                                                    " +
                "    ((TotalSubAmount + (TotalSubAmount * 0.07)) - (TotalSubAmount * 0.03)) as TotaAmount,         " +
                "    DeptBOI,                                                                                      " +
                "    CustomerTypeHDD                                                                         " +
                "FROM                                                                                              " +
                "    (SELECT                                                                                       " +
                "        s.sample_invoice AS Number,                                                               " +
                "            s.sample_invoice_date AS InvoiceDate,                                                 " +
                "            '' AS Terms,                                                                          " +
                "            s.sample_po AS PurchaseOrder,                                                         " +
                "            c.company_name AS Customer,                                                           " +
                "            ifnull(s.sample_invoice_amount, 0) AS TotalSubAmount,                                 " +
                "            (CASE                                                                                 " +
                "                WHEN RIGHT(s.job_number, 1) = 'B' THEN 'BOI'                                      " +
                "                ELSE 'NBOI'                                                                       " +
                "            END) AS DeptBOI," +
                "(case when SUBSTRING_INDEX(s.job_number, '-', 1) = 'ELN' then 'non-HDD' when SUBSTRING_INDEX(s.job_number, '-', 1) = 'ELP' then 'HDD' when SUBSTRING_INDEX(s.job_number, '-', 1) = 'GRP' then 'HDD' when SUBSTRING_INDEX(s.job_number, '-', 1) = 'ELWA' then 'HDD' else '' end) as CustomerTypeHDD" +
                "    FROM                                                                                          " +
                "        job_sample s                                                                              " +
                "    LEFT JOIN job_info i ON s.job_id = i.id                                                       " +
                "    LEFT JOIN m_customer c ON i.customer_id = c.id                                                " +
                "    LEFT JOIN m_type_of_test tot ON tot.ID = s.type_of_test_id                                                " +

                "    WHERE                                                                                         " +
                "        s.sample_invoice IS NOT NULL                                                                   " +
                "            ) x                                                                    " +
                "GROUP BY x.InvoiceDate , x.Terms , x.PurchaseOrder , x.Customer , x.TotalSubAmount , x.DeptBOI order by SUBSTRING(x.Number,3) asc) X  where 1 = 1";

                sqlCri.Append(" AND YEAR(X.InvoiceDate) = '" + ddlPhysicalYear.SelectedValue + "'");


                if (!String.IsNullOrEmpty(ddlBoiNonBoi.SelectedValue.ToString()))
                {
                    if (ddlBoiNonBoi.SelectedValue.ToString().Equals("NB"))
                    {
                        sqlCri.Append(" AND X.DeptBOI  <> 'BOI'");
                    }
                    else
                    {
                        sqlCri.Append(" AND X.DeptBOI  = 'BOI'");
                    }
                }
                if (!String.IsNullOrEmpty(txtSamplePo.Text))
                {
                    sqlCri.Append(" AND X.PurchaseOrder like '%" + txtSamplePo.Text + "%'");
                }
                if (!String.IsNullOrEmpty(txtInvoice.Text))
                {
                    sqlCri.Append(" AND X.Number like '%" + txtInvoice.Text + "%'");
                }


                DateTime receive_report_from = String.IsNullOrEmpty(txtInvoiceDateFrom.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtInvoiceDateFrom.Text);
                DateTime receive_report_to = String.IsNullOrEmpty(txtInvoiceDateTo.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtInvoiceDateTo.Text);
                if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
                {
                    sqlCri.Append(" AND X.InvoiceDate between'" + receive_report_from.ToString("yyyy-MM-dd") + "' AND '" + receive_report_to.ToString("yyyy-MM-dd") + "'");
                }


                sql += (sqlCri.ToString());



                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                this.searchResult = dt;

                gvJob.DataSource = this.searchResult;
                gvJob.DataBind();
                Console.WriteLine();
            }


        }

        private void removeSession()
        {
            txtInvoice.Text = String.Empty;
            txtSamplePo.Text = String.Empty;
            txtInvoiceDateFrom.Text = String.Empty;
            txtInvoiceDateTo.Text = String.Empty;

            ddlBoiNonBoi.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;

            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "SearchJobRequest");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (userLogin == null) Response.Redirect(Constants.LINK_LOGIN);

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindingData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            removeSession();

            bindingData();
        }

        protected void gvJob_RowCommand(object sender, GridViewCommandEventArgs e)
        {



        }

        protected void gvJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = this.searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }

        protected void gvJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //litDueDate.Text = due_date_lab.ToString("dd MMM yyyy");
            }

        }


        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }


        protected void ExportToExcel()
        {
            String tmpFile = Server.MapPath("~/Report/") + "SumInv_" + DateTime.Now.ToString("yyyyMMdd")+".xlsx";
            String pathTemplate = Server.MapPath("~/template/") + "SumInvoiceTemplate.xlsx";
            byte[] TemplateFile = System.IO.File.ReadAllBytes(pathTemplate);
            using (MemoryStream stream = new MemoryStream(TemplateFile))
            {
                using (ExcelPackage xPackage = new ExcelPackage(stream))
                {
                    int startIndex = 5;
                    int runningRow = 5;
                    var ws = xPackage.Workbook.Worksheets["Template"];
                    ws.Cells["A1"].Value = "Summary Invoice on "+ Convert.ToDateTime(this.searchResult.Rows[0][0]).ToString("MMMM yyyy");
                    foreach (DataRow row in this.searchResult.Rows)
                    {
                        ws.Cells["A" + runningRow].Value = Convert.ToDateTime(row.ItemArray.GetValue(0));
                        ws.Cells["B" + runningRow].Value = row.ItemArray.GetValue(1).ToString();
                        ws.Cells["C" + runningRow].Value = row.ItemArray.GetValue(2).ToString();
                        ws.Cells["D" + runningRow].Value = row.ItemArray.GetValue(3).ToString()+" ";
                        ws.Cells["E" + runningRow].Value = row.ItemArray.GetValue(4).ToString();
                        ws.Cells["F" + runningRow].Value = Convert.ToDouble(row.ItemArray.GetValue(5));
                        ws.Cells["G" + runningRow].Formula = "=F" + runningRow + "*0.07";
                        ws.Cells["H" + runningRow].Formula = "=F" + runningRow + "+G" + runningRow + "";

                        ws.Cells["I" + runningRow].Formula = "=F" + runningRow + "*0.03";//WithholdingTax(3 %)
                        ws.Cells["J" + runningRow].Formula = "=H" + runningRow + "-I" + runningRow + "";
                        ws.Cells["K" + runningRow].Value = row.ItemArray.GetValue(10).ToString(); //BOI/NBOI
                        ws.Cells["L" + runningRow].Value = row.ItemArray.GetValue(11).ToString(); 

                        if(!ws.Cells["K" + runningRow].Value.Equals("NBOI"))
                        {
                            ws.Cells["I" + runningRow].Value = "0";
                        }

                        runningRow++;
                    }
                    ws.Cells["F" + runningRow].Formula = "=SUM(F" + startIndex + ":F" + (runningRow-1) + ")";
                    ws.Cells["G" + runningRow].Formula = "=SUM(G" + startIndex + ":G" + (runningRow-1) + ")";
                    ws.Cells["H" + runningRow].Formula = "=SUM(H" + startIndex + ":H" + (runningRow-1) + ")";
                    ws.Cells["I" + runningRow].Formula = "=SUM(I" + startIndex + ":I" + (runningRow-1) + ")";
                    ws.Cells["J" + runningRow].Formula = "=SUM(J" + startIndex + ":J" + (runningRow-1) + ")";


                    if (File.Exists(tmpFile))
                    {
                        File.Delete(tmpFile);
                    }
                    byte[] Desctination = xPackage.GetAsByteArray();
                    System.IO.File.WriteAllBytes(tmpFile, Desctination);


                }

                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" +Path.GetFileName(tmpFile));
                Response.WriteFile(tmpFile);
                Response.Flush();

                if (File.Exists(tmpFile))
                {
                    File.Delete(tmpFile);
                }

            }

        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


    }
}