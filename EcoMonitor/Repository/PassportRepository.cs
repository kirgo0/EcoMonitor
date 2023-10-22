using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Repository
{
    public class PassportRepository : Repository<Passport>, IPassportRepository
    {
        public PassportRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Passport> UpdateAsync(Passport entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
