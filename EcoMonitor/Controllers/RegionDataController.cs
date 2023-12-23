using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.Company;
using EcoMonitor.Model.DTO.Region;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcoMonitor.Controllers
{
    public class RegionDataController : BasicDataController<IRegionRepository, Region, RegionDTO, RegionCreateDTO, RegionUpdateDTO>
    {
        public RegionDataController(IRegionRepository repository) : base(repository)
        {
        }


        [HttpGet("GetAllNarrowRegions")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllNarrowRegions()
        {
            try
            {
                var companies = await _repository.selectAsync<NarrowRegionDTO>(c => new NarrowRegionDTO { id = c.id, name = c.name });

                if (companies.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }

                _response.Result = companies;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode(500, _response);
            }
        }
    }
}
