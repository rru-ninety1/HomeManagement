using FluentValidation;
using HomeManagement.Business.Common.CommandQuery;
using HomeManagement.Business.Common.Interfaces;
using HomeManagement.Core.Catalog;
using HomeManagement.Core.Localization;
using Microsoft.EntityFrameworkCore;
using OperationResults;

namespace HomeManagement.Business.Catalog.ProductCategories;

public record ProductCategoryGetSingleQuery(string Id) : IQuery<ProductCategory>;

public class ProductCategoryGetSingleQueryHandler : IQueryHandler<ProductCategoryGetSingleQuery, ProductCategory>
{
    private readonly IReadOnlyDataContext _dataContext;
    private readonly ILocalizationService _localizationService;

    public ProductCategoryGetSingleQueryHandler(IReadOnlyDataContext dataContext, ILocalizationService localizationService)
    {
        _dataContext = dataContext;
        _localizationService = localizationService;
    }

    public async Task<Result<ProductCategory>> Handle(ProductCategoryGetSingleQuery query, CancellationToken cancellationToken)
    {
        var productCategory = await _dataContext.GetData<ProductCategory>()
            .FirstOrDefaultAsync(x => x.Id == query.Id)
            .ConfigureAwait(false);

        if (productCategory == null)
        {
            return Result.Fail(FailureReasons.ItemNotFound, _localizationService.GetLocalizedString("CategoryNotFound"));
        }

        return productCategory;
    }
}

public sealed class ProductCategoryGetSingleQueryValidator : AbstractValidator<ProductCategoryGetSingleQuery>
{
    public ProductCategoryGetSingleQueryValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(localizationService.GetLocalizedString("MandatoryCategory"));
    }
}
