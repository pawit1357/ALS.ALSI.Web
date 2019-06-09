using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
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

        // To calculate Running Total
        private double dblFooterAmount = 0;

        protected string jsonSeriesRpt01;
        protected string jsonSeriesRpt02;
        protected string jsonSeriesRpt03;
        protected string jsonSeriesRpt031;

        protected string jsonSeriesRpt04;

        private static Excel.Workbook MyBook = null;
        private static Excel.Application MyApp = null;
        private static Excel.Worksheet MySheet = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int year = DateTime.Now.Year;
                    if (DateTime.Now.Month < Constants.PHYSICAL_YEAR)
                    {
                        year= (DateTime.Now.Year - 1);
                    }
                    else
                    {
                        year = (DateTime.Now.Year);
                    }


                    txtStartDate.Text = new DateTime(year, 4, 1).ToString("dd/MM/yyyy");
                    txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    DateTime dtStartDate = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
                    DateTime dtEndDate = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);

                    jsonSeriesRpt01 = Report1(dtStartDate, dtEndDate);
                    jsonSeriesRpt02 = Report2(dtStartDate, dtEndDate);
                    jsonSeriesRpt031 = Report31(dtStartDate, dtEndDate);
                    jsonSeriesRpt04 = Report4(dtStartDate, dtEndDate);
                    //
                    Report3(dtStartDate, dtEndDate);

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
            DateTime dtStartDate = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            DateTime dtEndDate = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);

            jsonSeriesRpt01 = Report1(dtStartDate, dtEndDate);
            jsonSeriesRpt02 = Report2(dtStartDate, dtEndDate);
            jsonSeriesRpt031 = Report31(dtStartDate, dtEndDate);
            jsonSeriesRpt04 = Report4(dtStartDate, dtEndDate);
            //
            Report3(dtStartDate, dtEndDate);
        }

        protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dtStartDate = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            DateTime dtEndDate = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);

            jsonSeriesRpt01 = Report1(dtStartDate, dtEndDate);
            jsonSeriesRpt02 = Report2(dtStartDate, dtEndDate);
            jsonSeriesRpt031 = Report31(dtStartDate, dtEndDate);
            jsonSeriesRpt04 = Report4(dtStartDate, dtEndDate);
            //
            Report3(dtStartDate, dtEndDate);

        }

        public String Report1(DateTime s, DateTime e)
        {
            String sql = "" +
            "SELECT                                                                                                                     " +
            "  (case when s.sample_invoice_status is null or s.sample_invoice_status = 1 then 'revenue' else 'actual' end) invoice_type," +
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
            "WHERE YEAR(s.sample_invoice_date) is not null " +
            " and s.sample_invoice_date between'" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "'"+
            "GROUP BY  YEAR(s.sample_invoice_date)                                                                                      ";
            DataTable dt = MaintenanceBiz.ExecuteReturnDt(sql);

            StringBuilder sbResultJson = new StringBuilder();
            String data = "[";
            string type = "";
            string Jan = "";
            string Feb = "";
            string Mar = "";
            string Apr = "";
            string May = "";
            string Jun = "";
            string Jul = "";
            string Aug = "";
            string Sep = "";
            string Oct = "";
            string Nov = "";
            string Dec = "";
            switch (dt.Rows.Count)
            {
                case 1:
                    type = dt.Rows[0]["invoice_type"].ToString();
                    Jan = dt.Rows[0]["Jan"].ToString();
                    Feb = dt.Rows[0]["Feb"].ToString();
                    Mar = dt.Rows[0]["Mar"].ToString();
                    Apr = dt.Rows[0]["Apr"].ToString();
                    May = dt.Rows[0]["May"].ToString();
                    Jun = dt.Rows[0]["Jun"].ToString();
                    Jul = dt.Rows[0]["Jul"].ToString();
                    Aug = dt.Rows[0]["Aug"].ToString();
                    Sep = dt.Rows[0]["Sep"].ToString();
                    Oct = dt.Rows[0]["Oct"].ToString();
                    Nov = dt.Rows[0]["Nov"].ToString();
                    Dec = dt.Rows[0]["Dec"].ToString();
                    data += "{name : '" + type + "', data: [" + Jan + "," + Feb + "," + Mar + "," + Apr + "," + May + "," + Jun + "," + Jul + "," + Aug + "," + Sep + "," + Oct + "," + Nov + "," + Dec + "]}";
                    break;
                case 2:
                    type = dt.Rows[0]["invoice_type"].ToString();
                    Jan = dt.Rows[0]["Jan"].ToString();
                    Feb = dt.Rows[0]["Feb"].ToString();
                    Mar = dt.Rows[0]["Mar"].ToString();
                    Apr = dt.Rows[0]["Apr"].ToString();
                    May = dt.Rows[0]["May"].ToString();
                    Jun = dt.Rows[0]["Jun"].ToString();
                    Jul = dt.Rows[0]["Jul"].ToString();
                    Aug = dt.Rows[0]["Aug"].ToString();
                    Sep = dt.Rows[0]["Sep"].ToString();
                    Oct = dt.Rows[0]["Oct"].ToString();
                    Nov = dt.Rows[0]["Nov"].ToString();
                    Dec = dt.Rows[0]["Dec"].ToString();
                    data += "{name : '" + type + "', data: [" + Jan + "," + Feb + "," + Mar + "," + Apr + "," + May + "," + Jun + "," + Jul + "," + Aug + "," + Sep + "," + Oct + "," + Nov + "," + Dec + "]},";
                    type = dt.Rows[1]["invoice_type"].ToString();
                    Jan = dt.Rows[1]["Jan"].ToString();
                    Feb = dt.Rows[1]["Feb"].ToString();
                    Mar = dt.Rows[1]["Mar"].ToString();
                    Apr = dt.Rows[1]["Apr"].ToString();
                    May = dt.Rows[1]["May"].ToString();
                    Jun = dt.Rows[1]["Jun"].ToString();
                    Jul = dt.Rows[1]["Jul"].ToString();
                    Aug = dt.Rows[1]["Aug"].ToString();
                    Sep = dt.Rows[1]["Sep"].ToString();
                    Oct = dt.Rows[1]["Oct"].ToString();
                    Nov = dt.Rows[1]["Nov"].ToString();
                    Dec = dt.Rows[1]["Dec"].ToString();
                    data += "{name : '" + type + "', data: [" + Jan + "," + Feb + "," + Mar + "," + Apr + "," + May + "," + Jun + "," + Jul + "," + Aug + "," + Sep + "," + Oct + "," + Nov + "," + Dec + "]},";
                    break;
                default:
                    break;
            }

            data += "]";
            return data;
        }

        public String Report2(DateTime s, DateTime e)
        {
            String sql = "";
            String data = "[";
            #region "Incomplete"
            //sql = "select sample_invoice_date,count(sample_invoice) icount FROM job_sample where sample_invoice_date is not null and (sample_invoice_status=1 or sample_invoice_status is null) and sample_invoice_date between'" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "' GROUP BY DATE(sample_invoice_date) order by sample_invoice_date asc;";
            sql = "select * from (select sample_invoice_date,count(sample_invoice) icount FROM job_sample where sample_invoice_date is not null and (sample_invoice_status=1 or sample_invoice_status is null) and sample_invoice_date between'" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "' GROUP BY DATE(sample_invoice_date) order by sample_invoice_date asc limit 15) tmp order by  sample_invoice_date;";
            DataTable dtIncomplete = MaintenanceBiz.ExecuteReturnDt(sql);

            if (dtIncomplete.Rows.Count > 0)
            {
                data += "{name: \"Incomplete\",data: [";
                foreach (DataRow dr in dtIncomplete.Rows)
                {
                    DateTime epochDate = Convert.ToDateTime(dr["sample_invoice_date"].ToString());
                    int count = Convert.ToInt32(dr["iCount"].ToString());
                    data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + count + "],";
                }
                data = data.Substring(0, data.Length - 1);
                data += "]},";
            }
            #endregion

            #region "dtComplete"
            //sql = "select sample_invoice_date,count(sample_invoice) icount FROM job_sample where sample_invoice_date is not null and sample_invoice_status=2 and sample_invoice_date between'" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "' GROUP BY DATE(sample_invoice_date) order by sample_invoice_date asc;";
            sql = "select * from (select sample_invoice_date,count(sample_invoice) icount FROM job_sample where sample_invoice_date is not null and sample_invoice_status=2 and sample_invoice_date between'" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "' GROUP BY DATE(sample_invoice_date) order by sample_invoice_date asc limit 15) tmp order by  sample_invoice_date;";
            DataTable dtComplete = MaintenanceBiz.ExecuteReturnDt(sql);

            if (dtComplete.Rows.Count > 0)
            {
                data += "{name: \"Complete\",data: [";
                foreach (DataRow dr in dtComplete.Rows)
                {
                    DateTime epochDate = Convert.ToDateTime(dr["sample_invoice_date"].ToString());
                    int count = Convert.ToInt32(dr["iCount"].ToString());
                    data += "[Date.UTC(" + epochDate.Year + ", " + epochDate.Month + ", " + epochDate.Day + "), " + count + "],";
                }
                data = data.Substring(0, data.Length - 1);
                data += "]}";
            }
            #endregion

            data += "]";




            return data;
        }

        public void Report3(DateTime s,DateTime e)
        {
            String sql = "";
            sql += " select                                                              ";
            sql += " j.customer_id,                                                      ";
            sql += " c.company_name,                                                     ";
            sql += " s.sample_invoice,                                                   ";
            sql += " s.sample_invoice_date,                                              ";
            sql += " TO_DAYS(Now()) - TO_DAYS(s.sample_invoice_date) as overdue_date,    ";
            sql += " sum(s.sample_invoice_amount) sample_invoice_amount                  ";
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
                sql += " sum(s.sample_invoice_amount) as sumAmout                          ";
                sql += " from job_sample s                                                 ";
                sql += " left                                                              ";
                sql += " join job_info j on j.ID = s.job_id                                ";
                sql += " left                                                              ";
                sql += " join m_customer c on c.ID = j.customer_id                         ";
                sql += " where s.sample_invoice is not null                                ";
                sql += " and s.sample_invoice <> ''                                        ";
                sql += " and s.sample_invoice_date is not null                             ";
                sql += " and s.sample_invoice_date between'" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "'";
                sql += " group by c.ID) tmp order by sumAmout desc limit "+ddlPeriod.SelectedValue;


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


                String sql = "select * from (select sample_invoice_date,sum(sample_invoice_amount) amt FROM job_sample where sample_invoice_date is not null and sample_invoice_date between '" + s.ToString("yyyy-MM-dd") + "' AND '" + e.ToString("yyyy-MM-dd") + "'  GROUP BY DATE(sample_invoice_date) order by sample_invoice_date desc limit 30) x order by sample_invoice_date asc";

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
            catch (Exception ex)
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
                    dblGrandTotal += Convert.ToDouble(dr["sample_invoice_amount"]);
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


                    