using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.sampleLog
{
    public partial class SearchSampleLog : System.Web.UI.Page
    {

        #region "Property"
        public users_login userLogin
        {
            get { return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null); }
        }

        public List<job_sample_logs> searchResult
        {
            get { return (List<job_sample_logs>)Session[GetType().Name + "SearchRole"]; }
            set { Session[GetType().Name + "SearchRole"] = value; }
        }

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public int PKID { get; set; }

        public job_sample_logs obj
        {
            get
            {
                job_sample_logs tmp = new job_sample_logs();
                tmp.role_id = userLogin.role_id;
                tmp.resposible = userLogin.responsible_test;
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
            searchResult = obj.SelectNotification();


            //Show only 5 Row
            //List<job_sample_logs> tmp = new List<job_sample_logs>();
            //foreach (job_sample_logs log in searchResult)
            //{
            //    if (userLogin.responsible_test != null)
            //    {
            //        if (userLogin.responsible_test.StartsWith(log.job_sample.m_type_of_test.name))
            //        {
            //            tmp.Add(log);
            //        }
            //    }
            //    else
            //    {
            //        //1. Show By Role if not having responsible.
            //        if (userLogin.role_id == log.m_status.m_role.ID)
            //        {
            //            tmp.Add(log);
            //        }
            //    }
            //}

            //searchResult = tmp;
            //gvResult.DataSource = searchResult;
            //gvResult.DataBind();
            //if (gvResult.Rows.Count > 0)
            //{
            //    lbTotalRecords.Text = String.Format(Constants.TOTAL_RECORDS, gvResult.Rows.Count);
            //}
            //else
            //{
            //    lbTotalRecords.Text = string.Empty;
            //}
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name);
            Session.Remove(GetType().Name + "SearchRole");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        //protected void gvResult_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    CommandNameEnum cmd = (CommandNameEnum)Enum.Parse(typeof(CommandNameEnum), e.CommandName, true);
        //    this.CommandName = cmd;
        //    switch (cmd)
        //    {
        //        case CommandNameEnum.Edit:
        //        case CommandNameEnum.View:
        //            this.PKID = int.Parse(e.CommandArgument.ToString().Split(Constants.CHAR_COLON)[0]);
        //            Server.Transfer(Constants.LINK_ROLE);
        //            break;
        //    }
        //}

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.CommandName = CommandNameEnum.Add;
            Server.Transfer(Constants.LINK_ROLE);
        }

        //protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    this.PKID = int.Parse(e.Keys[0].ToString().Split(Constants.CHAR_COLON)[0]);

        //    job_sample_logs cus = new job_sample_logs().SelectByID(this.PKID);
        //    if (cus != null)
        //    {
        //        cus.Delete();
        //        //Commit
        //        GeneralManager.Commit();

        //        bindingData();
        //    }
        //}

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
            //txtId.Text = string.Empty;
            //txtName.Text = string.Empty;
            lbTotalRecords.Text = string.Empty;

            removeSession();
            bindingData();
        }
    }
}