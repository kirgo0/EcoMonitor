using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.TaxNorm;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcoMonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class TaxNormDataController : BasicCRUDController<ITaxNormRepository, TaxNorm, TaxNormDTO, TaxNormCreateDTO, TaxNormUpdateDTO>
    {
        public TaxNormDataController(ITaxNormRepository repository) : base(repository)
        {
        }

        [AllowAnonymous]
        public override Task<ActionResult<APIResponse>> Get(int id)
        {
            return base.Get(id);
        }

        [AllowAnonymous]
        public override Task<ActionResult<APIResponse>> GetAll()
        {
            return base.GetAll();
        }

    }
}
