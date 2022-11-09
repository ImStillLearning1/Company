using ApplicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationAPI.DbContexts
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
