using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.ShoppingList;
using Microsoft.EntityFrameworkCore;
using OperationResults;

namespace HomeManagement.Business.ShoppingList;
public sealed record ShoppingListItemGetSingleQuery(string Id) : IQuery<ShoppingListItem>;

public sealed class ShoppingListItemGetSingleQueryHandler : IQueryHandler<ShoppingListItemGetSingleQuery, ShoppingListItem>
{
    private readonly IReadOnlyDataContext _dataContext;

    public ShoppingListItemGetSingleQueryHandler(IReadOnlyDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result<ShoppingListItem>> Handle(ShoppingListItemGetSingleQuery query, CancellationToken cancellationToken)
    {
        var shoppingListItem = await _dataContext.GetData<ShoppingListItem>()
            .FirstOrDefaultAsync(x => x.Id == query.Id)
            .ConfigureAwait(false);

        if (shoppingListItem == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, "Item non presente");
        }

        return shoppingListItem;
    }
}

public sealed class ShoppingListItemGetSingleQueryValidator : AbstractValidator<ShoppingListItemGetSingleQuery>
{
    public ShoppingListItemGetSingleQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id obbligatorio");
    }
}
