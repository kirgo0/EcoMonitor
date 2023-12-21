using AutoMapper;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Services
{
    public interface INewsService
    {

    }

    public class NewsService : INewsService
    {

        private APIResponse _response;
        private readonly INewsRepository _dbNews;
        private IMapper _mapper;

        public NewsService(APIResponse response, INewsRepository dbNews, IMapper mapper)
        {
            _response = response;
            _dbNews = dbNews;
            _mapper = mapper;
        }


    }
}
