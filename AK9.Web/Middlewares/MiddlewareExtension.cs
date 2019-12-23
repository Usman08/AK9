using Microsoft.AspNetCore.Builder;

namespace AK9.Web.Middlewares
{
    public static class MiddlewareExtension
    {
        public static void ConfigureExceptionLoggingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionLoggingMiddleware>();
        }
    }
}
