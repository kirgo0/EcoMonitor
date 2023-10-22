using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;
using System.Linq.Expressions;

namespace EcoMonitor.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Company> UpdateAsync(Company entity)
        {
            //entity.UpdatedDate = DateTime.Now;
            dbSet.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
