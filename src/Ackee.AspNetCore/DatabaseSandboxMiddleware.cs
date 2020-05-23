using System.Threading.Tasks;
using Ackee.DataAccess;
using Microsoft.AspNetCore.Http;

namespace Ackee.AspNetCore
{
    public class DatabaseSandboxMiddleware
    {
        private readonly RequestDelegate _next;

        public DatabaseSandboxMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IConnectionStringResolver connectionStringResolver)
        {
            var connectionStringHeaders = "databaseSandboxConnection";
            bool hasConnectionStringHeaders = context.Request.Headers.ContainsKey(connectionStringHeaders);
            if (hasConnectionStringHeaders)
            {
                connectionStringResolver.Set(context.Request.Headers[connectionStringHeaders]);
            }
            await _next.Invoke(context);

        }
    }
}