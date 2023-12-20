using EcoMonitor.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EcoMonitor.Data
{
    public class ApplicationDbContext : IdentityDbContext<User> { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EnvFactor> env_Factors { get; set; }
        public DbSet<Passport> passports { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<RfcFactor> rfc_Factors { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<TaxNorm> tax_norms { get; set; }
        public DbSet<CompanyWaste> company_wastes { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=?;database=?;uid=?;pwd=?;");
        //}
        // Methods
        protected override void OnModelCreating(ModelBuilder model)
        {
            // Either
            model.ApplyConfiguration(new UserConfiguration());
            // Or
            model.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(model);
        }
    }
}
