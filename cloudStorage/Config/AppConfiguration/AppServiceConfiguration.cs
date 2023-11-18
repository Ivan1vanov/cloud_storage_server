using CloudStorage.Helpers;
using CloudStorage.Interfaces;
using CloudStorage.Repositories;
using CloudStorage.Services;

namespace CloudStorage.Config.AppConfiguration
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
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IBCryptHelpers, BCryptHelpers>();
        }
    }
}
