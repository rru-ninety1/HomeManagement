using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using OperationResults;

namespace HomeManagement.Business.Catalog.ProductCategories;

public record ProductCategoryEditCommand(string Id, string Description) : ICommand;

public class ProductCategoryEditCommandHandler : ICommandHandler<ProductCategoryEditCommand>
{
    private readonly IDataContext _dataContext;

    public ProductCategoryEditCommandHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result> Handle(ProductCategoryEditCommand request, CancellationToken cancellationToken)
    {
        var dbProductCategory = await _dataContext.GetAsync<ProductCategory>(request.Id)
            .ConfigureAwait(false);

        if (dbProductCategory == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Categoria non presente");
        }

        dbProductCategory.Description = request.Description;

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}
