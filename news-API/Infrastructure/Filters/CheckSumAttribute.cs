using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using news.Infrastructure.Enums;
using news.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace news_API.Infrastructure.Filters
{
    public class CheckSumAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// ChecksumAttribute
        /// </summary>
        /// <param name="Require"></param>
        public CheckSumAttribute(bool Require = false) : base(typeof(CheckSumAttributeImplement))
        {
            Arguments = new object[] { Require };
        }

        /// <summary>
        /// ChecksumAttributeImplement
        /// </summary>
        private class CheckSumAttributeImplement : ActionFilterAttribute
        {
            private readonly bool _require = false;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="require"></param>
            public CheckSumAttributeImplement(bool require)
            {
                _require = require;
            }

            /// <summary>
            /// 
            /// </summary>s
            /// <param name="context"></param>
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                                .Any(en => en.GetType() == typeof(AllowAnonymousAttribute));
                var role = Enum.TryParse<UserRole>(context.HttpContext.User.FindFirstValue(ClaimTypes.Role) ?? "", out var userRole);
                bool rolebyPass = role && (userRole != UserRole.Normal);

                /* by pass
                 * AllowAnonymous:
                 *      default: flase
                 *      optional: true if set _require variable = true
                 * Authorize:
                 *      default: true
                 *      optional: false if Role in (Visitor,Admin,SuperAdmin)
                */
                var byPassChecksum = (hasAllowAnonymous && !_require) || (rolebyPass && !hasAllowAnonymous);
                if (byPassChecksum
                    || (!byPassChecksum && IsValidCheckSum(context.HttpContext.Request)))
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                    var response = ResultObject.Fail("Dữ liệu không toàn vẹn. Vui lòng kiểm tra lại.", code: ResultCode.ErrorChecksumFail);
                    context.Result = new ContentResult()
                    {
                        Content = JsonConvert.SerializeObject(response),
                        ContentType = "application/json; charset=utf-8",
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
            }

            private bool IsValidCheckSum(HttpRequest request)
            {
                return true;
            }

            private async Task<string> ReadRequestBody(HttpRequest request)
            {
                request.EnableBuffering();
                request.Body.Seek(0, SeekOrigin.Begin);
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                var bodyAsText = Encoding.UTF8.GetString(buffer);
                request.Body.Seek(0, SeekOrigin.Begin);
                return bodyAsText;
            }
        }
    }
}
