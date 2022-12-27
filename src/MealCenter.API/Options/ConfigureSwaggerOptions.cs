namespace MealCenter.API.Options
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }

            var scheme = GetJwtSecurityScheme();
            options.AddSecurityDefinition(scheme.Reference.Id, scheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {scheme, new string[0] }
            });
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "Meal Center",
                Version = description.ApiVersion.ToString(),
                Description = "This Web API was builded only for learning proposol.",
                Contact = new OpenApiContact
                {
                    Name = "Baptista Neves",
                    Email = "baptistafirminoneves@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "CC BY"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description = "This API version has been deprecated";

            }
            return info;
        }

        private OpenApiSecurityScheme GetJwtSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Provide a JWT Bearer",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                },
            };
        }
    }
}
