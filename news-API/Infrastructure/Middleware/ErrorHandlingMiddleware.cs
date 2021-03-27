using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace news_API.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                await HandleException(e, context);
            }
        }

        private Task HandleException(Exception e, HttpContext context)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(new
            {
                ErrorCode = statusCode,
                ErrorMessage = e.Message
            }.ToString());

        }
    }
}
