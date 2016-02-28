using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class users_login 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(users_login));

        private static IRepository<users_login> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<users_login>>(); }
        }

        #region "Property"

        #endregion


        public IEnumerable<users_login> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public users_login SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            users_login existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public users_login Login()
        {
            users_login existing = _repository.Find(x => x.username == this.username && x.password == this.password).FirstOrDefault();
            return existing;
        }
        public users_login SelectByEmail(String _email)
        {

            users_login existing = _repository.Find(x => x.email == this.email).FirstOrDefault();
            return existing;

        }
        public IEnumerable SearchData()
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {

                var result = from u in ctx.users_login
                             //join p in ctx.users_login_profile on u.id equals p.users_login_id
                             join r in ctx.m_role on u.role_id equals r.ID
                             select new
                             {
                                 id = u.id,
                                 role = r.name,
                                 username = u.username,
                                 email = u.email,
                                 latest_login = u.latest_login,
                                 create_date = u.create_date,
                                 status = u.status,
                                 phone = u.mobile_phone
                             };
                if (!String.IsNullOrEmpty(this.username))
                {
                    result = result.Where(x => x.username.Contains(this.username));
                }
                if (!String.IsNullOrEmpty(this.email))
                {
                    result = result.Where(x => x.email.Contains(this.email));
                }
                //if (!String.IsNullOrEmpty(this.users_loginProfile.phone))
                //{
                //    result = result.Where(x => x.phone == this.users_loginProfile.phone);
                //}

                return result.ToList();
            }
        }

        #endregion
    }
}
