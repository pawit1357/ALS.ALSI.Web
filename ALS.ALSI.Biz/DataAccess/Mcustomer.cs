using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices;
using MoreLinq;

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
            Hashtable ht = new Hashtable();
            List<m_customer> listOfCuss = new List<m_customer>();
            foreach(m_customer item in _repository.GetAll())
            {
                if (!ht.ContainsKey(item.company_name))
                {
                    listOfCuss.Add(item);
                    ht.Add(item.company_name, item.company_name);
                }
            }
            return listOfCuss;
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


        public IEnumerable SearchData([Optional] string sortDirection, [Optional] string sortExpression)
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {
                IEnumerable dataList = null;

                var result = from j in ctx.m_customer
                             join a in ctx.m_customer_address on j.ID equals a.company_id 
                             into dep from dept in dep.DefaultIfEmpty() orderby j.company_name
                             select new
                             {
                                 j.ID,
                                 j.customer_code,
                                 j.company_name,
                                 j.code,
                                 dept.address
                             };



                


                if (!string.IsNullOrEmpty(this.customer_code))
                {
                    result = result.Where(x => x.customer_code.Contains(this.customer_code));
                }
                if (!string.IsNullOrEmpty(this.company_name))
                {
                    result = result.Where(x => x.company_name.Contains(this.company_name));
                }

                if (sortDirection != null)
                {
                    switch (sortDirection)
                    {
                        case "ASC":
                            dataList = result.ToList().OrderBy(x => x.GetType().GetProperty(sortExpression).GetValue(x, null)).ToList();
                            break;
                        case "DESC":
                            dataList = result.ToList().OrderByDescending(x => x.GetType().GetProperty(sortExpression).GetValue(x, null)).ToList();
                            break;

                    }
                }
                else
                {
                    dataList = result.ToList();
                }

                return dataList;
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

    public class CustomerAndAddress
    {
        public int ID { get; set; }
        public string customer_code { get; set; }
        public string company_name { get; set; }
        public string code { get; set; }
        public string address { get; set; }
    }
}
