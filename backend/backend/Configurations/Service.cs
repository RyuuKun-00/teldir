using backend.Application.Services;
using backend.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace backend.Configurations
{
    public static class Service
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            var s = builder.Services;

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

            s.AddAuthentication();
            s.AddAuthorization();
           
        }
    }
}
