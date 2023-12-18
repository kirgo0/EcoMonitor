using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.DTO.Region;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EcoMonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionDataController : BasicCRUDController<IRegionRepository, Region, RegionDTO, RegionCreateDTO, RegionUpdateDTO>
    {
        public RegionDataController(IRegionRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
