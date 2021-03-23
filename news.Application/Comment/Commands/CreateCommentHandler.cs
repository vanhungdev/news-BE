using Dapper;
using MediatR;
using news.Database;
using news.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Comment.Commands
{
    public class CreateCommentRequest : IRequest<int>
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int ParentId { get; set; }
        public string CommentDetail { get; set; }
        public string Name { get; set; }
        public int Star { get; set; }
        public DateTime Create_at { get; set; }
        public int Status { get; set; }
    }
    public class CreateCommentHandler : IRequestHandler<CreateCommentRequest, int>
    {
        private readonly IQuery _query;
        public CreateCommentHandler(IQuery query)
        {
            _query = query;
        }

        public async Task<int> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            string sql = "InsertComment";
            DynamicParameters parameter = CreateCommentHandler.addAllParameterComment(request);
            int status = _query.Execute(sql, parameter);
            return await Task.FromResult(status);
        }
        private static DynamicParameters addAllParameterComment(CreateCommentRequest comment)
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
