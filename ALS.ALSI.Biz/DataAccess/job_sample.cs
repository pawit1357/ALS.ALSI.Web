//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ALS.ALSI.Biz.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class job_sample
    {
        public job_sample()
        {
            this.template_wd_ir_coverpage = new HashSet<template_wd_ir_coverpage>();
        }
    
        public int ID { get; set; }
        public int job_id { get; set; }
        public int specification_id { get; set; }
        public int type_of_test_id { get; set; }
        public int template_id { get; set; }
        public string job_number { get; set; }
        public string description { get; set; }
        public string model { get; set; }
        public string surface_area { get; set; }
        public string remarks { get; set; }
        public string sample_po { get; set; }
        public string sample_invoice { get; set; }
        public Nullable<System.DateTime> sample_invoice_date { get; set; }
        public Nullable<double> sample_invoice_amount { get; set; }
        public Nullable<int> no_of_report { get; set; }
        public string uncertainty { get; set; }
        public Nullable<int> job_status { get; set; }
        public Nullable<int> job_role { get; set; }
        public string path_word { get; set; }
        public string path_pdf { get; set; }
        public Nullable<int> status_completion_scheduled { get; set; }
        public Nullable<int> step1owner { get; set; }
        public Nullable<int> step2owner { get; set; }
        public Nullable<int> step3owner { get; set; }
        public Nullable<int> step4owner { get; set; }
        public Nullable<int> step5owner { get; set; }
        public Nullable<int> step6owner { get; set; }
        public Nullable<int> step7owner { get; set; }
        public string internal_reference_remark { get; set; }
        public Nullable<System.DateTime> due_date { get; set; }
        public Nullable<System.DateTime> due_date_customer { get; set; }
        public Nullable<System.DateTime> due_date_lab { get; set; }
        public Nullable<System.DateTime> date_login_inprogress { get; set; }
        public Nullable<System.DateTime> date_login_complete { get; set; }
        public Nullable<System.DateTime> date_chemist_analyze { get; set; }
        public Nullable<System.DateTime> date_chemist_complete { get; set; }
        public Nullable<System.DateTime> date_srchemist_analyze { get; set; }
        public Nullable<System.DateTime> date_srchemist_complate { get; set; }
        public Nullable<System.DateTime> date_admin_word_inprogress { get; set; }
        public Nullable<System.DateTime> date_admin_word_complete { get; set; }
        public Nullable<System.DateTime> date_labman_analyze { get; set; }
        public Nullable<System.DateTime> date_labman_complete { get; set; }
        public Nullable<System.DateTime> date_admin_sent_to_cus { get; set; }
        public Nullable<System.DateTime> date_admin_pdf_inprogress { get; set; }
        public Nullable<System.DateTime> date_admin_pdf_complete { get; set; }
        public Nullable<System.DateTime> sr_approve_date { get; set; }
        public string ad_hoc_tempalte_path { get; set; }
        public string is_no_spec { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
        public string is_hold { get; set; }
        public Nullable<int> amend_count { get; set; }
        public Nullable<int> retest_count { get; set; }
        public Nullable<int> update_by { get; set; }
        public string part_no { get; set; }
        public string part_name { get; set; }
        public string lot_no { get; set; }
        public string other_ref_no { get; set; }
        public string singapore_ref_no { get; set; }
        public Nullable<sbyte> group_submit { get; set; }
        public string sample_prefix { get; set; }
        public string amend_or_retest { get; set; }
        public Nullable<int> last_status { get; set; }
        public string note { get; set; }
        public string note_lab { get; set; }
        public string am_retest_remark { get; set; }
        public Nullable<int> sample_invoice_status { get; set; }
        public Nullable<System.DateTime> sample_invoice_complete_date { get; set; }
        public string sample_so { get; set; }
    
        public virtual ICollection<template_wd_ir_coverpage> template_wd_ir_coverpage { get; set; }
    }
}
