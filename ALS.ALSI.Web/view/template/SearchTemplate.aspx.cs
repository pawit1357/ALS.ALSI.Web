using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.template
{
    public partial class SearchTemplate : System.Web.UI.Page
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SearchTemplate));

        #region "Property"

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "SearchTemplate"]; }
            set { Session[GetType().Name + "SearchTemplate"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public int PKID { get; set; }

        public m_template obj
        {
            get
            {
                m_template tmp = new m_template();
                tmp.name = txtName.Text;
                return tmp;
            }
        }
        #endregion

        #region "Method"
        private void initialPage()
        {
            bindingData();
        }

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
            Session.Remove(GetType().Name + "SearchTemplate");
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
                    Server.Transfer(Constants.LINK_TEMPLATE);
                    break;
                case CommandNameEnum.Inactive:
                    this.PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COMMA)[0]);
                    m_template template = new m_template().SelectByID(this.PKID);
                    if (template != null)
                    {
                        template.status = "I";
                        template.Update();
                        GeneralManager.Commit();

                        bindingData();
                    }
                        break;
            }

        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_TEMPLATE);
        }

        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.PKID = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);

            m_template template = new m_template().SelectByID(this.PKID);
            if (template != null)
            {
                template.Delete();
                bindingData();
            }
            //Commit
            GeneralManager.Commit();
        }

        protected void gvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0) return;
            GridView gv = (GridView)sender;
            gv.DataSource = searchResult;
            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
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

        protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                //int PKID = Convert.ToInt32(gvResult.DataKeys[e.Row.RowIndex].Values[0].ToString());
                //string status = gvResult.DataKeys[e.Row.RowIndex].Values[1].ToString();
                //LinkButton _btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                //LinkButton _btnInActive = (LinkButton)e.Row.FindControl("btnInActive");

                //if (_btnEdit != null && _btnInActive != null)
                //{
                //    if (status.Equals("A"))
                //    {
                //        _btnInActive.Visible = true;
                //        _btnEdit.Visible = true;
                //    }
                //    else
                //    {
                //        _btnInActive.Visible = false;
                //        _btnEdit.Visible = true;
                //    }
                //}
            }
        }
    }
}