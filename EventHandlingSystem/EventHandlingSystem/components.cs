//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventHandlingSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class components
    {
        public components()
        {
            this.filterdata = new HashSet<filterdata>();
        }
    
        public int Id { get; set; }
        public string FileName { get; set; }
        public Nullable<int> Row { get; set; }
        public Nullable<int> Column { get; set; }
        public bool IsDeleted { get; set; }
        public int Webpages_Id { get; set; }
    
        public virtual webpages webpages { get; set; }
        public virtual ICollection<filterdata> filterdata { get; set; }
    }
}
