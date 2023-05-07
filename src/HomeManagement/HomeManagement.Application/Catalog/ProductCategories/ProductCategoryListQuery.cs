using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using Microsoft.EntityFrameworkCore;
using OperationResults;

namespace HomeManagement.Business.Catalog.ProductCategories;

public record ProductCategoryListQuery : IQuery<IEnumerable<ProductCategory>>;

public class ProductCategoryListQueryHandler : IQueryHandler<ProductCategoryListQuery, IEnumerable<ProductCategory>>
{
    private readonly IReadOnlyDataContext _dataContext;

    public ProductCategoryListQueryHandler(IReadOnlyDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result<IEnumerable<ProductCategory>>> Handle(ProductCategoryListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _dataContext.GetData<ProductCategory>()
            .ToListAsync()
            .ConfigureAwait(false);

        return categories;
    }
}
