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
    
    public partial class m_status_group
    {
        public m_status_group()
        {
            this.m_status = new HashSet<m_status>();
        }
    
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    
        public virtual ICollection<m_status> m_status { get; set; }
    }
}
