using Dapper;
using FluentValidation;
using MediatR;
using news.Application.Common;
using news.Database;
using news.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Category.Queries
{
    public class GetCategoryBySlug : IRequest<Entities.Category>
    {
        public string slug { get; set; }
    }
    public class GetCategoryBySlugHandler : IRequestHandler<GetCategoryBySlug, Entities.Category>
    {
        private readonly IQuery _query;
        public GetCategoryBySlugHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<Entities.Category> Handle(GetCategoryBySlug request, CancellationToken cancellationToken)
        {
            string sql = "getCategoryBySlug";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@slug", request.slug, DbType.String, ParameterDirection.Input);
            Entities.Category listProduct = _query.Query<Entities.Category>(1, sql, parameter).FirstOrDefault();
            return await Task.FromResult(listProduct);
        }
    }
    public class GetCategoryBySlugValidator : AbstractValidator<GetCategoryBySlug>
    {
        public GetCategoryBySlugValidator()
        {
            RuleFor(v => v.slug).NotEmpty();
        }
    }
}
