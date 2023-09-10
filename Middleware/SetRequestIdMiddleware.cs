namespace CloudStorage.Middlewares
{
    public class SetRequestIdMiddleware {

        RequestDelegate _next;

        public SetRequestIdMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {

            var requestId = Guid.NewGuid().ToString();
            
            context.Request.Headers["X-Request-ID"] = requestId;

            await _next(context);
        }
    }
}