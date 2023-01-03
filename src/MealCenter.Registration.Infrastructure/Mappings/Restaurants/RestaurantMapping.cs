using MealCenter.Registration.Domain.Restaurants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Registration.Infrastructure.Mappings.Restaurants
{
    internal class RestaurantMapping : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(r => r.Location)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(r => r.ImageUrl)
                .HasColumnType("varchar(255)");

            builder.Property(r => r.Description)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Phone)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.EmailAddress)
               .HasColumnType("varchar(255)");

            builder.Property(r => r.Status)
                .HasColumnType("bit");

            builder.HasMany(r => r.Menus)
               .WithOne(m => m.Restaurant)
               .HasForeignKey(m => m.RestaurantId);

            builder.HasMany(r => r.Tables)
              .WithOne(t => t.Restaurant)
              .HasForeignKey(t => t.RestaurantId);

            builder.ToTable("Restaurants");
        }
    }
}
