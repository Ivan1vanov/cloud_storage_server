using CloudStorage.Middlewares;

namespace CloudStorage.Config.AppConfiguration
{
    public class AppMiddlewaresConfiguration
    {

        public static void ConfigureMiddlewares(WebApplication app)
        {
            app.UseMiddleware<SetRequestIdMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
