using Dapper;
using FluentValidation;
using MediatR;
using news.Database;
using news.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace news.Application.Category.Commands
{
    public class CreateCategoryRequest : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Parentid { get; set; }
        public int Orders { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public DateTime Created_at { get; set; }
        public int Created_by { get; set; }
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
        public int Status { get; set; }
    }
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, int>
    {
        private readonly IQuery _query;
        public CreateCategoryHandler(IQuery query)
        {
            _query = query;
        }

        public async Task<int> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            request.Slug = Helper.ToSlug(request.Name);
            request.Id = 0;
            string sql = "createCategory";
            DynamicParameters parameter = CreateCategoryHandler.addAllParameterCategory(request);
            int result = _query.Execute(sql, parameter);

            return await Task.FromResult(result);
        }
        public static DynamicParameters addAllParameterCategory(CreateCategoryRequest category)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Id", category.Id, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Name", category.Name, DbType.String, ParameterDirection.Input);
            parameter.Add("@Slug", category.Slug, DbType.String, ParameterDirection.Input);
            parameter.Add("@Parentid", category.Parentid, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Orders", category.Orders, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Metakey", category.Metakey, DbType.String, ParameterDirection.Input);
            parameter.Add("@Metadesc", category.Metadesc, DbType.String, ParameterDirection.Input);
            parameter.Add("@Created_at", category.Created_at, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Created_by", category.Created_by, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Updated_at", category.Updated_at, DbType.DateTime, ParameterDirection.Input);
            parameter.Add("@Updated_by", category.Updated_by, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Status", 1, DbType.Int32, ParameterDirection.Input);
            return parameter;
        }
    }
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {   
            RuleFor(v => v.Name).NotNull();
            RuleFor(v => v.Slug).NotNull();
            RuleFor(v => v.Parentid).NotNull();
        }
    }
}
