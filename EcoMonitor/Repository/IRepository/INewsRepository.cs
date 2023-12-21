using EcoMonitor.Model;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Repository.IRepository
{
    public interface INewsRepository : IRepository<News>
    {
        DbSet<News> dbSet { get;}
    }
}
