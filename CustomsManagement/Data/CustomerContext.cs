using CustomersManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Data
{
    public class CustomerContext:DbContext
    {
        public CustomerContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Users>().HasAlternateKey(u =>new { u.Email });
            modelBuilder.Entity<Customer>().HasAlternateKey(c => new { c.Email,c.Zip });
            //modelBuilder.Entity<Order>().HasKey(o => new {o.Id
              //  ,o.CustomerId,o.ProductId });
            //modelBuilder.Entity<Order>()
            //.Property(o => o.Id)
            //.ValueGeneratedOnAdd();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
