using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ALS.ALSI.Web
{
    public partial class Main : System.Web.UI.MasterPage
    {

        #region "Property"
        protected String notificationList
        {
            get { return (String)Session[GetType().Name + "notificationList"]; }
            set { Session[GetType().Name + "notificationList"] = value; }
        }
        public int PKID { get; set; }
        #endregion


        public users_login userLogin
        {
            get
            {
                return ((Session[Constants.SESSION_USER] != null) ? (users_login)Session[Constants.SESSION_USER] : null);
            }
        }

        private void removeSession()
        {
            Session.Remove(GetType().Name + "notificationList");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserUtils.isLogin())
            {
                Response.Redirect(Constants.LINK_LOGIN);
            }
            //Page.Header.DataBind();
            if (!Page.IsPostBack)
            {
                if (userLogin != null)
                {
                    //Clear old Notification
                    removeSession();

                    RoleEnum roleEnum = (RoleEnum)Enum.Parse(typeof(RoleEnum), userLogin.role_id.ToString());

                    menu_role menuRoleBiz = new menu_role();
                    MenuBiz menuBiz = new MenuBiz();

                    //Generate Navigator
                    litNavigator.Text = menuBiz.getNavigator(Request.PhysicalPath);
                    litMenu.Text = menuBiz.getMenuByRole(menuRoleBiz.getMenuByRole(userLogin.role_id),Request.PhysicalPath);
                    litUserData.Text = String.Format("Login by ::  {0} {1} ( {2} )", userLogin.first_name, userLogin.last_name, roleEnum.ToString());

                  
                    //Generate Alert
                    //renderAlert();
                }
            }
        }

        private void renderAlert()
        {
            String htmlNotification =
                    "<li><a href=\"javascript:;\" onclick=\"App.onNotificationClick(1)\">" +
                    "<span class=\"label label-success\"><i class=\"icon-plus\"></i></span>" +
                    "{0}.{1}-{2} <span class=\"time\">{3}</span></a></li>";

            /*
                    < li class="dropdown dropdown-extended dropdown-notification" id="header_notification_bar">
                        <a href = "javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                            <i class="icon-bell"></i>
                            <span class="badge badge-default">7 </span>
                        </a>
                        <ul class="dropdown-menu">
                            <li class="external">
                                <h3>
                                    <span class="bold">12 pending</span> notifications</h3>
                                <a href = "page_user_profile_1.html" > view all</a>
                            </li>
                            <li>
                                <ul class="dropdown-menu-list scroller" style="height: 250px;" data-handle-color="#637283">
                                    <li>
                                        <a href = "javascript:;" >
                                            < span class="time">just now</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-success">
                                                    <i class="fa fa-plus"></i>
                                                </span>New user registered. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href = "javascript:;" >
                                            < span class="time">3 mins</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-danger">
                                                    <i class="fa fa-bolt"></i>
                                                </span>Server #12 overloaded. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href = "javascript:;" >
                                            < span class="time">10 mins</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-warning">
                                                    <i class="fa fa-bell-o"></i>
                                                </span>Server #2 not responding. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href = "javascript:;" >
                                            < span class="time">14 hrs</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-info">
                                                    <i class="fa fa-bullhorn"></i>
                                                </span>Application error. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href = "javascript:;" >
                                            < span class="time">2 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-danger">
                                                    <i class="fa fa-bolt"></i>
                                                </span>Database overloaded 68%. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href = "javascript:;" >
                                            < span class="time">3 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-danger">
                                                    <i class="fa fa-bolt"></i>
                                                </span>A user IP blocked. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href = "javascript:;" >
                                            < span class="time">4 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-warning">
                                                    <i class="fa fa-bell-o"></i>
                                                </span>Storage Server #4 not responding dfdfdfd. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href = "javascript:;" >
                                            < span class="time">5 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-info">
                                                    <i class="fa fa-bullhorn"></i>
                                                </span>System Error. </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href = "javascript:;" >
                                            < span class="time">9 days</span>
                                            <span class="details">
                                                <span class="label label-sm label-icon label-danger">
                                                    <i class="fa fa-bolt"></i>
                                                </span>Storage server failed. </span>
                                        </a>
                                    </li>
                                </ul>
                            </li>
                        </u>
                        */



        //int countBadge = 0;
        //List<job_sample_logs> logs = new job_sample_logs().SelectNotification();

        //m_status mStatus = new m_status();
        //m_type_of_test mTypeOfTest = new m_type_of_test();
        //job_sample jobSample = new job_sample();
        //foreach (job_sample_logs log in logs)
        //{
        //Show only 5 Row
        //if (countBadge < 5)
        //{

        //    //2. Show by responsible.
        //    if (userLogin.responsible_test != null)
        //    {
        //        jobSample = jobSample.SelectByID((int)log.job_sample_id);
        //        if (jobSample != null)
        //        {
        //            mTypeOfTest = mTypeOfTest.SelectByID(jobSample.type_of_test_id);
        //            if (mTypeOfTest != null)
        //            {
        //                if (userLogin.responsible_test.StartsWith(mTypeOfTest.name))
        //                {
        //                        mStatus = mStatus.SelectByID(log.role_id);
        //                        if (mStatus != null)
        //                        {
        //                            notificationList += String.Format(htmlNotification, (countBadge + 1), log.m_status.name, (String.IsNullOrEmpty(log.job_remark) ? String.Empty : log.job_remark), Convert.ToDateTime(log.date).ToString("MM/dd/yyyy"));
        //                            countBadge++;
        //                        }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //1. Show By Role if not having responsible.
        //        //mStatus = mStatus.SelectByID(log.role_id);
        //        //if (mStatus != null)
        //        //{
        //        //    if (userLogin.role_id == mStatus.ID)
        //        //    {
        //        //        notificationList += String.Format(htmlNotification, (countBadge + 1), log.m_status.name, (String.IsNullOrEmpty(log.job_remark) ? String.Empty : log.job_remark), Convert.ToDateTime(log.date).ToString("MM/dd/yyyy"));
        //        //        countBadge++;
        //        //    }
        //        //}
        //    }
        //}
        //}

        //lbCountBadge.Text = countBadge.ToString();
        //lbCountBadge_1.Text = lbCountBadge.Text;

    }
    }
}