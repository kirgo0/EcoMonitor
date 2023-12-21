using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.CompanyWaste;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoMonitor.Controllers
{
    public class CompanyWasteDataController : BasicDataController<ICompanyWasteRepository, CompanyWaste, CompanyWasteDTO, CompanyWasteCreateDTO, CompanyWasteUpdateDTO>
    {
        public CompanyWasteDataController(ICompanyWasteRepository repository) : base(repository)
        {
        }
    }
}
