using Dapper;
using FluentValidation;
using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.User.QueryHandler
{
    public class GetUserByIdRequest : IRequest<Entities.User>
    {
        public int Id { get; set; }
    }
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, Entities.User>
    {
        private readonly IQuery _query;
        public GetUserByIdHandler(IQuery query)
        {
            _query = query;
        }
        public Task<Entities.User> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {       
            string sql = "getUserById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            var listProduct = _query.Query<Entities.User>(1, sql, parameter).FirstOrDefault();
            return Task.FromResult(listProduct);
        }
    }
    public class GetUserByIdRequestValidator : AbstractValidator<GetUserByIdRequest>
    {
        public GetUserByIdRequestValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
            RuleFor(v => v.Id).NotNull().WithMessage("Id Không được Null"); ;
        }
    }
}
