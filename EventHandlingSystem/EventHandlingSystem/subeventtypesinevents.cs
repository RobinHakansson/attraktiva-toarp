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
    
    public partial class subeventtypesinevents
    {
        public int Id { get; set; }
        public int SubEventType_Id { get; set; }
        public int Events_Id { get; set; }
    
        public virtual events events { get; set; }
        public virtual subeventtypes subeventtypes { get; set; }
    }
}
