using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using news.Application.Common;
using news.Database;
using news.Infrastructure.Configuration;
using news.Infrastructure.Consts;
using news.Infrastructure.Database;
using news_API.Infrastructure.Auth;
using news_API.Infrastructure.Filters;
using news_API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace news_API.Infrastructure.Extensions
{
    public static class StartupExtensions
    {

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));              
                options.Filters.Add(typeof(CheckSumAttribute));
                options.Filters.Add(typeof(ValidateModelStateFilter));

            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "API Document."
                });
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = Consts.HEADER_AUTHORIZATION,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập JWT",
                });
                c.OperationFilter<SwaggerOperationFilter>();
                //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                c.CustomSchemaIds(i => i.FullName);
                c.UseInlineDefinitionsForEnums();
            });
            return services;
        }
        /// <summary>
        /// AddCustomAuthentication
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                //set default JwtBearer for authen
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                //configure the JWT Bearer token
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Convert.ToBase64String(Encoding.ASCII.GetBytes("abc1234567899sdasdfsdgsdg")))),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = JwtEventsHandler.OnMessageReceivedHandler(),
                    OnTokenValidated = JwtEventsHandler.OnTokenValidatedHandler(),
                    OnChallenge = JwtEventsHandler.OnChallengeHandler(),
                    OnForbidden = JwtEventsHandler.OnForbiddenHandler()
                };
            });

            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>

        public static IApplicationBuilder UseCustomStaticFiles(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Logs")),
                RequestPath = "/demo/logs"
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Logs")),
                RequestPath = "/demo/logs"
            });

            return app;
        }
        public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
        {
            //mediatR
            services.AddApplication();
            //data
            services.AddSingleton<IQuery, Sqlsever>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRedisCacheDB, RedisCacheDB>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptionsMonitor<AppSettings>>().CurrentValue);
            return services;
        }
    }
}
