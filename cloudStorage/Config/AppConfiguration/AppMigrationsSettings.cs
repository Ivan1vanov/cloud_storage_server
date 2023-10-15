using CloudStorage.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CloudStorage.Config.AppConfiguration
{
    public static class AppMigrationsSettings
    {
        public static void ApplyAppMigrations(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                ApplyMigrationForContext(serviceScope.ServiceProvider.GetService<MsDatabaseContext>());
            }
        }

        public static void ApplyMigrationForContext<TContext>(TContext context) where TContext : DbContext
        {
            string contextName = typeof(TContext).Name;
            Console.WriteLine($"Applying migrations for DbContext: {contextName}");

            context.Database.Migrate();
        }
    }
}
