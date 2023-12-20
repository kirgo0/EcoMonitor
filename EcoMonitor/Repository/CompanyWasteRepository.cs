using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Repository
{
    public class CompanyWasteRepository : Repository<CompanyWaste>, ICompanyWasteRepository
    {
        public CompanyWasteRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
