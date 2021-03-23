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
    public class DeleteCategoryRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
    class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, int>
    {
        private readonly IQuery _query;
        public DeleteCategoryHandler(IQuery query)
        {
            _query = query;
        }

        public async Task<int> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            string sql = "deleteCategoryById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.String, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
    }
    public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
    {
        public DeleteCategoryRequestValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
            RuleFor(v => v.Id).NotNull().WithMessage("Id Không được Null");       
        }
    }
}
