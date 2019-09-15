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
        protected string jsonSeriesRpt04Categories;


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
                    rptForecastAndBudget();
                    jsonSeriesRpt031 = "[]";//Report31(dtStartDate, dtEndDate);
                    //
                    Report3();
                    #endregion

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            rptRevenueActual();
            rptDailyInvoice();
            rptForecastAndBudget();
            Report3();

            //Report31();
            //Report4();
            jsonSeriesRpt031 = "[]";

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
            DateTime receive_report_from = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            DateTime receive_report_to = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);

            string sql = "";
            sql += "select year(tmp.InvoiceDate) as xYear,";
            sql += " sum(ifnull(case when month(InvoiceDate) = 1 then TotalSubAmount end, 0)) as 'Jan',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 2 then TotalSubAmount end, 0)) as 'Feb',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 3 then TotalSubAmount end, 0)) as 'Mar',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 4 then TotalSubAmount end, 0)) as 'Apr',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 5 then TotalSubAmount end, 0)) as 'May',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 6 then TotalSubAmount end, 0)) as 'Jun',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 7 then TotalSubAmount end, 0)) as 'Jul',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 8 then TotalSubAmount end, 0)) as 'Aug',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 9 then TotalSubAmount end, 0)) as 'Sep',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 10 then TotalSubAmount end, 0)) as 'Oct',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 11 then TotalSubAmount end, 0)) as 'Nov',";
            sql += " sum(ifnull(case when month(InvoiceDate) = 12 then TotalSubAmount end, 0)) as 'Dec'";
            sql += " from(";
            sql += "    select";
            sql += "        s.sample_invoice_date as InvoiceDate,";
            sql += "        s.sample_invoice as Invoice,";
            sql += "        max(s.sample_invoice_amount_rpt) as TotalSubAmount";
            sql += "    from job_sample s";
            sql += "    left";
            sql += "    join job_info i on s.job_id = i.id";
            sql += " left";
            sql += "    join m_customer c on c.id = i.customer_id";
            sql += "    where 1 = 1";
            sql += "    and s.sample_so <> ''";
            if (!String.IsNullOrEmpty(ddlBoiNonBoi.SelectedValue.ToString()))
            {
                if (ddlBoiNonBoi.SelectedValue.ToString().Equals("B"))
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
                else
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
            }
            if (!ddlCompany.SelectedValue.ToString().Equals("0"))
            {
                sql += " and i.customer_id  = " + ddlCompany.SelectedValue;
            }
            if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
            {
                sql += " and s.sample_invoice_date between '" + receive_report_from.ToString("yyyy-MM-dd") + "' and '" + receive_report_to.ToString("yyyy-MM-dd") + "'";
            }
            sql += "    group by s.sample_invoice_date, s.sample_invoice";
            sql += " ) tmp";
            sql += " group by year(tmp.InvoiceDate)";


            DataTable dt = MaintenanceBiz.ExecuteReturnDt(sql);
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
            DateTime receive_report_from = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            DateTime receive_report_to = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);
            StringBuilder sqlCriIncomplete = new StringBuilder();

            Hashtable htInv = new Hashtable();

            #region "Incomplete"
            string sql = "";
            sql += "select 'WIP' as xType,InvoiceDate, count(Invoice) as iCount from(";
            sql += "    select";
            sql += "        s.sample_invoice_date as InvoiceDate,";
            sql += "        s.sample_invoice as Invoice,";
            sql += "        max(s.sample_invoice_amount_rpt) as TotalSubAmount";
            sql += "    from job_sample s";
            sql += "    left join job_info i on s.job_id = i.id";
            sql += "    left join m_customer c on c.id = i.customer_id";
            sql += "    where 1 = 1";
            sql += "    and s.sample_invoice <> '' and s.sample_invoice_status in (1, 2) and s.sample_invoice_date is not null";
            if (!String.IsNullOrEmpty(ddlBoiNonBoi.SelectedValue.ToString()))
            {
                if (ddlBoiNonBoi.SelectedValue.ToString().Equals("B"))
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
                else
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
            }
            if (!ddlCompany.SelectedValue.ToString().Equals("0"))
            {
                sql += " and i.customer_id  = " + ddlCompany.SelectedValue;
            }
            if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
            {
                sql += " and s.sample_invoice_date between '" + receive_report_from.ToString("yyyy-MM-dd") + "' and '" + receive_report_to.ToString("yyyy-MM-dd") + "'";
            }
            sql += "    group by s.sample_invoice_date, s.sample_invoice";
            sql += ") tmp";
            sql += " group by tmp.InvoiceDate";
            sql += " union";
            sql += " select 'TIV' as xType,InvoiceDate, count(Invoice) as iCount from(";
            sql += "    select";
            sql += "        s.sample_invoice_date as InvoiceDate,";
            sql += "        s.sample_invoice as Invoice,";
            sql += "        max(s.sample_invoice_amount_rpt) as TotalSubAmount";
            sql += "    from job_sample s";
            sql += "    left join job_info i on s.job_id = i.id";
            sql += "    left join m_customer c on c.id = i.customer_id";
            sql += "    where 1 = 1";
            sql += "    and s.sample_so <> ''";
            if (!String.IsNullOrEmpty(ddlBoiNonBoi.SelectedValue.ToString()))
            {
                if (ddlBoiNonBoi.SelectedValue.ToString().Equals("B"))
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
                else
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
            }
            if (!ddlCompany.SelectedValue.ToString().Equals("0"))
            {
                sql += " and i.customer_id  = " + ddlCompany.SelectedValue;
            }
            if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
            {
                sql += " and s.sample_invoice_date between'" + receive_report_from.ToString("yyyy-MM-dd") + "' and '" + receive_report_to.ToString("yyyy-MM-dd") + "'";
            }
            sql += "    group by s.sample_invoice_date, s.sample_invoice";
            sql += ") tmp";
            sql += " group by tmp.InvoiceDate";


            DataTable dtIncomplete = MaintenanceBiz.ExecuteReturnDt(sql);

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
            }

            dataCat = dataCat.Length == 0 ? "" : dataCat.Remove(dataCat.Length - 1, 1);
            dataWip = dataWip.Length == 0 ? "" : dataWip.Remove(dataWip.Length - 1, 1);
            dataTiv = dataTiv.Length == 0 ? "" : dataTiv.Remove(dataTiv.Length - 1, 1);

            #endregion

            jsonSeriesRpt02Categories = "[" + dataCat + "]";
            jsonSeriesRpt02 = "[{name : 'Invoice', data: [" + dataWip + "]},{name : 'Total Invoice', data: [" + dataTiv + "]}]";
        }

        public void Report3()
        {

            DateTime receive_report_from = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            DateTime receive_report_to = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);

            String sql = "";
            sql += "select";
            sql += " s.job_number,";
            //sql += "--s.status_completion_scheduled,                                                           ";
            //sql += "--s.job_status,                                                                            ";
            sql += " cs.name as jobType,";
            sql += " ms.name as jobStatus,";
            sql += " s.sample_invoice,";
            sql += " i.date_of_receive,";
            sql += " datediff(s.date_login_complete, s.date_login_inprogress) + 1 as 'Login',";
            //sql += " --s.date_login_inprogress,";
            //sql += " --s.date_login_complete,";
            sql += " datediff(s.date_chemist_complete, s.date_chemist_analyze) + 1 as 'Chemist',";
            //sql += "--s.date_chemist_analyze,";
            //sql += "--s.date_chemist_complete,";
            sql += " datediff(s.date_srchemist_complate, s.date_srchemist_analyze) + 1 as 'SRChemist',";
            //sql += "--s.date_srchemist_analyze,";
            //sql += "--s.date_srchemist_complate,";
            sql += " datediff(s.date_admin_word_complete, s.date_admin_word_inprogress) + 1 as 'AdminWord',";
            //sql += "--s.date_admin_word_inprogress,";
            //sql += "--s.date_admin_word_complete,";
            sql += " datediff(s.date_labman_complete, s.date_labman_analyze) + 1 as 'LabManager',";
            //sql += "--s.date_labman_analyze,";
            //sql += "--s.date_labman_complete,";
            sql += " datediff(s.date_admin_pdf_complete, s.date_admin_pdf_inprogress) + 1 as 'AdminPdf',";
            sql += " s.date_admin_sent_to_cus";
            //sql += "-- s.date_admin_pdf_inprogress,";
            //sql += "--s.date_admin_pdf_complete";
            sql += " from job_sample s";
            sql += " left join m_status ms on s.job_status = ms.ID";
            sql += " left join job_info i on i.ID = s.job_id";
            sql += " left join m_completion_scheduled cs on cs.ID = s.status_completion_scheduled";
            sql += " where 1=1";
            if (!String.IsNullOrEmpty(ddlBoiNonBoi.SelectedValue.ToString()))
            {
                if (ddlBoiNonBoi.SelectedValue.ToString().Equals("B"))
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
                else
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
            }
            if (!ddlCompany.SelectedValue.ToString().Equals("0"))
            {
                sql += " and i.customer_id  = " + ddlCompany.SelectedValue;
            }
            if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
            {
                sql += " and s.sample_invoice_date between '" + receive_report_from.ToString("yyyy-MM-dd") + "' and '" + receive_report_to.ToString("yyyy-MM-dd") + "'";
            }
            sql += " order by s.job_number desc";




            //if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
            //{
            //    sql += " and s.sample_invoice_date between'" + receive_report_from.ToString("yyyy-MM-dd") + "' and '" + receive_report_to.ToString("yyyy-MM-dd") + "'";
            //}

            //sql += " group by s.sample_invoice_date,j.customer_id,s.sample_invoice;";


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

        public void rptForecastAndBudget()
        {

            DateTime receive_report_from = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtStartDate.Text);
            DateTime receive_report_to = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.MinValue : CustomUtils.converFromDDMMYYYY(txtEndDate.Text);

            Hashtable htInv = new Hashtable();

            string sql = "";
            sql += " select InvoiceDate,sum(TotalSubAmount) as TotalSubAmount from (";
            sql += " select s.sample_invoice_date as InvoiceDate,s.sample_invoice as Invoice,max(s.sample_invoice_amount_rpt) as TotalSubAmount";
            sql += " from job_sample s";
            sql += " left join job_info i on s.job_id = i.id";
            sql += " left join m_customer c on c.id = i.customer_id";
            sql += " where 1 = 1";
            if (!String.IsNullOrEmpty(ddlBoiNonBoi.SelectedValue.ToString()))
            {
                if (ddlBoiNonBoi.SelectedValue.ToString().Equals("B"))
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
                else
                {
                    sql += " and right(s.job_number, 1) <> 'B'";
                }
            }
            if (!ddlCompany.SelectedValue.ToString().Equals("0"))
            {
                sql += " and i.customer_id  = " + ddlCompany.SelectedValue;
            }
            if (receive_report_from != DateTime.MinValue && receive_report_to != DateTime.MinValue)
            {
                sql += " and s.sample_invoice_date between '" + receive_report_from.ToString("yyyy-MM-dd") + "' and '" + receive_report_to.ToString("yyyy-MM-dd") + "'";
            }
            sql += " group by s.sample_invoice_date,s.sample_invoice) tmp";
            sql += " group by tmp.InvoiceDate";

            DataTable dtIncomplete = MaintenanceBiz.ExecuteReturnDt(sql);

            String dataCat = "";
            String dataTiv = "";

            foreach (DataRow dr in dtIncomplete.Rows)
            {
                try
                {
                    string invDate = dr["InvoiceDate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["InvoiceDate"]).ToString("yyyyMMdd");
                    double iCount = Convert.ToDouble(dr["TotalSubAmount"].ToString());

                    dataCat += string.Format("'{0}',", invDate);
                    dataTiv += string.Format("{0},", iCount);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            dataCat = dataCat.Length == 0 ? "" : dataCat.Remove(dataCat.Length - 1, 1);
            dataTiv = dataTiv.Length == 0 ? "" : dataTiv.Remove(dataTiv.Length - 1, 1);

            jsonSeriesRpt04Categories = "[" + dataCat + "]";
            jsonSeriesRpt04 = "[{name : 'Amout', data: [" + dataTiv + "]}]";

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
                //// Get the Row Amount
                //Label lbBalance = ((Label)e.Row.FindControl("lbBalance"));

                //// Each Row Amount values are added to the footer Amount variable
                //dblFooterAmount = dblFooterAmount + Convert.ToDouble(lbBalance.Text);
                //lbBalance.Text = Convert.ToDouble(lbBalance.Text).ToString("N0");
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
            }
        }

        protected void gvRpt3_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //int intNoOfMergeCol = e.Row.Cells.Count - 1; /*except last column */

                //GridViewRow footerRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Insert);

                ////Adding Footer Total Text Column
                //TableCell cell = new TableCell();
                //cell.Text = "Total : ";
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.ColumnSpan = intNoOfMergeCol;

                //footerRow.Cells.Add(cell);

                ////Adding Footer Total Amount Column
                //cell = new TableCell();
                //Label lbl = new Label();
                //lbl.ID = "lblFooterAmount";
                //lbl.Text = dblFooterAmount.ToString("N0");
                //cell.Controls.Add(lbl);
                //cell.HorizontalAlign = HorizontalAlign.Right;

                //footerRow.Cells.Add(cell);

                //gvRpt3.Controls[0].Controls.Add(footerRow);
                //double dblGrandTotal = 0;
                //foreach (DataRow dr in searchResult.Rows)
                //{
                //    dblGrandTotal += Convert.ToDouble(dr["sample_invoice_amount_rpt"]);
                //}

                //// First cell is used for specifying the Total text
                //for (int intCellCol = 1; intCellCol < intNoOfMergeCol; intCellCol++)
                //    e.Row.Cells.RemoveAt(1);
                //e.Row.Cells[0].ColumnSpan = intNoOfMergeCol;
                //e.Row.Cells[0].Text = "Grand Total : ";
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[1].Text = dblGrandTotal.ToString("N0");
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

