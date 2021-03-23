using Dapper;
using FluentValidation;
using MediatR;
using news.Database;
using news.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Post.CommandHandler
{
    public class CreatePostRequest : IRequest<int>
    {
        public int ID { get; set; }
        public int Topid { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Detail { get; set; }
        public string Img { get; set; }
        public string Type { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public DateTime Created_at { get; set; }
        public int Created_by { get; set; }
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
        public int Status { get; set; }
    }
    public class CreatePostHandler : IRequestHandler<CreatePostRequest, int>
    {
        private readonly IQuery _query;
        public CreatePostHandler(IQuery query)
        {
            _query = query;
        }

        public async Task<int> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            request.Status = 1;
            request.Slug = Helper.ToSlug(request.Title);
            string sql = "InsertPost";
            DynamicParameters parameter = CreatePostHandler.addAllParameterPost(request); 
            int result = _query.Execute(sql, parameter);

            return await Task.FromResult(result);
        }
        public static DynamicParameters addAllParameterPost(CreatePostRequest post)
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

    }
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(v => v.Topid).NotNull();
            RuleFor(v => v.Title).NotNull();
            RuleFor(v => v.Detail).NotNull();
        }
    }
}
