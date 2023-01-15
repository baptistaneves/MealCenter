namespace MealCenter.API.Extensions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this WebApplication app, IHostEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    };

                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (contextFeature != null)
                    {
                        var exceptionType = contextFeature.GetType();
                        if (exceptionType == typeof(DomainException))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                            var errors = (DomainException)contextFeature.Error;
                            var response = new {success = false, data = errors.ValidationErrors.AsEnumerable()};

                            var json = JsonSerializer.Serialize(response, options);
                            await context.Response.WriteAsync(json);
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                            var ex = contextFeature.Error;

                            var response = env.IsDevelopment()
                                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                                : new ApiException(context.Response.StatusCode, "Internal Server Error");

                            var json = JsonSerializer.Serialize(response, options);
                            await context.Response.WriteAsync(json);
                        }
                    }

                });
            });
        }
    }
}
