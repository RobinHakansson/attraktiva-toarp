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
    
    public partial class Community
    {
        public Community()
        {
            this.Association = new HashSet<Association>();
        }
    
        public int Id { get; set; }
        public int PublishingTermSetId { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual WebPage WebPage { get; set; }
        public virtual ICollection<Association> Association { get; set; }
    }
}
