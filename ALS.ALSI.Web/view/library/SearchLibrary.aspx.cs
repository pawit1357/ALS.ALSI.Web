using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.library
{
    public partial class SearchLibrary : System.Web.UI.Page
    {

        #region "Property"

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "SearchLibrary"]; }
            set { Session[GetType().Name + "SearchLibrary"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public int PKID { get; set; }

        //public tb_m_dhs_library obj
        //{
        //    get
        //    {
        //        tb_m_dhs_library tmp = new tb_m_dhs_library();
        //        tmp.specification_id = String.IsNullOrEmpty(ddlSpecification.SelectedValue) ? 0 : Convert.ToInt32(ddlSpecification.SelectedValue);
        //        tmp.classification = txtClassification.Text;
        //        return tmp;
        //    }
        //}

        #endregion

        #region "Method"
        private void initialPage()
        {
            bindingData();
        }

        private void bindingData()
        {
            searchResult = new m_specification().SearchData();
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
            Session.Remove(GetType().Name + "SearchLibrary");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
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
                    Server.Transfer(Constants.LINK_LIBRARY);
                    break;
            }
        }

        //protected void lbAdd_Click(object sender, EventArgs e)
        //{
        //    this.CommandName = CommandNameEnum.Add;
        //    Server.Transfer(Constants.LINK_LIBRARY);
        //}

        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.PKID = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);
            tb_m_dhs_library cus = new tb_m_dhs_library().SelectByID(this.PKID);
            if (cus != null)
            {
                cus.Delete();
                //Commit
                GeneralManager.Commit();

                bindingData();
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

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            bindingData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //txtClassification.Text = string.Empty;
            //ddlSpecification.SelectedIndex = 0;
            lbTotalRecords.Text = string.Empty;

            removeSession();
            bindingData();
        }
    }
}