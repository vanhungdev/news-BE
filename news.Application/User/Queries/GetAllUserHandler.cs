using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.User.QueryHandler
{
    public class GetAllUserRequest : IRequest<IEnumerable<Entities.User>>
    {

    }
    public class GetAllUserHandler : IRequestHandler<GetAllUserRequest, IEnumerable<Entities.User> >
    {
        private readonly IQuery _query;
        public GetAllUserHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<IEnumerable<Entities.User>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            string sql = "getAllUser";
            IEnumerable<Entities.User> listPost = _query.Query<Entities.User>(1, sql, null);
            return await Task.FromResult(listPost);
        }       
    }
}
