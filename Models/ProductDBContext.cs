using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

// ProductDBContext is derived from DbContext and it exposes DbSet properties of entities

namespace VismaOfficeProduct.Models
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<RebateVolume> RebateVolumes { get; set; }
        public DbSet<RebateDate> RebateDates { get; set; }
        public DbSet<RebateDeal> RebateDeals { get; set; }
        public DbSet<Rebate> Rebates { get; set; }
    }
}