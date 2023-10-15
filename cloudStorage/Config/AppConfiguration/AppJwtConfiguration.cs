using System.Text;
using CloudStorage.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CloudStorage.Config.AppConfiguration
{
    public class AppJwtConfiguration
    {

        public static void ConfigureJwtAuthProcess(WebApplicationBuilder builder)
        {
            var jwtConfigSection = builder.Configuration.GetSection("JwtConfig");

            builder.Services.Configure<JwtConfig>(jwtConfigSection);

            builder.Services.AddAuthentication(ConfigureAuthentication)
                .AddCookie("Cookies", ConfigureAuthenticationCookies)
                .AddJwtBearer(options => ConfigureJwtBearer(options, jwtConfigSection.Get<JwtConfig>()));
        }

        private static void ConfigureAuthentication(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        }

        private static void ConfigureAuthenticationCookies(CookieAuthenticationOptions options)
        {
            options.Cookie.Name = CookieKeyNames.access_token;
        }

        private static void ConfigureJwtBearer(JwtBearerOptions options, JwtConfig jwtConfig)
        {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies[CookieKeyNames.access_token];
                    return Task.CompletedTask;
                }
            };

            byte[] accessTokenKeyBytes = Encoding.UTF8.GetBytes(jwtConfig.AccessTokenKey);

            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(accessTokenKeyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
            };
        }
    }
}
