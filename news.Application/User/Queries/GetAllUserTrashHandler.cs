using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.User.QueryHandler
{
    public class GetAllUserTrashRequest : IRequest<IEnumerable<Entities.User>>
    {

    }
    class GetAllUserTrashHandler : IRequestHandler<GetAllUserTrashRequest, IEnumerable<Entities.User>>
    {
        private readonly IQuery _query;
        public GetAllUserTrashHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<IEnumerable<Entities.User>> Handle(GetAllUserTrashRequest request, CancellationToken cancellationToken)
        {
            string sql = "getAllUserTrash";
            IEnumerable<Entities.User> listPost = _query.Query<Entities.User>(1, sql, null);
            return await Task.FromResult(listPost);
        }
    }
}
