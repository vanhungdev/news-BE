using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using news.Application.Behaviours;
using news.Database;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace news.Application.Common
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // system config
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            //
            //services.AddSingleton<IQuery, Sqlsever>();
            return services;
        }
    }
}
