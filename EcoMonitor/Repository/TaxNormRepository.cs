using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Repository
{
    public class TaxNormRepository : Repository<TaxNorm>, ITaxNormRepository
    {
        public TaxNormRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
