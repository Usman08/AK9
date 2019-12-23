using Microsoft.AspNetCore.Builder;

namespace AK9.Admin.Middlewares
{
    public static class MiddlewareExtension
    {
        public static void ConfigureExceptionLoggingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionLoggingMiddleware>();
        }
    }
}
