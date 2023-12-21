using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Repository
{
    public class FormatedNewsRepository : Repository<FormattedNews>, IFormattedNewsRepository
    {
        public FormatedNewsRepository(ApplicationDbContext db) : base(db)
        {
        }

        public DbSet<FormattedNews> view => dbSet;
    }
}
