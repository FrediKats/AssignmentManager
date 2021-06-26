using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssignmentManager.Server.Middleware
{
    public class CustomErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomErrorHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        
        
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetErrorCode(exception);
 
            return context.Response.WriteAsync(new ExceptionInfo()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }

        private static HttpStatusCode GetErrorCode(Exception ex)
        {
            switch (ex)
            {
                case NullReferenceException _:
                    return HttpStatusCode.NotFound;
                case FormatException _:
                    return HttpStatusCode.BadRequest;
                case ValidationException _:
                    return HttpStatusCode.BadRequest;
                case AuthenticationException _:
                    return HttpStatusCode.Forbidden;
                case NotImplementedException _:
                    return HttpStatusCode.NotImplemented;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
