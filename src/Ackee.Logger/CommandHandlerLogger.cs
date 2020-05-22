using System.Threading.Tasks;
using Ackee.Application;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ackee.Logger
{
    public class LoggingDecoratorCommandHandler<T>: ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly ILogger _logger;

        public LoggingDecoratorCommandHandler(ICommandHandler<T> decorated,ILogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }
        public async Task Handle(T command)
        {
            _logger.LogInformation("command data is {@Command}",command);
           
            await _decorated.Handle(command);
        }
    }
}
