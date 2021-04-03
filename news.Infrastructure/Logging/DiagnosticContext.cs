using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace news.Infrastructure.Logging
{
    public static class DiagnosticContext
    {
        public static void EnrichFromRequest(
            IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;
            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("SourceType", "USER-LOG");
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);
            if (request.QueryString.HasValue) diagnosticContext.Set("QueryString", request.QueryString.Value);
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);
            diagnosticContext.Set("User", httpContext.User?.FindFirstValue(Consts.Consts.CLAIM_USERNAME) ?? "None");
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is object)
            {
                string folder = httpContext.Request.Path.Value.Replace(@"/", "_");
                diagnosticContext.Set("LogFolder", folder.ToLower());
                diagnosticContext.Set("EndpointName", httpContext.Request.Path);
            }
        }
    }
}
