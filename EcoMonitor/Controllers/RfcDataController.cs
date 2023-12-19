using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.RfcFactor;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Net;

namespace EcoMonitor.Controllers
{
    [ApiController]
    [Route("api/RfcData")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class RfcDataController : BasicCRUDController<IRfcFactorRepository, RfcFactor, RfcFactorDTO, RfcFactorCreateDTO, RfcFactorUpdateDTO>
    {
        public RfcDataController(IRfcFactorRepository repository) : base(repository)
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

        [HttpPost, Route("CreateRfcFactors")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status207MultiStatus)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MultipleAPIResponse>> CreateRfcFactors([FromBody] List<RfcFactorCreateDTO> createDTOlist)
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
                        if (error.ErrorMessage.Contains("The JSON array contains a trailing comma at the end which is not supported in this mode"))
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
                    RfcFactor factor = _mapper.Map<RfcFactor>(createDTO);

                    if (await _repository.GetAsync(f => f.factor_Name== createDTO.factor_Name) != null)
                    {
                        response.StatusCode = HttpStatusCode.Conflict;
                        response.IsSuccess = false;
                        response.ErrorMessages.Add($"Rfc factor with this name already exists");
                    }
                    else
                    {
                        await _repository.CreateAsync(factor);
                        response.Result = _mapper.Map<RfcFactorDTO>(factor);
                        response.StatusCode = HttpStatusCode.Created;
                    }
                    multipleResponse.apiResponses.Add(response);
                }
                catch (Exception ex)
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.IsSuccess = false;
                    response.ErrorMessages = new List<string>() { ex.ToString() };
                    multipleResponse.apiResponses.Add(response);
                }
            }
            return StatusCode(207, multipleResponse);
        }

    }
}
