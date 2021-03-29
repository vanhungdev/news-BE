using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using news.Infrastructure.Configuration;
using news.Infrastructure.Database;
using news.Infrastructure.Logging;
using news.Infrastructure.Utilities;
using news_API.Infrastructure.Extensions;
using news_API.Infrastructure.Middleware;
using Serilog;
using System;

namespace news_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration);
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddHttpServices(Configuration);     
            //redis cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Helper.Settings.RedisSettings.ServerRead;
            });

            //auththen JWT
            services.AddCustomAuthentication(Configuration);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {              
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //logging
            app.ApplicationServices.CreateLoggerConfiguration(env);

            //add read appseting.json
            AppSettingServices.Services = app.ApplicationServices;
            // 
            Helper.ConfigureContextAccessor(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            LoggingHelper.Config(app.ApplicationServices.GetRequiredService<IDiagnosticContext>());
            //logging
            app.UseSerilogRequestLogging(opts =>
            {
                opts.EnrichDiagnosticContext = DiagnosticContext.EnrichFromRequest;
            });

            app.UseSerilogRequestLogging();           
            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "news Api");
            });
            app.UseMiddleware<Maintenancemiddleware>();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCustomStaticFiles(env);
            app.UseRouting();       
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
