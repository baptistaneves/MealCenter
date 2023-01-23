namespace MealCenter.API.Registrars;

public class DependencyInjectionResgistrar : IWebApplicationBuilderRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        //Identity
        builder.Services.AddScoped<IdentityContext>();

        builder.Services.AddScoped<JwtAdminService>();
        builder.Services.AddScoped<JwtClientService>();
        builder.Services.AddScoped<JwtRestaurantService>();
        builder.Services.AddScoped<JwtService>();

        builder.Services.AddMediatR(typeof(GetAllAdminUserProfilesQuery));

        //MediatorHandler
        builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

        //Notifications
        builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        //Registration
        builder.Services.AddScoped<RegistrationContext>();

        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientAppService, ClientAppService>();

        builder.Services.AddScoped<IRestaurantAppService, RestaurantAppService>();
        builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

        builder.Services.AddScoped<IPostAppService, PostAppService>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();

        builder.Services.AddAutoMapper(typeof(RegistrationRequestToDomainMappingProfile));

        //Catalog
        builder.Services.AddScoped<CatalogContext>();

        builder.Services.AddScoped<IProductAppService, ProductAppService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
    }
}
