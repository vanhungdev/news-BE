using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using news.Database;
using news.Infrastructure.Configuration;
using news.Infrastructure.Database;
using news_API.Infrastructure.Extensions;
using news_API.Services;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using MediatR.Pipeline;
using news.Application.Behaviours;
using FluentValidation;
using news.Application.Common;
using Microsoft.OpenApi.Models;

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
            services.AddControllers();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<IQuery, Sqlsever>();
            services.AddControllersWithViews();

            // meidaTR
            services.AddApplication();

            // Swagger
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "newss",Version ="v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer -token-')",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer"
                 });
            });

            
            services.AddScoped<IUserService, UserService>();
            //redis DB
            services.AddScoped<IRedisCacheDB, RedisCacheDB>();

            //redis cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });

            //CorsPolicy
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

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
            //app.UseStaticFiles();
            app.UseSerilogRequestLogging(); // <-- Add this line //app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "news Api");
            });

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
