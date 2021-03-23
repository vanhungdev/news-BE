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

namespace news.Application.Post.Queries
{
    public class GetPostByIdRequest : IRequest<Entities.Post>
    {
        public int Id { get; set; }
    }
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdRequest, Entities.Post>
    {
        private readonly IQuery _query;
        public GetPostByIdHandler(IQuery query)
        {
            _query = query;
        }
        public Task<Entities.Post> Handle(GetPostByIdRequest request, CancellationToken cancellationToken)
        {
            //sp
            string sql = "findPostById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            var listProduct = _query.Query<Entities.Post>(1, sql, parameter).FirstOrDefault();
            return Task.FromResult(listProduct);
        }
    }
    public class GetPostByIdRequestValidator : AbstractValidator<GetPostByIdRequest>
    {
        public GetPostByIdRequestValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
            RuleFor(v => v.Id).NotNull().WithMessage("Id Không được Null"); ;
        }
    }
}
