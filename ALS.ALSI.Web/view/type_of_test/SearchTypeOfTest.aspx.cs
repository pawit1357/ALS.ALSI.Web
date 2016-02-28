using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.type_of_test
{
    public partial class SearchTypeOfTest : System.Web.UI.Page
    {

        #region "Property"

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "SearchTypeOfTest"]; }
            set { Session[GetType().Name + "SearchTypeOfTest"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public int PKID { get; set; }

        public m_type_of_test obj
        {
            get
            {
                m_type_of_test tmp = new m_type_of_test();
                //tmp.ID = String.IsNullOrEmpty(ddlTypeOfTest.SelectedValue) ? 0 : int.Parse(ddlTypeOfTest.SelectedValue);
                tmp.specification_id = String.IsNullOrEmpty(ddlSpecification.SelectedValue) ? 0 : int.Parse(ddlSpecification.SelectedValue);
                tmp.name = txtName.Text;
                return tmp;
            }
        }
        #endregion

        #region "Method"
        private void initialPage()
        {
            m_specification specification = new m_specification();
            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = specification.SelectAll();
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));

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
            Session.Remove(GetType().Name + "SearchTypeOfTest");
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
                    Server.Transfer(Constants.LINK_TYPE_OF_TEST);
                    break;
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_TYPE_OF_TEST);
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            bindingData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlSpecification.SelectedIndex = 0;
            //ddlTypeOfTest.SelectedIndex = 0;
            txtName.Text = string.Empty;
            lbTotalRecords.Text = string.Empty;

            removeSession();
            bindingData();
        }

        protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.PKID = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COMMA)[0]);

            m_type_of_test cus = new m_type_of_test().SelectByID(this.PKID);
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

        //protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    List<m_type_of_test> lists = new m_type_of_test().SelectParent(int.Parse(ddlSpecification.SelectedValue));
        //    List<m_type_of_test> parents = lists.FindAll(x => x.parent == -1);
        //    foreach (m_type_of_test parent in parents)
        //    {
        //        ListItem it = new ListItem(parent.name, parent.ID.ToString());
        //        ddlTypeOfTest.Items.Add(it);

        //        List<m_type_of_test> childs = new m_type_of_test().SelectChild(parent.ID);
        //        if (childs != null)
        //        {
        //            if (childs.Count > 0)
        //            {
        //                foreach (m_type_of_test child in childs)
        //                {
        //                    ListItem it1 = new ListItem("---" + child.name, child.ID.ToString());
        //                    ddlTypeOfTest.Items.Add(it1);
        //                }
        //            }
        //        }
        //    }

        //    ddlTypeOfTest.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
        //}


    }
}