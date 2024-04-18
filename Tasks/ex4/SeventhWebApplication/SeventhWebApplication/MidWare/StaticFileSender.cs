using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SeventhWebApplication.MidWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class StaticFileSender
    {
        private readonly RequestDelegate _next;

        public StaticFileSender(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext,IHostingEnvironment ihenv)
        {

            // await _next(httpContext);
            string rqpath = httpContext.Request.Path;

            // string dirpath = @"C:\Users\kru0142\Desktop\AT.NET\Tasks\ex4\SeventhWebApplication\SeventhWebApplication\Pictures";
            string dirpath = Path.Combine(ihenv.ContentRootPath, "Pictures");


            string finalpath = Path.Combine(dirpath, rqpath.TrimStart('/', '.', '\\'));
            FileInfo f = new FileInfo(finalpath);
            if (f.Exists)
            {
                httpContext.Response.ContentType = "image/png";
                await httpContext.Response.SendFileAsync(f.FullName);

            }
            else
            {
                //    httpContext.Response.ContentType = "text/html; charset=utf-8";
                // await httpContext.Response.WriteAsync(String.Format("StaticSender works for path {0},but file not found",rqpath));
                throw new FileNotFoundException(String.Format("The requested file {0} was not found",finalpath));

            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class StaticFileSenderExtensions
    {
        public static IApplicationBuilder UseStaticFileSender(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<StaticFileSender>();
        }
    }
}
