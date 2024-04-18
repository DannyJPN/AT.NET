using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SeventhWebApplication.MidWare;
using SeventhWebApplication.Services;

namespace SeventhWebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ExceptionCatcher>();
            app.UseMiddleware<BrowserAuthenticator>();
            app.UseMiddleware<StaticFileSender>();
            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                
                await context.Response.WriteAsync("<html><head><title>FirstPage</title></head><body></body></html>");
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ExceptionHandler>();
            services.AddScoped<Ifaces.ILogger,TxtLogger>();

        }



    }
}
