using MediatR;
using news.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Category.Queries
{
    public class GetAllCategory : IRequest<IEnumerable<Entities.Category>>
    {

    }
    class GetAllCategoryHandler : IRequestHandler<GetAllCategory,IEnumerable<Entities.Category> >
    {
        private readonly IQuery _query;
        public GetAllCategoryHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<IEnumerable<Entities.Category>> Handle(GetAllCategory request, CancellationToken cancellationToken)
        {
            string sql = "getAlltopic";
            var listCate = _query.Query<Entities.Category>(1, sql, null).ToList();
            return await Task.FromResult(listCate);
        }       
    }
}
