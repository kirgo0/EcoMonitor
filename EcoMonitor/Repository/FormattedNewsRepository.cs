using EcoMonitor.Data;
using EcoMonitor.Model;
using EcoMonitor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcoMonitor.Repository
{
    public class FormattedNewsRepository : IFormattedNewsRepository
    {
        public readonly ApplicationDbContext _context;
        private DbSet<FormattedNews> _dbSet;
        public FormattedNewsRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<FormattedNews>();
        }

        public IQueryable<FormattedNews> query => _dbSet;

        public IEnumerable<FormattedNews> GetView(Expression<Func<FormattedNews, bool>>? filter = null, List<int>? newsIdsOrder = null)
        {
            IQueryable<FormattedNews> query = _dbSet.AsNoTracking();

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(newsIdsOrder != null)
            {
                var orderedEntities = newsIdsOrder
                .Join(
                    query,
                    id => id,
                    entity => entity.id,
                    (id, entity) => entity);
                return orderedEntities;
            }

            return query;
        }
    }
}
