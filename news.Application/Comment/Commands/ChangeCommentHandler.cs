using Dapper;
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

namespace news.Application.Comment.Commands
{
    public class ChangeStatusCommentRequest : IRequest<int>
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }
    public class ChangeCommentHandler : IRequestHandler<ChangeStatusCommentRequest, int >
    {
        private readonly IQuery _query;
        public ChangeCommentHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<int> Handle(ChangeStatusCommentRequest request, CancellationToken cancellationToken)
        {
            string sql = "changeStatusComment";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", request.Status, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
    }
}
