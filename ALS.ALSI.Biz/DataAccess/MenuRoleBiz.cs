using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class menu_role
    {
        public CommandNameEnum RowState { get; set; }

        private static IRepository<menu_role> repo
        {
            get { return ObjectFactory.GetInstance<IRepository<menu_role>>(); }
        }

        public IEnumerable<menu_role> SelectAll()
        {

            return repo.GetAll().ToList();
        }

        public menu_role SelectByID(int _menu_id, int _role_id)
        {
            return repo.First(x => x.MENU_ID == _menu_id && x.ROLE_ID == _role_id);
        }

        public void Insert()
        {
            repo.Add(this);
        }

        public void Update()
        {
            menu_role existing = repo.Find(x => x.ROLE_ID == this.ROLE_ID && x.MENU_ID == this.MENU_ID).FirstOrDefault();
            if (existing != null)
            {
                repo.Edit(existing, this);
            }

        }

        public void Delete()
        {
            menu_role existing = repo.Find(x => x.ROLE_ID == this.ROLE_ID && x.MENU_ID == this.MENU_ID).FirstOrDefault();
            if (existing != null)
            {
                repo.Delete(existing);
            }
        }

        #region "Custom"

        public IEnumerable SearchData()
        {
            return repo.GetAll().ToList();

        }
        public void InsertList(List<menu_role> _lists)
        {
            foreach (menu_role tmp in _lists)
            {
                switch (tmp.RowState)
                {
                    case CommandNameEnum.Add:
                        tmp.Insert();
                        break;
                    case CommandNameEnum.Edit:
                        tmp.Update();
                        break;
                    case CommandNameEnum.Delete:
                        tmp.Delete();
                        break;
                }
            }
        }
        #endregion
        public List<menu_role> GetAll()
        {
            using (var ctx = new ALSIEntities())
            {
                IEnumerable<menu_role> menus = repo.GetAll();
                return menus.ToList();
            }
        }
        //public menu_role getRoleByUserId(int _userId)
        //{
        //    using (var ctx = new ALSIEntities())
        //    {
        //        return (from mr in ctx.menu_role
        //                join ur in ctx.users_role on mr.ROLE_ID equals ur.ROLE_ID
        //                where ur.USER_ID == _userId
        //                select mr).FirstOrDefault();

        //    }
        //}
        public List<menu_role> getRoleListByRoleId(int _roleId)
        {
            using (var ctx = new ALSIEntities())
            {
                return (from mr in ctx.menu_role
                        where mr.ROLE_ID == _roleId
                        select mr).ToList();

            }
        }
        public List<int> getMenuByRole(int _roleId)
        {
            List<int> result = new List<int>();
            using (var ctx = new ALSIEntities())
            {

                IEnumerable<menu_role> menuRoles = repo.GetAll().Where(x => x.ROLE_ID == _roleId);
                foreach (menu_role _menuRole in menuRoles)
                {
                    result.Add(_menuRole.MENU_ID);
                }
            }
            return result;
        }

        public void DeleteByRoleID(int _role_id)
        {
            //using (var ctx = new ALSIEntities())
            //{
            //    ctx.menu_role.de
            //    IEnumerable<menu_role> menus = repo.GetAll();
            //    //return menus.ToList();
            //}

            List<menu_role> menus = repo.GetAll().Where(x => x.ROLE_ID == _role_id).ToList();
            foreach (menu_role menu in menus)
            {
                menu.Delete();
            }
            //return repo.First(x => x.MENU_ID == _menu_id && x.ROLE_ID == _role_id);
        }
    }
}
