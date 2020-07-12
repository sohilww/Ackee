using System;
using System.Net;
using System.Threading.Tasks;
using Ackee.Core;
using Ackee.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Ackee.AspNetCore.ExceptionMiddleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _bcCode;

        public ExceptionHandlerMiddleware(RequestDelegate next, BcConfig config)
        {
            _next = next;
            _bcCode = config.Code;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception exception)
            {
                await HandleException(httpContext, exception);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception exception)
        {
            if (exception is UnauthorizedAccessException)
                await HandleUnAuthorizeException(httpContext, exception);
            else if (exception is AckeeException ackeeException)
                await HandleBusinessException(httpContext, ackeeException);
            else
                await HandleDefaultException(httpContext);
        }

        private async Task HandleBusinessException(HttpContext httpContext, AckeeException ackeeException)
        {
            var error = AckeeExceptionHandler.CreateErrorDetail(ackeeException, _bcCode);
            await WriteToResponse(httpContext, error);
        }

        private async Task HandleDefaultException(HttpContext httpContext)
        {
            var error = new ErrorDetails("unhandled exception", _bcCode);
            await WriteToResponse(httpContext, error);
        }
        private async Task HandleUnAuthorizeException(HttpContext httpContext, Exception exception)
        {
            var error = ErrorDetails.Build(exception.Message, 0, _bcCode);
            await WriteToResponse(httpContext, error, HttpStatusCode.Forbidden);
        }

        private static async Task WriteToResponse(HttpContext httpContext, ErrorDetails error, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}