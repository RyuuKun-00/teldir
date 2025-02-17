﻿using backend.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace backend.Migrations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Applying migrations");
            var webHost = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<ConsoleStartup>()
                .Build();

            using (var context = (ContactStoreDBContext?)webHost.Services.GetService(typeof(ContactStoreDBContext)))
            {
                if(context == null) throw new InvalidOperationException($"Context is null.");
                context.Database.Migrate();
            }
            Console.WriteLine("Done");
        }
    }
}
