using Dapper;
using news.Application.Entities;
using news.Database;
using news.Infrastructure.Configuration;
using news.Infrastructure.Utilities;
using news_API.Infrastructure.Auth;
using news_API.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.Services
{
    public interface IUserService
    {
        authReponse Authenticate(AuthRequest model);


    }

    public class UserService : IUserService
    {
        private readonly IQuery _query;
        public UserService( IQuery query)
        {       
            _query = query;
        }
        public authReponse Authenticate(AuthRequest model)
        {
            string sql = "getUserByUnameAndPassWord";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Username", model.Username, DbType.String, ParameterDirection.Input);
            parameter.Add("@Password", Helper.ToMD5(model.Password), DbType.String, ParameterDirection.Input);
            User user = _query.Query<User>(1, sql, parameter).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            var tokenResult = JwtAuthManager.GenerateTokens(model, user.access);
            return new authReponse(tokenResult.AccessToken.ToString(), user);
        }

    }
}
