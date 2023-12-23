using EcoMonitor.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcoMonitor.Repository.IRepository
{
    public interface IRegionRepository : IRepository<Region>
    {
        Task<List<TResult>> selectAsync<TResult>(Expression<Func<Region, TResult>> selector);
    }
}
