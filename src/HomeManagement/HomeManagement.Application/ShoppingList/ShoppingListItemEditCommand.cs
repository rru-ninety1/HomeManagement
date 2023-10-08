using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using HomeManagement.Core.ShoppingList;
using OperationResults;
using Riok.Mapperly.Abstractions;

namespace HomeManagement.Business.ShoppingList;
public sealed record ShoppingListItemEditCommand(string Id, string ProductId, double Quantity, string Annotation) : ICommand;

public sealed class ShoppingListItemEditCommandHandler : ICommandHandler<ShoppingListItemEditCommand>
{
    private readonly IDataContext _dataContext;

    public ShoppingListItemEditCommandHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result> Handle(ShoppingListItemEditCommand command, CancellationToken cancellationToken)
    {
        if (!_dataContext.GetData<Product>().Any(x => x.Id.Equals(command.ProductId)))
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Prodotto non presente");
        }

        var shoppingListItem = await _dataContext.GetAsync<ShoppingListItem>(command.Id)
            .ConfigureAwait(false);

        if (shoppingListItem == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Item lista non presente");
        }

        new ShoppingListItemEditCommandMapper().ToShoppingListItem(command, shoppingListItem);

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}

public sealed class ShoppingListItemEditCommandValidator : AbstractValidator<ShoppingListItemEditCommand>
{
    public ShoppingListItemEditCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id obbligatorio");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Prodotto obbligatorio");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La quantità deve essere positiva");
    }
}

[Mapper]
public partial class ShoppingListItemEditCommandMapper
{
    [MapperIgnoreSource(nameof(ShoppingListItemEditCommand.Id))]
    public partial void ToShoppingListItem(ShoppingListItemEditCommand command, ShoppingListItem shoppingListItem);
}
