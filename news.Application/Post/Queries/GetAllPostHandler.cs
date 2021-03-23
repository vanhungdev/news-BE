using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Post.Queries
{
    public class GetAllPostRequest : IRequest<IEnumerable<Entities.Post>>
    {

    }
    public class GetAllPostHandler : IRequestHandler<GetAllPostRequest, IEnumerable<Entities.Post> >
    {
        private readonly IQuery _query;
        public GetAllPostHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<IEnumerable<Entities.Post>> Handle(GetAllPostRequest request, CancellationToken cancellationToken)
        {
            string sql = "getAllPost";
            IEnumerable<Entities.Post> listPost = _query.Query<Entities.Post>(1, sql, null);
            return await Task.FromResult(listPost);
        }       
    }
}
