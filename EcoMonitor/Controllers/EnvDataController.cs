using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.DTO;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EcoMonitor.Controllers
{
    [ApiController]
    [Route("api/EnvData")]
    public class EnvDataController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IEnvFactorRepository _dbEnv;
        private readonly IPassportRepository _dbPassport;
        private readonly IMapper _mapper;

        public EnvDataController(IEnvFactorRepository dbEnv, IMapper mapper, IPassportRepository dbPassport)
        {
            _response = new();
            _dbEnv = dbEnv;
            _mapper = mapper;
            _dbPassport = dbPassport;
        }

        [HttpGet("passport_id:int", Name = "GetAllEnvFactors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // ------------
        public async Task<ActionResult<APIResponse>> GetAllEnvFactors(int passport_id)
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
                IEnumerable<EnvFactor> factors = await _dbEnv.GetAllAsync(ef => ef.passport_id == passport_id);
                _response.Result = _mapper.Map<IEnumerable<EnvFactorDTO>>(factors);
                if(_response.Result != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                } else
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                }
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }


        [HttpGet("factor_id:int,passport_id:int", Name = "GetEnvFactor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // ------------
        public async Task<ActionResult<APIResponse>> GetEnvFactor(int passport_id, int factor_id)
        {
            if (passport_id < 0 || factor_id < 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("id cannot be less than 0!");
                return BadRequest(_response);
            }
            try
            {
                EnvFactor factors = await _dbEnv.GetAsync(ef => ef.passport_id == passport_id && ef.id == factor_id);
                _response.Result = _mapper.Map<EnvFactorDTO>(factors);
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
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] //-------------
        public async Task<ActionResult<APIResponse>> CreateEnvFactor([FromBody] EnvFactorCreateDTO createDTO)
        {
            if (createDTO == null || !ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ModelState.ToString());
                return BadRequest(_response);
            }

            try
            {
                //if (await _dbEnv.GetAsync(u => u.factor_Name.ToLower() == createDTO.factor_Name.ToLower()) != null)
                //{
                //    _response.StatusCode = HttpStatusCode.Conflict;
                //    _response.IsSuccess = false;
                //    _response.ErrorMessages.Add("Environment factor already Exists!");
                //    return Conflict(_response);
                //}

                if(await _dbPassport.GetAsync(p => p.id == createDTO.passport_id) != null)
                {
                    EnvFactor envFactor = _mapper.Map<EnvFactor>(createDTO);

                    await _dbEnv.CreateAsync(envFactor);
                    _response.Result = _mapper.Map<EnvFactorDTO>(envFactor);
                    _response.StatusCode = HttpStatusCode.Created;

                    return CreatedAtRoute("GetEnvFactor", new { factor_id = envFactor.id, passport_id = envFactor.passport_id }, _response);
                } else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("No passport with this id was found!");
                    return NotFound(_response);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("id:int", Name = "DeleteEnvFactor")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut(Name = "UpdateEnvFactor")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateEnvFactor([FromBody] EnvFactorUpdateDTO updateDTO)
        {
            if (updateDTO == null || !ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ModelState.ToString());
                return BadRequest(_response);
            }
            try
            {
                EnvFactor model = _mapper.Map<EnvFactor>(updateDTO);

                if(await _dbEnv.GetAsync(u => u.id == model.id) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                if (await _dbPassport.GetAsync(p => p.id == updateDTO.passport_id) != null)
                {
                    await _dbEnv.UpdateAsync(model);
                    return NoContent();
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("No passport with this id was found!");
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
