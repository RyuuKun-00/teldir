

using backend.Application.Services;
using backend.DataAccess;
using backend.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(o =>
            {
                o.Listen(IPAddress.Parse("0.0.0.0"), 4001);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException($"Connection string \"DefaultConnection\" not found.");

            var withOrigins = builder.Configuration.GetSection("WithOrigins").Get<string[]>() ??
                throw new InvalidOperationException($"Configuration \"WithOrigins\" not found.");

            builder.Services.AddDbContext<ContactStoreDBContext>(options =>
                options.UseNpgsql(connectionString)
            );
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddScoped<IContactService, ContactService>();

            var app = builder.Build();

            

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI();
                app.UseSwagger();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseCors(config =>
            {
                config.WithHeaders().AllowAnyHeader();
                config.WithMethods().AllowAnyMethod();
                config.WithOrigins(withOrigins);
            });
            app.MapControllers();

            app.Run();
        }
    }
}
