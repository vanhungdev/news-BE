using Dapper;
using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Comment.Queries
{
    public class GetAllCommentByPostId : IRequest<IEnumerable<Entities.Comment>>
    {
        public int postId { get; set; }
    }
    class GetAllCommentHandler : IRequestHandler<GetAllCommentByPostId, IEnumerable<Entities.Comment> >
    {
        private readonly IQuery _query;
        public GetAllCommentHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<IEnumerable<Entities.Comment>> Handle(GetAllCommentByPostId request, CancellationToken cancellationToken)
        {
            string sql = "GetCommentByPost";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@PostId", request.postId, DbType.Int32, ParameterDirection.Input);
            var listComment = _query.Query<Entities.Comment>(1, sql, parameter);
            return await Task.FromResult(listComment);
        }       
    }
}
