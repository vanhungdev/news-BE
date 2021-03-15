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
        IEnumerable<User> getAllPostTrash();
        IEnumerable<Role> GetAllRole();
        int Edit(User user);

        int delete(int Id);
        int changeStatusPost(int Id, int Status);
        int deTrash(int Id);
        int reTrash(int Id);

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
            //sp
            string sql = "getUserById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            var listProduct = _query.Query<User>(1, sql, parameter).FirstOrDefault();
            return listProduct;
        }
        public int changeStatusPost(int Id, int Status)
        {
            //SQL stored procedure 
            string sql = "changeStatusUser";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", Status, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public int deTrash(int Id)
        {
            //SQL stored procedure 
            string sql = "deTrashUserById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public int delete(int Id)
        {
            //SQL stored procedure 
            string sql = "deleteUserById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public int reTrash(int Id)
        {
            //SQL stored procedure 
            string sql = "changeStatusUser";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", 2, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }
        public IEnumerable<User> getAllPostTrash()
        {
            string sql = "getAllUserTrash";
            IEnumerable<User> listPost = _query.Query<User>(1, sql, null);
            return listPost;
        }

        public IEnumerable<Role> GetAllRole()
        {
            string sql = "getAllRole";
            var listPost = _query.Query<Role>(1, sql, null);
            return listPost;
        }

        public int Edit(User user)
        {
            string sql = "updateUser";
            DynamicParameters parameter = UserService.addAllParameterPost(user);
            int status = _query.Execute(sql, parameter);
            return status;
        }

        //
        public static DynamicParameters addAllParameterPost(User user)
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
