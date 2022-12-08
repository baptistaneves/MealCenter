using MealCenter.Registration.Domain.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealCenter.Registration.Infrastructure.Mappings.Clients
{
    internal class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c=> c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.ImageUrl)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Description)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Status)
                .HasColumnType("bit");

            builder.ToTable("Clients");
        }
    }
}
