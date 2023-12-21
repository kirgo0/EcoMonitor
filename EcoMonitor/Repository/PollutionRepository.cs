using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;
using System.Linq.Expressions;

namespace EcoMonitor.Repository
{
    public class PollutionRepository : Repository<Pollution>, IPollutionRepository
    {
        private readonly ApplicationDbContext _db;

        public PollutionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
