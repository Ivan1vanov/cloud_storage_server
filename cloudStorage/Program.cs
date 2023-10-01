using CloudStorage.Config;

var builder = WebApplication.CreateBuilder(args);

AppServiceConfiguration.ConfigureServices(builder);
AppDatabaseConfiguration.ConfigureDatabase(builder);
AppCloudinaryConfiguration.ConfigureCloudinary(builder);
AppJwtConfiguration.ConfigureJwtAuthProcess(builder);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
AppMigrationsSettings.ApplyAppMigrations(app);
AppMiddlewaresConfiguration.ConfigureMiddlewares(app);

app.MapControllers();

app.Run();