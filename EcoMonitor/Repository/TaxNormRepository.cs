using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcoMonitor.Repository
{
    public class TaxNormRepository : Repository<TaxNorm>, ITaxNormRepository
    {
        public TaxNormRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<List<int>> GetDiscinctByYear()
        {
            IQueryable<TaxNorm> query = _dbSet;
            

            return await query.Select(t => t.year).Distinct().ToListAsync();
        }
    }
}
