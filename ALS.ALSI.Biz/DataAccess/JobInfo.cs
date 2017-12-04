using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class job_info 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(job_info));

        private static IRepository<job_info> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<job_info>>(); }
        }

        #region "Property"

        public String[] responsible_test { get; set; }
        public List<job_sample> jobSample { get; set; }
        public CommandNameEnum RowState { get; set; }
        public int sample_id { get; set; }
        public int status { get; set; }
        public String jobRefNo { get; set; }
        public int spec_id { get; set; }
        public String dataGroup { get; set; }

        public String sample_po { get; set; }
        public String sample_invoice { get; set; }

        public DateTime receive_report_from { get; set; }
        public DateTime receive_report_to { get; set; }

        public DateTime duedate_from { get; set; }
        public DateTime duedate_to { get; set; }

        public DateTime report_to_customer_from { get; set; }
        public DateTime report_to_customer_to { get; set; }

        #endregion


        public IEnumerable<job_info> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public job_info SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
            GeneralManager.Commit();

            job_running.IncrementRunning(this.job_prefix);

            foreach (job_sample sample in this.jobSample)
            {
                sample.job_id = this.ID;

                switch (sample.RowState)
                {
                    case CommandNameEnum.Add:
                        sample.Insert();
                        break;
                    case CommandNameEnum.Edit:
                        sample.Update();
                        break;
                    case CommandNameEnum.Delete:
                        sample.Delete();
                        break;
                }
            }
        }

        public void Update()
        {
            job_info existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
            if(this.jobSample !=null){
                foreach (job_sample sample in this.jobSample)
                {
                    sample.job_id = this.ID;

                    switch (sample.RowState)
                    {
                        case CommandNameEnum.Add:
                            sample.Insert();
                            break;
                        case CommandNameEnum.Edit:
                            sample.Update();
                            break;
                        case CommandNameEnum.Delete:
                            sample.Delete();
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
                //Status 
                //    Received.	 Report x
                //    Sent to Customer  x
                //    Receive Date.	
                //    Due Date.	
                //    ALS Ref 
                //    No.Cus 
                //    Ref 
                //    No.S'Ref No.	
                //    Company	
                //    Invoice	
                //    Po	
                //    Contact	
                //    Description	
                //    Model	
                //    Surface Area	
                //    Specification	
                //    Type of test

                var result = from j in ctx.job_info
                             join s in ctx.job_sample on j.ID equals s.job_id
                             join sp in ctx.m_specification on s.specification_id equals sp.ID
                             join tt in ctx.m_type_of_test on s.type_of_test_id equals tt.ID
                             join c in ctx.m_customer on j.customer_id equals c.ID
                             join cp in ctx.m_customer_contract_person on j.contract_person_id equals cp.ID
                             orderby s.job_number descending
                             select new
                             {
                                 ID = j.ID,
                                 s.date_srchemist_complate,
                                 s.date_admin_sent_to_cus,
                                 receive_date = j.date_of_receive,
                                 due_date = s.due_date,
                                 s.due_date_customer,
                                 s.due_date_lab,
                                 job_number = s.job_number,
                                 customer_ref_no = j.customer_ref_no,
                                 s_pore_ref_no = j.s_pore_ref_no,
                                 customer = c.company_name,
                                 s.sample_invoice,
                                 sample_po = s.sample_po,
                                 contract_person = cp.name,
                                 description = s.description,
                                 model = s.model,
                                 surface_area = s.surface_area,
                                 specification = sp.name,
                                 type_of_test = tt.name,
                                 customer_id = c.ID,
                                 job_status = s.job_status,
                                 create_date = j.create_date,
                                 sn = s.ID,
                                 remarks = s.remarks,
                                 contract_person_id = cp.ID,
                                 job_role = s.job_role,
                                 status_completion_scheduled = s.status_completion_scheduled,
                                 s.step1owner,
                                 s.step2owner,
                                 s.step3owner,
                                 s.step4owner,
                                 s.step5owner,
                                 s.step6owner,
                                 j.job_prefix,
                                 tt.data_group,
                                 type_of_test_id = tt.ID,
                                 type_of_test_name = tt.name,
                                 spec_id = sp.ID,
                                s.date_login_received_sample,
                                s.date_chemist_alalyze,
                                s.date_labman_complete,
                                s.is_hold,
                                s.amend_count,
                                s.retest_count
                             };

                if (this.ID > 0)
                {
                    result = result.Where(x => x.ID == this.ID);
                }
                if (this.sample_id > 0)
                {
                    result = result.Where(x => x.sn == this.sample_id);
                }

                if (this.job_prefix > 0)
                {
                    result = result.Where(x => x.job_prefix == this.job_prefix);
                }

                //if (this.date_of_receive != null && this.date_of_receive !=DateTime.MinValue)
                //{
                //    result = result.Where(x => x.receive_date == this.date_of_receive);
                //}
                if (this.customer_id > 0)
                {
                    result = result.Where(x => x.customer_id == this.customer_id);
                }
                if (this.contract_person_id > 0)
                {
                    result = result.Where(x => x.contract_person_id == this.contract_person_id);
                }

                //show type of test by user.
                if (responsible_test != null && responsible_test.Length > 0)
                {
                    result = result.Where(x => responsible_test.Contains(x.data_group));
                }


                if (this.status > 0)
                {
                    result = result.Where(x => x.job_status == this.status);
                }
                if (!String.IsNullOrEmpty(this.jobRefNo))
                {
                    result = result.Where(x => x.job_number.Contains(this.jobRefNo));

                }
                if (this.customer_id > 0)
                {
                    result = result.Where(x => x.customer_id == this.customer_id);
                }
                if (this.spec_id > 0)
                {
                    result = result.Where(x => x.spec_id == this.spec_id);
                }
                if (!String.IsNullOrEmpty(this.dataGroup))
                {
                    result = result.Where(x => x.data_group.Contains(this.dataGroup));
                }
                if (!String.IsNullOrEmpty(this.sample_po))
                {
                    result = result.Where(x => x.sample_po.Contains(this.sample_po));
                }
                if (!String.IsNullOrEmpty(this.sample_invoice))
                {
                    result = result.Where(x => x.sample_invoice.Contains(this.sample_invoice));
                }

                if (this.receive_report_from != DateTime.MinValue && this.receive_report_to != DateTime.MinValue)
                {
                    result = result.Where(x => x.receive_date >= this.receive_report_from && x.receive_date <= this.receive_report_to);
                }
                if (this.duedate_from != DateTime.MinValue && this.duedate_to != DateTime.MinValue)
                {
                    result = result.Where(x => x.due_date >= this.duedate_from && x.due_date <= this.duedate_to);
                }
                if (this.report_to_customer_from != DateTime.MinValue && this.report_to_customer_to != DateTime.MinValue)
                {
                    result = result.Where(x => x.date_admin_sent_to_cus >= this.report_to_customer_from && x.date_admin_sent_to_cus <= this.report_to_customer_to);
                }

                return result.ToList();
            }
        }


        #endregion

    }
}
