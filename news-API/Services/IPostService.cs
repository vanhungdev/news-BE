using Dapper;
using Microsoft.AspNetCore.Mvc;
using news.Database;
using news_API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.Services
{
    public interface IPostService
    {
       
        IEnumerable<Post> getAllPost();
        IEnumerable<Post> findPostById(int Id);
        public IEnumerable<Post> findPostBySlug(string slug);
        int createPost(Post post);
        int editPost(Post post);
        int deleteAnyPost(string[] arrayId);
        IEnumerable<Post> getPostByCategoryId(int id);
        IEnumerable<Post> getAllPostTrash();
        int delete(int Id);
        int changeStatusPost(int Id, int Status);
        int deTrash(int Id);
        int reTrash(int Id);

    }
    public class PostService : IPostService
    {
        private readonly IQuery _query;
        public PostService( IQuery query)
        {    
            _query = query;
        }
        
        public int createPost(Post post)
        {
            post.Status = 1;
            string sql = "InsertPost";
            DynamicParameters parameter = PostService.addAllParameterPost(post);
            int status = _query.Execute(sql, parameter);
            return status;
        }

        public int deleteAnyPost(string[] arrayId)
        {
            return -1;
        }

    
        public int editPost(Post post)
        {

            string sql = "updatePost";
            DynamicParameters parameter = PostService.addAllParameterPost(post);
            int status = _query.Execute(sql, parameter);
            return status;

        }

        public IEnumerable<Post> findPostById(int Id)
        {
            //sp
            string sql = "findPostById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            var listProduct = _query.Query<Post>(1, sql, parameter);
            return listProduct;
        }


        public IEnumerable<Post> findPostBySlug(string slug)
        {
            //sp
            string sql = "findPostBySlug";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@slug", slug, DbType.String, ParameterDirection.Input);
            var listProduct = _query.Query<Post>(1, sql, parameter);
            return listProduct;
        }

        public IEnumerable<Post> getAllPost()
        {
            //sp
            string sql = "getAllPost";
            IEnumerable<Post> listPost = _query.Query<Post>(1, sql, null);
            return listPost;
        }

        public IEnumerable<Post> getPostByCategoryId(int id)
        {
            string sql = "getPostByCategory";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@CategoryId", id, DbType.Int32, ParameterDirection.Input);
            IEnumerable<Post> listPost = _query.Query<Post>(1, sql, parameter);
            return listPost;
        }

        public int changeStatusPost(int Id, int Status)
        {
            //SQL stored procedure 
            string sql = "changeStatusPost";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", Status, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public int deTrash(int Id)
        {
            //SQL stored procedure 
            string sql = "deTrashPostById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public int delete(int Id)
        {
            //SQL stored procedure 
            string sql = "deletePostById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public int reTrash(int Id)
        {
            //SQL stored procedure 
            string sql = "changeStatusPost";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", 2, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public static DynamicParameters addAllParameterPost(Post post)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", post.ID, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Topid", post.Topid, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Title", post.Title, DbType.String, ParameterDirection.Input);
            parameter.Add("@Slug", post.Slug, DbType.String, ParameterDirection.Input);
            parameter.Add("@Detail", post.Detail, DbType.String, ParameterDirection.Input);
            parameter.Add("@Img", post.Img, DbType.String, ParameterDirection.Input);
            parameter.Add("@Type", post.Type, DbType.String, ParameterDirection.Input);
            parameter.Add("@Metakey", post.Metakey, DbType.String, ParameterDirection.Input);
            parameter.Add("@Metadesc", post.Metadesc, DbType.String, ParameterDirection.Input);
            parameter.Add("@Created_at", post.Created_at, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Created_by", post.Created_by, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Updated_at", post.Updated_at, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Updated_by", post.Updated_by, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", post.Status, DbType.Int32, ParameterDirection.Input);
            return parameter;
        }

        public IEnumerable<Post> getAllPostTrash()
        {
            string sql = "getAllPostTrash";
            IEnumerable<Post> listPost = _query.Query<Post>(1, sql, null);
            return listPost;
        }
    }
}
