using EcoMonitor.Model;

namespace EcoMonitor.Repository.IRepository
{
    public interface IPassportRepository : IRepository<Passport>
    {
        Task<Passport> UpdateAsync(Passport entity);
    }
}
