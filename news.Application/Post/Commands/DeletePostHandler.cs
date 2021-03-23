using Dapper;
using FluentValidation;
using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Post.CommandHandler
{
    public class DeletePostRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
    class DeletePostHandler : IRequestHandler<DeletePostRequest, int>
    {
        private readonly IQuery _query;
        public DeletePostHandler(IQuery query)
        {
            _query = query;
        }

        public async Task<int> Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            string sql = "deletePostById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
    }
    public class DeletePostRequestValidator : AbstractValidator<DeletePostRequest>
    {
        public DeletePostRequestValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
            RuleFor(v => v.Id).NotNull().WithMessage("Id Không được Null");
        }
    }
}
