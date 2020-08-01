using Ackee.Core.Exceptions;

namespace Ackee.Core
{
    public interface IExceptionHandler
    {
        int GetCode(AckeeException exception);
    }

    public class ExceptionHandler :IExceptionHandler
    {
        private readonly BcConfig _config;

        public ExceptionHandler(BcConfig config)
        {
            _config = config;
        }
        public int GetCode(AckeeException exception)
        {
            return _config.Code + exception.Code;
        }
    }
}