using EcoMonitor.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EcoMonitor.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasMany(user => user.news)
                .WithMany(news => news.authors);

            user.HasMany(user => user.likedNews)
                .WithMany(news => news.followers)
                .UsingEntity(j => j.ToTable("NewsFollowers"));

        }
    }
}
