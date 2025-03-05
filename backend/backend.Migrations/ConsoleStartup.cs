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
        const string nameConnection = "DefaultConnection";
        public ConsoleStartup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            var connectionString = Configuration.GetConnectionString(nameConnection) ??
                 throw new InvalidOperationException($"Connection string \"{nameConnection}\" not found.");
            Console.WriteLine("String connection:"+ connectionString);

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContactStoreDBContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString(nameConnection));
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

    }
}