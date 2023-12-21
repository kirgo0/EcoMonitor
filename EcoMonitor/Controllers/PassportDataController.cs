using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.EnvFactor;
using EcoMonitor.Model.DTO.Passport;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;

namespace EcoMonitor.Controllers
{
    public class PassportDataController : BasicDataController<IPassportRepository, Passport, PassportDTO, PassportCreateDTO, PassportUpdateDTO>
    {
        private readonly ICompanyRepository _repositoryComapny;

        public PassportDataController(ICompanyRepository repositoryComapny, IPassportRepository repository) : base(repository)
        {
            _repositoryComapny = repositoryComapny;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPassportsByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // ------------
        public async Task<ActionResult<APIResponse>> GetPassportsByCompany([FromQuery] int company_id)
        {
            if (company_id < 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("id cannot be less than 0!");
                return BadRequest(_response);
            }
            try
            {
                if (await _repositoryComapny.GetAsync(c => c.id == company_id) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                IEnumerable<Passport> passports = await _repository.GetAllAsync(p => p.company_id == company_id);
                var result = _mapper.Map<IEnumerable<PassportDTO>>(passports);
                _response.Result = result;

                if (result.Count() != 0)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return StatusCode(500, _response);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult<APIResponse>> Create([FromBody] PassportCreateDTO createDTO)
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
                if(await _repositoryComapny.GetAsync(c => c.id == createDTO.company_id) != null)
                {
                    Passport passport = _mapper.Map<Passport>(createDTO);

                    await _repository.CreateAsync(passport);
                    _response.Result = _mapper.Map<PassportDTO>(passport);
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(_response);
                } else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("No company with this id was found!");
                    return NotFound(_response);
                }

            }
            catch (DbUpdateException ex)
            {
                MySqlException innerException = ex.InnerException as MySqlException;
                if (innerException != null && (innerException.Number == 1062))
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Passport with this year already exists");
                    return Conflict(_response);
                }
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
        public override async Task<ActionResult<APIResponse>> Update([FromBody] PassportUpdateDTO updateDTO)
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
                Passport model = _mapper.Map<Passport>(updateDTO);

                if (await _repository.GetAsync(c => c.id == model.id, false) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                if (await _repositoryComapny.GetAsync(c => c.id == updateDTO.company_id) != null)
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
                MySqlException innerException = ex.InnerException as MySqlException;
                if (innerException != null && (innerException.Number == 1062))
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Passport with this year already exists");
                    return Conflict(_response);
                }
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
