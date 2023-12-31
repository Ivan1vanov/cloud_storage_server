namespace CloudStorage.Config.AppConfiguration
{
    public class AppCloudinaryConfiguration
    {
        public static void ConfigureCloudinary(WebApplicationBuilder builder)
        {
            var cloudinaryConfigSection = builder.Configuration.GetSection("CloudinaryConfig");
            builder.Services.Configure<CloudinaryConfig>(cloudinaryConfigSection);
        }
    }
}
