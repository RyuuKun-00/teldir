using backend.Configurations;
using System.Collections;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureKestrel();

            builder.AddServices();

            

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI();
                app.UseSwagger();
            }

            app.UseCors("CustomCors");

            app.MapControllers();

            app.Run();
        }
    }
}
