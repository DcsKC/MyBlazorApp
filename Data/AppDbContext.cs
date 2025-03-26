using Microsoft.EntityFrameworkCore;
using MyBlazorApp.Models;

namespace MyBlazorApp.Data
{
    
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            public DbSet<Employee> Employees { get; set; }
            public DbSet<TimeEntry> TimeEntries { get; set; }
        }
    
}
