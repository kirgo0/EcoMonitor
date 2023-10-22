using EcoMonitor.Model;

namespace EcoMonitor.Repository.IRepository
{
    public interface IEnvFactorRepository : IRepository<EnvFactor>
    {
        Task<EnvFactor> UpdateAsync(EnvFactor entity);
    }
}
