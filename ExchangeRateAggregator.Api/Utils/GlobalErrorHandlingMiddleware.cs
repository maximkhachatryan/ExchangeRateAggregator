using ExchangeRateAggregator.ApplicationContracts.Exceptions;
using System.Net;
using System.Text.Json;

namespace ExchangeRateAggregator.Api.Utils
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            //var stackTrace = string.Empty;
            string message;
            var exceptionType = exception.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                //stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                //stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(ApplicationContracts.Exceptions.NotImplementedException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotImplemented;
                //stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(ApplicationContracts.Exceptions.UnauthorizedAccessException))
            {
                message = exception.Message;
                status = HttpStatusCode.Unauthorized;
                //stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                //stackTrace = exception.StackTrace;
            }

            var exceptionResult = JsonSerializer.Serialize(new
            {
                error = message,
                //stackTrace = stackTrace
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
