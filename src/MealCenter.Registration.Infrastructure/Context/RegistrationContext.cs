using MealCenter.Core.Data;
using MealCenter.Registration.Domain.Clients;
using MealCenter.Registration.Domain.Posts;
using MealCenter.Registration.Domain.Restaurants;
using Microsoft.EntityFrameworkCore;

namespace MealCenter.Registration.Infrastructure.Context
{
    public class RegistrationContext : DbContext, IUnitOfWork
    {
        public RegistrationContext(DbContextOptions<RegistrationContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuOption> MenuOptions { get; set; }

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
            builder.ApplyConfigurationsFromAssembly(typeof(RegistrationContext).Assembly);

            base.OnModelCreating(builder);  
        }
    }
}
