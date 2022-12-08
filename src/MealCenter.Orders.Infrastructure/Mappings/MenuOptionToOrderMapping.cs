using MealCenter.Orders.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Orders.Infrastructure.Mappings
{
    internal class MenuOptionToOrderMapping : IEntityTypeConfiguration<MenuOptionToOrder>
    {
        public void Configure(EntityTypeBuilder<MenuOptionToOrder> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.MenuOptionName)
               .HasColumnType("varchar(255)");

            builder.Property(o => o.ProductName)
              .HasColumnType("varchar(255)");

            builder.Property(o => o.MenuOptionPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.ProductPrice)
               .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Subtotal)
               .HasColumnType("decimal(18,2)");

            builder.HasOne(mo => mo.Order)
                .WithMany(o => o.MenuOptionToOrders);

            builder.ToTable("MenuOptionsOrder");
        }
    }
}
