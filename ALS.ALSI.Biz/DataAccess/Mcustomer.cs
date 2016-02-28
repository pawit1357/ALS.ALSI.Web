﻿using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class m_customer
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(m_customer));

        private static IRepository<m_customer> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<m_customer>>(); }
        }

        #region "Property"
        public List<m_customer_contract_person> contractPersonList = new List<m_customer_contract_person>();
        public List<m_customer_address> addressList = new List<m_customer_address>();

        #endregion


        public IEnumerable<m_customer> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public m_customer SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
            if (contractPersonList != null && contractPersonList.Count > 0)
            {
                foreach (m_customer_contract_person person in contractPersonList)
                {
                    person.company_id = this.ID;
                    switch (person.RowState)
                    {
                        case CommandNameEnum.Add:
                            person.Insert();
                            break;
                        case CommandNameEnum.Edit:
                            person.Update();
                            break;
                        case CommandNameEnum.Delete:
                            person.Delete();
                            break;
                    }
                }
            }
            if (addressList != null && addressList.Count > 0)
            {
                foreach (m_customer_address address in addressList)
                {
                    address.company_id = this.ID;
                    switch (address.RowState)
                    {
                        case CommandNameEnum.Add:
                            address.Insert();
                            break;
                        case CommandNameEnum.Edit:
                            address.Update();
                            break;
                        case CommandNameEnum.Delete:
                            address.Delete();
                            break;
                    }
                }
            }

        }

        public void Update()
        {
            m_customer existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);

            if (contractPersonList != null && contractPersonList.Count > 0)
            {
                foreach (m_customer_contract_person person in contractPersonList)
                {
                    person.company_id = this.ID;
                    switch (person.RowState)
                    {
                        case CommandNameEnum.Add:
                            person.Insert();
                            break;
                        case CommandNameEnum.Edit:
                            person.Update();
                            break;
                        case CommandNameEnum.Delete:
                            person.Delete();
                            break;
                    }
                }
            }
            if (addressList != null && addressList.Count > 0)
            {
                foreach (m_customer_address address in addressList)
                {
                    address.company_id = this.ID;
                    switch (address.RowState)
                    {
                        case CommandNameEnum.Add:
                            address.Insert();
                            break;
                        case CommandNameEnum.Edit:
                            address.Update();
                            break;
                        case CommandNameEnum.Delete:
                            address.Delete();
                            break;
                    }
                }
            }
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"


        public IEnumerable SearchData()
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {
                var result = from j in ctx.m_customer
                             join a in ctx.m_customer_address on j.ID equals a.company_id
                             select new { 
                             j.ID,
                             j.customer_code,
                             j.company_name,
                             j.code,
                             a.address
                             };
                if (!string.IsNullOrEmpty(this.customer_code))
                {
                    result = result.Where(x => x.customer_code.Contains(this.customer_code));
                }
                if (!string.IsNullOrEmpty(this.company_name))
                {
                    result = result.Where(x => x.company_name.Contains(this.company_name));
                }
                return result.ToList();
            }
        }

        #endregion

        ////public void UpdateList()
        ////{
        ////    using (ALSIEntities context = new ALSIEntities())
        ////    {
        ////        var result = from j in context.m_customer where j.ID == this.ID select j;
        ////        m_customer cus = result.FirstOrDefault();
        ////        if (cus != null)
        ////        {
        ////            setEditData(cus);
        ////            var all = from c in context.m_customer_contract_person where c.company_id == this.ID select c;
        ////            List<m_customer_contract_person> lists = all.ToList();
        ////            if (lists != null)
        ////            {
        ////                foreach (m_customer_contract_person xx in all.ToList())
        ////                {
        ////                    context.m_customer_contract_person.Remove(xx);
        ////                }
        ////            }
        ////            foreach (m_customer_contract_person sample in this.listContract)
        ////            {
        ////                sample.company_id = this.ID;
        ////                context.m_customer_contract_person.Add(sample);
        ////            }
        ////            context.SaveChanges();
        ////        }
        ////    }
        ////}

    }
}
