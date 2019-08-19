using System;

namespace ALS.ALSI.Biz
{
    public class SearchResult
    {
        public int ID { get; set; }
        public string other_ref_no { get; set; }
        public DateTime? date_srchemist_complate { get; set; }
        public DateTime? date_admin_sent_to_cus { get; set; }
        public DateTime? receive_date { get; set; }
        public DateTime? due_date { get; set; }
        public DateTime? due_date_customer { get; set; }
        public DateTime? due_date_lab { get; set; }
        public string job_number { get; set; }
        public string customer_ref_no { get; set; }
        public string s_pore_ref_no { get; set; }
        public string customer { get; set; }
        public string sample_invoice { get; set; }
        public string sample_so { get; set; }
        public DateTime? sample_invoice_date { get; set; }
        public double? sample_invoice_amount { get; set; }
        public string sample_po { get; set; }
        public string contract_person { get; set; }
        public string description { get; set; }
        public string model { get; set; }
        public string surface_area { get; set; }
        public string specification { get; set; }
        public string type_of_test { get; set; }
        public int customer_id { get; set; }
        public int? job_status { get; set; }
        public DateTime? create_date { get; set; }
        public int sn { get; set; }
        public string remarks { get; set; }
        public int contract_person_id { get; set; }
        public int? job_role { get; set; }
        public int? status_completion_scheduled { get; set; }
        public int? step1owner { get; set; }
        public int? step2owner { get; set; }
        public int? step3owner { get; set; }
        public int? step4owner { get; set; }
        public int? step5owner { get; set; }
        public int? step6owner { get; set; }
        public int? job_prefix { get; set; }
        public string data_group { get; set; }
        public int? type_of_test_id { get; set; }
        public string type_of_test_name { get; set; }
        public int? spec_id { get; set; }
        public DateTime? date_login_inprogress { get; set; }
        public DateTime? date_chemist_analyze { get; set; }
        public DateTime? date_labman_complete { get; set; }
        public string is_hold { get; set; }
        public int? amend_count { get; set; }
        public int? retest_count { get; set; }
        public sbyte? group_submit { get; set; }
        public string status_name { get; set; }
        public string sample_prefix { get; set; }
        public string amend_or_retest { get; set; }
        public string note { get; set; }
        public string note_lab { get; set; }
        public string am_retest_remark { get; set; }
        public int? sample_invoice_status { get; set; }
        public int fisicalY { get; set; }
        public string sample_invoice_package { get; set; }
    }
}
