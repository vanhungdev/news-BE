using Microsoft.AspNetCore.Http;
using news.Infrastructure.Configuration;
using news.Infrastructure.Enums;
using news.Infrastructure.Models;
using news.Infrastructure.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.Infrastructure.Middleware
{
    public class Maintenancemiddleware
    {
        private readonly RequestDelegate _next;
        public Maintenancemiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (Helper.Settings.CommonSettings.IsMaintain == 1)
                {
                    await HandleResponse(context.Response);
                }
                else
                {
                    await _next(context);
                }
            }
            catch
            {
                await _next(context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private Task HandleResponse(HttpResponse response)
        {
            var responseData = ResultObject.Fail(
                "Hệ thống đang bảo trì, vui lòng quay lại sau.",
                code: ResultCode.ErrorMaintenance
                );
            string jsonData = JsonConvert.SerializeObject(responseData);
            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = 200;
            return response.WriteAsync(jsonData);
        }
    }
}
