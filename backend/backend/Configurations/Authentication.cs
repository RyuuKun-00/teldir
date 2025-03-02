using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace backend.Configurations
{
    public static class Authentication
    {
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            AuthOptions authOptions = AuthOptions.getInstance();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = authOptions.ValidateIssuer,
                        ValidIssuer = authOptions.ISSUER,
                        ValidateAudience = authOptions.ValidateAudience,
                        ValidAudience = authOptions.AUDIENCE,
                        ValidateLifetime = authOptions.ValidateLifetime,
                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = authOptions.ValidateIssuerSigningKey,
                    };
                });
        }
    }
}
