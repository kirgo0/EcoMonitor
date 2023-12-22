using EcoMonitor.Model;
using EcoMonitor.Model.DTO;
using EcoMonitor.Repository.IRepository;
using System.Linq.Expressions;

namespace EcoMonitor.Services
{
    public interface IFilteredNewsService
    {
        int? page { get; set; }
        int? count { get; set; }
        bool? isDescending { get; set; }
        DateTime? fromDate { get; set; }
        DateTime? toDate { get; set; }
        List<FormattedNews> GetFilteredFormattedNews(NewsFilterDTO dto);
    }

    public class FilteredNewsService : IFilteredNewsService
    {
        public int? page { get; set; } = 0;
        public int? count { get; set; } = 10;
        public bool? isDescending { get; set; } = false;
        public DateTime? fromDate { get; set; } = null;
        public DateTime? toDate { get; set; } = null;

        private readonly INewsRepository _newsRepository;
        private readonly IFormattedNewsRepository _formattedNewsRepository;

        public FilteredNewsService(INewsRepository newsRepository, IFormattedNewsRepository formattedNewsRepository)
        {
            _newsRepository = newsRepository;
            _formattedNewsRepository = formattedNewsRepository;
        }

        public List<FormattedNews> GetFilteredFormattedNews(NewsFilterDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException();
            }

            if(dto.page.HasValue) page = dto.page.Value;
            if(dto.count.HasValue) count = dto.count.Value;
            isDescending = dto.newerToOlder;
            if(dto.fromDate.HasValue) fromDate = dto.fromDate.Value;
            if(dto.toDate.HasValue) toDate = dto.toDate.Value;

            IQueryable<News> query = _newsRepository.dbSet;

            // filtering all news which are related to one of regions in list
            if (dto.region_ids != null && dto.region_ids.Count > 0)
            {
                query = query
                    .Where(n => n.regions.Any(r => dto.region_ids.Contains(r.id)));
            }

            // filtering all news which are related to one of author in list
            if (dto.author_ids != null && dto.author_ids.Count > 0)
            {
                query = query
                    .Where(n => n.authors.Any(a => dto.author_ids.Contains(a.Id)));
            }

            // filtering all news which are related to one of company in list
            if (dto.company_ids != null && dto.company_ids.Count > 0)
            {
                query = query
                    .Where(n => n.companies.Any(c => dto.company_ids.Contains(c.id)));
            }

            List<int> news_ids = new List<int>();

            // getting filttered news ids from query
            news_ids = GetFiltteredNewsIds<DateTime>(query);

            IEnumerable<FormattedNews> formattedNews;

            // getting selected news in formatted objects from view
            formattedNews = _formattedNewsRepository.GetView(
                fn => news_ids.Contains(fn.id), news_ids);

            // sorting by relevance if needed
            if (dto.byRelevance.HasValue)
            {
                if (dto.byRelevance.Value && !dto.newerToOlder.HasValue)
                {
                    formattedNews = formattedNews.OrderByDescending(fn => fn.likes);
                }
            }

            return formattedNews.ToList();
        }

        private List<int> GetFiltteredNewsIds<OrderByObj>(
            IQueryable<News> query,
            Expression<Func<News, OrderByObj>>? orderByFilter = null)
        {
            var news_Ids = query.ToList();

            if ((fromDate == null && toDate != null) || (fromDate != null && toDate == null))
            {
                throw new ArgumentException("Both Date parameters must be either null or not null.");
            }
            else if (fromDate != null && toDate != null)
            {
                // filtering by datetime range
                query = query.Where(n => n.post_date >= fromDate && n.post_date <= toDate);
            }

            // if param specified use it
            if (orderByFilter != null)
            {
                // check if descending needed
                if (isDescending.HasValue && isDescending.Value)
                {
                    query = query.OrderByDescending(orderByFilter);
                }
                else
                {
                    query = query.OrderBy(orderByFilter);
                }
            }
            else
            {
                // check if descending needed
                if (isDescending.HasValue && isDescending.Value)
                {
                    query = query.OrderByDescending(n => n.post_date);
                }
                else
                {
                    query = query.OrderBy(n => n.post_date);
                }
            }

            query = query
                .Skip(page.Value * count.Value)
                .Take(count.Value);

            return query.Select(n => n.id).ToList();
        }
    }
}
