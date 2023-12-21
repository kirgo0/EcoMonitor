using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.Region;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoMonitor.Controllers
{
    public class RegionDataController : BasicDataController<IRegionRepository, Region, RegionDTO, RegionCreateDTO, RegionUpdateDTO>
    {
        public RegionDataController(IRegionRepository repository) : base(repository)
        {
        }
    }
}
