using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.soCode
{
    public partial class SoCode : System.Web.UI.Page
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

        public job_sample_group_so_ignore_code obj
        {
            get
            {
                job_sample_group_so_ignore_code tmp = new job_sample_group_so_ignore_code();
                tmp.id = PKID;
                tmp.name = txtName.Text;
                tmp.code = txtCode.Text;
                tmp.isActive = "A";

                return tmp;
            }
        }

        private void initialPage()
        {
            lbCommandName.Text = CommandName.ToString();

            switch (CommandName)
            {
                case CommandNameEnum.Add:

                    txtName.Enabled = true;
                    txtCode.Enabled = true;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;

                    break;
                case CommandNameEnum.Edit:
                    fillinScreen();

                    txtName.Enabled = true;
                    txtCode.Enabled = true;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.View:
                    fillinScreen();


                    txtName.Enabled = false;
                    txtCode.Enabled = false;

                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;

                    break;
            }
        }
        private void fillinScreen()
        {
            job_sample_group_so_ignore_code code = new job_sample_group_so_ignore_code().SelectByID(this.PKID);
            txtName.Text = code.name;
            txtCode.Text = code.code;


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
            SearchSoCode prvPage = Page.PreviousPage as SearchSoCode;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_SO_CODE;

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


    }
}