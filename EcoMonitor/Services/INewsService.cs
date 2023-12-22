using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO;
using EcoMonitor.Repository;
using EcoMonitor.Repository.IRepository;
using System.Linq.Expressions;

namespace EcoMonitor.Services
{
    public interface INewsService
    {
        List<FormattedNews> GetFilteredFormattedNews(NewsFilterDTO dto);
    }

    public class NewsService : INewsService
    {

        private APIResponse _response;
        private readonly INewsRepository _newsRepository;
        private readonly IFilteredNewsService _filteredNewsService;
        private IMapper _mapper;

        public NewsService(INewsRepository newsRepository, IFilteredNewsService filteredNewsService, IMapper mapper)
        {
            _response = new APIResponse();
            _newsRepository = newsRepository;
            _filteredNewsService = filteredNewsService;
            _mapper = mapper;
        }

        public List<FormattedNews> GetFilteredFormattedNews(NewsFilterDTO dto)
        {
            return _filteredNewsService.GetFilteredFormattedNews(dto);
        }
    }
}
