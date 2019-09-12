using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;

namespace ALS.ALSI.Web.view.dashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {

        //http://jsfiddle.net/3y84ne8z/1/ --- sample filter


        public DataTable searchResult
        {
            get { return (DataTable)Session[GetType().Name + "Dashboard"]; }
            set { Session[GetType().Name + "Dashboard"] = value; }
        }

        // To calculate Running Total
        private double dblFooterAmount = 0;

        protected string jsonSeriesRpt01;
        protected string jsonSeriesRpt02;
        protected string jsonSeriesRpt02Categories;

        protected string jsonSeriesRpt03;
        protected string jsonSeriesRpt031;

        protected string jsonSeriesRpt04;

        //private static Excel.Workbook MyBook = null;
        //private static Excel.Application MyApp = null;
        //private static Excel.Worksheet MySheet = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    #region "Initial "
                    int year = DateTime.Now.Year;
                    if (DateTime.Now.Month < Constants.PHYSICAL_YEAR)
                    {
                        year = (DateTime.Now.Year - 1);
                    }
                    else
                    {
                        year = (DateTime.Now.Year);
                    }

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
                    #endregion

                    #region "Binding Report"
                    txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                    txtEndDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("dd/MM/yyyy");

                    rptRevenueActual();
                    rptDailyInvoice();
                    jsonSeriesRpt031 = "[]";//Report31(dtStartDate, dtEndDate);
                    jsonSeriesRpt04 = "[]";//Report4(dtStartDate, dtEndDate);
                    //
                    //Report3(dtStartDate, dtEndDate);
                    #endregion

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            Console.WriteLine();
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            rptRevenueActual();
            rptDailyInvoice();
            jsonSeriesRpt031 = "[]";//Report31(dtStartDate, dtEndDate);
            jsonSeriesRpt04 = "[]";//Report4(dtStartDate, dtEndDate);

            //DateTime dtStartDate = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            //DateTime dtEndDate = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);

            //jsonSeriesRpt01 = rptRevenueActual();
            //jsonSeriesRpt02 = Report2(dtStartDate, dtEndDate);
            //jsonSeriesRpt031 = Report31(dtStartDate, dtEndDate);
            //jsonSeriesRpt04 = Report4(dtStartDate, dtEndDate);
            //
            //Report3(dtStartDate, dtEndDate);
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DateTime dtStartDate = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            //DateTime dtEndDate = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);

            //jsonSeriesRpt01 = Report1(dtStartDate, dtEndDate);
            //jsonSeriesRpt02 = Report2(dtStartDate, dtEndDate);
            //jsonSeriesRpt031 = Report31(dtStartDate, dtEndDate);
            //jsonSeriesRpt04 = Report4(dtStartDate, dtEndDate);
            ////
            //Report3(dtStartDate, dtEndDate);

        }

        public void rptRevenueActual()
        {
            StringBuilder sqlCri = new StringBuilder();

            string sql = "select" +
            " year(x.InvoiceDate) as xYear," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 1 THEN TotalSubAmount END, 0)) AS 'Jan'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 2 THEN TotalSubAmount END, 0)) AS 'Feb'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 3 THEN TotalSubAmount END, 0)) AS 'Mar'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 4 THEN TotalSubAmount END, 0)) AS 'Apr'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 5 THEN TotalSubAmount END, 0)) AS 'May'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 6 THEN TotalSubAmount END, 0)) AS 'Jun'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 7 THEN TotalSubAmount END, 0)) AS 'Jul'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 8 THEN TotalSubAmount END, 0)) AS 'Aug'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 9 THEN TotalSubAmount END, 0)) AS 'Sep'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 10 THEN TotalSubAmount END, 0)) AS 'Oct'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 11 THEN TotalSubAmount END, 0)) AS 'Nov'," +
            " SUM(IFNULL(CASE WHEN MONTH(InvoiceDate) = 12 THEN TotalSubAmount END, 0)) AS 'Dec'" +
            " from(" +
            "     select * from (select" +
            "         s.sample_invoice_date as InvoiceDate," +
            "         s.sample_invoice as sInvoice," +
            "         s.sample_po as PurchaseOrder," +
            "         s.sample_so as sampleSo," +
            "         ifnull(s.sample_invoice_amount_rpt, 0) as TotalSubAmount," +
            "         (case when right(s.job_number, 1) = 'B' then 'BOI' else 'NBOI' end) as DeptBOI," +
            "         (case" +
            "             when substring_index(s.job_number, '-', 1) = 'ELN' then 'non-HDD'" +
            "             when substring_index(s.job_number, '-', 1) = 'ELP' then 'HDD'" +
            "             when substring_index(s.job_number, '-', 1) = 'GRP' then 'HDD'" +
            "             when substring_index(s.job_number, '-', 1) = 'ELWA' then 'HDD' else ''" +
            "         end) as CustomerTypeHDD" +
            "     from job_sample s" +
            "     left join job_info i on s.job_id = i.id" +
            " left join job_sample_group_invoice sgi on s.sample_invoice = sgi.inv_no" +
            " left join m_type_of_test tot on tot.ID = s.type_of_test_id" +
            " left join m_customer c on s.id = i.customer_id" +
            " where s.sample_invoice <> '' and s.sample_invoice_status = 2 and s.sample_invoice_date is not null) tmp where 1=1 {0}" +
            " ) x" +
            " GROUP BY year(x.InvoiceDate)";



            //sqlCri.Append(" and YEAR(tmp.InvoiceDate) = '" + ddlPhysicalYear.SelectedValue + "'");
            if (!String.IsNullOrEmpty(ddlBoiNonBoi.SelectedValue.ToString()))
            {
                if (ddlBoiNonBoi.SelectedValue.ToString().Equals("NB"))
                {
                    sqlCri.Append(" and tmp.DeptBOI  <> 'BOI'");
                }
                else
                {
                    sqlCri.Append(" and tmp.DeptBOI  = 'BOI'");
                }
            }

            DateTime receive_report_from = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            DateTime receive_report_to = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);
            if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
            {
                sqlCri.Append(" and tmp.InvoiceDate between'" + receive_report_from.ToString("yyyy-MM-dd") + "' and '" + receive_report_to.ToString("yyyy-MM-dd") + "'");
            }




            DataTable dt = MaintenanceBiz.ExecuteReturnDt(string.Format(sql, sqlCri.ToString()));
            StringBuilder sbResultJson = new StringBuilder();
            String data = "[";

            foreach (DataRow dr in dt.Rows)
            {
                string xYear = dr["xYear"].ToString();
                string Jan = dr["Jan"].ToString();
                string Feb = dr["Feb"].ToString();
                string Mar = dr["Mar"].ToString();
                string Apr = dr["Apr"].ToString();
                string May = dr["May"].ToString();
                string Jun = dr["Jun"].ToString();
                string Jul = dr["Jul"].ToString();
                string Aug = dr["Aug"].ToString();
                string Sep = dr["Sep"].ToString();
                string Oct = dr["Oct"].ToString();
                string Nov = dr["Nov"].ToString();
                string Dec = dr["Dec"].ToString();
                data += "{name : '" + xYear + "', data: [" + Jan + "," + Feb + "," + Mar + "," + Apr + "," + May + "," + Jun + "," + Jul + "," + Aug + "," + Sep + "," + Oct + "," + Nov + "," + Dec + "]},";
            }
            data = data.Remove(data.Length - 1, 1);
            data += "]";
            jsonSeriesRpt01 = data;
        }

        public void rptDailyInvoice()
        {
            StringBuilder sqlCriIncomplete = new StringBuilder();

            Hashtable htInv = new Hashtable();

            #region "Incomplete"
            string sql = "select" +
            " 'WIP' as xType,InvoiceDate,count(sInvoice) iCount" +
            " from(" +
            "     select * from (select" +
            "         s.sample_invoice_date as InvoiceDate," +
            "         s.sample_invoice as sInvoice," +
            "         s.sample_po as PurchaseOrder," +
            "         s.sample_so as sampleSo," +
            "         ifnull(s.sample_invoice_amount_rpt, 0) as TotalSubAmount," +
            "         (case when right(s.job_number, 1) = 'B' then 'BOI' else 'NBOI' end) as DeptBOI," +
            "         (case" +
            "             when substring_index(s.job_number, '-', 1) = 'ELN' then 'non-HDD'" +
            "             when substring_index(s.job_number, '-', 1) = 'ELP' then 'HDD'" +
            "             when substring_index(s.job_number, '-', 1) = 'GRP' then 'HDD'" +
            "             when substring_index(s.job_number, '-', 1) = 'ELWA' then 'HDD' else ''" +
            "         end) as CustomerTypeHDD" +
            "     from job_sample s" +
            " left join job_info i on s.job_id = i.id" +
            " left join job_sample_group_invoice sgi on s.sample_invoice = sgi.inv_no" +
            " left join m_type_of_test tot on tot.ID = s.type_of_test_id" +
            " left join m_customer c on s.id = i.customer_id" +
            " where s.sample_invoice <> '' and s.sample_invoice_status in ( 1,2) and s.sample_invoice_date is not null) tmp where 1=1 {0}" +
            " ) x" +
            " GROUP BY  x.InvoiceDate";

            string sql2 = "select" +
            " 'TIV' as xType,InvoiceDate,count(sInvoice) iCount" +
            " from(" +
            "     select * from (select" +
            "         s.sample_invoice_date as InvoiceDate," +
            "         s.sample_invoice as sInvoice," +
            "         s.sample_po as PurchaseOrder," +
            "         s.sample_so as sampleSo," +
            "         ifnull(s.sample_invoice_amount_rpt, 0) as TotalSubAmount," +
            "         (case when right(s.job_number, 1) = 'B' then 'BOI' else 'NBOI' end) as DeptBOI," +
            "         (case" +
            "             when substring_index(s.job_number, '-', 1) = 'ELN' then 'non-HDD'" +
            "             when substring_index(s.job_number, '-', 1) = 'ELP' then 'HDD'" +
            "             when substring_index(s.job_number, '-', 1) = 'GRP' then 'HDD'" +
            "             when substring_index(s.job_number, '-', 1) = 'ELWA' then 'HDD' else ''" +
            "         end) as CustomerTypeHDD" +
            "     from job_sample s" +
            " left join job_info i on s.job_id = i.id" +
            " left join job_sample_group_invoice sgi on s.sample_invoice = sgi.inv_no" +
            " left join m_type_of_test tot on tot.ID = s.type_of_test_id" +
            " left join m_customer c on s.id = i.customer_id" +
            " where s.sample_so <> '') tmp where 1=1 {0}" +
            " ) x" +
            " GROUP BY  x.InvoiceDate";

            sql = string.Format("{0} union {1}", sql, sql2);

            if (!String.IsNullOrEmpty(ddlBoiNonBoi.SelectedValue.ToString()))
            {
                if (ddlBoiNonBoi.SelectedValue.ToString().Equals("NB"))
                {
                    sqlCriIncomplete.Append(" and tmp.DeptBOI  <> 'BOI'");
                }
                else
                {
                    sqlCriIncomplete.Append(" and tmp.DeptBOI  = 'BOI'");
                }
            }

            DateTime receive_report_from = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            DateTime receive_report_to = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);
            if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
            {
                sqlCriIncomplete.Append(" and tmp.InvoiceDate between'" + receive_report_from.ToString("yyyy-MM-dd") + "' and '" + receive_report_to.ToString("yyyy-MM-dd") + "'");
            }

            DataTable dtIncomplete = MaintenanceBiz.ExecuteReturnDt(string.Format(sql, sqlCriIncomplete.ToString()));

            String dataCat = "";
            String dataTiv = "";
            String dataWip = "";

            foreach (DataRow dr in dtIncomplete.Rows)
            {
                try
                {
                    string invDate = dr["InvoiceDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["InvoiceDate"]).ToString("yyyyMMdd");
                    string xType = dr["xType"].ToString();
                    int iCount = Convert.ToInt32(dr["iCount"].ToString());

                    if (htInv.ContainsKey(invDate))
                    {
                        CWipTiv objEdit = (CWipTiv)htInv[invDate];
                        switch (xType)
                        {
                            case "TIV":
                                objEdit.tiv = iCount;
                                break;
                            case "WIP":
                                objEdit.wip = iCount;
                                break;
                        }
                    }
                    else
                    {
                        CWipTiv objNew = new CWipTiv();
                        switch (xType)
                        {
                            case "TIV":
                                objNew.tiv = iCount;
                                break;
                            case "WIP":
                                objNew.wip = iCount;
                                break;
                        }
                        htInv.Add(invDate, objNew);

                    }

                    //dataCat += string.Format("'{0}',", invDate);
                    //data += string.Format("{0},", dr["iCount"].ToString());
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            foreach (DictionaryEntry de in htInv)
            {
                dataCat += string.Format("'{0}',", de.Key);

                CWipTiv cWipTiv = (CWipTiv)de.Value;

                dataWip += string.Format("{0},", cWipTiv.wip);
                dataTiv += string.Format("{0},", cWipTiv.tiv);

                //data += string.Format("{0},", de.Value);

                //Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
            }

            dataCat = dataCat.Length == 0 ? "" : dataCat.Remove(dataCat.Length - 1, 1);
            dataWip = dataWip.Length == 0 ? "" : dataWip.Remove(dataWip.Length - 1, 1);
            dataTiv = dataTiv.Length == 0 ? "" : dataTiv.Remove(dataTiv.Length - 1, 1);

            #endregion

            jsonSeriesRpt02Categories = "[" + dataCat + "]";
            jsonSeriesRpt02 = "[{name : 'Invoice', data: [" + dataWip + "]},{name : 'Total Invoice', data: [" + dataTiv + "]}]";
        }

        public void Report3(DateTime s, DateTime e)
        {
            String sql = "";
            sql += " select                                                              ";
            sql += " j.customer_id,                                                      ";
            sql += " c.company_name,                                                     ";
            sql += " s.sample_invoice,                                                   ";
            sql += " s.sample_invoice_date,                                              ";
            sql += " TO_DAYS(Now()) - TO_DAYS(s.sample_invoice_date) as overdue_date,    ";
            sql += " sum(s.sample_invoice_amount_rpt) sample_invoice_amount_rpt                  ";
            sql += " from job_sample s                                                   ";
            sql += " left join job_info j on j.ID = s.job_id                             ";
            sql += " left join m_customer c on c.ID = j.customer_id                      ";
            sql += " where s.sample_invoice is not null                                  ";
            sql += " and s.sample_invoice <> ''                                          ";
            sql += " and s.sample_invoice_complete_date is null                          ";
            sql += " and s.sample_invoice_date is not null                               ";
            sql += " and s.sample_invoice_date between'" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "'";
            sql += " group by s.sample_invoice_date,j.customer_id,s.sample_invoice;      ";

            searchResult = MaintenanceBiz.ExecuteReturnDt(sql);

            gvRpt3.DataSource = searchResult;
            gvRpt3.DataBind();
        }

        public String Report31(DateTime s, DateTime e)
        {
            try
            {
                String sql = "";
                sql += " select * from  (select                                            ";
                sql += " c.company_name,                                                   ";
                sql += " sum(TO_DAYS(Now()) - TO_DAYS(s.sample_invoice_date)) as overDue,  ";
                sql += " sum(s.sample_invoice_amount_rpt) as sumAmout                          ";
                sql += " from job_sample s                                                 ";
                sql += " left                                                              ";
                sql += " join job_info j on j.ID = s.job_id                                ";
                sql += " left                                                              ";
                sql += " join m_customer c on c.ID = j.customer_id                         ";
                sql += " where s.sample_invoice is not null                                ";
                sql += " and s.sample_invoice <> ''                                          ";
                sql += " and s.sample_invoice_complete_date is null                          ";
                sql += " and s.sample_invoice_date is not null                               ";
                sql += " and s.sample_invoice_date between'" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "'";
                sql += " group by c.ID) tmp order by sumAmout desc limit " + ddlPeriod.SelectedValue;


                DataTable dt = MaintenanceBiz.ExecuteReturnDt(sql);
                int totalRow = dt.Rows.Count;




                StringBuilder sbResultJson = new StringBuilder();
                String data = "[";
                #region "Amout"
                if (dt.Rows.Count > 0)
                {
                    data += "{name: \"Customer\",colorByPoint: true,data: [";
                    foreach (DataRow dr in dt.Rows)
                    {
                        string company_name = dr["company_name"].ToString();
                        double sumAmout = Convert.ToDouble(dr["sumAmout"].ToString());

                        data += "{name: '" + company_name + "',y: " + sumAmout + "},";
                    }
                    data = data.Substring(0, data.Length - 1);
                    data += "]}";
                }
                #endregion
                data += "]";
                sbResultJson.Append(data);
                return sbResultJson.ToString();

            }
            catch (Exception)
            {
                return "[]";

            }
        }

        public String Report4(DateTime s, DateTime e)
        {
            try
            {


                String sql = "select * from (select sample_invoice_date,sum(sample_invoice_amount_rpt) amt FROM job_sample where sample_invoice_date is not null and sample_invoice_date between '" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "'  GROUP BY DATE(sample_invoice_date) order by sample_invoice_date desc limit 30) x order by sample_invoice_date asc";

                //String sql = "SELECT date as sample_invoice_date,value as amt FROM alsi.tmp_rpt_4 limit 30;";
                DataTable dt = MaintenanceBiz.ExecuteReturnDt(sql);
                StringBuilder sbResultJson = new StringBuilder();

                String data = "[";
                #region "Amout"
                if (dt.Rows.Count > 0)
                {
                    data += "{name: \"Amout\",data: [";
                    foreach (DataRow dr in dt.Rows)
                    {
                        DateTime epochDate = Convert.ToDateTime(dr["sample_invoice_date"].ToString());
                        double count = Convert.ToDouble(dr["amt"].ToString());
                        data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + count + "],";
                    }
                    data = data.Substring(0, data.Length - 1);
                    data += "]}";

                    #region "Forecast(Amout)"
                    //data += ",{name: \"Forecast(Amout)\",data: [";
                    //Hashtable listForecastAmtDate = new Hashtable();
                    //listForecastAmtDate[new DateTime(2019, 02, 12, 0, 0, 0)] = 17000;
                    //listForecastAmtDate[new DateTime(2019, 02, 17, 0, 0, 0)] = 18000;
                    //listForecastAmtDate[new DateTime(2019, 02, 19, 0, 0, 0)] = 19000;
                    //listForecastAmtDate[new DateTime(2019, 02, 25, 0, 0, 0)] = 22000;
                    //listForecastAmtDate[new DateTime(2019, 02, 27, 0, 0, 0)] = 24000;
                    //foreach (DictionaryEntry entry in listForecastAmtDate)
                    //{
                    //    DateTime epochDate = Convert.ToDateTime(entry.Key);
                    //    data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + entry.Value + "],";
                    //}
                    //data = data.Substring(0, data.Length - 1);
                    //data += "]}";
                    #endregion
                    #region "Lower Confidence Bound(Amout)"
                    //data += ",{name: \"Lower Confidence Bound(Amout)\",data: [";

                    //Hashtable listLowerConfidenceBoundAmout = new Hashtable();
                    //listLowerConfidenceBoundAmout[new DateTime(2019, 02, 12, 0, 0, 0)] = 170000;
                    //listLowerConfidenceBoundAmout[new DateTime(2019, 02, 17, 0, 0, 0)] = 180000;
                    //listLowerConfidenceBoundAmout[new DateTime(2019, 02, 19, 0, 0, 0)] = 190000;
                    //listLowerConfidenceBoundAmout[new DateTime(2019, 02, 25, 0, 0, 0)] = 220000;
                    //listLowerConfidenceBoundAmout[new DateTime(2019, 02, 27, 0, 0, 0)] = 240000;
                    //foreach (DictionaryEntry entry in listLowerConfidenceBoundAmout)
                    //{
                    //    DateTime epochDate = Convert.ToDateTime(entry.Key);
                    //    data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + entry.Value + "],";
                    //}
                    //data = data.Substring(0, data.Length - 1);
                    //data += "]}";
                    #endregion
                    #region "Upper Confidence Bound(Amout)"
                    //data += ",{name: \"Upper Confidence Bound(Amout)\",data: [";

                    //Hashtable listUpperConfidenceBoundAmout = new Hashtable();
                    //listUpperConfidenceBoundAmout[new DateTime(2019, 02, 12, 0, 0, 0)] = 19000;
                    //listUpperConfidenceBoundAmout[new DateTime(2019, 02, 17, 0, 0, 0)] = 22000;
                    //listUpperConfidenceBoundAmout[new DateTime(2019, 02, 19, 0, 0, 0)] = 30000;
                    //listUpperConfidenceBoundAmout[new DateTime(2019, 02, 25, 0, 0, 0)] = 35000;
                    //listUpperConfidenceBoundAmout[new DateTime(2019, 02, 27, 0, 0, 0)] = 44000;
                    //foreach (DictionaryEntry entry in listUpperConfidenceBoundAmout)
                    //{
                    //    DateTime epochDate = Convert.ToDateTime(entry.Key);
                    //    data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + entry.Value + "],";
                    //}
                    //data = data.Substring(0, data.Length - 1);
                    //data += "]}";
                    #endregion
                }
                #endregion
                data += "]";
                sbResultJson.Append(data);
                return sbResultJson.ToString();

            }
            catch (Exception)
            {
                return "[]";

            }


            //MyApp = new Excel.Application();
            //MyApp.Visible = false;
            //MyBook = MyApp.Workbooks.Open("D:\\Forecast_ets_example.xlsx");
            //MySheet = (Excel.Worksheet)MyBook.Sheets[1]; // Explicit cast is not required here
            //lastRow = MySheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;

            //using (FileStream fs = new FileStream("D:\\Forecast_ets_example.xlsx", FileMode.Open, FileAccess.Read))
            //{
            //    HSSFWorkbook wd = new HSSFWorkbook(fs);
            //    ISheet isComponent = wd.GetSheet("Sheet1");
            //    if (isComponent != null)
            //    {

            //    }
            //}
        }

        #region "Gridview"
        protected void gvJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();

        }

        protected void gvRpt3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the Row Amount
                Label lbBalance = ((Label)e.Row.FindControl("lbBalance"));

                // Each Row Amount values are added to the footer Amount variable
                dblFooterAmount = dblFooterAmount + Convert.ToDouble(lbBalance.Text);
                lbBalance.Text = Convert.ToDouble(lbBalance.Text).ToString("N0");
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }

        protected void gvRpt3_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                int intNoOfMergeCol = e.Row.Cells.Count - 1; /*except last column */

                GridViewRow footerRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Insert);

                //Adding Footer Total Text Column
                TableCell cell = new TableCell();
                cell.Text = "Total : ";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = intNoOfMergeCol;

                footerRow.Cells.Add(cell);

                //Adding Footer Total Amount Column
                cell = new TableCell();
                Label lbl = new Label();
                lbl.ID = "lblFooterAmount";
                lbl.Text = dblFooterAmount.ToString("N0");
                cell.Controls.Add(lbl);
                cell.HorizontalAlign = HorizontalAlign.Right;

                footerRow.Cells.Add(cell);

                gvRpt3.Controls[0].Controls.Add(footerRow);
                double dblGrandTotal = 0;
                foreach (DataRow dr in searchResult.Rows)
                {
                    dblGrandTotal += Convert.ToDouble(dr["sample_invoice_amount_rpt"]);
                }

                // First cell is used for specifying the Total text
                for (int intCellCol = 1; intCellCol < intNoOfMergeCol; intCellCol++)
                    e.Row.Cells.RemoveAt(1);
                e.Row.Cells[0].ColumnSpan = intNoOfMergeCol;
                e.Row.Cells[0].Text = "Grand Total : ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = dblGrandTotal.ToString("N0");
            }
        }
        #endregion

    }
}

public class CWipTiv
{
    public int wip { get; set; }
    public int tiv { get; set; }

}

