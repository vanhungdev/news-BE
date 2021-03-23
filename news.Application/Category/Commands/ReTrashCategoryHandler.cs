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
    public class ReTrashCategoryRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
    class ReTrashCategoryHandler : IRequestHandler<ReTrashCategoryRequest, int>
    {
        private readonly IQuery _query;
        public ReTrashCategoryHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<int> Handle(ReTrashCategoryRequest request, CancellationToken cancellationToken)
        {
            string sql = "changeStatusTopic";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", 2, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
    }
}
