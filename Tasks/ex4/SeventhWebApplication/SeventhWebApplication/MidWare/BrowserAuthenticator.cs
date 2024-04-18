using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SeventhWebApplication.MidWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BrowserAuthenticator
    {
        private readonly RequestDelegate _next;

        public BrowserAuthenticator(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string header = httpContext.Request.Headers["USER-AGENT"];
            if (header.ToLower().Contains("chrome"))
            { return _next(httpContext); }
            else
            {
                throw new AccessViolationException("You need Chrome");
            }
            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BrowserAuthenticatorExtensions
    {
        public static IApplicationBuilder UseBrowserAuthenticator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BrowserAuthenticator>();
        }
    }
}
