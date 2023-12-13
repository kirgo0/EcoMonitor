using EcoMonitor.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcoMonitor.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task SaveAsync();
    }
}
