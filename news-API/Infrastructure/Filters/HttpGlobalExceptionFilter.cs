using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using news.Infrastructure.Enums;
using news.Infrastructure.Exceptions;
using news.Infrastructure.Logging;
using news.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace news_API.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string errorId = (Convert.ToBase64String(Guid.NewGuid().ToByteArray())).Replace("=", "");
            string message = $"Đã xảy ra lỗi trong quá trình xử lý ({errorId}).";
            LoggingHelper.SetProperty("ErrorId:", errorId);
            string errorMessage = context.Exception.ToString();

            // TODO
            if (context.Exception.GetType() == typeof(CustomValidationException))
            {
                var validationException = (CustomValidationException)context.Exception;
                message = string.Join(" | ", validationException.Errors.SelectMany(x => x.Value));
            }
            var response = ResultObject.Error(message, errorMessage, code: ResultCode.ErrorException);
            context.Result = new ContentResult()
            {
                Content = JsonConvert.SerializeObject(response),
                ContentType = "application/json; charset=utf-8",
                StatusCode = (int)HttpStatusCode.OK
            };
            context.ExceptionHandled = true;
        }
    }
}
