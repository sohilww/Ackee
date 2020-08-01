using System;
using Ackee.Core;
using Ackee.Core.Exceptions;

namespace Ackee.AspNetCore.ExceptionMiddleware
{
    public class ErrorDetails
    {
        public ErrorDetails(string message, long code=0)
        {
            Message = message;
            Code = code;
        }
        public static ErrorDetails Build(AckeeException message, BcConfig config)
        {
            return new ErrorDetails(message.Message, new ExceptionHandler(config).GetCode(message));
        }
        public static ErrorDetails Build(AckeeException message)
        {
            return new ErrorDetails(message.Message, message.Code);
        }
        public static ErrorDetails Build(Exception message)
        {
            return new ErrorDetails(message.Message);
        }
        public string Message { get; set; }
        public long Code { get; set; }
    }
}