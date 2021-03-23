using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Post.Queries
{
    public class GetAllPostTrashRequest : IRequest<IEnumerable<Entities.Post>>
    {

    }
    class GetAllPostTrashHandler : IRequestHandler<GetAllPostTrashRequest, IEnumerable<Entities.Post>>
    {
        private readonly IQuery _query;
        public GetAllPostTrashHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<IEnumerable<Entities.Post>> Handle(GetAllPostTrashRequest request, CancellationToken cancellationToken)
        {
            string sql = "getAllPostTrash";
            IEnumerable<Entities.Post> listPost = _query.Query<Entities.Post>(1, sql, null);
            return await Task.FromResult(listPost);
        }
    }
}
