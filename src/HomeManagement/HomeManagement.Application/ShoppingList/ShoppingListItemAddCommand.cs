using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using HomeManagement.Core.Localization;
using HomeManagement.Core.ShoppingList;
using OperationResults;
using Riok.Mapperly.Abstractions;

namespace HomeManagement.Business.ShoppingList;
public sealed record ShoppingListItemAddCommand(string ProductId, double Quantity, string Annotation) : ICommand;

public sealed class ShoppingListItemAddCommandHandler : ICommandHandler<ShoppingListItemAddCommand>
{
    private readonly IDataContext _dataContext;
    private readonly ILocalizationService _localizationService;

    public ShoppingListItemAddCommandHandler(IDataContext dataContext, ILocalizationService localizationService)
    {
        _dataContext = dataContext;
        _localizationService = localizationService;
    }

    public async Task<Result> Handle(ShoppingListItemAddCommand command, CancellationToken cancellationToken)
    {
        if (!_dataContext.GetData<Product>().Any(x => x.Id.Equals(command.ProductId)))
        {
            return Result.Fail(FailureReasons.ItemNotFound, _localizationService.GetLocalizedString("ProductNotFound"));
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
    public ShoppingListItemAddCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage(localizationService.GetLocalizedString("MandatoryProduct"));
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(localizationService.GetLocalizedString("PositiveQuantity"));
    }
}

[Mapper]
public partial class ShoppingListItemAddCommandMapper
{
    public partial ShoppingListItem ToShoppingListItem(ShoppingListItemAddCommand command);
}
