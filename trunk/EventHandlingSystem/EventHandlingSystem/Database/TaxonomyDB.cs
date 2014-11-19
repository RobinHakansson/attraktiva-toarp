﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHandlingSystem.Database
{
    public class TaxonomyDB
    {
        private static readonly EventHandlingDataModelContainer Context = Database.Context;

        private static IEnumerable<Taxonomy> GetAllNotDeletedTaxonomies()
        {
            return Context.Taxonomies.Where(t => !t.IsDeleted);
        }

        public static Taxonomy GetTaxonomyById(int id)
        {
            return GetAllNotDeletedTaxonomies().SingleOrDefault(t => t.Id.Equals(id));
        }

        public static Taxonomy GetPublishingTaxonomy()
        {
            return GetTaxonomyById(1);
        }
        public static Taxonomy GetCategoryTaxonomy()
        {
            return GetTaxonomyById(2);
        }
        public static Taxonomy GetCustomizedTaxonomy()
        {
            return GetTaxonomyById(3);
        } 
    }
}