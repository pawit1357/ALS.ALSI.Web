using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public IEnumerable searchResult
        {
            get { return (IEnumerable)Session[GetType().Name + "SearchRole"]; }
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
            using (var ctx = new ALSIEntities())
            {
                job_sample_logs jsl = new job_sample_logs();
                var result = from l in ctx.job_sample_logs
                             join j in ctx.job_sample on l.job_sample_id equals j.ID
                             orderby l.date descending
                             select new
                             {
                                 l.ID,
                                 j.step1owner,
                                 j.step2owner,
                                 j.step3owner,
                                 j.step4owner,
                                 j.step5owner,
                                 j.step6owner,
                                 j.step7owner,
                                 l.log_title,
                                 l.job_remark,
                                 l.date,
                                 j.job_status,
                                 j.job_number
                             };

                RoleEnum roleEnum = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString());
                switch (roleEnum)
                {
                    case RoleEnum.LOGIN:
                        result = result.Where(x => x.step1owner == userLogin.id || x.step2owner == userLogin.id);
                        result = result.Where(x => x.job_status == 11);// Convert.ToInt16(StatusEnum.LOGIN_SELECT_SPEC));
                        break;
                    case RoleEnum.CHEMIST:
                        result = result.Where(x => x.step3owner == userLogin.id);
                        result = result.Where(x => x.job_status == 12);// Convert.ToInt16(StatusEnum.CHEMIST_TESTING));
                        break;
                    case RoleEnum.SR_CHEMIST:
                        result = result.Where(x => x.step4owner == userLogin.id);
                        result = result.Where(x => x.job_status == 4);// Convert.ToInt16(StatusEnum.SR_CHEMIST_CHECKING));
                        break;
                    case RoleEnum.ADMIN:
                        result = result.Where(x => x.step6owner == userLogin.id || x.step7owner == userLogin.id);
                        //result = result.Where(x => x.job_status == Convert.ToInt16(StatusEnum.ADMIN_CONVERT_PDF)||x.job_status == Convert.ToInt16(StatusEnum.ADMIN_CONVERT_WORD));
                        break;
                    case RoleEnum.LABMANAGER:
                        result = result.Where(x => x.step5owner == userLogin.id);
                        result = result.Where(x => x.job_status == 9);// Convert.ToInt16(StatusEnum.LABMANAGER_CHECKING));
                        break;
                }

                DataTable dt = result.ToDataTable();
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["ID"].ToString());
                    job_sample_logs _jsl = jsl.SelectByID(id);
                    if (_jsl != null)
                    {
                        _jsl.is_active = "1";
                        _jsl.Update();
                    }

                    Console.WriteLine();

                }
                searchResult = result.ToList();
                gvResult.DataSource = searchResult;
                gvResult.DataBind();
                //Commit
                GeneralManager.Commit();

                //if (gvResult.Rows.Count > 0)
                //{
                //    lbTotalRecords.Text = String.Format(Constants.TOTAL_RECORDS, gvResult.Rows.Count);
                //}
                //else
                //{
                //    lbTotalRecords.Text = string.Empty;
                //}
            }
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
            removeSession();
            bindingData();
        }
    }
}