using CQRS.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //Yer alan entityler burada DbSet olarak tanımlanmaktadır. Projedeki yapımız classlar tekil, tablolar ise çoğul olmak üzere tasarlanmıştır.
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Campaign> Order_Campaigns { get; set; }
        public DbSet<Order_Product> Order_Products { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User_Product> User_Products { get; set; }
        public DbSet<User_ProductItem> User_ProductItems { get; set; }
    }
}
