using MealCenter.Orders.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Orders.Infrastructure.Mappings
{
    internal class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Amount)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Code)
               .HasDefaultValueSql("NEXT VALUE FOR CodeSequency");

            builder.HasMany(o => o.MenuOptionToOrders)
                .WithOne(mo => mo.Order)
                .HasForeignKey(mo => mo.OrderId);

            builder.ToTable("Orders");
        }
    }
}
