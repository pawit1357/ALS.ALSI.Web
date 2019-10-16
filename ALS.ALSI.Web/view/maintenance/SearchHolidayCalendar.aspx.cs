using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class SearchHolidayCalendar : System.Web.UI.Page
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SearchTemplate));

        #region "Property"

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "SearchHolidayCalendar"]; }
            set { Session[GetType().Name + "SearchHolidayCalendar"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public DateTime dateHolidays { get; set; }

        
        public holiday_calendar obj
        {
            get
            {
                holiday_calendar tmp = new holiday_calendar();
                tmp.YEAR_HOLIDAYS = ddlCalYear.SelectedValue.ToString();
                return tmp;
            }
        }
        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<PhysicalYear> yesrList = new List<PhysicalYear>();
                for (int i = DateTime.Now.Year - 3; i < DateTime.Now.Year + 3; i++)
                {
                    PhysicalYear _year = new PhysicalYear();
                    _year.year = i;
                    yesrList.Add(_year);
                }

                ddlCalYear.Items.Clear();
                ddlCalYear.DataSource = yesrList;
                ddlCalYear.DataBind();
                ddlCalYear.SelectedValue = (DateTime.Now.Year).ToString();

                //
                bindingData();
            }
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
        }

        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.dateHolidays = Convert.ToDateTime(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);

            holiday_calendar template = new holiday_calendar().SelectByID(this.dateHolidays);
            if (template != null)
            {
                try
                {
                    int year = template.DATE_HOLIDAYS.Year;
                    int month = template.DATE_HOLIDAYS.Month;
                    int day = template.DATE_HOLIDAYS.Day;
                    string delSql = "delete from holiday_calendar where year(DATE_HOLIDAYS) ={0} and month(DATE_HOLIDAYS)={1} and day(DATE_HOLIDAYS)={2}";
                    MaintenanceBiz.ExecuteCommand(string.Format(delSql, year, month, day));
                    //template.Delete();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            //Commit
            //GeneralManager.Commit();
            bindingData();
        }

        protected void gvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {

            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_HILIDAY_CALENDAR);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //txtName.Text = string.Empty;
            lbTotalRecords.Text = string.Empty;

            removeSession();
            bindingData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindingData();
        }



        #region "Method"

        private void bindingData()
        {
            searchResult = obj.SearchData();
            gvResult.DataSource = searchResult;
            gvResult.DataBind();
            gvResult.UseAccessibleHeader = true;
            gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvResult.Rows.Count > 0)
            {
                lbTotalRecords.Text = String.Format(Constants.TOTAL_RECORDS, gvResult.Rows.Count);
            }
            else
            {
                lbTotalRecords.Text = string.Empty;
            }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "SearchHolidayCalendar");
        }

        #endregion
    }
}