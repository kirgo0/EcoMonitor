using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.City;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;

namespace EcoMonitor.Controllers
{
    public class CityDataController : BasicDataController<ICityRepository, City, CityDTO, CityCreateDTO, CityUpdateDTO>
    {
        private readonly IRegionRepository _repositoryRegion;
        public CityDataController(ICityRepository repository, IRegionRepository repositoryRegion) : base(repository)
        {
            _repositoryRegion = repositoryRegion;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult<APIResponse>> Create([FromBody] CityCreateDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                foreach (var modelError in ModelState.Values)
                {
                    foreach (ModelError error in modelError.Errors)
                    {
                        _response.ErrorMessages.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(_response);
            }
            try
            {
                if (await _repositoryRegion.GetAsync(r => r.id == createDTO.region_id) != null)
                {
                    City city = _mapper.Map<City>(createDTO);

                    await _repository.CreateAsync(city);
                    _response.Result = _mapper.Map<CityDTO>(city);
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtAction("Get", new { id = city.id}, _response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("No company with this id was found!");
                    return NotFound(_response);
                }

            }
            catch (DbUpdateException ex)
            {
                _response.StatusCode = HttpStatusCode.Conflict;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("city with this year already exists");
                return Conflict(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return StatusCode(500, _response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult<APIResponse>> Update([FromBody] CityUpdateDTO updateDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                foreach (var modelError in ModelState.Values)
                {
                    foreach (ModelError error in modelError.Errors)
                    {
                        _response.ErrorMessages.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(_response);
            }
            try
            {
                City model = _mapper.Map<City>(updateDTO);

                if (await _repository.GetAsync(c => c.id == model.id, false) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                if (await _repositoryRegion.GetAsync(r => r.id == updateDTO.region_id) != null)
                {
                    await _repository.UpdateAsync(model);
                    return NoContent();
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("No company with this id was found!");
                    return NotFound(_response);
                }

            }
            catch (DbUpdateException ex)
            {
                _response.StatusCode = HttpStatusCode.Conflict;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("city with this year already exists");
                return Conflict(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return StatusCode(500, _response);
        }
    }
}
