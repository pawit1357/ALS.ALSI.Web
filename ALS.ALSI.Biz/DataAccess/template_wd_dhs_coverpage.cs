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
    
    public partial class template_wd_dhs_coverpage
    {
        public int ID { get; set; }
        public Nullable<int> sample_id { get; set; }
        public Nullable<int> detail_spec_id { get; set; }
        public Nullable<int> component_id { get; set; }
        public string analytes { get; set; }
        public string specification_limits { get; set; }
        public string result { get; set; }
        public string result_pass_or_false { get; set; }
        public Nullable<int> row_type { get; set; }
        public string pm_procedure_no { get; set; }
        public string pm_number_of_pieces_used_for_extraction { get; set; }
        public string pm_extraction_medium { get; set; }
        public string pm_extraction_volume { get; set; }
        public string sample_size { get; set; }
        public Nullable<int> unit { get; set; }
    }
}
