using Dapper;
using MediatR;
using news.Database;
using news.Infrastructure.Enums;
using news.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.User.CommandHandler
{
    public class EditUserRequest : IRequest<int>
    {
        public int ID { get; set; }

        public string fullname { get; set; }

        public string username { get; set; }

        public string password { get; set; }
        public string email { get; set; }
        public string gender { get; set; }

        public string address { get; set; }
        public string phone { get; set; }
        public string img { get; set; }

        public UserRole access { get; set; }
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        public DateTime updated_at { get; set; }

        public int updated_by { get; set; }

        public int status { get; set; }
    }
    class EditUserHandler : IRequestHandler<EditUserRequest, int>
    {
        private readonly IQuery _query;
        public EditUserHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<int> Handle(EditUserRequest request, CancellationToken cancellationToken)
        {
            string sql = "updateUser";
            DynamicParameters parameter = EditUserHandler.addAllParameterPost(request);
            int result = _query.Execute(sql, parameter);
            return await Task.FromResult(result);
        }
        public static DynamicParameters addAllParameterPost(EditUserRequest user)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", user.ID, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@fullname", user.fullname, DbType.String, ParameterDirection.Input);
            parameter.Add("@username", user.username, DbType.String, ParameterDirection.Input);
            parameter.Add("@password", user.password, DbType.String, ParameterDirection.Input);
            parameter.Add("@email", user.email, DbType.String, ParameterDirection.Input);
            parameter.Add("@gender", user.gender, DbType.String, ParameterDirection.Input);
            parameter.Add("@phone", user.phone, DbType.String, ParameterDirection.Input);
            parameter.Add("@img", user.img, DbType.String, ParameterDirection.Input);
            parameter.Add("@access", user.access, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Created_at", user.created_at, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Created_by", user.created_by, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Updated_at", user.updated_at, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Updated_by", user.updated_by, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", user.status, DbType.Int32, ParameterDirection.Input);
            return parameter;
        }

    }

}
