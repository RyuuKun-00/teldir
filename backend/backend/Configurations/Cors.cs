namespace backend.Configurations
{
    public static class Cors
    {
        public static void AddCustomCors(this IServiceCollection services)
        {
            var portFrontend = CustomServiceEnv.GetEnv("FRONTEND_PORT");
            var portNginx = CustomServiceEnv.GetEnv("NGINX_PORT");

            string[] withOrigins = [
                $"http://localhost:{portFrontend}", 
                $"http://localhost:{portNginx}"
                ];

            services.AddCors(o =>
            {
                o.AddPolicy("CustomCors", builder =>
                {
                    builder.WithHeaders().AllowAnyHeader();
                    builder.WithMethods().AllowAnyMethod();
                    builder.WithOrigins(withOrigins);
                });
            });

        }

    }
}
