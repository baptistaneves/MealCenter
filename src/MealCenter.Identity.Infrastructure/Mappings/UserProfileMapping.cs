using MealCenter.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Identity.Infrastructure.Mappings
{
    internal class UserProfileMapping : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.IdentityId)
                .IsRequired();

            builder.Property(u => u.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(u => u.EmailAddress)
                .IsRequired()
               .HasColumnType("varchar(255)");

            builder.Property(u => u.Phone)
               .HasColumnType("varchar(255)");

            builder.ToTable("UserProfiles");
        }
    }
}
