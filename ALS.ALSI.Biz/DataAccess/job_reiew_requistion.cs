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
    
    public partial class job_reiew_requistion
    {
        public int ID { get; set; }
        public Nullable<int> job_id { get; set; }
        public string detail { get; set; }
        public string status { get; set; }
        public Nullable<int> create_by { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public Nullable<int> update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
    
        public virtual job_info job_info { get; set; }
    }
}
