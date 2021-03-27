using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using news.Infrastructure.Consts;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.Infrastructure.Filters
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Add header
            foreach (var item in new List<OpenApiParameter>
            {
                new OpenApiParameter
                {
                    Name = Consts.HEADER_CHECKSUM,
                    In = ParameterLocation.Header,
                    Required = false,
                }
            })
            {
                operation.Parameters.Add(item);
            }

            // Add authorization field
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ||
                               context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (!hasAuthorize)
            {
                return;
            }

            var jwtbearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
            };
            operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ jwtbearerScheme ] = new string [] { }
                    }
                };
        }
    }
}
