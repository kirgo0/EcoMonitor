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
        private readonly INewsRepository _newsRepository;
        private IMapper _mapper;

        public NewsService(APIResponse response, INewsRepository newsRepository, IMapper mapper)
        {
            _response = response;
            _newsRepository = newsRepository;
            _mapper = mapper;
        }

        public void GetNewsNamesByRegion()
        {
            
        }

    }
}
