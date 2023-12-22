using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Repository
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(ApplicationDbContext db) : base(db)
        {
        }

        public DbSet<Region> dbSet => _dbSet;
    }
}
