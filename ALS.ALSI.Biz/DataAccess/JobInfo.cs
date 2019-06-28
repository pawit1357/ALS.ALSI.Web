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
        public String customerText { get; set; }
        public String preFixText { get; set; }
        public int physicalYear { get; set; }
        public String section { get; set; }
        public RoleEnum userRole { get; set; }
        public Boolean bNotShowDelete { get; set; }
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
            return _repository.Find(x => x.ID == _id).FirstOrDefault();

        }

        public void Insert(bool genRunning = true)
        {
            _repository.Add(this);
            GeneralManager.Commit();

            if (genRunning)
            {
                job_running.IncrementRunning(this.job_prefix);
            }

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

                        job_sample deleteSample = new job_sample().SelectByID(sample.ID);
                        if (deleteSample != null)
                        {
                            deleteSample.Delete();

                        }

                        break;
                }
            }
        }

        public void Update()
        {
            job_info existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
            if (this.jobSample != null)
            {
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
                            sample.job_status = Convert.ToInt16(StatusEnum.JOB_DELETE);
                            sample.Update();
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
        public IEnumerable<SearchResult> SearchData()
        {
            using (ALSIEntities ctx = new ALSIEntities())
            {

                var result = from j in ctx.job_info
                             join s in ctx.job_sample on j.ID equals s.job_id
                             join ms in ctx.m_status on s.job_status equals ms.ID
                             join sp in ctx.m_specification on s.specification_id equals sp.ID
                             join tt in ctx.m_type_of_test on s.type_of_test_id equals tt.ID
                             join c in ctx.m_customer on j.customer_id equals c.ID
                             join cp in ctx.m_customer_contract_person on j.contract_person_id equals cp.ID
                             orderby s.ID descending
                             select new SearchResult
                             {

                                 ID = j.ID,
                                 other_ref_no = s.other_ref_no,
                                 date_srchemist_complate = s.date_srchemist_complate,
                                 date_admin_sent_to_cus = s.date_admin_sent_to_cus,
                                 receive_date = j.date_of_receive,
                                 due_date = s.due_date,
                                 due_date_customer = s.due_date_customer,
                                 due_date_lab = s.due_date_lab,
                                 job_number = s.job_number,
                                 customer_ref_no = j.customer_ref_no,
                                 s_pore_ref_no = j.s_pore_ref_no,
                                 customer = c.company_name,
                                 sample_invoice = s.sample_invoice,
                                 sample_invoice_date = s.sample_invoice_date,
                                 sample_invoice_amount = s.sample_invoice_amount,
                                 sample_po = s.sample_po,
                                 contract_person = (cp.name == null) ? "" : cp.name,
                                 description = s.description,
                                 model = s.model,
                                 surface_area = s.surface_area,
                                 specification = (sp.name == null) ? "" : sp.name,
                                 type_of_test = (tt.name == null) ? "" : tt.name,
                                 customer_id = c.ID,
                                 job_status = s.job_status,
                                 create_date = j.create_date,
                                 sn = s.ID,
                                 remarks = s.remarks,
                                 contract_person_id = cp.ID,
                                 job_role = s.job_role,
                                 status_completion_scheduled = s.status_completion_scheduled,
                                 step1owner = s.step1owner,
                                 step2owner = s.step2owner,
                                 step3owner = s.step3owner,
                                 step4owner = s.step4owner,
                                 step5owner = s.step5owner,
                                 step6owner = s.step6owner,
                                 job_prefix = j.job_prefix,
                                 data_group = tt.data_group,
                                 type_of_test_id = tt.ID,
                                 type_of_test_name = tt.name,
                                 spec_id = sp.ID,
                                 date_login_inprogress = s.date_login_inprogress,
                                 date_chemist_analyze = s.date_chemist_analyze,
                                 date_labman_complete = s.date_labman_complete,
                                 is_hold = s.is_hold,
                                 amend_count = s.amend_count,
                                 retest_count = s.retest_count,
                                 group_submit = s.group_submit,
                                 status_name = ms.name,
                                 sample_prefix = s.sample_prefix,
                                 amend_or_retest = s.amend_or_retest,
                                 note = s.note,
                                 note_lab = s.note_lab,
                                 am_retest_remark = s.am_retest_remark,
                                 sample_invoice_status = s.sample_invoice_status,
                                 fisicalY = (j.date_of_receive.Value.Month < 4) ? j.date_of_receive.Value.Year - 1 : j.date_of_receive.Value.Year
                             };

                //if (DateTime.Now.Month < Constants.PHYSICAL_YEAR)
                //{
                //    ddlPhysicalYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
                //}
                //else
                //{
                //    ddlPhysicalYear.SelectedValue = (DateTime.Now.Year).ToString();
                //}

                if (!String.IsNullOrEmpty(section))
                {
                    if (section.Equals("NB"))
                    {
                        result = result.Where(x => !x.job_number.EndsWith("B"));
                    }
                    else
                    {
                        result = result.Where(x => x.job_number.EndsWith(section));
                    }
                }
                if (this.physicalYear > 0)
                {
                    result = result.Where(x=>x.fisicalY == this.physicalYear);
                }
                if (this.ID > 0)
                {
                    result = result.Where(x => x.ID == this.ID);
                }
                if (this.sample_id > 0)
                {
                    result = result.Where(x => x.sn == this.sample_id);
                }

                if (!String.IsNullOrEmpty(this.preFixText))
                {
                    result = result.Where(x => x.sample_prefix.Trim().Contains(this.preFixText.Trim()));
                }

                //if (this.date_of_receive != null && this.date_of_receive !=DateTime.MinValue)
                //{
                //    result = result.Where(x => x.receive_date == this.date_of_receive);
                //}
                //if (this.customer_id > 0)
                //{
                //    result = result.Where(x => x.customer_id == this.customer_id);
                //}
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

                if (bNotShowDelete)
                {
                    result = result.Where(x => x.job_status > 0);

                }

                if (!String.IsNullOrEmpty(this.jobRefNo))
                {
                    result = result.Where(x => x.job_number.Contains(this.jobRefNo));

                }
                if (this.customer_id > 0)
                {
                    if (!String.IsNullOrEmpty(this.customerText))
                    {
                        result = result.Where(x => x.customer.Contains(this.customerText));


                    }
                }
                //if (this.customer_id > 0)
                //{
                //    result = result.Where(x => x.customer_id == this.customer_id);
                //}
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
                    //
                    switch (userRole)
                    {
                        case RoleEnum.LOGIN:
                        case RoleEnum.CHEMIST:
                        case RoleEnum.SR_CHEMIST:
                        case RoleEnum.LABMANAGER:
                            result = result.Where(x => x.due_date_lab >= this.duedate_from && x.due_date_lab <= this.duedate_to);
                            break;
                        case RoleEnum.ADMIN:
                        case RoleEnum.MARKETING:
                        case RoleEnum.BUSINESS_MANAGER:
                            result = result.Where(x => x.due_date_customer >= this.duedate_from && x.due_date_customer <= this.duedate_to);
                            break;
                        default:
                            result = result.Where(x => x.due_date_lab >= this.duedate_from && x.due_date_lab <= this.duedate_to);
                            break;
                    }
                }
                if (this.report_to_customer_from != DateTime.MinValue && this.report_to_customer_to != DateTime.MinValue)
                {
                    result = result.Where(x => x.date_admin_sent_to_cus >= this.report_to_customer_from && x.date_admin_sent_to_cus <= this.report_to_customer_to);
                }

                return result.Where(x=>x.job_status != 18).ToList();// JOB_STATUS EQUAL0 EQUAL "JOB_DELETE"
            }
        }


        #endregion

    }
}
