using Dapper;
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
}
