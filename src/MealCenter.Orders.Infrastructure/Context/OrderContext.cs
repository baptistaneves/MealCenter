using MealCenter.Core.Data;
using MealCenter.Orders.Domain;
using Microsoft.EntityFrameworkCore;

namespace MealCenter.Orders.Infrastructure.Context
{
    public class OrderContext : DbContext, IUnitOfWork
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuOptionToOrder> MenuOptionsToOrder { get; set; }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    entry.Property("LastModified").IsModified = false;

                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                    entry.Property("LastModified").CurrentValue = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken) > 0;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);

            builder.HasSequence<int>("CodeSequency").StartsAt(1000).IncrementsBy(1);

            base.OnModelCreating(builder);
        }
    }
}
