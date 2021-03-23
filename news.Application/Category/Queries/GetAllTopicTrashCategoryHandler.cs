using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Category.Queries
{
    public class GetAllTopicTrashCategoryRequest : IRequest<IEnumerable<Entities.Category>>
    {

    }
    class GetAllTopicTrashCategoryHandler : IRequestHandler<GetAllTopicTrashCategoryRequest, IEnumerable<Entities.Category>>
    {
        private readonly IQuery _query;
        public GetAllTopicTrashCategoryHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<IEnumerable<Entities.Category>> Handle(GetAllTopicTrashCategoryRequest request, CancellationToken cancellationToken)
        {
            string sql = "getAllTopicTrash";
            IEnumerable<Entities.Category> listPost = _query.Query<Entities.Category>(1, sql, null);
            return await Task.FromResult( listPost);
        }
    }
}
