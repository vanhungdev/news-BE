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

namespace news.Application.Post.Queries
{
    public class GetPostBySlug : IRequest<Entities.Post>
    {
        public string slug { get; set; }
    }
    public class GetPostBySlugHandler : IRequestHandler<GetPostBySlug, Entities.Post>
    {
        private readonly IQuery _query;
        public GetPostBySlugHandler(IQuery query)
        {
            _query = query;
        }
        public async Task<Entities.Post> Handle(GetPostBySlug request, CancellationToken cancellationToken)
        {
            string sql = "findPostBySlug";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@slug", request.slug, DbType.String, ParameterDirection.Input);
            var listProduct = _query.Query<Entities.Post>(1, sql, parameter).FirstOrDefault();
            return await Task.FromResult(listProduct);
        }
    }
    public class GetCategoryBySlugValidator : AbstractValidator<GetPostBySlug>
    {
        public GetCategoryBySlugValidator()
        {
            RuleFor(v => v.slug).NotEmpty();
        }
    }
}
