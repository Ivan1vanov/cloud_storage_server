using CloudStorage.Helpers;
using CloudStorage.Interfaces;
using CloudStorage.Repositories;
using CloudStorage.Services;

namespace CloudStorage.Config
{
    public class AppServiceConfiguration
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            builder.Services.AddScoped<IDokumentService, DokumentService>();
            builder.Services.AddScoped<IDokumentRepository, DokumentRepository>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IBCryptHelpers, BCryptHelpers>();
        }
    }
}