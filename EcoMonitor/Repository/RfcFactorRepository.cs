using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Repository
{
    public class RfcFactorRepository : Repository<RfcFactor>, IRfcFactorRepository
    {
        public RfcFactorRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<RfcFactor> UpdateAsync(RfcFactor entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
