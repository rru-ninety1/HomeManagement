using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using OperationResults;

namespace HomeManagement.Business.Catalog.Products;

public record ProductAddCommand(string Description, string CategoryId) : ICommand;

public class ProductAddCommandHandler : ICommandHandler<ProductAddCommand>
{
    private readonly IDataContext _dataContext;

    public ProductAddCommandHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result> Handle(ProductAddCommand request, CancellationToken cancellationToken)
    {
        _dataContext.Insert(new Product { Description = request.Description, CategoryId = request.CategoryId });

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}
