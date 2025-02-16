using System;
using backend.DataAccess;
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

            string nameConnection = Configuration.GetSection("CustomSettings:ConnectionStringsNameDefault").Value ??
                        "LocalConnection";
            Console.WriteLine("String connection:");
            Console.WriteLine(Configuration.GetConnectionString(nameConnection));
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContactStoreDBContext>(options =>
            {
                string nameConnection = Configuration.GetSection("CustomSettings:ConnectionStringsNameDefault").Value ??
                                        "LocalConnection";
                options.UseNpgsql(Configuration.GetConnectionString(nameConnection));

            });
        }

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{

        //}
    }
}