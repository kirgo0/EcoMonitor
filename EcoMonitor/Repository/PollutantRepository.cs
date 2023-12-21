using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Repository
{
    public class PollutantRepository : Repository<Pollutant>, IPollutantRepository
    {
        public PollutantRepository(ApplicationDbContext db) : base(db)
        {
        }
        
    }
}
