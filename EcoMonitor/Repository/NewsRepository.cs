using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Repository
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
