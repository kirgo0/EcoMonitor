using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.NewsService;
using EcoMonitor.Repository;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcoMonitor.Services
{
    public interface INewsService
    {
        int? lastRequestRemainingRows { get; }
        bool? isItEnd { get; }
        List<FormattedNews> GetFilteredFormattedNews(NewsFilterDTO dto);
        int? UpdateLikeField(string userId, int newsId);
    }

    public class NewsService : INewsService
    {

        private APIResponse _response;
        private readonly INewsRepository _newsRepository;
        private readonly UserManager<User> _userManager;
        private readonly IFormattedNewsRepository _formattedNewsRepository;
        private readonly IFilteredNewsService _filteredNewsService;
        private IMapper _mapper;

        public int? lastRequestRemainingRows { get; private set; } = 0;

        public bool? isItEnd { get; private set; }

        public NewsService(INewsRepository newsRepository, IFilteredNewsService filteredNewsService, IMapper mapper, IFormattedNewsRepository formattedNewsRepository, UserManager<User> userManager)
        {
            _response = new APIResponse();
            _newsRepository = newsRepository;
            _filteredNewsService = filteredNewsService;
            _mapper = mapper;
            _formattedNewsRepository = formattedNewsRepository;
            _userManager = userManager;
        }

        public List<FormattedNews> GetFilteredFormattedNews(NewsFilterDTO dto)
        {
            var result = _filteredNewsService.GetFilteredFormattedNews(dto);
            lastRequestRemainingRows = _filteredNewsService.lastRequestRemainingRows;
            isItEnd = _filteredNewsService.isItEnd;
            return result;
        }

        public int? UpdateLikeField(string userId, int newsId)
        {
            var query = _newsRepository.dbSet.Where(n => n.id == newsId).Include("followers");

            var news = query.FirstOrDefault();

            if(news == null)
                return null;

            var user = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();

            if (user == null)
                return null;

            if (!news.followers.Any(u => u.Id == userId))
            {
                news.followers.Add(user);
            }
            else
            {
                news.followers.Remove(user);
            }

            _newsRepository.SaveAsync().GetAwaiter().GetResult();

            var likesCount = _formattedNewsRepository.GetView(fn => fn.id == newsId).First().likes;

            return likesCount;
        }
    }
}
