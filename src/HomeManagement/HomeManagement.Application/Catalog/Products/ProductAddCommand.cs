using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using OperationResults;
using Riok.Mapperly.Abstractions;

namespace HomeManagement.Business.Catalog.Products;

public record ProductAddCommand(string Description, string CategoryId) : ICommand;

public class ProductAddCommandHandler : ICommandHandler<ProductAddCommand>
{
    private readonly IDataContext _dataContext;

    public ProductAddCommandHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result> Handle(ProductAddCommand command, CancellationToken cancellationToken)
    {
        if (!_dataContext.GetData<ProductCategory>().Any(x => x.Id.Equals(command.CategoryId)))
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Categoria non presente");
        }

        var product = new ProductAddCommandMapper().ToProduct(command);
        _dataContext.Insert(product);

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}

public sealed class ProductAddCommandValidator : AbstractValidator<ProductAddCommand>
{
    public ProductAddCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("Descrizione obbligatoria");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Categoria obbligatoria");
    }
}

[Mapper]
public partial class ProductAddCommandMapper
{
    public partial Product ToProduct(ProductAddCommand comand);
}
