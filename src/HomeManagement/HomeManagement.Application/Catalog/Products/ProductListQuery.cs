using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using Microsoft.EntityFrameworkCore;
using OperationResults;

namespace HomeManagement.Business.Catalog.Products;

public record ProductListQuery : IQuery<IEnumerable<Product>>;

public class ProductListQueryHandler : IQueryHandler<ProductListQuery, IEnumerable<Product>>
{
    private readonly IReadOnlyDataContext _dataContext;

    public ProductListQueryHandler(IReadOnlyDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result<IEnumerable<Product>>> Handle(ProductListQuery query, CancellationToken cancellationToken)
    {
        var products = await _dataContext.GetData<Product>()
            .ToListAsync()
            .ConfigureAwait(false);

        return products;
    }
}
