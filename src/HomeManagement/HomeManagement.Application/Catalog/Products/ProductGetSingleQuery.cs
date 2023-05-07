using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using Microsoft.EntityFrameworkCore;
using OperationResults;

namespace HomeManagement.Business.Catalog.Products;

public record ProductGetSingleQuery(string Id) : IQuery<Product>;

public class ProductGetSingleQueryHandler : IQueryHandler<ProductGetSingleQuery, Product>
{
    private readonly IReadOnlyDataContext _dataContext;

    public ProductGetSingleQueryHandler(IReadOnlyDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result<Product>> Handle(ProductGetSingleQuery request, CancellationToken cancellationToken)
    {
        var product = await _dataContext.GetData<Product>()
            .FirstOrDefaultAsync(x => x.Id == request.Id)
            .ConfigureAwait(false);

        if (product == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Prodotto non presente");
        }

        return product;
    }
}
