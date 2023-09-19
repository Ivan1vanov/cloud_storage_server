using CloudStorage.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CloudStorage.Config
{
    public class AppDatabaseConfiguration
    {
        public static void ConfigureDatabase(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DbConnection")
            ));
        }
    }
}