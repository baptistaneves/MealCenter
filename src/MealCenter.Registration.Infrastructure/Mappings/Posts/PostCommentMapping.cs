using MealCenter.Registration.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Registration.Infrastructure.Mappings.Posts
{
    internal class PostCommentMapping : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.Property(pc => pc.Comment)
               .IsRequired()
               .HasColumnType("varchar(max)");

            builder.ToTable("PostComments");
        }
    }
}
