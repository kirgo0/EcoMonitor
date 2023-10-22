using EcoMonitor.Model;

namespace EcoMonitor.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> UpdateAsync(Company entity);
    }
}
