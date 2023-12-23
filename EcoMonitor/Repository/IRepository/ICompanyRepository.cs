using EcoMonitor.Model;
using System.Linq.Expressions;

namespace EcoMonitor.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<List<TResult>> selectAsync<TResult>(Expression<Func<Company, TResult>> selector);
    }
}
