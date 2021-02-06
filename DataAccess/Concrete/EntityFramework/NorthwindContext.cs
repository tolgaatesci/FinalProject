using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context, Db tabloları ile proje class'larını bağlamak
    public class NorthwindContext:DbContext //NuGet ile yüklediğimiz entityframeworkcore.sql aracı sayesinde kullanabiliyoruz.
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //bu metod senin projen hangi veritabanı ile ilişkili olduğunu gösterir
        {
            //@ demek \ kullandığında / okusun diye kullanırız
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13;Database=Northwind;Trusted_Connection=true"); //(localdb)\MSSQLLocalDB kullanabilirsin bir sorun olursa
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
