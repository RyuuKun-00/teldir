

using backend.Application.Services;
using backend.DataAccess;
using backend.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException($"Connection string \"DefaultConnection\" not found.");



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

            app.MapControllers();

            app.UseCors(config =>
            {
                config.WithHeaders().AllowAnyHeader();
                config.WithMethods().AllowAnyMethod();
                config.WithOrigins("http://localhost:4002");
            });

            app.Run();
        }
    }
}
