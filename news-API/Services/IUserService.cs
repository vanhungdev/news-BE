using news_API.Entities;
using news_API.models;
using System;
using news.Infrastructure.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using news_API.Infrastructure.Auth;
using Microsoft.AspNetCore.Http;
using news.Infrastructure.Enums;
using news.Database;
using Dapper;
using System.Data;
using news.Infrastructure.Utilities;

namespace news.Api.Services
{
    public interface IUserService
    {
        authReponse Authenticate(AuthRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        private readonly IQuery _query;
        public UserService(IOptions<AppSettings> appSettings, IQuery query)
        {
            _appSettings = appSettings.Value;
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
            return new authReponse(tokenResult.AccessToken.ToString(),user);
        }

        public IEnumerable<User> GetAll()
        {
            string sql = "getAllUser";
            IEnumerable<User> listPost = _query.Query<User>(1, sql, null);
            return listPost;
        }

        public User GetById(int id)
        {

            throw new NotImplementedException();
        }

  
    }

}
