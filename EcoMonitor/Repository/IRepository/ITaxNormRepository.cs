using EcoMonitor.Model;
using System.Linq.Expressions;

namespace EcoMonitor.Repository.IRepository
{
    public interface ITaxNormRepository : IRepository<TaxNorm>
    {
        Task<List<int>> GetDiscinctByYear();
    }
}
