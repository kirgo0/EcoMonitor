using EcoMonitor.Model;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Utilities;
using System.Linq.Expressions;

namespace EcoMonitor.Repository.IRepository
{
    public interface IFormattedNewsRepository
    {
        DbContext context { get; }
        IEnumerable<FormattedNews> GetView(Expression<Func<FormattedNews, bool>>? filter = null, List<int>? newsIdsOrder = null);
    }
}
