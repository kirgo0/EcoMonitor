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

        public async Task<EnvFactor> UpdateAsync(EnvFactor entity)
        {
            //entity.UpdatedDate = DateTime.Now;
            dbSet.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
