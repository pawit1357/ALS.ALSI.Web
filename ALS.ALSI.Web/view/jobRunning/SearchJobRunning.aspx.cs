using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.jobRunning
{
    public partial class SearchJobRunning : System.Web.UI.Page
    {

        #region "Property"

        public CommandNameEnum CommandName
        {
            get { return (CommandNameEnum)ViewState[Constants.COMMAND_NAME]; }
            set { ViewState[Constants.COMMAND_NAME] = value; }
        }

        public List<job_running> jobrunnings
        {
            get { return (List<job_running>)Session[GetType().Name + "jobrunnings"]; }
            set { Session[GetType().Name + "jobrunnings"] = value; }
        }

        public int PKID { get; set; }

        #endregion

        #region "Method"
        private void initialPage()
        {
            bindingData();
        }

        private void bindingData()
        {
            this.jobrunnings = (List<job_running>)new job_running().SearchData();
            gvResult.DataSource = this.jobrunnings;
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
            Session.Remove(GetType().Name + "jobrunnings");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initialPage();
            }
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            bindingData();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lbTotalRecords.Text = string.Empty;
            removeSession();
            bindingData();
        }

        protected void gvResult_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvResult.EditIndex = e.NewEditIndex;
            gvResult.DataSource = this.jobrunnings;
            gvResult.DataBind();
        }

        protected void gvResult_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int _id = Convert.ToInt32(gvResult.DataKeys[e.RowIndex].Values[0].ToString());
            TextBox _txtRunningNumber = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtRunningNumber");
            if (_txtRunningNumber != null)
            {

                job_running _tmp = this.jobrunnings.Find(x => x.ID == _id);
                if (_tmp != null)
                {
                    _tmp.running_number = Convert.ToInt32(_txtRunningNumber.Text);
                }
                _tmp.Update();
                //Commit
                GeneralManager.Commit();
            }

            gvResult.EditIndex = -1;
            gvResult.DataSource = this.jobrunnings;
            bindingData();
        }

        protected void gvResult_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvResult.EditIndex = -1;
            gvResult.DataSource = this.jobrunnings;
            gvResult.DataBind();
        }
    }
}