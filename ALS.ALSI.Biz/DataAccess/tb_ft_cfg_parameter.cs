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
    
    public partial class tb_ft_cfg_parameter
    {
        public int id { get; set; }
        public string template_id { get; set; }
        public string param_group { get; set; }
        public Nullable<int> param_seq { get; set; }
        public string param_name { get; set; }
        public string param_value { get; set; }
        public string is_formular { get; set; }
        public string formular_sheet { get; set; }
        public string status { get; set; }
        public string created_by { get; set; }
        public System.DateTime created_date { get; set; }
    }
}
