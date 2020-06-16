using Ackee.Core.Exceptions;

namespace Ackee.AspNetCore.ExceptionMiddleware
{
    public class AckeeExceptionHandler
    {
        internal static ErrorDetails CreateErrorDetail(AckeeException exception,int bcCode)
        {
            return HandleAckeeException(exception,bcCode);
        }

        private static ErrorDetails HandleAckeeException(AckeeException exception, int bcCode)
        {
            if (exception is IInternalException)
                return HandleInternalException(exception);
            return HandleBoundedContextException(exception,bcCode);

        }

        private static ErrorDetails HandleInternalException(AckeeException exception)
        {
            return ErrorDetails.Build(exception.Message, exception.Code);
        }

        private static ErrorDetails HandleBoundedContextException(AckeeException exception, int bcCode)
        {
            return ErrorDetails.Build(exception.Message, exception.Code, bcCode);
        }
    }
}