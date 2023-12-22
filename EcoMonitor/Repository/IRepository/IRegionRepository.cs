using EcoMonitor.Model;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Repository.IRepository
{
    public interface IRegionRepository : IRepository<Region>
    {
        DbSet<Region> dbSet { get; }
    }
}
