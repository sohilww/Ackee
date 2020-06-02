namespace Ackee.AspNetCore.ExceptionMiddleware
{
    public class ErrorDetails
    {
        public ErrorDetails(string message, long code)
        {
            Message = message;
            Code = code;
        }
        public static ErrorDetails Build(string message, long errorCode, int bcCode)
        {
            return new ErrorDetails(message, (errorCode + bcCode));
        }
        public string Message { get; set; }
        public long Code { get; set; }
    }
}