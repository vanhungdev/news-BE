using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("----- Handling command ({@Command})", request);
            var response = await next();
            _logger.LogInformation("----- Command handled - response: {@Response}", response);
            return response;
        }

        //private object GetValue(object value)
        //{
        //    Type t = value.GetType();
        //    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ResultObject<>))
        //    {
        //        return t.GetProperty("Message").GetValue(value, null);
        //    }
        //    return value;
        //}
    }
}
