using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using Microsoft.EntityFrameworkCore;
using OperationResults;

namespace HomeManagement.Business.Catalog.ProductCategories;

public record ProductCategoryGetSingleQuery(string Id) : IQuery<ProductCategory>;

public class ProductCategoryGetSingleQueryHandler : IQueryHandler<ProductCategoryGetSingleQuery, ProductCategory>
{
    private readonly IReadOnlyDataContext _dataContext;

    public ProductCategoryGetSingleQueryHandler(IReadOnlyDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result<ProductCategory>> Handle(ProductCategoryGetSingleQuery request, CancellationToken cancellationToken)
    {
        var productCategory = await _dataContext.GetData<ProductCategory>()
            .FirstOrDefaultAsync(x => x.Id == request.Id)
            .ConfigureAwait(false);

        if (productCategory == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Categoria non presente");
        }

        return productCategory;
    }
}
