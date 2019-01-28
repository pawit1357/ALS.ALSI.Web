using ALS.ALSI.Biz;
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
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;

namespace ALS.ALSI.Web.view.dashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {

        public DataTable searchResult
        {
            get { return (DataTable)Session[GetType().Name + "Dashboard"]; }
            set { Session[GetType().Name + "Dashboard"] = value; }
        }


        protected string jsonSeriesRpt01;
        protected string jsonSeriesRpt02;
        protected string jsonSeriesRpt03;
        protected string jsonSeriesRpt04;

        private static Excel.Workbook MyBook = null;
        private static Excel.Application MyApp = null;
        private static Excel.Worksheet MySheet = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                jsonSeriesRpt01 = "[" + Report1_1() + "," + Report1_2() + "]";
                jsonSeriesRpt02 = "[" + Report2_1() + "," + Report2_2() + "]";
                Report3();
                jsonSeriesRpt04 = Report4();
            }
            Console.WriteLine();
        }

        public String Report1_1()
        {
            String sql = "" +
            "SELECT                                                                                                                     " +
            "  (case when s.sample_invoice_status is null or s.sample_invoice_status = 1 then 'revenue' else 'actual' end) invoice_type," +
            //"  YEAR(s.sample_invoice_date)                                             AS 'Year',                                       " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 1  THEN s.sample_invoice_amount END, 0)) AS 'Jan',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 2  THEN s.sample_invoice_amount END, 0)) AS 'Feb',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 3  THEN s.sample_invoice_amount END, 0)) AS 'Mar',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 4  THEN s.sample_invoice_amount END, 0)) AS 'Apr',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 5  THEN s.sample_invoice_amount END, 0)) AS 'May',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 6  THEN s.sample_invoice_amount END, 0)) AS 'Jun',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 7  THEN s.sample_invoice_amount END, 0)) AS 'Jul',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 8  THEN s.sample_invoice_amount END, 0)) AS 'Aug',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 9  THEN s.sample_invoice_amount END, 0)) AS 'Sep',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 10 THEN s.sample_invoice_amount END, 0)) AS 'Oct',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 11 THEN s.sample_invoice_amount END, 0)) AS 'Nov',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 12 THEN s.sample_invoice_amount END, 0)) AS 'Dec'                    " +
            "FROM                                                                                                                       " +
            "  job_sample s                                                                                                             " +
            "WHERE YEAR(s.sample_invoice_date) is not null and (s.sample_invoice_status = 1  or s.sample_invoice_status is null)                                                                            " +
            "AND YEAR(s.sample_invoice_date) ={0}                                                                                       " +
            "GROUP BY  YEAR(s.sample_invoice_date)                                                                                      ";
            DataTable dt = MaintenanceBiz.ExecuteReturnDt(String.Format(sql, 2018));

            StringBuilder sbResultJson = new StringBuilder();
            String data = "";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string type = dr["invoice_type"].ToString();
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

                    data = "{name : '" + type + "', data: [" + Jan + "," + Feb + "," + Mar + "," + Apr + "," + May + "," + Jun + "," + Jul + "," + Aug + "," + Sep + "," + Oct + "," + Nov + "," + Dec + "]},";
                }
                data = data.Substring(0, data.Length - 1);
            }
            else
            {
                data = "{name : 'revenue', data: [0,0,0,0,0,0,0,0,0,0,0,0]}";
            }

            sbResultJson.Append(data);
            return sbResultJson.ToString();
        }
        public String Report1_2()
        {
            String sql = "" +
            "SELECT                                                                                                                     " +
            "  (case when s.sample_invoice_status is null or s.sample_invoice_status = 1 then 'revenue' else 'actual' end) invoice_type," +
            //"  YEAR(s.sample_invoice_date)                                             AS 'Year',                                       " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 1  THEN s.sample_invoice_amount END, 0)) AS 'Jan',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 2  THEN s.sample_invoice_amount END, 0)) AS 'Feb',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 3  THEN s.sample_invoice_amount END, 0)) AS 'Mar',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 4  THEN s.sample_invoice_amount END, 0)) AS 'Apr',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 5  THEN s.sample_invoice_amount END, 0)) AS 'May',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 6  THEN s.sample_invoice_amount END, 0)) AS 'Jun',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 7  THEN s.sample_invoice_amount END, 0)) AS 'Jul',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 8  THEN s.sample_invoice_amount END, 0)) AS 'Aug',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 9  THEN s.sample_invoice_amount END, 0)) AS 'Sep',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 10 THEN s.sample_invoice_amount END, 0)) AS 'Oct',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 11 THEN s.sample_invoice_amount END, 0)) AS 'Nov',                   " +
            "  SUM(IFNULL(CASE WHEN MONTH(s.sample_invoice_date) = 12 THEN s.sample_invoice_amount END, 0)) AS 'Dec'                    " +
            "FROM                                                                                                                       " +
            "  job_sample s                                                                                                             " +
            "WHERE YEAR(s.sample_invoice_date) is not null and (s.sample_invoice_status = 2)                                                                            " +
            "AND YEAR(s.sample_invoice_date) ={0}                                                                                       " +
            "GROUP BY  YEAR(s.sample_invoice_date)                                                                                      ";
            DataTable dt = MaintenanceBiz.ExecuteReturnDt(String.Format(sql, 2018));
            StringBuilder sbResultJson = new StringBuilder();
            String data = "";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string type = dr["invoice_type"].ToString();
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

                    data = "{name : '" + type + "', data: [" + Jan + "," + Feb + "," + Mar + "," + Apr + "," + May + "," + Jun + "," + Jul + "," + Aug + "," + Sep + "," + Oct + "," + Nov + "," + Dec + "]},";
                }
                data = data.Substring(0, data.Length - 1);
            }
            else
            {
                data = "{name : 'actual', data: [0,0,0,0,0,0,0,0,0,0,0,0]}";
            }
            sbResultJson.Append(data);
            return sbResultJson.ToString();
        }
        public String Report2_1()
        {
            String sql = "select sample_invoice_date,count(sample_invoice_date) icount FROM job_sample where sample_invoice_date is not null and (sample_invoice_status=1 or sample_invoice_status is null) GROUP BY DATE(sample_invoice_date) order by sample_invoice_date asc;";
            DataTable dt = MaintenanceBiz.ExecuteReturnDt(sql);
            StringBuilder sbResultJson = new StringBuilder();

            String data = "{name: \"Incomplete\",data: [";
            foreach (DataRow dr in dt.Rows)
            {
                DateTime epochDate = Convert.ToDateTime(dr["sample_invoice_date"].ToString());
                int count = Convert.ToInt32(dr["iCount"].ToString());
                data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + count + "],";
            }
            data = data.Substring(0, data.Length - 1);
            data += "]}";
            sbResultJson.Append(data);
            return sbResultJson.ToString();
        }
        public String Report2_2()
        {
            String sql = "select sample_invoice_date,count(sample_invoice_date) icount FROM job_sample where sample_invoice_date is not null and sample_invoice_status=2 GROUP BY DATE(sample_invoice_date) order by sample_invoice_date asc;";
            DataTable dt = MaintenanceBiz.ExecuteReturnDt(sql);
            StringBuilder sbResultJson = new StringBuilder();

            String data = "{name: \"Complete\",data: [";
            foreach (DataRow dr in dt.Rows)
            {
                DateTime epochDate = Convert.ToDateTime(dr["sample_invoice_date"].ToString());
                int count = Convert.ToInt32(dr["iCount"].ToString());
                data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + count + "],";
            }
            data = data.Substring(0, data.Length - 1);
            data += "]}";
            sbResultJson.Append(data);
            return sbResultJson.ToString();
        }
        public void Report3()
        {
            String sql = "" +
                        "SELECT                                                                                                          " +
                        "    `mc`.`ID` AS `company_id`,                                                                                  " +
                        "    `mc`.`company_name` AS `company_name`,                                                                      " +
                        "    `s`.`sample_invoice` AS `sample_invoice`,                                                                   " +
                        "    SUM((TO_DAYS(`s`.`sample_invoice_complete_date`) - TO_DAYS(`s`.`sample_invoice_date`))) AS `overdue_date`,  " +
                        "    SUM(`s`.`sample_invoice_amount`) AS `outstanding_balance`                                                   " +
                        "FROM                                                                                                            " +
                        "    ((`job_sample` `s`                                                                                          " +
                        "    LEFT JOIN `job_info` `j` ON((`j`.`ID` = `s`.`job_id`)))                                                     " +
                        "    LEFT JOIN `m_customer` `mc` ON((`mc`.`ID` = `j`.`customer_id`)))                                            " +
                        "GROUP BY `mc`.`ID` , `s`.`sample_invoice`                                                                       ";

            searchResult = MaintenanceBiz.ExecuteReturnDt(sql);
            gvRpt3.DataSource = searchResult;
            gvRpt3.DataBind();
        }
        public String Report4()
        {

            String sql = "select sample_invoice_date,sum(sample_invoice_amount) amt FROM job_sample where sample_invoice_date is not null  GROUP BY DATE(sample_invoice_date) order by sample_invoice_date asc;";
            DataTable dt = MaintenanceBiz.ExecuteReturnDt(sql);
            StringBuilder sbResultJson = new StringBuilder();

            String data = "[";
            #region "Amout"
            data += "{name: \"Amout\",data: [";
            foreach (DataRow dr in dt.Rows)
            {
                DateTime epochDate = Convert.ToDateTime(dr["sample_invoice_date"].ToString());
                int count = Convert.ToInt32(dr["amt"].ToString());
                data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + count + "],";
            }
            data = data.Substring(0, data.Length - 1);
            data += "]}";
            //sbResultJson.Append(data);
            #endregion
            #region "Forecast(Amout)"
            data += ",{name: \"Forecast(Amout)\",data: [";

            Hashtable listForecastAmtDate = new Hashtable();
            listForecastAmtDate[new DateTime(2019, 02, 12, 0, 0, 0)] = 17000;
            listForecastAmtDate[new DateTime(2019, 02, 17, 0, 0, 0)] = 18000;
            listForecastAmtDate[new DateTime(2019, 02, 19, 0, 0, 0)] = 19000;
            listForecastAmtDate[new DateTime(2019, 02, 25, 0, 0, 0)] = 22000;
            listForecastAmtDate[new DateTime(2019, 02, 27, 0, 0, 0)] = 24000;
            foreach (DictionaryEntry entry in listForecastAmtDate)
            {
                DateTime epochDate = Convert.ToDateTime(entry.Key);
                data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + entry.Value + "],";
            }
            data = data.Substring(0, data.Length - 1);
            data += "]}";
            #endregion
            #region "Lower Confidence Bound(Amout)"
            data += ",{name: \"Lower Confidence Bound(Amout)\",data: [";

            Hashtable listLowerConfidenceBoundAmout = new Hashtable();
            listLowerConfidenceBoundAmout[new DateTime(2019, 02, 12, 0, 0, 0)] = 170000;
            listLowerConfidenceBoundAmout[new DateTime(2019, 02, 17, 0, 0, 0)] = 180000;
            listLowerConfidenceBoundAmout[new DateTime(2019, 02, 19, 0, 0, 0)] = 190000;
            listLowerConfidenceBoundAmout[new DateTime(2019, 02, 25, 0, 0, 0)] = 220000;
            listLowerConfidenceBoundAmout[new DateTime(2019, 02, 27, 0, 0, 0)] = 240000;
            foreach (DictionaryEntry entry in listLowerConfidenceBoundAmout)
            {
                DateTime epochDate = Convert.ToDateTime(entry.Key);
                data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + entry.Value + "],";
            }
            data = data.Substring(0, data.Length - 1);
            data += "]}";
            #endregion
            #region "Upper Confidence Bound(Amout)"
            data += ",{name: \"Upper Confidence Bound(Amout)\",data: [";

            Hashtable listUpperConfidenceBoundAmout = new Hashtable();
            listUpperConfidenceBoundAmout[new DateTime(2019, 02, 12, 0, 0, 0)] = 19000;
            listUpperConfidenceBoundAmout[new DateTime(2019, 02, 17, 0, 0, 0)] = 22000;
            listUpperConfidenceBoundAmout[new DateTime(2019, 02, 19, 0, 0, 0)] = 30000;
            listUpperConfidenceBoundAmout[new DateTime(2019, 02, 25, 0, 0, 0)] = 35000;
            listUpperConfidenceBoundAmout[new DateTime(2019, 02, 27, 0, 0, 0)] = 44000;
            foreach (DictionaryEntry entry in listUpperConfidenceBoundAmout)
            {
                DateTime epochDate = Convert.ToDateTime(entry.Key);
                data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + entry.Value + "],";
            }
            data = data.Substring(0, data.Length - 1);
            data += "]}";
            #endregion
            data += "]";
            sbResultJson.Append(data);
            return sbResultJson.ToString();



//
//

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


        protected void gvJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();

            jsonSeriesRpt01 = "[" + Report1_1() + "," + Report1_2() + "]";
            jsonSeriesRpt02 = "[" + Report2_1() + "," + Report2_2() + "]";
            jsonSeriesRpt04 = Report4();
        }

        protected void gvRpt3_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
