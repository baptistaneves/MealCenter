namespace MealCenter.API.Registrars
{
    public class IdentityRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var jwtSetting = new JwtSettings();

            builder.Configuration.Bind(nameof(JwtSettings), jwtSetting);


            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

            builder.Services
                .AddAuthentication(a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.SigningKey)),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSetting.Issuer,
                        ValidateAudience = true,
                        ValidAudiences = jwtSetting.Audiences,
                        RequireExpirationTime = false,
                        ValidateLifetime = true,
                    };

                    jwt.Audience = jwtSetting.Audiences[0];
                    jwt.ClaimsIssuer = jwtSetting.Issuer;
                });
        }
    }
}
