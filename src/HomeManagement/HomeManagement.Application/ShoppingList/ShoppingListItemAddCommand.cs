using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using HomeManagement.Core.ShoppingList;
using OperationResults;
using Riok.Mapperly.Abstractions;

namespace HomeManagement.Business.ShoppingList;
public sealed record ShoppingListItemAddCommand(string ProductId, double Quantity, string Annotation) : ICommand;

public sealed class ShoppingListItemAddCommandHandler : ICommandHandler<ShoppingListItemAddCommand>
{
    private readonly IDataContext _dataContext;

    public ShoppingListItemAddCommandHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result> Handle(ShoppingListItemAddCommand command, CancellationToken cancellationToken)
    {
        if (!_dataContext.GetData<Product>().Any(x => x.Id.Equals(command.ProductId)))
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Prodotto non presente");
        }

        var item = new ShoppingListItemAddCommandMapper().ToShoppingListItem(command);

        _dataContext.Insert(item);

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}

public sealed class ShoppingListItemAddCommandValidator : AbstractValidator<ShoppingListItemAddCommand>
{
    public ShoppingListItemAddCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Prodotto obbligatorio");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La quantità deve essere positiva");
    }
}

[Mapper]
public partial class ShoppingListItemAddCommandMapper
{
    public partial ShoppingListItem ToShoppingListItem(ShoppingListItemAddCommand command);
}
