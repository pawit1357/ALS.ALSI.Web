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
        public Nullable<System.DateTime> date_login_received_sample { get; set; }
        public Nullable<System.DateTime> date_chemist_alalyze { get; set; }
        public Nullable<System.DateTime> date_chemist_complete { get; set; }
        public Nullable<System.DateTime> date_srchemist_complate { get; set; }
        public Nullable<System.DateTime> date_admin_sent_to_cus { get; set; }
        public Nullable<System.DateTime> sr_approve_date { get; set; }
        public Nullable<System.DateTime> date_labman_complete { get; set; }
    
        public virtual ICollection<template_wd_ir_coverpage> template_wd_ir_coverpage { get; set; }
    }
}
