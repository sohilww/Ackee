using Microsoft.AspNetCore.Builder;

namespace Ackee.AspNetCore.ExceptionMiddleware
{
    public static class AckeeMiddlewareExtension
    {
        public static void UseAckeeExceptionMiddleware(this IApplicationBuilder app, int bcCode)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>(bcCode);
        }
    }
}