using MealCenter.Registration.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Registration.Infrastructure.Mappings.Posts
{
    internal class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
               .IsRequired()
               .HasColumnType("varchar(500)");

            builder.Property(p => p.Content)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(p => p.ImageUrl)
               .IsRequired()
               .HasColumnType("varchar(255)");

            builder.HasMany(p => p.PostComments)
                .WithOne(pc => pc.Post)
                .HasForeignKey(pc => pc.PostId);

            builder.HasMany(p => p.PostReactions)
               .WithOne(pc => pc.Post)
               .HasForeignKey(pc => pc.PostId);

            builder.ToTable("Posts");
        }
    }
}
