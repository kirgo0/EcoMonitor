using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO;
using EcoMonitor.Model.DTO.News;
using EcoMonitor.Repository.IRepository;
using EcoMonitor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;

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
        public NewsController(INewsRepository repository, INewsService newsService) 
        {
            _newsService = newsService;
            _mapper = new MapperConfiguration(
                options => options.CreateMap<FormattedNews, FormattedNewsDTO>().ReverseMap()
                ).CreateMapper();
            _response = new APIResponse();
        }

        [HttpGet]
        [Route("GetNewsByFilter")]
        public ActionResult<APIResponse> GetNewsByFilter(
            [FromQuery] int? page,
            [FromQuery] int? count,
            [FromQuery] bool? byRelevance,
            [FromQuery] bool? newerToOlder,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] List<int>? region_ids,
            [FromQuery] List<string>? author_ids,
            [FromQuery] List<int>? company_ids
            )
        {

            if(page < 0 || count <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Page and count must be a positive numbers!");
                return BadRequest(_response);
            }
            if(author_ids != null)
            {
                author_ids.RemoveAll(a => a.IsNullOrEmpty());
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

            var result = _newsService.GetFilteredFormattedNews(filters);
            if(result != null)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = result;
                return Ok(_response);

            }

            return NoContent();
        }

    }
}
