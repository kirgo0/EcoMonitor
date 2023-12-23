using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO;
using EcoMonitor.Model.DTO.News;
using EcoMonitor.Model.DTO.NewsService;
using EcoMonitor.Repository.IRepository;
using EcoMonitor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace EcoMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;
        private APIResponse _response;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
            _mapper = new MapperConfiguration(
                options => options.CreateMap<FormattedNews, FormattedNewsDTO>().ReverseMap()
                ).CreateMapper();
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("GetNewsByFilter")]
        public ActionResult<APIResponse> GetNewsByFilter(
            [FromQuery][Range(0, int.MaxValue)] int? page,
            [FromQuery][Range(0, int.MaxValue)] int? count,
            [FromQuery] bool? byRelevance,
            [FromQuery] bool? newerToOlder,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] List<int>? region_ids,
            [FromQuery] List<string>? author_ids,
            [FromQuery] List<int>? company_ids
            )
        {

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;

                _response.ErrorMessages.AddRange(
                    ModelState.Values.SelectMany
                    (v => v.Errors
                    .Select(e => e.ErrorMessage)
                    )
                    );

                return BadRequest(_response);
            }

            if (author_ids != null)
            {
                author_ids.RemoveAll(a => a.IsNullOrEmpty());
            }

            if (fromDate != null && toDate == null || fromDate == null && toDate != null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("You need to specify two date parameters or none of them");
                return BadRequest(_response);
            }

            if (fromDate != null && toDate != null)
            {
                if (fromDate > toDate)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("FromDate must be less that toDate!");
                    return BadRequest(_response);
                } else
                {
                    fromDate = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, 0, 0, 0);
                    toDate = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, 23, 59, 59);
                }
            }

            var filters = new NewsFilterDTO()
            {
                page = page,
                count = count,
                byRelevance = byRelevance,
                newerToOlder = newerToOlder,
                fromDate = fromDate,
                toDate = toDate,
                region_ids = region_ids,
                author_ids = author_ids,
                company_ids = company_ids
            };

            try
            {
                var result = _newsService.GetFilteredFormattedNews(filters);

                if (result != null && result.Count > 0)
                {
                    var news = _mapper.Map<List<FormattedNewsDTO>>(result);

                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = new FormattedNewsResponseDTO()
                    {
                        remainingRowsCount = _newsService.lastRequestRemainingRows.Value,
                        selectedNews = news,
                        isItEnd = _newsService.isItEnd.Value
                    };
                    return Ok(_response);
                } else if (result.Count == 0)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = new FormattedNewsResponseDTO()
                    {
                        remainingRowsCount = 0,
                        selectedNews = new List<FormattedNewsDTO>(),
                        isItEnd = true
                    };
                    return Ok(_response);
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
        [Authorize]
        [Route("LikeNews")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<APIResponse> LikeNews([FromQuery, Required] string userId, [FromQuery, Range(0, int.MaxValue), Required] int newsId)
        {
            var tokneUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(tokneUserId != userId)
            {
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.IsSuccess = false;
                return Unauthorized(_response);
            }

            if (userId.IsNullOrEmpty())
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("You need to specify user id!");
                return BadRequest(_response);
            }

            try
            {
                var result = _newsService.UpdateLikeField(userId, newsId);

                if(result == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = result.Value;
                return Ok(_response);
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
