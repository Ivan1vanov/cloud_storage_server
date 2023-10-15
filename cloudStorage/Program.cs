using CloudStorage.Config.AppConfiguration;

var builder = WebApplication.CreateBuilder(args);

AppCorsePolicyConfiguration.ConfigurePolisies(builder);
AppServiceConfiguration.ConfigureServices(builder);
AppDatabaseConfiguration.ConfigureDatabase(builder);
AppCloudinaryConfiguration.ConfigureCloudinary(builder);
AppJwtConfiguration.ConfigureJwtAuthProcess(builder);

var app = builder.Build();
AppUseCorsPolicies.UseCorsPolicies(app, builder.Configuration);

app.UseAuthentication();
app.UseAuthorization();

AppMigrationsSettings.ApplyAppMigrations(app);
AppMiddlewaresConfiguration.ConfigureMiddlewares(app);

app.MapControllers();
app.Run();
