using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using news.Infrastructure.Enums;
using news.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.Infrastructure.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }
            var validationErrors = string.Join(" | ", context.ModelState
                .Keys
                .SelectMany(k => context.ModelState[k].Errors)
                .Select(e => e.ErrorMessage)
                .ToArray());

            var response = ResultObject.Error(
                "Thông tin không hợp lệ! Vui lòng kiểm tra lại.",
                validationErrors,
                code: ResultCode.ErrorInputInvalid);

            context.HttpContext.Response.StatusCode = 200;
            context.Result = new ContentResult()
            {
                Content = JsonConvert.SerializeObject(response),
                ContentType = "application/json; charset=utf-8"
            };
        }

    }
}
