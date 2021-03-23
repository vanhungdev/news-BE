using Dapper;
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
    public class ReTrashPostRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
    class ReTrashPostHandler : IRequestHandler<ReTrashPostRequest, int>
    {
        private readonly IQuery _query;
        public ReTrashPostHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<int> Handle(ReTrashPostRequest request, CancellationToken cancellationToken)
        {
            string sql = "changeStatusPost";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", 2, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
    }
}
