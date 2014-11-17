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
    
    public partial class EventHandlingDataModelContainer : DbContext
    {
        public EventHandlingDataModelContainer()
            : base("name=EventHandlingDataModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Taxonomy> Taxonomies { get; set; }
        public DbSet<TermSet> TermSets { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<WebPage> WebPages { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Association> Associations { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Navigation> Navigations { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<ContentLayout> ContentLayouts { get; set; }
    }
}
