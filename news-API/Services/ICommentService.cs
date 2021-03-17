using Dapper;
using news.Database;
using news_API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.Services
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAllCommentByPost(int Id);
        int delete(int id);
        int create(Comment comment);
        int changeStatusComment(int Id,int Status);
    }
    public class CommentService : ICommentService
    {
        private readonly IQuery _query;
        public CommentService(IQuery query)
        {
            _query = query;
        }

        public int changeStatusComment(int Id,int Status)
        {
            string sql = "changeStatusComment";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", Status, DbType.Int32, ParameterDirection.Input);
            int listComment = _query.Execute( sql, parameter);
            return listComment;
        }

        public int delete(int id)
        {
            return -1;
        }

        public IEnumerable<Comment> GetAllCommentByPost(int Id)
        {
            string sql = "GetCommentByPost";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@PostId", Id, DbType.Int32, ParameterDirection.Input);
            var listComment = _query.Query<Comment>(1, sql, parameter);
            return listComment;
        }

        public int create(Comment comment)
        {
            
              string sql = "InsertComment";
            DynamicParameters parameter = CommentService.addAllParameterComment(comment);
            int status = _query.Execute(sql, parameter);
            return status;

        }

        private static DynamicParameters addAllParameterComment(Comment comment)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", comment.Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@PostId", comment.PostId, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@parentId", comment.ParentId, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@commentDetail", comment.CommentDetail, DbType.String, ParameterDirection.Input);
            parameter.Add("@name", comment.Name, DbType.String, ParameterDirection.Input);
            parameter.Add("@Star", comment.Star, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Create_at", comment.Create_at, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Status", comment.Status, DbType.Int32, ParameterDirection.Input);
            return parameter;
        }
    }
}
