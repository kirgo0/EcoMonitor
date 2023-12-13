using AutoMapper;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcoMonitor.Controllers
{
    [Route("api/News")]
    [ApiController]
    [Authorize(Roles = "NewsModerator")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class NewsController : ControllerBase
    {

        protected APIResponse _response;
        private readonly INewsRepository _dbNews;
        private readonly IMapper _mapper;

        public NewsController(APIResponse response, INewsRepository dbNews, IMapper mapper)
        {
            _response = response;
            _dbNews = dbNews;
            _mapper = mapper;
        }


    }
}
