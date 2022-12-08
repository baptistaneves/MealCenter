using MealCenter.Registration.Domain.Restaurants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Registration.Infrastructure.Mappings.Restaurants
{
    internal class TableMapping : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.State)
                .HasDefaultValue(TableState.TableIsFree)
                .HasColumnType("varchar(255)");

            builder.Property(t => t.TableNumber)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(t => t.Status)
                .HasColumnType("bit");

            builder.ToTable("Tables");
        }
    }
}
