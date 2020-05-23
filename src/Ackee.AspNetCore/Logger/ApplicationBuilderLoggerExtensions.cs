using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Ackee.AspNetCore.Logger
{
    public static class ApplicationBuilderLoggerExtensions
    {
        public static IApplicationBuilder UseAckeeRequestLogging(
            this IApplicationBuilder app)
        {

            return app.UseSerilogRequestLogging(opt =>
            {
                opt.EnrichDiagnosticContext = (context, httpContext) =>
                {
                    context.Set("UserName", "system");
                    context.Set("Client IP",httpContext.Connection.RemoteIpAddress.ToString());
                };
            });
            

        }
    }
}