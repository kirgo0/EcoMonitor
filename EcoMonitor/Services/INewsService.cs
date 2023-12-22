﻿using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.News;
using EcoMonitor.Repository;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace EcoMonitor.Services
{
    public interface INewsService
    {
        int? lastRequestRemainingRows { get; }
        bool? isItEnd { get; }
        List<FormattedNews> GetFilteredFormattedNews(NewsFilterDTO dto);
    }

    public class NewsService : INewsService
    {

        private APIResponse _response;
        private readonly INewsRepository _newsRepository;
        private readonly IFilteredNewsService _filteredNewsService;
        private IMapper _mapper;

        public int? lastRequestRemainingRows { get; private set; } = 0;

        public bool? isItEnd { get; private set; }

        public NewsService(INewsRepository newsRepository, IFilteredNewsService filteredNewsService, IMapper mapper)
        {
            _response = new APIResponse();
            _newsRepository = newsRepository;
            _filteredNewsService = filteredNewsService;
            _mapper = mapper;
        }

        public List<FormattedNews> GetFilteredFormattedNews(NewsFilterDTO dto)
        {
            var result = _filteredNewsService.GetFilteredFormattedNews(dto);
            lastRequestRemainingRows = _filteredNewsService.lastRequestRemainingRows;
            isItEnd = _filteredNewsService.isItEnd;
            return result;
        }
    }
}
