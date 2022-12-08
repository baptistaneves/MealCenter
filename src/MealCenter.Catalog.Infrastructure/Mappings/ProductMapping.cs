using MealCenter.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealCenter.Catalog.Infrastructure.Mappings
{
    internal class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Description)
               .IsRequired()
               .HasColumnType("varchar(max)");

            builder.Property(p => p.CategoryId)
               .IsRequired();

            builder.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ImageUrl)
              .IsRequired()
              .HasColumnType("varchar(255)");

            builder.Property(p => p.Status)
              .HasColumnType("bit");

            builder.ToTable("Products");
        }
    }
}

