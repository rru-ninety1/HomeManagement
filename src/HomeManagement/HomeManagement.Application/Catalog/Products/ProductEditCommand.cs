using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using OperationResults;
using Riok.Mapperly.Abstractions;

namespace HomeManagement.Business.Catalog.Products;

public record ProductEditCommand(string Id, string Description, string CategoryId) : ICommand;

public class ProductEditCommandHandler : ICommandHandler<ProductEditCommand>
{
    private readonly IDataContext _dataContext;

    public ProductEditCommandHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result> Handle(ProductEditCommand command, CancellationToken cancellationToken)
    {
        if (!_dataContext.GetData<ProductCategory>().Any(x => x.Id.Equals(command.CategoryId)))
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Categoria non presente");
        }

        var product = await _dataContext.GetAsync<Product>(command.Id)
            .ConfigureAwait(false);

        if (product == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Prodotto non presente");
        }

        new ProductEditCommandMapper().ToProduct(command, product);

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}

public sealed class ProductEditCommandValidator : AbstractValidator<ProductEditCommand>
{
    public ProductEditCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("Descrizione obbligatoria");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Categoria obbligatoria");
    }
}

[Mapper]
public partial class ProductEditCommandMapper
{
    [MapperIgnoreSource(nameof(ProductEditCommand.Id))]
    public partial void ToProduct(ProductEditCommand command, Product product);
}
