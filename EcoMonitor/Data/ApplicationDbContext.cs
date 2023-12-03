using EcoMonitor.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Data
{
    public class ApplicationDbContext : IdentityDbContext { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EnvFactor> env_Factors { get; set; }
        public DbSet<Passport> passports { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<RfcFactor> rfc_Factors { get; set; }
        public DbSet<User> users { get; set; }
      

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=?;database=?;uid=?;pwd=?;");
        //}
    }
}
