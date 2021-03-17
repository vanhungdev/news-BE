using Dapper;
using Microsoft.AspNetCore.Mvc;
using news.Database;
using news.Infrastructure.Utilities;
using news_API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> getAll();

        IEnumerable<Category> getAllTopicTrash();
        int create(Category category);
        Category findById(int Id);
        Category findBySlug(string slug);
        int edit(Category category);
        int delete(int Id);
        int changeStatusTopic(int Id, int Status);
        int deTrash(int Id);
        int reTrash(int Id);
    }
    public class CategoryService : ICategoryService
    {
        private readonly IQuery _query;
        public CategoryService(IQuery query)
        {
            _query = query;
        }

        public int create(Category category)
        {
            category.Slug = Helper.ToSlug(category.Name);
            category.Id = 0;
            string sql = "createCategory";
            DynamicParameters parameter = CategoryService.addAllParameterCategory(category);
            int status = _query.Execute(sql, parameter);
            return status;
        }

        public int delete(int id)
        {
            string sql = "deleteCategoryById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", id, DbType.String, ParameterDirection.Input);
            int status = _query.Execute(sql, parameter);
            return status;
        }

        public int edit(Category category)
        {
            string sql = "updateCategory";
            DynamicParameters parameter = CategoryService.addAllParameterCategory(category);
            int status = _query.Execute(sql, parameter);
            return status;
        }
        public IEnumerable<Category> getAll()
        {
            string sql = "getAlltopic";
            IEnumerable<Category> listPost = _query.Query<Category>(1, sql, null);
            return listPost;        
        }
        Category ICategoryService.findById(int Id)
        {
            string sql = "getCategoryById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.String, ParameterDirection.Input);
            IEnumerable<Category> listPost = _query.Query<Category>(1, sql, parameter);
            return listPost.FirstOrDefault();
        }

        public int changeStatusTopic(int Id, int Status)
        {
            //SQL stored procedure 
            string sql = "changeStatusTopic";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", Status, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public int deTrash(int Id)
        {
            //SQL stored procedure 
            string sql = "deTrashTopicById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public int reTrash(int Id)
        {
            //SQL stored procedure 
            string sql = "changeStatusTopic";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", 2, DbType.Int32, ParameterDirection.Input);
            int result = _query.Execute(sql, parameter);
            return result;
        }

        public static DynamicParameters addAllParameterCategory(Category category)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", category.Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Name", category.Name, DbType.String, ParameterDirection.Input);
            parameter.Add("@Slug", category.Slug, DbType.String, ParameterDirection.Input);
            parameter.Add("@Parentid", category.Parentid, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Orders", category.Orders, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Metakey", category.Metakey, DbType.String, ParameterDirection.Input);
            parameter.Add("@Metadesc", category.Metadesc, DbType.String, ParameterDirection.Input);
            parameter.Add("@Created_at", category.Created_at, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Created_by", category.Created_by, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Updated_at", category.Updated_at, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Updated_by", category.Updated_by, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", category.Status, DbType.Int32, ParameterDirection.Input);
            return parameter;
        }

        public IEnumerable<Category> getAllTopicTrash()
        {
            //sp
            string sql = "getAllTopicTrash";
            IEnumerable<Category> listPost = _query.Query<Category>(1, sql, null);
            return listPost;
        }

        public Category findBySlug(string slug)
        {
            string sql = "getCategoryBySlug";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Slug", slug, DbType.String, ParameterDirection.Input);
            IEnumerable<Category> listPost = _query.Query<Category>(1, sql, parameter);
            return listPost.FirstOrDefault();
        }
    }
}
