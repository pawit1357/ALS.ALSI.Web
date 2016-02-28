    using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.type_of_test
{
    public partial class TypeOfTest : System.Web.UI.Page
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(TypeOfTest));

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public string PreviousPath
        {
            get { return (string)ViewState[GetType().Name + Constants.PREVIOUS_PATH]; }
            set { ViewState[GetType().Name + Constants.PREVIOUS_PATH] = value; }
        }

        public int PKID
        {
            get { return (int)Session[GetType().Name + "PKID"]; }
            set { Session[GetType().Name + "PKID"] = value; }
        }

        public m_type_of_test obj
        {
            get
            {
                m_type_of_test tmp = new m_type_of_test();
                tmp.ID = PKID;
                tmp.specification_id = String.IsNullOrEmpty(ddlSpecification.SelectedValue) ? 0 : int.Parse(ddlSpecification.SelectedValue);
                tmp.prefix = txtPrefix.Text;
                tmp.name = txtName.Text;
                tmp.parent = String.IsNullOrEmpty(ddlSpecification.SelectedValue) ? -1 : int.Parse(ddlSpecification.SelectedValue);
                tmp.status = "A";
                tmp.data_group = txtDataGroup.Text;
                return tmp;
            }
        }

        private void initialPage()
        {
            lbCommandName.Text = CommandName.ToString();


            m_specification specification = new m_specification();

            ddlSpecification.Items.Clear();
            ddlSpecification.DataSource = specification.SelectAll();
            ddlSpecification.DataBind();
            ddlSpecification.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    ddlSpecification.Enabled = true;
                    txtPrefix.Enabled = true;
                    txtName.Enabled = true;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;

                    break;
                case CommandNameEnum.Edit:
                    fillinScreen();

                    ddlSpecification.Enabled = true;
                    txtPrefix.Enabled = true;
                    txtName.Enabled = true;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.View:
                    fillinScreen();

                    ddlSpecification.Enabled = false;
                    txtPrefix.Enabled = false;
                    txtName.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;

                    break;
            }
        }
        private void fillinScreen()
        {
                m_type_of_test type_of_test = new m_type_of_test().SelectByID(this.PKID);
                ddlSpecification.SelectedValue = type_of_test.specification_id.ToString();
                txtPrefix.Text = type_of_test.prefix;
                txtName.Text = type_of_test.name;

                //ddlTypeOfTest.Items.Clear();
                //ddlTypeOfTest.DataSource = type_of_test.SelectParent(type_of_test.specification_id);
                //ddlTypeOfTest.DataBind();
                //ddlTypeOfTest.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));

                //ddlTypeOfTest.SelectedValue = type_of_test.parent.ToString();
                txtDataGroup.Text = type_of_test.data_group;
          
        }
        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "PKID");
            Session.Remove(GetType().Name + Constants.PREVIOUS_PATH);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SearchTypeOfTest prvPage = Page.PreviousPage as SearchTypeOfTest;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_TYPE_OF_TEST;

            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (CommandName)
            {
                case CommandNameEnum.Add:
                    obj.Insert();
                    break;
                case CommandNameEnum.Edit:
                    obj.Update();
                    break;
            }
            //Commit
            GeneralManager.Commit();
            removeSession();
            Response.Redirect(PreviousPath);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            removeSession();
            Response.Redirect(PreviousPath);
        }

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {

                //ddlTypeOfTest.Items.Clear();
                //ddlTypeOfTest.DataSource = new m_type_of_test().SelectParent(String.IsNullOrEmpty(ddlSpecification.SelectedValue) ? 0 : Convert.ToInt32(ddlSpecification.SelectedValue));
                //ddlTypeOfTest.DataBind();
                //ddlTypeOfTest.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, ""));
            
        }
    }
}