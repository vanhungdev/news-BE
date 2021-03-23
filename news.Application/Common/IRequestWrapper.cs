using MediatR;
using news.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace news.Application.Common
{
    public interface IRequestWrapper<T> : IRequest<ResultObject<T>>
    {
    }

    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ResultObject<TOut>>
        where TIn : IRequestWrapper<TOut>
    {
    }
}
