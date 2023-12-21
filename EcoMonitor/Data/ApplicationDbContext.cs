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

        public DbSet<Pollution> pollutions { get; set; }
        public DbSet<Passport> passports { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Pollutant> pollutants { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<TaxNorm> tax_norms { get; set; }
        public DbSet<CompanyWaste> company_wastes { get; set; }

        public DbSet<FormattedNews> formatted_news { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // Either
            model.ApplyConfiguration(new UserConfiguration());
            // Or
            model.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // view for news

            model.Entity<FormattedNews>()
                        .ToView(nameof(formatted_news)).HasKey(n => n.id);

            base.OnModelCreating(model);
        }
    }
}
