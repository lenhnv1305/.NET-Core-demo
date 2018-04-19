using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GettingStarted
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Map is only used one time when url matches the param text 
            app.Map("/index", MapIndex);
            app.Map("/manage", MapManagePost);

            // Use func is only called 1 time whether you can clone the other Use func
            // Use will not proceed the next pepline
            app.Use(async (context, next) => {
                await context.Response.WriteAsync("Start");
                await context.Response.WriteAsync("End");
            });

            app.Use(async (context, next) => {
                await context.Response.WriteAsync("Start");
                await next.Invoke();
                await context.Response.WriteAsync("End");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
        private void MapIndex(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Map index path");
            });
        }
        private void MapManagePost(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Map manage post");
            });
        }
    }
}
