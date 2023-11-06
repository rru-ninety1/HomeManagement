using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using HomeManagement.Core.Localization;
using HomeManagement.Core.ShoppingList;
using OperationResults;
using Riok.Mapperly.Abstractions;

namespace HomeManagement.Business.ShoppingList;
public sealed record ShoppingListItemEditCommand(string Id, string ProductId, double Quantity, string Annotation) : ICommand;

public sealed class ShoppingListItemEditCommandHandler : ICommandHandler<ShoppingListItemEditCommand>
{
    private readonly IDataContext _dataContext;
    private readonly ILocalizationService _localizationService;

    public ShoppingListItemEditCommandHandler(IDataContext dataContext, ILocalizationService localizationService)
    {
        _dataContext = dataContext;
        _localizationService = localizationService;
    }

    public async Task<Result> Handle(ShoppingListItemEditCommand command, CancellationToken cancellationToken)
    {
        if (!_dataContext.GetData<Product>().Any(x => x.Id.Equals(command.ProductId)))
        {
            return Result.Fail(FailureReasons.ItemNotFound, _localizationService.GetLocalizedString("ProductNotFound"));
        }

        var shoppingListItem = await _dataContext.GetAsync<ShoppingListItem>(command.Id)
            .ConfigureAwait(false);

        if (shoppingListItem == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, _localizationService.GetLocalizedString("ListItemNotFound"));
        }

        new ShoppingListItemEditCommandMapper().ToShoppingListItem(command, shoppingListItem);

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}

public sealed class ShoppingListItemEditCommandValidator : AbstractValidator<ShoppingListItemEditCommand>
{
    public ShoppingListItemEditCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(localizationService.GetLocalizedString("MandatoryId"));
        RuleFor(x => x.ProductId).NotEmpty().WithMessage(localizationService.GetLocalizedString("MandatoryProduct"));
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(localizationService.GetLocalizedString("PositiveQuantity"));
    }
}

[Mapper]
public partial class ShoppingListItemEditCommandMapper
{
    [MapperIgnoreSource(nameof(ShoppingListItemEditCommand.Id))]
    public partial void ToShoppingListItem(ShoppingListItemEditCommand command, ShoppingListItem shoppingListItem);
}
