﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class attraktiva_toarpEntities : DbContext
    {
        public attraktiva_toarpEntities()
            : base("name=attraktiva_toarpEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<associations> associations { get; set; }
        public DbSet<associationsinevents> associationsinevents { get; set; }
        public DbSet<associationtypes> associationtypes { get; set; }
        public DbSet<associationtypesinassociation> associationtypesinassociation { get; set; }
        public DbSet<associationtypesinevents> associationtypesinevents { get; set; }
        public DbSet<communities> communities { get; set; }
        public DbSet<communitiesinevents> communitiesinevents { get; set; }
        public DbSet<components> components { get; set; }
        public DbSet<events> events { get; set; }
        public DbSet<eventtypes> eventtypes { get; set; }
        public DbSet<filterdata> filterdata { get; set; }
        public DbSet<members> members { get; set; }
        public DbSet<subeventtypes> subeventtypes { get; set; }
        public DbSet<subeventtypesinevents> subeventtypesinevents { get; set; }
        public DbSet<webpages> webpages { get; set; }
    }
}
