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
    
    public partial class tb_ft_sample_mp
    {
        public int id { get; set; }
        public int sample_id { get; set; }
        public int column_seq { get; set; }
        public int column_index { get; set; }
        public string column_value { get; set; }
        public string column_display_flag { get; set; }
        public string status { get; set; }
        public System.DateTime created_date { get; set; }
        public string created_by { get; set; }
        public System.DateTime updated_date { get; set; }
        public string updated_by { get; set; }
    }
}