using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;
using System.Linq.Expressions;

namespace EcoMonitor.Repository
{
    public class EnvFactorRepository : Repository<EnvFactor>, IEnvFactorRepository
    {
        private readonly ApplicationDbContext _db;

        public EnvFactorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
