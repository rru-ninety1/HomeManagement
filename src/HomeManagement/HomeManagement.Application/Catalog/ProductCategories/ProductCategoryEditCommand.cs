using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using HomeManagement.Core.Localization;
using OperationResults;

namespace HomeManagement.Business.Catalog.ProductCategories;

public record ProductCategoryEditCommand(string Id, string Description) : ICommand;

public class ProductCategoryEditCommandHandler : ICommandHandler<ProductCategoryEditCommand>
{
    private readonly IDataContext _dataContext;
    private readonly ILocalizationService _localizationService;

    public ProductCategoryEditCommandHandler(IDataContext dataContext, ILocalizationService localizationService)
    {
        _dataContext = dataContext;
        _localizationService = localizationService;
    }

    public async Task<Result> Handle(ProductCategoryEditCommand command, CancellationToken cancellationToken)
    {
        var dbProductCategory = await _dataContext.GetAsync<ProductCategory>(command.Id)
            .ConfigureAwait(false);

        if (dbProductCategory == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, _localizationService.GetLocalizedString("CategoryNotFound"));
        }

        dbProductCategory.Description = command.Description;

        await _dataContext.SaveAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}

public sealed class ProductCategoryEditCommandValidator : AbstractValidator<ProductCategoryEditCommand>
{
    public ProductCategoryEditCommandValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage(localizationService.GetLocalizedString("MandatoryDescription"));
    }
}
}
