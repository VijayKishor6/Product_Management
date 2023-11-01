using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Product_Management.Models
{
    public class Users_context:DbContext
    {
        public Users_context():base("dbcon")
        {

        }
        public DbSet<Users>Users { get; set; }
        public DbSet<Product>Product { get; set; }
        public DbSet<ProductWatchLists> ProductWatchLists { get; set; }
        public DbSet<ProductWatchListProducts> ProductWatchListProducts { get; set; }
        public DbSet<ProductWatchListShareLogs> ProductWatchListShareLogs { get; set; }
    }
}