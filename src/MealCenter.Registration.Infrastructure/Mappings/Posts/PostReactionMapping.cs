using MealCenter.Registration.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Registration.Infrastructure.Mappings.Posts
{
    internal class PostReactionMapping : IEntityTypeConfiguration<PostReaction>
    {
        public void Configure(EntityTypeBuilder<PostReaction> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.ToTable("PostReactions");
        }
    }
}
