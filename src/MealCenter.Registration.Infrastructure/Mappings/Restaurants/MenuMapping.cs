using MealCenter.Registration.Domain.Restaurants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Registration.Infrastructure.Mappings.Restaurants
{
    internal class MenuMapping : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Type)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(m => m.Status)
                .HasColumnType("bit");

            builder.HasMany(m => m.MenuOptions)
                .WithOne(mo => mo.Menu)
                .HasForeignKey(mo => mo.MenuId);

            builder.ToTable("Menus");
        }
    }
}
