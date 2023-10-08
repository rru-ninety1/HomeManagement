using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using OperationResults;

namespace HomeManagement.Business.Catalog.ProductCategories;

public sealed record ProductCategoryAddCommand(string Description) : ICommand;

public sealed class ProductCategoryAddCommandHandler : ICommandHandler<ProductCategoryAddCommand>
{
    private readonly IDataContext _dataContext;

    public ProductCategoryAddCommandHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result> Handle(ProductCategoryAddCommand command, CancellationToken cancellationToken)
    {
        _dataContext.Insert(new ProductCategory { Description = command.Description });

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}

public sealed class ProductCategoryAddCommandValidator : AbstractValidator<ProductCategoryAddCommand>
{
    public ProductCategoryAddCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("Descrizione obbligatoria");
    }
}
