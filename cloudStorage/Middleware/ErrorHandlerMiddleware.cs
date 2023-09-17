using System.Net;

namespace CloudStorage.Middlewares
{
    public class ErrorHandlerMiddleware
    {

        RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var requestId = context.Request.Headers["X-Request-ID"];
                Console.WriteLine($"Error: {error.Message}");
                Console.WriteLine($"StackTrace: {error.StackTrace}");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"Unexpected error occurs. Request ID: {requestId}");
            }
        }
    }
}