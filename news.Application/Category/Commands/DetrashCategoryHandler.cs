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

namespace news.Application.Category.Commands
{
    public class DeTrashCategoryRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
    class DetrashCategoryHandler : IRequestHandler<DeTrashCategoryRequest, int>
    {
        private readonly IQuery _query;
        public DetrashCategoryHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<int> Handle(DeTrashCategoryRequest request, CancellationToken cancellationToken)
        {
            string sql = "deTrashTopicById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
    }
    public class DeTrashCategoryRequestValidator : AbstractValidator<DeTrashCategoryRequest>
    {
        public DeTrashCategoryRequestValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
            RuleFor(v => v.Id).NotNull().WithMessage("Id Không được Null");
        }
    }
}
