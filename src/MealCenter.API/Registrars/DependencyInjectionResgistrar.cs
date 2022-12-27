using MealCenter.Identity.Application.Queries;
using MealCenter.Identity.Application.Services;

namespace MealCenter.API.Registrars
{
    public class DependencyInjectionResgistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            //MediatorHandler
            builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Notifications
            builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Registration
            builder.Services.AddScoped<JwtAdminService>();
            builder.Services.AddScoped<JwtClientService>();
            builder.Services.AddScoped<JwtRestaurantService>();
            builder.Services.AddScoped<JwtService>();

            builder.Services.AddMediatR(typeof(GetAllAdminUserProfilesQuery));
        }
    }
}
