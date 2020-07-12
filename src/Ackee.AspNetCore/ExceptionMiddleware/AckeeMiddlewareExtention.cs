using Microsoft.AspNetCore.Builder;

namespace Ackee.AspNetCore.ExceptionMiddleware
{
    public static class AckeeMiddlewareExtension
    {
        public static IApplicationBuilder UseAckeeExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}