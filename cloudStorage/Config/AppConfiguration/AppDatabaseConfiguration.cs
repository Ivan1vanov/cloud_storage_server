using CloudStorage.Contexts;
using CloudStorage.Models;
using CloudStorage.Utils;
using Microsoft.EntityFrameworkCore;

namespace CloudStorage.Config.AppConfiguration
{
    public class AppDatabaseConfiguration
    {
        public static void ConfigureDatabase(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<MsDatabaseContext>(options => options.UseSqlServer(
                GetMysqlServerConnectionString(builder.Configuration)
            ));
        }

        public static string GetMysqlServerConnectionString(ConfigurationManager configuration)
        {
            var dbSettings = new MysqlServerDatabaseConnectionData();
            configuration.GetSection("DatabaseSettings:MysqlServer").Bind(dbSettings);

            var validationResult = ObjectUtils.CheckForNullProperties(dbSettings);

            if (validationResult.HasNullProperties)
            {
                string missingproperties = string.Join(", ", validationResult.NullProperties);
                throw new InvalidOperationException($"Missing following properties to connect to database: {missingproperties}");
            }

            return $"Server={dbSettings.Server},{dbSettings.Port};Initial Catalog={dbSettings.Database};User ID={dbSettings.User};Password={dbSettings.Password};TrustServerCertificate=true";
        }
    }
}
