using ALS.ALSI.Biz;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using ALS.ALSI.Utils;
using ALS.ALSI.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Web.view.role
{
    public partial class Role : System.Web.UI.Page
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Role));

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

        public m_role obj
        {
            get
            {
                m_role tmp = new m_role();
                tmp.ID = PKID;
                tmp.name = txtName.Text;
                tmp.status = "A";
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
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;

                    break;
                case CommandNameEnum.Edit:
                    fillinScreen();

                    txtName.Enabled = true;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;


                    break;
                case CommandNameEnum.View:
                    fillinScreen();

                    txtName.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;
                    //
                    
                    break;
            }


            menu_role menuRoleBiz = new menu_role();
            MenuBiz menuBiz = new MenuBiz();

            List<menu_role> _menuRoles = menuRoleBiz.getRoleListByRoleId(PKID);
            menuBiz.getmenuByTree(ref this.tvPermission, _menuRoles);
        }
        private void fillinScreen()
        {
            m_role role = new m_role().SelectByID(this.PKID);
            txtName.Text = role.name;
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
            SearchRole prvPage = Page.PreviousPage as SearchRole;
            this.CommandName = (prvPage == null) ? this.CommandName : prvPage.CommandName;
            this.PKID = (prvPage == null) ? this.PKID : prvPage.PKID;
            this.PreviousPath = Constants.LINK_SEARCH_ROLE;

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
            this.PKID = (this.PKID == 0) ? obj.GetMax() : this.PKID;
            MenuBiz menuBiz = new MenuBiz();
            menu_role menuRoleBiz = new menu_role();
            List<menu_role> _menuRoles = menuRoleBiz.getRoleListByRoleId(PKID);
            List<menu> menus = menuBiz.GetAll().Where(x => x.PREVIOUS_MENU_ID != null).OrderBy(x => x.DISPLAY_ORDER).ToList();
            foreach (menu _menu in menus)
            {
                menu_role menuRole = _menuRoles.Where(x => x.MENU_ID == _menu.MENU_ID).FirstOrDefault();
                if (menuRole != null)
                {
                    menuRole.IS_CREATE = isChecked(menuRole, CommandNameEnum.Add);
                    menuRole.IS_EDIT = isChecked(menuRole, CommandNameEnum.Edit);
                    menuRole.IS_DELETE = isChecked(menuRole, CommandNameEnum.Delete);
                    menuRole.RowState = CommandNameEnum.Edit;
                }
                else
                {
                    menu_role _menuRole = new menu_role();
                    _menuRole.ROLE_ID = this.PKID;
                    _menuRole.MENU_ID = Convert.ToInt32(_menu.MENU_ID);
                    _menuRole.IS_REQUIRED_ACTION = false;
                    _menuRole.IS_CREATE = false;
                    _menuRole.IS_DELETE = false;
                    _menuRole.IS_EDIT = false;
                    _menuRole.UPDATE_BY = "SYSTEM";
                    _menuRole.CREATE_DATE = DateTime.Now;
                    _menuRole.UPDATE_DATE = DateTime.Now;
                    _menuRole.RowState = CommandNameEnum.Add;
                    _menuRoles.Add(_menuRole);
                    menu_role tmp_menuRole = _menuRoles.Where(x => x.MENU_ID == _menu.MENU_ID).FirstOrDefault();
                    tmp_menuRole.IS_CREATE = isChecked(tmp_menuRole, CommandNameEnum.Add);
                    tmp_menuRole.IS_DELETE = isChecked(tmp_menuRole, CommandNameEnum.Edit);
                    tmp_menuRole.IS_EDIT = isChecked(tmp_menuRole, CommandNameEnum.Delete);
                    tmp_menuRole.RowState = CommandNameEnum.Add;
                }
            }
            foreach (menu_role _menuRole in _menuRoles.Where(x => x.IS_CREATE == (bool)false && x.IS_EDIT == (bool)false && x.IS_DELETE == (bool)false))
            {
                _menuRole.RowState = CommandNameEnum.Delete;
            }
            menuRoleBiz.InsertList(_menuRoles);
            GeneralManager.Commit();
            removeSession();
            MessageBox.Show(this, Resources.MSG_SAVE_SUCCESS, PreviousPath);
        }

        public Boolean isChecked(menu_role _menu, CommandNameEnum cmd)
        {
            Boolean isChecked = false;
            foreach (TreeNode tn in tvPermission.CheckedNodes)
            {
                if (Convert.ToInt32(tn.Value) == _menu.MENU_ID && tn.Text.Equals(cmd.ToString()))
                {
                    isChecked = true;
                    break;
                    //Console.WriteLine();
                }

            }
            return isChecked;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            removeSession();
            Response.Redirect(PreviousPath);
        }
    }
}