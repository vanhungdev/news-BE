using MediatR;
using news.Application.Entities;
using news.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.User.Queries
{
    public class GetAllRoleRequest : IRequest<IEnumerable<Entities.Role>>
    {

    }
    public class GetAllRoleHandler : IRequestHandler<GetAllRoleRequest, IEnumerable<Entities.Role>>
    {
        private readonly IQuery _query;
        public GetAllRoleHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<IEnumerable<Role>> Handle(GetAllRoleRequest request, CancellationToken cancellationToken)
        {
            string sql = "getAllRole";
            var listPost = _query.Query<Entities.Role>(1, sql, null).ToList();
            return await Task.FromResult(listPost);
        }
    }

}
