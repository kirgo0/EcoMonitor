using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcoMonitor.Repository
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<TResult>> selectAsync<TResult>(Expression<Func<Region, TResult>> selector)
        {
            var result = await _dbSet.Select(selector).ToListAsync();
            return result;
        }
    }
}
