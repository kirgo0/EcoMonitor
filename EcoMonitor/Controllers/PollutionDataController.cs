using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.EnvFactor;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Net;

namespace EcoMonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PollutionDataController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IPollutionRepository _dbEnv;
        private readonly IPollutantRepository _dbRfc;
        private readonly IPassportRepository _dbPassport;
        private readonly IMapper _mapper;

        public PollutionDataController(IPollutionRepository dbEnv, IMapper mapper, IPassportRepository dbPassport, IPollutantRepository dbRfc)
        {
            _response = new();
            _dbEnv = dbEnv;
            _mapper = mapper;
            _dbPassport = dbPassport;
            _dbRfc = dbRfc;
        }


        [AllowAnonymous]
        [HttpGet(Name = "GetAllEnvFactors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // ------------
        public async Task<ActionResult<APIResponse>> GetAllEnvFactors()
        {
            try
            {
                IEnumerable<Pollution> factors = await _dbEnv.GetAllAsync();
                var result = _mapper.Map<IEnumerable<PollutionDTO>>(factors);
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

        
        [AllowAnonymous]
        [HttpGet]
        [Route("GetEnvFactorsByPassport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // ------------
        public async Task<ActionResult<APIResponse>> GetEnvFactorsByPassport(int passport_id)
        {
            if(passport_id < 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("id cannot be less than 0!");
                return BadRequest(_response);
            }
            try
            {
                if (await _dbPassport.GetAsync(p => p.id == passport_id) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                IEnumerable<Pollution> factors = await _dbEnv.GetAllAsync(ef => ef.passport_id == passport_id);
                var result = _mapper.Map<IEnumerable<PollutionDTO>>(factors);
                _response.Result = result;
                if(result.Count() != 0)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                } else
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

        [AllowAnonymous]
        [HttpGet]
        [Route("GetEnvFactor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // ------------
        public async Task<ActionResult<APIResponse>> GetEnvFactor(int id)
        {
            if (id < 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("id cannot be less than 0!");
                return BadRequest(_response);
            }
            try
            {
                Pollution factors = await _dbEnv.GetAsync(ef => ef.id == id);
                _response.Result = _mapper.Map<PollutionDTO>(factors);
                if (_response.Result != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
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

        [HttpPost, Route("CreateEnvFactor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] //-------------
        public async Task<ActionResult<APIResponse>> CreateEnvFactor([FromBody] PollutionCreateDTO createDTO)
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
                var passport = await _dbPassport.GetAsync(p => p.id == createDTO.passport_id);
                var rfc = await _dbRfc.GetAsync(r => r.id == createDTO.pollutant_id);
                if (passport != null && rfc != null)
                {
                    Pollution envFactor = _mapper.Map<Pollution>(createDTO);

                    await _dbEnv.CreateAsync(envFactor);
                    _response.Result = _mapper.Map<PollutionDTO>(envFactor);
                    _response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetEnvFactor", new { factor_id = envFactor.id, passport_id = envFactor.passport_id }, _response);
                } else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    if (passport == null)
                        _response.ErrorMessages.Add($"No passport with this id:{createDTO.passport_id} was found!");
                    if (rfc == null)
                        _response.ErrorMessages.Add($"No rfc factor with this id:{createDTO.pollutant_id} was found!");
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
                    _response.ErrorMessages.Add("Factor with this name already exists");
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


        [HttpPost, Route("CreateEnvFactors")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status207MultiStatus)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] //-------------
        public async Task<ActionResult<MultipleAPIResponse>> CreateEnvFactors([FromBody] List<PollutionCreateDTO> createDTOlist)
        {
            MultipleAPIResponse multipleResponse = new MultipleAPIResponse();

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                foreach (var modelError in ModelState.Values)
                {
                    foreach (ModelError error in modelError.Errors)
                    {
                        _response.ErrorMessages.Add(error.ErrorMessage);
                        multipleResponse.apiResponses.Add(_response);
                        if(error.ErrorMessage.Contains("The JSON array contains a trailing comma at the end which is not supported in this mode"))
                        {
                            return BadRequest(multipleResponse);
                        }
                    }
                }
            }
            foreach (var createDTO in createDTOlist)
            {
                APIResponse response = new APIResponse();
                try
                {
                    var passport = await _dbPassport.GetAsync(p => p.id == createDTO.passport_id);
                    var rfc = await _dbRfc.GetAsync(r => r.id == createDTO.pollutant_id);
                    if (passport != null && rfc != null)
                    {
                        Pollution envFactor = _mapper.Map<Pollution>(createDTO);

                        if (await _dbEnv.GetAsync(e => e.passport_id == createDTO.passport_id && e.name == createDTO.name) != null)
                        {
                            response.StatusCode = HttpStatusCode.Conflict;
                            response.IsSuccess = false;
                            response.ErrorMessages.Add($"Factor with this name already exists, passport id:{createDTO.passport_id}, factor name:{createDTO.name}");
                        } else
                        {
                            await _dbEnv.CreateAsync(envFactor);
                            response.Result = _mapper.Map<PollutionDTO>(envFactor);
                            response.StatusCode = HttpStatusCode.Created;
                        }
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.IsSuccess = false;
                        if(passport == null) 
                            response.ErrorMessages.Add($"No passport with this id:{createDTO.passport_id} was found!");
                        if(rfc == null) 
                            response.ErrorMessages.Add($"No rfc factor with this id:{createDTO.pollutant_id} was found!");
                    }
                    multipleResponse.apiResponses.Add(response);
                }
                catch (Exception ex)
                {
                    response.StatusCode=HttpStatusCode.InternalServerError;
                    response.IsSuccess = false;
                    response.ErrorMessages = new List<string>() { ex.ToString() };
                    multipleResponse.apiResponses.Add(response);
                }
            }
            return StatusCode(207, multipleResponse); 
        }

        [HttpDelete("id:int", Name = "DeleteEnvFactor")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeleteEnvFactor(int id)
        {
            try
            {
                if (id < 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Factor id cannot be less than 0!");
                    return BadRequest(_response);
                }
                var factor = await _dbEnv.GetAsync(u => u.id == id);
                if (factor == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                await _dbEnv.RemoveAsync(factor);
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return StatusCode(500,_response);
        }

        [HttpPut(Name = "UpdateEnvFactor")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateEnvFactor([FromBody] PollutionUpdateDTO updateDTO)
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
                Pollution model = _mapper.Map<Pollution>(updateDTO);

                if (await _dbEnv.GetAsync(u => u.id == model.id, false) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                var passport = await _dbPassport.GetAsync(p => p.id == updateDTO.passport_id);
                var rfc = await _dbRfc.GetAsync(r => r.id == updateDTO.pollutant_id);
                if (passport != null && rfc != null)
                {
                    await _dbEnv.UpdateAsync(model);
                    return NoContent();
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    if (passport == null)
                        _response.ErrorMessages.Add($"No passport with this id:{updateDTO.passport_id} was found!");
                    if (rfc == null)
                        _response.ErrorMessages.Add($"No rfc factor with this id:{updateDTO.pollutant_id} was found!");
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
                    _response.ErrorMessages.Add("Factor with this name already exists");
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
