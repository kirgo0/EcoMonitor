using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.CompanyWaste;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoMonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class CompanyWasteController : BasicCRUDController<ICompanyWasteRepository, CompanyWaste, CompanyWasteDTO, CompanyWasteCreateDTO, CompanyWasteUpdateDTO>
    {
        public CompanyWasteController(ICompanyWasteRepository repository) : base(repository)
        {
        }


        [AllowAnonymous]
        public override Task<ActionResult<APIResponse>> GetAll()
        {
            return base.GetAll();
        }

        [AllowAnonymous]
        public override Task<ActionResult<APIResponse>> Get(int id)
        {
            return base.Get(id);
        }
    }
}
