using EcoMonitor.Model;

namespace EcoMonitor.Repository.IRepository
{
    public interface IRfcFactorRepository : IRepository<RfcFactor>
    {
        Task<RfcFactor> UpdateAsync(RfcFactor entity);
    }
}
