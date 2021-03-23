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
    public class DeleteUserRequest : IRequest<int>
    {
        public int Id { get; set; }
    }
    class DeleteUserHandler : IRequestHandler<DeleteUserRequest, int>
    {
        private readonly IQuery _query;
        public DeleteUserHandler(IQuery query)
        {
            _query = query;
        }

        public async Task<int> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            //SQL stored procedure 
            string sql = "deleteUserById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
    }
}
