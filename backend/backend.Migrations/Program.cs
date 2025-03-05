using backend.DataAccess;
using backend.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace backend.Migrations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Created database");
            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ConsoleStartup>()
                .Build();

            using (var context = (ContactStoreDBContext?)webHost.Services.GetService(typeof(ContactStoreDBContext)))
            {
                if(context == null) throw new InvalidOperationException($"Context is null.");
                context.Database.EnsureCreated();

                context.Init();
                
            }
            Console.WriteLine("Done");
        }
    }
}
