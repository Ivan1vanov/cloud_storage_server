using CloudStorage.Constants;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace CloudStorage.Config.AppConfiguration
{
    public class AppCorsePolicyConfiguration
    {

        public static void ConfigurePolisies(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                ConfigureDefaultPolicy(options, builder);
            });
        }

        public static void ConfigureDefaultPolicy(CorsOptions options, WebApplicationBuilder builder)
        {
            string policyName = CorsePolicyNames.Default;

            string[] defaultOrigins = builder.Configuration.GetSection($"CorsePolicyConfigurations:Policies:{policyName}").Get<string[]>();

            options.AddPolicy(name: policyName,
                            builder =>
                            {
                                builder.WithOrigins(defaultOrigins)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials();
                            });
        }
    }
}
