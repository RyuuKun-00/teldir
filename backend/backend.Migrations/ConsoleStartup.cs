using System;
using backend.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace backend.Migrations
{
    public class ConsoleStartup
    {
        public ConsoleStartup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            var connectionString = Configuration.GetConnectionString("DefaultConnection") ??
                 throw new InvalidOperationException($"Connection string \"DefaultConnection\" not found.");
            Console.WriteLine("String connection:"+ connectionString);

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContactStoreDBContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));


            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

    }
}