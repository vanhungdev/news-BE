using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using news_API.Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace news_API.Infrastructure.Extensions
{
    public static class StartupExtensions
    {
        /// <summary>
        /// AddCustomAuthentication
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //string secretKey = configuration.GetValue<string>("AppSettings:SecuritySettings:JwtSecretKey");

            services.AddAuthentication(x =>
            {

                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
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

    }
}
