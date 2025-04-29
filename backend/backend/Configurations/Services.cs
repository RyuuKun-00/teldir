using backend.Application.Services;
using backend.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;

namespace backend.Configurations
{
	public static class Services
	{
		public static void AddServices(this WebApplicationBuilder builder)
		{
			var s = builder.Services;

			var authOptions = builder.Configuration.GetSection("Jwt").Get<AuthOptions>();

			s.AddControllers();
			s.AddEndpointsApiExplorer();
			s.AddSwaggerGen(c =>
			{
				c.CustomSchemaIds(r => r.FullName);
			});

			s.AddCustomCors();

			s.AddCustomDBContext(builder.Configuration);

			s.AddScoped<IContactRepository, ContactRepository>();
			s.AddScoped<IContactService, ContactService>();
			s.AddScoped<IAuthService, AuthService>();
			s.AddScoped<ITokenRepository, TokenRepository>();
			s.AddScoped<IUserRepository, UserRepository>();
			s.AddScoped<TokenService>();
			s.AddSingleton<AuthOptions>(authOptions!);


			s.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				   .AddJwtBearer(options =>
				   {
					   options.RequireHttpsMetadata = true;
					   options.TokenValidationParameters = authOptions!.GetTokenValidationParameters(true);
				   });
			s.AddAuthorization();

			s.AddCookiePolicy(options =>
			{
				options.HttpOnly = HttpOnlyPolicy.Always;
				options.Secure = CookieSecurePolicy.Always;
			});

		}
	}
}
