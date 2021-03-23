using Dapper;
using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.User.CommandHandler
{
    public class DeTrashUserRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
    class DetrashUserHandler : IRequestHandler<DeTrashUserRequest, int>
    {
        private readonly IQuery _query;
        public DetrashUserHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<int> Handle(DeTrashUserRequest request, CancellationToken cancellationToken)
        {
            string sql = "deTrashUserById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
    }
}
