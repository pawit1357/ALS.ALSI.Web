using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using ALS.ALSI.Biz.DataAccess;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace ALS.ALSI.Biz.DataAccess
{
    public class MenuBiz
    {
        public List<string> nav = new List<string>();

        private static IRepository<menu> repo
        {
            get { return ObjectFactory.GetInstance<IRepository<menu>>(); }
        }
        public List<menu> GetAll()
        {
            using (var ctx = new ALSIEntities())
            {


                IEnumerable<menu> menus = repo.GetAll();
                return menus.ToList();
            }
        }
        public string getMenuByRole(List<int> _menuByRole, string _currentPage)
        {
            StringBuilder result = new StringBuilder();
            using (var ctx = new ALSIEntities())
            {
                _currentPage = Path.GetFileName(_currentPage);

                IEnumerable<menu> menus = repo.GetAll().Where(x => _menuByRole.Contains(x.MENU_ID));
                Console.WriteLine();
                /*
                    <li class="nav-item start ">
                            <a href="javascript:;" class="nav-link nav-toggle">
                                <i class="icon-home"></i>
                                <span class="title">Dashboard</span>
                                <span class="arrow"></span>
                            </a>
                            <ul class="sub-menu">
                                <li class="nav-item start ">
                                    <a href="index.html" class="nav-link ">
                                        <i class="icon-bar-chart"></i>
                                        <span class="title">Dashboard 1</span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                */
                IEnumerable<menu> parentMenus = menus.Where(x => x.PREVIOUS_MENU_ID == null).OrderBy(x => x.DISPLAY_ORDER);
                foreach (menu _parent in parentMenus)
                {
                    IEnumerable<menu> menuChilds = menus.Where(x => x.PREVIOUS_MENU_ID == _parent.MENU_ID).OrderBy(x => x.DISPLAY_ORDER);
                    if (menuChilds != null)
                    {
                        Boolean bActiveSelectedMenu = false;

                        foreach (menu _findCurrentMenu in menuChilds)
                        {
                            if (Path.GetFileName(_findCurrentMenu.URL_NAVIGATE).Equals(_currentPage))
                            {
                                if (_parent.MENU_ID == _findCurrentMenu.PREVIOUS_MENU_ID)
                                {
                                    bActiveSelectedMenu = true;
                                }
                                break;
                            }
                        }
                        Console.WriteLine();
                        /* - BEGIN ADD MAIN MENU -*/
                        result.Append("<li class=\"nav-item " + (bActiveSelectedMenu ? "start active open" : "") + "  \">");
                        result.Append("<a href=\"javascript:;\" class=\"nav-link nav-toggle\">");
                        result.Append("<i class=\"" + _parent.MENU_ICON + "\"></i>");
                        result.Append("<span class=\"title\">" + _parent.MENU_NAME + "</span>");
                        result.Append("<span class=\"arrow\"></span>");
                        result.Append("</a>");
                        /* - BEGIN ADD SUBMENU -*/
                        result.Append("<ul class=\"sub-menu\">");
                        foreach (menu _child in menuChilds)
                        {
                            result.Append("<li class=\"nav-item  \">");
                            result.Append("<a href=\"" + _child.URL_NAVIGATE + "\" class=\"nav-link \">");
                            result.Append("<span class=\"title\">" + _child.MENU_NAME + "</span>");
                            result.Append("</a>");
                            result.Append("</li>");
                        }
                        result.Append("</ul>");
                        /* - END SUBMENU -*/
                        result.Append("</li>");
                        /* - END MAIN MENU -*/

                    }
                }
            }

            return result.ToString();
        }

        public void getmenuByTree(ref TreeView tv, List<menu_role> roles)
        {
            using (var ctx = new ALSIEntities())
            {

                IEnumerable<menu> menus = repo.GetAll();

                foreach (menu _menu in menus.Where(x => x.PREVIOUS_MENU_ID == null).OrderBy(x => x.DISPLAY_ORDER))
                {
                    TreeNode root = new TreeNode(_menu.MENU_NAME, _menu.MENU_ID.ToString());
                    IEnumerable<menu> menuChilds = menus.Where(x => x.PREVIOUS_MENU_ID == _menu.MENU_ID).OrderBy(x => x.DISPLAY_ORDER);
                    if (menuChilds != null)
                    {
                        foreach (menu _childmenu in menuChilds)
                        {
                            TreeNode child = new TreeNode(_childmenu.MENU_NAME, _childmenu.MENU_ID.ToString());
                            child.ShowCheckBox = true;
                            menu_role menuRole = roles.Where(x => x.MENU_ID == _childmenu.MENU_ID).FirstOrDefault();
                            if (menuRole != null)
                            {
                                child.Expanded = !((bool)menuRole.IS_CREATE && (bool)menuRole.IS_DELETE && (bool)menuRole.IS_EDIT);
                                child.Checked = true;
                                foreach (MenuRoleActionEnum val in Enum.GetValues(typeof(MenuRoleActionEnum)))
                                {
                                    TreeNode childLevel1 = new TreeNode(val.ToString(), menuRole.MENU_ID.ToString());
                                    childLevel1.ShowCheckBox = true;
                                    switch (val)
                                    {
                                        case MenuRoleActionEnum.Add:
                                            childLevel1.Checked = (bool)menuRole.IS_CREATE;
                                            break;
                                        case MenuRoleActionEnum.Delete:
                                            childLevel1.Checked = (bool)menuRole.IS_DELETE;
                                            break;
                                        case MenuRoleActionEnum.Edit:
                                            childLevel1.Checked = (bool)menuRole.IS_EDIT;
                                            break;
                                    }
                                    child.ChildNodes.Add(childLevel1);
                                }
                            }
                            else
                            {
                                //

                                foreach (MenuRoleActionEnum val in Enum.GetValues(typeof(MenuRoleActionEnum)))
                                {
                                    TreeNode childLevel1 = new TreeNode(val.ToString(), _childmenu.MENU_ID.ToString());
                                    childLevel1.ShowCheckBox = true;
                                    child.ChildNodes.Add(childLevel1);
                                }
                            }
                            root.ChildNodes.Add(child);
                        }
                        tv.Nodes.Add(root);
                    }
                }
                Console.WriteLine("");
            }
        }

        public string getNavigator(string _currentPage)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append("<i class=\"fa fa-home\"></i>");
            sb.Append("<a href=\"" + Constants.LINK_SEARCH_JOB_REQUEST + "\">Home</a>");
            sb.Append("</li>");
            using (var ctx = new ALSIEntities())
            {
                _currentPage = Path.GetFileName(_currentPage);
                menu child = repo.GetAll().Where(x => Path.GetFileName(x.URL_NAVIGATE) == _currentPage).FirstOrDefault();
                if (child != null)
                {
                    menu parent = repo.GetAll().Where(x => x.MENU_ID == child.PREVIOUS_MENU_ID).FirstOrDefault();
                    if (parent != null)
                    {


                        sb.Append("<li>");
                        sb.Append("<i class=\"fa fa-angle-right\"></i>");
                        sb.Append("<a href=\"#\">" + parent.MENU_NAME + "</a>");
                        sb.Append("</li>");
                        nav.Add(parent.MENU_NAME);
                    }
                    sb.Append("<li>");
                    sb.Append("<i class=\"fa fa-angle-right\"></i>");
                    sb.Append("<a href=\"#\">" + child.MENU_NAME + "</a>");
                    sb.Append("</li>");
                    nav.Add(child.MENU_NAME);
                }
            }
            return sb.ToString();

        }

    }
}
