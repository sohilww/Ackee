using System;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Ackee.AspNetCore.Logger
{
    public static class HostBuilderLoggerExtensions
    {
        public static IHostBuilder UseAckeeLogger(this IHostBuilder builder)
        {

            var path = Environment.CurrentDirectory + "\\bin\\debug\\Logs\\log.json";
            return builder.UseSerilog((context, configuration) =>
            {
                configuration
                    .Enrich.FromLogContext()
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Error)
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.File(new CompactJsonFormatter(), path,
                        LogEventLevel.Information,
                        buffered:true,
                        rollingInterval:RollingInterval.Hour);
            });


        }
    }
}