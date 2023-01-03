namespace MealCenter.API.Registrars
{
    public class DbResgistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //Registration
            builder.Services.AddDbContext<RegistrationContext>(options => options.UseSqlServer(connectionString));

            //Identity
            builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
            })
            //.AddUserManager<IdentityUser>()
            .AddSignInManager<SignInManager<IdentityUser>>()
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<IdentityContext>();
        }
    }
}
