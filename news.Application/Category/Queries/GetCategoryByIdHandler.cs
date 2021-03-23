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

namespace news.Application.Category.Queries
{
    public class GetCategoryById : IRequest<Entities.Category>
    {
        public int Id { get; set; }
    }
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Entities.Category>
    {
        private readonly IQuery _query;
        public GetCategoryByIdHandler(IQuery query)
        {
            _query = query;
        }
        public Task<Entities.Category> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            string sql = "getCategoryById";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", request.Id, DbType.String, ParameterDirection.Input);
            Entities.Category listPost = _query.Query<Entities.Category>(1, sql, parameter).FirstOrDefault();
            return Task.FromResult(listPost);
        }
    }
}
