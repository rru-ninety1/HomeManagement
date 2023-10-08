using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.ShoppingList;
using Microsoft.EntityFrameworkCore;
using OperationResults;

namespace HomeManagement.Business.ShoppingList;
public sealed record ShoppingListItemListQuery : IQuery<IEnumerable<ShoppingListItem>>;

public sealed class ShoppingListItemListQueryHandler : IQueryHandler<ShoppingListItemListQuery, IEnumerable<ShoppingListItem>>
{
    private readonly IReadOnlyDataContext _dataContext;

    public ShoppingListItemListQueryHandler(IReadOnlyDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Result<IEnumerable<ShoppingListItem>>> Handle(ShoppingListItemListQuery request, CancellationToken cancellationToken)
    {
        var shoppingListItems = await _dataContext.GetData<ShoppingListItem>()
           .ToListAsync()
           .ConfigureAwait(false);

        return shoppingListItems;
    }
}
