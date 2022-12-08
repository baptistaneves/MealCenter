using MealCenter.Registration.Domain.Restaurants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Registration.Infrastructure.Mappings.Restaurants
{
    internal class MenuOptionMapping : IEntityTypeConfiguration<MenuOption>
    {
        public void Configure(EntityTypeBuilder<MenuOption> builder)
        {
            builder.HasKey(mo => mo.Id);

            builder.Property(mo => mo.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(mo => mo.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

            builder.Property(mo => mo.Ingredients)
               .IsRequired()
               .HasColumnType("varchar(max)");

            builder.Property(mo => mo.Status)
                .HasColumnType("bit");

            builder.ToTable("MenuOptions");
        }
    }
}
