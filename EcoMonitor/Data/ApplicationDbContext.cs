using EcoMonitor.Model;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Data
{
    public class ApplicationDbContext : DbContext { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EnvFactor> env_Factors { get; set; }
        public DbSet<Passport> passports { get; set; }
        public DbSet<Company> companies { get; set; }
      

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=?;database=?;uid=?;pwd=?;");
        //}
    }
}
