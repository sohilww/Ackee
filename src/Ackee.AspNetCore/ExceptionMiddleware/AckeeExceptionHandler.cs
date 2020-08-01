using Ackee.Core;
using Ackee.Core.Exceptions;

namespace Ackee.AspNetCore.ExceptionMiddleware
{
    public class AckeeExceptionHandler
    {
        internal static ErrorDetails CreateErrorDetail(AckeeException exception,BcConfig config)
        {
            return HandleAckeeException(exception,config);
        }

        private static ErrorDetails HandleAckeeException(AckeeException exception, BcConfig config)
        {
            if (exception is IInternalException)
                return HandleInternalException(exception);
            return HandleBoundedContextException(exception,config);

        }

        private static ErrorDetails HandleInternalException(AckeeException exception)
        {
            return ErrorDetails.Build(exception);
        }

        private static ErrorDetails HandleBoundedContextException(AckeeException exception, BcConfig config)
        {
            return ErrorDetails.Build(exception, config);
        }
    }
}