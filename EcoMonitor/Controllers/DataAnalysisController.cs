using AutoMapper;
using EcoMonitor.Calculator;
using EcoMonitor.Model;
using EcoMonitor.Model.DTO;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace EcoMonitor.Controllers
{
    [ApiController]
    [Route("api/DataAnalysis")]
    public class DataAnalysisController : Controller
    {
        protected APIResponse _response;
        private readonly CarcinogenicRiskCalculator _carcinogenicRiskCalculator;
        private readonly IEnvFactorRepository _dbEnv;
        private readonly IPassportRepository _dbPassport;
        private readonly IMapper _mapper;

        public DataAnalysisController(CarcinogenicRiskCalculator carcinogenicRiskCalculator, IEnvFactorRepository dbEnv, IMapper mapper, IPassportRepository dbPassport)
        {
            _carcinogenicRiskCalculator = carcinogenicRiskCalculator;
            _response = new();
            _dbEnv = dbEnv;
            _mapper = mapper;
            _dbPassport = dbPassport;
        }


        [HttpPost(Name = "CalculateCarcinogenicRisk")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]// ------------
        public ActionResult<APIResponse> CalculateCarcinogenicRisk([FromBody] CarcinogenicRiskDTO carsinogenicRiskDTO)
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
                _response.Result = _carcinogenicRiskCalculator.CalculateRisk(carsinogenicRiskDTO);
                if (_response.Result != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
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

    }
}
