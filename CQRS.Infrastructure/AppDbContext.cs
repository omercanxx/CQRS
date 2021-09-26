using CQRS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CQRS.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
        public DbSet<Course> Courses { get; set; }
    }
}
