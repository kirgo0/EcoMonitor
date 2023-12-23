using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.NewsService;
using EcoMonitor.Repository;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace EcoMonitor.Services
{
    public interface INewsService
    {
        int? lastRequestRemainingRows { get; }
        bool? isItEnd { get; }
        FormattedNewsDTO GetFormattedNewsById(int newsId, string userId);
        List<FormattedNewsDTO> GetFilteredFormattedNews(NewsFilterDTO dto, string userId);
        int? UpdateLikeField(string userId, int newsId);
        List<RegionNewsDTO> GetRegionsNews(int regionsCount, int newsCount);
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

        public List<FormattedNewsDTO> GetFilteredFormattedNews(NewsFilterDTO dto, string userId)
        {
            var result = _filteredNewsService.GetFilteredFormattedNews(dto);
            lastRequestRemainingRows = _filteredNewsService.lastRequestRemainingRows;
            isItEnd = _filteredNewsService.isItEnd;

            var mappedResults = _mapper.Map<List<FormattedNewsDTO>>(result);

            if(!userId.IsNullOrEmpty())
            {
                mappedResults.ForEach(mr =>
                {
                    mr.isLiked = _newsRepository.dbSet.Where(n => n.id == mr.id).Include("followers").First().followers.Any(u => u.Id == userId);
                });
            }
            return mappedResults;
        }

        public FormattedNewsDTO GetFormattedNewsById(int newsId, string userId)
        {
            var result = _filteredNewsService.GetFormattedNews(newsId);

            if(result != null) 
            {
                if(!userId.IsNullOrEmpty())
                {
                    result.isLiked = _newsRepository.dbSet.Where(n => n.id == result.id).Include("followers").First().followers.Any(u => u.Id == userId);
                }
                return result;
            }
            return null;
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

        public List<RegionNewsDTO> GetRegionsNews(int regionsCount, int newsCount)
        {

            var regionNews = _newsRepository.dbSet
            .Where(n => n.regions.Any())
            .SelectMany(n => n.regions, (news, region) => new { News = news, Region = region })
            .GroupBy(x => x.Region.name)
            .Select(g => new RegionNewsDTO
            {
                region_name = g.Key,
                news = g
                    .OrderByDescending(x => x.News.post_date) 
                    .Select(x => new NarrowNewsDTO 
                    {
                        id = x.News.id,
                        title = x.News.title
                    })
                    .Take(newsCount) 
                    .ToList()
            })
            .Take(regionsCount)
            .ToList();

            return regionNews;
        }

    }
}
