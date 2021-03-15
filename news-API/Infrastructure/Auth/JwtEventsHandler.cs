using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using news.Infrastructure.Configuration;
using news.Infrastructure.Consts;
using news.Infrastructure.Enums;
using news.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;


namespace news_API.Infrastructure.Auth
{
    public class JwtEventsHandler
    {
  
        /// <summary>
        /// Invoked after the security token has passed validation and a ClaimsIdentity has been generated.
        /// </summary>
        /// <returns></returns>
        public static Func<TokenValidatedContext, Task> OnTokenValidatedHandler()
        {
            return context =>
            {
                if ( !context.HttpContext.Request.Headers.TryGetValue(Consts.HEADER_AUTHORIZATION, out var mbsExternal))
                {
                    context.Fail("The authorization header is empty.");
                    return Task.CompletedTask;
                }

                //string token = !string.IsNullOrWhiteSpace(mbsExternal) ? mbsExternal : mbsInternal;
                //if (JwtAuthManager.IsExistInBlacklist(context.Principal.FindFirstValue(Consts.CLAIM_USERNAME) ?? string.Empty, token, string.Empty))
                //{
                //    context.Fail("Token is blocked.");
                //    return Task.CompletedTask;
                //}

                var claims = new[] {
                    new Claim(ClaimTypes.Role,
                        ((int)Enum.Parse(typeof(UserRole), context.Principal.FindFirstValue(Consts.CLAIM_USERTYPE)
                        ?? UserRole.Normal.ToString())).ToString())
                };
                var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                context.Principal.AddIdentity(identity);
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// Invoked when a protocol message is first received.
        /// </summary>
        /// <returns></returns>
        public static Func<MessageReceivedContext, Task> OnMessageReceivedHandler()
        {
            return context =>
            {
                if (context.HttpContext.Request.Headers.TryGetValue(Consts.HEADER_AUTHORIZATION, out var mbsExternal))
                {
                    context.Token = mbsExternal;
                }

                if (context.HttpContext.Request.Headers.TryGetValue(Consts.HEADER_PARTNER, out var mbsPartner)
                    && string.IsNullOrWhiteSpace(mbsExternal))
                {
                    var partnerKey = "PartnerKey";
                    if (partnerKey.Equals(mbsPartner, StringComparison.OrdinalIgnoreCase))
                    {
                        var claims = new Claim[]
                        {
                            new Claim(ClaimTypes.Role, ((int)Enum.Parse(typeof(UserRole), UserRole.Visitor.ToString())).ToString())
                        };
                        context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
                        context.Success();
                        return Task.CompletedTask;
                    }
                }

                return Task.CompletedTask;
            };
        }
        /// <summary>
        /// Invoked before a challenge is sent back to the caller.
        /// </summary>
        /// <returns></returns>
        public static Func<JwtBearerChallengeContext, Task> OnChallengeHandler()
        {
            return context =>
            {
                context.HandleResponse();
                var response = ResultObject.Error("Unauthorized.",
                    context.AuthenticateFailure?.ToString() ?? string.Empty,
                    code: ResultCode.ErrorAuthenticationFail);

                if (context.AuthenticateFailure?.GetType() == typeof(SecurityTokenExpiredException))
                {
                    response.Code = ResultCode.ErrorTokenExpired;
                    response.Message.Message = "Phiên làm việc của bạn đã kết thúc. Vui lòng đăng nhập lại.";
                }

                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response)).Wait();
                return Task.CompletedTask;
            };
        }
        /// <summary>
        /// Invoked if exceptions are thrown during request processing. The exceptions will be re-thrown after this event unless suppressed.
        /// </summary>
        /// <returns></returns>
        public static Func<AuthenticationFailedContext, Task> OnAuthenticationFailedHandler()
        {
            return context =>
            {
                return Task.CompletedTask;
            };
        }
        /// <summary>
        /// Invoked if Authorization fails and results in a Forbidden response
        /// </summary>
        /// <returns></returns>
        public static Func<ForbiddenContext, Task> OnForbiddenHandler()
        {
            return context =>
            {
                var response = ResultObject.Error("Unauthorized.", "OnForbiddenHandler", code: ResultCode.ErrorAuthenticationFail);
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response)).Wait();
                return Task.CompletedTask;
            };
        }
    }
}
