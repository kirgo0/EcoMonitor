using EcoMonitor.Model;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Repository.IRepository
{
    public interface IFormattedNewsRepository : IRepository<FormattedNews>
    {
        DbSet<FormattedNews> view { get; }
    }
}
