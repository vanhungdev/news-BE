using Dapper;
using FluentValidation;
using MediatR;
using news.Application.Common;
using news.Database;
using news.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Category.Commands
{
    public class ChangeStatusCategoryRequest : IRequest<int>
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }
    public class ChangeStatusHandler : IRequestHandler<ChangeStatusCategoryRequest, int >
    {
        private readonly IQuery _query;
        public ChangeStatusHandler(IQuery query)
        {
            _query = query;
        }


        public async Task<int> Handle(ChangeStatusCategoryRequest request, CancellationToken cancellationToken)
        {
            //SQL stored procedure 
            string sql = "changeStatusTopic";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", request.Status, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
    }
    public class ChangeStatusCategoryRequestValidator : AbstractValidator<ChangeStatusCategoryRequest>
    {
        public ChangeStatusCategoryRequestValidator()
        {
            RuleFor(v => v.Id).GreaterThan(0).WithMessage("Id phải lớn hơn 0");
            RuleFor(v => v.Id).NotNull().WithMessage("Id Không được Null");
            RuleFor(v => v.Status).LessThan(2).WithMessage("Status phải bé hơn 2");
            RuleFor(v => v.Id).NotNull().WithMessage("Status Không được Null");
        }
    }
}
