using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Publicon.Core.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Publicon.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _request;
        //private readonly ILogger<GlobalExceptionMiddleware> _logger;


        public GlobalExceptionMiddleware(RequestDelegate request/*, ILogger<GlobalExceptionMiddleware> logger*/)
        {
            _request = request;
           // _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.ToString());
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = nameof(HttpStatusCode.InternalServerError);
            var statusCode = HttpStatusCode.InternalServerError;
            var message = exception.Message;

            if (exception is UnauthorizedAccessException)
            {
                errorCode = nameof(HttpStatusCode.Unauthorized);
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exception is PubliconException medvoException)
            {
                statusCode = medvoException.ErrorCode.HttpStatusCode;
                errorCode = medvoException.ErrorCode.Message;
                message = string.IsNullOrEmpty(medvoException.Message) ? errorCode : medvoException.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var responseBody = JsonConvert.SerializeObject(new { errorCode, message });
            return context.Response.WriteAsync(responseBody);
        }
    }
}
