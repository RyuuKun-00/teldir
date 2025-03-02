using System.Net;

namespace backend.Configurations
{
    public static class Kestrel
    {
        public static void ConfigureKestrel(this WebApplicationBuilder builder)
        {

            int port = Convert.ToInt32(CustomServiceEnv.GetEnv("BACKEND_PORT"));

            builder.WebHost.ConfigureKestrel(o =>
            {
                o.Listen(IPAddress.Parse("0.0.0.0"), port);
            });
        }
    }
}
