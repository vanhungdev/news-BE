using Dapper;
using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Post.Queries
{
    public class GetAllPostByCategoryId : IRequest<IEnumerable<Entities.Post>>
    {
        public int Catid { get; set; }
    }
    public class GetAllPostByCategoryIdHandler :IRequestHandler<GetAllPostByCategoryId, IEnumerable<Entities.Post> >
    {
        private readonly IQuery _query;
        public GetAllPostByCategoryIdHandler(IQuery query)
        {
            _query = query;
        }

        public async Task<IEnumerable<Entities.Post>> Handle(GetAllPostByCategoryId request, CancellationToken cancellationToken)
        {
            string sql = "getPostByCategory";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@CategoryId", request.Catid, DbType.Int32, ParameterDirection.Input);
            IEnumerable<Entities.Post> listPost = _query.Query<Entities.Post>(1, sql, parameter);
            return await Task.FromResult(listPost);
        }
    }
}
