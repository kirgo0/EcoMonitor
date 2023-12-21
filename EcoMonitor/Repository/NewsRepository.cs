using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Repository
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext db) : base(db)
        {
        }

        DbSet<News> INewsRepository.dbSet => _dbSet;
    }
}
