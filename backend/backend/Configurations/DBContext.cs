using backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace backend.Configurations
{
    public static class DBContext
    {
        public static void AddCustomDBContext(this IServiceCollection services,IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException($"Connection string \"DefaultConnection\" not found.");

            services.AddDbContext<ContactStoreDBContext>(o =>
                o.UseNpgsql(connectionString)
            );
        }
    }
}
