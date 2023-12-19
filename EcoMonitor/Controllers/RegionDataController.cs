using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.Region;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoMonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionDataController : BasicCRUDController<IRegionRepository, Region, RegionDTO, RegionCreateDTO, RegionUpdateDTO>
    {
        public RegionDataController(IRegionRepository repository) : base(repository)
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
