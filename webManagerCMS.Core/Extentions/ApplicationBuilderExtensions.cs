using S9.Core.Middlewares;
using webManagerCMS.Core.Middlewares;

namespace webManagerCMS.Core.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            return app;
        }
        public static IApplicationBuilder UseTenant(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantMiddleware>();
            return app;
        }
        public static IApplicationBuilder UseMyRewriteMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyRewriteMiddleware>();
            return app;
        }
    }
}
