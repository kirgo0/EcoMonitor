﻿using EcoMonitor.Calculator;
using EcoMonitor.Model;
using EcoMonitor.Model.DTO;
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
        private readonly NonCarcinogenicRiskCalculator _nonCarcinogenicRiskCalculator;

        public DataAnalysisController(CarcinogenicRiskCalculator carcinogenicRiskCalculator, NonCarcinogenicRiskCalculator nonCarcinogenicRiskCalculator)
        {
            _carcinogenicRiskCalculator = carcinogenicRiskCalculator;
            _response = new();
            _nonCarcinogenicRiskCalculator = nonCarcinogenicRiskCalculator;
        }

        [Route("CarcinogenicRisk")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                    StatusCode(500, _response);
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


        [Route("NonCarcinogenicRisk")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> CalculateNonCarcinogenicRisk([FromBody] NonCarcinogenicRiskDTO nonCarsinogenicRiskDTO)
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
                _response.Result = _nonCarcinogenicRiskCalculator.CalculateRisk(nonCarsinogenicRiskDTO);
                if (_response.Result != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    return StatusCode(500, _response);
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
