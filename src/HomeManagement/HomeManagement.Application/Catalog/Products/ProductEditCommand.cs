using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using OperationResults;

namespace HomeManagement.Business.Catalog.Products;

public record ProductEditCommand(string Id, string Description, string CategoryId) : ICommand;

public class ProductEditCommandHandler : ICommandHandler<ProductEditCommand>
{
    private readonly IDataContext _dataContext;

    public ProductEditCommandHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result> Handle(ProductEditCommand request, CancellationToken cancellationToken)
    {
        var dbProduct = await _dataContext.GetAsync<Product>(request.Id)
            .ConfigureAwait(false);

        if (dbProduct == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Categoria non presente");
        }

        dbProduct.CategoryId = request.CategoryId;
        dbProduct.Description = request.Description;

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}
