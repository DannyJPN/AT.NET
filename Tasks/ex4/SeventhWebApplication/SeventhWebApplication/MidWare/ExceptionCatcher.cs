using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SeventhWebApplication.Services;

namespace SeventhWebApplication.MidWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionCatcher
    {
        private readonly RequestDelegate _next;

        public ExceptionCatcher(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ExceptionHandler exchandler)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                httpContext.Response.ContentType = "text/html; charset=utf-8";
                await httpContext.Response.WriteAsync(String.Format("Exception works for path {0},but file not found", httpContext.Request.Path));
                exchandler.Handle(e);
            }
        }

    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionCatcherExtensions
    {
        public static IApplicationBuilder UseExceptionCatcher(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionCatcher>();
        }
    }
}
