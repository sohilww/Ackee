using Microsoft.AspNetCore.Builder;

namespace Ackee.AspNetCore.ExceptionMiddleware
{
    public static class AckeeMiddlewareExtension
    {
        public static IApplicationBuilder UseAckeeExceptionMiddleware(this IApplicationBuilder app, int bcCode)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>(bcCode);
        }
    }
}