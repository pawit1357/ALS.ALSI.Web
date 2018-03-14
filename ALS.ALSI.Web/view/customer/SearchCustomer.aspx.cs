using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections;
using System.Data;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.customer
{
    public partial class SearchCustomer : System.Web.UI.Page
    {

        #region "Property"

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "SearchCustomer"]; }
            set { Session[GetType().Name + "SearchCustomer"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        public m_customer obj
        {
            get
            {
                m_customer tmp = new m_customer();
                tmp.customer_code = txtCustomerCode.Text;
                tmp.company_name = txtCompanyName.Text;
                //tmp.job_number = txtJobNumber.Text;
                //tmp.date_of_receive = String.IsNullOrEmpty(txtDateOfRecieve.Text) ? DateTime.MinValue : Convert.ToDateTime(txtDateOfRecieve.Text);
                //tmp.contract_person_id = String.IsNullOrEmpty(ddlContract_person.SelectedValue) ? 0 : int.Parse(ddlContract_person.SelectedValue);
                //tmp.customer_id = String.IsNullOrEmpty(ddlCompany.SelectedValue) ? 0 : int.Parse(ddlCompany.SelectedValue);

                return tmp;
            }
        }

        public int PKID { get; set; }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "SearchCustomer");
        }

        #endregion

        #region "Method"
        private void initialPage()
        {
            bindingData();
        }

        private void bindingData([Optional] string sortDirection, [Optional] string sortExpression)
        {
            searchResult = obj.SearchData(sortDirection, sortExpression);
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_CUSTOMER);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindingData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //txtCompanyName.Text = string.Empty;
            //txtCustomerCode.Text = string.Empty;
            lbTotalRecords.Text = string.Empty;

            removeSession();
            bindingData();
        }

        protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
            this.CommandName = cmd;
            switch (cmd)
            {
                case CommandNameEnum.Edit:
                case CommandNameEnum.View:
                    this.PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    Server.Transfer(Constants.LINK_CUSTOMER);
                    break;
                case CommandNameEnum.Sort:
                   
                    break;
            }
        }

        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.PKID = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);
            m_customer cus = new m_customer().SelectByID(this.PKID);
            if (cus != null)
            {
                try
                {
                    cus.Delete();
                    //Commit
                    GeneralManager.Commit();
                    bindingData();
                }
                catch (Exception ex) {
                    Console.WriteLine();
                    MessageBox.Show(this.Page, Resources.MSG_BE_USED);
                }

            }
        }

        protected void gvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
        }

        protected void gvResult_Sorting(object sender, GridViewSortEventArgs e)
        {

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
            };

            String sortExpression = e.SortExpression;
            String sortDirection = ConvertSortDirectionToSql(GridViewSortDirection);
            bindingData(sortDirection,sortExpression);
            Console.WriteLine();
        }


        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

        //protected void gvResult_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    //DataTable dataTable = gridView.DataSource as DataTable;

        //    if (searchResult != null)
        //    {

        //        //DataView dataView = new DataView(
        //        gvResult.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

        //        gvResult.DataSource = dataView;
        //        gvResult.DataBind();
        //    }
        //}


    }
}